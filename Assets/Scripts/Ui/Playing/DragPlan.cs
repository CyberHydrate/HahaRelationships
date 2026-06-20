using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPlan : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [Header("射线设置")]
    public LayerMask targetLayer;   // 要检测的场景层级
    public float rayDistance = 100f;
    public E_EventType assignEvent;

    private GameObject _dragClone;
    private RectTransform _cloneRect;
    private Canvas _canvas;
    private Vector2 _offset;
    public MapGenerator mapGenerator;
    bool candrag;
    public TextMeshProUGUI count;
    void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        
        UpdateCount();
    }
    public void UpdateCount()
    {
        switch(assignEvent)
        {
            case E_EventType.工作:
                count.text = PlayerDataManager.Instance.playerData.workcount.ToString();
                break;
            case E_EventType.娱乐:
                count.text = PlayerDataManager.Instance.playerData.entertainmentcount.ToString();
                break;
            case E_EventType.休息:
                count.text = PlayerDataManager.Instance.playerData.restcount.ToString();
                break;
            case E_EventType.和ta互动:
                count.text = PlayerDataManager.Instance.playerData.interactcount.ToString();
                break;
            case E_EventType.自我提升:
                count.text = PlayerDataManager.Instance.playerData.selfimprovecount.ToString();
                break;
        }
    }
    void DeleteCount()
    {
        switch (assignEvent)
        {
            case E_EventType.工作:
                if (candrag)
                    PlayerDataManager.Instance.playerData.workcount--;
                break;
            case E_EventType.娱乐:
                if (candrag)
                    PlayerDataManager.Instance.playerData.entertainmentcount--;
                break;
            case E_EventType.休息:
                if (candrag)
                    PlayerDataManager.Instance.playerData.restcount--;
                break;
            case E_EventType.和ta互动:
                if (candrag)
                    PlayerDataManager.Instance.playerData.interactcount--;
                break;
            case E_EventType.自我提升:
                if (candrag)
                    PlayerDataManager.Instance.playerData.selfimprovecount--;
                break;
        }
    }
    bool CheckCount(E_EventType eventType)
    {
        switch (eventType)
        {
            case E_EventType.工作:
                if (PlayerDataManager.Instance.playerData.workcount <= 0)
                {
                    Debug.LogWarning("工作次数已空！");
                    return false;
                }
                break;

            case E_EventType.娱乐:
                if (PlayerDataManager.Instance.playerData.entertainmentcount <= 0)
                {
                    Debug.LogWarning("娱乐次数已空！");
                    return false;
                }
                break;

            case E_EventType.休息:
                if (PlayerDataManager.Instance.playerData.restcount <= 0)
                {
                    Debug.LogWarning("休息次数已空！");
                    return false;
                }    
                break;

            case E_EventType.和ta互动:
                if (PlayerDataManager.Instance.playerData.interactcount <= 0)
                {
                    Debug.LogWarning("和ta互动次数已空！");
                    return false;
                }
                break;

            case E_EventType.自我提升:
                if (PlayerDataManager.Instance.playerData.selfimprovecount <= 0)
                {
                    Debug.LogWarning("自我提升次数已空！");
                    return false;
                }
                break;

            default:
                Debug.LogError($"未知的事件类型：{eventType}");
                return false;
        }
        UpdateCount();
        return true;
    }
    // 开始拖拽：生成克隆UI
    public void OnBeginDrag(PointerEventData eventData)
    {
        candrag = CheckCount(assignEvent);
        if (!candrag) return;
        // 克隆本体UI
        _dragClone = Instantiate(gameObject, transform.parent);
        _cloneRect = _dragClone.GetComponent<RectTransform>();
        _cloneRect.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

        // 禁用克隆体脚本，防止递归
        _dragClone.GetComponent<DragPlan>().enabled = false;

        // 可选：克隆体半透明
        var canvasGroup = _dragClone.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0.7f;

        Camera cam = GetUICamera();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _cloneRect, eventData.position, cam, out _offset
        );
    }

    // 拖拽移动克隆体
    public void OnDrag(PointerEventData eventData)
    {
        if (!candrag) return;
        if (_dragClone == null) return;

        Camera cam = GetUICamera();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _cloneRect.parent as RectTransform,
            eventData.position,
            cam,
            out Vector2 mousePos
        );

        _cloneRect.anchoredPosition = mousePos - _offset;
    }

    // 松手：发射射线检测场景物体 + 销毁克隆UI
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!candrag) return;
        // 1. 从松手的屏幕位置发射3D射线
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, targetLayer))
        {
            // 拿到击中的物体
            GameObject hitObj = hit.collider.gameObject;
            int id = hitObj.GetComponent<blockID>().id;
            Debug.Log($"击中场景物体：{hitObj.name} | 标签：{hitObj.tag}|序号：{id},地块类型为{PlayerDataManager.Instance.playerData.playerBlocks[id].blockType.ToString()}");
            if (PlayerDataManager.Instance.playerData.playerBlocks[id].blockType == E_BlockType.空)
            {
                SetEvent(id, hitObj.tag);
            }
        }
        else
        {
            Debug.Log("松手位置没有击中任何场景物体");
        }

        // 2. 销毁克隆UI
        if (_dragClone != null)
        {
            Destroy(_dragClone);
            _dragClone = null;
        }
    }

    // 自动适配Overlay/相机模式
    private Camera GetUICamera()
    {
        return _canvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null
            : _canvas.worldCamera;
    }
    private void SetEvent(int id, string tag)
    {
        if (tag == "Player")
        {
            PlayerDataManager.Instance.playerData.playerBlocks[id] = Assign(assignEvent);
            mapGenerator.SetMap();
        }
        else if (tag == "Npc")
        {
            PlayerDataManager.Instance.playerData.npcBlocks[id] = Assign(assignEvent);
            mapGenerator.SetMap();
        }
        DeleteCount();
        UpdateCount();
    }
    private Block Assign(E_EventType assignEvent)
    {
        switch (assignEvent)
        {
            case E_EventType.工作:
                return WorkEventPool.GetWorkEvent();
            case E_EventType.娱乐:
                return EntertainEventPool.GetEntertainEvent();
            case E_EventType.休息:
                return RestEventPool.GetRestEvent();
            case E_EventType.和ta互动:
                return InteractEventPool.GetInteractEvent();
            case E_EventType.自我提升:
                return SelfEventPool.GetSelfEvent();
            default:
                Debug.LogError($"未知的分配事件类型：{assignEvent}");
                return null;
        }
    }
}