using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Practice3 : MonoBehaviour
{
    private string folder;
    private string path;

    private Dictionary<string, string> settings = new Dictionary<string, string>();

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        folder = Path.Combine(Application.persistentDataPath, "Settings");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
        path = Path.Combine(folder, "settings.cfg");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Load();
            PrintAll();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Modify();
            Save();
            PrintAll();
        }
    }

    private void Load()
    {
        settings.Clear();
        using (var reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    settings[parts[0]] = parts[1];
                }
            }
            Debug.Log($"설정 로드 완료 (항목 {settings.Count})");
        }
    }

    private void Modify()
    {
        settings["bgm_volume"] = "50";
        settings["language"] = "en";

        Debug.Log("설정 수정 완료");
    }

    private void Save()
    {
        using (var writer = new StreamWriter(path))
        {
            foreach (var pair in settings)
            {
                writer.WriteLine($"{pair.Key}={pair.Value}");
            }
        }
        Debug.Log("설정 저장 완료");
    }

    private void PrintAll()
    {
        Debug.Log($"-- 설정 전체 ({settings.Count}개) --");
        foreach (var pair in settings)
        {
            Debug.Log($"{pair.Key} = {pair.Value}");
        }
    }
}
