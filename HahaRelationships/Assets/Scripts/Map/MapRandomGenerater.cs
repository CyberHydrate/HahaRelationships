using UnityEngine;

public class MapRandomGenerator : MonoBehaviour
{   
#region 预制体及固定设置
public GameObject EmptyPrefab;      // 空白地块
public GameObject EventPrefab;      // 事件地块
public GameObject SignificantPrefab;  // 重要事件地块
public GameObject PlanPrefab;    // 日程安排地块

public int PlanInterval = 7; // 每7块一个日程安排地块
#endregion

public static MapRandomGenerator Instance;

void Awake()
{
    Instance = this;
}

void Start()
{
    GenerateAllMap();
}

public void GenerateAllMap()
{
    GenerateSideMap(MapManager.Instance.playerMap, MapManager.Instance.playerMapList);
    GenerateSideMap(MapManager.Instance.npcMap, MapManager.Instance.npcMapList);
    Debug.Log("✅ 地图生成完成：7空白 2事件 1重要事件 | 每7块固定日程安排地块");
}

void GenerateSideMap(GameObject[] mapArray, Transform parent)
{
    for (int i = 0; i < mapArray.Length; i++)
    {
        GameObject oldPlane = mapArray[i];
        if (oldPlane == null) continue;

        Vector3 pos = oldPlane.transform.position;
        Quaternion rot = oldPlane.transform.rotation;
        Destroy(oldPlane);

        GameObject newPlane = null;

        if (i % PlanInterval == 0 )
        {
            newPlane = Instantiate(PlanPrefab, pos, rot);
            newPlane.name = $"PlanPlane_{i}";
        }
        else
        {
            newPlane = GetRandomPlaneByWeight();
            newPlane = Instantiate(newPlane, pos, rot);
            newPlane.name = $"{newPlane.name}_{i}";
        }

        newPlane.transform.parent = parent;
        mapArray[i] = newPlane;
    }
}

GameObject GetRandomPlaneByWeight()
{
    int random = Random.Range(0, 10);

    if (random < 7)
        return EmptyPrefab;
    else if (random < 9)
        return EventPrefab;
    else
        return SignificantPrefab;
}
}