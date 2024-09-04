using System.Collections;
using System.Collections.Generic;
using Localization;
using UnityEngine;

public class LanguageSelect : MonoBehaviour
{
    [SerializeField] private string _languageTag;
    [SerializeField] private TranslatedText _backButton;
    void Start()
    {
        if (LanguageManager.Instance.CheckIfThisIsCurrentLanguage(_languageTag))
        {
            LanguageManager.Instance.RefreshLanguageMenu();
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
       
    }

    public void SelectThisLang()
    {
        LanguageManager.Instance.RefreshLanguageMenu();
        LanguageManager.Instance.SetLanguage(_languageTag);
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        PlayerPrefs.SetString("Language", _languageTag);
        _backButton.UpdateText();
    }
}
