using System;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

namespace Core
{
    public class PongEventManager : MonoBehaviour
    {
        public enum PongEventType
        {
            DestroyBrick,
            IncreaseScore,
            LoseHealth,
            IncreaseLevel,
            DecreaseLevel,
            GameOver,
            StartGame,
            AllClear,
        }

        private Dictionary< PongEventType, List< Action > > _subscribers = new Dictionary< PongEventType, List< Action > >();

        private void OnDestroy() => _subscribers.Clear();

        public void SubscribeEvent( PongEventType eventType, Action action )
        {
            if( !_subscribers.ContainsKey( eventType ) )
                _subscribers.Add( eventType, new List< Action >() );

            var targetSubscribers = _subscribers[ eventType ];
            if( !targetSubscribers.Contains( action ) )
                targetSubscribers.Add( action );
            else
                Debug.LogError( $"[PongEventManager] Already subscribed" );
        }

        public void UnsubscribeEvent( PongEventType eventType, Action action )
        {
            if( !_subscribers.ContainsKey( eventType ) ) return;
            var targetSubscribers = _subscribers[ eventType ];
            if(targetSubscribers.Contains( action ))
                targetSubscribers.Remove( action );
            else
                Debug.LogError( $"[PongEventManager] Haven't subscribed before" );
        }

        public void BroadcastEvent( PongEventType eventType )
        {
            if( !_subscribers.ContainsKey( eventType ) ) return;
            var targetSubscribers = _subscribers[ eventType ];
            foreach( var action in targetSubscribers )
                action.Invoke();
        }
    }
}
