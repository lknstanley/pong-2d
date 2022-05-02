using System;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainGameScreen
{
    public class UIMainGameScreen : MonoBehaviour
    {
        [ Header( "UI References" ) ]
        public Transform heartContainer;
        public Image heartPrefab;
        public List< Image > hearts = new List< Image >();
        public TMP_Text levelLbl;
        public Transform getReadyTransform;
        public Transform gameOverOverlayTransform;
        public Transform allClearOverlayTransform;

        [ Header( "Image References" ) ]
        public Sprite emptyHeartSprite;
        public Sprite heartSprite;

        private void Start()
        {
            // Bind events
            var eventManager = PongGameManager.GetInstance().GetEventManager();
            eventManager.SubscribeEvent( PongEventManager.PongEventType.GameOver, OnGameOver );
            eventManager.SubscribeEvent( PongEventManager.PongEventType.IncreaseLevel, OnLevelChanged );
            eventManager.SubscribeEvent( PongEventManager.PongEventType.DecreaseLevel, OnLevelChanged );
            eventManager.SubscribeEvent( PongEventManager.PongEventType.LoseHealth, OnLoseHealth );
            eventManager.SubscribeEvent( PongEventManager.PongEventType.AllClear, OnAllClear );

            Reset();
        }

        public void UpdateUI()
        {
            levelLbl.text = $"Level: {PongGameManager.GetInstance().GetLevelGenerator().currentLevel}";
        }

        #region Internal

        void Reset()
        {
            // Reset all
            hearts.Clear();
            
            // Instantiate hearts
            int maxHealth = PongGameManager.GetInstance().GetLevelGenerator().maxHealth;
            for( int i = 0; i < maxHealth; i++ )
            {
                Image spr = Instantiate( heartPrefab, heartContainer );
                hearts.Add( spr );
                spr.sprite = heartSprite;
            }
            
            // Init current level label
            levelLbl.text = $"Level: {PongGameManager.GetInstance().GetLevelGenerator().currentLevel}";
        }

        #endregion
        
        #region Event Handlers

        void OnLevelChanged()
        {
            getReadyTransform.gameObject.SetActive( true );
        }
        
        void OnGameOver()
        {
            gameOverOverlayTransform.gameObject.SetActive( true );
        }

        void OnAllClear()
        {
            allClearOverlayTransform.gameObject.SetActive( true );
        }

        void OnLoseHealth()
        {
            // Get latest health value from level generator
            int newHealth = PongGameManager.GetInstance().GetLevelGenerator().health;
            
            // Change heart sprite to empty heart
            for( int i = newHealth - 1; i < hearts.Count; i++ )
                hearts[ i ].sprite = emptyHeartSprite;
        }

        public void OnStartButtonClicked()
        {
            PongGameManager.GetInstance().GetEventManager().BroadcastEvent( PongEventManager.PongEventType.StartGame );
        }

        public void OnBackButtonClicked()
        {
            SceneManager.LoadScene( "MainMenuScene" );
        }

        #endregion
    }
}
