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
            LoseHealth,
            GameOver,
            StartGame,
            LevelChanged
        }

        private Dictionary< PongEventType, List< Action > > _subscribers = new Dictionary< PongEventType, List< Action > >();

        private void OnDestroy() => _subscribers.Clear();

        /// <summary>
        /// Subscribe target game event
        /// </summary>
        /// <param name="eventType">Target event type</param>
        /// <param name="action">Callback function</param>
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

        /// <summary>
        /// Unsubscribe event from the event manager
        /// </summary>
        /// <param name="eventType">Target event</param>
        /// <param name="action">Callback function that has bound to the target event</param>
        public void UnsubscribeEvent( PongEventType eventType, Action action )
        {
            if( !_subscribers.ContainsKey( eventType ) ) return;
            var targetSubscribers = _subscribers[ eventType ];
            if(targetSubscribers.Contains( action ))
                targetSubscribers.Remove( action );
            else
                Debug.LogError( $"[PongEventManager] Haven't subscribed before" );
        }

        /// <summary>
        /// Broadcast target event to all listeners
        /// </summary>
        /// <param name="eventType">Target event</param>
        public void BroadcastEvent( PongEventType eventType )
        {
            if( !_subscribers.ContainsKey( eventType ) ) return;
            var targetSubscribers = _subscribers[ eventType ];
            foreach( var action in targetSubscribers )
                action.Invoke();
        }
    }
}
