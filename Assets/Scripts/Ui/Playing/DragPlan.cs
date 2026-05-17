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

    void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
    }

    // 开始拖拽：生成克隆UI
    public void OnBeginDrag(PointerEventData eventData)
    {
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
        // 1. 从松手的屏幕位置发射3D射线
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, targetLayer))
        {
            // 拿到击中的物体
            GameObject hitObj = hit.collider.gameObject;
            int id = hitObj.GetComponent<blockID>().id;
            Debug.Log($"击中场景物体：{hitObj.name} | 标签：{hitObj.tag}|序号：{id},地块类型为{ MapManager.Instance.playerBlocks[id].blockType.ToString()}");
            if (MapManager.Instance.playerBlocks[id].blockType==E_BlockType.空)
            SetEvent(id, hitObj.tag);
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
            MapManager.Instance.playerBlocks[id] =Assign(assignEvent);
            MapGenerator.Instance.SetMap();
        }
        else if (tag == "Npc")
        {
           MapManager.Instance.npcBlocks[id] =Assign(assignEvent);
            MapGenerator.Instance.SetMap();
        }
    }
    private Block Assign(E_EventType assignEvent)
    {
        switch (assignEvent)
        {
            case E_EventType.工作:
                return new Block(E_BlockType.事件,new TestWorkEvent());
            case E_EventType.娱乐:
                return new Block(E_BlockType.事件,new TestEntertainmentEvent());
            case E_EventType.休息:
                return new Block(E_BlockType.事件,new TestRestEvent());
            case E_EventType.和ta互动:
                return new Block(E_BlockType.事件,new TestInteractEvent());
            case E_EventType.自我提升:
                return new Block(E_BlockType.事件,new TestSelfEvent());
            default:
                Debug.LogError($"未知的分配事件类型：{assignEvent}");
                return null;
        }
    }
}