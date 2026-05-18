using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    #region 单例模式实现
    public static BlockManager Instance { get; private set; }
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
    [System.NonSerialized]
    public Block[] playerBlocks = new Block[101];
    [System.NonSerialized]
    public Block[] npcBlocks = new Block[101];
    [Header("事件")]
    public GameObject eventUI;
    public TextMeshProUGUI eventName;
    public TextMeshProUGUI eventDescription;
    public GameObject choiceList;
    public Button[] choices;
    public Button closeBtn;
    [Header("日程")]
    public GameObject scheduleUI;
    public void PlayerInvokeBlock()
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
    public void NpcInvokeBlock()
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
        int id = playerBlocks[PlayerDataManager.Instance.playerData.stepCount].blockEvent.eventId - 1;
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
                Debug.Log("id=" + (id + 1) + "第" + i + ExcelReader.eventData[id].choices[index].choiceName);
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

}
