using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    #region 单例模式实现
    public static MapManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    #endregion
    public GameObject player;
    public GameObject npc;
    public Action Init;
    public Action beforeMove;
    public Action afterMove;
    private void Start()
    {
        //switch (Characters.characters[PlayerDataManager.Instance.playerData.characteristic].characterType)
        //{
        //    case E_CharacterType.初始:
        //        Init += Characters.characters[PlayerDataManager.Instance.playerData.characteristic].effect;
        //        break;
        //    case E_CharacterType.行动后:
        //        afterMove += Characters.characters[PlayerDataManager.Instance.playerData.characteristic].effect;
        //        break;
        //}
        Init+=MapGenerator.Instance.MapInit;
        _Move(player, MapGenerator.Instance.playerMapList);
        _Move(npc, MapGenerator.Instance.npcMapList);
        Init.Invoke();

    }

    private void _Move(GameObject obj, GameObject[] maplist)
    {
        int i = PlayerDataManager.Instance.playerData.stepCount;
        obj.transform.position = new Vector3(maplist[i].transform.position.x, 1, maplist[i].transform.position.z);
    }
    public void Move()
    {
        //currentBlock = playerBlocks[PlayerDataManager.Instance.playerData.stepCount];
        _Move(player, MapGenerator.Instance.playerMapList);
        BlockManager.Instance.PlayerInvokeBlock();
        _Move(npc, MapGenerator.Instance.npcMapList);
        BlockManager.Instance.NpcInvokeBlock();
        afterMove.Invoke();
        GameOverCheck.Instance.Check();
    }

}