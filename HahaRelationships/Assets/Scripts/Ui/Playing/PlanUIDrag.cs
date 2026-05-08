using UnityEngine;
using UnityEngine.EventSystems;


public class PlanUIDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform _rect;
    private Canvas _canvas;
    private Vector2 _startOffset;

    void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        //if (MapManager.Instance.currentThing.Type is not E_MapThingType.Schedule) return;

        RectTransform parentRect = _rect.parent as RectTransform;
        if (parentRect == null) return;

        // 适配 Overlay / Camera 模式
        Camera cam = _canvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null
            : _canvas.worldCamera;

        // 计算：鼠标在父容器中的位置 - UI当前锚点位置 = 偏移
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            cam,
            out Vector2 mousePos
        );
        _startOffset = mousePos - _rect.anchoredPosition;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (MapManager.Instance.currentThing.Type is not E_MapThingType.Schedule) return;
        RectTransform parentRect = _rect.parent as RectTransform;
        if (parentRect == null) return;

        Camera cam = _canvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null
            : _canvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            cam,
            out Vector2 mousePos
        );

        // 实时赋值：鼠标位置 - 固定偏移 → 不会跳位置
        _rect.anchoredPosition = mousePos - _startOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}