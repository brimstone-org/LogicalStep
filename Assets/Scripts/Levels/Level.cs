using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public LevelData levelData = new LevelData();

    public void StoreData()
    {
        //levelData.levelID = levelID;
    }
    public void LoadData()
    {
       // levelID = levelData.levelID;

    }
    public void ApplyData()
    {
        SaveData.AddLevelData(levelData);
    }
    private void OnEnable()
    {
        SaveData.OnLoaded += LoadData;
        SaveData.OnBeforeSave += StoreData;
        SaveData.OnBeforeSave += ApplyData;
    }
    private void OnDisable()
    {
        SaveData.OnLoaded -= LoadData;
        SaveData.OnBeforeSave -= StoreData;
        SaveData.OnBeforeSave -= ApplyData;
    }
}

[System.Serializable]
public class LevelData
{
    
    public int levelID;
    public List<TileData> tiles;
    public List<ObstacleData> obstacles;
    public List<int> snakes;
    public List<MoveType> moves;
    bool isLocked = true;

    public LevelData(){
        tiles = new List<TileData>();
        obstacles = new List<ObstacleData>();
        snakes = new List<int>();
        moves = new List<MoveType>();
    }
}

[System.Serializable]
public class SnakeData{
    public float posX;
}

[System.Serializable]
public class ObstacleData
{
    public Vector2 startingPoint;
    public Vector2 endPoint;
    public int posY;
}

[System.Serializable]
public class TileData
{

    public int gridPositionX;
    public int gridPositionY;
    public TileType type;
    public int health=0;

}
public enum TileType{
    StartTile,EndTile,NormalTile
}
public enum MoveType{
    up,right,down,left
}