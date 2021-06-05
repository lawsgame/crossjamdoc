using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder : MonoBehaviour
{
    public static readonly int TILE_Z = 0;
    private static Vector3Int ONE_X = new Vector3Int(1, 0, 0);
    private static Vector3Int ONE_Y = new Vector3Int(0, 1, 0);
    private static Vector3Int MINUS_ONE_X = new Vector3Int(-1, 0, 0);
    private static Vector3Int MINUS_ONE_Y = new Vector3Int(0, -1, 0);

    [SerializeField] private Tilemap worldMap;


    private Node rootNode = null;
    private Dictionary<Vector3Int, Node> network = null;

    public Tilemap WorldMap => worldMap;
    public Dictionary<Vector3Int, Node> Network() {
        if (network == null)
            BuildNetwork();
        return network;
     }

    public void Start()
    {
        if (network == null)
            BuildNetwork();
    }

    public Node FindNode(Vector3Int cellpos)
    {
        if(network == null)
            BuildNetwork();

        Node foundNode = null;
        if (network.ContainsKey(cellpos))
            foundNode = network[cellpos];
        return foundNode;
    }

    void BuildNetwork()
    {

        rootNode = FindRootNode();
        Debug.Log(string.Format("Build path network from root note at {0}", rootNode.Position));

        network = new Dictionary<Vector3Int, Node>();
        network.Add(rootNode.Position, rootNode);
        ExpandNetwork(rootNode, rootNode.Position + ONE_X);
        ExpandNetwork(rootNode, rootNode.Position + MINUS_ONE_X);
        ExpandNetwork(rootNode, rootNode.Position + ONE_Y);
        ExpandNetwork(rootNode, rootNode.Position + MINUS_ONE_Y);

        Debug.Log(PathToString());
    }

    public Vector3 FindSpawn() => FindRootNode().GetWorldPos(worldMap);

    public Node FindRootNode()
    {
        if(rootNode != null)
        {
            return rootNode;
        }

        // scan through the whole tilemap to found the first node with the status of Root Node

        foreach (var pos in worldMap.cellBounds.allPositionsWithin)
        {
            WorldTile worldTile;
            if (worldMap.HasTile(pos))
            {
                worldTile = worldMap.GetTile<WorldTile>(pos);
                if (worldTile.RootNode)
                {
                    Node foundRootNode = new Node(pos, worldTile);
                    Debug.Log(string.Format("find root node at {0}", foundRootNode.Position));
                    return foundRootNode;
                }
            }
        }
        return null;
    }


    void ExpandNetwork(Node previousNode, Vector3Int currentPos)
    {
        if (!worldMap.HasTile(currentPos))
            return;

        WorldTile currentTile = worldMap.GetTile<WorldTile>(currentPos);
        
        if (!currentTile.Traversable)
            return;

        Node currentNode = new Node(currentPos, currentTile);
        if (!previousNode.MoveTowardsAllowed(currentNode))
            return;

        if (network.ContainsKey(currentNode.Position))
        {
            currentNode = network[currentNode.Position];
            if (currentNode.Children.Contains(previousNode))
            {
                //Debug.Log(string.Format("current {0} already known and is already parent of {1} => stop building path", currentNode.ToString(), previousNode.ToString()));
                return;
            }
            else
            {
                //Debug.Log(string.Format("current {0} already known but is not parent of previous node {1} => make previous node parent of the current and stop building path", currentNode.ToString(), previousNode.ToString()));
                previousNode.Children.Add(currentNode);
                return;
            }
            
        }

        //Debug.Log(string.Format("Add node " + currentNode.ToString()));
        network.Add(currentNode.Position, currentNode);
        if (previousNode != null)
            previousNode.Children.Add(currentNode);

        Vector3Int nextPos1 = currentNode.Position + ONE_X;
        if (!nextPos1.Equals(previousNode.Position))
            ExpandNetwork(currentNode, nextPos1);
        Vector3Int nextPos2 = currentNode.Position + ONE_Y;
        if (!nextPos2.Equals(previousNode.Position))
            ExpandNetwork(currentNode, nextPos2);
        Vector3Int nextPos3 = currentNode.Position + MINUS_ONE_X;
        if (!nextPos3.Equals(previousNode.Position))
            ExpandNetwork(currentNode, nextPos3);
        Vector3Int nextPos4 = currentNode.Position + MINUS_ONE_Y;
        if (!nextPos4.Equals(previousNode.Position))
            ExpandNetwork(currentNode, nextPos4);

    }


    string PathToString()
    {
        String pathstring = "Network built: \n";
        foreach (KeyValuePair<Vector3Int, Node> entry in network)
        {
            pathstring += entry.Value.ToLongString() + "\n";
        }
        return pathstring;
    }





    public class Node
    {
        public Vector3Int Position { get;  }
        public List<Node> Children { get; }
        public WorldTile WorldTile { get; }

        private int nextNodeId = 0;

        public Node(Vector3Int nodePos, WorldTile worldTile)
        {
            Position = nodePos;
            Children = new List<Node>();
            this.WorldTile = worldTile;
        }


        public List<Node> GetPath() => GetPath(new List<Node>());

        private List<Node> GetPath(List<Node> path)
        {
            path.Add(this);
            return (HasNext()) ? this.Next().GetPath(path) : path;
        }

        public bool HasNext() => Next() != null;

        public Node Next() => (EndOfPath()) ? null : Children[nextNodeId];

        public void Switch()
        {
            if (nextNodeId < Children.Count - 1)
            {
                nextNodeId++;
            }
            else
            {
                nextNodeId = 0;
            }
        }

        public bool EndOfPath() => Children.Count == 0;

        public bool IsIntersection() => Children.Count > 1;

        public Vector3 GetWorldPos(Tilemap map)
        {
            Vector3 worldPos = map.CellToWorld(Position);
            worldPos.y += 0.25f;
            return worldPos;
        }

        /**
         * Give the direction towars the neighbor.
         * WARNING : The method works only for adjacent neighbors
         */
        public Direction GetDirectionTowards(Node neighbor)
        {
            if (neighbor == null)
                return Direction.None;

            Vector3Int dir = neighbor.Position - Position;

            if(dir.magnitude != 1)
            {
                Debug.LogError(string.Format("{0} is not a neighbor node of {1}, can't get direction", this, neighbor));
                return Direction.None;
            }

            if (dir.x == -1)
                return Direction.West;
            else if (dir.x == 1)
                return Direction.East;
            else if (dir.y == -1)
                return Direction.South;
            else if (dir.y == 1)
                return Direction.North;
            return Direction.None;
        }

        public Direction GetDirectionTowardsNext() => GetDirectionTowards(Next());

        public override bool Equals(object obj)
        {
            Node other = obj as Node;
            if (other == null)
                return false;
            else
                return Position.Equals(other.Position);
        }

        public override int GetHashCode() => Position.GetHashCode();
        public string ToLongString() => string.Format("Node {0}, children : {1}", Position, string.Join(", ", Children));
        public override string ToString() => string.Format("Node {0} [{1}]", Position, WorldTile.ForcedDirection);

        /**
         * check if one of the two world tiles / nodes has a forced direction of circulation
         * and wether the direction towars the next node does not infringe on those constraints.
         * If so, return false
         */
        public bool MoveTowardsAllowed(Node nextNode)
        {
            if (nextNode.WorldTile.ForcedDirection == Direction.None && WorldTile.ForcedDirection == Direction.None)
                return true;

            Direction towardsNext = GetDirectionTowards(nextNode);
            
            if (WorldTile.ForcedDirection != Direction.None && towardsNext != WorldTile.ForcedDirection)
                return false;

            if (nextNode.WorldTile.ForcedDirection != Direction.None && nextNode.WorldTile.ForcedDirection != towardsNext)
                return false;

            return true; 
        }
    }
}
