using System.Collections.Generic;
using UnityEngine;

public class LocalizationTable : DataTable
{
    private readonly Dictionary<string, string> localizedStrings =
    new Dictionary<string, string>();


    public string Get(string id)
    {
        if (localizedStrings.TryGetValue(id, out string data))
            return data;

        return null;
    }

    public override void Load(string filename)
    {
        string path = $"DataTables/{filename}";

        Debug.Log($"CSV 로드 경로: {path}");

        List<LocalizationData> datas = CsvLoader.ReadCsv<LocalizationData>(path);

        if (datas == null)
            return;

        localizedStrings.Clear();

        foreach (var data in datas)
        {
            if (localizedStrings.ContainsKey(data.Id))
                continue;

            localizedStrings.Add(data.Id, data.String);
        }
    }
}
