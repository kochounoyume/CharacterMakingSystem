using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CharacterMakingSystem.Window
{
    using UI;
    
    /// <summary>
    /// 顔選択シーンのビュークラス
    /// </summary>
    public class FaceWindowView : WindowViewBase
    {
        [SerializeField, Tooltip("顔の形を選択できるボタン群")]
        private Button[] buttons = null;

        [SerializeField, Tooltip("顔の肌の色の編集用のスライダー群")]
        private ColorSliders faceSkinColorSlider = null;

        [SerializeField, Tooltip("瞳の色の編集用のスライダー群")]
        private ColorSliders eyeColorSliders = null;
        
        /// <summary>
        /// 最初の一回だけボタンを選択状態にしておくのでそのためのフラグ変数
        /// </summary>
        private bool isFirst = true;

        /// <summary>
        /// 顔選択シーンに必要な各種UIに設定をする
        /// </summary>
        /// <param name="faceBtnFunc">顔の形を変更するボタンに紐づける処理</param>
        /// <param name="faceSkinColor">顔の肌の色情報</param>
        /// <param name="faceSkinColorFunc">顔の肌の色を変更する処理</param>
        /// <param name="eyeColor">瞳の色情報</param>
        /// <param name="eyeColorFunc">瞳の色を変更する処理</param>
        public void SetFaceView(
            UnityAction<int> faceBtnFunc, 
            Color faceSkinColor, 
            UnityAction<Color> faceSkinColorFunc,
            Color eyeColor,
            UnityAction<Color> eyeColorFunc)
        {
            // 選択したボタンはアウトライン機能を使う
            var outlines = buttons.FirstOrDefault()?.transform.parent.GetComponentsInChildren<Outline>();
            foreach (var button in buttons)
            {
                if (!button.TryGetComponent(out Outline outline)) return;
                button.OnClickAsObservable().Subscribe(_ =>
                {
                    faceBtnFunc.Invoke(Array.IndexOf(buttons, button) + 1);
                    outline.enabled = true;
                    foreach (var variOutline in outlines?.Where(_ => _ != outline && _.enabled))
                    {
                        variOutline.enabled = false;
                    }
                }).AddTo(this);
            }

            faceSkinColorSlider.SetColor(faceSkinColor);
            faceSkinColorSlider.OnValueChanged = faceSkinColorFunc;
            eyeColorSliders.SetColor(eyeColor);
            eyeColorSliders.OnValueChanged = eyeColorFunc;
        }
        
        /// <summary>
        /// 最初に一回だけ顔の形を選択状態にしておく
        /// </summary>
        /// <param name="id">顔の形のID</param>
        public void SetFaceBtn(int id)
        {
            if(!buttons[id - 1].TryGetComponent(out Outline outline) && !isFirst) return;
            outline.enabled = true;
            isFirst = false;
        }
    }
}
