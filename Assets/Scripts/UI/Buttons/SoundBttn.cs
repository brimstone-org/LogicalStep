using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBttn : MonoBehaviour 
 {
    public Button button;
    public Image image;
    public Sprite soundOn;
    public Sprite soundOff;

    private void Start() {
        UpdateImage();
    }
    public void UpdateImage() 
    {
        if (SoundManager.Instance.SoundOn==false) 
        {
            PlayerPrefs.SetInt("soundStatus",0);
            image.sprite = soundOff;
        }
        else 
        {
            PlayerPrefs.SetInt("soundStatus",1);
            SoundManager.Instance.playBackgroundSound();
            image.sprite = soundOn;
        }
    }

   
    
    public void OnClick() 
    {
        SoundManager.Instance.SoundTrigger();
        UpdateImage();
    }



}
