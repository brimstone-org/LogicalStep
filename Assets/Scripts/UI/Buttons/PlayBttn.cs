using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayBttn : MonoBehaviour
{
    public GameObject AdPanel;
    public GameObject NoAdPanel;
    public Image image;
    public Text levelNumber;
    public int packID=0;
    public Button button;
    public Sprite[] sprites;
    public bool isLocked = true;
    public int levelIndex;
    public WatchAd watchAdScript;

    private void Start()
    {
        watchAdScript = AdPanel.GetComponent<WatchAd>();
        levelIndex = int.Parse(levelNumber.text);

        if (PlayerPrefs.HasKey("currentLevel") || PlayerPrefs.HasKey("currentLevelPack2") || PlayerPrefs.HasKey("currentLevelPackBonus"))
        {
            if ( (PlayerPrefs.GetInt("currentLevel")>= int.Parse(levelNumber.text) && PlayerPrefs.GetInt("currentLevel") <= 100 && packID==1) || (PlayerPrefs.GetInt("currentLevelPack2") >= int.Parse(levelNumber.text) && (PlayerPrefs.GetInt("currentLevelPack2")>100 && PlayerPrefs.GetInt("currentLevelPack2")<=200) && packID == 2) || (PlayerPrefs.GetInt("currentLevelPackBonus") >= int.Parse(levelNumber.text) && (PlayerPrefs.GetInt("currentLevelPackBonus") > 200 && PlayerPrefs.GetInt("currentLevelPackBonus") <= 300) && packID == 3))
            {
                button.interactable = true;
                gameObject.transform.GetChild(0).GetComponent<Text>().enabled = true;
                image.sprite = sprites[0];
                isLocked = false;
            }
            else
            {
                image.sprite = sprites[1];
                gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                button.interactable = true;
                isLocked = true;
            }
        }
    }

    
    public void LoadLevelOnButtonPressed(int x)
    {
        if(isLocked == false) {
            SaveData.Load(x);
            PlayerPrefs.SetInt("levelIndex",x);
            PlayerPrefs.SetInt("levelPlayed",x);
            SceneManager.LoadScene("Gameplay");
        } else 
        if ((levelIndex == PlayerPrefs.GetInt("currentLevel") + 1 && (packID==1)) || (levelIndex == PlayerPrefs.GetInt("currentLevelPack2") + 1 && (packID==2)) || (levelIndex == PlayerPrefs.GetInt("currentLevelPackBonus") + 1 && (packID==3))) {
            if(isLocked == true) 
            {
                //if(Rewards.Instance.IsAvailable() == true) 
                // {
                //    GameObject rew = Instantiate(AdPanel,transform);
                //    rew.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
           
                //}

               // if(Rewards.Instance.IsAvailable() == false) 
               // {
                    GameObject noAd = Instantiate(NoAdPanel,transform);
                    noAd.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
              //  }
            }
        }

    }

    //public void UnlockLevel(int x) 
    //{

    //    Rewards.OnAdCompleted += unlockLvl;
    //}

    
    public void setUnlock(int level) 
     {
        isLocked = false;
        gameObject.transform.GetChild(0).GetComponent<Text>().enabled = true;
        image.sprite = sprites[0];
    }
    public void unlockLvl(string text) 
    {
        isLocked = false;
        gameObject.transform.GetChild(0).GetComponent<Text>().enabled = true;
        image.sprite = sprites[0];
       // Rewards.OnAdCompleted -= unlockLvl;

        if(packID == 1) {
            PlayerPrefs.SetInt("currentLevel",levelIndex);

        } else
            if(packID == 2) {
            PlayerPrefs.SetInt("currentLevelPack2",levelIndex);
        } else
            if(packID == 3)
            { PlayerPrefs.SetInt("currentLevelPackBonus",levelIndex); }
         
    }    
}
