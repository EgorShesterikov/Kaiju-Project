using UnityEditor;
using UnityEditor.UI;

namespace Kaiju
{
    [CustomEditor(typeof(ArrowButton), true)]
    public class ArrowButtonEditor : ButtonEditor
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("arrow"));
        }
    }
}