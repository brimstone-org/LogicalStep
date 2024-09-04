using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBttn : MonoBehaviour {

    [SerializeField]
    private GameObject HintMenu;
    [SerializeField]
    private GameManager gameManager;


    public void OpenHintMenu(){
        gameManager.SetState(State.pause);
        Time.timeScale = 0.0f;
        HintMenu.SetActive(true);
    }
}
