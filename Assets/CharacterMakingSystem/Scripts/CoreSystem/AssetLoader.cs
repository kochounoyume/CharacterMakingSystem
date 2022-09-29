using System;
using System.Collections;
using System.Collections.Generic;
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
        //// ロードされたアセットをhandleのまま保持持続するディクショナリ
        private Dictionary<string, AsyncOperationHandle> casheAssets = null;
        
        

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
