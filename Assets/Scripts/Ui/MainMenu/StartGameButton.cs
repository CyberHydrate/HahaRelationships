using TMPro;
using UnityEngine;
using UnityEngine.UI;   

public class StartGameButton : MonoBehaviour
{
    Button startBtn;
    public Button yesBtn;
    public Button noBtn;
    public GameObject saveTip;
    public TextMeshProUGUI tip;
    private void Awake()
    {
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.SwitchState(GameState.GameSelect);
           saveTip.SetActive(false);
        });


        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() =>
        {
            saveTip.SetActive(false);
        });


        startBtn = GetComponent<Button>();
        startBtn.onClick.RemoveAllListeners();
        startBtn.onClick.AddListener(() =>
        {
            if(SaveManager.Instance.CheckSaveFile())
            {
                tip.text = "检测到已有存档，确定要覆盖吗？";
                saveTip.SetActive(true);
            }
            else
            {
                GameManager.Instance.SwitchState(GameState.GameSelect);
            }
        });
    }
}
