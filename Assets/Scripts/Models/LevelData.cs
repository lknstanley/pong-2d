using System;

namespace Models
{
    [ Serializable ]
    public class LevelData
    {
        public int level;
        public int mapWidth;
        public int mapHeight;
        public int[] map;
    }
}