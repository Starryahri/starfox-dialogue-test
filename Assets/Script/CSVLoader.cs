using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVLoader
{
    public static List<Dictionary<string, string>> Load(string filename)
    {
        string filePath = Path.Combine(Application.dataPath, filename);
        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found: {filePath}");
            return data;
        }

        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length < 2)
        {
            Debug.LogError("CSV file must have at least one header row and one data row.");
            return data;
        }
            string[] headers = lines[0].Split(',');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            Dictionary<string, string> rowData = new Dictionary<string, string>();

            for (int j = 0; j < headers.Length && j < values.Length; j++)
            {
                rowData.Add(headers[j], values[j]);
            }

            data.Add(rowData);
        }

        return data;
    }
}
