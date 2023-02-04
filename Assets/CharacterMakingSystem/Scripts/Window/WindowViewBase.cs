using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CharacterMakingSystem.Window
{
    /// <summary>
    /// 性別・外見・髪・顔の各Windowのビューの基底クラス
    /// </summary>
    public class WindowViewBase : MonoBehaviour
    {
        [SerializeField, Tooltip("性別選択ボタン")] 
        private Button sexBtn = null;

        [SerializeField, Tooltip("外見選択ボタン")] 
        private Button lookBtn = null;

        [SerializeField, Tooltip("髪選択ボタン")] 
        private Button hairBtn = null;

        [SerializeField, Tooltip("顔選択ボタン")] 
        private Button faceBtn = null;

        [SerializeField, Tooltip("作成ボタン")]
        private Button createProgBtn = null;

        [SerializeField, Tooltip("次へボタン")] 
        private Button nextProgBtn = null;

        /// <summary>
        /// 性別選択ボタンに処理を登録するメソッド
        /// </summary>
        /// <param name="func">メソッド</param>
        public void SetFuncSexBtn(UnityAction func) => sexBtn.OnClickAsObservable().Subscribe(_ => func.Invoke());

        /// <summary>
        /// 外見選択ボタンに処理を登録するメソッド
        /// </summary>
        /// <param name="func">メソッド</param>
        public void SetFuncLookBtn(UnityAction func) => lookBtn.OnClickAsObservable().Subscribe(_ => func.Invoke());

        /// <summary>
        /// 髪選択ボタンに処理を登録するメソッド
        /// </summary>
        /// <param name="func">メソッド</param>
        public void SetFuncHairBtn(UnityAction func) => hairBtn.OnClickAsObservable().Subscribe(_ => func.Invoke());

        /// <summary>
        /// 顔選択ボタンに処理を登録するメソッド
        /// </summary>
        /// <param name="func">メソッド</param>
        public void SetFuncFaceBtn(UnityAction func) => faceBtn.OnClickAsObservable().Subscribe(_ => func.Invoke());

        /// <summary>
        /// 作成ボタンに処理を登録するメソッド
        /// </summary>
        /// <param name="func">メソッド</param>
        public void SetFuncCreateProgBtn(UnityAction func) =>
            createProgBtn.OnClickAsObservable().Subscribe(_ => func.Invoke());

        /// <summary>
        /// 次へ選択ボタンに処理を登録するメソッド
        /// </summary>
        /// <param name="func">メソッド</param>
        public void SetFuncNextProgBtn(UnityAction func) =>
            nextProgBtn.OnClickAsObservable().Subscribe(_ => func.Invoke());
    }
}
