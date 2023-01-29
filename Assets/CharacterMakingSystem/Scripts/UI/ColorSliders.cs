using UnityEngine;
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
