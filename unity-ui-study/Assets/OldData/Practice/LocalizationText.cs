using UnityEngine;
using TMPro;


[RequireComponent(typeof(TMP_Text))] // 유니티 엔진에 선언 되어 있는 어트리뷰트 
[ExecuteInEditMode]
public class LocalizationText : MonoBehaviour
{
    public string stringId;

#if UNITY_EDITOR
    public Languages editorLang;
#endif 
    //!UNITY_EDIIOR //전 처리기 , 디파인?
    //유니티엔진에서 자동으로 처리하는 디파인과 개발자가 직접 적용하는 종류 두개
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {

#if UNITY_EDITOR
        if (!Application.isPlaying) // 에디터 플레이중일때 true
        {
            OnChangedLangauge(editorLang);
            return;
        }
#endif
        Variables.OnLanguageChanged += OnChangedLangauge;
        OnChangedLangauge();
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) // 에디터 플레이중일때 true
        {
            return;
        }
#endif
        Variables.OnLanguageChanged -= OnChangedLangauge;
    }

    public void OnChangedLangauge()
    {
        text.text = DataTableManager.StringTable.Get(stringId);
    }

#if UNITY_EDITOR
    public void OnChangedLangauge(Languages lang)
    {
        var tableId = DataTableIds.StringTableIds[(int)lang];
        var stringTable = DataTableManager.Get<StringTable>(tableId);
        text.text = stringTable.Get(stringId);
    }

    [ContextMenu("Apply Editor Lang To All Active")]
    private void ApplyEditorLangAllActive()
    {
        var all = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);                 //실행중에 사용하면 안됨 씬에있는 모든 오브젝트를 탐색 성능에 안좋음
        foreach (var item in all)
        {
            item.editorLang = editorLang;
            item.OnChangedLangauge(editorLang);
        }
    }
#endif
}
//리소시스 폴더,에디터 폴더는 유니티에 약속된 폴더명