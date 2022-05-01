using System;
using UnityEngine;

namespace Core
{
    public class LevelGenerator : MonoBehaviour
    {

        #region Inspector

        [ Header( "Level Config" ) ]
        public Vector3 levelCenterPosition;
        public Vector3 levelSize;
        public GameObject brickPrefab;
        public int currentLevel = 1;
        
        [ Header( "Map Config" ) ]
        public int maxColumn = 4;
        public int maxRow = 11;

        #endregion

        #region Interfaces

        /// <summary>
        /// Generate specified level
        /// </summary>
        /// <param name="level">Target level</param>
        public void PopulateLevel( int level )
        {
            currentLevel = level;
            GenerateLevel();
        }

        #endregion

        #region Internal

        /// <summary>
        /// Generate level from json file
        /// </summary>
        [ ContextMenu( "Generate Level" ) ]
        void GenerateLevel()
        {
            // var firstBrick = Instantiate( brickPrefab, transform );
            var brickRenderer = brickPrefab.GetComponent< SpriteRenderer >();
            var brickSize = brickRenderer.bounds.size;
            maxColumn = Mathf.FloorToInt( levelSize.x / brickRenderer.bounds.size.x );
            maxRow = Mathf.FloorToInt( levelSize.y / ( brickRenderer.bounds.size.y + 0.2f ) );
            
            for( int y = 1; y <= maxRow; y++ )
            {
                for( int x = 1; x <= maxColumn; x++ )
                {
                    var go = Instantiate( brickPrefab, transform );
                    go.name = $"Brick_{x}_{y}";
                    var goTrans = go.transform;
                    go.transform.localPosition = new Vector3( 
                        -levelSize.x / 2 - brickSize.x / 2 + ( brickSize.x * x ), 
                        levelCenterPosition.y + levelSize.y / 2 - goTrans.localScale.y / 2 * y, 
                        0 );
                }
            }
        }

        #endregion

        #region Debug Usage

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color( 1, 0, 0, 0.2f );
            Gizmos.DrawCube( levelCenterPosition, levelSize );
        }

        #endregion
    }
}