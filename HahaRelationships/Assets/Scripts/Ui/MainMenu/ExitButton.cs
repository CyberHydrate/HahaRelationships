using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    Button exitBtn;
    private void Awake()
    {
        exitBtn = GetComponent<Button>();
        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(() =>
        {
            Debug.Log("Exit button clicked. Quitting application...");
            Application.Quit();
        });
    }
}
