using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWrapper : MonoBehaviour 
{
    public GameObject watchAdPanel;
    public GameManager gameManager;
    public void LoadTutorial() 
    {
        if(TutorialPanel.Instance.isCreated == false) 
        {
            TutorialPanel.Instance.gameManager.SetState(State.pause);
            Time.timeScale = 0.0f;
            TutorialPanel.Instance.LoadTutorial();
            TutorialPanel.Instance.isCreated = true;
        }
    }
    public void continueToNext() 
    {
        TutorialPanel.Instance.isCreated = true;
        TutorialPanel.Instance.buttonNext();
    }

    public void closePanel() 
    {
        TutorialPanel.Instance.isCreated = false;
        TutorialPanel.Instance.gameManager.SetState(State.running);
        Time.timeScale = 1.0f;
        TutorialPanel.Instance.onButtonClose();
    }

    //public void LoadAdPanel() 
    //{
    //    if(AdsManager.Instance.solutionsCount % 3 == 0 && !(Application.internetReachability == NetworkReachability.NotReachable)) 
    //    {
           

    //       if(Rewards.Instance.IsAvailable("hint") == true) 
    //        {
    //            Debug.Log("HasRewards");
    //            gameManager.SetState(State.isSolutionPopOn);
    //            watchAdPanel.SetActive(true);
    //        }
    //    }
    //    else 
    //    {
    //            AdsManager.Instance.solutionsCount++;
    //            GetComponent<SolutionUI>().reLoadSolution();
    //    }
        
        
    //}

    public void closeAdPanel() 
    {
        watchAdPanel.SetActive(false);
        gameManager.SetState(State.running);
        Time.timeScale = 1.0f;
    }
}
