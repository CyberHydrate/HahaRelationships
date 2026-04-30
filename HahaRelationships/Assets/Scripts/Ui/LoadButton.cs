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
            SaveManager.Instance.LoadData();
        });
    }
}
