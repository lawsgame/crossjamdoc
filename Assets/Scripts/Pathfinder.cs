using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder : MonoBehaviour
{
    public static readonly int TILE_Z = 0;

    [SerializeField] private Tilemap worldMap;

    private Node rootNode = null;
    private Dictionary<Vector3Int, Node> network = null;

    public Tilemap WorldMap => worldMap;
    
    public void Start()
    {
        if (network == null)
            BuildNetwork();
    }

    public Node FindNode(Vector3Int cellpos)
    {
        if(network == null)
            BuildNetwork();
        return network[cellpos];
    }

    void BuildNetwork()
    {

        rootNode = FindRootNode();
        Debug.Log(string.Format("Build path network from root note at {0}", rootNode.Position));
        network = new Dictionary<Vector3Int, Node>();
        ExpandNetwork(rootNode, null);

        foreach (KeyValuePair<Vector3Int, Node> entry in network)
        {
            Debug.Log(entry.Value.ToLongString());
        }
    }

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

                    Node foundRootNode = new Node(pos.x, pos.y, pos.z);
                    Debug.Log(string.Format("find root node at {0}", foundRootNode.Position));
                    return foundRootNode;
                }
            }
        }
        return null;
    }

    void ExpandNetwork(Node currentNode, Node previousNode)
    {
        Vector3Int initCell = new Vector3Int(currentNode.Position.x, currentNode.Position.y, TILE_Z);
        if (!worldMap.HasTile(initCell))
            return;
        WorldTile initTile = worldMap.GetTile<WorldTile>(initCell);
        if (!initTile.Traversable)
            return;
        if (network.ContainsKey(currentNode.Position))
            return;

        network.Add(currentNode.Position, currentNode);
        if (previousNode != null)
            previousNode.Children.Add(currentNode);

        Node nextNode1 = new Node(currentNode.Position.x + 1, currentNode.Position.y, currentNode.Position.z);
        if (!nextNode1.Equals(previousNode))
            ExpandNetwork(nextNode1, currentNode);
        Node nextNode2 = new Node(currentNode.Position.x, currentNode.Position.y + 1, currentNode.Position.z);
        if (!nextNode2.Equals(previousNode))
            ExpandNetwork(nextNode2, currentNode);
        Node nextNode3 = new Node(currentNode.Position.x - 1, currentNode.Position.y, currentNode.Position.z);
        if (!nextNode3.Equals(previousNode))
            ExpandNetwork(nextNode3, currentNode);
        Node nextNode4 = new Node(currentNode.Position.x, currentNode.Position.y - 1, currentNode.Position.z);
        if (!nextNode4.Equals(previousNode))
            ExpandNetwork(nextNode4, currentNode);

    }



    public class Node
    {
        public Vector3Int Position { get; set; }
        public List<Node> Children { get; }

        private int nextNodeId = 0;

        public Node(int x, int y, int z)
        {
            Position = new Vector3Int(x, y, z);
            Children = new List<Node>();
        }

        public List<Node> GetPath() => GetPath(new List<Node>());

        private List<Node> GetPath(List<Node> path)
        {
            path.Add(this);
            return (HasNext()) ? this.Next().GetPath(path) : path;
        }

        public bool HasNext() => Next() != null;

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

        public Node Next() => (EndOfPath()) ? null : Children[nextNodeId];

        public bool EndOfPath() => Children.Count == 0;

        public bool IsIntersection() => Children.Count > 1;

        public Vector3 GetWorldPos(Tilemap map)
        {
            Vector3 worldPos = map.CellToWorld(Position);
            worldPos.y += 0.25f;
            return worldPos;
        }

        public Direction GetDirection()
        {
            Vector3Int dir = Position - Next().Position;
            if (dir.x == -1)
                return Direction.West;
            else if (dir.x == 1)
                return Direction.East;
            else if (dir.y == -1)
                return Direction.South;
            else if (dir.y == -1)
                return Direction.North;
            return Direction.None;
        }

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
        public override string ToString() => string.Format("Node {0}", Position);
    }
}
