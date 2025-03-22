using UnityEditor;
using UnityEditor.UI;

namespace Kaiju
{
    [CustomEditor(typeof(ArrowSlider), true)]
    public class ArrowSliderEditor : SliderEditor
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