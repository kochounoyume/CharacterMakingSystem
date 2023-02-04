using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CharacterMakingSystem.Stage
{
    using Data;
    using CoreSystem;
    using Window;
    
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
            // デフォルトの男性・女性用データ登録
            maleData = new CharacterData(Gender.Male, database.FindDefaultHairData(Gender.Male), database.FindDefaultFaceData(Gender.Male));
            femaleData = new CharacterData(Gender.Female, database.FindDefaultHairData(Gender.Female), database.FindDefaultFaceData(Gender.Female));
            
            // Todo:WindowBtnFuncDataを作成し、nullを置き換える

            // 最初に性別選択シーンを起動
            sceneController.LoadSceneAsync(SceneName.Sex, container =>
            {
                container.BindInstance(new SexWindowData(isMale, gender => SetSex(gender).Forget(), null)).WhenInjectedInto<SexWindowPresenter>();
            });
            
            // 最初に配置しているのは女性キャラなので、色も整えておく
            stageView.SetSkinColor(femaleData.skinColor);
            stageView.SetHairColor(femaleData.hairColor);
            stageView.SetEyeColor(femaleData.eyeColor);
            stageView.SetFaceSkinColor(femaleData.faceSkinColor);
        }

        /// <summary>
        /// 性別変更のため、まるっとオブジェクトを切り替える
        /// </summary>
        private async UniTask SetSex(Gender gender)
        {
            // 性別の変更がなければ流す
            if((isMale && gender == Gender.Male) || (!isMale && gender == Gender.Female)) return;
            
            isMale = !isMale;
            var (data, cos) = isMale ? (maleData, database.GetDefaultCos(Gender.Male)) : (femaleData, database.GetDefaultCos(Gender.Female));

            bool isOverlap = data.IsOverlap();

            var asyncList = new List<UniTask<GameObject>>()
            {
                assetLoader.LoadAsync<GameObject>(cos, _ => stageView.InstantiateObj(_, StageView.Part.CostumeBody)),
                assetLoader.LoadAsync<GameObject>(data.hairData.Address, _ => stageView.InstantiateObj(_, StageView.Part.Hair, isOverlap))
            };

            // 重複生成回避
            if (!isOverlap)
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
