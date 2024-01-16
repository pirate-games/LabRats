using Audio;
using UnityEditor;
using UnityEngine;

namespace Editor.EditorWindows
{
#if UNITY_EDITOR

    [CustomEditor(typeof(AudioEvent), true)]
    public class AudioEventEditor : UnityEditor.Editor
    {
        [SerializeField] private AudioSource preview;
        private const int ExtraGUISpace = 10; 

        private void OnEnable()
        {
            // creates an instance of an audio source to play a preview of a sound directly in the editor 
            preview = EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave,
                typeof(AudioSource)).GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            DestroyImmediate(preview.gameObject);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(ExtraGUISpace);
            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            
            if (GUILayout.Button("Preview")) ((AudioEvent)target).Play(preview);

            GUILayout.Space(ExtraGUISpace);

            if (GUILayout.Button("Stop")) preview.Stop();
            
            EditorGUI.EndDisabledGroup();
        }
    }
#endif
}