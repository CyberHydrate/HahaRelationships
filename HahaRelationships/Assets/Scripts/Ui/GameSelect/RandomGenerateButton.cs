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
        randomBtn.onClick.AddListener(() =>
        {
            RandomGenerate();
        });
    }
    private void RandomGenerate()
    {
        d1.value = Random.Range(0, d1.options.Count);
        do
        {
            d2.value = Random.Range(0, d2.options.Count);
        } while (d2.value == d1.value);
        do
        { 
            d3.value = Random.Range(0, d3.options.Count);
        } while (d3.value == d1.value || d3.value == d2.value);
    }
}
