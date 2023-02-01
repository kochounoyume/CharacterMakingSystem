using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// キャラクターメイキングの髪型のデータ
    /// </summary>
    [System.Serializable]
    public sealed class HairData
    {
        [SerializeField, Tooltip("性別")]
        private Gender gender;

        /// <summary>
        /// 性別
        /// </summary>
        public Gender Gender => gender;

        [SerializeField, Tooltip("髪型のID")]
        private int hairID;

        /// <summary>
        /// 髪型のID
        /// </summary>
        public int HairID => hairID;
        
        [SerializeField, Tooltip("プレファブのAddressableでのアドレス")]
        private AssetReferenceT<GameObject> address;

        /// <summary>
        /// プレファブのAddressableでのアドレス
        /// </summary>
        public AssetReferenceT<GameObject> Address => address;

        [SerializeField, Tooltip("ベースヘアーが必要ならtrue")]
        private bool baseHair;

        /// <summary>
        /// ベースヘアーが必要ならtrue
        /// </summary>
        public bool BaseHair => baseHair;
        
        [SerializeField, Tooltip("素体に最初から付属している部位であればtrue")]
        private bool defaultPart;

        /// <summary>
        /// 素体に最初から付属している部位であればtrue
        /// </summary>
        public bool DefaultPart => defaultPart;

        /// <summary>
        /// キャラクターメイキングの髪型のデータのコンストラクタ
        /// </summary>
        /// <param name="gender">性別</param>
        /// <param name="hairID">髪型のID</param>
        /// <param name="address">プレファブのAddressableでのアドレス</param>
        /// <param name="baseHair">ベースヘアーが必要かどうかのフラグ</param>
        /// <param name="defaultPart">素体に最初から付属しているかどうかのフラグ</param>
        public HairData(Gender gender, int hairID, AssetReferenceT<GameObject> address, bool baseHair, bool defaultPart)
        {
            this.gender = gender;
            this.hairID = hairID;
            this.address = address;
            this.baseHair = baseHair;
            this.defaultPart = defaultPart;
        }
    }
}