using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;



public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;

    public Vector3 position;
    public Quaternion rotation;
    public Color color;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health} / {position} / {rotation} / {color}";
    }

}

public class JsonTest1 : MonoBehaviour
{
    private void Start()
    {
        var obj = new PlayerState
        {
            playerName = "ABC",
            lives = 10,
            health = 10.999f,
            position = new Vector3(1f, 2f, 3f),
            rotation = new Quaternion(1f, 2f, 3f, 4f),
            color = new Color(255f,255f,255f,255f)
        };

        var path = Path.Combine(Application.persistentDataPath, "test.json");
        Debug.Log(path);
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented, new Vecter3Converter(), new QuaternionConverter(),new ColorConverter());
        File.WriteAllText(path, json);

        var json2 = File.ReadAllText(path);
        var obj2 = JsonConvert.DeserializeObject<PlayerState>(json2, new Vecter3Converter(), new QuaternionConverter(), new ColorConverter());
        Debug.Log(obj2);
    }
}
