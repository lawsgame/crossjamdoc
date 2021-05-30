using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pathfinder))]
public class PathFinderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Pathfinder pathfinder = target as Pathfinder;

        RebuildNetwork(pathfinder);
    }

    private void RebuildNetwork(Pathfinder pathfinder)
    {
        if (GUILayout.Button("Rebuild Network"))
        {
            pathfinder.BuildNetwork();
        }
    }
}
