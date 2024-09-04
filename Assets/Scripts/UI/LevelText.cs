using System.Collections;
using System.Collections.Generic;
using Localization;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    GetComponent<Text>().font = LanguageManager.GetFont();
        GetComponent<Text>().text =LanguageManager.Get("level") + " " + PlayerPrefs.GetInt("levelPlayed").ToString();
	}

}
