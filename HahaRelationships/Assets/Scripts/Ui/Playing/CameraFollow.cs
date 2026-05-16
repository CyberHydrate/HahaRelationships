using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraDragFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 baseOffset = new Vector3(0, 10, -12);
    public float dragSpeed = 0.3f;

    private Vector3 dragOffset = Vector3.zero;
    public Vector3 rotationOffset;

    void LateUpdate()
    {
        if (player == null) return;

        // 按住鼠标右键拖动
        if (Input.GetMouseButton(1))
        {
            // 左右拖动 → X 轴
            dragOffset.x += Input.GetAxis("Mouse X") * dragSpeed;
            // 上下拖动 → Z 轴（前后）
            dragOffset.z += Input.GetAxis("Mouse Y") * dragSpeed;
        }

        // 相机位置 = 玩家位置 + 固定偏移 + 拖动偏移
        transform.position = player.position + baseOffset + dragOffset;

        // 固定俯视角度，永远不随玩家转
        // 将 transform.rotation = rotationOffset; 替换为如下代码：
        transform.rotation = Quaternion.Euler(rotationOffset);
    }
}