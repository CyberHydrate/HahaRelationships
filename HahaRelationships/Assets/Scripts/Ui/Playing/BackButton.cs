using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    Button backBtn;
    private void Awake()
    {
        backBtn = GetComponent<Button>();
    }
    private void Start()
    {
        backBtn.onClick.RemoveAllListeners();
        backBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.SwitchState(GameState.MainMenu);
        });
    }
}
