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
        if (d.value == 0)
            d.value = old;
        else if ((d1 != 0 && (d1 == d2)) || (d2 != 0 && (d2 == d3)) || (d3 != 0 && (d1 == d3)))
        {
            dropdown1.value = old1;
            dropdown2.value = old2;
            dropdown3.value = old3;
        }
        else if (Check1())
        {
            dropdown1.value = old1;
            dropdown2.value = old2;
            dropdown3.value = old3;
        }
        else if(Check2())
        {
            dropdown1.value = old1;
            dropdown2.value = old2;
            dropdown3.value = old3;
        }
        else
        {
            // 没重复就记录新状态
            old1 = dropdown1.value;
            old2 = dropdown2.value;
            old3 = dropdown3.value;
            ShowText();
        }
    }
    bool Check1()
    {
        int i = 0;
        if (d1 > 0 && d1 < 5)
            i++;
        if(d2> 0 && d2 < 5)
            i++;
        if (d3 > 0 &&d3<5)
            i++;
        if(i>1)
            return true;
        return false;
    }
    bool Check2()
    {
        int i = 0;
        if(d1>38&&d1<41)
            i++;
        if(d2>38&&d2<41)
            i++;
        if(d3>38&&d3<41)
            i++;
        if(i>1) return true;
        return false;

    }
    void ShowText()
    {
        if (dropdown1.value != 0)
            text1.text = ExcelReader.CharacterData[dropdown1.value - 1].ChaDesc;
        else
            text1.text = "";
        if (dropdown2.value != 0)
            text2.text = ExcelReader.CharacterData[dropdown2.value - 1].ChaDesc;
        else
            text2.text = "";
        if (dropdown3.value != 0)
            text3.text = ExcelReader.CharacterData[dropdown3.value - 1].ChaDesc;
        else
            text3.text = "";
    }
}