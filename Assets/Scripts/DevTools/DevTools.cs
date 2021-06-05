using UnityEngine;
using UnityEngine.Tilemaps;

public class DevTools : MonoBehaviour
{
    [SerializeField] private bool displayCellPosOnMouseClick = true;
    [SerializeField] private Tilemap WorldMap;
    [Tooltip("Prefab pour utilser la visualisation le path")]
    public  GameObject PathNodeItem;
    [Tooltip("Prefab pour afficher les coordonnées des tuiles de la carte")]
    public GameObject CellCoordRenderer;


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
