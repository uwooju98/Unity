using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonTest2SaveData
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Color color;
}

public class JsonTest2 : MonoBehaviour
{
    public GameObject[] prefabs;
    private List<GameObject> objects = new List<GameObject>();
    private JsonSerializerSettings jsonSettings;

    private void Awake()
    {
        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.Converters.Add(new Vectro3Converter());
        jsonSettings.Converters.Add(new QuaternionConverter());
        jsonSettings.Converters.Add(new ColorConverter());
    }

    public void Create()
    {
        int randomIndex = Random.Range(0, prefabs.Length);
        Vector3 randomPosition = Random.insideUnitSphere * 10f;
        Quaternion randomRotation = Random.rotation;
        Vector3 randomScale = Vector3.one * Random.Range(0.5f, 2f);
        Color color = Random.ColorHSV();

        GameObject newGo = Instantiate(prefabs[randomIndex]);
        newGo.transform.position = randomPosition;
        newGo.transform.rotation = randomRotation;
        newGo.transform.localScale = randomScale;
        newGo.GetComponent<Renderer>().material.color = color;

        objects.Add(newGo);
    }

    public void Clear()
    {
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
        objects.Clear();
    }

    public void Save()
    {
        List<JsonTest2SaveData> saveDatas = new List<JsonTest2SaveData>();
        foreach (GameObject obj in objects)
        {
            JsonTest2SaveData data = new JsonTest2SaveData();
            data.name = obj.name;
            data.position = obj.transform.position;
            data.rotation = obj.transform.rotation;
            data.scale = obj.transform.localScale;
            data.color = obj.GetComponent<Renderer>().material.color;
            saveDatas.Add(data);
        }
        string json = JsonConvert.SerializeObject(saveDatas, jsonSettings);
        string path = Path.Combine(Application.persistentDataPath, "JsonTest2.json");
        Debug.Log(path);
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        Clear();

        string path = Path.Combine(Application.persistentDataPath, "JsonTest2.json");
        string json = File.ReadAllText(path);

        var saveDatas = JsonConvert.DeserializeObject<List<JsonTest2SaveData>>(json, jsonSettings);

        foreach (var data in saveDatas)
        {
            int prefabIndex = -1;
            for (int i = 0; i < prefabs.Length; ++i)
            {
                if (data.name.Contains(prefabs[i].name))
                {
                    prefabIndex = i;
                    break;
                }
            }

            var newGo = Instantiate(prefabs[prefabIndex]);
            newGo.transform.position = data.position;
            newGo.transform.rotation = data.rotation;
            newGo.transform.localScale = data.scale;
            newGo.GetComponent<Renderer>().material.color = data.color;
            objects.Add(newGo);
        }
    }
}
