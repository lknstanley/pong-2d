using System;
using System.IO;
using System.Threading.Tasks;
using Models;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Database
{
    [ CustomEditor( typeof( LevelDatabaseManager ) ) ]
    public class LevelDatabaseManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if ( GUILayout.Button( "Randomise New Level" ) )
            {
                GenerateNewLevel();
            }
        }

        void GenerateNewLevel()
        {
            LevelDatabaseManager manager = (LevelDatabaseManager) target;
            LevelData levelData = new LevelData
            {
                level = manager.GetCount(),
                mapWidth = 4,
                mapHeight = 11
            };
            
            levelData.map = new int[ levelData.mapWidth * levelData.mapHeight ];
            for ( var i = 0; i < levelData.map.Length; i++ )
                levelData.map[ i ] = Random.Range( 0, 100 ) <= 50 ? 0 : 1;

            File.WriteAllText( $"Assets/Resources/Levels/level_{manager.GetCount() + 1}.json",
                               JsonUtility.ToJson( levelData ) );
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            TextAsset levelTextAsset =
                Resources.Load< TextAsset >( $"Levels/level_{manager.GetCount() + 1}" );
            manager.levels.Add( levelTextAsset );
        }
    }
}