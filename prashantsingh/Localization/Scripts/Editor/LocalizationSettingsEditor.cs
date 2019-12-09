using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Prashant.Localization
{
    public class LocalizationSettingsEditor : EditorWindow
    {
        LanguageType _selectedType;
        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            _selectedType = (LanguageType)EditorGUILayout.EnumPopup(_selectedType, new GUILayoutOption[0]);
            if (GUI.changed)
            {

            }
            GUILayout.EndHorizontal();
        }

        void FetchLanguageFiles()
        {

        }

        void ShowWords()
        {

        }
    }
}