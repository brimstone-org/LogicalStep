using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchAd : MonoBehaviour 
{

    GameObject level;
    GameObject[] globalBackButton;
    public GameObject panel;
    public Button watch;
    public Button goBack;
    bool init = false;
    public bool isPlaying = false;
    

    private void Awake() {
        panel = this.gameObject;
        level = transform.parent.gameObject;
        
    }
    // Use this for initialization
   
	

    public void disableUI() 
    {
        foreach(GameObject i in globalBackButton) {
            i.GetComponent<Button>().interactable = true;
        }
        GameObject.Destroy(panel);
    }

    public void destroyOnAd() 
    {
        foreach(GameObject i in globalBackButton) {
            i.GetComponent<Button>().interactable = true;
        }
        GameObject.Destroy(panel);
    }

    public void deActivateUI() 
    {
        panel.SetActive(false);
    }

   // public void playAd() 
        //{ Rewards.Instance.PlayAd(); }



    private void Update() 
        {
        globalBackButton = GameObject.FindGameObjectsWithTag("BackButtons");

        if(panel.activeSelf==true) 
        {
            foreach (GameObject i in globalBackButton) 
            {
                i.GetComponent<Button>().interactable = false;
            }
        } 
    }

    //public void ShowAd() 
    //{
    //    isPlaying = true;
    //    if(Rewards.Instance.IsAvailable()) {
    //        Rewards.Instance.PlayAd();  
    //        disableUI();
    //    }
    //    Rewards.OnAdCompleted+= level.GetComponent<PlayBttn>().unlockLvl;   
    //}

    
}
