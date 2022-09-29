using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorScripting : MonoBehaviour
{
    private const string exclusionPath = "Assets/CharacterMakingSystem/Resources/";
    private const string extensionPath = ".prefab";

    [MenuItem("Assets/クリップボードにコピー")]
    private static void FilePathGetter()
    {
        int instanceID = Selection.activeInstanceID;
        string path = AssetDatabase.GetAssetPath(instanceID);

        // copy clipboard
        GUIUtility.systemCopyBuffer = path.Replace(exclusionPath,"").Replace(extensionPath,"");
    }
}
