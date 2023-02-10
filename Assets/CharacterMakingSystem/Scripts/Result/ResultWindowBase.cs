using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CharacterMakingSystem.Result
{
    using CoreSystem;
    using Data;
    using UI;
    
    /// <summary>
    /// キャラクターメイク確定画面の実装と最終的な完成データを返すクラス
    /// </summary>
    public class ResultWindowBase : MonoBehaviour
    {
        [SerializeField, Tooltip("完了ボタン")]
        private Button completeBtn = null;

        [SerializeField, Tooltip("キャンセルボタン")]
        private Button cancelBtn = null;

        [SerializeField, Tooltip("テロップアニメーション")]
        private TelopAnimManager telopAnimManager = null;

        [Inject] 
        private ISceneController sceneController;

        [Inject] 
        private ResultWindowData data;

        private void Start()
        {
            completeBtn.OnClickAsObservable().Subscribe(_ =>
            {
                telopAnimManager.TelopAnim(true);
                data?.completeBtnFunc.Invoke();
            }).AddTo(this);
            
            cancelBtn.OnClickAsObservable().Subscribe(_ =>
            {
                sceneController.UnloadSceneAsync(SceneName.Result);
            }).AddTo(this);
        }
    }
}
