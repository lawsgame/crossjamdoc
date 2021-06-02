using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pathfinder))]
public class PathFinderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Pathfinder pathfinder = target as Pathfinder;

    }

}
