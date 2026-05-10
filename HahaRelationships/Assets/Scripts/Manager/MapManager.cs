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
        playerBlocks[0] = new Block(E_BlockType.Plan);
        SetMap();
        currentBlock = playerBlocks[0];
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
    public GameObject[] playerMapList = new GameObject[100];
    [System.NonSerialized]
    public GameObject[] npcMapList = new GameObject[100];
    public void GenerateMap()
    {
        Debug.Log("Generating map...");
        for (int i = 0; i < 100; i++)
        {
            Vector3 pos = new Vector3(10, 0, i * 10);
            GameObject plane = Instantiate(planePrefab, pos, Quaternion.identity);
            plane.tag = "Player";
            plane.transform.parent = playerMap;
            plane.name = "Plane_" + i;
            playerMapList[i] = plane;
        }
        for (int i = 0; i < 100; i++)
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
        _Move(player, playerMapList);
        PlayerInvokeBlock();
        _Move(npc, npcMapList);
        NpcInvokeBlock();
    }
    #endregion

    #region 事件与日程
    [System.NonSerialized]
    public Block[] playerBlocks = new Block[100];
    [System.NonSerialized]
    public Block[] npcBlocks = new Block[100];
    public Block currentBlock;
    [Header("事件")]
    public GameObject eventUI;
    public TextMeshProUGUI eventName;
    public TextMeshProUGUI eventDescription;
    public Button[] choices;
    public Button end;
    [System.NonSerialized]
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

    }
    private void PlayerEventInvoke()
    {
        currentBlock = playerBlocks[PlayerDataManager.Instance.playerData.stepCount];
        for (int i = 0; i < currentBlock.blockEvent.choiceCount; i++)
        {
            if (currentBlock.blockEvent.choices[i] == null)
            {
                choices[i].gameObject.SetActive(false);
            }
            else
            {
                choices[i].gameObject.SetActive(true);
                choices[i].GetComponentInChildren<TextMeshProUGUI>().text = currentBlock.blockEvent.choices[i].choiceName;
                int index = i; // 捕获当前的i值
                choices[i].onClick.RemoveAllListeners(); // 移除之前的监听器，避免重复添加
                choices[i].onClick.AddListener(() => currentBlock.blockEvent.choices[index].choiceFunc.Invoke());
                choices[i].onClick.AddListener(() => eventDescription.text = currentBlock.blockEvent.choices[index].choiceDesc); // 更新事件描述
                choices[i].onClick.AddListener(() => end.gameObject.SetActive(true)); // 选择后显示结束按钮
                choices[i].onClick.AddListener(() => choices[index].gameObject.SetActive(false));
            }
        }
        end.onClick.RemoveAllListeners(); // 移除之前的监听器，避免重复添加
        end.onClick.AddListener(() => eventUI.SetActive(false)); // 选择后关闭事件UI
        end.gameObject.SetActive(false);
        eventName.text = currentBlock.blockEvent.eventName;
        eventDescription.text = currentBlock.blockEvent.eventDesc;
        eventUI.SetActive(true);
    }
    private void NpcEventInvoke(int steps)
    {
        // NPC事件触发逻辑
    }
    private void PlayerPlanInvoke()
    {
        scheduleUI.SetActive(true);
    }
    private void NpcScheduleInvoke()
    {
        // NPC日程事件触发逻辑
    }
    #endregion
}