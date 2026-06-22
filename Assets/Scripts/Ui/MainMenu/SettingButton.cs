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

    const int MIN_W = 800;
    const int MIN_H = 600;
    const int MAX_W = 3840;
    const int MAX_H = 2160;

    private void Awake()
    {
        setBtn.onClick.AddListener(OpenPanel);
        changeBtn.onClick.AddListener(ApplySetting);
        cancelBtn.onClick.AddListener(() => settingPanel.SetActive(false));
    }

    void OpenPanel()
    {
        settingPanel.SetActive(true);
        // 读取本地赋值输入框
        int w = PlayerPrefs.GetInt("GameScreenW", 1920);
        int h = PlayerPrefs.GetInt("GameScreenH", 1080);
        bool full = PlayerPrefs.GetInt("GameFullScreen", 1) == 1;

        screenWidthInput.text = w.ToString();
        screenHeightInput.text = h.ToString();
        fullScreenToggle.isOn = full;
    }

    public void ApplySetting()
    {
        // 安全解析
        if (!int.TryParse(screenWidthInput.text, out int width))
            width = PlayerPrefs.GetInt("GameScreenW", 1920);
        if (!int.TryParse(screenHeightInput.text, out int height))
            height = PlayerPrefs.GetInt("GameScreenH", 1080);

        // 限制范围
        width = Mathf.Clamp(width, MIN_W, MAX_W);
        height = Mathf.Clamp(height, MIN_H, MAX_H);
        bool isFull = fullScreenToggle.isOn;

        // 存本地
        PlayerPrefs.SetInt("GameScreenW", width);
        PlayerPrefs.SetInt("GameScreenH", height);
        PlayerPrefs.SetInt("GameFullScreen", isFull ? 1 : 0);
        PlayerPrefs.Save();

        // 立刻生效
        FullScreenMode mode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(width, height, mode);

        settingPanel.SetActive(false);
    }
}

public class InitScreenSetting : MonoBehaviour
{
    void Start()
    {
        int w = PlayerPrefs.GetInt("GameScreenW", 1920);
        int h = PlayerPrefs.GetInt("GameScreenH", 1080);
        bool full = PlayerPrefs.GetInt("GameFullScreen", 1) == 1;
        FullScreenMode mode = full ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(w, h, mode);
    }
}