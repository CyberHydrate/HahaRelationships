using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollowDragZ : MonoBehaviour
{
    public Transform player;

    [Header("拖动控制")]
    public float dragSpeed = 0.3f;
    [Header("Z轴拖动范围（相对初始Z）")]
    public float minZ = -8f;
    public float maxZ = 15f;

    private Vector3 _startCamPos;
    private Quaternion _startCamRot;
    private float _dragZ;
    private Vector3 _startPlayerPos;

    void Start()
    {
        _startCamPos = transform.position;
        _startCamRot = transform.rotation;
        _startPlayerPos = player.position;
        _dragZ = 0;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // 右键左右拖动控制Z
        if (Input.GetMouseButton(1))
        {
            _dragZ -= Input.GetAxis("Mouse X") * dragSpeed;
            _dragZ = Mathf.Clamp(_dragZ, minZ, maxZ);
        }

        // 玩家的总偏移 = 当前玩家位置 - 初始玩家位置
        Vector3 playerOffset = player.position - _startPlayerPos;

        // 相机 = 初始相机位置 + 玩家整体偏移 + 拖动Z偏移
        Vector3 newPos = _startCamPos + playerOffset;
        newPos.z += _dragZ;

        transform.position = newPos;
        transform.rotation = _startCamRot;
    }
}