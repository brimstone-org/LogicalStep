using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadTutorial : MonoBehaviour {

    private void Awake() {
        GetComponent<Button>().onClick.AddListener(delegate { LoadTutorialLevel(); });
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadTutorialLevel()
    {
        SaveData.Load(-1);
        PlayerPrefs.SetInt("levelPlayed",0);
        SceneManager.LoadScene("Gameplay");
        
    }
}
