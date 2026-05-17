using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 数据结构
public class CharacterDataRow
{
    public int ChaID;
    public string ChaName;
    public string ChaDesc;
    public string ChaEffect;
}
public class ChoiceDataRow
{
    public string choiceName;
    public E_ChoiceType choiceType;
    public string choiceDesc;
}
public class EventDataRow
{
    public int eventID;
    public string eventName;
    public E_EventType eventType;
    public string eventDesc;
    public int choiceCount;

    public ChoiceDataRow[] choices=new ChoiceDataRow[5];
}

// 纯静态 Excel 读取类（不继承 MonoBehaviour）
public static class ExcelReader
{
    #region 全局静态设置
    // 全局静态数据，任何脚本直接用 ExcelReader.CharacterData 访问
    public static List<CharacterDataRow> CharacterData = new List<CharacterDataRow>();
    public static List<EventDataRow> eventData=new List<EventDataRow>();

    // 静态构造函数 → 游戏启动自动执行一次
    static ExcelReader()
    {
        ReadCharacterCSV("Character");
        ReadEventCSV("Event");
    }
    #endregion

    private static void ReadCharacterCSV(string fileName)
    {
        // 读取 CSV
        TextAsset csvText = Resources.Load<TextAsset>("Excels/" + fileName);

        if (csvText == null)
        {
            Debug.LogError("找不到CSV文件：" + fileName);
            return;
        }

        // 清空旧数据
        CharacterData.Clear();

        // 按行拆分
        string[] lines = csvText.text.Split('\n');

        
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] cells = line.Split(',');

            // 安全取值
            CharacterDataRow row = new CharacterDataRow();
            row.ChaID = int.Parse(GetCell(cells, 0));
            row.ChaName = GetCell(cells, 1);
            row.ChaDesc = GetCell(cells, 2);
            row.ChaEffect = GetCell(cells, 3);

            CharacterData.Add(row);
        }

        Debug.Log("角色特性CSV读取完成：" + CharacterData.Count + " 条");
    }
    private static void ReadEventCSV(string fileName)
    {
        // 读取 CSV
        TextAsset csvText = Resources.Load<TextAsset>("Excels/" + fileName);

        if (csvText == null)
        {
            Debug.LogError("找不到CSV文件：" + fileName);
            return;
        }

        // 清空旧数据
        eventData.Clear();

        // 按行拆分
        List<string> lines = GetRealCSVLines(csvText.text);


        for (int i = 1; i < lines.Count; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] cells = line.Split(',');

            // 安全取值
            EventDataRow row = new EventDataRow();
            row.eventID = int.Parse(GetCell(cells, 0));
            row.eventType =(E_EventType)System.Enum.Parse(typeof(E_EventType),GetCell(cells,1));
            row.eventName= GetCell(cells, 2);
            row.eventDesc= GetCell(cells, 11);
            row.choiceCount = int.Parse(GetCell(cells, 10));
            for (int j = 0; j <row.choiceCount; j++)
            {
                row.choices[j] = new ChoiceDataRow();
                row.choices[j].choiceName = GetCell(cells, 12+j*4);
                string choiceTypeStr = GetCell(cells, 13 + j * 4).Trim();
                if (!string.IsNullOrWhiteSpace(choiceTypeStr))
                    row.choices[j].choiceType = (E_ChoiceType)System.Enum.Parse(typeof(E_ChoiceType), choiceTypeStr);
                else
                    Debug.Log($"第{i}行 第{j}个选项类型 = [{GetCell(cells,13+j*4)}]"); // 加这行
                row.choices[j].choiceDesc = GetCell(cells, 15+j*4);
            }
                eventData.Add(row);
        }

        Debug.Log("事件CSV读取完成：" + eventData.Count + " 条");
    }


    private static string GetCell(string[] cells, int index)
    {
        return index < cells.Length ? cells[index].Trim() : "";
    }
    private static List<string> GetRealCSVLines(string fullText)
    {
        List<string> lines = new List<string>();
        string currentLine = "";
        bool inQuotes = false;

        foreach (char c in fullText)
        {
            if (c == '"') inQuotes = !inQuotes;

            // 只有【不在引号内】的换行，才是真正的换行
            if (c == '\n' && !inQuotes)
            {
                lines.Add(currentLine);
                currentLine = "";
            }
            else
            {
                currentLine += c;
            }
        }

        if (!string.IsNullOrEmpty(currentLine))
            lines.Add(currentLine);

        return lines;
    }
}