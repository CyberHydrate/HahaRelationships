using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ConfirmButton : MonoBehaviour
{
    Button confirmBtn;
    public TMP_InputField playerName;
    public TMP_InputField npcName;
    public ToggleGroup playerGender;
    public ToggleGroup npcGender;
    public ToggleGroup relationship;
    public TMP_Dropdown characteristic;
    private void Awake()
    {
        confirmBtn = GetComponent<Button>();
        confirmBtn.onClick.RemoveAllListeners();
        confirmBtn.onClick.AddListener(() =>
        {
            SaveData();
        });
    }
    private void SaveData()
    {
        string playerGenderValue = playerGender.ActiveToggles().FirstOrDefault().name;
        string npcGenderValue = npcGender.GetFirstActiveToggle().name;
        string relationshipValue = relationship.GetFirstActiveToggle().name;
        PlayerDataManager.Instance.playerData = new PlayerData(playerName.text, npcName.text, playerGenderValue, npcGenderValue, relationshipValue, characteristic.value,  50, 100, 0f, 0, 50);
        SaveManager.Instance.SaveData(PlayerDataManager.Instance.playerData);
        GameManager.Instance.SwitchState(GameState.Playing);
    }
}
