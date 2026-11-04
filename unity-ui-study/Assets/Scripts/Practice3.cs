using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class Practice3 : MonoBehaviour
{
    private string folder;
    private string path;


    private Dictionary<string, string> setting = new Dictionary<string, string>();


    private void Start()
    {
        folder = Path.Combine(Application.persistentDataPath, "Settings");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

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
        setting.Clear();
        using (var reader = new StreamReader(path))
        {
            string line;
            while((line = reader.ReadLine()) != null)
            {
                var parts = line.Split('=');

                if (parts.Length == 2)
                    setting[parts[0]] = parts[1];
            }
            Debug.Log($"설정 로드 완료 (항목 {setting.Count})");
        }
    }

    private void Modify()
    {
        setting["bgm_volume"] = "50";
        setting["language"] = "en";

        Debug.Log("수정 완료");
    }

    private void Save()
    {
        using (var writer = new StreamWriter(path))
        {
            foreach (var pair in setting)
                writer.WriteLine($"{pair.Key}={pair.Value}");
        }
        Debug.Log("설정 저장 완료");
    }


    private void PrintAll()
    {
        Debug.Log($"-- 설정 전체 ({setting.Count}개) --");

        foreach (var pair in setting)
            Debug.Log($"{pair.Key} = {pair.Value}");
    }
}
