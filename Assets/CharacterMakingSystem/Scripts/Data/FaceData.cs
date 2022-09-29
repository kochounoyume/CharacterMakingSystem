using UnityEngine.AddressableAssets;

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
        /// プレファブのAddressableでのアドレス
        /// </summary>
        public AssetReference address;
        
        /// <summary>
        /// 素体に最初から付属している部位であればtrue
        /// </summary>
        public bool defaultPart;

        /// <summary>
        /// キャラクターメイキングの顔セットのデータのコンストラクタ
        /// </summary>
        /// <param name="gender">性別</param>
        /// <param name="faceID">顔セットのID</param>
        /// <param name="address">プレファブのアドレス</param>
        /// <param name="defaultPart">素体に最初から付属しているかどうかのフラグ</param>
        public FaceData(Gender gender, int faceID, AssetReference address, bool defaultPart)
        {
            this.gender = gender;
            this.faceID = faceID;
            this.address = address;
            this.defaultPart = defaultPart;
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
