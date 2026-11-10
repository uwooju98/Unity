using UnityEngine;

public class SaveLoadTest1 : MonoBehaviour
{

    public void CreateItem()
    {
        SaveLoadManager.Data.ItemList.Add(SaveItemData.GetRandomItem());
    }
    public void Save()
    {
        Debug.Log(SaveLoadManager.Save());
    }


    public void Load()
    {
        Debug.Log(SaveLoadManager.Load());

        foreach (var data in SaveLoadManager.Data.ItemList)
        {
            Debug.Log(data);
        }
    }

}
