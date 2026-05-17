using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MapGenerator:MonoBehaviour
{
    #region 单例模式实现
    public static MapGenerator Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    #endregion
    public Transform playerMap;
    public Transform npcMap;
    public GameObject planePrefab;

    public Material emptyMaterial;
    public Material eventMaterial;
    public Material importantEventMaterial;
    public Material planMaterial;
    public Material unknownMaterial;

    [System.NonSerialized]
    public GameObject[] playerMapList = new GameObject[101];
    [System.NonSerialized]
    public GameObject[] npcMapList = new GameObject[101];


    public void GenerateNext7BlocksWhenOnPlan(bool isPlayer)
    {
        Block[] playerBlocks = MapManager.Instance.playerBlocks;
        Block[] npcBlocks = MapManager.Instance.npcBlocks;

        int currentIndex = PlayerDataManager.Instance.playerData.stepCount;
        Block[] targetBlocks = isPlayer ? playerBlocks : npcBlocks;

        for (int i = 1; i <= 7; i++)
        {
            int targetIndex = currentIndex + i;

            if (targetIndex >= 100)
                break;

            if (targetIndex % 7 == 0)
                continue;

            if (targetBlocks[targetIndex] == null || targetBlocks[targetIndex].blockType == E_BlockType.Unknown)
            {
                targetBlocks[targetIndex] = GetRandomBlockByWeight();
            }
        }

        SetMap();
    }

    public void SetMap()
    {
        Block[] playerBlocks = MapManager.Instance.playerBlocks;
        Block[] npcBlocks = MapManager.Instance.npcBlocks;

        for (int i = 0; i < 100; i++)
        {
            //if (playerBlocks[i]==null)
            //{
            //    playerBlocks[i] = new Block(E_BlockType.Empty);
            //}
            switch (playerBlocks[i].blockType)
            {
                case E_BlockType.Empty:
                    playerMapList[i].GetComponent<MeshRenderer>().material = emptyMaterial;
                    break;
                case E_BlockType.Event:
                    playerMapList[i].GetComponent<MeshRenderer>().material = eventMaterial;
                    break;
                case E_BlockType.Important:
                    playerMapList[i].GetComponent<MeshRenderer>().material = importantEventMaterial;
                    break;
                case E_BlockType.Plan:
                    playerMapList[i].GetComponent<MeshRenderer>().material = planMaterial;
                    break;
                case E_BlockType.Unknown:
                    playerMapList[i].GetComponent<MeshRenderer>().material = unknownMaterial;
                    break;
                default:
                    playerMapList[i].GetComponent<MeshRenderer>().material = unknownMaterial;
                    break;
            }
        }
    }

    private Block GetRandomBlockByWeight()
    {
        int random = Random.Range(0, 10);

        int currentStep = PlayerDataManager.Instance.playerData.stepCount;

        if (random < 7)
        {
            Debug.Log("Empty");
            return new Block(E_BlockType.Empty);
        }
        else if (random < 9)
        {
            Debug.Log("Event");
            int i = Random.Range(0, 9);
            switch (i)
            {
                case 0:
                    return new Block(E_BlockType.Event, new TestWorkEvent());
                case 1:
                    return new Block(E_BlockType.Event, new TestEntertainmentEvent());
                case 2:
                    return new Block(E_BlockType.Event, new TestRestEvent());
                case 3:
                    return new Block(E_BlockType.Event, new TestInteractEvent());
                case 4:
                    return new Block(E_BlockType.Event, new Event6());
                case 5:
                    return new Block(E_BlockType.Event, new Event7());
                case 6:
                    return new Block(E_BlockType.Event, new Event8());
                case 7:
                    return new Block(E_BlockType.Event, new Event9());
                case 8:
                    return new Block(E_BlockType.Event, new Event10());
                case 9:
                    return new Block(E_BlockType.Event, new TestSelfEvent());
                default:
                    return new Block(E_BlockType.Event, new TestRestEvent());
            }
        }
        else
        {
            if (currentStep <= 16)
            {
                Debug.Log("Important blocked (step ≤16), generate Event instead");
                int i = Random.Range(0, 9);
                switch (i)
                {
                    case 0:
                        return new Block(E_BlockType.Event, new TestWorkEvent());
                    case 1:
                        return new Block(E_BlockType.Event, new TestEntertainmentEvent());
                    case 2:
                        return new Block(E_BlockType.Event, new TestRestEvent());
                    case 3:
                        return new Block(E_BlockType.Event, new TestInteractEvent());
                    case 4:
                        return new Block(E_BlockType.Event, new Event6());
                    case 5:
                        return new Block(E_BlockType.Event, new Event7());
                    case 6:
                        return new Block(E_BlockType.Event, new Event8());
                    case 7:
                        return new Block(E_BlockType.Event, new Event9());
                    case 8:
                        return new Block(E_BlockType.Event, new Event10());
                    case 9:
                        return new Block(E_BlockType.Event, new TestSelfEvent());
                    default:
                        return new Block(E_BlockType.Event, new TestRestEvent());
                }
            }
            else
            {
                Debug.Log("Important");
                return new Block(E_BlockType.Important, new TestRestEvent());
            }
        }
    }

    public void MapInit()
    {
        GenerateMap();
        InitFixedPlanBlocks(MapManager.Instance.playerBlocks);
        InitFixedPlanBlocks(MapManager.Instance.npcBlocks);
        SetMap();
    }
    public void GenerateMap()
    {
        Debug.Log("Generating map...");
        for (int i = 0; i < 101; i++)
        {
            Vector3 pos = new Vector3(10, 0, i * 10);
            GameObject plane = Instantiate(planePrefab, pos, Quaternion.identity);
            plane.tag = "Player";
            plane.transform.parent = playerMap;
            plane.name = "Plane_" + i;
            plane.GetComponent<blockID>().id = i;
            playerMapList[i] = plane;
        }
        for (int i = 0; i < 101; i++)
        {
            Vector3 pos = new Vector3(-10, 0, i * 10);
            GameObject plane = Instantiate(planePrefab, pos, Quaternion.identity);
            plane.tag = "Npc";
            plane.transform.parent = npcMap;
            plane.name = "Plane_" + i;
            plane.GetComponent<blockID>().id = i;
            npcMapList[i] = plane;
        }
    }

    private void InitFixedPlanBlocks(Block[] blocks)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (i % 7 == 0)
            {
                blocks[i] = new Block(E_BlockType.Plan);
            }
            else
            {
                blocks[i] = new Block(E_BlockType.Unknown);
            }
        }
    }

}