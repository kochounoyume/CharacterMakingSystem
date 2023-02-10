using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CharacterMakingSystem.CoreSystem
{
    public enum SceneName
    {
        None,
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
        /// 非同期で基底シーンをロードする
        /// </summary>
        /// <param name="extraBindings">追加でバインドしたいデリゲート</param>
        UniTask LoadBaseSceneAsync(Action<DiContainer> extraBindings = null);

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

        /// <summary>
        /// アクティブなシーン名を全取得
        /// </summary>
        /// <returns>シーン名文字列</returns>
        string[] GetActiveScenes();

        /// <summary>
        /// アクティブなシーンのうち、sceneNameTableに登録してあるものを全取得する
        /// </summary>
        /// <param name="exclusionScenes">検索から除外するシーン</param>
        /// <returns>シーン名文字列</returns>
        string[] GetActiveSceneNames(params SceneName[] exclusionScenes);

        /// <summary>
        /// アクティブなシーンのうち、sceneNameTableに登録してあるものを全取得する
        /// </summary>
        /// <param name="exclusionScenes">検索から除外するシーン</param>
        /// <returns>シーン名文字列</returns>
        string[] GetActiveSceneNames(string[] exclusionScenes = null);

        /// <summary>
        /// 任意のシーン以外のテーブルに登録されているシーンを返す
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        /// <returns>指定のシーン以外のシーン名群</returns>
        string[] FindOtherSceneNames(SceneName sceneName);

        /// <summary>
        /// 任意のシーン以外のテーブルに登録されているシーンを返す
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        /// <returns>指定のシーン以外のシーン名群</returns>
        string[] FindOtherSceneNames(string sceneName);
    }
}
