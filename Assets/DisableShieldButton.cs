using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableShieldButton : MonoBehaviour {

    public GameObject shieldButton;

	// Use this for initialization
	void Start () {
        if(GameManager.shieldIAP == true) {
            shieldButton.SetActive(false);
        } else
                if(GameManager.shieldIAP == false) {
            shieldButton.SetActive(true);
        }
    }
	
	
}
