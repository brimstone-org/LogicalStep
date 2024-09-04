using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SolutionUI: MonoBehaviour {

    public GameObject panel;
    public GameManager gameManager;
    public void loadLevel() 
    {
        SaveData.Load(PlayerPrefs.GetInt("levelIndex"));
        //PlayerPrefs.SetInt("levelPlayed",PlayerPrefs.GetInt("currentLevel"));
        SceneManager.LoadScene("Gameplay");
        gameManager.SetState(State.running);
        Time.timeScale = 1.0f;
    }

    private void Update() {
       
        
    }
    public void reLoadSolution() {
        SaveData.Load(PlayerPrefs.GetInt("levelIndex"));
        SceneManager.LoadScene("Solution");
    }

    public void loadSolution(string txt) {
        
        //AdsManager.Instance.solutionsCount++;
        SaveData.Load(PlayerPrefs.GetInt("levelIndex"));
        SceneManager.LoadScene("Solution");
        
    }

    public void resetCounter(string txt) 
    {
        //if(AdsManager.Instance.solutionsCount > 0)
          //  AdsManager.Instance.solutionsCount--;
      //  else
         //   AdsManager.Instance.solutionsCount = 0;
        gameManager.SetState(State.running);
        Time.timeScale = 1.0f;
    }

    
        public void ShowSolAd() 
        {

        //if(Rewards.Instance.IsAvailable("hint")) {
        //    Rewards.Instance.PlayAd("hint");
        //    panel.SetActive(false);
            
        //}
        //Rewards.OnAdCompleted += loadSolution;
        //Rewards.OnAdInterrupted += resetCounter;
        
    }
}



