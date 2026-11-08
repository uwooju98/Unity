using System.Collections.Generic;
using UnityEngine;

public class StringTable : DataTable
{
    public class Data
    {
        public string Id { get; set; }
        public string String { get; set; }
    }


    private readonly Dictionary<string, string> dictionary = new Dictionary<string, string>();


    public override void Load (string filename) 
    {
        dictionary.Clear();

        string path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<Data> list = LoadCSV<Data>(textAsset.text);

        foreach (var item in list)
        {
            if (!dictionary.ContainsKey(item.Id)) 
                dictionary.Add(item.Id, item.String);
            else 
                Debug.Log($"키 중복: {item.Id}");
        }
    }

    public static readonly string Unknown = "키 없음";
    public string Get(string id)
    {
        if (!dictionary.ContainsKey(id))
            return Unknown;

        return dictionary[id];
    }
}