using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        bool changed = DrawDefaultInspector();

        if (changed && Application.isPlaying)
        {
            UIManager uiManager = target as UIManager;
            uiManager.ApplyInspectorLanguage();
        }
    }
}