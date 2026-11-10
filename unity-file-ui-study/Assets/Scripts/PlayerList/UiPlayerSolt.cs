using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiPlayerSolt : MonoBehaviour
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
    "CHAR_1_NAME",
    "CHAR_2_NAME",
    "CHAR_3_NAME",
    "CHAR_4_NAME",
    };

    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiPlayerSoltList uiPlayerSoltList;

    private void OnEnable()
    {
        UpdateDropDownOptions();

        OnChangeSorting(sorting.value);
        OnChangeFiltering(filtering.value);

        Variables.OnLanguageChanged += UpdateDropDownOptions;
    }

    private void OnDisable()
    {
        Variables.OnLanguageChanged -= UpdateDropDownOptions;
    }

    public void UpdateDropDownOptions()
    {
        var stringTable = DataTableManager.StringTable;

        int sortingIndex = sorting.value;
        int filteringIndex = filtering.value;

        var options = new List<string>();

        foreach (var id in SortingOptionNames)
        {
            options.Add(stringTable.Get(id));
        }

        sorting.ClearOptions();
        sorting.AddOptions(options);
        sorting.SetValueWithoutNotify(Mathf.Clamp(sortingIndex, 0, options.Count - 1));
        sorting.RefreshShownValue();

        options.Clear();

        foreach (var id in FilteringOptionNames)
        {
            options.Add(stringTable.Get(id));
        }

        filtering.ClearOptions();
        filtering.AddOptions(options);
        filtering.SetValueWithoutNotify(Mathf.Clamp(filteringIndex, 0, options.Count - 1));
        filtering.RefreshShownValue();
    }

    public void OnCreatePlayer()
    {
        if (uiPlayerSoltList == null)
        {
            return;
        }

        uiPlayerSoltList.AddRandomPlayer();
    }

    public void OnRemovePlayer()
    {
        if (uiPlayerSoltList == null)
        {
            return;
        }

        uiPlayerSoltList.RemoveSelectedPlayer();
    }

    public void OnChangeSorting(int index)
    {
        uiPlayerSoltList.Sorting = (UiPlayerSoltList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiPlayerSoltList.Filtering = (UiPlayerSoltList.FilteringOptions)index;
    }
}