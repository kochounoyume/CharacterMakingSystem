using System;
using UnityEngine;
using Zenject;

namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// 顔セットと髪型のデータを網羅するデータベース
    /// </summary>
    [CreateAssetMenu(menuName = "CharacterMakingSystem/ChaPartDatabase")]
    public class ChaPartDatabase : ScriptableObjectInstaller
    {
        // 顔セットのデータ郡
        [SerializeField] private FaceData[] faceDatas = null;

        // 髪型のデータ郡
        [SerializeField] private HairData[] hairDatas = null;

        /// <summary>
        /// データベースから性別とIDが合致する顔セットデータを返す
        /// </summary>
        /// <param name="gender">性別</param>
        /// <param name="faceID">顔セットのID</param>
        /// <returns></returns>
        public FaceData FindFaceData(Gender gender, int faceID)
        {
            return Array.Find(faceDatas, data => data.gender == gender && data.faceID == faceID);
        }
        
        /// <summary>
        /// データベースから性別とIDが合致する髪型データを返す
        /// </summary>
        /// <param name="gender">性別</param>
        /// <param name="hairID">髪型のID</param>
        /// <returns></returns>
        public HairData FindHairData(Gender gender, int hairID)
        {
            return Array.Find(hairDatas, data => data.gender == gender && data.hairID == hairID);
        }
        
        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}
