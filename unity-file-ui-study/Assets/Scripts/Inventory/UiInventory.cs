using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;

public class UiInventory : MonoBehaviour
{
    public readonly string[] SortingOptionNames =
    {
        "SORT_DATE_ASC",
        "SORT_DATE_DESC",
        "SORT_NAME_ASC",
        "SORT_NAME_DESC",
    };

    public readonly string[] FilteringOptionNames =
    {
        "FILTER_ALL",
        "FILTER_WEAPON",
        "FILTER_EQUIP",
        "FILTER_CONSUMABLE",
    };
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiItemSlotList uiItemSlotList;



    private void OnEnable()
    {
        UpdateDropDownOptions();

        OnLoad();
        OnChangeSorting(sorting.value);
        OnChangeFiltering(filtering.value);

        Variables.OnLanguageChanged += UpdateDropDownOptions;
    }

    private void OnDisable()
    {
        Variables.OnLanguageChanged -= UpdateDropDownOptions;
        OnSave();
    }

    public void UpdateDropDownOptions()
    {
        var stringTalbe = DataTableManager.StringTable;
        sorting.ClearOptions();
        var options = new List<string>();
        foreach (var id in SortingOptionNames)
        {
            options.Add(stringTalbe.Get(id));
        }
        sorting.AddOptions(options);
        options.Clear();
        filtering.ClearOptions();
        options = new List<string>();
        foreach (var id in FilteringOptionNames)
        {
            options.Add(stringTalbe.Get(id));
        }
        filtering.AddOptions(options);
    }


    public void OnChangeSorting(int index)
    {
        uiItemSlotList.Sorting = (UiItemSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiItemSlotList.Filtering = (UiItemSlotList.FitteringOptions)index;
    }
    public void OnSave()
    {
        //SaveLoadManager.Data.ItemList = uiItemSlotList.GetSaveItemDataList();
        //SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        //SaveLoadManager.Load();
        //uiItemSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
    }

    public void OnCreateItem()
    {
        uiItemSlotList.AddRandomItem();
    }

    public void OnRemoveItem()
    {
        uiItemSlotList.RemoveItem();
    }

}
