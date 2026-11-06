using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LocalizationText))]
public class LocalizztionTextEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var text = target as LocalizationText;

        var newId = EditorGUILayout.TextField("String ID", text.stringId);
        var newLnag = (Languages)EditorGUILayout.EnumPopup("Language", text.editorLang);

        if (newId != text.stringId || newLnag != text.editorLang)
        {
            text.stringId = newId;
            text.editorLang = newLnag;
            text.OnChangedLangauge(text.editorLang);

            EditorUtility.SetDirty(text);
        }
    }
}
