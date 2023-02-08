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
        private const float duration = 1.0f;
        
        private void Start()
        {
            var mask = GetComponent<RectMask2D>();
            mask.padding = SetRight(mask.padding, Screen.width * 2);
            DOTween.To(
                getter: () => mask.padding.z,
                setter: value => mask.padding = SetRight(mask.padding, value),
                endValue: 0,
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
