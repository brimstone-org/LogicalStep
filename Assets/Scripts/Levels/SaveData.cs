using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData 
{
    

    public static LevelData levelData = new LevelData();

    public delegate void SerializeAction();
    public static event SerializeAction OnLoaded;
    public static event SerializeAction OnBeforeSave;

    public static void Load(int x){

        if(x>=1 && x <= 100) {

            if(PlayerPrefs.GetInt("currentLevel") < x) 
            {
                PlayerPrefs.SetInt("currentLevel",x);
            }
        }
        else if(x >= 101 && x <= 200) {
            if(PlayerPrefs.GetInt("currentLevelPack2") < x) 
            {
                PlayerPrefs.SetInt("currentLevelPack2",x);
            }
        }
        else if (x>=201 && x<=300) 
        {
            if(PlayerPrefs.GetInt("currentLevelPackBonus") < x) 
            {
                PlayerPrefs.SetInt("currentLevelPackBonus",x);
            }
        }
    
        string path = "Levels/" + x.ToString();
        levelData = LoadLevel(path);
        LevelDataLoaded.levelData = levelData;
    }

    public static void Save(LevelData levelData,string path="x"){
        OnBeforeSave();
        SaveLevel(path, levelData);
        Debug.Log(path);
        ClearLevelsList();
    }

    private static LevelData LoadLevel(string path){

        //string json = File.ReadAllText(path);
        TextAsset txtAsset = (TextAsset)Resources.Load(path, typeof(TextAsset));
        return JsonUtility.FromJson<LevelData>(txtAsset.text);
    }


    private static void SaveLevel(string path,LevelData level){
        string json = JsonUtility.ToJson(level);
        StreamWriter sw = File.CreateText(path);
        sw.Close();
        File.WriteAllText(path, json);
    }


    public static void AddLevelData(LevelData levelData){
        
    }
    public static void ClearLevelsList(){  
    }


}
