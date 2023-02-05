using System;
using System.Collections.Generic;
using System.Linq;
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
        /// シーン名のテーブル
        /// </summary>
        private readonly Dictionary<SceneName, string> sceneNameTable = new()
        {
            {SceneName.Sex, "SexScene"},
            {SceneName.Face, "FaceScene"},
            {SceneName.Look, "LookScene"},
            {SceneName.Hair, "HairScene"},
            {SceneName.Result, "ResultScene"}
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
            => await LoadSceneAsync(sceneNameTable[sceneName], extraBindings);

        /// <summary>
        /// 非同期でシーンをロードする
        /// </summary>
        /// <param name="sceneName">表示したいシーン名</param>
        /// <param name="extraBindings">追加でバインドしたいデリゲート</param>
        public async UniTask LoadSceneAsync(string sceneName, Action<DiContainer> extraBindings = null)
        {
            // 同じシーンをロードしないようにする
            if (GetActiveScenes().Contains(sceneName)) return;

            if (extraBindings != null)
            {
                await zenjectSceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive, extraBindings);
            }
            else
            {
                await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }
        
        /// <summary>
        /// シーンを削除する
        /// </summary>
        /// <param name="sceneName">削除したいシーン名</param>
        public async UniTask UnloadSceneAsync(SceneName sceneName) => await UnloadSceneAsync(sceneNameTable[sceneName]);
        
        /// <summary>
        /// シーンを削除する
        /// </summary>
        /// <param name="sceneName">削除したいシーン名</param>
        public async UniTask UnloadSceneAsync(string sceneName) => await SceneManager.UnloadSceneAsync(sceneName);

        /// <summary>
        /// アクティブなシーン名を全取得
        /// </summary>
        /// <returns>シーン名文字列</returns>
        public string[] GetActiveScenes()
        {
            var length = SceneManager.sceneCount;
            var result = new string[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = SceneManager.GetSceneAt(i).name;
            }
            return result;
        }

        /// <summary>
        /// アクティブなシーンのうち、sceneNameTableに登録してあるものを全取得する
        /// </summary>
        /// <returns>シーン名文字列</returns>
        public string[] GetActiveSceneNames() 
            => GetActiveScenes().Where(scene => sceneNameTable.ContainsValue(scene)).ToArray();

        /// <summary>
        /// 任意のシーン以外のテーブルに登録されているシーンを返す
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        /// <returns>指定のシーン以外のシーン名群</returns>
        public string[] FindOtherSceneNames(SceneName sceneName) => FindOtherSceneNames(sceneNameTable[sceneName]);
        
        /// <summary>
        /// 任意のシーン以外のテーブルに登録されているシーンを返す
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        /// <returns>指定のシーン以外のシーン名群</returns>
        public string[] FindOtherSceneNames(string sceneName) 
            => (from pair in sceneNameTable where pair.Value != sceneName select pair.Value).ToArray();
    }
}
