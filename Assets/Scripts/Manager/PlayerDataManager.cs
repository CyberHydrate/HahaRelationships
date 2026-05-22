using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class PlayerData
{
    public string playerName;
    public string npcName;
    public string playerGender;
    public string npcGender;
    public string relationship;
    public int characteristic;
    public float heartWallWidth;
    public int stepCount;
    public int playerhp;
    public int npchp;
    public int relationshiphp;
    public Block[] playerBlocks;
    public Block[] npcBlocks;
    public PlayerData(string playerName, string npcName, string playerGender, string npcGender, string relationship, int characteristic, int playerhp, int npchp, float heartWallWidth, int stepCount,  int relationshiphp)
    {
        this.playerName = playerName;
        this.npcName = npcName;
        this.playerGender = playerGender;
        this.npcGender = npcGender;
        this.relationship = relationship;
        this.characteristic = characteristic;

        this.playerhp = playerhp;
        this.npchp = npchp;
        this.heartWallWidth = heartWallWidth;
        this.stepCount = stepCount;
        this.relationshiphp = relationshiphp;

        playerBlocks = new Block[101];
        npcBlocks = new Block[101];

        for(int i=0;i<101;i++)
        {
            this.playerBlocks[i] = new Block(E_BlockType.未知);
            this.npcBlocks[i] = new Block(E_BlockType.未知);
        }
    }
    public PlayerData()
    {
        playerBlocks = new Block[101];
        npcBlocks = new Block[101];
        for (int i = 0; i < 101; i++)
        {
            this.playerBlocks[i] = new Block(E_BlockType.未知);
            this.npcBlocks[i] = new Block(E_BlockType.未知);
        }
    }
}
public class PlayerDataManager
{
    #region 单例模式实现
    private static PlayerDataManager mInstance;
    public static PlayerDataManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new PlayerDataManager();
            }
            return mInstance;
        }
    }

    private PlayerDataManager() { }//构造函数私有化，确保外部无法实例化
    #endregion
    public PlayerData playerData = new PlayerData();
}
