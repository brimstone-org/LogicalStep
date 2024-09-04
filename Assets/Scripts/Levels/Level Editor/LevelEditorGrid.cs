using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorGrid : MonoBehaviour {

    public static LevelEditorGrid instance = null;

    [SerializeField]
    private int height = 6;

    [SerializeField]
    private int width = 3;

    [SerializeField]
    private GameObject editorEmptyTile;

    private PlayerController playerController;

    [HideInInspector]
    public GameObject[,] tiles;

    EmptyTileEditorScript emptyTileEditorScript;

    void Awake()
    {
        if (instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
        tiles = new GameObject[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tiles[i, j] = Instantiate(editorEmptyTile) as GameObject;
                emptyTileEditorScript = tiles[i, j].GetComponent<EmptyTileEditorScript>();
                emptyTileEditorScript.posX = j;
                emptyTileEditorScript.posY = i;


                tiles[i, j].transform.SetParent(transform);
                tiles[i, j].transform.position = new Vector2(transform.position.x + j * 3.0f, transform.position.y - i * 3.0f);
                tiles[i, j].transform.name = (i * width + j + 1).ToString();
            }
        }
    }
	
    private bool isSetup = false;
    private bool createdController = false;
	// Update is called once per frame
	void Update () {
        if (isSetup == false)
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
	}



    private void SetUp()
    {
        playerController.DistanceOX = tiles[0, 1].transform.position - tiles[0, 0].transform.position;
        playerController.DistanceOY = tiles[0, 0].transform.position - tiles[1, 0].transform.position;

        Debug.Log(playerController.DistanceOY);
        Debug.Log(playerController.DistanceOX);

        //set limits
        playerController.MaxWidth = width;
        playerController.MaxHeight = height;
    }
}
