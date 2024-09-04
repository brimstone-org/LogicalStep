using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int height = 6;

    [SerializeField]
    private int width = 3;

    [SerializeField]
    private GameObject emptyTile;
    [SerializeField]
    private GameObject startingTile;
    [SerializeField]
    private GameObject endTile;
    [SerializeField]
    private GameObject normalTile;
    [SerializeField]
    private GameObject obstacle;
    [SerializeField]
    private GameObject snake;
    [SerializeField]
    Vector2 offset;
    [SerializeField]
    GameManager gameManager;



    private PlayerController playerController;
    private GameObject[,] tiles;

    private bool isSetup = false;


    int startingPosX, startingPosY;

    #region gettersAndSetters
    public int Height
    {
        get
        {
            return height;
        }

        set
        {
            height = value;
        }
    }

    public int Width
    {
        get
        {
            return width;
        }

        set
        {
            width = value;
        }
    }

    public GameObject[,] Tiles
    {
        get
        {
            return tiles;
        }

        set
        {
            tiles = value;
        }
    }
    #endregion


    private float obstaclesLength;
    private float snakesLength;
    void Awake()
    {
        Assert.IsNotNull(gameManager);
    }
	private void Start()
	{
        tiles = new GameObject[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tiles[i, j] = Instantiate(emptyTile) as GameObject;
                tiles[i, j].transform.SetParent(transform);
                tiles[i, j].transform.position = new Vector2(transform.position.x + j * 3.0f, transform.position.y - i * 3.0f);
                tiles[i, j].transform.name = (i * width + j + 1).ToString();
                }
        }
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (gameManager.tiles[i, j] == 1)
                {
                    startingPosX = i;
                    startingPosY = j;
                    startingTile = Instantiate(startingTile, tiles[startingPosX, startingPosY].transform);

                }
                else if (gameManager.tiles[i, j] == 2)
                {
                    GameObject tile = Instantiate(normalTile, tiles[i, j].transform) as GameObject;
                    tile.GetComponent<TileScript>().SetHealth(gameManager.tileHealth[i, j]);

                }
                if (gameManager.tiles[i, j] == 3)
                {
                    Instantiate(endTile, tiles[i, j].transform);
                }
            }

        }
        obstaclesLength = gameManager.obstacles.Length;
        snakesLength = gameManager.snakes.Length;


        /*
        for (int i = 0; i < gameManager.obstacles.Length;i++){
            Vector3 obstaclePosition = new Vector3(0, tiles[gameManager.obstacles[i], 0].transform.position.y, 0);
            Instantiate(obstacle, obstaclePosition, Quaternion.identity);
        }
        for (int i = 0; i < gameManager.snakes.Length;i++){
            Vector3 snakePosition=new Vector3 (tiles[gameManager.snakes[i],0].transform.position.x,0,0);
            Instantiate(snake, snakePosition, Quaternion.identity);
        }*/

	    gameManager.InitializePlayer(startingTile);

	}


    float timer = 1.5f;
	private bool createdController = false;
    private float currentTime = 0;
    private int currentNumberOfObstacles = 0;
    private void Update()
    {

        if (isSetup== false)
        {
            
            if (playerController != null && createdController == true)
            {
                SetUp();
                isSetup = true;

            }
            if (createdController == false)
            {
                playerController = FindObjectOfType<PlayerController>();
                if (playerController != null)
                {
                    createdController = true;
                }
            }
        }

        //instantiation of snakes and obstacles
        if(currentTime <= 0.0f ){
            currentTime = timer;
            if (currentNumberOfObstacles < obstaclesLength)
            {
                Vector3 obstaclePosition = new Vector3(0, tiles[gameManager.obstacles[currentNumberOfObstacles], 0].transform.position.y, 0);
                Instantiate(obstacle, obstaclePosition, Quaternion.identity);
            }
            if(currentNumberOfObstacles < snakesLength){
                Vector3 snakePosition = new Vector3(tiles[0,gameManager.snakes[currentNumberOfObstacles]].transform.position.x, 0, 0);

                Instantiate(snake, snakePosition, Quaternion.identity);
            }
            currentNumberOfObstacles++;
        }
        currentTime -= Time.deltaTime;


    }


    private void SetUp()
    {

        playerController.DistanceOX = tiles[0, 1].transform.position - tiles[0, 0].transform.position;
        playerController.DistanceOY = tiles[0, 0].transform.position - tiles[1, 0].transform.position;
        //set positions
        playerController.PosX = startingPosY;
        playerController.PosY = startingPosX;
        //Debug.Log(playerController.DistanceOY);
       // Debug.Log(playerController.DistanceOX);
        //set limits
        playerController.MaxWidth = width;
        playerController.MaxHeight = height;

    }
}
