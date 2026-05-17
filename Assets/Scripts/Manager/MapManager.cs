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
    private void Start()
    {
        Debug.Log(PlayerDataManager.Instance.playerData.relationship);
        MapGenerator.Instance.MapInit();
        _Move(player, MapGenerator.Instance.playerMapList);
        _Move(npc, MapGenerator.Instance.npcMapList);
    }
    #region 地图生成
    

   
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
        _Move(player, MapGenerator.Instance.playerMapList);
        PlayerInvokeBlock();
        _Move(npc, MapGenerator.Instance.npcMapList);
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
    public GameObject choiceList;
    public Button[] choices;
    public Button closeBtn;
    [Header("日程")]
    public GameObject scheduleUI;
    private void PlayerInvokeBlock()
    {
        int i = PlayerDataManager.Instance.playerData.stepCount;
        if (playerBlocks[i] != null)
        {
            if (playerBlocks[i].blockType == E_BlockType.事件)
            {
                PlayerEventInvoke();
            }
            else if (playerBlocks[i].blockType == E_BlockType.计划)
            {
                PlayerPlanInvoke();
            }
            else if (playerBlocks[i].blockType == E_BlockType.重要事件)
            {
                PlayerEventInvoke();
            }
            else if (playerBlocks[i].blockType == E_BlockType.空)
            {

            }
        }
    }
    private void NpcInvokeBlock()
    {
        int i = PlayerDataManager.Instance.playerData.stepCount; 
        if (npcBlocks[i] != null)
        {
            if (npcBlocks[i].blockType == E_BlockType.事件)
            {
                NpcEventInvoke(i);
            }
            else if (npcBlocks[i].blockType == E_BlockType.计划)
            {
                NpcPlanInvoke();
            }
            else if (npcBlocks[i].blockType == E_BlockType.重要事件)
            {
                NpcEventInvoke(i);
            }
            else if (npcBlocks[i].blockType == E_BlockType.空)
            {

            }
        }
    }
    private void PlayerEventInvoke()
    {
        int id = currentBlock.blockEvent.eventId-1;
        for (int i = 0; i < 5; i++)
        {
            if (i >= ExcelReader.eventData[id].choiceCount)
            {
                choices[i].gameObject.SetActive(false);
            }
            else
            {
                int index = i;
                choices[index].gameObject.SetActive(true);
                Debug.Log("id="+(id+1)+"第"+i+ExcelReader.eventData[id].choices[index].choiceName);
                choices[index].GetComponentInChildren<TextMeshProUGUI>().text = ExcelReader.eventData[id].choices[index].choiceName;
                choices[index].onClick.RemoveAllListeners(); // 移除之前的监听器，避免重复添加

                choices[index].onClick.AddListener(() => Events.events[id].choices[index].Invoke());
                choices[index].onClick.AddListener(() => eventDescription.text = ExcelReader.eventData[id].choices[index].choiceDesc);
                choices[index].onClick.AddListener(() => closeBtn.gameObject.SetActive(true)); // 选择后显示结束按钮
                choices[index].onClick.AddListener(() => choiceList.SetActive(false));
            }
        }
        choiceList.SetActive(true);
        closeBtn.onClick.RemoveAllListeners(); // 移除之前的监听器，避免重复添加
        closeBtn.onClick.AddListener(() => eventUI.SetActive(false)); // 选择后关闭事件UI
        closeBtn.gameObject.SetActive(false);
        eventName.text = ExcelReader.eventData[id].eventName;
        eventDescription.text = ExcelReader.eventData[id].eventDesc;
        eventUI.SetActive(true);
    }
    private void NpcEventInvoke(int steps)
    {
        //NpcController.Instance.GetChoice(npcBlocks[PlayerDataManager.Instance.playerData.stepCount].blockEvent.eventId);
    }
    private void PlayerPlanInvoke()
    {
        scheduleUI.SetActive(true);
        MapGenerator.Instance.GenerateNext7BlocksWhenOnPlan(true);
    }
    private void NpcPlanInvoke()
    {
        MapGenerator.Instance.GenerateNext7BlocksWhenOnPlan(false);
    }
    #endregion

    #region 游戏结束判定
    [Header("游戏结束")]
    public int maxSteps;
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
        else if (p.stepCount < maxSteps)
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
        overword.text = $"{p.playerName} 和 {p.npcName}  以 {p.relationship}  关系，在经过了 {p.stepCount}  步后达成了结局 {name}";
        data.text = "当前数值：\n" + "玩家心理健康：" + p.playerhp + "\n" + "npc心理健康：" + p.npchp + "\n" + "羁绊值：" + p.relationshiphp + "\n" + "心之壁厚度：" + p.heartWallWidth;
    }
    #endregion
}