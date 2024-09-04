
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelEditor : MonoBehaviour {

    public static LevelEditor instance = null;

    public InputField levelIndex;
    public GameObject snake;
    public Toggle snakePos0;
    public Toggle snakePos1;
    public Toggle snakePos2;
    public GameObject obstacle;
    public List<InputField> inputObstacles;


    [HideInInspector]
    public List<TileData> tileDatas;
    [HideInInspector]
    public bool editingDone=false;

    GameObject snakeInstantiated0;
    GameObject snakeInstantiated1;
    GameObject snakeInstantiated2;

    GameObject[] obstacles;

    float timeCounter = 0;
	private void Awake()
	{
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        tileDatas = new List<TileData>();
        obstacles = new GameObject[inputObstacles.Capacity];
	}

    public void AddTileData(int posX,int posY,int health,TileType tileType){
        TileData tileData = new TileData();
        tileData.gridPositionX = posX;
        tileData.gridPositionY = posY;
        tileData.health = health;
        tileData.type = tileType;
        tileDatas.Add(tileData);
    }
    public void EditTileDataHP(int posX,int posY,int health){
        
        foreach(TileData tile in tileDatas){
            if(posX == tile.gridPositionX && posY == tile.gridPositionY){
                tile.health = health;
            }
        }

    }
    public void EditLevelIndex() {
        
        if (levelIndex.text!= "")
        {
            SaveData.levelData.levelID = int.Parse(levelIndex.text);
        }else{
            SaveData.levelData.levelID = -1;
        }
    }
	private void Update()
	{
        //spawn snakes in editor
        timeCounter += Time.deltaTime;
        if(snakePos0.isOn && snakeInstantiated0== null){
            snakeInstantiated0 = Instantiate(snake) as GameObject;
            snakeInstantiated0.transform.position = new Vector2(LevelEditorGrid.instance.tiles[0, 0].transform.position.x,snakeInstantiated0.transform.position.y);
                                  
        }else if(!snakePos0.isOn && snakeInstantiated0!=null)
        {
            Destroy(snakeInstantiated0);
        }
        if (snakePos1.isOn && snakeInstantiated1 == null)
        {
            snakeInstantiated1 = Instantiate(snake) as GameObject;
            snakeInstantiated1.transform.position = new Vector2(LevelEditorGrid.instance.tiles[0, 1].transform.position.x, snakeInstantiated1.transform.position.y);

        }
        else if (!snakePos1.isOn && snakeInstantiated1 != null)
        {
            Destroy(snakeInstantiated1);
        }
        if (snakePos2.isOn && snakeInstantiated2 == null)
        {
            snakeInstantiated2 = Instantiate(snake) as GameObject;
            snakeInstantiated2.transform.position = new Vector2(LevelEditorGrid.instance.tiles[0, 2].transform.position.x, snakeInstantiated2.transform.position.y);

        }
        else if (!snakePos2.isOn && snakeInstantiated2 != null)
        {
            Destroy(snakeInstantiated2);
        }

        //spawn obstacles in editor
        for (int i = 0; i < inputObstacles.Capacity;i++){
            if(inputObstacles[i].text != "0" && obstacles[i]== null){
                int result;
                if(int.TryParse(inputObstacles[i].text,out result)){
                    if(Mathf.FloorToInt(timeCounter)%result==0){
                        obstacles[i] = Instantiate(obstacle);
                        obstacles[i].transform.position = new Vector2(obstacles[i].transform.position.x, LevelEditorGrid.instance.tiles[i, 0].transform.position.y); 
                    }
                }
            }//else if()
        }

	}
}
