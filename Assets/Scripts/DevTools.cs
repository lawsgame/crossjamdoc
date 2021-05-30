using UnityEngine;
using UnityEngine.Tilemaps;

public class DevTools : MonoBehaviour
{
    [SerializeField] private bool displayCellPosOnMouseClick = true;
    [SerializeField] private Tilemap WorldMap;

    private Camera worldCamera;

    void Awake()
    {
        worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (displayCellPosOnMouseClick && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            Vector3 worldPos = worldCamera.ScreenToWorldPoint(mousePos);
            Vector3Int cellPos = WorldMap.WorldToCell(worldPos);
            Debug.Log(string.Format("mousepos {0} - worldPos {1} - cellPos {2} - hasTile?{3}", mousePos, worldPos, cellPos, WorldMap.HasTile(cellPos)));
        }
    }
}
