using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MapGenerator:MonoBehaviour
{
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

    private int GetSteps()
    {
        return PlayerDataManager.Instance.playerData.stepCount;
    }
    private Block[] GetPlayerBlocks()
    {
        return PlayerDataManager.Instance.playerData.playerBlocks;
    }
    private Block[] GetNpcBlocks()
    {
        return PlayerDataManager.Instance.playerData.npcBlocks;
    }
    public void GenerateNext7BlocksWhenOnPlan(bool isPlayer)
    {
        Block[] targetBlocks = isPlayer ? PlayerDataManager.Instance.playerData.playerBlocks : PlayerDataManager.Instance.playerData.npcBlocks;

        for (int i = 1; i <= 7; i++)
        {
            int targetIndex = GetSteps() + i;

            if (targetIndex >= 100)
                break;

            if (targetIndex % 7 == 0)
                continue;

            if (targetBlocks[targetIndex] == null || targetBlocks[targetIndex].blockType == E_BlockType.未知)
            {
                targetBlocks[targetIndex] = GetRandomBlockByWeight();
            }
            Debug.Log((GetSteps()+i).ToString()+ PlayerDataManager.Instance.playerData.playerBlocks[GetSteps()+i].blockType);
        }

        SetMap();
    }

    public void SetMap()
    {

        for (int i = 0; i < 100; i++)
        {
            switch (GetPlayerBlocks()[i].blockType)
            {
                case E_BlockType.空:
                    playerMapList[i].GetComponent<MeshRenderer>().material = emptyMaterial;
                    break;
                case E_BlockType.事件:
                    playerMapList[i].GetComponent<MeshRenderer>().material = eventMaterial;
                    break;
                case E_BlockType.重要事件:
                    playerMapList[i].GetComponent<MeshRenderer>().material = importantEventMaterial;
                    break;
                case E_BlockType.计划:
                    playerMapList[i].GetComponent<MeshRenderer>().material = planMaterial;
                    break;
                case E_BlockType.未知:
                    playerMapList[i].GetComponent<MeshRenderer>().material = unknownMaterial;
                    break;
                default:
                    Debug.Log("如果你看到这句话，那么地块生成错误");
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
            return new Block(E_BlockType.空);
        }
        else if (random < 9)
        {
            int i = Random.Range(0, 5);
            switch (i)
            {
                case 0:
                    return WorkEventPool.GetWorkEvent();
                case 1:
                    return EntertainEventPool.GetEntertainEvent();
                case 2:
                    return RestEventPool.GetRestEvent();
                case 3:
                    return InteractEventPool.GetInteractEvent();
                case 4:
                    return SelfEventPool.GetSelfEvent();
                default:
                    return RestEventPool.GetRestEvent();
            }
        }
        else
        {
            if (currentStep <= 16)
            {
                Debug.Log("Important blocked (step ≤16), generate Event instead");
                int i = Random.Range(0, 5);
                switch (i)
                {
                    case 0:
                        return new Block(E_BlockType.事件, new WorkEvent1());
                    case 1:
                        return new Block(E_BlockType.事件, new EntertainEvent1());
                    case 2:
                        return new Block(E_BlockType.事件, new RestEvent());
                    case 3:
                        return new Block(E_BlockType.事件, new InteractEvent1());
                    case 4:
                        return new Block(E_BlockType.事件, new WorkEvent10());
                    default:
                        return new Block(E_BlockType.事件, new RestEvent());
                }
            }
            else
            {
                Debug.Log("Important");
                return new Block(E_BlockType.重要事件, new RestEvent());
            }
        }
    }

    public void MapInit()
    {
        GenerateMap();
        InitFixedPlanBlocks(PlayerDataManager.Instance.playerData.playerBlocks);//生成日程格
        InitFixedPlanBlocks(PlayerDataManager.Instance.playerData.npcBlocks);
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
                blocks[i] = new Block(E_BlockType.计划);
            }
        }
        
    }

}