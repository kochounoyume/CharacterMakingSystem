using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CharacterMakingSystem.Stage
{
    using Data;
    using CoreSystem;
    
    /// <summary>
    /// 実際に3Dゲーム空間を扱うプレゼンタークラス
    /// </summary>
    public class StagePresenter : MonoBehaviour
    {
        [SerializeField, Tooltip("ビュークラス")] 
        private StageView stageView = null;

        /// <summary>
        /// 性別管理変数
        /// </summary>
        private bool isMale = false;

        /// <summary>
        /// 男性キャラクターデータ
        /// </summary>
        private CharacterData maleData;

        /// <summary>
        /// 女性キャラクターデータ
        /// </summary>
        private CharacterData femaleData;

        [Inject] 
        private ChaPartDatabase database;
        
        [Inject] 
        private IAssetLoader assetLoader;
        
        [Inject]
        private ISceneController sceneController;

        // Start is called before the first frame update
        void Start()
        {
            maleData = new CharacterData(Gender.Male, database.FindDefaultHairData(Gender.Male), database.FindDefaultFaceData(Gender.Male));
            femaleData = new CharacterData(Gender.Female, database.FindDefaultHairData(Gender.Female), database.FindDefaultFaceData(Gender.Female));
        }

        /// <summary>
        /// 性別変更のため、まるっとオブジェクトを切り替える
        /// </summary>
        async UniTask SetSex(Gender gender)
        {
            // 性別の変更がなければ流す
            if((isMale && gender == Gender.Male) || (!isMale && gender == Gender.Female)) return;
            
            isMale = !isMale;
            var (data, cos) = isMale ? (maleData, database.GetDefaultCos(Gender.Male)) : (femaleData, database.GetDefaultCos(Gender.Female));

            var asyncList = new List<UniTask<GameObject>>()
            {
                assetLoader.LoadAsync<GameObject>(cos, _ => stageView.InstantiateObj(_, StageView.Part.CostumeBody)),
                assetLoader.LoadAsync<GameObject>(data.hairData.Address, _ => stageView.InstantiateObj(_, StageView.Part.Hair, data.IsOverlap()))
            };

            // 重複生成回避
            if (!data.IsOverlap())
            {
                asyncList.Add(assetLoader.LoadAsync<GameObject>(data.faceData.Address, _ => stageView.InstantiateObj(_, StageView.Part.Face)));
            }
            
            await UniTask.WhenAll(asyncList);
            
            stageView.SetScale(data.bodyHeight);
            stageView.SetSkinColor(data.skinColor);
            stageView.SetHairColor(data.hairColor);
            stageView.SetEyeColor(data.eyeColor);
            stageView.SetFaceSkinColor(data.faceSkinColor);
        }
        
        
    }
}
