using System.Collections.Generic;
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
        playerMapThings[0] = new Plan();
        SetMap();
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
            if(playerMapThings[i] == null)
            {
                playerMapThings[i] = new Empty();
            }
            switch (playerMapThings[i].Type)
            {
                case E_MapThingType.Empty:
                    playerMapList[i].GetComponent<MeshRenderer>().material = emptyMaterial;
                    break;
                case E_MapThingType.Event:
                    playerMapList[i].GetComponent<MeshRenderer>().material = eventMaterial;
                    break;
                case E_MapThingType.ImportantEvent:
                    playerMapList[i].GetComponent<MeshRenderer>().material = importantEventMaterial;
                    break;
                case E_MapThingType.Plan:
                    playerMapList[i].GetComponent<MeshRenderer>().material = scheduleMaterial;
                    break;
                case E_MapThingType.Unknown:
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
        PlayerTakeMapThing();
        _Move(npc, npcMapList);
        NpcTakeMapThing();
    }
    #endregion

    #region 事件与日程
    [System.NonSerialized]
    public MapThing[] playerMapThings = new MapThing[100];
    [System.NonSerialized]
    public MapThing[] npcMapThings = new MapThing[100];
    public MapThing currentThing;
    [Header("事件")]
    public GameObject eventUI;
    public TextMeshProUGUI eventName;
    public TextMeshProUGUI eventDescription;
    public Button[] choices;
    public Button end;
    [System.NonSerialized]
    [Header("日程")]
    public GameObject scheduleUI;
    private void PlayerTakeMapThing()
    {
        int i = PlayerDataManager.Instance.playerData.stepCount;
        if (playerMapThings[i] != null)
        {
            if (playerMapThings[i].Type == E_MapThingType.Event)
            {
                (playerMapThings[i] as MapEvent).offset = 1;
                PlayerEventInvoke();
            }
            else if (playerMapThings[i].Type == E_MapThingType.Plan)
            {
                PlayerScheduleInvoke();
            }
            else if (playerMapThings[i].Type == E_MapThingType.ImportantEvent)
            {
                (playerMapThings[i] as MapEvent).offset = 2;
                PlayerEventInvoke();
            }
            else if (playerMapThings[i].Type == E_MapThingType.Empty)
            {

            }
        }
    }
    private void NpcTakeMapThing()
    {

    }
    private void PlayerEventInvoke()
    {
        MapEvent currentEvent = playerMapThings[PlayerDataManager.Instance.playerData.stepCount] as MapEvent;
        for (int i = 0; i < currentEvent.Choices.Count; i++)
        {
            if (currentEvent.Choices[i] == null)
            {
                choices[i].gameObject.SetActive(false);
            }
            else
            {
                choices[i].gameObject.SetActive(true);
                choices[i].GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.Choices[i].ChoiceName;
                int index = i; // 捕获当前的i值
                choices[i].onClick.RemoveAllListeners(); // 移除之前的监听器，避免重复添加
                choices[i].onClick.AddListener(() => currentEvent.Choices[index].ExecuteChoice());
                choices[i].onClick.AddListener(() => eventDescription.text = currentEvent.Choices[index].ChoiceDescription); // 更新事件描述
                choices[i].onClick.AddListener(() => end.gameObject.SetActive(true)); // 选择后显示结束按钮
                choices[i].onClick.AddListener(() => choices[index].gameObject.SetActive(false));
            }
        }
        end.onClick.RemoveAllListeners(); // 移除之前的监听器，避免重复添加
        end.onClick.AddListener(() => eventUI.SetActive(false)); // 选择后关闭事件UI
        end.gameObject.SetActive(false);
        eventName.text = currentEvent.EventName;
        eventDescription.text = currentEvent.EventDescription;
        eventUI.SetActive(true);
    }
    private void NpcEventInvoke(int steps)
    {
        // NPC事件触发逻辑
    }
    private void PlayerScheduleInvoke()
    {
        scheduleUI.SetActive(true);
    }
    private void NpcScheduleInvoke()
    {
        // NPC日程事件触发逻辑
    }
    #endregion
}