using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayMenusBttns : MonoBehaviour {

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    public GameObject packMenu;

    public void ExitButtonPressed(){
        //gameObject.transform.parent.transform.parent.gameObject.SetActive(false);
        gameManager.SetState(State.running);
        Time.timeScale = 1.0f;
    }
    public void ResumeButtonPressed(){
        
        Time.timeScale = 1.0f;
        gameManager.pauseMenu.SetActive(false);
        //gameObject.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);
        gameManager.SetState(State.running);
    }
    public void RestartButtonPressed(){
        gameManager.SetState(State.running);
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1.0f;
    }
    public void MainMenuButtonPressed(){
        SceneManager.LoadScene("MainMenu");
        gameManager.SetState(State.running);
        Time.timeScale = 1.0f;
    }
    public void LoadNextLevel(){
        
        int x = PlayerPrefs.GetInt("levelPlayed")+1;
        PlayerPrefs.SetInt("levelIndex",x);

        if(/*(x == 201 && MainMenuManager.IAPLevelActive == false) || */(x==301)) 
        {
            SceneManager.LoadScene("MainMenu");
            packMenu = GameObject.FindGameObjectWithTag("MainMenuManager");
            gameManager.SetState(State.running);
            Time.timeScale = 1.0f;
        }
        else 
        {
            SaveData.Load(x);
            PlayerPrefs.SetInt("levelPlayed",x);
            SceneManager.LoadScene("Gameplay");
        }
    }
}
