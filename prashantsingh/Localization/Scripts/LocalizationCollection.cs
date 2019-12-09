using System.Collections.Generic;
using UnityEngine;
namespace Prashant.Localization
{
    [System.Serializable]
    public class WordData
    {
        public string word;
        public List<TranslatedMeaning> translations;
        public WordData()
        {
            translations = new List<TranslatedMeaning>();
        }
    }

    [System.Serializable]
    public class TranslatedMeaning
    {
        public string meaning;
        public LanguageType _type;

        public TranslatedMeaning(LanguageType tempType = LanguageType.English, string tempMeaning = "")
        {
            _type = tempType;
            meaning = tempMeaning;
        }
    }

    public enum LanguageType
    {
        English,
        Dutch
    }

    [System.Serializable]
    public class LanguageClassCollection
    {
        public LanguageType _type;
        public LanguageData data;
    }

    [CreateAssetMenu(fileName = "Localization", menuName = "Localization Collection", order = 0)]
    public class LocalizationCollection : ScriptableObject
    {
        public List<LanguageClassCollection> languageCollections;
        LanguageClassCollection selectedLanguage;
        public LanguageType languageType;

        public LanguageData defaultWordsData;

        public delegate void LanguageChange();
        public event LanguageChange OnLanguageChanged;

        void EditorLanguageChange()
        {
            ChangeLanguage(LanguageType.Dutch);
        }

        public void ChangeLanguage(LanguageType _type)
        {
            languageType = _type;
            selectedLanguage = languageCollections.Find(x => (x._type == _type));
            if (OnLanguageChanged != null)
                OnLanguageChanged();
        }

        public string TranslateThis(string sourceString)
        {

            if (selectedLanguage == null) selectedLanguage = languageCollections.Find(x => (x._type == languageType));

            if (selectedLanguage._type != languageType)
                selectedLanguage = languageCollections.Find(x => (x._type == languageType));
            string translatedString = sourceString;
            int index = selectedLanguage.data.wordsCollection.FindIndex(x => (x.id.Equals(sourceString)));
            if (index >= 0)
            {
                translatedString = selectedLanguage.data.wordsCollection.Find(x => (x.id.Equals(sourceString))).translatedWord;
            }
            else translatedString = "NAN";
            return translatedString;
        }

        public void UpdateNewWord(string sourceWord)
        {
            if (defaultWordsData != null)
            {
                if (defaultWordsData.wordsCollection.Find(x => (x.id.Equals(sourceWord))) == null)
                {
                    defaultWordsData.wordsCollection.Add(new Translation(sourceWord));
                    UpdateWordsInLanguages();
                }
            }
        }
        public void UpdateWordsInLanguages()
        {
            if (defaultWordsData == null) return;

            for (int count = 0; count < languageCollections.Count; count++)
            {
                languageCollections[count].data.UpdateWordsList(defaultWordsData.wordsCollection);
            }
        }
    }
}