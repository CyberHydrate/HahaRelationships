using UnityEngine;
using System.Collections.Generic;

// 数据结构
public class CharacterDataRow
{
    public string ChaID;
    public string ChaName;
    public string ChaDesc;
    public string ChaEffect;
}

// 纯静态 Excel 读取类（不继承 MonoBehaviour）
public static class ExcelReader
{
    // 全局静态数据，任何脚本直接用 ExcelReader.CharacterData 访问
    public static List<CharacterDataRow> CharacterData = new List<CharacterDataRow>();

    // 静态构造函数 → 游戏启动自动执行一次
    static ExcelReader()
    {
        ReadCSV("CharacteristicData");
    }

    private static void ReadCSV(string fileName)
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

        // 自动读取所有行（不写死101）
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] cells = line.Split(',');

            // 安全取值
            CharacterDataRow row = new CharacterDataRow();
            row.ChaID = GetCell(cells, 0);
            row.ChaName = GetCell(cells, 1);
            row.ChaDesc = GetCell(cells, 2);
            row.ChaEffect = GetCell(cells, 3);

            CharacterData.Add(row);
        }

        Debug.Log("角色特性CSV读取完成：" + CharacterData.Count + " 条");
    }

    // 安全取单元格，避免越界
    private static string GetCell(string[] cells, int index)
    {
        return index < cells.Length ? cells[index].Trim() : "";
    }
}