using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace SMoonJail
{
    [CustomEditor(typeof(Area))]
    public class AreaEditor : UnityEditor.Editor
    {
        public Area script;

        public override void OnInspectorGUI()
        {
            //script.T = EditorGUILayout.Slider(
            //    label: "T",
            //    value: script.T,
            //    leftValue: 0,
            //    rightValue: 1
            //    );
            //script.width = EditorGUILayout.FloatField(
            //    label: "Width",
            //    value: script.width
            //    );

            EditorGUILayout.LabelField(
                label: "T: " + script.T.ToString()
                );
            EditorGUILayout.LabelField(
                label: "Width: " + script.width.ToString()
                );
        }

        private void OnEnable()
        {
            script = target as Area;
        }
    }
}
