using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class Player : Car
    {

        const float steerSpeed = 150f;
        const float bulletSpread = 5f;

        public int playerHealth = 100;

        Random random = new Random();
        Timer shootCooldownTimer;

        SoundChannel engineSound;
        SoundChannel dirtSound;

        public Player() : base("player_sheet.png", "player_hitbox.png", "player_flash.png", 3, 1, 3)
        {
            SetScaleXY(1.4f, 1.4f);
            shootCooldownTimer = new Timer(0.15f, true);
            AddChild(shootCooldownTimer);

            engineSound = new Sound("engine.wav", true, false).Play();
            dirtSound = new Sound("dirt.wav", true, false).Play();
        }

        public void Update()
        {
            float delta = Time.deltaTime / 1000f;

            int inputDirection = 0;
            if (Input.GetKey(Key.RIGHT)) inputDirection += 1;
            if (Input.GetKey(Key.LEFT)) inputDirection -= 1;

            x += inputDirection * steerSpeed * delta;

            x = Mathf.Clamp(x, game.width*0.4f, game.width*0.6f);

            Shake(delta);
            visibleCar.Animate();

            HitAnimation();
            EmitSparks();

            if (shootCooldownTimer.finishedThisFrame)
            {
                if (Input.GetKey(Key.DOWN)) Shoot();
                shootCooldownTimer.Start();
            }

            if (this.x > game.width / 2)
                dirtSound.Volume = MathUtils.Map(this.x, game.width / 2, game.width * 0.6f, 0.5f, 1);
            else
                dirtSound.Volume = MathUtils.Map(this.x, game.width * 0.4f, game.width / 2, 1, 0.5f);

        }

        public void OnCollision(GameObject other)
        {
            if (other is Bullet)
            {
                if ((other as Bullet).isEnemyBullet)
                {
                    other.LateDestroy();
                    doHitAnimation = true;
                    sparksPosition = new Vector2(other.parent.x + other.x - this.x, other.parent.y + other.y - this.y);
                    doEmitSparks = true;
                    playerHealth -= 10;
                    
                    bulletHitSound.Play();
                }
            }

            if (other is Obstacle) 
            {
                if (!(other as Obstacle).collidedWith.Contains(this))
                {
                    doHitAnimation = true;
                    (other as Obstacle).collidedWith.Add(this);
                    playerHealth -= 10;
                }
            }

            if (other is Explosion)
            {
                if (!(other as Explosion).hitPlayer)
                {
                    (other as Explosion).hitPlayer = true;
                    doHitAnimation = true;
                    playerHealth -= 10;
                }
            }

            if (other is Rocket)
            {
                if (!(other as Rocket).collidedWithPlayer)
                {
                    (other as Rocket).collidedWithPlayer = true;
                }
            }

        }

        void Shoot()
        {
            float spread = MathUtils.Map((float)random.NextDouble(), 0, 1, -bulletSpread, bulletSpread);
            Bullet bullet = new Bullet(false, Vector2.UP.Rotated(spread, true) * Globals.bulletSpeed);
            Globals.bulletHolder.AddChild(bullet);
            bullet.SetXY(this.x, this.y);

        }

    }
}
