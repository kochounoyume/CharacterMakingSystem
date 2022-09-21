using UnityEngine;

namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// キャラクターメイキングの髪型のデータ
    /// </summary>
    [System.Serializable]
    public sealed class HairData
    {
        /// <summary>
        /// 性別
        /// </summary>
        public Gender gender;

        /// <summary>
        /// 髪型のID
        /// </summary>
        public int hairID;

        /// <summary>
        /// プレファブが配置してあるResourcesフォルダ以下のパス
        /// </summary>
        public string filePath;

        /// <summary>
        /// ベースヘアーが必要ならtrue
        /// </summary>
        public bool baseHair;
        
        /// <summary>
        /// 素体に最初から付属している部位であればtrue
        /// </summary>
        public bool defaultPart;

        /// <summary>
        /// キャラクターメイキングの髪型のデータのコンストラクタ
        /// </summary>
        /// <param name="gender">性別</param>
        /// <param name="hairID">髪型のID</param>
        /// <param name="filePath">プレファブのファイルパス</param>
        /// <param name="baseHair">ベースヘアーが必要かどうかのフラグ</param>
        /// <param name="defaultPart">素体に最初から付属しているかどうかのフラグ</param>
        public HairData(Gender gender, int hairID, string filePath, bool baseHair, bool defaultPart)
        {
            this.gender = gender;
            this.hairID = hairID;
            this.filePath = filePath;
            this.baseHair = baseHair;
            this.defaultPart = defaultPart;
        }
    }
}