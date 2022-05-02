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
        public static PongGameManager GetInstance() => _instance;

        #endregion

        [ SerializeField ] 
        private LevelGenerator levelGenerator;
        /// <summary>
        /// Get Level generator instance
        /// </summary>
        /// <returns></returns>
        public LevelGenerator GetLevelGenerator() => levelGenerator;

        [ SerializeField ] 
        private UIManager uiManager;
        /// <summary>
        /// Get UI manager instance
        /// </summary>
        /// <returns></returns>
        public UIManager GetUIManager() => uiManager;

        [ SerializeField ]
        private PongEventManager pongEventManager;
        /// <summary>
        /// Get Pong event manager instance
        /// </summary>
        /// <returns></returns>
        public PongEventManager GetEventManager() => pongEventManager;
    }
}
