using System;
using Core;
using Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controls
{
    public class BouncerControl : MonoBehaviour
    {
        #region Inspector
        
        [ Header( "Config" ) ] 
        public float minX;
        public float maxX;

        [ Header( "Ball References" ) ] 
        public BouncingBall ball;

        #endregion

        private Vector3 _lastTouchPosition, _startPosition;
        private Camera currentCamera;

        #region Unity Lifecycles
        
        private void Awake()
        {
            currentCamera = Camera.main;
        }

        private void Update()
        {
            UpdateInput();
        }

        #endregion

        #region Internal

        void UpdateInput()
        {
            if( Input.GetMouseButton( 0 ) && !EventSystem.current.IsPointerOverGameObject() )
            {
                ball.InitSpeed();
                
                Vector3 currentPosition = Input.mousePosition;
                Vector3 touchToWorldPosition = currentCamera.ScreenToWorldPoint( currentPosition );
                transform.position = new Vector3( Mathf.Clamp( touchToWorldPosition.x, minX, maxX ), 
                                                  transform.position.y,
                                                  transform.position.z );
            }
        }

        #endregion
    }
}