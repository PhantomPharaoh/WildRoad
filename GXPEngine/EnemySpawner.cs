using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class EnemySpawner : GameObject
    {

        List<Enemy[]> enemies = new List<Enemy[]>();
        Timer spawnTimer;
        Random random;

        const float enemySeparation = 40f;

        public EnemySpawner()
        {
            random = new Random();
            spawnTimer = new Timer(1, true);
            enemies.Append(new Enemy[4]);
        }


        public void Update()
        {

            if (spawnTimer.finishedThisFrame)
            {
                //removing empty rows
                foreach (Enemy[] row in enemies)//let's hope this works
                {
                    if (IsArrayEmpty(row)) enemies.Remove(row);
                }

                if (enemies.Count == 0) enemies.Append(new Enemy[4]);
                //if all rows were empty and were removed, we add in a new empty one

                if (IsArrayFull(enemies.Last())) enemies.Append(new Enemy[4]);
                //if the last row if full, we add in a new empty one

                int spot = GetEmptySpot(enemies.Last());
                Enemy enemy = new Enemy();
                enemies.Last()[spot] = enemy;
                AddChild(enemy);
                //enemy.x = 
                //enemy.AddChild(new Tween(Tween.Property.y));




                spawnTimer.SetWaitTime(random.Next(2, 10));
                spawnTimer.Start();
            }


        }

        bool IsArrayEmpty(Enemy[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null) return false;
            }
            return true;
        }

        bool IsArrayFull(Enemy[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null) return false;
            }
            return true;
        }

        int GetEmptySpot(Enemy[] array)
        {
            if (!IsArrayFull(array))
            {
                int spot = -1;
                while (spot == -1)
                {
                    int randSpot = random.Next(array.Length);
                    if (array[randSpot] == null) spot = randSpot;
                }
                return spot;
            }
            return -1;
        }


    }
}
