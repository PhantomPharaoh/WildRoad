﻿using System;
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
        public bool shooting = false;

        protected int health = 100;
        protected float shootSpread = 5;
        protected float timeBetweenShots = 0.2f;
        protected int amountOfShots = 3;

        int spreadDirection = 1;//-1 or 1
        int amountShot = 0;

        Timer shotDelayTimer;

        public Enemy(string texturePath, string hitboxPath, string flashPath, int col = 1, int row = 1, int frames = 1) : base(texturePath, hitboxPath, flashPath, col, row, frames)
        {
            SetOrigin(this.width / 2, this.height / 2);
            SetScaleXY(1.4f, 1.4f);
            shotDelayTimer = new Timer(timeBetweenShots, false);
            AddChild(shotDelayTimer);
        }

        public void Update()
        {
            Shake(Time.deltaTime / 1000f);
            HitAnimation();
            EmitSparks();
            visibleCar.Animate();

            if (shotDelayTimer.finishedThisFrame)
            {
                if (amountShot < amountOfShots)
                {
                    Shoot();

                    shotDelayTimer.Start();
                }
                else shooting = false;
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
                    bulletHitSound.Play();
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
            Globals.bulletHolder.AddChild(bullet);
            Vector2 globalBulletPos = TransformPoint(0, 0);
            bullet.SetXY(globalBulletPos.x, globalBulletPos.y);
            

            shootDirection = shootDirection.Rotated(shootSpread * spreadDirection, true);
            amountShot++;
        }

        public virtual void StartShooting()
        {
            shooting = true;
            spreadDirection = random.Next(0, 2) == 0 ? 1 : -1;

            shootDirection = Vector2.DOWN;
            shootDirection = shootDirection.Rotated(shootSpread * amountOfShots * 0.5f * -spreadDirection, true);
            amountShot = 0;

            shotDelayTimer.SetWaitTime(timeBetweenShots);
            shotDelayTimer.Start();
        }

    }
}
