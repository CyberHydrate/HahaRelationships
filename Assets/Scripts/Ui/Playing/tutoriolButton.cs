using UnityEngine;
using UnityEngine.UI;

public class tutoriolButton : MonoBehaviour
{
    public GameObject tutoriolUI;
    public Button exitBtn;
    private void Start()
    {
        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(() =>
        {
            tutoriolUI.SetActive(false);
        });
    }
}
