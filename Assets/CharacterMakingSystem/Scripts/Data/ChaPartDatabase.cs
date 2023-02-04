using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CharacterMakingSystem.Data
{
    /// <summary>
    /// 顔セットと髪型のデータを網羅するデータベース
    /// </summary>
    [CreateAssetMenu(menuName = "CharacterMakingSystem/ChaPartDatabase")]
    public class ChaPartDatabase : ScriptableObjectInstaller
    {
        [SerializeField, Tooltip("顔セットのデータ郡")] 
        private FaceData[] faceDatas = null;
        
        [SerializeField, Tooltip("髪型のデータ郡")]
        private HairData[] hairDatas = null;

        [SerializeField, Tooltip("男性用コスチュームアドレス群")]
        private AssetReferenceT<GameObject>[] maleCostume = Array.Empty<AssetReferenceT<GameObject>>();

        [SerializeField, Tooltip("女性用コスチュームアドレス群")]
        private AssetReferenceT<GameObject>[] femaleCostume = Array.Empty<AssetReferenceT<GameObject>>();

        /// <summary>
        /// データベースから性別とIDが合致する顔セットデータを返す
        /// </summary>
        /// <param name="gender">性別</param>
        /// <param name="faceID">顔セットのID</param>
        /// <returns>顔セットデータ</returns>
        public FaceData FindFaceData(Gender gender, int faceID) 
            => Array.Find(faceDatas, data => data.Gender == gender && data.FaceID == faceID);

        /// <summary>
        /// データベースから性別が合致する顔セットのデフォルトデータを返す
        /// </summary>
        /// <param name="gender">性別</param>
        /// <returns>顔セットのデフォルトデータ</returns>
        public FaceData FindDefaultFaceData(Gender gender) 
            => Array.Find(faceDatas, data => data.Gender == gender && data.DefaultPart);
 
        /// <summary>
        /// データベースから性別とIDが合致する髪型データを返す
        /// </summary>
        /// <param name="gender">性別</param>
        /// <param name="hairID">髪型のID</param>
        /// <returns>髪型データ</returns>
        public HairData FindHairData(Gender gender, int hairID)
            => Array.Find(hairDatas, data => data.Gender == gender && data.HairID == hairID);

        /// <summary>
        /// データベースから性別が合致する髪型のデフォルトデータを返す
        /// </summary>
        /// <param name="gender">性別</param>
        /// <returns>髪型のデフォルトデータ</returns>
        public HairData FindDefaultHairData(Gender gender) 
            => Array.Find(hairDatas, data => data.Gender == gender && data.DefaultPart);

        /// <summary>
        /// データベースから性別が合致するコスチュームのデフォルトデータを返す
        /// </summary>
        /// <param name="gender">性別</param>
        /// <returns>コスチュームのアドレス</returns>
        public AssetReferenceT<GameObject> GetDefaultCos(Gender gender) 
            => gender == Gender.Male ? maleCostume.FirstOrDefault() : femaleCostume.FirstOrDefault();

        public override void InstallBindings() => Container.BindInstance(this);
    }
}
