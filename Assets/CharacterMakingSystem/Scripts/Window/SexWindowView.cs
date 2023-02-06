using System;
using System.Linq;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CharacterMakingSystem.Window
{
    /// <summary>
    /// 性別選択シーンのビュークラス
    /// </summary>
    public class SexWindowView : WindowViewBase
    {
        [SerializeField, Tooltip("ボタン周りの処理で必要な要素の管理配列")]
        private BtnElement[] btnElements = null;

        [Serializable]
        private struct BtnElement
        {
            public Data.Gender gender;
            public Button btn;
            public RectTransform btnRect;
            public RectTransform iconImageRect;
        }

        /// <summary>
        /// 最初の一回だけボタン押しておくのでそのためのフラグ変数
        /// </summary>
        private bool isFirst = true;

        /// <summary>
        /// 性別選択ボタンがおされたときの処理設定メソッド
        /// </summary>
        /// <param name="minSizeRatio">最小縮小率</param>
        /// <param name="maxSizeRatio">最大拡大率</param>
        /// <param name="duration">アニメーション再生時間</param>
        /// <param name="func">ボタン押下時に起動する処理</param>
        public void SetSexWindow(float minSizeRatio, float maxSizeRatio, float duration, UnityAction<Data.Gender> func = null)
        {
            var btnSize = btnElements.FirstOrDefault().btnRect.sizeDelta;
            var btnMinSize = btnSize * minSizeRatio;
            var btnMaxSize = btnSize * maxSizeRatio;
            var iconSize = btnElements.FirstOrDefault().iconImageRect.sizeDelta;
            var iconMinSize = iconSize * minSizeRatio;
            var iconMaxSize = iconSize * maxSizeRatio;
            
            foreach (var btnElement in btnElements)
            {
                var otherElems = btnElements.Where(element => element.gender != btnElement.gender).ToArray();
                
                btnElement.btn.OnClickAsObservable().Subscribe(_ =>
                {
                    // 既にMAXサイズならボタン二度押ししているということなので、処理は流す
                    if(btnElement.btnRect.sizeDelta == btnMaxSize) return;
                    btnElement.btnRect.DOSizeDelta(btnMaxSize, duration);
                    btnElement.iconImageRect.DOSizeDelta(iconMaxSize, duration);
                    foreach (var otherElem in otherElems)
                    {
                        otherElem.btnRect.DOSizeDelta(btnMinSize, duration);
                        otherElem.iconImageRect.DOSizeDelta(iconMinSize, duration);
                    }
                    // 最初の一回以外は実行
                    if (isFirst) return;
                    func?.Invoke(btnElement.gender);
                });
            }
        }

        /// <summary>
        /// 最初に一回だけボタンをプログラム上から押しておく
        /// </summary>
        /// <param name="gender">性別情報</param>
        public void SetPushBtn(Data.Gender gender)
        {
            if (!isFirst) return;
            Array.Find(btnElements, element => element.gender == gender).btn.onClick.Invoke();
            isFirst = false;
        }
    }
}
