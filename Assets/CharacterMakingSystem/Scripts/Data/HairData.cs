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
        /// <summary>
        /// 性別
        /// </summary>
        public Gender gender;

        /// <summary>
        /// 髪型のID
        /// </summary>
        public int hairID;

        /// <summary>
        /// プレファブのAddressableでのアドレス
        /// </summary>
        public AssetReferenceT<GameObject> address;

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