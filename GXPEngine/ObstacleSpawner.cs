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
        Timer ammoSpawnTimer;

        string[] obstacleTextures = { "cactus.png", "bones.png" };

        public ObstacleSpawner()
        {
            random = new Random();
            spawnTimer = new Timer(1, false);
            AddChild(spawnTimer);
            warningTimer = new Timer(2, false);
            AddChild(warningTimer);
            warning = new Sprite("warning.png", false, false);
            AddChild(warning);
            warning.SetOrigin(warning.width/2, 0);
            warning.SetScaleXY(0.07f, 0.07f);
            warning.visible = false;

            ammoSpawnTimer = new Timer(5, false);
            AddChild(ammoSpawnTimer);
        }

        public void Start()
        {
            ammoSpawnTimer.SetWaitTime(5);
            ammoSpawnTimer.Start();
            spawnTimer.SetWaitTime(1);
            spawnTimer.Start();
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
                if (Globals.gameState == Globals.States.InGame)
                    spawnTimer.Start();
            }

            if (ammoSpawnTimer.finishedThisFrame)
            {
                AmmoPickup ammo = new AmmoPickup();
                AddChild(ammo);
                ammo.SetXY(MathUtils.Map((float)random.NextDouble(), 0, 1, game.width * 0.4f, game.width * 0.6f), -100);

                ammoSpawnTimer.SetWaitTime(random.Next(5, 7));
                if (Globals.gameState == Globals.States.InGame)
                    ammoSpawnTimer.Start();
            }

        }


    }
}
