using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class WorldTile : Tile
{
    [SerializeField] private bool traversable;
    [SerializeField] private bool rootNode = false;

    public bool Traversable => traversable;
    public bool RootNode => rootNode;
}
