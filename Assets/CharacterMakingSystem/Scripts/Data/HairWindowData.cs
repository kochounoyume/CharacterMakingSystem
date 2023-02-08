using UnityEngine;
using UnityEngine.Events;

namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// 髪選択ウィンドウで必要な情報のデータクラス
    /// </summary>
    public sealed class HairWindowData
    {
        /// <summary>
        /// 髪型のIDデータ
        /// </summary>
        public int hairId { get; private set; }
        
        /// <summary>
        /// 髪型を変更するボタンに紐づける処理
        /// </summary>
        public UnityAction<int> hairBtnFunc { get; private set; }
        
        /// <summary>
        /// 肌の色情報
        /// </summary>
        public Color hairColor { get; private set; }
        
        /// <summary>
        /// 肌の色を変更する処理
        /// </summary>
        public UnityAction<Color> hairColorFunc { get; private set; }
        
        /// <summary>
        /// 各Windowで共通で使用する処理登録用のデータクラス
        /// </summary>
        public WindowBtnFuncData windowBtnFuncData { get; private set; }

        /// <summary>
        /// 髪選択ウィンドウで必要な情報のデータクラスのコンストラクタ
        /// </summary>
        /// <param name="hairId">髪型のIDデータ</param>
        /// <param name="hairBtnFunc">髪型を変更するボタンに紐づける処理</param>
        /// <param name="hairColor">肌の色情報</param>
        /// <param name="hairColorFunc">肌の色を変更する処理</param>
        /// <param name="windowBtnFuncData">各Windowで共通で使用する処理登録用のデータクラス</param>
        public HairWindowData(
            int hairId,
            UnityAction<int> hairBtnFunc, 
            Color hairColor, 
            UnityAction<Color> hairColorFunc,
            WindowBtnFuncData windowBtnFuncData)
        {
            this.hairId = hairId;
            this.hairBtnFunc = hairBtnFunc;
            this.hairColor = hairColor;
            this.hairColorFunc = hairColorFunc;
            this.windowBtnFuncData = windowBtnFuncData;
        }
    }
}
