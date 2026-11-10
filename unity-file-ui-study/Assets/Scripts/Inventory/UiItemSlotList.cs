using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class UiItemSlotList : MonoBehaviour
{
    public enum SortingOptions
    {
        CreationTimeAsscending,
        CreationTimeDesscending,

        NameAsscending,
        NameDesscending,


    }

    public enum FitteringOptions
    {
        None,
        Weapon,
        Equip,
        Consumable
    }

    public readonly System.Comparison<SaveItemData>[] comparisons =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),
        (lhs, rhs) => lhs.ItemData.StringName.CompareTo(rhs.ItemData.StringName),
        (lhs, rhs) => rhs.ItemData.StringName.CompareTo(lhs.ItemData.StringName),
    };

    public readonly System.Predicate<SaveItemData>[] filerings =
    {
        (x) => true,
        (x) => x.ItemData.Type == ItemTypes.Weapon,
        (x) => x.ItemData.Type == ItemTypes.Equip,
        (x) => x.ItemData.Type == ItemTypes.Consumable
    };

    private SortingOptions sorting = SortingOptions.CreationTimeAsscending;
    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlot();
            }
        }
    }

    private FitteringOptions filtering = FitteringOptions.None;
    public FitteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlot();
            }
        }
    }

    private int selectedSlot = -1;
    public UnityEvent OnUpdateSlots;
    public UnityEvent<SaveItemData> OnSelectSlot;

    public UiItemSlot prefab;

    public ScrollRect scrollRect;

    private List<UiItemSlot> uiSlotlist = new List<UiItemSlot>();

    private List<SaveItemData> saveItemDataList = new List<SaveItemData>();

    public void SetSaveItemDataList(List<SaveItemData> source)
    {
        saveItemDataList = source == null ? new List<SaveItemData>() : new List<SaveItemData>(source);
        UpdateSlot();
    }

    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }

    private void Start()
    {
        OnSelectSlot.AddListener((x) => Debug.Log(x));

    }

    private void OnEnable()
    {
        Variables.OnLanguageChanged += UpdateSlot;
    }

    private void OnDisable()
    {
        Variables.OnLanguageChanged -= UpdateSlot;
    }

    private void UpdateSlot()
    {
        var list = saveItemDataList.FindAll(filerings[(int)filtering]);
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotlist.Count < list.Count)
        {
            for (int i = uiSlotlist.Count; i < list.Count; ++i)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.SlotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlot = newSlot.SlotIndex;
                    if (newSlot.ItemData != null)
                    {
                        OnSelectSlot.Invoke(newSlot.ItemData);
                    }
                });

                uiSlotlist.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotlist.Count; ++i)
        {
            if (i < list.Count)
            {
                uiSlotlist[i].gameObject.SetActive(true);
                uiSlotlist[i].SetItem(list[i]);
            }
            else
            {
                uiSlotlist[i].gameObject.SetActive(false);
                uiSlotlist[i].SetEmpty();
            }
        }

        selectedSlot = -1;
        OnUpdateSlots.Invoke();
    }


    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlot();
    }

    public void RemoveItem()
    {
        if (selectedSlot == -1)
            return;

        saveItemDataList.Remove(uiSlotlist[selectedSlot].ItemData);
        UpdateSlot();
    }
}
