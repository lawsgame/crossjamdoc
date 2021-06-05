using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterMovement))]
public class MonsterMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MonsterMovement m = target as MonsterMovement;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Play"))
        {
            if (m.Moving())
            {
                m.Resume();
            }
            else
            {
                m.Initialize();
                m.StartMoving();
            }
            
        }

        if (GUILayout.Button("pause"))
        {
            m.Pause();
        }

        if(GUILayout.Button("Reset Position"))
        {
            m.ResetPosition();
        }
        GUILayout.EndHorizontal();
    }

    
}
