using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingPanel : MonoBehaviour
{
    public GameObject eventContent;
    public GameObject planContent;

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= OnStateChanged;
    }
    void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnStateChanged;

        OnStateChanged(GameManager.Instance.CurrentState);

    }
    void OnStateChanged(GameState state)
    {
        if (state == GameState.GameOver)
        {
            eventContent.SetActive(false);
            planContent.SetActive(false);
        }
        if (state == GameState.Playing)
        {
            eventContent.SetActive(false);
            if (PlayerDataManager.Instance.playerData.stepCount % 7 == 0)
                planContent.SetActive(true);
        }
    }

}
