using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace CharacterMakingSystem.Stage
{
    /// <summary>
    /// 仮想カメラを管理するクラス
    /// </summary>
    public class VcamManager : MonoBehaviour
    {
        [SerializeField, Tooltip("デフォルトカメラ")] 
        private CinemachineVirtualCamera defaultCam = null;

        [SerializeField, Tooltip("外見選択カメラ")]
        private CinemachineVirtualCamera lookCam = null;

        [SerializeField, Tooltip("髪選択カメラ")]
        private CinemachineVirtualCamera hairCam = null;

        [SerializeField, Tooltip("顔選択カメラ")]
        private CinemachineVirtualCamera faceCam = null;
        
        public enum Vcam
        {
            defaultCam = 0,
            lookCam = 1,
            hairCam = 2,
            faceCam = 3
        }

        private Dictionary<int, CinemachineVirtualCamera> vCamDictionary = new();

        private void Awake()
        {
            vCamDictionary.Add((int)Vcam.defaultCam, defaultCam);
            vCamDictionary.Add((int)Vcam.lookCam, lookCam);
            vCamDictionary.Add((int)Vcam.hairCam, hairCam);
            vCamDictionary.Add((int)Vcam.faceCam, faceCam);
        }

        /// <summary>
        /// 仮想カメラを設定する
        /// </summary>
        /// <param name="cam">任意のカメラ</param>
        public void SetVcam(int cam)
        {
            var numbers = Enum.GetValues(typeof(Vcam));
            Array.Sort(numbers);
            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                var index = (int) numbers.GetValue(i);
                if (index > cam)
                {
                    vCamDictionary[index].enabled = false;
                }
                else if(index == cam)
                {
                    vCamDictionary[index].enabled = true;
                    continue;
                }
            }
        }
    }
}
