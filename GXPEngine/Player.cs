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
        const int initialAmmoCount = 20;
        const int ammoPickupAmount = 20;

        public int playerHealth = 100;
        public int playerAmmoCount = initialAmmoCount;

        Random random = new Random();
        Timer shootCooldownTimer;

        SoundChannel engineSound;
        SoundChannel dirtSound;
        Sound gunshotSound;
        Sound impactSound;
        Sound reloadSound;

        public Player() : base("player_sheet.png", "player_hitbox.png", "player_flash.png", 3, 1, 3)
        {
            SetScaleXY(1.4f, 1.4f);
            shootCooldownTimer = new Timer(0.15f, true);
            AddChild(shootCooldownTimer);

            engineSound = new Sound("engine.wav", true, false).Play();
            dirtSound = new Sound("dirt.wav", true, false).Play();
            gunshotSound = new Sound("gunshot2.wav");
            impactSound = new Sound("impact.wav");
            reloadSound = new Sound("reload.wav");
        }

        public void Start()
        {
            playerHealth = 100;
            playerAmmoCount = initialAmmoCount;
            AddChild(new Tween(Tween.Property.y, game.height + 100, game.height / 2, 2, Tween.Curves.EaseInOut));
            x = game.width / 2;
        }

        public void Update()
        {
            float delta = Time.deltaTime / 1000f;

            if (Globals.gameState == Globals.States.InGame)
            {
                if (playerHealth <= 0)
                {
                    Explosion explosion = new Explosion();
                    Globals.bulletHolder.AddChild(explosion);
                    explosion.SetXY(x, y);

                    y = game.height + 100;
                    Globals.gameState = Globals.States.InsertCoin;
                    Globals.enemySpawner.DestroyAllEnemies();
                }

                int inputDirection = 0;
                if (Input.GetKey(Key.RIGHT)) inputDirection += 1;
                if (Input.GetKey(Key.LEFT)) inputDirection -= 1;

                x += inputDirection * steerSpeed * delta;

                x = Mathf.Clamp(x, game.width * 0.4f, game.width * 0.6f);

                Shake(delta);
                visibleCar.Animate();

                HitAnimation();
                EmitSparks();

                if (this.x > game.width / 2)
                    dirtSound.Volume = MathUtils.Map(this.x, game.width / 2, game.width * 0.6f, 0.5f, 1);
                else
                    dirtSound.Volume = MathUtils.Map(this.x, game.width * 0.4f, game.width / 2, 1, 0.5f);
            }
            if (shootCooldownTimer.finishedThisFrame)
            {
                if (Input.GetKey(Key.DOWN)) Shoot();
                shootCooldownTimer.Start();
            }
        }

        public void OnCollision(GameObject other)
        {
            if (other is Bullet)
            {
                if ((other as Bullet).isEnemyBullet)
                {
                    other.LateDestroy();
                    doHitAnimation = true;
                    sparksPosition = new Vector2(other.x - this.x, other.y - this.y);
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
                    impactSound.Play();
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

            if (other is AmmoPickup)
            {
                other.LateDestroy();
                playerAmmoCount += ammoPickupAmount;
                reloadSound.Play();
            }
        }

        void Shoot()
        {
            if (playerAmmoCount > 0 && Globals.gameState == Globals.States.InGame)
            {
                float spread = MathUtils.Map((float)random.NextDouble(), 0, 1, -bulletSpread, bulletSpread);
                Bullet bullet = new Bullet(false, Vector2.UP.Rotated(spread, true) * Globals.bulletSpeed);
                Globals.bulletHolder.AddChild(bullet);
                bullet.SetXY(this.x, this.y);
                gunshotSound.Play();
                playerAmmoCount--;
            }
            
        }

    }
}
