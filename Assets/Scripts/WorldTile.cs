using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class WorldTile : Tile
{
    [SerializeField] private bool traversable;

    public bool Traversable => traversable;
}
