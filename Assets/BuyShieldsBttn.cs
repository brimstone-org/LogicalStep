using System.Collections;
using System.Collections.Generic;
using Localization;
using UnityEngine;
using UnityEngine.UI;

public class BuyShieldsBttn : MonoBehaviour 
{
    // public GameObject thisPanel;
    
    public ShieldPopUp loadPanel;


    public void LoadBuyPanel()
    {
        loadPanel.Shield1.SetActive(false);
        loadPanel.Shield2.SetActive(false);
        loadPanel.Shield3.SetActive(false);
        if (PlayerPrefs.GetInt("levelIndex") <= 200)
        {
            loadPanel.Description.font = LanguageManager.GetFont();
            loadPanel.Description.text = LanguageManager.Get("oneshield");
            loadPanel.Shield1.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("levelIndex") > 200)
        {
            loadPanel.Description.font = LanguageManager.GetFont();
            loadPanel.Description.text = LanguageManager.Get("twoshield");
            loadPanel.Shield1.SetActive(true);
            loadPanel.Shield2.SetActive(true);
        }
        loadPanel.gameObject.SetActive(true);
        
    }

}
