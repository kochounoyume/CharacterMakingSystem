using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CharacterMakingSystem.CoreSystem
{
    /// <summary>
    /// 要求されたアセットをロードして、メモリのキャッシュに貯蓄し、Disposeされるまで保持する
    /// </summary>
    public class AssetLoader : IAssetLoder
    {
        // ロードされたアセットを保持持続するキャッシュリスト
        private List<AssetReference> casheAssets = null;
        
        /// <summary>
        /// 同時に同じものをロードしないように交通整理する
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="TObject"></typeparam>
        /// <returns></returns>
        public async UniTask<TObject> LoadAsync<TObject>(string key) where TObject : UnityEngine.Object
        {
            // 指定されたアセットがキャッシュにあったらそれを返す
            // if (casheAssets.TryGetValue(key, out var cachedObject))
            // {
            //     // 
            // }
            
            try
            {
                // キャッシュになかったのでロードする
                var handle = await Addressables.LoadAssetAsync<TObject>(key);
                
                // 同時に同じアセットの読み込みが走ること回避で、ハンドルをキャッシュに入れる
                //casheAssets.Add(key, handle);

                return handle;
            }
            catch (Exception e)
            {
                return null;
            }
            
            //return LoadAsyncImpl(handle, key);
        }

        public void Dispose()
        {
            foreach (var casheAsset in casheAssets)
            {
                Addressables.Release(casheAsset);
            }
            
            casheAssets.Clear();
        }
    }
}
