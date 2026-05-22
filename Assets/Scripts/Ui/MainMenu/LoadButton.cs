using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    Button loadBtn;
    public GameObject saveTip;
    public TextMeshProUGUI tip;
    private void Awake()
    {
        loadBtn = GetComponent<Button>();
        loadBtn.onClick.RemoveAllListeners();
        loadBtn.onClick.AddListener(() =>
        {
            if(SaveManager.Instance.CheckSaveFile())
            {
                SaveManager.Instance.LoadData();
                Debug.Log(PlayerDataManager.Instance.playerData.stepCount.ToString());
                GameManager.Instance.SwitchState(GameState.Playing);
            }
            else
            {
                tip.text = "当前存档为空，是否新建存档？";
                saveTip.SetActive(true);
            }
        });
    }
}
