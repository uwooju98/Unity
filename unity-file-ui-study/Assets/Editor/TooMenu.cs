
using UnityEditor;
using UnityEngine;
using TMPro;

public static class ToolMenu
{
    [MenuItem("Tools/언어/한국어")]
    private static void Korean()
    {
        ChangeLanguage(Languages.Korean);
    }

    [MenuItem("Tools/언어/영어")]
    private static void English()
    {
        ChangeLanguage(Languages.English);
    }

    [MenuItem("Tools/언어/일본어")]
    private static void Japanese()
    {
        ChangeLanguage(Languages.Japanese);
    }


private static void ChangeLanguage(Languages language)
{
    if (Application.isPlaying)
    {
        Variables.Language = language;
        return;
    }

    var texts = Object.FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);

    foreach (LocalizationText text in texts)
    {
        TMP_Text tmp = text.GetComponent<TMP_Text>();

        Undo.RecordObject(text, "Change Language");
        Undo.RecordObject(tmp, "Change Language");

        text.editorLang = language;
        text.OnChangedLanguage(language);

        EditorUtility.SetDirty(text);
        EditorUtility.SetDirty(tmp);
    }
}

}
