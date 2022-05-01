using System;
using UnityEngine;

namespace Core
{
    public class PongGameManager : MonoBehaviour
    {
        #region Singleton Setup
        
        private static PongGameManager _instance;
        private void Awake()
        {
            _instance = this;
        }

        #endregion

        #region Interfaces

        /// <summary>
        /// Get Pong Game Manager Instance
        /// </summary>
        /// <returns></returns>
        public PongGameManager GetInstance() => _instance;

        #endregion
    }
}
