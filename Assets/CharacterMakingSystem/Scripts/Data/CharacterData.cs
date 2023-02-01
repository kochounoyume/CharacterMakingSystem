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
        /// 肌の色
        /// </summary>
        public Color skinColor { get; set; }
        
        /// <summary>
        /// 髪型
        /// </summary>
        public HairData hairData { get; set; }
        
        /// <summary>
        /// 髪の色
        /// </summary>
        public Color hairColor { get; set; }
        
        /// <summary>
        /// 瞳の色
        /// </summary>
        public Color eyeColor { get; set; }
        
        /// <summary>
        /// 顔の形
        /// </summary>
        public FaceData faceData { get; set; }
        
        /// <summary>
        /// 顔の肌の色
        /// </summary>
        public Color faceSkinColor { get; set; }

        /// <summary>
        /// キャラクターの総合データクラスのコンストラクタ
        /// </summary>
        /// <param name="gender">性別</param>
        /// <param name="bodyHeight">身長</param>
        /// <param name="skinColor">肌の色</param>
        /// <param name="hairData">髪型</param>
        /// <param name="hairColor">髪の色</param>
        /// <param name="eyeColor">瞳の色</param>
        /// <param name="faceData">顔の形</param>
        /// <param name="faceSkinColor">顔の肌の色</param>
        public CharacterData(
            Gender gender,
            float bodyHeight,
            Color skinColor,
            HairData hairData,
            Color hairColor,
            Color eyeColor,
            FaceData faceData,
            Color faceSkinColor
        )
        {
            this.gender = gender;
            this.bodyHeight = bodyHeight;
            this.skinColor = skinColor;
            this.hairData = hairData;
            this.hairColor = hairColor;
            this.eyeColor = eyeColor;
            this.faceData = faceData;
            this.faceSkinColor = faceSkinColor;
        }
    }
}
