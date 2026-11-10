
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class PlrSaveLoadManager
{
    private static readonly string path =
        Path.Combine(Application.persistentDataPath, "PlayerList.json");

    public static void Save(List<SavePlrData> players)
    {
        var saveData = new PlrSaveDataV1
        {
            Players = players
        };

        try
        {
            string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            File.WriteAllText(path, json);
        }
        catch (Exception e)
        {
            Debug.LogError($"플레이어 저장 실패: {e.Message}");
        }
    }

    public static List<SavePlrData> Load()
    {
        if (!File.Exists(path))
        {
            return new List<SavePlrData>();
        }

        try
        {
            string json = File.ReadAllText(path);

            PlrSaveDataV1 saveData =
                JsonConvert.DeserializeObject<PlrSaveDataV1>(json);

            return saveData?.Players ?? new List<SavePlrData>();
        }
        catch (Exception e)
        {
            Debug.LogError($"플레이어 불러오기 실패: {e.Message}");
            return new List<SavePlrData>();
        }
    }
}
