using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace CharacterMakingSystem.CoreSystem
{
    public interface IAssetLoader : IDisposable
    {
        /// <summary>
        /// 同時に同じものをロードしないように交通整理する
        /// </summary>
        /// <param name="assetReference">アセットの参照</param>
        /// <param name="callback">コールバック（引数はロードしたオブジェクト）</param>
        /// <returns>ロードしたアセット</returns>
        UniTask<TObject> LoadAsync<TObject>(AssetReference assetReference, UnityAction<TObject> callback = null)
            where TObject : UnityEngine.Object;

        /// <summary>
        /// 同時に同じものをロードしないように交通整理する
        /// </summary>
        /// <param name="assetReference">アセットの参照</param>
        /// <param name="callback">コールバック（引数はロードしたオブジェクト）</param>
        /// <returns>ロードしたアセット</returns>
        UniTask<TObject> LoadAsync<TObject>(string assetReference, UnityAction<TObject> callback = null)
            where TObject : UnityEngine.Object;
    }
}
