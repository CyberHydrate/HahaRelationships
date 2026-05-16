using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectPanel : MonoBehaviour
{
    //这个脚本负责在游戏状态切换时显示或隐藏初始界面
    [Header("选择界面")]
    public GameObject content;
    
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        //取消订阅，避免内存泄漏
        GameManager.Instance.OnGameStateChanged -= OnStateChanged;
    }
    void Start()
    {
        //初始订阅游戏状态改变事件
        GameManager.Instance.OnGameStateChanged += OnStateChanged;
        //根据当前状态设置初始界面显示
        OnStateChanged(GameManager.Instance.CurrentState);
        
    }
    void OnStateChanged(GameState state)
    {
        content.SetActive(state == GameState.GameSelect);
    }
    
}
