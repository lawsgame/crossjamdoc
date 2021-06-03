using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Pathfinder;

[CustomEditor(typeof(Pathfinder))]
public class PathFinderEditor : Editor
{

    private static readonly string PATH_NODE_ITEM_TAG = "PathNodeItem";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Pathfinder pathfinder = target as Pathfinder;

        ShowPathway(pathfinder);
        HidePathway(pathfinder);
    }

    void ShowPathway(Pathfinder pathfinder)
    {
        if (GUILayout.Button("Show Pathways"))
        {
            GameObject pathfinderHolder = pathfinder.gameObject;
            foreach (KeyValuePair<Vector3Int, Node> entry in pathfinder.Network())
            {
                GameObject pathNodeItem = GameObject.Instantiate(pathfinder.PathNodeItem, pathfinderHolder.transform);
                pathNodeItem.tag = PATH_NODE_ITEM_TAG;
                pathNodeItem.GetComponent<PathNodeDirectionRenderer>().Render(entry.Value, pathfinder.WorldMap);

            }

        }
    }

    void HidePathway(Pathfinder pathfinder)
    {
        if (GUILayout.Button("hide Pathways"))
        {
            foreach (Transform child in pathfinder.gameObject.transform)
            {
                if (child.tag == PATH_NODE_ITEM_TAG)
                    Destroy(child.gameObject);
            }

        }
    }
}
