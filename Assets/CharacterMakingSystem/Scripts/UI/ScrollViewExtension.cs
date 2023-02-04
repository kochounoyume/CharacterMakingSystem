using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterMakingSystem.UI
{
    /// <summary>
    /// スクロールビューの要素のパフォーマンス調整とスクロールバーのアニメーションの管理クラス
    /// </summary>
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollViewExtension : MonoBehaviour
    {
        [SerializeField, Tooltip("スクロールバーの消えていくアニメーションの所要時間")]
        private float animDuration = 3.0f;

        // Start is called before the first frame update
        private void Start()
        {
            var scrollRect = GetComponent<ScrollRect>();
            var viewport = scrollRect.viewport;
            var content = scrollRect.content;
            var scrollbarImages = scrollRect.horizontalScrollbar.GetComponentsInChildren<Image>(true);
            var scrollbarDatas = new (Image barImage, Color color, Tweener tweener)[scrollbarImages.Length];
            for (int i = 0; i < scrollbarDatas.Length; i++)
            {
                var scrollbarImage = scrollbarImages[i];
                scrollbarDatas[i] = (scrollbarImage, scrollbarImage.color, null);
            }

            // ScrollViewの各要素は画面反映外では非表示になる
            for (int i = 0; i < content.childCount; i++)
            {
                var child = content.GetChild(i);
                if (!child.TryGetComponent(out Image image) || !child.TryGetComponent(out RectTransform rect)) continue;
                var images = child.GetComponentsInChildren<Image>();
                foreach (var img in images)
                {
                    img.enabled = IsVisible(rect, viewport);
                }
                rect.ObserveEveryValueChanged(_ => _.position)
                    .Where(_ => image.enabled != IsVisible(rect, viewport))
                    .Subscribe(_ =>
                    {
                        foreach (var img in images)
                        {
                            img.enabled = !img.enabled;
                        }
                    })
                    .AddTo(this);
            }

            // 操作した時に表示されるScrollBar
            scrollRect
                .OnDragAsObservable()
                .Subscribe(_ =>
                {
                    foreach (var scrollbarData in scrollbarDatas)
                    {
                        if (scrollbarData.tweener != null && scrollbarData.tweener.IsPlaying())
                        {
                            scrollbarData.tweener.Kill();
                            scrollbarData.barImage.color = scrollbarData.color;
                        }
                        scrollbarData.barImage.enabled = true;
                    }
                })
                .AddTo(this);

            // 手を離すとゆっくり消えていくタイプのScrollBarアニメーション
            scrollRect
                .OnEndDragAsObservable()
                .Subscribe(_ =>
                {
                    for (int i = 0; i < scrollbarDatas.Length; i++)
                    {
                        var barImage = scrollbarDatas[i].barImage;
                        var barColor = scrollbarDatas[i].color;
                        scrollbarDatas[i].tweener = DOTween
                            .ToAlpha(
                                getter: () => barImage.color, 
                                setter: color => barImage.color = color,
                                endValue: 0,
                                duration: animDuration
                                )
                            .OnComplete(() =>
                            {
                                barImage.enabled = false;
                                barImage.color = barColor;
                            });
                        WaitAnim(i).Forget(); // 投げっぱなしで並列非同期
                    }
                })
                .AddTo(this);

            // アニメーションを非同期待機処理したのちリセットする
            async UniTask WaitAnim(int index)
            {
                await scrollbarDatas[index].tweener;
                scrollbarDatas[index].tweener = null;
            }

            // 対象のRectTransformが別のRectTransformの矩形内にあるかどうかを判定する
            bool IsVisible(RectTransform targetRect,RectTransform viewportRect)
            {
                // 返される4つの頂点は時計回りに 左下，左上，右上，右下
                var targetCorners = new Vector3[4];
                targetRect.GetWorldCorners(targetCorners);
                var viewportCorners = new Vector3[4];
                viewportRect.GetWorldCorners(viewportCorners);

                return targetCorners[2].x > viewportCorners[0].x
                       && targetCorners[0].x < viewportCorners[2].x
                       && targetCorners[2].y > viewportCorners[0].y
                       && targetCorners[0].y < viewportCorners[2].y;
            }
        }
    }
}
