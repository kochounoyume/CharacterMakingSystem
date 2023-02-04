using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// キャラクターの総合データ
    /// </summary>
    public sealed class CharacterData
    {
        /// <summary>
        /// 性別
        /// </summary>
        public Gender gender { get; set; }

        /// <summary>
        /// 身長
        /// </summary>
        public float bodyHeight { get; set; }
        
        /// <summary>
        /// 肌の色（デフォルト R:250 G:235 B:222）
        /// </summary>
        public Color skinColor { get; set; }
        
        /// <summary>
        /// 髪型
        /// </summary>
        public HairData hairData { get; set; }
        
        /// <summary>
        /// 髪の色（デフォルト R:130 G:90 B:70）
        /// </summary>
        public Color hairColor { get; set; }

        /// <summary>
        /// 瞳の色（デフォルト R:158 G:98 B:51）
        /// </summary>
        public Color eyeColor { get; set; }
        
        /// <summary>
        /// 顔の形
        /// </summary>
        public FaceData faceData { get; set; }
        
        /// <summary>
        /// 顔の肌の色（デフォルト R:250 G:235 B:222）
        /// </summary>
        public Color faceSkinColor { get; set; }
        
        private static readonly Color defaultSkinColor = new Color(250 / 255, 235 / 255, 222 / 255);
        private static readonly Color defaultHairColor = new Color(130 / 255, 90 / 255, 70 / 255);
        private static readonly Color defaultEyeColor = new Color(158 / 255, 98 / 255, 51 / 255);

        /// <summary>
        /// キャラクターの総合データクラスのコンストラクタ
        /// </summary>
        /// <param name="gender">性別</param>
        /// <param name="hairData">髪型</param>
        /// <param name="faceData">顔の形</param>
        /// <param name="bodyHeight">身長</param>
        /// <param name="skinColor">肌の色</param>
        /// <param name="eyeColor">瞳の色</param>
        /// <param name="hairColor">髪の色</param>
        /// <param name="faceSkinColor">顔の肌の色</param>
        public CharacterData(
            Gender gender,
            HairData hairData,
            FaceData faceData,
            float bodyHeight = 0.5f,
            Color? skinColor = null,
            Color? eyeColor = null,
            Color? hairColor = null,
            Color? faceSkinColor = null
            )
        {
            this.gender = gender;
            this.hairData = hairData;
            this.faceData = faceData;
            this.bodyHeight = bodyHeight;
            this.skinColor = skinColor ?? defaultSkinColor;
            this.hairColor = hairColor ?? defaultHairColor;
            this.eyeColor = eyeColor ?? defaultEyeColor;
            this.faceSkinColor = faceSkinColor ?? defaultSkinColor;
        }

        /// <summary>
        /// RawBody使用時のアドレス重複検証
        /// </summary>
        /// <returns>trueなら重複している</returns>
        public bool IsOverlap() => this.hairData.Address == this.faceData.Address;
    }
}
