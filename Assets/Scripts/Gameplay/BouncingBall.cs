using System;
using UnityEngine;

namespace Gameplay
{
    public class BouncingBall : MonoBehaviour
    {

        [ Header( "Ball Config" ) ] 
        public Vector2 initialVelocity;
        
        private Rigidbody2D _rigidbody;
        private Vector2 _lastVelocity;

        #region Unity Lifecycles

        void Start()
        {
            _rigidbody = GetComponent< Rigidbody2D >();
            _rigidbody.velocity = initialVelocity;
        }

        void Update()
        {
            _lastVelocity = _rigidbody.velocity;
        }

        void OnCollisionEnter2D( Collision2D collision )
        {
            Vector2 velocity = _lastVelocity;
            float speed = _lastVelocity.magnitude;
            var reflectDir = Vector3.Reflect( velocity.normalized, collision.contacts[ 0 ].normal );
            _rigidbody.velocity = reflectDir * Mathf.Max( speed, 0 );
        }

        #endregion
    }
}
