using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace CharacterMakingSystem.CoreSystem
{
    public interface IAssetLoader : IDisposable
    {
        /// <summary>
        /// 同時に同じものをロードしないように交通整理する
        /// </summary>
        /// <param name="assetReference">アセットの参照</param>
        /// <returns>ロードしたアセット</returns>
        UniTask<TObject> LoadAsync<TObject>(AssetReference assetReference) where TObject : UnityEngine.Object;

        /// <summary>
        /// 同時に同じものをロードしないように交通整理する
        /// </summary>
        /// <param name="assetReference">アセットの参照</param>
        /// <returns>ロードしたアセット</returns>
        UniTask<TObject> LoadAsync<TObject>(string assetReference) where TObject : UnityEngine.Object;
    }
}
