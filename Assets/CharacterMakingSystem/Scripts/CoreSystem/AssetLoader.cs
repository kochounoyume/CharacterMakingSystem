using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace CharacterMakingSystem.CoreSystem
{
    /// <summary>
    /// 要求されたアセットをロードして、メモリのキャッシュに貯蓄し、Disposeされるまで保持する
    /// </summary>
    public sealed class AssetLoader : IAssetLoader
    {
        // ロードされたアセットを保持持続する参照リスト
        private readonly List<AssetReference> cacheReferences = new List<AssetReference>();

        /// <summary>
        /// 同時に同じものをロードしないように交通整理する
        /// </summary>
        /// <param name="assetReference">アセットの参照</param>
        /// <param name="callback">コールバック（引数はロードしたオブジェクト）</param>
        /// <returns>ロードしたアセット</returns>
        public async UniTask<TObject> LoadAsync<TObject>(AssetReference assetReference, UnityAction<TObject> callback = null) where TObject : UnityEngine.Object
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
                var result = await Addressables.LoadAssetAsync<TObject>(assetReference);
                // コールバックがあれば起動することもできる
                callback?.Invoke(result);
                return result;
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
        /// <param name="callback">コールバック（引数はロードしたオブジェクト）</param>
        /// <returns>ロードしたアセット</returns>
        public async UniTask<TObject> LoadAsync<TObject>(string assetReference, UnityAction<TObject> callback = null) where TObject : UnityEngine.Object 
            => await LoadAsync<TObject>(new AssetReference(assetReference), callback);
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
