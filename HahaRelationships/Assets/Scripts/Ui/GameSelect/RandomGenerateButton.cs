using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class RandomGenerateButton : MonoBehaviour
{
    Button randomBtn;
    public TMP_Dropdown d1;
    public TMP_Dropdown d2;
    public TMP_Dropdown d3;

    private void Awake()
    {
        randomBtn = GetComponent<Button>();
        randomBtn.onClick.RemoveAllListeners();
        randomBtn.onClick.AddListener(RandomGenerate);
    }

    private void RandomGenerate()
    {
        int v1, v2, v3;

        // 循环直到生成符合所有规则的组合
        while (true)
        {
            // 随机 1 ~ 最大选项（不包含0）
            v1 = Random.Range(1, d1.options.Count);
            v2 = Random.Range(1, d2.options.Count);
            v3 = Random.Range(1, d3.options.Count);

            // 规则1：三个不能重复
            if (v1 == v2 || v2 == v3 || v1 == v3)
                continue;

            // 规则2：1-4 最多只能有1个
            int count1_4 = 0;
            if (v1 > 0 && v1 < 5) count1_4++;
            if (v2 > 0 && v2 < 5) count1_4++;
            if (v3 > 0 && v3 < 5) count1_4++;
            if (count1_4 > 1)
                continue;

            // 规则3：39-40 最多只能有1个
            int count39_40 = 0;
            if (v1 > 38 && v1 < 41) count39_40++;
            if (v2 > 38 && v2 < 41) count39_40++;
            if (v3 > 38 && v3 < 41) count39_40++;
            if (count39_40 > 1)
                continue;
            int count61_69 = 0;
            if (v1 > 60 && v1 < 70) count61_69++;
            if (v2 >60&&v2 <70) count61_69++;
            if (v3 > 60 && v3 < 70) count61_69++;
            if (count61_69 > 1)
                continue;

            // 所有规则都满足 → 跳出循环
            break;
        }

        // 赋值
        d1.value = v1;
        d2.value = v2;
        d3.value = v3;
    }
}