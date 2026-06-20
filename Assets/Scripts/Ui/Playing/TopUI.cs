using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TopUI : MonoBehaviour
{
    public TextMeshProUGUI stepCount;
    public Scrollbar hpBar;
    public Scrollbar relationshipBar;
    private void Update()
    {
        stepCount.text =PlayerDataManager.Instance.playerData.stepCount.ToString();
        hpBar.size= (float)PlayerDataManager.Instance.playerData.playerhp / 100f;
        relationshipBar.size = PlayerDataManager.Instance.playerData.relationshiphp / 100f;
    }

}