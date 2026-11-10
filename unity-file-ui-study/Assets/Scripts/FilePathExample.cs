using UnityEngine;

public class FilePathExample : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"persistentDataPath: {Application.persistentDataPath}");
        Debug.Log($"dataPath: {Application.dataPath}");
        Debug.Log($"streamingAssetsPath: {Application.streamingAssetsPath}");
    }
}

/*
persistentDataPath: C:/Users/rockd/AppData/LocalLow/DefaultCompany/unity-file-ui-study
dataPath: C:/Users/rockd/repos/lecture-live/unity-file-ui-study/Assets
streamingAssetsPath: C:/Users/rockd/repos/lecture-live/unity-file-ui-study/Assets/StreamingAssets
*/