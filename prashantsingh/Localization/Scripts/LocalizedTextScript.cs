using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Prashant.Localization
{
    public class LocalizedTextScript : MonoBehaviour
    {
        Text _text;
        TextMeshProUGUI _txtPRO;
        public Translation wordData;
        // public string sourceString = "";
        public LocalizationCollection localizationFile;


        private void Awake()
        {
            GetTextComponent();
            OnLanguageChanged();
            localizationFile.OnLanguageChanged += OnLanguageChanged;
        }

        void GetTextComponent()
        {
            if (_text != null || _txtPRO != null) return;
            if (GetComponent<Text>() != null)
            {
                _text = GetComponent<Text>();
            }
            else if (GetComponent<TextMeshProUGUI>() != null)
            {
                _txtPRO = GetComponent<TextMeshProUGUI>();
            }
        }

        private void OnLanguageChanged()
        {
            wordData = new Translation(wordData.id, localizationFile.TranslateThis(wordData.id));
            UpdateText();
        }

        public void UpdateText()
        {
            GetTextComponent();
            if (_txtPRO != null)
            {
                _txtPRO.text = wordData.translatedWord;
            }
            if (_text != null)
            {
                _text.text = wordData.translatedWord;
            }
        }

        public void UpdateWordToDatabase()
        {
            // GetTextComponent();
            // if (_txtPRO == null)
            //     wordData.id = _text.text;
            // else
            //     sourceString = _txtPRO.text;
            // localizationFile.UpdateNewWord(sourceString);
        }
    }
}