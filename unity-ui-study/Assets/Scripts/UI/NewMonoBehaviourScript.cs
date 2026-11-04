using UnityEngine;

public class FilePathExample : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"persistentDataPath: {Application.persistentDataPath}");
        Debug.Log($"dataPath: {Application.dataPath}");
        Debug.Log($"streamingAssetsPath: {Application.streamingAssetsPath}");
    }
}