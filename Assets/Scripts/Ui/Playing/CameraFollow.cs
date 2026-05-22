using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform player;

    [Header("相机相对玩家的固定偏移")]
    public Vector3 cameraOffset;

    [Header("拖动控制")]
    public float dragSpeed = 0.3f;
    public float minZ = -8f;
    public float maxZ = 15f;

    private Quaternion _baseRot;
    private float _dragZ;

    public bool _isPlaying;

    void Awake()
    {
        _isPlaying = false;
    }

    // 初始化：相机 = 玩家位置 + 固定偏移
    public void Init()
    {
        if (player == null) return;

        transform.position = player.position + cameraOffset;
        _baseRot = transform.rotation;
        _dragZ = 0;
        _isPlaying = true;
    }

    void LateUpdate()
    {
        if (player == null || !_isPlaying) return;

        // 拖动控制
        if (Input.GetMouseButton(1))
        {
            _dragZ -= Input.GetAxis("Mouse X") * dragSpeed;
            _dragZ = Mathf.Clamp(_dragZ, minZ, maxZ);
        }

        // ==============================================
        // 相机 = 当前玩家位置 + 固定偏移 + 拖动Z
        // ==============================================
        Vector3 finalPos = player.position + cameraOffset;
        finalPos.z += _dragZ;

        transform.position = finalPos;
        transform.rotation = _baseRot;
    }
}