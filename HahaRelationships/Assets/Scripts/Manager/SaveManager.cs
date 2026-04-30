using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[Serializable]
public class SaveData
{
    public string playerName;
    public string npcName;
    public string playerGender;
    public string npcGender;
    public string relationship;
    public string characteristic1;
    public string characteristic2;
    public string characteristic3;
    public float heartWallWidth;
    public SaveData(string playerName, string npcName, string playerGender, string npcGender, string relationship, string characteristic1, string characteristic2, string characteristic3, float heartWallWidth)
    {
        this.playerName = playerName;
        this.npcName = npcName;
        this.playerGender = playerGender;
        this.npcGender = npcGender;
        this.relationship = relationship;
        this.characteristic1 = characteristic1;
        this.characteristic2 = characteristic2;
        this.characteristic3 = characteristic3;
        this.heartWallWidth = heartWallWidth;
    }
}

public class SaveManager
{
    #region 单例模式实现

    private static readonly SaveManager mInstance = new SaveManager();

    public static SaveManager Instance => mInstance;//单例模式，确保全局只有一个SaveManager实例

    private SaveManager() { }//构造函数私有化，确保外部无法实例化

    #endregion
    string GetSavePath()
    {
        return Application.persistentDataPath + "/save.json";
    }
    //保存数据到文件
    public void SaveData(SaveData data)
    {
        string path = GetSavePath();
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    //检测存档是否存在
    public bool CheckSaveFile()
    {
        string path = GetSavePath();
        return File.Exists(path);
    }
    
    //加载数据
    public SaveData LoadData()
    {
        string path = GetSavePath();
        SaveData data= JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
        Debug.Log(data.playerName);
        return data;
    }
}
