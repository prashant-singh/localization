using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prashant.Localization;
using System;

public class LocalizationExample : MonoBehaviour
{
    [SerializeField] Text txtObj;
    [SerializeField] Button btnUpdate;
    [SerializeField] Dropdown languageOptions;
    [SerializeField] LocalizationCollection localization;

    private void Awake()
    {
        btnUpdate.onClick.AddListener(OnUpdateLanguage);
        SetupDropDown();
        languageOptions.value = (int)localization.languageType;
    }

    void SetupDropDown()
    {
        List<Dropdown.OptionData> listOfOptions = new List<Dropdown.OptionData>();
        for (int count = 0; count < 3; count++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = ((LanguageType)count).ToString();
            listOfOptions.Add(option);
        }
        languageOptions.options = listOfOptions;
    }

    void OnUpdateLanguage()
    {
        localization.ChangeLanguage((LanguageType)Enum.Parse(typeof(LanguageType),languageOptions.options[languageOptions.value].text));
    }


}
