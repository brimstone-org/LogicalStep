using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Localization;
using UnityEngine.UI;


public enum State
{
    pause, running, characterDieing,gameOver, gameWon, isTutorialOn, isSolutionPopOn
}
public class GameManager: MonoBehaviour
{

    public static GameManager Instance;

    public List<MoveType> moveTypeList; 
    public static bool shieldIAP;
    public GameObject player;
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject pauseMenu;
    public GameObject rewardPanel;
    public GameObject AdsManager;
    public GameObject hintMenu;
    public GameObject[] shields;
    public GameObject AchievementAwardMenu;
    public Text ShieldText;
    public Text AchievementUnlockedText;
    public Button GetShieldsPause;
    public Text GetShieldsPauseText;
    public Button GetShieldsGameOver;
    public Text GetShieldsGameOverText;
    public int[,] tiles;
    public int[,] tileHealth;
    [HideInInspector]
    public int[] obstacles;
    [HideInInspector]
    public int[] snakes;
    public static int totalTilesHealth;
    private bool playerAlive = false;
    private GameObject startingTile;

    private State state;
    public void SetState(State state) { this.state = state; }
    public State GetState() { return state; }

    public static bool CanUseShield;
    //
    public int lives = 1;

    public delegate void LevelEvent(int level);


    //public static event LevelEvent OnLevelWin;
    //public static event LevelEvent OnPackFinish;
    //public static event LevelEvent OnLevelChange;

    

    private void Awake() {
       // PlayerPrefs.DeleteAll();

        if (Instance != null)
        {
            Destroy(Instance);
            Instance = this;
        }
        else
        {
            Instance = this;
        }

        state = State.running;
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            GetShieldsPause.interactable = true;
            GetShieldsPauseText.font = LanguageManager.GetFont();
            GetShieldsPauseText.text = LanguageManager.Get("shields");
            GetShieldsGameOver.interactable = true;
            GetShieldsGameOverText.font = LanguageManager.GetFont();
            GetShieldsGameOverText.text = LanguageManager.Get("shields");
            if (CanUseShield)
            {
                Usehields();
            }
        }
        else if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            CanUseShield = false;
        }
        
        //CanUseShield = true; //can use shield at the beginning of the game
        Time.timeScale = 1.0f;
        //create keys
        if(PlayerPrefs.HasKey("levelIndex")==false) 
        {
            PlayerPrefs.SetInt("levelIndex",0);
        }

        if(PlayerPrefs.HasKey("tutorialStatus") == false) {
            PlayerPrefs.SetInt("tutorialStatus",0);
        }
        if(PlayerPrefs.HasKey("adsState") == false) {
            PlayerPrefs.SetInt("adsState",0);

        }
        if(PlayerPrefs.HasKey("soundStatus") == false) {
            PlayerPrefs.SetInt("soundStatus",1);
        }
        if(PlayerPrefs.HasKey("currentLevel") == false) {
            PlayerPrefs.SetInt("currentLevel",1);
           // PlayerPrefs.SetInt("currentLevel", 100);
        }

        if(PlayerPrefs.HasKey("currentLevelPack2") == false) {
            PlayerPrefs.SetInt("currentLevelPack2",101);
           // PlayerPrefs.SetInt("currentLevelPack2", 200);
        }

        if(PlayerPrefs.HasKey("currentLevelPackBonus") == false) {
            PlayerPrefs.SetInt("currentLevelPackBonus",201);
           // PlayerPrefs.SetInt("currentLevelPackBonus", 300);
        }

        if(PlayerPrefs.HasKey("levelPlayed") == false) {
            PlayerPrefs.SetInt("levelPlayed",1);
        }
        // PlayerPrefs.SetInt("currentLevel", 100);
         //PlayerPrefs.SetInt("currentLevelPack2", 200);
         //PlayerPrefs.SetInt("currentLevelPackBonus", 300);
        //procedrually generate gameplay
        if (SceneManager.GetActiveScene().name == "Gameplay" || SceneManager.GetActiveScene().name == "Solution") {
            LoadLevel();
        }

        //if(PlayerPrefs.GetInt("com.tedrasoft.logicalstep.shield",0) == 1) {
        //    GameManager.shieldIAP = true;
      // }
        //Assert.IsNotNull(player);
      
    }

    /// <summary>
    /// disables and enables
    /// </summary>
    public void Usehields()
    {
        if (state == State.characterDieing) //if we got shields in the game over
        {
            CanUseShield = true;
        }
        else
        {
            if (PlayerPrefs.GetInt("levelIndex") <= 200)
            {
                lives = 2;
                if (shields != null && SceneManager.GetActiveScene().name == "Gameplay")
                {
                       
                       shields[0].SetActive(false);
                       shields[1].SetActive(false);
                        shields[2].SetActive(true);
                

                }
              
            }
            else if (PlayerPrefs.GetInt("levelIndex") > 200)
            {

                lives = 3;
                if (shields != null && SceneManager.GetActiveScene().name == "Gameplay")
                {
                    shields[0].SetActive(false);
                    shields[1].SetActive(true);
                    shields[2].SetActive(true);
                }
            }

           
        }
        CanUseShield = true;
        GetShieldsPause.interactable = false;
        GetShieldsPauseText.font = LanguageManager.GetFont();
        GetShieldsPauseText.text = LanguageManager.Get("shieldsNo");
        GetShieldsGameOver.interactable = false;
        GetShieldsGameOverText.font = LanguageManager.GetFont();
        GetShieldsGameOverText.text = LanguageManager.Get("shieldsNo");
    }
    public IEnumerator OpenUnlockedAchievementPanel(string text)
    {
        yield return new WaitForSecondsRealtime(0.4f);
        AchievementUnlockedText.font = LanguageManager.GetFont();
        AchievementUnlockedText.text = text;
        AchievementAwardMenu.SetActive(true);
    }
  
    public void InitializePlayer(GameObject startTile)
    {
        Instantiate(player, startTile.transform.position + player.transform.position, Quaternion.identity);
    }
    private void Update() {

        //OneSignalManager.instance.SendTag("pack1",PlayerPrefs.GetInt("currentLevel").ToString());
        //OneSignalManager.instance.SendTag("pack1",PlayerPrefs.GetInt("currentLevel").ToString());
        //OneSignalManager.instance.SendTag("pack2",PlayerPrefs.GetInt("currentLevelPack2").ToString());
        //OneSignalManager.instance.SendTag("pack3",PlayerPrefs.GetInt("currentLevelPackBonus").ToString());

        // Debug.Log("GAME MANAGER IS ON");
        //Debug.Log(player);



        //Debug.Log(shieldIAP);
        //if(shieldIAP == true && shields != null && SceneManager.GetActiveScene().name=="Gameplay") {
            
        //       // Debug.Log("TEST ONE");
        //        if(lives == 4) {
        //            shields[0].SetActive(true);
        //            shields[1].SetActive(true);
        //            shields[2].SetActive(true);
        //        }
        //        if(lives == 3) {
        //            shields[0].SetActive(false);
        //            shields[1].SetActive(true);
        //            shields[2].SetActive(true);
                    
        //        }
        //        if(lives == 2) {
        //            shields[0].SetActive(false);
        //            shields[1].SetActive(false);
        //            shields[2].SetActive(true);
        //        }
        //        if(lives == 1) {
        //            shields[0].SetActive(false);
        //            shields[1].SetActive(false);
        //            shields[2].SetActive(false);
        //        }
            
        //} else if(shields.Length != 0 && SceneManager.GetActiveScene().name=="Gameplay") {
        //    lives = 1;
        //    shields[0].SetActive(false);
        //    shields[1].SetActive(false);
        //    shields[2].SetActive(false);
        //}

    }


    void InitializeTiles() {
        tiles = new int[6,3];
        tileHealth = new int[6,3];
        for(int i = 0;i < 6;i++) {
            for(int j = 0;j < 3;j++) {
                tiles[i,j] = 0;
                tileHealth[i,j] = 0;
            }
        }
    }


    void LoadLevel() {
        totalTilesHealth = 0;
        InitializeTiles();
        LevelData level = LevelDataLoaded.levelData;
        for(int i = 0;i < level.tiles.Count;i++) {
            if(level.tiles[i].type == TileType.NormalTile) {
                tiles[level.tiles[i].gridPositionY,level.tiles[i].gridPositionX] = 2;
                tileHealth[level.tiles[i].gridPositionY,level.tiles[i].gridPositionX] = level.tiles[i].health;
                totalTilesHealth += level.tiles[i].health;
            } else if(level.tiles[i].type == TileType.StartTile) {
                tiles[level.tiles[i].gridPositionY,level.tiles[i].gridPositionX] = 1;
            } else if(level.tiles[i].type == TileType.EndTile) {
                tiles[level.tiles[i].gridPositionY,level.tiles[i].gridPositionX] = 3;
            }
        }

        if(SceneManager.GetActiveScene().name != "Solution") {
            obstacles = new int[level.obstacles.Count];
            for(int i = 0;i < obstacles.Length;i++) {
                obstacles[i] = level.obstacles[i].posY;
            }
            snakes = new int[level.snakes.Count];
            for(int i = 0;i < snakes.Length;i++) {
                snakes[i] = level.snakes[i];
            }
        }

        
        for(int i = 0;i < level.moves.Count;i++) {
            moveTypeList.Add(level.moves[i]);
        }
    }

    public IEnumerator AddDelay(GameObject loadObj) {
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("Delay Added");
        loadObj.SetActive(true);
        Time.timeScale = 0.0f;
    }

}

