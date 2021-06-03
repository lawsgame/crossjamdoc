using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class WorldTile : Tile
{
    [SerializeField] private bool traversable;
    [SerializeField] private bool rootNode = false;
    [SerializeField] private Direction forcedDirection = Direction.None;

    public bool Traversable => traversable;
    public bool RootNode => rootNode;
    public Direction ForcedDirection => forcedDirection;
}
