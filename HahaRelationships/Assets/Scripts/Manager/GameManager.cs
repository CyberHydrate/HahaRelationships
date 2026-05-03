using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,   // 主菜单
    GameSelect,   // 游戏选择界面
    Playing,    // 游戏进行中
    Pause,      // 暂停
    GameOver    // 游戏结束
}
public class GameManager : MonoBehaviour
{
    #region 单例模式实现
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 切换场景不销毁
    }
    #endregion

    #region 游戏状态管理
    public GameState CurrentState { get; private set; }

    public delegate void StateChangeEvent(GameState newState);

    public event StateChangeEvent OnGameStateChanged;

    public void SwitchState(GameState newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                // 进入主菜单时要做的事，比如显示主菜单UI、停止游戏逻辑等
                LoadScene("MainMenu");
                break;

            case GameState.GameSelect:
                // 进入游戏选择界面时要做的事
                break;

            case GameState.Playing:
                // 正式开始游戏时要做的事，比如重置游戏数据、开始计时等
                LoadScene("GameScene");
                break;

            case GameState.Pause:
                // 暂停游戏时要做的事，比如显示暂停UI、停止游戏逻辑等
                Time.timeScale = 0;
                break;

            case GameState.GameOver:
                // 游戏结束时要做的事，比如显示结束UI、停止所有游戏逻辑等
                Time.timeScale = 0;
                break;
        }
        // 触发状态改变事件，通知其他系统
        OnGameStateChanged?.Invoke(newState);
    }

    #endregion

    #region 游戏初始化
    void Start()
    {
        GameInit();
    }
    void GameInit()
    {
        // 这里可以添加一些游戏初始化的逻辑，比如加载资源、设置初始参数等
        // 游戏开始时默认进入主菜单
        SwitchState(GameState.MainMenu);
    }
    #endregion

    #region 场景切换
    public void LoadScene(string sceneName)
    {
        // 这里可以添加一些过渡动画或者加载界面
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    #endregion

    #region 退出自动存档(目前不用)
    private void OnApplicationQuit()
    {
        //SaveManager.Instance.SaveData(PlayerDataManager.Instance.playerData);
    }
    #endregion
}
    