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
    [System.NonSerialized]
    public MapEvent[] playerMapEvents = new MapEvent[100];
    [System.NonSerialized]
    public MapEvent[] npcMapEvents = new MapEvent[100];
    private void Start()
    {
        GenerateMap();
    }
    #region 地图生成
    [System.NonSerialized]
    public GameObject[] playerMapList = new GameObject[100];
    [System.NonSerialized]
    public GameObject[] npcMapList = new GameObject[100];
    public Transform playerMap;
    public Transform npcMap;
    public GameObject planePrefab;
    public void GenerateMap()
    {
        Debug.Log("Generating map...");
        for (int i = 0; i < 100; i++)
        {
            Vector3 pos = new Vector3(10, 0, i * 10);
            GameObject plane = Instantiate(planePrefab, pos, Quaternion.identity);
            plane.transform.parent = playerMap;
            plane.name = "Plane_" + i;
            playerMapList[i] = plane;
        }
        for (int i = 0; i < 100; i++)
        {
            Vector3 pos = new Vector3(-10, 0, i * 10);
            GameObject plane = Instantiate(planePrefab, pos, Quaternion.identity);
            plane.transform.parent = npcMap;
            plane.name = "Plane_" + i;
            npcMapList[i] = plane;
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
        PlayerEventInvoke(PlayerDataManager.Instance.playerData.stepCount);
        _Move(npc, npcMapList);
        NpcEventInvoke(PlayerDataManager.Instance.playerData.stepCount);
    }
    #endregion

    #region 事件系统
    [System.NonSerialized]
    public MapEvent currentEvent;
    public GameObject eventUI;
    public TextMeshProUGUI eventName;
    public TextMeshProUGUI eventDescription;
    public Button[] choices;
    private void PlayerEventInvoke(int steps)
    {
        //currentEvent = playerMapEvents[steps];
        currentEvent = new WorkEvent(); // 测试用，实际应根据steps获取对应事件
        for (int i = 0; i < currentEvent.Choices.Count; i++)
        {
            if (currentEvent.Choices[i]==null)
            {
                choices[i].gameObject.SetActive(false);
            }
            else
            {
                choices[i].gameObject.SetActive(true);
                choices[i].GetComponentInChildren<TextMeshProUGUI>().text = currentEvent.Choices[i].ChoiceDescription;
                int index = i; // 捕获当前的i值
                choices[i].onClick.RemoveAllListeners(); // 移除之前的监听器，避免重复添加
                choices[i].onClick.AddListener(() => currentEvent.Choices[index].ExecuteChoice()); // 添加新的监听器
                choices[i].onClick.AddListener(() => eventUI.SetActive(false)); // 选择后关闭事件UI
            }
        }
        eventName.text = currentEvent.EventName;
        eventDescription.text = currentEvent.EventDescription;
        eventUI.SetActive(true);
    }
    private void NpcEventInvoke(int steps)
    {
        // NPC事件触发逻辑
    }
    #endregion
}
//测试用
//测试用
//测试用
public class WorkEvent : MapEvent
{
    public override int EventID => 0;
    public override string EventName => "工作";
    public override string EventDescription => "上班时间到了";
    public override EventType Type => EventType.Work;
    public override int EventWeight => 1;
    public override int SpawnLimit => 1;
    public override EventProperty Property => EventProperty.Neutral;
    public override List<EventChoice> Choices => new List<EventChoice>
    {
        new WorkChoice(),
        null,
        null,
        null,
        null
    };
    public override bool SpawnCondition()
    {
        return true; // 可以根据需要添加生成条件
    }
}
public class WorkChoice : EventChoice
{
    public override ChoiceType Type => ChoiceType.Positive;
    public override string ChoiceDescription => "努力挣钱";
    public override void ExecuteChoice()
    {
        // 执行选择的逻辑，例如增加金钱、减少精力等
        Debug.Log("你得到了钱");

    }
}
