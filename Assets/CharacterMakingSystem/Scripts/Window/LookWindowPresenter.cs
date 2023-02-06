using UnityEngine;
using Zenject;

namespace CharacterMakingSystem.Window
{
    using Data;
    
    /// <summary>
    /// 外見選択シーンのプレゼンタークラス
    /// </summary>
    public class LookWindowPresenter : WindowPresenterBase
    {
        [SerializeField, Tooltip("外見選択シーンのビュークラス")]
        private LookWindowView lookWindowView = null;

        [Inject] 
        private LookWindowData data;
        
        // Start is called before the first frame update
        private void Start()
        {
            base.SetWindowFunc(lookWindowView, data.windowBtnFuncData, CoreSystem.SceneName.Hair);
            lookWindowView.SetLookWindow(data.bodyHeight, data.bodyHeightFunc, data.skinColor, data.skinColorFunc);
        }
    }
}
