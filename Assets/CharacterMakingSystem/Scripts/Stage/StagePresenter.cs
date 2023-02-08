using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// 各ウィンドウで使用するシーン遷移処理統括管理クラス
        /// </summary>
        private WindowBtnFuncData windowBtnFuncData = null;

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
            
            // シーン遷移関連の処理の設定
            windowBtnFuncData = new WindowBtnFuncData(
                sexBtnFunc: () => 
                {
                    SceneUnLoader(SceneName.Sex);
                    sceneController.LoadSceneAsync(SceneName.Sex, container =>
                    {
                        // 登録する処理は性別変更のため、まるっとオブジェクトを切り替える
                        container
                            .BindInstance(new SexWindowData(
                                isMale,
                                async gender =>
                                {
                                    // 性別の変更がなければ流す
                                    if((isMale && gender == Gender.Male) || (!isMale && gender == Gender.Female)) return;
            
                                    isMale = !isMale;
                                    var (charaData, cos) 
                                        = isMale 
                                            ? (maleData, database.GetDefaultCos(Gender.Male)) 
                                            : (femaleData, database.GetDefaultCos(Gender.Female));

                                    bool isOverlap = charaData.IsOverlap();

                                    var asyncList = new List<UniTask<GameObject>>()
                                    {
                                        assetLoader.LoadAsync<GameObject>(
                                            cos, 
                                            _ => stageView.InstantiateObj(_, StageView.Part.CostumeBody)),
                                        assetLoader.LoadAsync<GameObject>(
                                            charaData.hairData.Address, 
                                            _ => stageView.InstantiateObj(_, StageView.Part.Hair, isOverlap))
                                    };

                                    // 重複生成回避
                                    if (!isOverlap)
                                    {
                                        asyncList.Add(
                                            assetLoader.LoadAsync<GameObject>(charaData.faceData.Address,
                                                _ => 
                                                    stageView.InstantiateObj(
                                                        _, 
                                                        StageView.Part.Face)));
                                    }
            
                                    await UniTask.WhenAll(asyncList);
            
                                    stageView.SetScale(charaData.bodyHeight);
                                    if (stageView.GetSkinColor() != charaData.skinColor)
                                    {
                                        stageView.SetSkinColor(charaData.skinColor);
                                    }
                                    if (stageView.GetHairColor() != charaData.hairColor)
                                    {
                                        stageView.SetHairColor(charaData.hairColor);
                                    }
                                    if (stageView.GetEyeColor() != charaData.eyeColor)
                                    {
                                        stageView.SetEyeColor(charaData.eyeColor);
                                    }
                                    if (stageView.GetFaceSkinColor() != charaData.faceSkinColor)
                                    {
                                        stageView.SetFaceSkinColor(charaData.faceSkinColor);
                                    }
                                },
                                windowBtnFuncData))
                            .WhenInjectedInto<SexWindowPresenter>();
                    });
                },
                lookBtnFunc: () =>
                {
                    SceneUnLoader(SceneName.Look);
                    sceneController.LoadSceneAsync(SceneName.Look, container =>
                    {
                        var charaData = isMale ? maleData : femaleData;
                        container
                            .BindInstance(new LookWindowData(
                                charaData.bodyHeight,
                                _ =>
                                {
                                    charaData.bodyHeight = _;
                                    stageView.SetScale(_);
                                },
                                charaData.skinColor,
                                _ =>
                                {
                                    charaData.skinColor = _;
                                    stageView.SetSkinColor(_);
                                },
                                windowBtnFuncData))
                            .WhenInjectedInto<LookWindowPresenter>();
                    });
                },
                hairBtnFunc: () =>
                {
                    SceneUnLoader(SceneName.Hair);
                    sceneController.LoadSceneAsync(SceneName.Hair, container =>
                    {
                        var charaData = isMale ? maleData : femaleData;
                        container
                            .BindInstance(new HairWindowData(
                                charaData.hairData.HairID,
                                value =>
                                {
                                    var targetData = database.FindHairData(charaData.gender, value);
                                    charaData.hairData = targetData;
                                    assetLoader.LoadAsync<GameObject>(targetData.Address, _ => 
                                    {
                                        stageView.InstantiateObj(_, StageView.Part.Hair, targetData == database.FindDefaultHairData(charaData.gender));
                                        if (stageView.GetHairColor() != charaData.hairColor)
                                        {
                                            stageView.SetHairColor(charaData.hairColor);
                                        }
                                        if (stageView.GetFaceSkinColor() != charaData.faceSkinColor)
                                        {
                                            stageView.SetFaceSkinColor(charaData.faceSkinColor);
                                        }
                                    }).Forget();
                                },
                                charaData.hairColor,
                                _ =>
                                {
                                    charaData.hairColor = _;
                                    stageView.SetHairColor(_);
                                },
                                windowBtnFuncData))
                            .WhenInjectedInto<HairWindowPresenter>();
                    });
                },
                faceBtnFunc: () =>
                {
                    SceneUnLoader(SceneName.Face);
                    sceneController.LoadSceneAsync(SceneName.Face, container =>
                    {
                        var charaData = isMale ? maleData : femaleData;
                        container
                            .BindInstance(new FaceWindowData(
                                charaData.faceData.FaceID,
                                value =>
                                {
                                    var targetData = database.FindFaceData(charaData.gender, value);
                                    charaData.faceData = targetData;
                                    assetLoader.LoadAsync<GameObject>(targetData.Address, _ =>
                                    {
                                        stageView.InstantiateObj(_, StageView.Part.Face, targetData == database.FindDefaultFaceData(charaData.gender));
                                        if (stageView.GetHairColor() != charaData.hairColor)
                                        {
                                            stageView.SetHairColor(charaData.hairColor);
                                        }

                                        if (stageView.GetFaceSkinColor() != charaData.faceSkinColor)
                                        {
                                            stageView.SetFaceSkinColor(charaData.faceSkinColor);
                                        }
                                    }).Forget();
                                },
                                charaData.faceSkinColor,
                                _ =>
                                {
                                    charaData.faceSkinColor = _;
                                    stageView.SetFaceSkinColor(_);
                                },
                                charaData.eyeColor,
                                _ =>
                                {
                                    charaData.eyeColor = _;
                                    stageView.SetEyeColor(_);
                                },
                                windowBtnFuncData))
                            .WhenInjectedInto<FaceWindowPresenter>();
                    });
                },
                createProgBtnFunc: () =>
                {
                    SceneUnLoader(SceneName.Result);
                    sceneController.LoadSceneAsync(SceneName.Result);
                },
                nextProgBtnFunc: _ =>
                {
                    SceneUnLoader(_);
                    sceneController.LoadSceneAsync(_);
                });
            
            // 最初に性別選択シーンを起動
            windowBtnFuncData.sexBtnFunc.Invoke();

            // 最初に配置しているのは女性キャラなので、色も整えておく
            stageView.SetSkinColor(femaleData.skinColor);
            stageView.SetHairColor(femaleData.hairColor);
            stageView.SetEyeColor(femaleData.eyeColor);
            stageView.SetFaceSkinColor(femaleData.faceSkinColor);
        }

        /// <summary>
        /// 該当シーン以外のシーンを削除する
        /// </summary>
        /// <param name="sceneName"></param>
        private void SceneUnLoader(SceneName sceneName)
        {
            var otherScenes = sceneController.FindOtherSceneNames(sceneName);
            foreach (var activeScene in sceneController.GetActiveSceneNames())
            {
                if (otherScenes.Contains(activeScene))
                {
                    sceneController.UnloadSceneAsync(activeScene).Forget();
                }
            }
        }
    }
}
