using UnityEngine;
using Zenject;

namespace CharacterMakingSystem.Window
{
    using Data;
    
    /// <summary>
    /// 顔選択シーンのプレゼンタークラス
    /// </summary>
    public class FaceWindowPresenter : WindowPresenterBase
    {
        [SerializeField, Tooltip("顔選択シーンのビュークラス")]
        private FaceWindowView faceWindowView = null;

        [Inject] private FaceWindowData data;
        
        private void Start()
        {
            base.SetWindowFunc(faceWindowView, data.windowBtnFuncData, CoreSystem.SceneName.Result);
            faceWindowView.SetFaceView(data.faceBtnFunc, data.faceSkinColor, data.faceSkinColorFunc, data.eyeColor, data.eyeColorFunc);
            faceWindowView.SetFaceBtn(data.faceId);
        }
    }
}
