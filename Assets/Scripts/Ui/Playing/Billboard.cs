using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        // 让纸片永远面向相机（标准2D纸片效果）
        transform.rotation = mainCam.transform.rotation;
    }
}
