using UnityEngine.Events;

namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// ステージウィンドウで必要な情報のデータクラス
    /// </summary>
    public sealed class StageWindowData
    {
        /// <summary>
        /// 最終的なキャラクターデータを回収する処理
        /// </summary>
        public UnityAction<CompleteCharaData> dataEntryFunc { get; private set; }

        public StageWindowData(UnityAction<CompleteCharaData> dataEntryFunc)
        {
            this.dataEntryFunc = dataEntryFunc;
        }
    }
}