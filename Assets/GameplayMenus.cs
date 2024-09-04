using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMenus : MonoBehaviour {
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    public GameObject winMenu, loseMenu, pauseMenu, hintMenu,rewardMenu;

	private void Update()
	{
        if(gameManager.GetState() == State.gameWon) {
            winMenu.SetActive(true);
        } else if(gameManager.GetState() == State.characterDieing || gameManager.GetState() == State.gameOver) {
            loseMenu.SetActive(true);
            //rewardMenu.SetActive(true);
        }
        //else if (gameManager.GetState()==State.noReward) {
           // rewardMenu.SetActive(false);
            loseMenu.SetActive(true);
       // }
    }
}
