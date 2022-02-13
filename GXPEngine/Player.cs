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

        int playerHealth = 100;

        Random random = new Random();

        public Player() : base("car_1.png", "square.png")
        {
            SetScaleXY(1.5f, 1.5f);
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

            if (Input.GetKeyDown(Key.DOWN)) Shoot();

        }

        public void OnCollision(GameObject other)
        {
            if (other is Bullet)
            {
                if ((other as Bullet).isEnemyBullet) other.LateDestroy();
            }
        }

        void Shoot()
        {
            float spread = MathUtils.Map((float)random.NextDouble(), 0, 1, -bulletSpread, bulletSpread);
            Bullet bullet = new Bullet(false, Vector2.UP.Rotated(spread, true) * Globals.bulletSpeed);
            parent.AddChild(bullet);
            bullet.SetXY(this.x, this.y);

        }

    }
}
