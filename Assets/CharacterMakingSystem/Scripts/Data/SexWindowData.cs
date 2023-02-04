using UnityEngine.Events;

namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// 性別選択ウィンドウで必要な情報のデータクラス
    /// </summary>
    public sealed class SexWindowData
    {
        /// <summary>
        /// 現在の性別
        /// </summary>
        public bool isCurrentMale { get; private set; }
        
        /// <summary>
        /// 性別を変更する処理の登録デリゲート
        /// </summary>
        public UnityAction<Gender> genderFunc { get; private set; }
        
        /// <summary>
        /// 各Windowで共通で使用する処理登録用のデータクラス
        /// </summary>
        public WindowBtnFuncData windowBtnFuncData { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="isCurrentMale">現在の性別</param>
        /// <param name="genderFunc">性別を変更する処理の登録デリゲート</param>
        /// <param name="windowBtnFuncData">各Windowで共通で使用する処理登録用のデータクラス</param>
        public SexWindowData(bool isCurrentMale, UnityAction<Gender> genderFunc, WindowBtnFuncData windowBtnFuncData)
        {
            this.isCurrentMale = isCurrentMale;
            this.genderFunc = genderFunc;
            this.windowBtnFuncData = windowBtnFuncData;
        }
    }
}
