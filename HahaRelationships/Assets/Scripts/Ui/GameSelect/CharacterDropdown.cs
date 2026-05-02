using UnityEngine;
using TMPro;

public class CharacterDropdown : MonoBehaviour
{
    public TMP_Dropdown drowdown1, dropdown2, dropdown3;

    int old1, old2, old3;

    private void Awake()
    {
        drowdown1.value = 0;
        dropdown2.value = 1;
        dropdown3.value = 2;
    }
    void Start()
    {    
        old1 = drowdown1.value;
        old2 = dropdown2.value;
        old3 = dropdown3.value;

        drowdown1.onValueChanged.AddListener(v => Check());
        dropdown2.onValueChanged.AddListener(v => Check());
        dropdown3.onValueChanged.AddListener(v => Check());
    }

    void Check()
    {
        // 有重复就还原
        if (drowdown1.value == dropdown2.value || drowdown1.value == dropdown3.value || dropdown2.value == dropdown3.value)
        {
            drowdown1.value = old1;
            dropdown2.value = old2;
            dropdown3.value = old3;
        }
        else
        {
            // 没重复就记录新状态
            old1 = drowdown1.value;
            old2 = dropdown2.value;
            old3 = dropdown3.value;
        }
    }
}