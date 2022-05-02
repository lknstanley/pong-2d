using System;
using Core;
using Gameplay;
using UnityEngine;

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
        private Rigidbody2D _rigidbody;
        private Camera currentCamera;

        #region Unity Lifecycles
        
        private void Awake()
        {
            _rigidbody = GetComponent< Rigidbody2D >();
            currentCamera = Camera.main;
        }

        private void Update()
        {
            #if UNITY_EDITOR
            UpdateMouse();
            #else
            UpdateTouch();
            #endif
        }

        #endregion

        #region Internal

        void UpdateTouch()
        {
            
        }

        void UpdateMouse()
        {
            if( Input.GetMouseButton( 0 ) )
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