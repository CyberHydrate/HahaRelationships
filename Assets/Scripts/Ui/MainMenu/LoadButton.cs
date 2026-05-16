using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    Button loadBtn;
    private void Awake()
    {
        loadBtn = GetComponent<Button>();
        loadBtn.onClick.RemoveAllListeners();
        loadBtn.onClick.AddListener(() =>
        {
            if(SaveManager.Instance.CheckSaveFile())
            {
                SaveManager.Instance.LoadData();
                GameManager.Instance.SwitchState(GameState.Playing);
            }
            else
            {

            }
        });
    }
}
