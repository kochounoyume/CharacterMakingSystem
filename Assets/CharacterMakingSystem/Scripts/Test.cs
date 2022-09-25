using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    
    private void Start()
    {
        
        
        //thi.ResetThirdPersonControllerArmature();
    }

    private GameObject FindObjData(GameObject target,GameObject[] gameObjects)
    {
        return Array.Find(gameObjects, data => data.name == target.name);
    }
}
