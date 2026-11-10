using System.Linq.Expressions;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
[ExecuteInEditMode]
public class LocalizationText : MonoBehaviour
{
    public string stringId;

#if UNITY_EDITOR
    public Languages editorLang;
#endif

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            OnChangedLanguage(editorLang);
            return;
        }
#endif

        Variables.OnLanguageChanged += OnChangedLanguage;
        OnChangedLanguage();
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif

        Variables.OnLanguageChanged -= OnChangedLanguage;
    }

    public void OnChangedLanguage()
    {
        text.text = DataTableManager.StringTable.Get(stringId);
    }

#if UNITY_EDITOR
    public void OnChangedLanguage(Languages lang)
    {
        var tableId = DataTableIds.StringTableIds[(int)lang];
        var stringTable = DataTableManager.Get<StringTable>(tableId);
        text.text = stringTable.Get(stringId);
    }

    [ContextMenu("Apply Editor Lang To All Active")]
    private void ApplyEditorLangToAllActive()
    {
        var all = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);
        foreach (var item in all)
        {
            item.editorLang = editorLang;
            item.OnChangedLanguage(editorLang);
        }
    }
#endif
}
