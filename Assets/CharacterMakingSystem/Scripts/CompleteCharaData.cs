using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CharacterMakingSystem
{
    /// <summary>
    /// 最終的なキャラクターのデータ
    /// </summary>
    [System.Serializable]
    public sealed class CompleteCharaData
    {
        /// <summary>
        /// 大きさ
        /// </summary>
        public float scale { get; private set; }
        
        /// <summary>
        /// 肌の色
        /// </summary>
        public Color skinColor { get; private set; }
        
        /// <summary>
        /// 髪の色
        /// </summary>
        public Color hairColor { get; private set; }
        
        /// <summary>
        /// 瞳の色
        /// </summary>
        public Color eyeColor { get; private set; }
        
        /// <summary>
        /// 顔の肌の色
        /// </summary>
        public Color faceSkinColor { get; private set; }
        
        /// <summary>
        /// 髪プレファブのAddressableでのアドレス
        /// </summary>
        public AssetReferenceT<GameObject> hairAddress { get; private set; }

        /// <summary>
        /// ベースヘアーが必要ならtrue
        /// </summary>
        public bool baseHair { get; private set; }
        
        /// <summary>
        /// 素体に最初から付属している髪部位であればtrue
        /// </summary>
        public bool defaultHairPart { get; private set; }
        
        /// <summary>
        /// 顔プレファブのAddressableでのアドレス
        /// </summary>
        public AssetReferenceT<GameObject> faceAddress { get; private set; }

        /// <summary>
        /// 素体に最初から付属している顔部位であればtrue
        /// </summary>
        public bool defaultFacePart { get; private set; }

        /// <summary>
        /// 最終的なキャラクターのデータのコンストラクタ
        /// </summary>
        /// <param name="scale">大きさ</param>
        /// <param name="skinColor">肌の色</param>
        /// <param name="hairColor">髪の色</param>
        /// <param name="eyeColor">瞳の色</param>
        /// <param name="faceSkinColor">顔の肌の色</param>
        /// <param name="hairAddress">髪プレファブのAddressableでのアドレス</param>
        /// <param name="baseHair">ベースヘアーが必要ならtrue</param>
        /// <param name="defaultHairPart">素体に最初から付属している髪部位であればtrue</param>
        /// <param name="faceAddress">顔プレファブのAddressableでのアドレス</param>
        /// <param name="defaultFacePart">素体に最初から付属している顔部位であればtrue</param>
        public CompleteCharaData(
            float scale,
            Color skinColor,
            Color hairColor,
            Color eyeColor,
            Color faceSkinColor,
            AssetReferenceT<GameObject> hairAddress,
            bool baseHair,
            bool defaultHairPart,
            AssetReferenceT<GameObject> faceAddress,
            bool defaultFacePart)
        {
            this.scale = scale;
            this.skinColor = skinColor;
            this.hairColor = hairColor;
            this.eyeColor = eyeColor;
            this.faceSkinColor = faceSkinColor;
            this.hairAddress = hairAddress;
            this.baseHair = baseHair;
            this.defaultHairPart = defaultHairPart;
            this.faceAddress = faceAddress;
            this.defaultFacePart = defaultFacePart;
        }
    }
}