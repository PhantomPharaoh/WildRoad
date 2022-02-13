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
        Vector2 shootDirection = Vector2.DOWN;
        
        int health = 100;

        public Enemy() : base("car_2.png", "square.png", "player_flash.png", 1, 1, 1)
        {
            SetOrigin(this.width / 2, this.height / 2);
            SetScaleXY(1.3f, 1.3f);
        }

        public void Update()
        {
            Shake(Time.deltaTime / 1000f);
        }

        public void OnCollision(GameObject other)
        {
            if (other is Bullet)
            {
                if (!(other as Bullet).isEnemyBullet)
                {
                    other.LateDestroy();
                    health -= 10;
                    if (health <= 0) LateDestroy();
                }
            }
        }

        public void Shoot()
        {
            Bullet bullet = new Bullet(true, shootDirection * Globals.bulletSpeed);
            parent.AddChild(bullet);
        }


    }
}
