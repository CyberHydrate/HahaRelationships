using UnityEngine;
using UnityEngine.UI;   

public class StartGameButton : MonoBehaviour
{
    Button startBtn;
    public Button yesBtn;
    public Button noBtn;
    public GameObject saveTip;
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
                saveTip.SetActive(true);
            }
            else
            {
                GameManager.Instance.SwitchState(GameState.GameSelect);
            }
        });
    }
}
