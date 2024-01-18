#if UNITY_EDITOR

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace ElementsSystem
{
    /// <summary>
    /// Static class responsible for generating and managing ElementObjects.
    /// </summary>
    public static class ElementGenerator
    {
        // ConcurrentBag is used to store exceptions encountered during the generation process.
        private static readonly ConcurrentBag<string> ConcurrentExceptions = new();
        // Array of JTokens representing the children of the root JSON token.
        private static JToken[] _tokenChildren;

        /// <summary>
        /// Indicates whether the element generation process is completed.
        /// </summary>
        public static bool IsDoneGenerating { get; private set; }

        /// <summary>
        /// Array of ElementObjects generated from JSON data.
        /// </summary>
        public static ElementObject[] Elements { get; private set; }

        /// <summary>
        /// Collection of exceptions encountered during the generation process.
        /// </summary>
        public static string[] Exceptions => ConcurrentExceptions.ToArray();

        /// <summary>
        /// Tries to decode a JSON file and cache its contents into ElementObjects.
        /// </summary>
        /// <param name="path">The file path of the JSON file.</param>
        public static void TryDecodeAndCacheElementsJson(string path)
        {
            IsDoneGenerating = false;
            ConcurrentExceptions.Clear();

            try
            {
                var jsonToken = JToken.Parse(File.ReadAllText(path));

                _tokenChildren = jsonToken.Children().ToArray();
                Elements = new ElementObject[_tokenChildren.Length];

                for (var i = 0; i < _tokenChildren.Length; i++)
                    Elements[i] = ScriptableObject.CreateInstance<ElementObject>();

                var parallelLoopResult = Parallel.For(
                    0,
                    _tokenChildren.Length,
                    DecodeAndCacheElementsJson
                );

                IsDoneGenerating = parallelLoopResult.IsCompleted;
            }
            catch (Exception e)
            {
                ConcurrentExceptions.Add($"Failed to parse json file at {path}: {e.Message}");
                IsDoneGenerating = true;
            }
        }
        
        // Private method to decode and cache elements from JSON for a given index.
        private static void DecodeAndCacheElementsJson(int index)
        {
            var elementObject = Elements[index];
            var elementObjectFields = elementObject
                .GetType()
                .GetFields(
                    BindingFlags.Default |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance
                );
            var element = _tokenChildren[index].ToObject<JObject>();

            foreach (var fieldInfo in elementObjectFields)
            {
                if (!element.TryGetValue(fieldInfo.Name, out var token))
                {
                    ConcurrentExceptions.Add($"Could not find {fieldInfo.Name} in object index {index + 1}!");
                    continue;
                }

                if (TryProcessField(fieldInfo, token, index, out var value))
                    fieldInfo.SetValue(elementObject, value);
            }
        }

        // Tries to process a JSON token into the corresponding field of an ElementObject.
        private static bool TryProcessField(FieldInfo fieldInfo, JToken token, int index, out object value)
        {
            value = null;

            try
            {
                value = token switch
                {
                    JValue jValue => jValue.ToObject(fieldInfo.FieldType),
                    JArray jArray when fieldInfo.FieldType == typeof(Color32) => new Color32(
                        jArray[0].ToObject<byte>(), jArray[1].ToObject<byte>(), jArray[2].ToObject<byte>(),
                        jArray[3].ToObject<byte>()),
                    _ => null
                };

                return value != null;
            }
            catch (Exception e)
            {
                ConcurrentExceptions.Add(
                    $"Failed to parse {fieldInfo.Name} as {fieldInfo.FieldType.Name} for object index {index + 1}: {e.Message}"
                );

                return false;
            }
        }

        /// <summary>
        /// Saves generated elements to a specified folder in Unity's Asset Database.
        /// </summary>
        /// <param name="folderPath">Path of the folder to save elements.</param>
        /// <param name="overwrite">Whether to overwrite existing assets.</param>
        public static void SaveElementsToFolder(string folderPath, bool overwrite)
        {
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                var folderPathParts = folderPath.Split('\\');
                var currentPath = "Assets";

                foreach (var path in folderPathParts)
                {
                    if (string.IsNullOrEmpty(path))
                        continue;

                    var newPath = $"{currentPath}\\{path}";

                    if (!AssetDatabase.IsValidFolder(newPath))
                        AssetDatabase.CreateFolder(currentPath, path);

                    currentPath = newPath;
                }
            }

            foreach (var element in Elements)
            {
                var elementPath = $"Assets{folderPath}\\{element.Element}.asset";

                if (overwrite && AssetDatabase.LoadAssetAtPath<ElementObject>(elementPath) != null)
                    AssetDatabase.DeleteAsset(elementPath);

                AssetDatabase.CreateAsset(element, elementPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
#endif