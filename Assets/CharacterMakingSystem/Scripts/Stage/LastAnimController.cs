using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace CharacterMakingSystem.Stage
{
    /// <summary>
    /// 最終部分のアニメーション部分を制御するクラス
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class LastAnimController : MonoBehaviour
    {
        [SerializeField, Tooltip("キャラクターの歩く速度")]
        private float speed = 5.0f;

        [SerializeField, Tooltip("移動時間")] 
        private float duration = 10f;

        [SerializeField, Tooltip("ポストプロセッシング")]
        private PostProcessVolume volume = null;

        [SerializeField, Tooltip("ホワイトアウト専用Image")]
        private Image image = null;
        
        /// <summary>
        /// Transformコンポーネント
        /// </summary>
        private Transform myTrans = null;
        
        /// <summary>
        /// アニメーターコンポーネント
        /// </summary>
        private Animator animator = null;
        
        /// <summary>
        /// アニメーション制御パラメータのID
        /// </summary>
        private int animIDSpeed = default;

        /// <summary>
        /// ポストプロセッシングのブルーム
        /// </summary>
        private Bloom bloom = null;

        void Start()
        {
            myTrans = transform;
            animator = GetComponent<Animator>();
            animIDSpeed = Animator.StringToHash("Speed");
            bloom = volume.profile.GetSetting<Bloom>();
        }

        public async UniTask StartAnim()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            animator.SetFloat(animIDSpeed, 2);
            myTrans.DOMove(myTrans.position +myTrans.forward * speed, duration);
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            // ポストプロセッシングで最後画面が真っ白になって終わる
            await DOTween.To(
                getter: () => bloom.intensity,
                setter: value => bloom.intensity.Override(value),
                endValue: 70.0f,
                duration: 5.0f);
            await image.DOFade(1, 5).OnComplete(() =>
            {
                Destroy(image.gameObject);
                Destroy(myTrans.gameObject);
            });
        }
    }
}
