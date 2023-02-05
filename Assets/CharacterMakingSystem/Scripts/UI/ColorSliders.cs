using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CharacterMakingSystem.UI
{
    /// <summary>
    /// HSVで色を表すスライダー群
    /// </summary>
    public class ColorSliders : MonoBehaviour
    {
        [SerializeField, Tooltip("色・鮮やかさ・明るさ用スライダー")]
        private Slider color, saturation, brightness = null;

        /// <summary>
        /// コールバック
        /// </summary>
        private UnityAction<Color> onValueChanged = null;
        
        /// <summary>
        /// コールバックの取得・設定
        /// </summary>
        public UnityAction<Color> OnValueChanged
        {
            get => onValueChanged;
            set
            {
                onValueChanged = value;
                color.OnValueChangedAsObservable().Subscribe(_ => value.Invoke(GetColor())).AddTo(this);
                saturation.OnValueChangedAsObservable().Subscribe(_ => value.Invoke(GetColor())).AddTo(this);
                brightness.OnValueChangedAsObservable().Subscribe(_ => value.Invoke(GetColor())).AddTo(this);
            }
        }

        /// <summary>
        /// HSVでSliderで指定した色をRGBで返す
        /// </summary>
        /// <returns>RGBの色</returns>
        public Color GetColor() => Color.HSVToRGB(color.value, saturation.value, brightness.value);

        /// <summary>
        /// RGBで指定された色を各Sliderに設定する
        /// </summary>
        /// <param name="rgb"></param>
        public void SetColor(Color rgb)
        {
            Color.RGBToHSV(rgb, out float h, out float s, out float v);
            (color.value, saturation.value, brightness.value) = (h, s, v);
        }
    }
}
