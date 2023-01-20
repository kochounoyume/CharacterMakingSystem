using UnityEditor;
using UnityEngine;

public class RenameImages : EditorWindow
{
    private string m_baseName = "image";
    private int m_startIndex = 1;
    public Object[] m_images;

    [MenuItem("Window/Rename Images")]
    public static void ShowWindow()
    {
        GetWindow<RenameImages>();
    }

    private void OnGUI()
    {
        m_baseName = EditorGUILayout.TextField("Base Name", m_baseName);
        m_startIndex = EditorGUILayout.IntField("Start Index", m_startIndex);
        //m_images = EditorGUILayout.ObjectField("Images", m_images, typeof(Object[]), true) as Object[];
        
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty($"{nameof(m_images)}");
        EditorGUILayout.PropertyField(stringsProperty, true);
        so.ApplyModifiedProperties(); 

        if (GUILayout.Button("Rename"))
        {
            Rename();
        }
    }

    private void Rename()
    {
        int index = m_startIndex;
        foreach (Object image in m_images)
        {
            string path = AssetDatabase.GetAssetPath(image);
            string newPath = path.Replace(image.name, m_baseName + index);
            AssetDatabase.MoveAsset(path, newPath);
            index++;
        }
    }
}
