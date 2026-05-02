using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
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
    public int stepCount;
    public int playerhp;
    public int npchp;
    public PlayerData(string playerName, string npcName, string playerGender, string npcGender, string relationship, string characteristic1, string characteristic2, string characteristic3, float heartWallWidth, int stepCount, int playerhp, int npchp)
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
        this.stepCount = stepCount;
        this.playerhp = playerhp;
        this.npchp = npchp;
    }
}
public class PlayerDataManager
{
    #region 单例模式实现
    private static readonly PlayerDataManager mInstance = new PlayerDataManager();

    public static PlayerDataManager Instance => mInstance;//单例模式，确保全局只有一个PlayerDataManager实例

    private PlayerDataManager() { }//构造函数私有化，确保外部无法实例化
    #endregion
    public PlayerData playerData = new PlayerData("", "", "", "", "", "", "", "", 0f, 0, 0, 0);
}
