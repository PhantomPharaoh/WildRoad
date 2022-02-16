﻿using System;
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
        Timer shootTimer;
        Random random;

        const float enemyHorizontalSeparation = 70f;
        const float enemyVerticalSeparation = 80f;

        public EnemySpawner()
        {
            random = new Random();
            spawnTimer = new Timer(1, true);
            AddChild(spawnTimer);
            shootTimer = new Timer(3, true);
            AddChild(shootTimer);
        }


        public void Update()
        {
            foreach (Enemy[] row in enemies)//remove destroyed enemies
            {
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i] != null && row[i].isDestroyed)
                    {
                        row[i] = null;
                    }
                }
            }

            if (enemies.Count > 0)//remove empty rows
            {
                bool containsEnemy = false;
                for (int i = 0; i < enemies[0].Length; i++)
                {
                    if (enemies[0][i] != null)
                    {
                        containsEnemy = true;
                    }
                }
                if (!containsEnemy) enemies.RemoveAt(0);
            }


            if (spawnTimer.finishedThisFrame)
            {
                //if all rows were empty and were removed, we add in a new empty one
                if (enemies.Count == 0) enemies.Add(new Enemy[4]);

                if (IsArrayFull(enemies.Last()) && enemies.Count < 3)
                {
                    foreach (Enemy[] row in enemies)//all rows go up
                    {
                        for (int i = 0; i < row.Length; i++)
                        {
                            if (row[i] != null)
                            {
                                row[i].AddChild(new Tween(
                                    Tween.Property.y,
                                    row[i].y,
                                    row[i].y - enemyVerticalSeparation,
                                    1,
                                    Tween.Curves.EaseOut));
                            }
                        }
                    }


                    enemies.Add(new Enemy[4]);//if the last row if full, we add in a new empty one
                }
                
                if (!IsArrayFull(enemies.Last()))
                {
                    int spot = GetEmptySpot(enemies.Last());
                    Enemy enemy = new Enemy();
                    enemies.Last()[spot] = enemy;
                    AddChild(enemy);
                    enemy.x = (spot - enemies.Last().Length / 2f) * enemyHorizontalSeparation + enemyHorizontalSeparation / 2f;
                    enemy.AddChild(new Tween(Tween.Property.y, 0, -enemyVerticalSeparation, 1, Tween.Curves.EaseOut));
                }
                
                spawnTimer.SetWaitTime(random.Next(2, 10));
                spawnTimer.Start();
            }

            if (shootTimer.finishedThisFrame)
            {
                if (enemies.Count > 0)
                {
                    int shooterInex = -1;
                    int rowIndex = 0;
                    while (shooterInex == -1)
                    {
                        int randomCol = random.Next(0, enemies[0].Length);
                        int randomRow = random.Next(0, enemies.Count);
                        if (enemies[randomRow][randomCol] != null)
                        {
                            if (!enemies[randomRow][randomCol].shooting)
                            {
                                shooterInex = randomCol;
                                rowIndex = randomRow;
                            }
                        }
                    }

                    enemies[rowIndex][shooterInex].StartShooting();

                }



                shootTimer.SetWaitTime(random.Next(1, 4));
                shootTimer.Start();
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
