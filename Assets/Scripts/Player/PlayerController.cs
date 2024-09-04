using System.Collections;
using System.Collections.Generic;
using Localization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using ChartboostSDK;

public class PlayerController : MonoBehaviour
{
    bool animationFinished = false;

    public float timer;
    public float timeDelay = 1.0f;
    public bool isHit = false;

    [SerializeField]
    private float movementSpeed = 10;

    //GameManager
    private GameManager gameManager;

    private int posX, posY;
    private int maxHeight, maxWidth;
    public static int lives=3;


    private bool movementDone = true;
    [HideInInspector]
    public bool jumpAnimation = false;
    [HideInInspector]
    public bool deadAnimation = false;

    private SpriteRenderer spriteRenderer;

    
    private Vector3 nextPosition;
    private Vector3 distanceOX;
    private Vector3 distanceOY;
    private bool isEditing = false;
    private bool isSolution = false;
    public int tutorialPosition = 0;
    private Swipe swipeControls;
    public AudioSource audioSource;
    public AudioClip audioClip;
    /* old swipe
    //private Vector3 firstPressPos;
    //private Vector3 secondPressPos;
    //private Vector3 currentSwipe;
    */
    //getters and setters
    #region getters and setter
    public Vector3 DistanceOX
    {
        get
        {
            return distanceOX;
        }

        set
        {
            distanceOX = value;
        }
    }

    public Vector3 DistanceOY
    {
        get
        {
            return distanceOY;
        }

        set
        {
            distanceOY = value;
        }
    }

    public int PosX
    {
        get
        {
            return posX;
        }

        set
        {
            posX = value;
        }
    }

    public int PosY
    {
        get
        {
            return posY;
        }

        set
        {
            posY = value;
        }
    }

    public int MaxHeight
    {
        get
        {
            return maxHeight;
        }

        set
        {
            maxHeight = value;
        }
    }

    public int MaxWidth
    {
        get
        {
            return maxWidth;
        }

        set
        {
            maxWidth = value;
        }
    }
    #endregion

    public List<MoveType> moves;
    private void Awake()
	{
        //Debug.Log(transform.position);
        if(SceneManager.GetActiveScene().name == "Level Editor") {
            isEditing = true;
           
        } else if(SceneManager.GetActiveScene().name == "Solution") 
        {
            isSolution = true;
           
        } else
            {
            swipeControls = GetComponent<Swipe>();
        }

       
       
	}
	
    void Start()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        moves = gameManager.GetComponent<GameManager>().moveTypeList;
    }

    private void Update()
    {
       // Debug.Log(this.gameObject);
        //Debug.Log(transform.position);
        if (isSolution) 
        {
            TutorialRun(tutorialPosition);
        }
        else
        if (!isEditing)
        {
            jumpAnimation = false;
            if (gameManager.GetState() == State.gameWon)
            {
                //spriteRenderer.sprite=;
            }

            if (isHit==true ) 
            {
                timer += Time.deltaTime;

                spriteRenderer.enabled = false;
                deadAnimation = true;

                if (timer>=timeDelay) 
                {
                    isHit = false;
                    timer = 0;
                    deadAnimation = false;
                    
                    spriteRenderer.enabled = true;
                }
            }
            if (gameManager.GetState() != State.gameOver || gameManager.GetState() != State.gameWon || gameManager.GetState() != State.pause || gameManager.GetState()!=State.isTutorialOn)
            {
                if (transform.position == nextPosition)
                {
                    movementDone = true;


                }
                if (!movementDone)
                {

                    transform.position = Vector3.MoveTowards(transform.position, nextPosition, movementSpeed * Time.deltaTime);
                }
                if (movementDone)
                {
                    if (gameManager.GetState() == State.running)
                    {
                        Swipe();

                      
                    }

                }
                if (gameManager.GetState() == State.characterDieing)
                {
                    //Player Dead Animation
                    spriteRenderer.enabled = false;
                    deadAnimation = true;
                    movementDone = true;
                    GameManager.Instance.StartCoroutine(GameManager.Instance.AddDelay(GameManager.Instance.loseMenu));
                }
            }
        }
        else
        {
            
            if (!LevelEditor.instance.editingDone)
            {
                jumpAnimation = false;
                if (transform.position == nextPosition)
                {
                    movementDone = true;
                }
                if (!movementDone)
                {
                    transform.position = Vector3.MoveTowards(transform.position, nextPosition, movementSpeed * Time.deltaTime);
                }
                if (movementDone)
                {
                    //Swipe();
                    InputInEditor();
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, movementSpeed * Time.deltaTime);
            }
        
        }
    }

    #region movement
    public void InputInEditor()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && (posX-1)>=0)
        {
            movementDone = false;
            jumpAnimation = true;
            posX--;
            nextPosition = transform.position - distanceOX;
            moves.Add(MoveType.left);
        }
        else if (Input.GetKey(KeyCode.RightArrow)&& (posX+1)<maxWidth)
        {
            movementDone = false;
            jumpAnimation = true;
            posX++;
            nextPosition = transform.position + distanceOX;
            moves.Add(MoveType.right);

        }
        else if (Input.GetKey(KeyCode.UpArrow) && (posY-1)>=0)
        {
            movementDone = false;
            jumpAnimation = true;
            posY--;
            nextPosition = transform.position + distanceOY;
            moves.Add(MoveType.up);
        }
        else if(Input.GetKey(KeyCode.DownArrow)&&(posY+1)<maxHeight){
            movementDone = false;
            jumpAnimation = true;
            posY++;
            nextPosition = transform.position - distanceOY;
            moves.Add(MoveType.down);
        }
    }

    public void Swipe()
    {

        if(swipeControls.swipeLeft){
            nextPosition = transform.position - distanceOX;
            movementDone = false;
            jumpAnimation = true;
            //posX--;
            //Debug.Log("left swipe" + posX);
        }else if(swipeControls.swipeRight){
            nextPosition = transform.position + distanceOX;
            movementDone = false;
            jumpAnimation = true;
            //posX++;
           // Debug.Log("right swipe" + posX);
        }else if(swipeControls.swipeUp){
            nextPosition = transform.position + distanceOY;
            movementDone = false;
            jumpAnimation = true;
            //posY--;
           // Debug.Log("up swipe" + posY);
        }else if(swipeControls.swipeDown){
            nextPosition = transform.position - distanceOY;
            movementDone = false;
            jumpAnimation = true;
            //posY++;
           // Debug.Log("down swipe" + posY);
        }

        //old inputs

        /*
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            if (Mathf.Abs(currentSwipe.x) >= Mathf.Abs(currentSwipe.y))
            {
                
                if (currentSwipe.x > 0)
                {
                    
                    if (posX + 1 <= maxWidth)
                    {

                        nextPosition = transform.position + distanceOX;
                        movementDone = false;
                        jumpAnimation = true;
                        posX++;
                        Debug.Log("right swipe" + posX);
                    }
                }
                else
                {
                    if (posX - 1 >= -1)
                    {
                        nextPosition = transform.position - distanceOX;
                        movementDone = false;
                        jumpAnimation = true;
                        posX--;
                        Debug.Log("left swipe" + posX);
                    }
                }
            }
            else
            {
                if (currentSwipe.y > 0)
                {
                    if (posY - 1 >= -1)
                    {
                        nextPosition = transform.position + distanceOY;

                        movementDone = false;
                        jumpAnimation = true;
                        posY--;
                        Debug.Log("up swipe" + posY);
                    }
                }
                else
                {
                    if (posY + 1 <= maxHeight)
                    {
                        nextPosition = transform.position - distanceOY;
                        movementDone = false;
                        jumpAnimation = true;
                        posY++;
                        Debug.Log("down swipe" + posY);
                    }
                }
            }
        }
        */
    }
    #endregion
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag=="Margins" || collision.gameObject.tag == "EmptyTile" || collision.gameObject.tag=="Enemy") && !isEditing && !isSolution)
        {
            isHit = true;
            
            if(collision.gameObject.tag=="EmptyTile" || collision.gameObject.tag == "Margins") 
             {
                gameManager.GetComponent<GameManager>().lives = 0;
            }
            else 
            {
                gameManager.GetComponent<GameManager>().lives--;
               
            }
            if (GameManager.Instance.shields != null && SceneManager.GetActiveScene().name == "Gameplay")
            {

                // Debug.Log("TEST ONE");
                //if (lives == 4)
                //{
                //    GameManager.Instance.shields[0].SetActive(true);
                //    GameManager.Instance.shields[1].SetActive(true);
                //    GameManager.Instance.shields[2].SetActive(true);
                //}
                if (gameManager.lives == 3)
                {
                    GameManager.Instance.shields[0].SetActive(false);
                    GameManager.Instance.shields[1].SetActive(true);
                    GameManager.Instance.shields[2].SetActive(true);

                }
                if (gameManager.lives == 2)
                {
                    GameManager.Instance.shields[0].SetActive(false);
                    GameManager.Instance.shields[1].SetActive(false);
                    GameManager.Instance.shields[2].SetActive(true);
                }
                if (gameManager.lives == 1)
                {
                    GameManager.Instance.shields[0].SetActive(false);
                    GameManager.Instance.shields[1].SetActive(false);
                    GameManager.Instance.shields[2].SetActive(false);
                }

            }
           

            SoundManager.Instance.playFallSound();
            if (gameManager.GetComponent<GameManager>().lives == 0)
            {
                gameManager.SetState(State.characterDieing);
                
                GameManager.Instance.StartCoroutine(GameManager.Instance.AddDelay(GameManager.Instance.loseMenu));
            }
            
        }
        else if (collision.gameObject.tag == "EndTile")
        {
            if(isEditing){
                LevelEditor.instance.editingDone = true;
                foreach(TileData tile in LevelEditor.instance.tileDatas){
                    
                    Debug.Log(tile.gridPositionY+" "+tile.gridPositionX+" "+tile.health+" "+tile.type);
                }
                SaveData.levelData = new LevelData();
                SaveData.levelData.moves = moves;
                LevelEditor.instance.EditLevelIndex();
                SaveData.levelData.tiles = LevelEditor.instance.tileDatas;
                if(LevelEditor.instance.snakePos0.isOn == true){
                    SaveData.levelData.snakes.Add(0);
                }
                if (LevelEditor.instance.snakePos1.isOn == true)
                {
                    SaveData.levelData.snakes.Add(1);
                }
                if (LevelEditor.instance.snakePos2.isOn == true)
                {
                    SaveData.levelData.snakes.Add(2);
                }
                for (int i = 0; i < LevelEditor.instance.inputObstacles.Capacity; i++)
                {
                    if (LevelEditor.instance.inputObstacles[i].text == "1")
                    {
                        ObstacleData obstacleData = new ObstacleData();
                        obstacleData.posY = i;
                        obstacleData.startingPoint = new Vector2(-8, 0);
                        obstacleData.endPoint = new Vector2(8, 0);
                        SaveData.levelData.obstacles.Add(obstacleData);
                    }else if(LevelEditor.instance.inputObstacles[i].text == "2")
                    {
                        ObstacleData obstacleData = new ObstacleData();
                        obstacleData.posY = i;
                        obstacleData.startingPoint = new Vector2(-8, 0);
                        obstacleData.endPoint = new Vector2(0, 0);
                        SaveData.levelData.obstacles.Add(obstacleData);
                    }
                }
                SaveData.Save(SaveData.levelData, Application.dataPath + "/Resources/Levels/" + SaveData.levelData.levelID.ToString() + ".json");
            }else if(!isEditing && GameManager.totalTilesHealth <= 0 && !isSolution){
                GameWon();
               // AdsManager.Instance.UpdateAds();
            } else if(!isEditing && GameManager.totalTilesHealth>0){
               if (GameManager.totalTilesHealth > 0)
                {
                    gameManager.SetState(State.characterDieing);
                    GameManager.Instance.StartCoroutine(GameManager.Instance.AddDelay(GameManager.Instance.loseMenu));
                }
                else{
                    //AdsManager.Instance.UpdateAds();
                    GameWon();
                }
            }
        }
    }


    void GameWon()
    {
        

        gameManager.SetState(State.gameWon);
        if (PlayerPrefs.GetInt("currentLevel") == 100 && PlayerPrefs.GetInt("Achievement1")==0)
        {
            // GPGSManager.Instance.UnlockAchievement(GPGSIds.achievement_easy_pack);
            GameManager.Instance.StartCoroutine(GameManager.Instance.OpenUnlockedAchievementPanel(LanguageManager.Get("easypack")));
            PlayerPrefs.SetInt("Achievement1", 1);
        }
        if (PlayerPrefs.GetInt("currentLevelPack2") == 200 && PlayerPrefs.GetInt("Achievement2")==0)
        {
            // GPGSManager.Instance.UnlockAchievement(GPGSIds.achievement_hard_pack);
            GameManager.Instance.StartCoroutine(GameManager.Instance.OpenUnlockedAchievementPanel(LanguageManager.Get("hardpack")));
            PlayerPrefs.SetInt("Achievement2", 1);
        }

        if (PlayerPrefs.GetInt("currentLevelPackBonus") == 300 && PlayerPrefs.GetInt("Achievement3")==0 )
        {
            // GPGSManager.Instance.UnlockAchievement(GPGSIds.achievement_bonus_pack);
            GameManager.Instance.StartCoroutine(GameManager.Instance.OpenUnlockedAchievementPanel(LanguageManager.Get("bonuspack")));
            PlayerPrefs.SetInt("Achievement3", 1);
        }


        if (PlayerPrefs.GetInt("currentLevel")<100 && PlayerPrefs.GetInt("levelPlayed")>=PlayerPrefs.GetInt("currentLevel")) 
        {
            PlayerPrefs.SetInt("currentLevel",PlayerPrefs.GetInt("currentLevel") + 1);
            PlayerPrefs.SetInt("levelIndex",PlayerPrefs.GetInt("currentLevel"));
        }
        else
        if(PlayerPrefs.GetInt("currentLevelPack2") < 200 && PlayerPrefs.GetInt("levelPlayed") >= PlayerPrefs.GetInt("currentLevelPack2")) 
        {
            PlayerPrefs.SetInt("currentLevelPack2",PlayerPrefs.GetInt("currentLevelPack2") + 1);
            PlayerPrefs.SetInt("levelIndex",PlayerPrefs.GetInt("currentLevelPack2"));
        }
        else
        if(PlayerPrefs.GetInt("currentLevelPackBonus") < 300 && PlayerPrefs.GetInt("levelPlayed") >= PlayerPrefs.GetInt("currentLevelPackBonus")) 
        {
            PlayerPrefs.SetInt("currentLevelPackBonus",PlayerPrefs.GetInt("currentLevelPackBonus") + 1);
            PlayerPrefs.SetInt("levelIndex",PlayerPrefs.GetInt("currentLevelPackBonus"));

        }


        GameManager.CanUseShield = false;
        GameManager.Instance.StartCoroutine(GameManager.Instance.AddDelay(GameManager.Instance.winMenu));

        Debug.Log("GAME WON!");
    }

    
    public void TutorialRun(int i) 
    {
        if(i < moves.Count && movementDone==true) 
        {
            movementDone = false;

            if(moves[i] == MoveType.up) 
            {
               
                nextPosition = transform.position + new Vector3(0.0F,3.0F,0.0F);

            } 
            if(moves[i] == MoveType.down) 
            {
               
                nextPosition = transform.position - new Vector3(0.0F,3.0F,0.0F);
                
                
            } 
            if(moves[i] == MoveType.left) 
            {

               
                nextPosition = transform.position - new Vector3(3.0F, 0.0F, 0.0F);
                ;
            } 
            if(moves[i] == MoveType.right) 
            {

                nextPosition = transform.position + new Vector3(3.0F,0.0F,0.0F);
            }
            
            
        }


        transform.position = Vector3.MoveTowards(transform.position, nextPosition,0.5f* movementSpeed * Time.deltaTime);
       // Debug.Log(transform.position);
        if (transform.position == nextPosition && movementDone==false) 
         {
                i++;
                tutorialPosition = i;
                movementDone = true;            
        }
        
    }



}

