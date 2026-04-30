using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    Button saveBtn;
    private void Awake()
    {
        saveBtn = GetComponent<Button>();
        saveBtn.onClick.RemoveAllListeners();
        saveBtn.onClick.AddListener(() =>
        {
            //占位符，存档需要将具体游戏数据存入SaveData对象中，目前先用默认值测试
            //占位符，存档需要将具体游戏数据存入SaveData对象中，目前先用默认值测试
            //占位符，存档需要将具体游戏数据存入SaveData对象中，目前先用默认值测试
            SaveManager.Instance.SaveData(new SaveData("PlayerName", "NPCName", "PlayerGender", "NPCGender", "Relationship", "Characteristic1", "Characteristic2", "Characteristic3", 0f));
        });
    }
}
