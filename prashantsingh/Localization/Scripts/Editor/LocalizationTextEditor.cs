using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
namespace Prashant.Localization
{
    [CustomEditor(typeof(LocalizedTextScript))]
    public class LocalizationTextEditor : Editor
    {
        LocalizedTextScript _target;
        Text unityText;
        TextMeshProUGUI tmpText;
        private void Awake()
        {
            foundWords = new List<Translation>();
            tmpText = null;
            unityText = null;
            _target = (LocalizedTextScript)target;
        }
        List<Translation> foundWords = new List<Translation>();

        string lastSearchedString = "";
        public override void OnInspectorGUI()
        {
            _target = (LocalizedTextScript)target;
            if (unityText == null && tmpText == null)
            {
                // Debug.Log("both are null");
                if (_target.GetComponent<Text>()) unityText = _target.GetComponent<Text>();
                if (_target.GetComponent<TextMeshProUGUI>()) tmpText = _target.GetComponent<TextMeshProUGUI>();
            }
            // if (tmpText == null)
            //     Debug.Log("tmpText is Null");
            // if (_target.sourceString == null)
            //     Debug.Log("sourcestring is null");
            // if (tmpText != null)
            // {
            //     if (tmpText.text == null) Debug.Log("TEXT IS NULL");
            //     if (!_target.sourceString.Equals(tmpText.text))
            //     {
            //         EditorGUILayout.HelpBox("String doesn't match please click update", MessageType.Warning);
            //     }
            // }
            // else if (unityText != null)
            // {
            //     if (!_target.sourceString.Equals(unityText.text))
            //     {
            //         EditorGUILayout.HelpBox("String doesn't match please click update", MessageType.Warning);
            //     }
            // }
            _target.localizationFile = (LocalizationCollection)EditorGUILayout.ObjectField("Localization", _target.localizationFile, typeof(LocalizationCollection), new GUILayoutOption[0]);

            _target.wordData.id = EditorGUILayout.TextField("ID", _target.wordData.id, new GUILayoutOption[0]);

            if (GUI.changed)
            {
                if (_target.wordData.id.Length > 1)
                {
                    if (!_target.wordData.id.Equals(lastSearchedString))
                    {
                        lastSearchedString = _target.wordData.id;
                        foundWords = FetchSearchedWords(_target.wordData.id);
                    }
                }
            }

            GUILayout.BeginVertical();
            for (int count = 0; count < foundWords.Count; count++)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Select"))
                {
                    _target.wordData = new Translation(foundWords[count].id, foundWords[count].translatedWord);
                    _target.UpdateText();
                    foundWords = new List<Translation>();
                    GUI.FocusControl(null);
                    EditorUtility.SetDirty(_target);
                    break;
                }
                EditorGUILayout.LabelField(foundWords[count].id, GUILayout.MaxWidth(200));
                EditorGUILayout.LabelField(foundWords[count].translatedWord, GUILayout.MaxWidth(200));
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }

        List<Translation> FetchSearchedWords(string _id)
        {
            List<Translation> foundWords = _target.localizationFile.defaultWordsData.wordsCollection.FindAll(x => ((x.id.ToLower().Contains(_id.ToLower())) || x.translatedWord.ToLower().Contains(_id.ToLower())));
            return foundWords;
        }
    }
}