using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterMakingSystem.Animation
{
    /// <summary>
    /// 同じアニメーターコントローラーを使う二者のパラメーターを同期する
    /// </summary>
    public class AnimSyncManager : MonoBehaviour
    {
        [SerializeField] private Animator otherAnimator = null;

        private AnimatorControllerParameter[] parameters;

        private void Start()
        {
            parameters = otherAnimator.parameters;

            //UniRxをいれて変数監視
            //Typeとnameで条件分岐
        }
    }
}
