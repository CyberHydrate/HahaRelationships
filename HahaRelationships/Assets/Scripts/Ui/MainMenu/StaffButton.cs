using UnityEngine;
using UnityEngine.UI;

public class StaffButton : MonoBehaviour
{
    Button staffBtn;
    public GameObject stafflist;
    private void Awake()
    {
        staffBtn = GetComponent<Button>();
        staffBtn.onClick.RemoveAllListeners();
        staffBtn.onClick.AddListener(() =>
        {
            stafflist.SetActive(true);
        });
    }
    public void Exit()
    {
        stafflist.SetActive(false);
    }
}

