using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    
    [MenuItem("Assets/スクショ", false)]
    public static void CaptureScreenshot()
    {
        string productName = Application.productName;
        if (string.IsNullOrEmpty(productName))
        {
            productName = "Unity";
        }
        string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), productName);
        DateTime now = DateTime.Now;
        string fileName = string.Format("{0}_{1}x{2}_{3}{4:D2}{5:D2}{6:D2}{7:D2}{8:D2}.png", productName, Screen.width, Screen.height, now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
        string path = Path.Combine(directory, fileName);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        ScreenCapture.CaptureScreenshot(path);
        Debug.LogFormat("Screenshot Save : {0}", path);
    }
}
