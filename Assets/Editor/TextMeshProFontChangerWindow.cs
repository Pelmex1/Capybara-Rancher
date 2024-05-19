using UnityEngine;
using UnityEditor;
using TMPro;

public class TextMeshProFontChangerWindow : EditorWindow
{
    private TMP_FontAsset newFont;
    private float fontSizeMultiplier = 1.0f;

    [MenuItem("Tools/TextMeshPro Font Changer")]
    public static void ShowWindow()
    {
        GetWindow<TextMeshProFontChangerWindow>("TextMeshPro Font Changer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Change TextMeshPro Fonts", EditorStyles.boldLabel);

        newFont = (TMP_FontAsset)EditorGUILayout.ObjectField("New Font Asset", newFont, typeof(TMP_FontAsset), false);
        fontSizeMultiplier = EditorGUILayout.FloatField("Font Size Multiplier", fontSizeMultiplier);

        if (GUILayout.Button("Apply Changes"))
        {
            ChangeFonts();
        }
    }

    private void ChangeFonts()
    {
        if (newFont == null)
        {
            EditorUtility.DisplayDialog("Error", "Please assign a font asset.", "OK");
            return;
        }

        TextMeshProUGUI[] textComponents = FindObjectsOfType<TextMeshProUGUI>();

        foreach (TextMeshProUGUI textComponent in textComponents)
        {
            Undo.RecordObject(textComponent, "Change Font and Size");
            textComponent.font = newFont;
            textComponent.fontSize *= fontSizeMultiplier;
            EditorUtility.SetDirty(textComponent);
        }

        Debug.Log($"Changed font and size for {textComponents.Length} TextMeshProUGUI components.");
    }
}
