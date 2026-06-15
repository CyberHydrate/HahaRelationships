using System;
using UnityEngine;
using DG.Tweening;


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
    public BlockInvoke blockManager;
    public MapGenerator mapGenerator;
    private void Start()
    {
        DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
        //switch (Characters.characters[PlayerDataManager.Instance.playerData.characteristic].characterType)
        //{
        //    case E_CharacterType.初始:
        //        Init += Characters.characters[PlayerDataManager.Instance.playerData.characteristic].effect;
        //        break;
        //    case E_CharacterType.行动后:
        //        afterMove += Characters.characters[PlayerDataManager.Instance.playerData.characteristic].effect;
        //        break;
        //}
        Init +=mapGenerator.MapInit;
        Init.Invoke();
        _Move(player, mapGenerator.playerMapList);
        _Move(npc, mapGenerator.npcMapList);
        Camera.main.GetComponent<CameraFollow>().Init();

        if (!SaveManager.Instance.CheckSaveFile())
        {
            mapGenerator.GenerateNext7BlocksWhenOnPlan(true);
            mapGenerator.GenerateNext7BlocksWhenOnPlan(false);
        }
    }
    private int GetSteps()
    {
        return PlayerDataManager.Instance.playerData.stepCount;
    }
    private Block[] GetPlayerBlocks()
    {
        return PlayerDataManager.Instance.playerData.playerBlocks;
    }
    private Block[] GetNpcBlocks()
    {
        return PlayerDataManager.Instance.playerData.npcBlocks;
    }

    private void _Move(GameObject obj, GameObject[] maplist)
    {
        int i = PlayerDataManager.Instance.playerData.stepCount;
        obj.transform.DOMove(new Vector3(maplist[i].transform.position.x, 1, maplist[i].transform.position.z), 0.5f);
        //obj.transform.position = new Vector3(maplist[i].transform.position.x, 1, maplist[i].transform.position.z);
    }
    public void Move()
    {
        //currentBlock = playerBlocks[PlayerDataManager.Instance.playerData.stepCount];
        _Move(player, mapGenerator.playerMapList);
        blockManager.PlayerInvokeBlock();
        _Move(npc, mapGenerator.npcMapList);
        blockManager.NpcInvokeBlock();
        //afterMove.Invoke();
        GameOverCheck.Instance.Check();
    }

}