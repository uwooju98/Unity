using UnityEngine;

public class CsvData
{
    public string Id { get; set; }
    public string String { get; set; }
}

public class CSVTest1 : MonoBehaviour
{
    public UiItemButton[] itemButtons;
    public UiItemInfo itemInfo;

    private void Start()
    {
        itemButtons[0].SetItem("ITEM_1");
        itemButtons[0].button.onClick.AddListener(() => itemInfo.SetItem("ITEM_1"));
        itemButtons[1].SetItem("ITEM_2");
        itemButtons[1].button.onClick.AddListener(() => itemInfo.SetItem("ITEM_2"));
        itemButtons[2].SetItem("ITEM_3");
        itemButtons[2].button.onClick.AddListener(() => itemInfo.SetItem("ITEM_3"));
        itemButtons[3].SetItem("ITEM_4");
        itemButtons[3].button.onClick.AddListener(() => itemInfo.SetItem("ITEM_4"));
    }

    // private void Start()
    // {
    //     Debug.Log(DataTableManager.StringTable.Get("HELLO"));
    //     Debug.Log(DataTableManager.StringTable.Get("BYE"));
    //     Debug.Log(DataTableManager.StringTable.Get("DIE"));

    //     var itemData = DataTableManager.ItemTable.Get("ITEM_1");
    //     Debug.Log($"{itemData.StringName}, {itemData.StringDesc}");
    // }

    // private void Start()
    // {
    //     TextAsset csv = Resources.Load<TextAsset>("DataTables/StringTableKr");
    //     using (var reader = new StringReader(csv.text))
    //     using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
    //     {
    //         var records = csvReader.GetRecords<CsvData>();
    //         foreach (CsvData record in records)
    //         {
    //             Debug.Log($"{record.Id} : {record.String}");
    //         }
    //     }
    // }
}
