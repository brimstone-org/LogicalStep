using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour 
{
    public static TutorialPanel Instance { get; private set; }
    [SerializeField]
    private bool dontDestroyOnLoad = true;

    public GameManager gameManager;
    public GameObject[] tutorialPanels;
    public GameObject panelInstance;
    Image panel;
    public Image hand;
    public Transform start;
    public Transform end;
    float startX = 0;
    float startY = 0;
    int panelIndex = 0;
    public bool isCreated = false;
    

    private void Awake() 
    {
        if (Instance==null) 
        {
            Instance = this;
            //if (dontDestroyOnLoad)
            //     DontDestroyOnLoad(gameObject);
        }else 
        {
            Destroy(gameObject);
        }
        
        panel = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
    }

    
    
    // Use this for initialization
    public void Start () 
    {
        if(PlayerPrefs.GetInt("levelPlayed",1) == 1 && PlayerPrefs.GetInt("tutorialStatus",0)==0) 
        {
            PlayerPrefs.SetInt("tutorialStatus",1);
            PlayerPrefs.Save();
            panelInstance = Instantiate(tutorialPanels[panelIndex],transform);
            gameManager.SetState(State.isTutorialOn);
            Time.timeScale = 0.0f;
        }
 

    }
    public void LoadTutorial() 
    {
        
        if(isCreated == false) 
        {

            panelIndex = 0;
            panelInstance = Instantiate(tutorialPanels[panelIndex],transform);
        }
    }
    public void buttonNext() 
    {
        GameObject.DestroyImmediate(panelInstance);
        if(panelIndex == tutorialPanels.Length-1)
        {
            GameObject.DestroyImmediate(panelInstance);
            gameManager.SetState(State.running);
            Time.timeScale = 1.0f;
            isCreated = false;
        } 
        else 
        {
            panelIndex++;
            panelInstance = Instantiate(tutorialPanels[panelIndex],transform);
        }
        
    }

    public void onButtonClose() 
    {
        isCreated = false;
        panelIndex = 0;
        GameObject.DestroyImmediate(panelInstance);
        gameManager.SetState(State.running);
        Time.timeScale = 1.0f;
    }
}
