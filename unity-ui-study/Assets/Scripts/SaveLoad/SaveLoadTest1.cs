using UnityEngine;

public class SaveLoadTest1 : MonoBehaviour
{
public void Save()
    {
        SaveLoadManager.Data = new SaveDataV4();
        SaveLoadManager.Data.firstName = "You";
        SaveLoadManager.Data.lastName = "WooJu";
        SaveLoadManager.Data.Gold = Random.Range(100, 200);
        Debug.Log(SaveLoadManager.Save());
    }


    public void Load()
    {
        Debug.Log(SaveLoadManager.Load());
        Debug.Log(SaveLoadManager.Data.firstName);
        Debug.Log(SaveLoadManager.Data.lastName);
    }

}
