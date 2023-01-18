using UnityEngine;
using UnityEditor;
using System.IO;

public class PrefabPreviewExporter : EditorWindow
{
    private GameObject prefab;
    private string savePath;

    [MenuItem("Window/Prefab Preview Exporter")]
    public static void ShowWindow()
    {
        GetWindow<PrefabPreviewExporter>("Prefab Preview Exporter");
    }

    private void OnGUI()
    {
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        savePath = EditorGUILayout.TextField("Save Path", savePath);

        if (GUILayout.Button("Export"))
        {
            if (prefab == null)
            {
                Debug.LogError("Please select a prefab.");
                return;
            }

            if (string.IsNullOrEmpty(savePath))
            {
                Debug.LogError("Please enter a save path.");
                return;
            }

            string path = AssetDatabase.GetAssetPath(prefab);
            Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
            Texture2D preview = AssetPreview.GetAssetPreview(obj);
            byte[] pngData = preview.EncodeToPNG();
            File.WriteAllBytes($"{savePath}/{prefab.name}.png", pngData);
            AssetDatabase.Refresh();

            Debug.Log("Prefab preview exported to " + savePath);
        }
    }
}