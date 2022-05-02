using Models;
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
        public TextAsset levelJson;
        public LevelData level;
        public int maxColumn = 4;
        public int maxRow = 11;

        [ Header( "Game Condition" ) ]
        public int leftBrick = 0;
        public int health = 3;

        #endregion

        #region Unity Lifecycle

        private void Start()
        {
            var eventManager = PongGameManager.GetInstance().GetEventManager();
            eventManager.SubscribeEvent( PongEventManager.PongEventType.IncreaseLevel, OnIncreaseLevel );
            eventManager.SubscribeEvent( PongEventManager.PongEventType.DecreaseLevel, OnDecreaseLevel );
            eventManager.SubscribeEvent( PongEventManager.PongEventType.DestroyBrick, OnDestroyBrick );
            eventManager.SubscribeEvent( PongEventManager.PongEventType.LoseHealth, OnLoseHealth );
            eventManager.SubscribeEvent( PongEventManager.PongEventType.GameOver, OnGameOver );
        }

        #endregion

        #region Event Handlers

        void OnIncreaseLevel()
        {
            currentLevel++;
            PopulateLevel();
        }

        void OnDecreaseLevel()
        {
            currentLevel--;
            PopulateLevel();
        }

        void OnDestroyBrick()
        {
            leftBrick--;
            if( leftBrick <= 0 )
                PongGameManager.GetInstance().GetEventManager()
                               .BroadcastEvent( PongEventManager.PongEventType.IncreaseLevel );
        }

        void OnLoseHealth()
        {
            health--;
            if( health <= 0 )
                PongGameManager.GetInstance().GetEventManager()
                               .BroadcastEvent( PongEventManager.PongEventType.GameOver );
        }

        void OnGameOver()
        {
            Debug.Log( $"Game Over" );
            CleanLevel();
            Time.timeScale = 0;
        }

        #endregion
        
        #region Interfaces

        /// <summary>
        /// Generate specified level
        /// </summary>
        [ ContextMenu( "Generate Level" ) ]
        public void PopulateLevel()
        {
            CleanLevel();
            levelJson = Resources.Load< TextAsset >( $"Levels/level_{currentLevel}" );
            GenerateLevel();
        }

        /// <summary>
        /// Remove all bricks from level generator to clean the level
        /// </summary>
        public void CleanLevel()
        {
            for( int i = 0; i < transform.childCount; i++ )
            {
                var child = transform.GetChild( i );
                Destroy( child.gameObject );
            }
        }
        
        /// <summary>
        /// Upgrade the level / difficulties
        /// </summary>
        [ ContextMenu( "Level Up" ) ]
        public void LevelUp() => currentLevel++;
        
        /// <summary>
        /// Downgrade the level / difficulties
        /// </summary>
        [ ContextMenu( "Level Down" ) ]
        public void LevelDown() => currentLevel--;

        #endregion

        #region Internal

        /// <summary>
        /// Generate level from json file
        /// </summary>
        void GenerateLevel()
        {
            level = JsonUtility.FromJson < LevelData >( levelJson.text );

            leftBrick = 0;
            
            var brickRenderer = brickPrefab.GetComponent< SpriteRenderer >();
            var brickBounds = brickRenderer.bounds;
            var brickSize = brickBounds.size;
            
            maxColumn = Mathf.FloorToInt( levelSize.x / brickBounds.size.x );
            maxRow = Mathf.FloorToInt( levelSize.y / ( brickRenderer.bounds.size.y + 0.2f ) );
            
            for( int y = 1; y <= maxRow; y++ )
            {
                for( int x = 1; x <= maxColumn; x++ )
                {
                    int targetIndex = ( y - 1 ) * level.mapWidth + ( x - 1 );
                    if( level.map[ targetIndex ] == 0 ) continue;

                    var go = Instantiate( brickPrefab, transform );
                    go.name = $"Brick_{x}_{y}";
                    var goTrans = go.transform;
                    go.transform.localPosition = new Vector3( 
                        -levelSize.x / 2 - brickSize.x / 2 + ( brickSize.x * x ), 
                        levelCenterPosition.y + levelSize.y / 2 - goTrans.localScale.y / 2 * y, 
                        0 );
                    leftBrick++;
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