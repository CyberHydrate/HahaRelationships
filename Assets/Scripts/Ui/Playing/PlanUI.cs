using UnityEngine;
using UnityEngine.UI;

public class PlanUI : MonoBehaviour
{
    public GameObject planUI;
    public Button planBtn;
    public Button exitBtn;
    private void Start()
    {
        planBtn.onClick.RemoveAllListeners();
        planBtn.onClick.AddListener(() =>
        {
            planUI.SetActive(true);
        });
        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(() =>
        {
            planUI.SetActive(false);
        });
    }
}
