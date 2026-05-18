using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverCheck : MonoBehaviour
{
    public static GameOverCheck Instance { get; private set; }
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

    public int maxSteps;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI overword;
    public TextMeshProUGUI data;
    public void Check()
    {
        PlayerData p = PlayerDataManager.Instance.playerData;
        if (p.npchp == 0)
        {
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("A1结局", "ta崩溃了");
        }
        else if (p.playerhp == 0)
        {
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("A2结局", "你崩溃了");
        }
        else if (p.relationshiphp == 100)
        {
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("A3结局", "珍贵的情谊");
        }
        else if (p.relationshiphp == 0)
        {
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("A4结局", "缘分已尽");
        }
        else if (p.stepCount < maxSteps)
        {
            Debug.Log("当前步数：" + PlayerDataManager.Instance.playerData.stepCount);
            return;
        }
        else if (p.playerhp >= 50 && p.relationshiphp < 50)
        {
            Debug.Log("结局B1");
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("B1结局", "渐行渐远");
        }
        else if (p.playerhp < 50 && p.relationshiphp < 50)
        {
            Debug.Log("结局B2");
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("B2结局", "冤冤相报");
        }
        else if (p.playerhp >= 50 && p.relationshiphp >= 50)
        {
            Debug.Log("结局B3");
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("B3结局", "君子之交");
        }
        else if (p.playerhp < 50 && p.relationshiphp >= 50)
        {
            Debug.Log("结局B4");
            GameManager.Instance.SwitchState(GameState.GameOver);
            SetOverPanel("B4结局", "恨海情天");
        }
    }
    private void SetOverPanel(string name, string desc)
    {
        PlayerData p = PlayerDataManager.Instance.playerData;
        title.text = name;
        description.text = desc;
        overword.text = $"{p.playerName} 和 {p.npcName}  以 {p.relationship}  关系，在经过了 {p.stepCount}  步后达成了结局 {name}";
        data.text = "当前数值：\n" + "玩家心理健康：" + p.playerhp + "\n" + "npc心理健康：" + p.npchp + "\n" + "羁绊值：" + p.relationshiphp + "\n" + "心之壁厚度：" + p.heartWallWidth;
    }
}
