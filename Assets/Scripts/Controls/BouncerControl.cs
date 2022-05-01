using System;
using UnityEngine;

namespace Controls
{
    public class BouncerControl : MonoBehaviour
    {
        #region Inspector
        
        [ Header( "Config" ) ] 
        public float minX;
        public float maxX;

        #endregion

        private Vector3 _lastTouchPosition, _startPosition;
        private Rigidbody2D _rigidbody;

        #region Unity Lifecycles
        
        private void Awake()
        {
            _rigidbody = GetComponent< Rigidbody2D >();
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
            if( Input.GetMouseButton( 0 ))
            {
                float dragDistance = Vector3.Distance( _startPosition, _lastTouchPosition );
                float directionVec = Vector3.Dot( transform.right, _lastTouchPosition - _startPosition );
                Debug.Log( $"[Input Control] Drag Distance: {dragDistance}" );
                _rigidbody.velocity = transform.right * dragDistance * Time.deltaTime;
                

                _lastTouchPosition = Input.mousePosition;
            }
        }

        #endregion
    }
}