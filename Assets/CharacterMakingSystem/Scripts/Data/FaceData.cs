using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// キャラクターメイキングの顔セットのデータ
    /// </summary>
    [System.Serializable]
    public sealed class FaceData
    {
        [SerializeField, Tooltip("性別")]
        private Gender gender;

        /// <summary>
        /// 性別
        /// </summary>
        public Gender Gender => gender;
        
        [SerializeField, Tooltip("顔セットのID")]
        private int faceID;

        /// <summary>
        /// 顔セットのID
        /// </summary>
        public int FaceID => faceID;
        
        [SerializeField, Tooltip("プレファブのAddressableでのアドレス")]
        private AssetReferenceT<GameObject> address;

        /// <summary>
        /// プレファブのAddressableでのアドレス
        /// </summary>
        public AssetReferenceT<GameObject> Address => address;
        
        [SerializeField, Tooltip("素体に最初から付属している部位であればtrue")]
        private bool defaultPart;

        /// <summary>
        /// 素体に最初から付属している部位であればtrue
        /// </summary>
        public bool DefaultPart => defaultPart;

        /// <summary>
        /// キャラクターメイキングの顔セットのデータのコンストラクタ
        /// </summary>
        /// <param name="gender">性別</param>
        /// <param name="faceID">顔セットのID</param>
        /// <param name="address">プレファブのアドレス</param>
        /// <param name="defaultPart">素体に最初から付属しているかどうかのフラグ</param>
        public FaceData(Gender gender, int faceID, AssetReferenceT<GameObject> address, bool defaultPart)
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
