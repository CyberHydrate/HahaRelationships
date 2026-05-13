using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
    private void Start()
    {
        GenerateMap();
        InitFixedPlanBlocks(playerBlocks);
        InitFixedPlanBlocks(npcBlocks);
        playerBlocks[0] = new Block(E_BlockType.Plan);
        SetMap();
        currentBlock = playerBlocks[0];
        PlayerPlanInvoke();

    }
    #region 地图生成
    public Transform playerMap;
    public Transform npcMap;
    public GameObject planePrefab;
    public Material emptyMaterial;
    public Material eventMaterial;
    public Material importantEventMaterial;
    public Material scheduleMaterial;
    public Material unknownMaterial;
    [System.NonSerialized]
    public GameObject[] playerMapList = new GameObject[101];
    [System.NonSerialized]
    public GameObject[] npcMapList = new GameObject[101];
    public void GenerateMap()
    {
        Debug.Log("Generating map...");
        for (int i = 0; i < 101; i++)
        {
            Vector3 pos = new Vector3(10, 0, i * 10);
            GameObject plane = Instantiate(planePrefab, pos, Quaternion.identity);
            plane.tag = "Player";
            plane.transform.parent = playerMap;
            plane.name = "Plane_" + i;
            playerMapList[i] = plane;
        }
        for (int i = 0; i < 101; i++)
        {
            Vector3 pos = new Vector3(-10, 0, i * 10);
            GameObject plane = Instantiate(planePrefab, pos, Quaternion.identity);
            plane.tag = "Npc";
            plane.transform.parent = npcMap;
            plane.name = "Plane_" + i;
            npcMapList[i] = plane;
        }
    }
    public void SetMap()
    {
        for (int i = 0; i < 100; i++)
        {
            if (playerBlocks[i]==null)
            {
                playerBlocks[i] = new Block(E_BlockType.Empty);
            }
            switch (playerBlocks[i].blockType)
            {
                case E_BlockType.Empty:
                    playerMapList[i].GetComponent<MeshRenderer>().material = emptyMaterial;
                    break;
                case E_BlockType.Event:
                    playerMapList[i].GetComponent<MeshRenderer>().material = eventMaterial;
                    break;
                case E_BlockType.Important:
                    playerMapList[i].GetComponent<MeshRenderer>().material = importantEventMaterial;
                    break;
                case E_BlockType.Plan:
                    playerMapList[i].GetComponent<MeshRenderer>().material = scheduleMaterial;
                    break;
                case E_BlockType.Unknown:
                    playerMapList[i].GetComponent<MeshRenderer>().material = unknownMaterial;
                    break;
                default:
                    playerMapList[i].GetComponent<MeshRenderer>().material = unknownMaterial;
                    break;
            }
        }
        //for (int i = 0; i < 100; i++)
        //{
        //    if (npcBlocks[i] == null)
        //    {
        //        npcBlocks[i] = new Block(E_BlockType.Empty);
        //    }
        //    switch (npcBlocks[i].blockType)
        //    {
        //        case E_BlockType.Empty:
        //            npcMapList[i].GetComponent<MeshRenderer>().material = emptyMaterial;
        //            break;
        //        case E_BlockType.Event:
        //            npcMapList[i].GetComponent<MeshRenderer>().material = eventMaterial;
        //            break;
        //        case E_BlockType.Important:
        //            npcMapList[i].GetComponent<MeshRenderer>().material = importantEventMaterial;
        //            break;
        //        case E_BlockType.Plan:
        //            npcMapList[i].GetComponent<MeshRenderer>().material = scheduleMaterial;
        //            break;
        //        case E_BlockType.Unknown:
        //            npcMapList[i].GetComponent<MeshRenderer>().material = unknownMaterial;
        //            break;
        //        default:
        //            npcMapList[i].GetComponent<MeshRenderer>().material = unknownMaterial;
        //            break;
        //    }
        //}
    }
    #endregion

    #region 玩家和NPC移动
    private void _Move(GameObject obj, GameObject[] maplist)
    {
        int i = PlayerDataManager.Instance.playerData.stepCount;
        obj.transform.position = new Vector3(maplist[i].transform.position.x, 1, maplist[i].transform.position.z);
    }
    public void Move()
    {
        currentBlock = playerBlocks[PlayerDataManager.Instance.playerData.stepCount];
        _Move(player, playerMapList);
        PlayerInvokeBlock();
        _Move(npc, npcMapList);
        NpcInvokeBlock();
        GameOverCheck();
    }
    #endregion

    #region 事件与日程
    [System.NonSerialized]
    public Block[] playerBlocks = new Block[101];
    [System.NonSerialized]
    public Block[] npcBlocks = new Block[101];
    public Block currentBlock;
    [Header("事件")]
    public GameObject eventUI;
    public TextMeshProUGUI eventName;
    public TextMeshProUGUI eventDescription;
    public Button[] choices;
    public Button close;
    [Header("日程")]
    public GameObject scheduleUI;
    private void PlayerInvokeBlock()
    {
        int i = PlayerDataManager.Instance.playerData.stepCount;
        if (playerBlocks[i] != null)
        {
            if (playerBlocks[i].blockType == E_BlockType.Event)
            {
                PlayerEventInvoke();
            }
            else if (playerBlocks[i].blockType == E_BlockType.Plan)
            {
                PlayerPlanInvoke();
            }
            else if (playerBlocks[i].blockType == E_BlockType.Important)
            {
                PlayerEventInvoke();
            }
            else if (playerBlocks[i].blockType == E_BlockType.Empty)
            {

            }
        }
    }
    private void NpcInvokeBlock()
    {
        int i = PlayerDataManager.Instance.playerData.stepCount; 
        if (npcBlocks[i] != null)
        {
            // 补充NPC的区块触发逻辑
        }
    }
    private void PlayerEventInvoke()
    {
        //for (int i = 0; i < currentBlock.blockEvent.choiceCount; i++)
        //{
        //    if (currentBlock.blockEvent.choices[i] == null)
        //    {
        //        choices[i].gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        choices[i].gameObject.SetActive(true);
        //        choices[i].GetComponentInChildren<TextMeshProUGUI>().text = currentBlock.blockEvent.choices[i].choiceName;
        //        int index = i; // 捕获当前的i值
        //        choices[i].onClick.RemoveAllListeners(); // 移除之前的监听器，避免重复添加
        //        choices[i].onClick.AddListener(() => currentBlock.blockEvent.choices[index].choiceFunc.Invoke());
        //        choices[i].onClick.AddListener(() => eventDescription.text = currentBlock.blockEvent.choices[index].choiceDesc); // 更新事件描述
        //        choices[i].onClick.AddListener(() => end.gameObject.SetActive(true)); // 选择后显示结束按钮
        //        choices[i].onClick.AddListener(() => choices[index].gameObject.SetActive(false));
        //    }
        //}
        //end.onClick.RemoveAllListeners(); // 移除之前的监听器，避免重复添加
        //end.onClick.AddListener(() => eventUI.SetActive(false)); // 选择后关闭事件UI
        //end.gameObject.SetActive(false);
        //eventName.text = currentBlock.blockEvent.eventName;
        //eventDescription.text = currentBlock.blockEvent.eventDesc;
        //eventUI.SetActive(true);
    }
    private void NpcEventInvoke(int steps)
    {
        // NPC事件触发逻辑
    }
    private void PlayerPlanInvoke()
    {
        scheduleUI.SetActive(true);
        GenerateNext7BlocksWhenOnPlan(true);
    }
    private void NpcScheduleInvoke()
    {
        // NPC日程事件触发逻辑
    }
    #endregion

    #region 地图区块分配

    public void GenerateNext7BlocksWhenOnPlan(bool isPlayer)
    {
        int currentIndex = PlayerDataManager.Instance.playerData.stepCount;
        Block[] targetBlocks = isPlayer ? playerBlocks : npcBlocks;

        for (int i = 1; i <= 7; i++)
        {
            int targetIndex = currentIndex + i;

            if (targetIndex >= 100)
                break;

            if (targetIndex % 7 == 0)
                continue;

            if (targetBlocks[targetIndex] == null || targetBlocks[targetIndex].blockType == E_BlockType.Unknown)
            {
                targetBlocks[targetIndex] = GetRandomBlockByWeight();
            }
        }

        SetMap();
    }


    private void InitFixedPlanBlocks(Block[] blocks)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (i % 7 == 0)
            {
                blocks[i] = new Block(E_BlockType.Plan);
            }
            else
            {
                blocks[i] = new Block(E_BlockType.Unknown);
            }
        }
    }

    private Block GetRandomBlockByWeight()
    {
        int random = Random.Range(0, 10);

        if (random < 7)
        {
            Debug.Log("Empty");
            return new Block(E_BlockType.Empty);
        }
        else if (random < 9)
        {
            Debug.Log("Event");
            return new Block(E_BlockType.Event);
        }
        else
        {
            Debug.Log("Important");
            return new Block(E_BlockType.Important);
        }
    }


    #endregion

    #region 游戏结束判定
    [Header("游戏结束")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI overword;
    public TextMeshProUGUI data;
    private void GameOverCheck()
    {
        PlayerData p = PlayerDataManager.Instance.playerData;
        if (p.npchp == 0)
        {
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("A1结局","ta崩溃了");
        }
        else if (p.playerhp == 0)
        {
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("A2结局","你崩溃了");
        }
        else if (p.relationshiphp == 100)
        {
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("A3结局","珍贵的情谊");
        }
        else if (p.relationshiphp == 0)
        {
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("A4结局","缘分已尽");
        }
        else if (p.stepCount < 10)
        {
            Debug.Log("当前步数："+PlayerDataManager.Instance.playerData.stepCount);
            return;
        }
        else if (p.playerhp >= 50 && p.relationshiphp < 50)
        {
            Debug.Log("结局B1");
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("B1结局","渐行渐远");
        }
        else if (p.playerhp < 50 && p.relationshiphp < 50)
        {
            Debug.Log("结局B2");
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("B2结局","冤冤相报");
        }
        else if (p.playerhp >= 50 && p.relationshiphp >= 50)
        {
            Debug.Log("结局B3");
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("B3结局","君子之交");
        }
        else if (p.playerhp < 50 && p.relationshiphp >= 50)
        {
            Debug.Log("结局B4");
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("B4结局", "恨海情天");
        }
    }
    private void SetOverPanel(string name,string desc)
    {
        PlayerData p = PlayerDataManager.Instance.playerData;
        title.text = name;
        description.text = desc;
        overword.text = p.playerName + " 和 " + p.npcName + " 以 " + p.relationship + " 关系，在经过了 " + p.stepCount + " 步后达成了结局 " + name;
        data.text = "当前数值：\n" + "玩家心理健康：" + p.playerhp + "\n" + "npc心理健康：" + p.npchp + "\n" + "羁绊值：" + p.relationshiphp + "\n" + "心之壁厚度：" + p.heartWallWidth;
    }
    #endregion
}