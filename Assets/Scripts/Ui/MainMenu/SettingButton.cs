using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    public Button setBtn;
    public Button changeBtn;
    public Button cancelBtn;
    public GameObject settingPanel;
    public TMP_InputField screenWidthInput;
    public TMP_InputField screenHeightInput;
    public Toggle fullScreenToggle;
    private void Awake()
    {
        setBtn.onClick.RemoveAllListeners();
        setBtn.onClick.AddListener(() =>
        {
            settingPanel.SetActive(true);
            screenWidthInput.text = SettingDataManager.Instance.settingData.screenWidth.ToString();
            screenHeightInput.text = SettingDataManager.Instance.settingData.screenHeight.ToString();
        });
        changeBtn.onClick.RemoveAllListeners();
        changeBtn.onClick.AddListener(() =>
        {
            ChangeSet();
            settingPanel.SetActive(false);
        });
        cancelBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.AddListener(() =>
        {
            settingPanel.SetActive(false);
        });
    }
    public void ChangeSet()
    {
        int.TryParse(screenWidthInput.text, out SettingDataManager.Instance.settingData.screenWidth);
        int.TryParse(screenHeightInput.text, out SettingDataManager.Instance.settingData.screenHeight);
        SettingDataManager.Instance.settingData.isFullScreen = fullScreenToggle.isOn;
        settingPanel.SetActive(false);
    }
}
