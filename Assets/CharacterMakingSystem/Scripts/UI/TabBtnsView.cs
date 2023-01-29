using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace CharacterMakingSystem.UI
{
    /// <summary>
    /// タブボタンの管理ビュークラス
    /// </summary>
    public class TabBtnsView : MonoBehaviour
    {
        [SerializeField, Tooltip("スクロールバーの消えていくアニメーションの所要時間")]
        private float animDuration = 0.5f;
        
        [SerializeField, Tooltip("タブボタン")]
        private TabView[] tabViews = null;
        
        [SerializeField, Tooltip("タブラインバー")]
        private Scrollbar tabLineBar = null;

        /// <summary>
        /// ボタンが押されていないときの色
        /// </summary>
        private static readonly Color DISABLE_COLOR = new Color(1, 1, 1, 0.7f);

        /// <summary>
        /// タブボタンとタブそのものを一括で扱う
        /// </summary>
        [System.Serializable]
        private struct TabView
        {
            public Button btn;
            public GameObject tab;
            public Image icon;
            public TextMeshProUGUI text;
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            if(tabViews.Length <= 1) return;

            // ボタンの配置域と最小座標
            var btnsRect = tabViews[0].btn.transform.parent.GetComponent<RectTransform>();
            var btnsSize = btnsRect.rect.size.x * btnsRect.lossyScale.x;
            var btnRect = tabViews[0].btn.GetComponent<RectTransform>();
            var btnWidth = btnRect.rect.size.x * btnRect.lossyScale.x;
            var barMoveRange = btnsSize - btnWidth;
            var barMin = GetCenterX(btnsRect) - btnsSize / 2 + btnWidth / 2;

            // タブの切り替え
            foreach (var tabView in tabViews)
            {
                var otherViews = tabViews.Where(x => x.btn != tabView.btn);
                tabView.btn
                    .OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        // 押された要素以外のタブの非表示とボタンの色を薄くする
                        foreach (var otherView in otherViews)
                        {
                            otherView.tab.SetActive(false);
                            otherView.icon.color = DISABLE_COLOR;
                            otherView.text.color = DISABLE_COLOR;
                        }
                        
                        // 押された要素のタブの表示とボタンの色を濃くする
                        tabView.tab.SetActive(true);
                        tabView.icon.color = Color.white;
                        tabView.text.color = Color.white;
                        
                        // タブラインバーのアニメーション
                        DOTween.To(
                            getter: () => tabLineBar.value,
                            setter: value => tabLineBar.value = value,
                            endValue: (GetCenterX(tabView.text.rectTransform) - barMin) / barMoveRange,
                            duration: animDuration
                        );
                    })
                    .AddTo(this);
            }

            // 指定のUIの中央のｘ座標を取得
            float GetCenterX(RectTransform targetRect)
            {
                var position = targetRect.position.x;

                if (targetRect.pivot != Vector2.one / 2)
                {
                    var width = targetRect.rect.size.x;
                    position -= Mathf.Lerp(-width / 2f, width / 2f, targetRect.pivot.x) * targetRect.lossyScale.x;
                }

                return position;
            }
        }
    }
}
