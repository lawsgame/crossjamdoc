using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder : MonoBehaviour
{
    public readonly int TILE_Z = 0;

    [SerializeField] private int initTileX;
    [SerializeField] private int initTileY;
    [SerializeField] private Tilemap worldMap;

    private Node rootNode;
    private Dictionary<Vector2Int, Node> network;


    public Node RootNode => rootNode;

    void Start()
    {
        BuildNetwork();
    }

    public void BuildNetwork()
    {
        rootNode = new Node(initTileX, initTileY);
        network = new Dictionary<Vector2Int, Node>();
        ExpandNetwork(rootNode, null);

        foreach (KeyValuePair<Vector2Int, Node> entry in network)
        {
            Debug.Log(entry.Value.ToLongString());
        }
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
        if(previousNode != null)
            previousNode.Children.Add(currentNode);

        Node nextNode1 = new Node(currentNode.Position.x + 1, currentNode.Position.y);
        if (!nextNode1.Equals(previousNode))
            ExpandNetwork(nextNode1, currentNode);
        Node nextNode2 = new Node(currentNode.Position.x, currentNode.Position.y + 1);
        if (!nextNode2.Equals(previousNode))
            ExpandNetwork(nextNode2, currentNode);
        Node nextNode3 = new Node(currentNode.Position.x - 1, currentNode.Position.y);
        if (!nextNode3.Equals(previousNode))
            ExpandNetwork(nextNode3, currentNode);
        Node nextNode4 = new Node(currentNode.Position.x, currentNode.Position.y - 1);
        if (!nextNode4.Equals(previousNode))
            ExpandNetwork(nextNode4, currentNode);

    }



    public class Node
    {
        public Vector2Int Position { get; set; }
        public List<Node> Children { get;}

        private int nextNodeId = 0;

        public Node(int x, int y)
        {
            Position = new Vector2Int(x, y);
            Children = new List<Node>();
        }

        public List<Node> GetPath() => GetPath(new List<Node>());

        private List<Node> GetPath(List<Node> path)
        {
            path.Add(this);
            return (HasNext()) ? this.Next().GetPath(path) : path;
        }

        public bool HasNext() => Next() == null;

        public void Switch()
        {
            if(nextNodeId < Children.Count - 1)
            {
                nextNodeId++;
            }
            else
            {
                nextNodeId = 0;
            }
        }

        public Node Next() => (EndOfPath()) ?  null : Children[nextNodeId];

        public bool EndOfPath() => Children.Count == 0;

        public bool IsIntersection() => Children.Count > 1;

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
