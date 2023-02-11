using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;
using Zenject;

namespace CharacterMakingSystem
{
    using CoreSystem;
    using Data;
    using Stage;
    
    /// <summary>
    /// キャラクターメイキングシステムを呼び出すクラス
    /// </summary>
    public sealed class CharacterMakingSystem
    {
        private CompleteCharaData data = null;
        private const string SKYBOX = "SkyboxLiteWarm";

        public async UniTask<CompleteCharaData> EntryCharacterMaking()
        {
            // Skyboxの設定
            var assetLoader = new AssetLoader();
            assetLoader.LoadAsync<Material>(SKYBOX).ToObservable().Subscribe(_ =>
            {
                RenderSettings.skybox = _;
            });

            var sceneController = new SceneController();
            sceneController.LoadBaseSceneAsync(container =>
            {
                container.BindInstance(new StageWindowData(_ => data = _)).WhenInjectedInto<StagePresenter>();
            }).Forget();

            await UniTask.WaitUntil(() => data != null);

            return data;
        }
    }
}