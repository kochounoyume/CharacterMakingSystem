using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField, Tooltip("タブボタン")]
        private TabView[] tabViews = null;

        /// <summary>
        /// ボタンが押されていないときの色
        /// </summary>
        private static readonly Color disableColor = new Color(1, 1, 1, 0.7f);

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
        void Start()
        {
            if(tabViews.Length <= 1) return;

            // タブの切り替え
            foreach (var tabView in tabViews)
            {
                var otherViews = tabViews.Where(x => x.btn != tabView.btn).ToArray();
                tabView.btn
                    .OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        Debug.Log("マーシー");
                        
                        // 押された要素以外のタブの非表示とボタンの色を薄くする
                        foreach (var otherView in otherViews)
                        {
                            otherView.tab.SetActive(false);
                            otherView.icon.color = disableColor;
                            otherView.text.color = disableColor;
                        }
                        
                        // 押された要素のタブの表示とボタンの色を濃くする
                        tabView.tab.SetActive(true);
                        tabView.icon.color = Color.white;
                        tabView.text.color = Color.white;
                    })
                    .AddTo(this);
            }
            
            //Todo:タブバーのアニメーション
        }
    }
}
