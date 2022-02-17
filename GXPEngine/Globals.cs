using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal static class Globals
    {
        public enum States
        {
            InsertCoin, InGame
        }
        
        public static float score = 0;
        public static int difficulty = 0;
        public static float scrollSpeed = 1f;
        public static float bulletSpeed = 3f;

        public static Player player;
        public static Pivot bulletHolder;
        public static EnemySpawner enemySpawner;

        public static States gameState = States.InsertCoin;
    }
}
