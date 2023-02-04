using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CharacterMakingSystem.CoreSystem
{
    public enum SceneName
    {
        Stage,
        Sex,
        Face,
        Look,
        Hair,
        Result
    }
    
    public interface ISceneController
    {
        /// <summary>
        /// 非同期でシーンをロードする
        /// </summary>
        /// <param name="sceneName">表示したいシーン名</param>
        /// <param name="extraBindings">追加でバインドしたいデリゲート</param>
        UniTask LoadSceneAsync(SceneName sceneName, Action<DiContainer> extraBindings = null);

        /// <summary>
        /// 非同期でシーンをロードする
        /// </summary>
        /// <param name="sceneName">表示したいシーン名</param>
        /// <param name="extraBindings">追加でバインドしたいデリゲート</param>
        UniTask LoadSceneAsync(string sceneName, Action<DiContainer> extraBindings = null);

        /// <summary>
        /// シーンを削除する
        /// </summary>
        /// <param name="sceneName">削除したいシーン名</param>
        UniTask UnloadSceneAsync(SceneName sceneName);

        /// <summary>
        /// シーンを削除する
        /// </summary>
        /// <param name="sceneName">削除したいシーン名</param>
        UniTask UnloadSceneAsync(string sceneName);
    }
}
