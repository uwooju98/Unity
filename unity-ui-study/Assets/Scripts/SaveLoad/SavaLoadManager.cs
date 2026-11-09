using UnityEngine;
using Newtonsoft.Json;
using SaveDataVC = SaveDataV4;
using System.IO;

public static class SaveLoadManager
{
    public static readonly string[] SaveFileNames =
    {
        "SaveAuto.json",
        "Save1.json",
        "Save2.json",
        "Save3.json",
    };

    public static string SaveDirectory => Path.Combine(Application.persistentDataPath, "Save");

    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All
    };
    public static int SaveDataVersion { get; } = 4;
    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    public static bool Save(int slot = 0)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length)
            return false;

        try
        {
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);

            var path = Path.Combine(SaveDirectory, SaveFileNames[slot]);
            var json = JsonConvert.SerializeObject(Data, settings);
            File.WriteAllText(path, json);
            return true;
        }
        catch
        {
            Debug.LogError("Save 예외 발생");
            return false;
        }
    }

    public static bool Load(int slot = 0)
    {
        if (slot < 0 || slot >= SaveFileNames.Length)
            return false;

        var path = Path.Combine(SaveDirectory, SaveFileNames[slot]);
        Debug.Log(path);
        if (!File.Exists(path))
            return false;

        try
        {
            var json = File.ReadAllText(path);
            var dataSave = JsonConvert.DeserializeObject<SaveData>(json, settings);
            while (dataSave.Version < SaveDataVersion)
            {
                var pervVersion = dataSave.Version;
                dataSave = dataSave.VersionUp();
                Debug.Log($"[SaveLoad] 마이그레이션 v{pervVersion} -> v{dataSave.Version}");
            }
            Data = dataSave as SaveDataVC;
            return true;
        }
        catch
        {
            Debug.LogError("Load 예외 발생");
            return false;
        }
    }
}
