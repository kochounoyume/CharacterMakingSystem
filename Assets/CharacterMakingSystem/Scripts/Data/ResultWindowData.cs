using UnityEngine.Events;

namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// 最終結果ウィンドウで必要な情報のデータクラス
    /// </summary>
    public sealed class ResultWindowData
    {
        /// <summary>
        /// 完了ボタンに登録する処理
        /// </summary>
        public UnityAction completeBtnFunc { get; private set; }

        /// <summary>
        /// 最終結果ウィンドウで必要な情報のデータクラスのコンストラクタ
        /// </summary>
        /// <param name="completeBtnFunc">完了ボタンに登録する処理</param>
        public ResultWindowData(UnityAction completeBtnFunc)
        {
            this.completeBtnFunc = completeBtnFunc;
        }
    }
}