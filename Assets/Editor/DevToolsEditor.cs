using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Pathfinder;

[CustomEditor(typeof(DevTools))]
public class DevToolsEditor : Editor
{

    private static readonly string PATH_NODE_ITEM_TAG = "DevTools_PathNodeItem";
    private static readonly string MAP_COORD_RENDERER = "DevTools_MapCoordRenderer";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DevTools devtools = target as DevTools;
        Pathfinder pathfinder = devtools.GetComponent<Pathfinder>();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Path Network ");
        ShowPathway(devtools, pathfinder);
        HidePathway(devtools, pathfinder);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Map Cells Coords ");
        ShowMapCoordinates(devtools, pathfinder);
        HideMapCoordinates(devtools, pathfinder);
        GUILayout.EndHorizontal();

    }

    

    void ShowPathway(DevTools devtools, Pathfinder pathfinder)
    {
        if (GUILayout.Button("Show"))
        {
            GameObject pathfinderHolder = pathfinder.gameObject;
            foreach (KeyValuePair<Vector3Int, Node> entry in pathfinder.Network())
            {
                GameObject pathNodeItem = GameObject.Instantiate(devtools.PathNodeItem, pathfinderHolder.transform);
                pathNodeItem.tag = PATH_NODE_ITEM_TAG;
                pathNodeItem.GetComponent<PathNodeDirectionRenderer>().Render(entry.Value, pathfinder.WorldMap);

            }
        }
    }

    void HidePathway(DevTools devtools, Pathfinder pathfinder)
    {
        if (GUILayout.Button("Hide"))
        {
            foreach (Transform child in pathfinder.gameObject.transform)
            {
                if (child.tag == PATH_NODE_ITEM_TAG)
                    Destroy(child.gameObject);
            }

        }
    }

    private void ShowMapCoordinates(DevTools devtools, Pathfinder pathfinder)
    {
        if (GUILayout.Button("Show"))
        {
            Tilemap worldMap = pathfinder.WorldMap;
            foreach (var pos in worldMap.cellBounds.allPositionsWithin)
            {
                if (worldMap.HasTile(pos))
                {
                    GameObject mapCoordRenderer = GameObject.Instantiate(devtools.CellCoordRenderer, pathfinder.transform);
                    mapCoordRenderer.tag = MAP_COORD_RENDERER;
                    mapCoordRenderer.GetComponent<MapCoordsRenderer>().init(pos, pathfinder.WorldMap);

                }
            }
        }
    }

    private void HideMapCoordinates(DevTools devtools, Pathfinder pathfinder)
    {
        if (GUILayout.Button("Hide"))
        {
            foreach (Transform child in pathfinder.gameObject.transform)
            {
                if (child.tag == MAP_COORD_RENDERER)
                    Destroy(child.gameObject);
            }

        }
    }
}
