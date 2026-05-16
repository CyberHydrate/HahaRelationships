using UnityEngine;
public class SettingData
{
    public int screenWidth;
    public int screenHeight;
    public bool isFullScreen;
}
public class SettingDataManager
{
    #region 单例模式实现
    private static readonly SettingDataManager instance = new SettingDataManager();
    public static SettingDataManager Instance { get { return instance; } }

    private SettingDataManager() { }//构造函数私有化，确保外部无法实例化
    #endregion
    
    public SettingData settingData = new SettingData();
    public void SetScreenResolution()
    {
        Screen.SetResolution(settingData.screenWidth, settingData.screenHeight, settingData.isFullScreen);
    }
    
}
