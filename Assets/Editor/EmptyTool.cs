using UnityEditor;
using UnityEngine;

public class LevelDesignTool : EditorWindow
{
    [MenuItem("Tools/My First Tool")]
    public static void ShowWindow()
    {
        GetWindow<LevelDesignTool>("My First Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Hello Tool World!", EditorStyles.boldLabel);
    }
}
