using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class Enemy : Car
    {
        protected Vector2 shootDirection = Vector2.DOWN;

        public bool isDestroyed = false;

        protected int health = 100;
        protected float shootSpread = 1;
        protected float timeBetweenShots = 0.2f;
        protected int amountOfShots = 3;

        int amountShot = 0;

        Timer shotDelayTimer;

        public Enemy() : base("car_2.png", "square.png", "player_flash.png", 1, 1, 1)
        {
            SetOrigin(this.width / 2, this.height / 2);
            SetScaleXY(1.3f, 1.3f);
            shotDelayTimer = new Timer(timeBetweenShots, false);
            AddChild(shotDelayTimer);
        }

        public void Update()
        {
            Shake(Time.deltaTime / 1000f);
            HitAnimation();
            EmitSparks();

            if (shotDelayTimer.finishedThisFrame)
            {
                if (amountShot < amountOfShots)
                {
                    Shoot();

                    shotDelayTimer.Start();
                }
            }

        }

        public void OnCollision(GameObject other)
        {
            if (other is Bullet)
            {
                if (!(other as Bullet).isEnemyBullet)
                {
                    doHitAnimation = true;
                    //doEmitSparks = true;
                    //sparksPosition = new Vector2(other.x - this.x, other.y - this.y);
                    other.LateDestroy();
                    health -= 10;
                    if (health <= 0) 
                    {
                        isDestroyed = true;
                        LateDestroy();
                    }
                }
            }
            if (other is Obstacle)
            {
                if (!(other as Obstacle).collidedWith.Contains(this))
                {
                    doHitAnimation = true;
                    health -= 5;
                    if (health <= 0)
                    {
                        isDestroyed = true;
                        LateDestroy();
                    }
                    (other as Obstacle).collidedWith.Add(this);
                }
            }
        }

        void Shoot()
        {
            Bullet bullet = new Bullet(true, shootDirection * Globals.bulletSpeed);
            parent.AddChild(bullet);
            bullet.SetXY(this.x, this.y);

            shootDirection = shootDirection.Rotated(shootSpread, true);
            amountShot++;
        }

        public void StartShooting()
        {
            shootDirection = Vector2.DOWN;
            shootDirection = shootDirection.Rotated(-shootSpread * amountOfShots * 0.5f, true);
            amountShot = 0;

            shotDelayTimer.SetWaitTime(timeBetweenShots);
            shotDelayTimer.Start();
        }

    }
}
