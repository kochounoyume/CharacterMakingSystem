using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharacterMakingSystem.UI
{
    /// <summary>
    /// FillAmountを使用したSlider
    /// </summary>
    [RequireComponent(typeof(Slider), typeof(RectTransform))]
    public class BeautifulSlider : MonoBehaviour
    {
        private const string FILL = "Fill";
        private const string HANDLE = "Handle";
        private const string AREA = "Handle Slide Area";

        [SerializeField] private RectTransform rectTransform = null;
        [SerializeField] private Slider slider = null;
        [SerializeField] private Image fillAmount = null;

        // ゲーム起動時
        private void Start() => SetBeautifulSlider();
        
        // コンポーネント追加時
        private void Reset() => SetBeautifulSlider();
        
        // スクリプトロード時やインスペクターの値変更時
        private void OnValidate() => SetBeautifulSlider();

        /// <summary>
        /// 必要なものの設定をするメソッド
        /// </summary>
        private void SetBeautifulSlider()
        {
            // RectTransform周りの実装
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
                rectTransform.anchoredPosition = Vector2.zero;
                rectTransform.sizeDelta = new Vector2(171, 15);
            }
            
            // Image(fillAmount)周りの実装："Fill"子オブジェクト
            fillAmount ??= CheckUI<Image>(FILL, transform, fillRect =>
            {
                fillRect.anchorMin = new Vector2(0, 0.5f);
                fillRect.anchorMax = new Vector2(1, 0.5f);
                fillRect.offsetMax = Vector2.zero;
                fillRect.offsetMin = Vector2.zero;
                fillRect.sizeDelta = new Vector2(0, 8);
            });
            
            if (fillAmount.type != Image.Type.Filled)
            {
                fillAmount.type = Image.Type.Filled;
            }
            if (fillAmount.fillMethod != Image.FillMethod.Horizontal)
            {
                fillAmount.fillMethod = Image.FillMethod.Horizontal;
            }
            fillAmount.sprite ??= GetBuiltinResource(DefaultSpritePath.StandardSpritePath);
            
            // Slider周りの実装
            slider ??= GetComponent<Slider>();
            if (slider.targetGraphic == null)
            {
                if (slider.handleRect == null)
                {
                    // ハンドルの可動範囲用のオブジェクトも作成する
                    CheckUI<RectTransform>(AREA, transform, areaRect =>
                    {
                        // 親オブジェクトにStretchするようにサイズを変更
                        areaRect.anchorMin = Vector2.zero;
                        areaRect.anchorMax = Vector2.one;
                        areaRect.offsetMin = new Vector2(7.5f, 0);
                        areaRect.offsetMax = new Vector2(-7.5f, 0);
                        
                        var image = CheckUI<Image>(HANDLE, areaRect, rect =>
                        {
                            rect.offsetMin = Vector2.zero;
                            rect.offsetMax = Vector2.zero;
                            rect.anchoredPosition = new Vector2(0, rect.anchoredPosition.y);
                            rect.sizeDelta = new Vector2(15f, 15f);
                            slider.handleRect = rect;
                        });
                        image.sprite = GetBuiltinResource(DefaultSpritePath.KnobPath);
                        slider.targetGraphic = image;
                    });
                }
                else
                {
                    slider.targetGraphic = slider.handleRect.TryGetComponent(out Image targetGraphic) ? targetGraphic : slider.handleRect.gameObject.AddComponent<Image>();
                }
            }
            else
            {
                slider.handleRect ??= slider.targetGraphic.rectTransform;
            }
            
            // イベントが重複していたら削除して登録(削除対象がなくてもエラーにならないようなので消し得)
            void Action(float sliderValue) => fillAmount.fillAmount = sliderValue;
            slider.onValueChanged.RemoveListener(Action);
            slider.onValueChanged.AddListener(Action);
        }

        /// <summary>
        /// 指定のUIがあるかを判定し、なければ不足部分をセットする
        /// </summary>
        /// <param name="objName">UIのゲームオブジェクト名</param>
        /// <param name="parent">指定のUIを配置する親オブジェクト</param>
        /// <param name="callback">RectTransformを用いた処理が可能なコールバック</param>
        /// <typeparam name="TObject">UIの種類(コンポーネント)</typeparam>
        /// <returns></returns>
        private TObject CheckUI<TObject>(string objName, Transform parent, UnityAction<RectTransform> callback = null) where TObject : UnityEngine.Component
        {
            var objTra = transform.Find(objName);
            TObject result = null;
            RectTransform objRecTra = null;

            if (objTra == null)
            {
                var obj = new GameObject(objName, typeof(TObject));
                result = obj.GetComponent<TObject>();
                obj.transform.SetParent(parent);
                objRecTra = obj.GetComponent<RectTransform>();
            }
            else
            {
                if (!objTra.TryGetComponent(out objRecTra))
                {
                    objRecTra = objTra.gameObject.AddComponent<RectTransform>();
                }
                result = objTra.GetComponent<TObject>();
                result ??= objTra.gameObject.AddComponent<TObject>();
            }

            callback?.Invoke(objRecTra);
            return result;
        }

        private enum DefaultSpritePath
        {
            StandardSpritePath,
            BackgroundSpritePath,
            InputFieldBackgroundPath,
            KnobPath,
            CheckmarkPath,
            DropdownArrowPath,
            MaskPath
        }
        
        // keyをenumではなく、intにキャストしたenumを使うと速い
        private static readonly Dictionary<int, string> defSpritePathDictionary = new Dictionary<int, string>()
        {
            {(int)DefaultSpritePath.StandardSpritePath,       "UI/Skin/UISprite.psd"},
            {(int)DefaultSpritePath.BackgroundSpritePath,     "UI/Skin/Background.psd"},
            {(int)DefaultSpritePath.InputFieldBackgroundPath, "UI/Skin/InputFieldBackground.psd"},
            {(int)DefaultSpritePath.KnobPath,                 "UI/Skin/Knob.psd"},
            {(int)DefaultSpritePath.CheckmarkPath,            "UI/Skin/Checkmark.psd"},
            {(int)DefaultSpritePath.DropdownArrowPath,        "UI/Skin/DropdownArrow.psd"},
            {(int)DefaultSpritePath.MaskPath,                 "UI/Skin/UIMask.psd"},
        };

        /// <summary>
        /// デフォルトのスプライトをリソースから取得するメソッド
        /// </summary>
        /// <param name="defaultSpritePath">列挙体でリソースのスプライトのパスを指定</param>
        /// <returns></returns>
        private Sprite GetBuiltinResource(DefaultSpritePath defaultSpritePath)
        {
            #if UNITY_EDITOR
            var result = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>(defSpritePathDictionary[(int)defaultSpritePath]);
            #else
            var result = Resources.GetBuiltinResource<Sprite>(defSpritePathDictionary[(int)defaultSpritePath]);
            #endif

            return result;
        }
    }
}