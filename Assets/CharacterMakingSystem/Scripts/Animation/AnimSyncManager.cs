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
        // 同期のターゲットとなるアニメーター
        [SerializeField] private Animator otherAnimator = null;

        // trueにすると自動でotherAnimatorにPlayerのタグのゲームオブジェクトのAnimatorを取得する
        [SerializeField] private bool isFindPlayer = true;

        private const string PLAYER = "Player";

        private void Start()
        {
            // 同期するアニメーターを取得
            var animator = GetComponent<Animator>();
            
            // PlayerのAnimatorを取得
            if (isFindPlayer)
            {
                otherAnimator = GameObject.FindWithTag(PLAYER).GetComponent<Animator>();
                animator.runtimeAnimatorController = otherAnimator.runtimeAnimatorController;
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
    }
}
