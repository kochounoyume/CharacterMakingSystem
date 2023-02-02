using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CharacterMakingSystem.Stage
{
    using Data;
    using CoreSystem;
    
    /// <summary>
    /// 実際に3Dゲーム空間を扱うプレゼンタークラス
    /// </summary>
    public class StagePresenter : MonoBehaviour
    {
        [SerializeField, Tooltip("ビュークラス")] 
        private StageView stageView = null;

        [Inject] 
        private ChaPartDatabase database;
        
        [Inject] 
        private IAssetLoader assetLoader;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
