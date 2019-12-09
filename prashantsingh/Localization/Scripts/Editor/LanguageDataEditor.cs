using UnityEditor;
using UnityEngine;
namespace Prashant.Localization
{
    [CustomEditor(typeof(LanguageData))]
    public class LanguageDataEditor : Editor
    {
        LanguageData _languageData;
        GUIStyle styleBox;

        private void Awake()
        {
            styleBox = new GUIStyle("Box");
            styleBox.padding = new RectOffset(4, 4, 4, 4);
        }

        public override void OnInspectorGUI()
        {
            _languageData = (LanguageData)target;
            GUILayout.BeginHorizontal();
            GUILayout.Label("ID", new GUILayoutOption[0]);
            GUILayout.Label("Translation", new GUILayoutOption[0]);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical(styleBox);
            for (int wordsCount = 0; wordsCount < _languageData.wordsCollection.Count; wordsCount++)
            {
                GUILayout.BeginHorizontal();
                _languageData.wordsCollection[wordsCount].id = EditorGUILayout.TextField(_languageData.wordsCollection[wordsCount].id, GUILayout.MinWidth(100));
                _languageData.wordsCollection[wordsCount].translatedWord = EditorGUILayout.TextArea(_languageData.wordsCollection[wordsCount].translatedWord, GUILayout.MinWidth(500), GUILayout.MinHeight(50), GUILayout.MaxWidth(600));
                GUILayout.BeginVertical();
                if (GUILayout.Button("Delete Word", GUILayout.MaxWidth(100)))
                {
                    _languageData.wordsCollection.RemoveAt(wordsCount);
                    break;
                }
                GUILayout.BeginHorizontal();
                if (wordsCount > 0)
                {
                    if (GUILayout.Button("^", GUILayout.MaxWidth(50)))
                    {
                        _languageData.MoveUp(wordsCount);
                    }
                }
                if (wordsCount < _languageData.wordsCollection.Count - 1)
                {
                    if (GUILayout.Button("v", GUILayout.MaxWidth(50)))
                    {
                        _languageData.MoveDown(wordsCount);
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add Word"))
            {
                _languageData.wordsCollection.Add(new Translation());
            }
            Undo.RecordObject(_languageData, "localizationCollection");
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
            GUILayout.EndVertical();
        }
    }
}