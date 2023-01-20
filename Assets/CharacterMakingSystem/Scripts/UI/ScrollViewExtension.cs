using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterMakingSystem.UI
{
    /// <summary>
    /// スクロールビューの要素のパフォーマンス調整とスクロールバーのアニメーションの管理クラス
    /// </summary>
    [RequireComponent(typeof(RectTransform), typeof(Image))]
    public class ScrollViewExtension : MonoBehaviour
    {
        private RectTransform rectTransform = null;
        private Image image = null;
        private Camera camera = null;
        
        // Start is called before the first frame update
        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
            
        }

        /// <summary>
        /// 対象のRectTransformの境界ボックスのコーナーのうち、指定のCameraから画面空間内で見えるものがあるかを判定する
        /// </summary>
        /// <param name="rectTransform">対象のRectTransform</param>
        /// <param name="camera">カメラ</param>
        /// <returns></returns>
        private bool IsVisibleCorners(RectTransform rectTransform, Camera camera)
        {
            // スクリーンスペース境界（カメラがスクリーン全体にレンダリングすることを想定）
            var screenBounds = new Rect(0f, 0f, Screen.width, Screen.height);
            var objectCorners = new Vector3[4];
            rectTransform.GetWorldCorners(objectCorners);

            return objectCorners.Select(camera.WorldToScreenPoint).Any(tempScreenSpaceCorner => screenBounds.Contains(tempScreenSpaceCorner));
        }
    }
}
