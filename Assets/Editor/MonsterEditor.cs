using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterMovement))]
public class MonsterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MonsterMovement m = target as MonsterMovement;

        Move(m);
    }

    private void Move(MonsterMovement m)
    {
        if (GUILayout.Button("Resume Moving"))
        {
            m.StartMoving();
        }
    }
}
