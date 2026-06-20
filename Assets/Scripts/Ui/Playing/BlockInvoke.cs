using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockInvoke : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public DragPlan dragPlan1;
    public DragPlan dragPlan2;
    public DragPlan dragPlan3;
    public DragPlan dragPlan4;
    public DragPlan dragPlan5;
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
        if (PlayerDataManager.Instance.playerData.playerBlocks[i] != null)
        {
            if (PlayerDataManager.Instance.playerData.playerBlocks[i].blockType == E_BlockType.工作)
            {
                PlayerEventInvoke();
            }
            else if (PlayerDataManager.Instance.playerData.playerBlocks[i].blockType == E_BlockType.娱乐)
            {
                PlayerEventInvoke();
            }
            else if (PlayerDataManager.Instance.playerData.playerBlocks[i].blockType == E_BlockType.和ta互动)
            {
                PlayerEventInvoke();
            }
            else if (PlayerDataManager.Instance.playerData.playerBlocks[i].blockType == E_BlockType.休息)
            {
                PlayerEventInvoke();
            }
            else if (PlayerDataManager.Instance.playerData.playerBlocks[i].blockType == E_BlockType.自我提升)
            {
                PlayerEventInvoke();
            }
            else if (PlayerDataManager.Instance.playerData.playerBlocks[i].blockType == E_BlockType.计划)
            {
                PlayerPlanInvoke();
            }
            else if (PlayerDataManager.Instance.playerData.playerBlocks[i].blockType == E_BlockType.重要工作)
            {
                PlayerEventInvoke();
            }
            else if (PlayerDataManager.Instance.playerData.playerBlocks[i].blockType == E_BlockType.重要娱乐)
            {
                PlayerEventInvoke();
            }
            else if (PlayerDataManager.Instance.playerData.playerBlocks[i].blockType == E_BlockType.重要和ta互动)
            {
                PlayerEventInvoke();
            }
            else if (PlayerDataManager.Instance.playerData.playerBlocks[i].blockType == E_BlockType.空)
            {

            }
        }
    }
    public void NpcInvokeBlock()
    {
        int i = PlayerDataManager.Instance.playerData.stepCount;
        if (PlayerDataManager.Instance.playerData.npcBlocks[i] != null)
        {
            if (PlayerDataManager.Instance.playerData.npcBlocks[i].blockType == E_BlockType.和ta互动)
            {
                NpcEventInvoke(i);
            }
            else if (PlayerDataManager.Instance.playerData.npcBlocks[i].blockType == E_BlockType.计划)
            {
                NpcPlanInvoke();
            }
            else if (PlayerDataManager.Instance.playerData.npcBlocks[i].blockType == E_BlockType.重要和ta互动)
            {
                NpcEventInvoke(i);
            }
            else if (PlayerDataManager.Instance.playerData.npcBlocks[i].blockType == E_BlockType.空)
            {

            }
        }
    }
    private void PlayerEventInvoke()
    {
        int id = PlayerDataManager.Instance.playerData.playerBlocks[PlayerDataManager.Instance.playerData.stepCount].blockEvent.eventId - 1;
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
        closeBtn.gameObject.SetActive(false);
        if (ExcelReader.eventData[id].choiceCount == 0)
        {
            choiceList.SetActive(false);
            closeBtn.gameObject.SetActive(true);
        }
        closeBtn.onClick.RemoveAllListeners(); // 移除之前的监听器，避免重复添加
        closeBtn.onClick.AddListener(() => eventUI.SetActive(false)); // 选择后关闭事件UI
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
        PlayerDataManager.Instance.playerData.workcount = 5;
        PlayerDataManager.Instance.playerData.entertainmentcount = 2;
        PlayerDataManager.Instance.playerData.restcount = 2;
        PlayerDataManager.Instance.playerData.interactcount = 1;
        dragPlan1.UpdateCount();
        dragPlan2.UpdateCount();
        dragPlan3.UpdateCount();
        dragPlan4.UpdateCount();
        dragPlan5.UpdateCount();
        scheduleUI.SetActive(true);
        mapGenerator.GenerateNext7BlocksWhenOnPlan(true);
    }
    private void NpcPlanInvoke()
    {
        mapGenerator.GenerateNext7BlocksWhenOnPlan(false);
    }

}
