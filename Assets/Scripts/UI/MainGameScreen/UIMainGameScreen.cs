using System;
using System.Collections.Generic;
using Core;
using Models;
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
            eventManager.SubscribeEvent( PongEventManager.PongEventType.LevelChanged, OnLevelChanged );
        }

        public void InitUI()
        {
            // Reset all
            hearts.Clear();
            
            // Instantiate hearts
            PlayerStatus playerStatus = PongGameManager.GetInstance().GetLevelGenerator().GetPlayerStatus();
            int maxHealth = playerStatus.maxHealth;
            for( int i = 0; i < maxHealth; i++ )
            {
                Image spr = Instantiate( heartPrefab, heartContainer );
                hearts.Add( spr );
                spr.sprite = heartSprite;
            }
            
            // Update once
            UpdateUI();
        }
        
        public void UpdateUI()
        {
            // Get player status from the level manager
            PlayerStatus playerStatus = PongGameManager.GetInstance().GetLevelGenerator().GetPlayerStatus();
            
            // Update level label
            levelLbl.text = $"Level: {playerStatus.currentLevel}";
            
            // Change heart sprite to empty heart
            for( int i = playerStatus.currentHealth; i < hearts.Count; i++ )
                hearts[ i ].sprite = emptyHeartSprite;
        }

        #region Event Handlers

        void OnLevelChanged()
        {
            // Show get ready panel
            getReadyTransform.gameObject.SetActive( true );
            gameOverOverlayTransform.gameObject.SetActive( false );
            // Pause the game
            Time.timeScale = 0;
        }
        
        void OnGameOver()
        {
            // Show game over panel
            getReadyTransform.gameObject.SetActive( false );
            gameOverOverlayTransform.gameObject.SetActive( true );
            // Pause the game
            Time.timeScale = 0;
        }

        public void OnStartButtonClicked()
        {
            // Trigger the start game event
            PongGameManager.GetInstance().GetEventManager().BroadcastEvent( PongEventManager.PongEventType.StartGame );
            // Resume the game
            Time.timeScale = 1;
        }

        public void OnBackButtonClicked()
        {
            // Resume game time
            Time.timeScale = 1;
            
            // Load menu scene
            SceneManager.LoadScene( "MainMenuScene" );
        }

        #endregion
    }
}
