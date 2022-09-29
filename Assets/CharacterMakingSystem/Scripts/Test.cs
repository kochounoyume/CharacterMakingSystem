using System;
using CharacterMakingSystem.CoreSystem;
using CharacterMakingSystem.Data;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject] private ChaPartDatabase database;
    private IAssetLoader assetLoader;
    [SerializeField] private GameObject obj = null;

    [Inject]
    private void Inject(IAssetLoader assetLoader)
    {
        this.assetLoader = assetLoader;
    }

    private void Start()
    {
        var data = database.FindFaceData(Gender.Male, 6);
        assetLoader.LoadAsync<GameObject>(data.address).ToObservable().Subscribe(load =>
        {
            obj = load;
        });
    }

    private GameObject FindObjData(GameObject target,GameObject[] gameObjects)
    {
        return Array.Find(gameObjects, data => data.name == target.name);
    }
}
