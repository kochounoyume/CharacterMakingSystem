using UnityEngine;
using UnityEngine.Events;

namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// 外見選択ウィンドウで必要な情報のデータクラス
    /// </summary>
    public sealed class LookWindowData
    {
        /// <summary>
        /// 身長情報
        /// </summary>
        public float bodyHeight { get; private set; }
        
        /// <summary>
        /// 身長を変更する処理
        /// </summary>
        public UnityAction<float> bodyHeightFunc { get; private set; }
        
        /// <summary>
        /// 肌の色情報
        /// </summary>
        public Color skinColor { get; private set; }
        
        /// <summary>
        /// 肌の色を変更する処理
        /// </summary>
        public UnityAction<Color> skinColorFunc { get; private set; }
        
        /// <summary>
        /// 各Windowで共通で使用する処理登録用のデータクラス
        /// </summary>
        public WindowBtnFuncData windowBtnFuncData { get; private set; }

        /// <summary>
        /// 外見選択ウィンドウで必要な情報のデータクラスのコンストラクタ
        /// </summary>
        /// <param name="bodyHeight">身長情報</param>
        /// <param name="bodyHeightFunc">身長を変更する処理</param>
        /// <param name="skinColor">肌の色情報</param>
        /// <param name="skinColorFunc">肌の色を変更する処理</param>
        /// <param name="windowBtnFuncData">各Windowで共通で使用する処理登録用のデータクラス</param>
        public LookWindowData(
            float bodyHeight,
            UnityAction<float> bodyHeightFunc, 
            Color skinColor, 
            UnityAction<Color> skinColorFunc,
            WindowBtnFuncData windowBtnFuncData
            )
        {
            this.bodyHeight = bodyHeight;
            this.bodyHeightFunc = bodyHeightFunc;
            this.skinColor = skinColor;
            this.skinColorFunc = skinColorFunc;
            this.windowBtnFuncData = windowBtnFuncData;
        }
    }
}
