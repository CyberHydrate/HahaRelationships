using UnityEngine;
using TMPro;

public class CharacterDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI desc;
    int old;

    private void Awake()
    {
        dropdown.value = 0;
    }

    void Start()
    {
        dropdown.onValueChanged.AddListener(v => Check(dropdown, old));
    }

    void Check(TMP_Dropdown d, int old)
    {
        // 不允许选0
        if (d.value == 0)
        {
            d.value = old;
            return;
        }

        // 合法
        old= d.value;
        ShowText();
    }
    void ShowText()
    {
        desc.text = dropdown.value != 0 ? ExcelReader.CharacterData[dropdown.value - 1].ChaDesc : "";
    }
}