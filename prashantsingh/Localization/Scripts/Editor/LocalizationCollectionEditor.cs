using UnityEditor;
using UnityEngine;
namespace Prashant.Localization
{
    [CustomEditor(typeof(LocalizationCollection))]
    public class LocalizationCollectionEditor : Editor
    {
        LocalizationCollection _collection;
        GUIStyle styleBox;

        private void Awake()
        {
            styleBox = new GUIStyle("Box");
            styleBox.padding = new RectOffset(4, 4, 4, 4);
        }
        bool hasDefaultData = false;
        public override void OnInspectorGUI()
        {
            _collection = (LocalizationCollection)target;

            //checking if the default data is present or not
            hasDefaultData = _collection.defaultWordsData != null;

            GUILayout.BeginVertical(styleBox);
            _collection.languageType = (LanguageType)EditorGUILayout.EnumPopup("Current Language", _collection.languageType, new GUILayoutOption[0]);
            GUILayout.BeginHorizontal(styleBox);
            _collection.defaultWordsData = (LanguageData)EditorGUILayout.ObjectField("Default Language", _collection.defaultWordsData, typeof(LanguageData), new GUILayoutOption[0]);
            if (GUILayout.Button("Update"))
            {
                _collection.UpdateWordsInLanguages();
            }
            GUILayout.EndHorizontal();
            if (!hasDefaultData)
            {
                EditorGUILayout.HelpBox("Please select a default language", MessageType.Warning);
            }
            for (int count = 0; count < _collection.languageCollections.Count; count++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Language", new GUILayoutOption[0]);
                _collection.languageCollections[count]._type = (LanguageType)EditorGUILayout.EnumPopup(_collection.languageCollections[count]._type, new GUILayoutOption[0]);
                _collection.languageCollections[count].data = (LanguageData)EditorGUILayout.ObjectField(_collection.languageCollections[count].data, typeof(LanguageData), new GUILayoutOption[0]);
                if (GUILayout.Button("Delete", new GUILayoutOption[0]))
                {
                    _collection.languageCollections.RemoveAt(count);
                    break;
                }
                GUILayout.EndHorizontal();
            }
            Undo.RecordObject(_collection, "localizationCollection");
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
            if (GUILayout.Button("Add"))
            {
                _collection.languageCollections.Add(new LanguageClassCollection());
            }
            GUILayout.EndVertical();
        }
    }
}