using ElementsSystem;
using Global.Tools;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Editor.EditorWindows
{
    public class ElementGeneratorWindow : EditorWindow
    {
        private string _jsonSourcePath = "";
        private string _jsonSourcePathDisk = Application.dataPath;
        private string _elementsObjectPath = @"\Objects\Elements";
        private string _elementsObjectPathDisk = Application.dataPath + @"\Objects\Elements";

        private ReorderableList _exceptionsList;
        private ReorderableList _elementsList;
        private Vector2 _exceptionsScrollPosition;
        private Vector2 _elementsScrollPosition;
        private Vector2 _windowScrollPosition;
        private bool _exceptionsFoldout;
        private bool _outputFoldout;
        private bool _overwriteExisting;

        [MenuItem("Assets/Generate Element Files")]
        private static void GenerateElementFiles() =>
            GetWindow(typeof(ElementGeneratorWindow), false, "Element Generator");

        private void OnGUI()
        {
            _windowScrollPosition = EditorGUILayout.BeginScrollView(_windowScrollPosition);
            EditorGUILayout.BeginVertical(EditorStyles.inspectorDefaultMargins, GUILayout.MinWidth(300));

            DrawSettingsSection();
            DrawOutputSection();
            DrawActionButtons();

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        #region Settings section

        private void DrawSettingsSection()
        {
            EditorGUILayout.Space();
            GUILayout.Label("Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            DrawJsonPathSettings();
            DrawObjectPathSettings();
            DrawOverwriteToggle();
            EditorGUILayout.EndVertical();
        }

        private void DrawJsonPathSettings()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Path to json file with elements:", GUILayout.MinWidth(200));

            _jsonSourcePathDisk = Application.dataPath + GUILayout.TextField(
                _jsonSourcePath,
                GUILayout.MaxWidth(300),
                GUILayout.MinWidth(200)
            );

            if (GUILayout.Button("Change...", GUILayout.Width(100)))
            {
                var newPath = EditorUtility.OpenFilePanel(
                    "Select json file with elements",
                    _jsonSourcePathDisk,
                    "json"
                );

                if (!string.IsNullOrEmpty(newPath))
                    _jsonSourcePathDisk = newPath;
            }

            _jsonSourcePath = _jsonSourcePathDisk.Replace(Application.dataPath, "");

            EditorGUILayout.EndHorizontal();
        }

        private void DrawObjectPathSettings()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Path to save generated objects:", GUILayout.MinWidth(200));

            _elementsObjectPathDisk = Application.dataPath + GUILayout.TextField(
                _elementsObjectPath,
                GUILayout.MaxWidth(300),
                GUILayout.MinWidth(200)
            );

            if (GUILayout.Button("Change...", GUILayout.Width(100)))
            {
                var newPath = EditorUtility.OpenFolderPanel(
                    "Select folder to save generated objects",
                    _elementsObjectPathDisk,
                    ""
                );

                if (!string.IsNullOrEmpty(newPath))
                    _elementsObjectPathDisk = newPath;
            }

            _elementsObjectPath = _elementsObjectPathDisk.Replace(Application.dataPath, "");

            EditorGUILayout.EndHorizontal();
        }

        private void DrawOverwriteToggle()
        {
            EditorGUILayout.Space();
            _overwriteExisting = EditorGUILayout.ToggleLeft("Overwrite existing files", _overwriteExisting);
            EditorGUILayout.Space();
            GUILayout.Label("The paths shown above are relative to the Assets folder.",
                EditorStyles.centeredGreyMiniLabel);
        }

        #endregion

        #region Ouput section

        private void DrawOutputSection()
        {
            if (!ElementGenerator.IsDoneGenerating) 
                return;
            
            GUILayout.Label("Output", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            DrawExceptionsList();
            DrawGeneratedObjectsList();
            EditorGUILayout.EndVertical();
        }

        private void DrawExceptionsList()
        {
            _exceptionsFoldout = EditorGUILayout.Foldout(_exceptionsFoldout, "Exceptions");

            if (!_exceptionsFoldout || _exceptionsList == null) 
                return;
            
            if (_exceptionsList.count > 0)
            {
                _exceptionsScrollPosition = EditorGUILayout.BeginScrollView(_exceptionsScrollPosition);
                _exceptionsList.DoLayoutList();
                EditorGUILayout.EndScrollView();
            }
            else
            {
                GUILayout.Label("No exceptions found!", EditorStyles.centeredGreyMiniLabel);
            }
        }

        private void DrawGeneratedObjectsList()
        {
            if (_elementsList == null || _elementsList.count == 0)
            {
                GUILayout.Label(
                    "Error: No elements generated, see exceptions for more info!",
                    EditorStyles.centeredGreyMiniLabel
                );
            }
            else
            {
                _outputFoldout = EditorGUILayout.Foldout(_outputFoldout, "Generated Objects");

                if (!_outputFoldout) 
                    return;
                
                if (_elementsList.count <= 0) 
                    return;
                    
                _elementsScrollPosition = EditorGUILayout.BeginScrollView(_elementsScrollPosition);
                _elementsList.DoLayoutList();
                EditorGUILayout.EndScrollView();
            }
        }

        #endregion

        #region Action section

        private void DrawActionButtons()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            DrawGenerateObjectsButton();
            DrawSaveObjectsButton();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        private void DrawGenerateObjectsButton()
        {
            if (!GUILayout.Button("Generate objects", GUILayout.MaxWidth(150))) 
                return;
            
            ElementGenerator.TryDecodeAndCacheElementsJson(_jsonSourcePathDisk);

            _exceptionsList = ElementGenerator.Exceptions.GetImmutableGUIList<string>();
            _elementsList = ElementGenerator.Elements.GetImmutableGUIList<ElementObject>();
        }

        private void DrawSaveObjectsButton()
        {
            GUI.enabled = ElementGenerator.IsDoneGenerating;

            if (GUILayout.Button("Save objects", GUILayout.MaxWidth(150)))
            {
                ElementGenerator.SaveElementsToFolder(_elementsObjectPath, _overwriteExisting);
            }

            GUI.enabled = true;
        }

        #endregion
    }
}