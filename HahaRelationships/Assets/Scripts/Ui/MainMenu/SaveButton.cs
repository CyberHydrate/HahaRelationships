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
            
            SaveManager.Instance.SaveData(PlayerDataManager.Instance.playerData);
        });
    }
}
