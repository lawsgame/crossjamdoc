using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCoordsRenderer : MonoBehaviour
{
    public int cellX = 0;
    public int cellY = 0;
    public int cellZ = 0;
    public Tilemap tilemap = null;
    private TextMeshPro textHolder;


    public void init(int cellX, int cellY, int cellZ, Tilemap tilemap) => init(new Vector3Int(cellX, cellY, cellZ), tilemap);

    public void init(Vector3Int cellPos, Tilemap tilemap)
    {
        if(tilemap != null)
        {
            this.textHolder = GetComponent<TextMeshPro>();
            this.cellX = cellPos.x;
            this.cellY = cellPos.y;
            this.cellZ = cellPos.z;
            this.tilemap = tilemap;

            textHolder.SetText(string.Format("({0}, {1})", cellPos.x, cellPos.y));
            Vector3 worldPosition = tilemap.CellToWorld(cellPos);
            worldPosition.y += 0.25f;
            transform.position = worldPosition;
        }
    }

    
    
}
