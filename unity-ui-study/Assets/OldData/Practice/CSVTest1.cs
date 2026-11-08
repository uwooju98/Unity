using UnityEngine;


public class CsvData
{
    public string Id { get; set; }
    public string String { get; set; }
}

public class CSVTest1 : MonoBehaviour
{
    public UiItemButton[] itemButton;
    public UiItemInfo itemInfo;

    private void Start()
    {
        itemButton[0].SetItem("ITEM_1");
        itemButton[0].button.onClick.AddListener(() => itemInfo.SetItem("ITEM_1"));
        itemButton[1].SetItem("ITEM_2");
        itemButton[1].button.onClick.AddListener(() => itemInfo.SetItem("ITEM_2"));
        itemButton[2].SetItem("ITEM_3");
        itemButton[2].button.onClick.AddListener(() => itemInfo.SetItem("ITEM_3"));
        itemButton[3].SetItem("ITEM_4");
        itemButton[3].button.onClick.AddListener(() => itemInfo.SetItem("ITEM_4"));

    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        itemButton[].SetEmpty();
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        itemButton.SetItem("ITEM_1");
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        itemButton.SetItem("ITEM_2");
    //    }
    //}

    //private void Start()
    //{
    //    stringTable = DataTableManager.StringTable;

    //    Debug.Log(stringTable.Get("Hellow"));
    //    Debug.Log(stringTable.Get("NoHellow"));
    //    Debug.Log(stringTable.Get("Hi"));

    //    var itemData = DataTableManager.ItemTable.Get("ITEM_1");
    //    Debug.Log($"{itemData.StringName},{itemData.StringDesc}");
    //}





    //public TextAsset textAsset;
    //private void Start()
    //{
    //    //Debug.Log(textAsset.text);

    //    TextAsset csv = Resources.Load<TextAsset>("DataTables/StringTableKR");
    //    //TextAsset csv = Resources.UnLoad<TextAsset>("DataTables/StringTableKR");
    //    //Debug.Log(csv.text);

    //    using (var reader = new StringReader(csv.text))
    //    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
    //    {
    //        var records = csvReader.GetRecords<CsvData>();

    //        foreach (var record in records)
    //            Debug.Log($"{record.Id} : {record.String}");

    //    }
    //}
}
