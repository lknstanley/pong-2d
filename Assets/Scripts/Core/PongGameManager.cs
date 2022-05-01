using System;
using UnityEngine;

namespace Core
{
    public class PongGameManager : MonoBehaviour
    {
        #region Singleton Setup
        
        private static PongGameManager _instance;
        
        private void Awake() => _instance = this;
        
        /// <summary>
        /// Get Pong Game Manager Instance
        /// </summary>
        /// <returns></returns>
        public PongGameManager GetInstance() => _instance;

        #endregion

        [ SerializeField ] 
        private LevelGenerator levelGenerator;
        /// <summary>
        /// Get Level generator instance
        /// </summary>
        /// <returns></returns>
        public LevelGenerator GetLevelGenerator() => levelGenerator;
    }
}
