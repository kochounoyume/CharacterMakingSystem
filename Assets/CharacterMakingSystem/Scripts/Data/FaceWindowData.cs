using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// 顔選択ウィンドウで必要な情報のデータクラス
    /// </summary>
    public sealed class FaceWindowData
    {
        /// <summary>
        /// 顔の形のIDデータ
        /// </summary>
        public int faceId { get; private set; }
        
        /// <summary>
        /// 顔の形を変更するボタンに紐づける処理
        /// </summary>
        public UnityAction<int> faceBtnFunc { get; private set; } 
        
        /// <summary>
        /// 顔の肌の色情報
        /// </summary>
        public Color faceSkinColor { get; private set; }
        
        /// <summary>
        /// 顔の肌の色を変更する処理
        /// </summary>
        public UnityAction<Color> faceSkinColorFunc { get; private set; }
        
        /// <summary>
        /// 瞳の色情報
        /// </summary>
        public Color eyeColor { get; private set; }
        
        /// <summary>
        /// 瞳の色を変更する処理
        /// </summary>
        public UnityAction<Color> eyeColorFunc { get; private set; }
        
        /// <summary>
        /// 各Windowで共通で使用する処理登録用のデータクラス
        /// </summary>
        public WindowBtnFuncData windowBtnFuncData { get; private set; }

        /// <summary>
        /// 顔選択ウィンドウで必要な情報のデータクラスのコンストラクタ
        /// </summary>
        /// <param name="faceId">顔の形のIDデータ</param>
        /// <param name="faceBtnFunc">顔の形を変更するボタンに紐づける処理</param>
        /// <param name="faceSkinColor">顔の肌の色情報</param>
        /// <param name="faceSkinColorFunc">顔の肌の色を変更する処理</param>
        /// <param name="eyeColor">瞳の色情報</param>
        /// <param name="eyeColorFunc">瞳の色を変更する処理</param>
        /// <param name="windowBtnFuncData">各Windowで共通で使用する処理登録用のデータクラス</param>
        public FaceWindowData(
            int faceId,
            UnityAction<int> faceBtnFunc,
            Color faceSkinColor,
            UnityAction<Color> faceSkinColorFunc,
            Color eyeColor,
            UnityAction<Color> eyeColorFunc,
            WindowBtnFuncData windowBtnFuncData)
        {
            this.faceId = faceId;
            this.faceBtnFunc = faceBtnFunc;
            this.faceSkinColor = faceSkinColor;
            this.faceSkinColorFunc = faceSkinColorFunc;
            this.eyeColor = eyeColor;
            this.eyeColorFunc = eyeColorFunc;
            this.windowBtnFuncData = windowBtnFuncData;
        }
    }
}
