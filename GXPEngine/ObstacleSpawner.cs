using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class ObstacleSpawner : GameObject
    {

        Timer spawnTimer;
        Random random;

        public ObstacleSpawner()
        {
            random = new Random();
            spawnTimer = new Timer(1, true);
            AddChild(spawnTimer);
        }

        public void Update()
        {



            if (spawnTimer.finishedThisFrame)
            {

                Obstacle obstacle = new Obstacle();
                AddChild(obstacle);
                obstacle.x = MathUtils.Map((float)random.NextDouble(), 0, 1, game.width * 0.4f, game.width * 0.6f);
                obstacle.y = -100;

                spawnTimer.SetWaitTime(random.Next(2, 5));
                spawnTimer.Start();
            }

        }


    }
}
