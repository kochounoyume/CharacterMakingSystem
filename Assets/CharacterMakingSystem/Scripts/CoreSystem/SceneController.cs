using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace CharacterMakingSystem.CoreSystem
{
    /// <summary>
    /// シーン遷移周りの管理クラス
    /// </summary>
    public sealed class SceneController : ISceneController
    {
        /// <summary>
        /// 現在アクティブなシーン名を記録管理するリスト
        /// </summary>
        private List<string> activeSceneNames = new List<string>() {"Stage"};

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
        private const string BASE_SCENE = "Stage";
        
        [Inject] private ZenjectSceneLoader zenjectSceneLoader;

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
            // 同じシーンをロードしないようにする
            if (activeSceneNames.Contains(sceneName)) return;

            if (extraBindings != null)
            {
                await zenjectSceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive, extraBindings);
            }
            else
            {
                await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
            
            activeSceneNames.Add(sceneName);
        }
        
        /// <summary>
        /// シーンを削除する
        /// </summary>
        /// <param name="sceneName">削除したいシーン名</param>
        public async UniTask UnloadSceneAsync(SceneName sceneName) => await UnloadSceneAsync(SceneNameTable[sceneName]);
        
        /// <summary>
        /// シーンを削除する
        /// </summary>
        /// <param name="sceneName">削除したいシーン名</param>
        public async UniTask UnloadSceneAsync(string sceneName)
        {
            await SceneManager.UnloadSceneAsync(sceneName);
            activeSceneNames.Remove(sceneName);
        }
    }
}
