using System;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;
}

public class JsonUtilityTest : MonoBehaviour
{
    private void Start()
    {
        var obj = new PlayerInfo
        {
            playerName = "ABC",
            lives = 10,
            health = 10.999f,
            position = new Vector3(1f, 2f, 3f),
        };

        var path = Path.Combine(Application.persistentDataPath, "player.json");
        var json = JsonUtility.ToJson(obj, prettyPrint: true);
        File.WriteAllText(path, json);
        Debug.Log($"저장 경로: {path}");

        string json2 = File.ReadAllText(path);
        PlayerInfo loaded = new PlayerInfo();
        JsonUtility.FromJsonOverwrite(json2, loaded);
        Debug.Log($"{loaded.playerName} / {loaded.lives} / {loaded.health} / {loaded.position}");
    }
}
