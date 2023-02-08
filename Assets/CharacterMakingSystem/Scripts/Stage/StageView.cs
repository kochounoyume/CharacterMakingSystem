using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CharacterMakingSystem.Stage
{
    /// <summary>
    /// 実際に3Dゲーム空間を扱うビュークラス
    /// </summary>
    public class StageView : MonoBehaviour
    {
        [SerializeField, Tooltip("キャラクターの親オブジェクト：部位生成および身長変更で使用")]
        private Transform playerArmature = null;

        [SerializeField, Tooltip("コスチューム")] 
        private GameObject costumeBody = null;

        [SerializeField, Tooltip("顔")] 
        private GameObject face = null;

        [SerializeField, Tooltip("髪")] 
        private GameObject hair = null;

        /// <summary>
        /// 肌の色変更用マテリアル
        /// </summary>
        private Material skinColor = null;

        /// <summary>
        /// 髪の色変更用マテリアル（後髪あるかもなので配列）
        /// </summary>
        private Material[] hairColors = null;

        /// <summary>
        /// 瞳の色変更用マテリアル
        /// </summary>
        private Material eyeColor = null;

        /// <summary>
        /// 顔の肌の色変更用マテリアル
        /// </summary>
        private Material faceSkinColor = null;

        private const string BODY = "Body";
        private const string FACE = "Face";
        private const string HAIR = "Hair";

        private const string SKINBODY = "N00_000_00_Body_00_SKIN";
        private const string HAIRBACK = "N00_000_00_HairBack_00_HAIR";
        private const string HAIRMAIN = "N00_000_Hair_00_HAIR";
        private const string EYE = "N00_000_00_EyeIris_00_EYE";
        private const string SKINFACE = "N00_000_00_Face_00_SKIN";

        public enum Part { CostumeBody, Face, Hair }

        // Start is called before the first frame update
        void Start()
        {
            SetParameter(costumeBody, face, hair);
        }

        /// <summary>
        /// 必要な取得要素を設定する（一部だけ更新することも可能）
        /// </summary>
        /// <param name="bodyObj">costumeBody</param>
        /// <param name="faceObj">face</param>
        /// <param name="hairObj">hair</param>
        private void SetParameter(GameObject bodyObj, GameObject faceObj, GameObject hairObj)
        {
            var bodyTrans = bodyObj != null ? bodyObj.transform.Find(BODY) : null;
            var faceTrans = faceObj != null ? faceObj.transform.Find(FACE) : null;
            var hairTrans = hairObj != null ? hairObj.transform.Find(HAIR) : null;
            
            var bodyMats = bodyTrans != null && bodyTrans.TryGetComponent(out SkinnedMeshRenderer bodyRenderer)
                ? bodyRenderer.materials
                : null;
            var faceMats = faceTrans != null && faceTrans.TryGetComponent(out SkinnedMeshRenderer faceRenderer)
                ? faceRenderer.materials
                : null;
            var hairMats = hairTrans != null && hairTrans.TryGetComponent(out SkinnedMeshRenderer hairRenderer)
                ? hairRenderer.materials
                : null;

            var skins = FindMat(SKINBODY, bodyMats);
            skinColor = skins != Array.Empty<Material>() ? skins.FirstOrDefault() : skinColor;
            var hairList = FindMat(HAIRMAIN, hairMats).Union(FindMat(HAIRBACK, bodyMats)).ToList();
            hairList.RemoveAll(element => element == null);
            if (hairColors == null || (bodyTrans !=null && hairTrans != null))
            {
                hairColors = hairList.ToArray();
            }
            //後で部分更新したい場合haircolorにhairlistをマージする
            else if(bodyTrans !=null || hairTrans != null)
            {
                for (var i = 0; i < hairColors.Length; i++)
                {
                    foreach (var hairMat in hairList.Where(hairMat => hairColors[i].name == hairMat.name))
                    {
                        hairColors[i] = hairMat;
                        continue;
                    }
                }
            }
            var eyes = FindMat(EYE, faceMats);
            eyeColor = eyes != Array.Empty<Material>() ? eyes.FirstOrDefault() : eyeColor;
            var faceSkins = FindMat(SKINFACE, faceMats);
            faceSkinColor = faceSkins != Array.Empty<Material>() ? faceSkins.FirstOrDefault() : faceSkinColor;

            // 指定の文字列が一致する、あるいは含まれているマテリアルを返す（第二引数nullならnullを返す）
            IEnumerable<Material> FindMat(string matName, IEnumerable<Material> materials)
                => materials != null ? materials.Where(mat => mat.name.Contains(matName)) : Array.Empty<Material>();
        }

        /// <summary>
        /// 生成し、指定の部位として上書きする
        /// </summary>
        /// <param name="prefab">生成したいプレファブ</param>
        /// <param name="part">該当部位</param>
        /// <param name="isRawBody">hairとfaceに同じオブジェクトを登録したいときtrue</param>
        public void InstantiateObj(GameObject prefab, Part part, bool isRawBody = false)
        {
            switch (part)
            {
                case Part.CostumeBody:
                    SetObj(ref costumeBody, BODY);
                    break;
                
                case Part.Face:
                    SetObj(ref face, FACE);
                    break;
                
                case Part.Hair:
                    SetObj(ref hair, HAIR);
                    break;
                
                default:
                    Debug.LogError("エラー発生");
                    break;
            }

            void SetObj(ref GameObject obj, string partChild)
            {
                // rawbody使っていた場合、部位二つ持っているはずなので、間違って削除しないように対策
                var isRawBodyCheck = new List<bool>(){obj == costumeBody, obj == face, obj == hair};
                if (isRawBodyCheck.Count(_ => _) > 1)
                {
                    var child = obj.transform.Find(partChild);
                    if (child != null)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                else
                {
                    Destroy(obj);
                }
                obj = Instantiate(prefab, playerArmature);
                if (isRawBody)
                {
                    if (obj == hair)
                    {
                        Destroy(face);
                        face = obj;
                    }
                    else if(obj == face)
                    {
                        Destroy(hair);
                        hair = obj;
                    }
                }
                else // 単体部位で生成時
                {
                    var nonChild = obj.transform.Find(partChild == HAIR ? FACE : HAIR);
                    if (nonChild != null)
                    {
                        nonChild.gameObject.SetActive(false);
                    }
                }
                // もし重複しているプロパティがあっても、しっかり機能するはず
                SetParameter(obj == costumeBody ? obj : null, obj == face ? obj : null, obj == hair ? obj : null);
            }
        }

        /// <summary>
        /// プレイヤーの全体的な大きさを変更する
        /// </summary>
        /// <param name="scale">拡大比率（0~1）</param>
        public void SetScale(float scale)
        {
            var trueScale = ScaleConvert(scale);
            playerArmature.localScale = scale is > 0 and < 1 ? new Vector3(trueScale, trueScale, trueScale) : playerArmature.localScale;
        }

        /// <summary>
        /// プレイヤーの全体的な大きさが0になると大変なので、事前に設定した変更可能範囲内になるように数値を変換する
        /// </summary>
        /// <param name="target">変換する数値</param>
        /// <param name="min">変更可能範囲の最小値</param>
        /// <param name="max">変更可能範囲の最大値</param>
        /// <returns>変換後の数値</returns>
        private float ScaleConvert(float target, float min = 0.6f, float max = 1.4f) => min + (max - min) * target;

        /// <summary>
        /// 肌の色を変更する
        /// </summary>
        /// <param name="color"></param>
        public void SetSkinColor(Color color) => skinColor.color = color;

        /// <summary>
        /// 肌の色を取得する
        /// </summary>
        /// <returns></returns>
        public Color GetSkinColor() => skinColor.color;

        /// <summary>
        /// 髪の色を変更する(後ろ髪があれば身体の部位が必要なことに注意)
        /// </summary>
        /// <param name="color"></param>
        public void SetHairColor(Color color)
        {
            foreach (var hairColor in hairColors)
            {
                hairColor.color = color;
            }
        }

        /// <summary>
        /// 髪の色を取得する
        /// </summary>
        /// <returns></returns>
        public Color GetHairColor() => hairColors.FirstOrDefault()!.color;

        /// <summary>
        /// 瞳の色を変更する
        /// </summary>
        /// <param name="color"></param>
        public void SetEyeColor(Color color) => eyeColor.color = color;

        /// <summary>
        /// 瞳の色を取得する
        /// </summary>
        /// <returns></returns>
        public Color GetEyeColor() => eyeColor.color;

        /// <summary>
        /// 顔の肌の色を変更する
        /// </summary>
        /// <param name="color"></param>
        public void SetFaceSkinColor(Color color) => faceSkinColor.color = color;

        /// <summary>
        /// 顔の肌の色を取得する
        /// </summary>
        /// <returns></returns>
        public Color GetFaceSkinColor() => faceSkinColor.color;
    }
}
