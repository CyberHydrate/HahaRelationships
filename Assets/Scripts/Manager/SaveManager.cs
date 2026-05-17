using System;
using System.IO;
using UnityEngine;
[Serializable]


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
    public void SaveData(PlayerData data)
    {
        string path = GetSavePath();
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    //检测存档是否存在
    public bool CheckSaveFile()
    {
        Debug.Log("Checking save file at: " + GetSavePath());
        string path = GetSavePath();
        return File.Exists(path);
    }
    

    public void LoadData()
    {
        string path = GetSavePath();
        PlayerData data= JsonUtility.FromJson<PlayerData>(File.ReadAllText(path));
        PlayerDataManager.Instance.playerData = data;
        Debug.Log(data.playerName+data.npcName+data.playerGender+data.npcGender+data.relationship+data.characteristic);
    }
}
