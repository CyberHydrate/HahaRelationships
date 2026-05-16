using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

// 直接挂在 Dropdown 上
public class DropdownShowTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("提示框UI（拖进来）")]
    public GameObject tooltipPanel;
    public TextMeshProUGUI tipText;

    [Header("跟随鼠标偏移")]
    public Vector2 offset = new Vector2(10, -10);

    private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown=GetComponent<TMP_Dropdown>();
    }
    // 鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTooltip();
    }

    // 鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }

    void ShowTooltip()
    {
        int value = dropdown.value;

        if (value <= 0)
        {
            tooltipPanel.SetActive(false);
            return;
        }

        int index = value - 1;

        if (index >= 0 && index < ExcelReader.CharacterData.Count)
        {
            tipText.text = ExcelReader.CharacterData[index].ChaDesc;
        }
        else
        {
            tipText.text = "无数据";
        }

        tooltipPanel.transform.position = Input.mousePosition + (Vector3)offset;
        tooltipPanel.SetActive(true);
    }

    void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}