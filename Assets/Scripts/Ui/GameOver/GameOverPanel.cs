using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public GameObject content;
    public Button closeBtn;
    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= OnStateChanged;
    }
    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnStateChanged;
        OnStateChanged(GameManager.Instance.CurrentState);
        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(CloseWindow);
    }
    void OnStateChanged(GameState state)
    {
        content.SetActive(state==GameState.GameOver);
    }
    public void CloseWindow()
    {
        content.SetActive(false);
        GameManager.Instance.SwitchState(GameState.MainMenu);
    }
}
