using UnityEngine;
using UnityEngine.UI;
public class MoveButton : MonoBehaviour
{
    Button MoveBtn;
    private void Start()
    {
        MoveBtn = GetComponent<Button>();
        MoveBtn.onClick.RemoveAllListeners();
        MoveBtn.onClick.AddListener(Move);
    }
    private void Move()
    {
        PlayerDataManager.Instance.playerData.stepCount++;
        MapManager.Instance.Move();
    }
}
