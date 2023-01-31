using System;
using CharacterMakingSystem.CoreSystem;
using CharacterMakingSystem.Data;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Profiling;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject] private ChaPartDatabase database;
    [Inject] private IAssetLoader assetLoader;
    [SerializeField] private GameObject obj = null;
    [SerializeField] private Gender _gender;
    [SerializeField] private int _faceID ;
    //private TextMeshProUGUI systemText = null;

    private async UniTaskVoid Start()
    {
        //systemText=FindObjectOfType<TextMeshProUGUI>();
        
        var data = database.FindFaceData(_gender, _faceID);
        obj = await assetLoader.LoadAsync<GameObject>(data.address);
        // assetLoader.LoadAsync<GameObject>(data.address).ToObservable().Subscribe(load =>
        // {
        //     obj = load;
        // });
    }

    private void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        float mem = (Profiler.GetTotalAllocatedMemoryLong() >> 10) / 1024f;
        float unused = (Profiler.GetTotalUnusedReservedMemoryLong() >> 10) / 1024f;

        //systemText.text = fps.ToString("0.00") + " FPS\nMemory: " + mem + " / " + unused;
    }

    private GameObject FindObjData(GameObject target,GameObject[] gameObjects)
    {
        return Array.Find(gameObjects, data => data.name == target.name);
    }
}
