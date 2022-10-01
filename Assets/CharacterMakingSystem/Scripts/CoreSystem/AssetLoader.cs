using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CharacterMakingSystem.CoreSystem
{
    /// <summary>
    /// 要求されたアセットをロードして、メモリのキャッシュに貯蓄し、Disposeされるまで保持する
    /// </summary>
    public class AssetLoader : IAssetLoader
    {
        // ロードされたアセットを保持持続する参照リスト
        private readonly List<AssetReference> cacheReferences = new List<AssetReference>();
        
        /// <summary>
        /// 同時に同じものをロードしないように交通整理する
        /// </summary>
        /// <param name="assetReference">アセットの参照</param>
        /// <returns>ロードしたアセット</returns>
        public async UniTask<TObject> LoadAsync<TObject>(AssetReference assetReference) where TObject : UnityEngine.Object
        {
            // 指定されたアセットを既に参照していたらキャッシュから返す
            if (cacheReferences.Contains(assetReference))
            {
                // 同時に同じアセットをロードしようとしていたら待機させる
                await UniTask.WaitUntil(() => assetReference.IsDone);
            }
            else
            {
                // キャッシュになかったので参照リストに追加する
                cacheReferences.Add(assetReference);
            }

            try
            {
                // ロードする
                return await Addressables.LoadAssetAsync<TObject>(assetReference);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }

        /// <summary>
        /// 同時に同じものをロードしないように交通整理する
        /// </summary>
        /// <param name="assetReference">アセットの参照</param>
        /// <returns>ロードしたアセット</returns>
        public async UniTask<TObject> LoadAsync<TObject>(string assetReference) where TObject : UnityEngine.Object
        {
            return await LoadAsync<TObject>(new AssetReference(assetReference));
        }

        public void Dispose()
        {
            // 参照リストのアセットのキャッシュを開放する
            foreach (var cacheReference in cacheReferences)
            {
                cacheReference.ReleaseAsset();
            }
            
            // 参照リストをクリアする
            cacheReferences.Clear();
        }
    }
}
