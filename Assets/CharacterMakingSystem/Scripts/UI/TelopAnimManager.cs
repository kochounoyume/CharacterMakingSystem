using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterMakingSystem.UI
{
    /// <summary>
    /// テロップの登場アニメを実装するクラス
    /// </summary>
    [RequireComponent(typeof(RectMask2D))]
    public class TelopAnimManager : MonoBehaviour
    {
        [SerializeField, Tooltip("アニメーション時間")]
        private float duration = 1.0f;
        
        private void Start() => TelopAnim();

        /// <summary>
        /// テロップ部分のアニメーションを実行する
        /// </summary>
        /// <param name="isBehind">アニメーションの表示化・非表示化の挙動フラグ</param>
        public void TelopAnim(bool isBehind = false)
        {
            var mask = GetComponent<RectMask2D>();
            var screenWidth = Screen.width;
            if (!isBehind)
            {
                mask.padding = SetRight(mask.padding, screenWidth * 2);
            }
            DOTween.To(
                getter: () => mask.padding.z,
                setter: value => mask.padding = SetRight(mask.padding, value),
                endValue: isBehind ? screenWidth * 2 : 0,
                duration: duration
            );

            Vector4 SetRight(Vector4 target, float z)
            {
                target.z = z;
                return target;
            }
        }
    }
}
