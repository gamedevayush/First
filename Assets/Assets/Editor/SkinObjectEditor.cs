using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkinObject))]
public class SkinObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SkinObject skinObject = (SkinObject)target;

        if(GUILayout.Button("Assign Cost"))
        {
            skinObject.AssignCosting();
        }
    }

}
