using System.Collections;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CharacterMakingSystem.CoreSystem
{
    /// <summary>
    /// シーン遷移周りの管理クラス
    /// </summary>
    public sealed class SceneController : ISceneController
    {
        [Inject] private ZenjectSceneLoader zenjectSceneLoader;

        /// <summary>
        /// シーン名のテーブル
        /// </summary>
        private readonly Dictionary<SceneName, string> SceneNameTable = new()
        {
            {SceneName.Stage,        "Stage"},
            {SceneName.Sex,          "SexScene"},
            {SceneName.Face,         "FaceScene"},
            {SceneName.Look,         "LookScene"},
            {SceneName.Hair,         "HairScene"},
            {SceneName.Result,       "ResultScene"}
        };

        /// <summary>
        /// 基底シーンのシーン名
        /// </summary>
        private const string SYSTEM_SCENE_NAME = "ChaMakSystem";

        /// <summary>
        /// 非同期でシーンをロードする
        /// </summary>
        /// <param name="sceneName">表示したいシーン名</param>
        /// <param name="extraBindings">追加でバインドしたいデリゲート</param>
        public async UniTask LoadSceneAsync(SceneName sceneName, Action<DiContainer> extraBindings = null) 
            => await LoadSceneAsync(SceneNameTable[sceneName], extraBindings);

        /// <summary>
        /// 非同期でシーンをロードする
        /// </summary>
        /// <param name="sceneName">表示したいシーン名</param>
        /// <param name="extraBindings">追加でバインドしたいデリゲート</param>
        public async UniTask LoadSceneAsync(string sceneName, Action<DiContainer> extraBindings = null)
        {
            if (extraBindings != null)
            {
                await zenjectSceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive, extraBindings);
            }
            else
            {
                await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }
    }
}
