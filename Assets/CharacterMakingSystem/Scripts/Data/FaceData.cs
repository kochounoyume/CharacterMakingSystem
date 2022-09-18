namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// キャラクターメイキングの顔セットのデータ
    /// </summary>
    [System.Serializable]
    public sealed class FaceData
    {
        /// <summary>
        /// 性別
        /// </summary>
        public Gender gender;
        
        /// <summary>
        /// 顔セットのID
        /// </summary>
        public int faceID;
        
        /// <summary>
        /// プレファブが配置してあるResourcesフォルダ以下のパス
        /// </summary>
        public string filePath;
        
        /// <summary>
        /// 首を伸ばす調整が必要な場合のボーンの調整値
        /// </summary>
        public float stretchValue;

        /// <summary>
        /// キャラクターメイキングの顔セットのデータのコンストラクタ
        /// </summary>
        /// <param name="gender">性別</param>
        /// <param name="faceID">顔セットのID</param>
        /// <param name="filePath">プレファブのファイルパス</param>
        /// <param name="stretchValue">ボーンの調整値</param>
        public FaceData(Gender gender, int faceID, string filePath, float stretchValue)
        {
            this.gender = gender;
            this.faceID = faceID;
            this.filePath = filePath;
            this.stretchValue = stretchValue;
        }
    }

    /// <summary>
    /// 性別
    /// </summary>
    public enum Gender
    {
        Male = 0,
        Female = 1
    }
}
