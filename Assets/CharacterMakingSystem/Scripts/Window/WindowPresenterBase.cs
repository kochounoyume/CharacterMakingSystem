using UnityEngine;

namespace CharacterMakingSystem.Window
{
    using Data;
    
    /// <summary>
    /// 性別・外見・髪・顔の各Windowのプレゼンターの基底クラス
    /// </summary>
    public class WindowPresenterBase : MonoBehaviour
    {
        /// <summary>
        /// 各Windowで共通で使用する処理を登録するメソッド
        /// </summary>
        /// <param name="view">ビュークラス</param>
        /// <param name="funcData">各Windowで共通で使用する処理登録用のデータクラス</param>
        /// <param name="nextScene">次のシーン名</param>
        protected void SetWindowFunc(WindowViewBase view, WindowBtnFuncData funcData, CoreSystem.SceneName nextScene)
        {
            view.SetFuncSexBtn(funcData.sexBtnFunc);
            view.SetFuncLookBtn(funcData.lookBtnFunc);
            view.SetFuncHairBtn(funcData.hairBtnFunc);
            view.SetFuncFaceBtn(funcData.faceBtnFunc);
            view.SetFuncCreateProgBtn(funcData.createProgBtnFunc);
            view.SetFuncNextProgBtn(funcData.nextProgBtnFunc, nextScene);
        }
    }
}
