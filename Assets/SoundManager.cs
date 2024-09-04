using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour 
{
    public static event System.Action OnSoundTrigger;
    public static SoundManager Instance { get; set; }

    private bool soundOn=true;
    private bool musique = false;
    private float volume = 0.5f;
    private Stack<AudioSource> cancellAudioList = new Stack<AudioSource>();
    [SerializeField]
    AudioSource backgroundSound;

    [SerializeField]
    AudioSource snakeSound;

    [SerializeField]
    AudioSource tileSound;

    [SerializeField]
    AudioSource fallSound;

    public bool SoundOn 
    {
        get {
            return soundOn;
        }
         set {
            soundOn = value;
            SoundUpdate();
        }
    }

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(Instance != this)
            DestroyImmediate(gameObject);

        if(PlayerPrefs.GetInt("soundStatus",1) == 1) {
            soundOn = true;
        }
        else 
        {
            soundOn = false;
        }
    }

    private void Start()
    {
        if(SoundOn && musique)
            backgroundSound.Play();

        OnSoundTrigger += OnTrigger;
        cancellAudioList.Push(backgroundSound);
        cancellAudioList.Push(snakeSound);
        cancellAudioList.Push(tileSound);
        cancellAudioList.Push(fallSound);
    }

    void updateCancelSound(AudioSource targetSound) 
    {
        cancellAudioList.Push(targetSound);
    }

    public void stopSounds() 
    {
        foreach (AudioSource sounds in cancellAudioList) 
            { sounds.Stop(); }
    }

    public void SoundTrigger() 
        { SoundOn = !SoundOn; }

    public void playBackgroundSound()
    {
        if(soundOn==false)
            backgroundSound.Stop();
        else
        backgroundSound.Play();
    }

    public void playSnakeSound() 
    {
        if(soundOn==false)
            snakeSound.Stop();
        else
        snakeSound.Play();
    }

    public void playFallSound()
    {
        if(soundOn==false)
            fallSound.Stop();
        else
        fallSound.Play();
    }

    public void playTileSound() 
    {
        if(soundOn == false)
            tileSound.Stop();
        else
            tileSound.PlayOneShot(tileSound.clip);
    }  

    public void triggerSound() 
    {
        if (soundOn==true) 
        {
            PlayerPrefs.SetInt("soundStatus",0);
            soundOn = false;
        }
        else 
        {
            playBackgroundSound();
            PlayerPrefs.SetInt("soundStatus",1);
            soundOn = true;
        }
    }

    void SoundUpdate() 
        { int sound = soundOn ? 1 : 0;
        PlayerPrefs.Save();
        if(OnSoundTrigger != null)
            OnSoundTrigger();
    }

    public void OnTrigger ()
    {
        if (SoundOn && musique) 
        {
            backgroundSound.Play();
        }
        else 
        {
            backgroundSound.Stop();
        }
    }
        
 }

   
