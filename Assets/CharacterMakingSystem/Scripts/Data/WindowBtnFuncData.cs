using UnityEngine.Events;

namespace CharacterMakingSystem.Data
{
    using CoreSystem;
    
    /// <summary>
    /// 各Windowで共通で使用するシーン遷移処理登録用のデータクラス
    /// </summary>
    public sealed class WindowBtnFuncData
    {
        /// <summary>
        /// 性別選択ボタンに登録する処理
        /// </summary>
        public UnityAction sexBtnFunc { get; private set; }
        
        /// <summary>
        /// 外見選択ボタンに登録する処理
        /// </summary>
        public UnityAction lookBtnFunc { get; private set; }
        
        /// <summary>
        /// 髪選択ボタンに登録する処理
        /// </summary>
        public UnityAction hairBtnFunc { get; private set; }
        
        /// <summary>
        /// 顔選択ボタンに登録する処理
        /// </summary>
        public UnityAction faceBtnFunc { get; private set; }
        
        /// <summary>
        /// 作成ボタンに処理を登録する処理
        /// </summary>
        public UnityAction createProgBtnFunc { get; private set; }
        
        /// <summary>
        /// 次へ選択ボタンに処理を登録する処理
        /// </summary>
        public UnityAction<SceneName> nextProgBtnFunc { get; private set; }

        /// <summary>
        /// 各Windowで共通で使用するシーン遷移処理登録用のデータクラスのコンストラクタ
        /// </summary>
        /// <param name="sexBtnFunc">性別選択ボタンに登録する処理</param>
        /// <param name="lookBtnFunc">外見選択ボタンに登録する処理</param>
        /// <param name="hairBtnFunc">髪選択ボタンに登録する処理</param>
        /// <param name="faceBtnFunc">顔選択ボタンに登録する処理</param>
        /// <param name="createProgBtnFunc">作成ボタンに処理を登録する処理</param>
        /// <param name="nextProgBtnFunc">次へ選択ボタンに処理を登録する処理</param>
        public WindowBtnFuncData(
            UnityAction sexBtnFunc,
            UnityAction lookBtnFunc, 
            UnityAction hairBtnFunc,
            UnityAction faceBtnFunc,
            UnityAction createProgBtnFunc, 
            UnityAction<SceneName> nextProgBtnFunc
            )
        {
            this.sexBtnFunc = sexBtnFunc;
            this.lookBtnFunc = lookBtnFunc;
            this.hairBtnFunc = hairBtnFunc;
            this.faceBtnFunc = faceBtnFunc;
            this.createProgBtnFunc = createProgBtnFunc;
            this.nextProgBtnFunc = nextProgBtnFunc;
        }
    }
}
