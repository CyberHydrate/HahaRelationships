using UnityEngine;

public class MapManager : MonoBehaviour
{
    #region 单例模式实现
    public static MapManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
          Destroy(gameObject);
            return;
        }
        Instance = this;
        MapManager.Instance.GenerateMap();
        //DontDestroyOnLoad(gameObject);
    }
    #endregion
    public GameObject[] playerMap = new GameObject[100];
    public GameObject[] npcMap = new GameObject[100];
    public Transform playerMapList;
    public Transform npcMapList;
    public GameObject planePrefab;
    public void GenerateMap()
    {
        Debug.Log("Generating map...");
        for (int i = 0; i < 100; i++)
        {
            Vector3 pos = new Vector3(10, 0, i * 10);
            GameObject plane = Instantiate(planePrefab, pos, Quaternion.identity);
            plane.transform.parent = playerMapList;
            plane.name = "Plane_" + i;
            playerMap[i] = plane;
        }
        for (int i = 0; i < 100; i++)
        {
            Vector3 pos = new Vector3(-10, 0, i * 10);
            GameObject plane = Instantiate(planePrefab, pos, Quaternion.identity);
            plane.transform.parent = npcMapList;
            plane.name = "Plane_" + i;
            npcMap[i] = plane;
        }
    }
}
