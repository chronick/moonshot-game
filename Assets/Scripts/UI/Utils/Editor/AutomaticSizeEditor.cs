using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AutomaticSize))]
public class AutomaticSizeEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Recalc size")) {
            ((AutomaticSize)target).AdjustSize();
        }
    }
}
