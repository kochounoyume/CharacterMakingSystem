using UnityEngine;
using Zenject;

namespace CharacterMakingSystem.Window
{
    using Data;
    
    /// <summary>
    /// 髪選択シーンのプレゼンタークラス
    /// </summary>
    public class HairWindowPresenter : WindowPresenterBase
    {
        [SerializeField, Tooltip("髪選択シーンのビュークラス")]
        private HairWindowView hairWindowView = null;

        [Inject] 
        private HairWindowData data;
        
        // Start is called before the first frame update
        void Start()
        {
            base.SetWindowFunc(hairWindowView, data.windowBtnFuncData, CoreSystem.SceneName.Face);
            hairWindowView.SetHairView(data.hairBtnFunc, data.hairColor, data.hairColorFunc);
            hairWindowView.SetHairBtn(data.hairId);
        }
    }

}