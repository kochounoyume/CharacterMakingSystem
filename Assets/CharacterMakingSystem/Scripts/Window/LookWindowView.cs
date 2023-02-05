using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CharacterMakingSystem.Window
{
    using UI;
    
    /// <summary>
    /// 外見選択シーンのビュークラス
    /// </summary>
    public class LookWindowView : WindowViewBase
    {
        [SerializeField, Tooltip("身長編集用のスライダー")]
        private Slider bodyHeightSlider = null;

        [SerializeField, Tooltip("肌の色の編集用のスライダー群")]
        private ColorSliders skinColorSlider = null;

        /// <summary>
        /// 外見選択シーンに必要な各種スライダー群に設定をする
        /// </summary>
        /// <param name="bodyHeight">身長情報</param>
        /// <param name="bodyHeightFunc">身長編集処理</param>
        /// <param name="skinColor">肌の色情報</param>
        /// <param name="skinColorFunc">肌の色変更処理</param>
        public void SetLookSliders(float bodyHeight, UnityAction<float> bodyHeightFunc, Color skinColor, UnityAction<Color> skinColorFunc)
        {
            bodyHeightSlider.value = bodyHeight;
            bodyHeightSlider.OnValueChangedAsObservable().Subscribe(bodyHeightFunc.Invoke).AddTo(this);
            skinColorSlider.SetColor(skinColor);
            skinColorSlider.OnValueChanged = skinColorFunc;
        }
    }
}
