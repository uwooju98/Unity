
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;
//using Random = UnityEngine.Random;

//[Serializable]
//public class SaveData
//{
//    public string name;
//    public Vector3 position;
//    public Quaternion rotation;
//    public Vector3 scale;

//    public Color color;
//}

//public class JsonObjManager : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject[] objs;

//    private JsonSerializerSettings jsonSettings;


//    private List<GameObject> Objs = new List<GameObject>();

//    private void Awake()
//    {
//        jsonSettings = new JsonSerializerSettings();
//        jsonSettings.Formatting = Formatting.Indented;
//        jsonSettings.Converters.Add(new Vecter3Converter());
//        jsonSettings.Converters.Add(new QuaternionConverter());
//        jsonSettings.Converters.Add(new ColorConverter());

//    }

//    private void OnEnable()
//    {
//        //JsonButtonManager.OnSaved += Save;
//        JsonButtonManager.OnLoaded += Load;
//        JsonButtonManager.OnCreated += Craete;
//        JsonButtonManager.OnClreaed += Clear;
//    }

//    private void OnDisable()
//    {
//        //JsonButtonManager.OnSaved -= Save;
//        JsonButtonManager.OnLoaded -= Load;
//        JsonButtonManager.OnCreated -= Craete;
//        JsonButtonManager.OnClreaed -= Clear;
//    }

//    //private void Save()
//    //{
//    //    var path = Path.Combine(Application.persistentDataPath, "Objs.json");

//    //    List<SaveData> saveDatas = new List<SaveData>();

//    //    foreach (GameObject obj in Objs)
//    //    {
//    //        if (obj == null)
//    //        {
//    //            continue;
//    //        }

//    //        Renderer objRenderer = obj.GetComponent<Renderer>();

//    //        //SaveData data = new SaveData
//    //        //{
//    //        //    name = obj.name,
//    //        //    position = obj.transform.position,
//    //        //    rotation = obj.transform.rotation,
//    //        //    scale = obj.transform.localScale,
//    //        //    color = objRenderer.material.color
//    //        //};

//    //        saveDatas.Add(data);
//    //    }

//    //    string json = JsonConvert.SerializeObject(saveDatas, Formatting.Indented, jsonSettings);

//    //    File.WriteAllText(path, json);
//    //    Debug.Log($"저장 경로: {path}");
//    //}

//    private void Load()
//    {
//        Clear();

//        var path = Path.Combine(Application.persistentDataPath, "Objs.json");
//        string json = File.ReadAllText(path);

//        List<SaveData> saveDatas = JsonConvert.DeserializeObject<List<SaveData>>(json, jsonSettings);


//        foreach (SaveData data in saveDatas)
//        {
//            int prefabIndex = -1;

//            for (int i = 0; i < objs.Length; i++)
//            {
//                if (data.name == objs[i].name || data.name == objs[i].name + "(Clone)")
//                {
//                    prefabIndex = i;
//                    break;
//                }
//            }

//            GameObject newObj = Instantiate(objs[prefabIndex]);
//            Renderer objRenderer = newObj.GetComponent<Renderer>();
//            newObj.transform.position = data.position;
//            newObj.transform.rotation = data.rotation;
//            newObj.transform.localScale = data.scale;
//            objRenderer.material.color = data.color;
//            Objs.Add(newObj);
//        }
//    }

//    private void Craete()
//    {
//        int index = Random.Range(0, objs.Length);

//        GameObject newObj = Instantiate(objs[index]);
//        Renderer objRenderer = newObj.GetComponent<Renderer>();

//        newObj.transform.position = new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), Random.Range(-50f, 50f));
//        newObj.transform.rotation = Random.rotation;
//        newObj.transform.localScale = new Vector3(Random.Range(0f, 20f), Random.Range(0f, 20f), Random.Range(0f, 20f));
//        objRenderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
//        Objs.Add(newObj);
//    }

//    private void Clear()
//    {
//        for (int i = Objs.Count - 1; i >= 0; i--)
//        {
//            if (Objs[i] != null)
//            {
//                Destroy(Objs[i]);
//            }
//        }

//        Objs.Clear();
//    }
//}
