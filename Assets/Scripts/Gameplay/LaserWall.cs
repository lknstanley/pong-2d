using System;
using Core;
using UnityEngine;

namespace Gameplay
{
    public class LaserWall : MonoBehaviour
    {
        private void OnCollisionEnter2D( Collision2D collision )
        {
            PongGameManager.GetInstance().GetEventManager().BroadcastEvent( PongEventManager.PongEventType.LoseHealth );
        }
    }
}