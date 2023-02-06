using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CharacterMakingSystem.Window
{
    using UI;

    /// <summary>
    /// 髪選択シーンのプレゼンタークラス
    /// </summary>
    public class HairWindowView : WindowViewBase
    {
        [SerializeField, Tooltip("髪型を選択できるボタン群")]
        private Button[] buttons = null;

        [SerializeField, Tooltip("髪の色の編集用のスライダー群")]
        private ColorSliders hairColorSlider = null;

        /// <summary>
        /// 最初の一回だけボタンを選択状態にしておくのでそのためのフラグ変数
        /// </summary>
        private bool isFirst = true;

        /// <summary>
        /// 髪選択シーンに必要な各種UIに設定をする
        /// </summary>
        /// <param name="hairBtnFunc">髪型を変更するボタンに紐づける処理</param>
        /// <param name="hairColor">肌の色情報</param>
        /// <param name="hairColorFunc">肌の色を変更する処理</param>
        public void SetHairView(UnityAction<int> hairBtnFunc, Color hairColor, UnityAction<Color> hairColorFunc)
        {
            // 選択したボタンはアウトライン機能を使う
            var outlines = buttons.FirstOrDefault()?.transform.parent.GetComponentsInChildren<Outline>();
            foreach (var button in buttons)
            {
                if (!button.TryGetComponent(out Outline outline)) return;
                button.OnClickAsObservable().Subscribe(_ =>
                {
                    hairBtnFunc.Invoke(Array.IndexOf(buttons, button) + 1);
                    outline.enabled = true;
                    foreach (var variOutline in outlines?.Where(_ => _ != outline && _.enabled))
                    {
                        variOutline.enabled = false;
                    }
                }).AddTo(this);
            }

            hairColorSlider.SetColor(hairColor);
            hairColorSlider.OnValueChanged = hairColorFunc;
        }
        
        /// <summary>
        /// 最初に一回だけ髪型を選択状態にしておく
        /// </summary>
        /// <param name="id">髪型のID</param>
        public void SetHairBtn(int id)
        {
            if(!buttons[id - 1].TryGetComponent(out Outline outline) && !isFirst) return;
            outline.enabled = true;
            isFirst = false;
        }
    }
}
