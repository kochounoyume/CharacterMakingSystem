using UnityEngine;
using Zenject;

namespace CharacterMakingSystem.Window
{
    using Data;
    
    /// <summary>
    /// 性別選択シーンのプレゼンタークラス
    /// </summary>
    public class SexWindowPresenter : WindowPresenterBase
    {
        [SerializeField, Tooltip("性別選択シーンのビュークラス")] 
        private SexWindowView sexWindowView = null;
        
        private const float minSizeRatio = 0.8f;
        private const float maxSizeRatio = 1.2f;
        private const float animTime = 0.3f;

        [Inject]
        private SexWindowData data;

        // Start is called before the first frame update
        void Start()
        {
            base.SetWindowFunc(sexWindowView, data.windowBtnFuncData, CoreSystem.SceneName.Look);
            sexWindowView.SetSexBtn(minSizeRatio, maxSizeRatio, animTime, data.genderFunc);
            sexWindowView.SetPushBtn(data.isCurrentMale ? Gender.Male : Gender.Female);
        }
    }
}
