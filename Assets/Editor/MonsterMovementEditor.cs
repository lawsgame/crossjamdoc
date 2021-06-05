using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterMovement))]
public class MonsterMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MonsterMovement m = target as MonsterMovement;

        if (GUILayout.Button("Start"))
        {
            m.Initialize();
            m.StartMoving();
        }

        if (GUILayout.Button("pause"))
        {
            m.Pause();
        }

        if (GUILayout.Button("Resume"))
        {
            m.Resume();
        }
    }

    
}
