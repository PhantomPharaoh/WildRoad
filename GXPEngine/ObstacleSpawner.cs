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
        Timer warningTimer;
        Sprite warning;

        string[] obstacleTextures = { "cactus.png", "bones.png" };

        public ObstacleSpawner()
        {
            random = new Random();
            spawnTimer = new Timer(1, true);
            AddChild(spawnTimer);
            warningTimer = new Timer(2, false);
            AddChild(warningTimer);
            warning = new Sprite("warning.png", false, false);
            AddChild(warning);
            warning.SetOrigin(warning.width/2, 0);
            warning.SetScaleXY(0.1f, 0.1f);
            warning.visible = false;
        }

        public void Update()
        {



            if (spawnTimer.finishedThisFrame)
            {
                warning.visible = true;
                warning.AddChild(new Tween(Tween.Property.alpha, 0, 1, 2, Tween.Curves.SinDamp));
                warning.x = MathUtils.Map((float)random.NextDouble(), 0, 1, game.width * 0.4f, game.width * 0.6f);
                warningTimer.Start();
            }

            if (warningTimer.finishedThisFrame)
            {
                Obstacle obstacle = new Obstacle(obstacleTextures[random.Next(obstacleTextures.Length)]);
                AddChild(obstacle);
                obstacle.x = warning.x;
                obstacle.y = -100;
                warning.visible = false;

                spawnTimer.SetWaitTime(random.Next(2, 5));
                spawnTimer.Start();
            }



        }


    }
}
