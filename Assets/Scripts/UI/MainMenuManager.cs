using System;
using Localization;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

//using ChartboostSDK;

public class MainMenuManager : MonoBehaviour {

    [SerializeField]
    private GameObject InitialMenu;
    [SerializeField]
    private GameObject SelectPackMenu;
    [SerializeField]
    private GameObject EasyPacks;
    [SerializeField]
    private GameObject HardPacks;
    [SerializeField]
    private GameObject BonusPacks;
    [SerializeField]
    private GameObject AchievementsMenu;
    [SerializeField]
    private GameObject LanguagesMenu;
    public static bool IAPLevelActive;
    public Text EasyAchievement;
    public Text HardAchievement;
    public Text BonusAchievement;
    public Image Easy;
    public Image Hard;
    public Image Bonus;
    [SerializeField] private Transform[] _flags;

    public enum Achievements
    {
        Easypack,
        HardPack,
        BonusPack
    }

    private void Awake()
	{
        InitialMenu.SetActive(true);
        SelectPackMenu.SetActive(false);
        EasyPacks.SetActive(false);
        //Chartboost.showInterstitial(CBLocation.Default);

    }

    private void Start()
    {
        SoundManager.Instance.playBackgroundSound();
        LanguageManager.Instance.PopulateFlags(_flags);
        CheckAchievements();
    }

    /// <summary>
    /// checks and updates achievement UI
    /// </summary>
    private void CheckAchievements()
    {
        EasyAchievement.font = LanguageManager.GetFont();
        HardAchievement.font = LanguageManager.GetFont();
        BonusAchievement.font = LanguageManager.GetFont();
        EasyAchievement.text = LanguageManager.Get("easypacknot");
        HardAchievement.text = LanguageManager.Get("hardpacknot");
        BonusAchievement.text = LanguageManager.Get("bonuspacknot");
        Easy.color = Color.grey;
        Hard.color = Color.grey;
        Bonus.color = Color.grey;
        if (PlayerPrefs.GetInt("Achievement1") == 1)
        {
            // GPGSManager.Instance.UnlockAchievement(GPGSIds.achievement_easy_pack);
            UnlockAchievement(Achievements.Easypack);
        }
        if (PlayerPrefs.GetInt("Achievement2") == 1)
        {
            // GPGSManager.Instance.UnlockAchievement(GPGSIds.achievement_hard_pack);
            UnlockAchievement(Achievements.HardPack);
        }

        if (PlayerPrefs.GetInt("Achievement3") == 1)
        {
            // GPGSManager.Instance.UnlockAchievement(GPGSIds.achievement_bonus_pack);
            UnlockAchievement(Achievements.BonusPack);
        }
    }

    public void UnlockAchievement(Achievements achievment)
    {
        switch (achievment)
        {
            case Achievements.Easypack:
                EasyAchievement.font = LanguageManager.GetFont();
                EasyAchievement.text = LanguageManager.Get("easypack");
                Easy.color = Color.white;
                break;
            case Achievements.HardPack:
                HardAchievement.font = LanguageManager.GetFont();
                HardAchievement.text = LanguageManager.Get("hardpack");
                Hard.color = Color.white;
                break;
            case Achievements.BonusPack:
                BonusAchievement.font = LanguageManager.GetFont();
                BonusAchievement.text = LanguageManager.Get("bonuspack");
                Bonus.color = Color.white;
                break;
        }
    }

    public void PlayButtonPressed(){
        InitialMenu.SetActive(false);
        SelectPackMenu.SetActive(true);
    }

    public void OpenAchievementsPanel()
    {
        AchievementsMenu.SetActive(true);
        InitialMenu.SetActive(false);
    }

    public void OpenLanguagesPanel()
    {
        LanguagesMenu.SetActive(true);
        InitialMenu.SetActive(false);
    }


    public void BackButtonPressed() 
     {
     
        if(SelectPackMenu.activeSelf == true){
            SelectPackMenu.SetActive(false);
            InitialMenu.SetActive(true);
        }else if(EasyPacks.activeSelf == true){
            EasyPacks.SetActive(false);
            SelectPackMenu.SetActive(true);
        }else if (HardPacks.activeSelf == true)
        {
            HardPacks.SetActive(false);
            SelectPackMenu.SetActive(true);
        }else if (BonusPacks.activeSelf == true)
        {
            BonusPacks.SetActive(false);
            SelectPackMenu.SetActive(true);
        }
        else if (AchievementsMenu.activeSelf)
        {
            AchievementsMenu.SetActive(false);
            InitialMenu.SetActive(true);
        }
        else if (LanguagesMenu.activeSelf)
        {
            LanguagesMenu.SetActive(false);
            InitialMenu.SetActive(true);
        }


    }
    public void EasyPackPressed(){
        SelectPackMenu.SetActive(false);
        EasyPacks.SetActive(true);
    }
    public void HardPackPressed(){
        SelectPackMenu.SetActive(false);
        HardPacks.SetActive(true);
    }
    public void BonusPackPressed()
    {
       
            SelectPackMenu.SetActive(false);
            BonusPacks.SetActive(true);
       
    }
}
