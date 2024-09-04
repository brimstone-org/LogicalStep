using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBttn : MonoBehaviour {

    [SerializeField]
    private GameObject PauseMenu;
    [SerializeField]
    private GameManager gameManager;

    public void OpenPauseMenu(){
        gameManager.SetState(State.pause);
        Time.timeScale = 0.0f;
        PauseMenu.SetActive(true);
    }
}
