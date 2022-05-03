using Controls;
using Core;
using UnityEngine;

namespace Gameplay
{
    public class BouncingBall : MonoBehaviour
    {

        [ Header( "Ball Config" ) ] 
        public Vector2 initialVelocity;
        public float ballSpeed = 1.5f;
        public BouncerControl bouncer;
        
        private Rigidbody2D _rigidbody;
        private Vector2 _lastVelocity;

        private bool _isPlaying = false;
        
        #region Unity Lifecycles

        void Start()
        {
            _rigidbody = GetComponent< Rigidbody2D >();
            Reset();

            var eventManager = PongGameManager.GetInstance().GetEventManager();
            eventManager.SubscribeEvent( PongEventManager.PongEventType.LevelChanged, Reset );
            eventManager.SubscribeEvent( PongEventManager.PongEventType.LoseHealth, Reset );
            eventManager.SubscribeEvent( PongEventManager.PongEventType.GameOver, Reset );
        }

        void Update()
        {
            if( !_isPlaying )
                transform.position = bouncer.transform.position + Vector3.up * 0.5f;
            _lastVelocity = _rigidbody.velocity;
        }

        void OnCollisionEnter2D( Collision2D collision )
        {
            Vector2 velocity = _lastVelocity;
            var reflectDir = Vector3.Reflect( velocity.normalized, collision.contacts[ 0 ].normal );
            _rigidbody.velocity = reflectDir * ballSpeed;
        }

        #endregion

        #region Interface

        [ ContextMenu( "Init Speed" ) ]
        public void InitSpeed()
        {
            if( _isPlaying ) return;
            
            _isPlaying = true;
            _rigidbody.velocity = initialVelocity;
        }

        [ ContextMenu( "Reset" ) ]
        public void Reset()
        {
            _rigidbody.velocity = Vector2.zero;
            _isPlaying = false;
        }

        #endregion
    }
}
