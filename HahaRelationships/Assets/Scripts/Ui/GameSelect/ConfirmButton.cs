using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class ConfirmButton : MonoBehaviour
{
    Button confirmBtn;
    public TMP_InputField playerName;
    public TMP_InputField npcName;
    public ToggleGroup playerGender;
    public ToggleGroup npcGender;
    public ToggleGroup relationship;
    public TMP_Dropdown characteristic1;
    public TMP_Dropdown characteristic2;
    public TMP_Dropdown characteristic3;
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
        string playerGenderValue = playerGender.GetFirstActiveToggle().GetComponentInChildren<TextMeshProUGUI>().text;
        string npcGenderValue = npcGender.GetFirstActiveToggle().GetComponentInChildren<TextMeshProUGUI>().text;
        string relationshipValue = relationship.GetFirstActiveToggle().GetComponentInChildren<TextMeshProUGUI>().text;
        PlayerDataManager.Instance.playerData = new PlayerData(playerName.text, npcName.text, playerGenderValue, npcGenderValue, relationshipValue, characteristic1.value, characteristic2.value, characteristic3.value, 50, 100, 0f, 0, 50);
        SaveManager.Instance.SaveData(PlayerDataManager.Instance.playerData);
        GameManager.Instance.SwitchState(GameState.Playing);
    }
}
