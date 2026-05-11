using UnityEngine;
using TMPro;

public class CharacterDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown1, dropdown2, dropdown3;
    public TextMeshProUGUI text1, text2, text3;
    int d1, d2, d3;
    int old1, old2, old3;

    private void Awake()
    {
        dropdown1.value = 0;
        dropdown2.value = 0;
        dropdown3.value = 0;
        d1 = dropdown1.value;
        d2 = dropdown2.value;
        d3 = dropdown3.value;
    }

    void Start()
    {
        old1 = d1;
        old2 = d2;
        old3 = d3;

        dropdown1.onValueChanged.AddListener(v => Check(dropdown1, old1));
        dropdown2.onValueChanged.AddListener(v => Check(dropdown2, old2));
        dropdown3.onValueChanged.AddListener(v => Check(dropdown3, old3));
    }

    void Check(TMP_Dropdown d, int old)
    {
        d1 = dropdown1.value;
        d2 = dropdown2.value;
        d3 = dropdown3.value;

        // 不允许选0
        if (d.value == 0)
        {
            d.value = old;
            return;
        }

        // 不允许重复
        if ((d1 != 0 && d1 == d2) || (d2 != 0 && d2 == d3) || (d3 != 0 && d1 == d3))
        {
            Rollback();
            return;
        }

        // 1-4 最多1个
        if (Check1())
        {
            Rollback();
            return;
        }

        // 39-40 最多1个
        if (Check2())
        {
            Rollback();
            return;
        }

        // 61-69 最多1个（你之前漏加的！）
        if (Check3())
        {
            Rollback();
            return;
        }

        // 全部合法
        old1 = d1;
        old2 = d2;
        old3 = d3;
        ShowText();
    }

    void Rollback()
    {
        dropdown1.value = old1;
        dropdown2.value = old2;
        dropdown3.value = old3;
    }

    bool Check1() // 1-4
    {
        int i = 0;
        if (d1 > 0 && d1 < 5) i++;
        if (d2 > 0 && d2 < 5) i++;
        if (d3 > 0 && d3 < 5) i++;
        return i > 1;
    }

    bool Check2() // 39-40
    {
        int i = 0;
        if (d1 > 38 && d1 < 41) i++;
        if (d2 > 38 && d2 < 41) i++;
        if (d3 > 38 && d3 < 41) i++;
        return i > 1;
    }

    bool Check3() // 61-69
    {
        int i = 0;
        if (d1 > 60 && d1 < 70) i++;
        if (d2 > 60 && d2 < 70) i++;
        if (d3 > 60 && d3 < 70) i++;
        return i > 1;
    }

    void ShowText()
    {
        text1.text = dropdown1.value != 0 ? ExcelReader.CharacterData[dropdown1.value - 1].ChaDesc : "";
        text2.text = dropdown2.value != 0 ? ExcelReader.CharacterData[dropdown2.value - 1].ChaDesc : "";
        text3.text = dropdown3.value != 0 ? ExcelReader.CharacterData[dropdown3.value - 1].ChaDesc : "";
    }
}