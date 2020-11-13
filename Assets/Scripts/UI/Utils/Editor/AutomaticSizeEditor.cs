using UnityEditor;
using UnityEngine;

namespace UI.Utils.Editor {
    [CustomEditor(typeof(AutomaticSize))]
    public class AutomaticSizeEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            this.DrawDefaultInspector();

            if (GUILayout.Button("Recalc size")) {
                ((AutomaticSize)this.target).AdjustSize();
            }
        }
    }
}
