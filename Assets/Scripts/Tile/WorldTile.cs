using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class WorldTile : Tile
{
    [SerializeField] private bool traversable;
    [SerializeField] private bool rootNode = false;

    public bool Traversable => traversable;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        base.StartUp(position, tilemap, go);
        if (rootNode)
        {
            Data.rootNodeLocation = position;
            Debug.Log(string.Format("Set root node location {0}", Data.rootNodeLocation));
        }
        return true;
    }
}
