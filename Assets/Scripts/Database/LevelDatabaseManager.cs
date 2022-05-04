using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Database
{
    public class LevelDatabaseManager : MonoBehaviour
    {
        public List< TextAsset > levels;

        #region Interfaces

        /// <summary>
        /// Get total level count
        /// </summary>
        /// <returns></returns>
        public int GetCount() => levels.Count;

        /// <summary>
        /// Get target level by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [ CanBeNull ]
        public TextAsset GetLevel( int index )
        {
            if ( index < 0 || index >= levels.Count )
                return null;
            return levels[ index ];
        }

        #endregion
    }
}