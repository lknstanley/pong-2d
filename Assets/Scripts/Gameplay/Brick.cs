using System;
using Core;
using UnityEngine;

namespace Gameplay
{
    public class Brick : MonoBehaviour
    {
        private void OnCollisionEnter2D( Collision2D collision )
        {
            // Trigger destroy brick event
            PongGameManager.GetInstance().GetEventManager()
                           .BroadcastEvent( PongEventManager.PongEventType.DestroyBrick );
            
            // Destroy self
            Destroy( gameObject );
        }
    }
}