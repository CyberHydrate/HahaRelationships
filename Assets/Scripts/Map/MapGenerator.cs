using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MapGenerator:MonoBehaviour
{
    public Transform playerMap;
    public Transform npcMap;
    public GameObject planePrefab;

    public Material emptyMaterial;
    public Material planMaterial;
    public Material unknownMaterial;
    public Material workMaterial;
    public Material entertainMaterial;
    public Material selfMaterial;
    public Material interactMaterial;
    public Material restMaterial;
    public Material importantWorkMaterial;
    public Material importantEntertainMaterial;
    public Material importantInteractMaterial;

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
                case E_BlockType.工作:
                    playerMapList[i].GetComponent<MeshRenderer>().material = workMaterial;
                    break;
                case E_BlockType.娱乐:
                    playerMapList[i].GetComponent<MeshRenderer>().material = entertainMaterial;
                    break;
                case E_BlockType.和ta互动:
                    playerMapList[i].GetComponent<MeshRenderer>().material = interactMaterial;
                    break;
                case E_BlockType.休息:
                    playerMapList[i].GetComponent<MeshRenderer>().material = restMaterial;
                    break;
                case E_BlockType.自我提升:
                    playerMapList[i].GetComponent<MeshRenderer>().material = selfMaterial;
                    break;
                case E_BlockType.重要工作:
                    playerMapList[i].GetComponent<MeshRenderer>().material = importantWorkMaterial;
                    break;
                case E_BlockType.重要娱乐:
                    playerMapList[i].GetComponent<MeshRenderer>().material = importantEntertainMaterial;
                    break;
                case E_BlockType.重要和ta互动:
                    playerMapList[i].GetComponent<MeshRenderer>().material = importantInteractMaterial;
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
                Debug.Log("Important");
                int i = Random.Range(0, 3);
                switch (i) 
                {
                    case 0:
                        return ImportantWorkEventPool.GetImportantWorkEvent();
                    case 1:
                        return ImportantEntertainEventPool.GetImportantEntertainEvent();
                    case 2:
                        return ImportantInteractEventPool.GetImportantInteractEvent();
                    default:
                        return ImportantEntertainEventPool.GetImportantEntertainEvent();
                }

            }
        }
    }

    public void MapInit()
    {
        GenerateMap();
        InitFixedPlanBlocks(PlayerDataManager.Instance.playerData.playerBlocks);//生成日程格
        InitFixedPlanBlocks(PlayerDataManager.Instance.playerData.npcBlocks);
        if(PlayerDataManager.Instance.playerData.stepCount==0)
        GenerateNext7BlocksWhenOnPlan(true);
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