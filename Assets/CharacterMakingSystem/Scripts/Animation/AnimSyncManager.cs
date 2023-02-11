using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace CharacterMakingSystem.Animation
{
    /// <summary>
    /// 同じアニメーターコントローラーを使う二者のパラメーターを同期する
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimSyncManager : MonoBehaviour
    {
        [SerializeField, Tooltip("同期のターゲットとなるアニメーター")] 
        private Animator otherAnimator = null;
        
        [SerializeField, Tooltip("自動でotherAnimatorにPlayerのタグのゲームオブジェクトのAnimatorを取得するかどうかのフラグ変数")]
        private bool isFindPlayer = true;

        /// <summary>
        /// 同期するアニメーター
        /// </summary>
        private Animator animator = null;

        /// <summary>
        /// 全同期対象のアニメーターを管理するリスト
        /// </summary>
        private static List<Animator> syncAnimators = new List<Animator>();

        /// <summary>
        /// 該当のアニメーションコントローラーレイヤー
        /// </summary>
        private const int LAYER_INDEX = 0;
        
        /// <summary>
        /// プレイヤーのタグ
        /// </summary>
        private const string PLAYER = "Player";

        private void Start()
        {
            // 同期するアニメーターを取得
            animator = GetComponent<Animator>();

            // PlayerのAnimatorを取得
            if (isFindPlayer)
            {
                var playerObj = GameObject.FindWithTag(PLAYER);
                if (playerObj == null || !playerObj.TryGetComponent(out otherAnimator)) return;
            }

            animator.runtimeAnimatorController = otherAnimator.runtimeAnimatorController;
            syncAnimators.Add(animator);

            var info = otherAnimator.GetCurrentAnimatorStateInfo(LAYER_INDEX);
            var nameHash = info.fullPathHash;
            var normalizedTime = info.normalizedTime % 1; // ループしていると1以上になるので、小数点以下のみ抽出

            foreach (var syncAnimator in syncAnimators)
            {
                syncAnimator.Play(nameHash, LAYER_INDEX, normalizedTime);
                //syncAnimator.Rebind();
            }

            // アニメーションによってはAnimationEventを吐いてエラーになることがあるので無効化する
            animator.fireEvents = false;
            
            // ターゲットのアニメーターのパラメーターを監視して変更があれば同期対象の方でも変更する
            foreach (var parameter in animator.parameters)
            {
                switch (parameter.type)
                {
                    case AnimatorControllerParameterType.Bool:
                        otherAnimator
                            .ObserveEveryValueChanged(_ => _.GetBool(parameter.nameHash))
                            .Where(_ => _!=animator.GetBool(parameter.nameHash))
                            .Subscribe(_ => animator.SetBool(parameter.nameHash, _))
                            .AddTo(this.gameObject);
                        break;
                    
                    case AnimatorControllerParameterType.Float:
                        otherAnimator
                            .ObserveEveryValueChanged(_ => _.GetFloat(parameter.nameHash))
                            .Where(_ => Mathf.Abs(_ - animator.GetFloat(parameter.nameHash)) > 0)
                            .Subscribe(_ => animator.SetFloat(parameter.nameHash, _))
                            .AddTo(this.gameObject);
                        break;
                    
                    case AnimatorControllerParameterType.Int:
                        otherAnimator
                            .ObserveEveryValueChanged(_ => _.GetInteger(parameter.nameHash))
                            .Where(_ => _ != animator.GetInteger(parameter.nameHash))
                            .Subscribe(_ => animator.SetInteger(parameter.nameHash, _))
                            .AddTo(this.gameObject);
                        break;
                    
                    // Triggerでまともに動くかは検証不足だが「Triggerは派手なBool」らしいからいけるんじゃない？
                    // https://forum.unity.com/threads/get-if-trigger-is-set-or-is-a-condition-of-a-state.1004269/
                    case AnimatorControllerParameterType.Trigger:
                        otherAnimator
                            .ObserveEveryValueChanged(_ => _.GetBool(parameter.nameHash))
                            .Where(_ => _ != animator.GetBool(parameter.nameHash))
                            .Subscribe(_ => animator.SetTrigger(parameter.nameHash))
                            .AddTo(this.gameObject);
                        break;
                    
                    default:
                        Debug.LogError($"パラメータのタイプが異常です \n AnimatorControllerParameterType.{parameter.type}");
                        break;
                } 
            }
        }

        private void OnDestroy() => syncAnimators.Remove(animator);
    }
}
