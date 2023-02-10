using Cysharp.Threading.Tasks;

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
        
        // private UniTask<CompleteCharaData> EntryCharacterMaking()
        // {
        //     var sceneController = new SceneController();
        //     sceneController?.LoadBaseSceneAsync(container =>
        //     {
        //         container.BindInstance(new StageWindowData(_ => data = _)).WhenInjectedInto<StagePresenter>();
        //     });
        // }
    }
}