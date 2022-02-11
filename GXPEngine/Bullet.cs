using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class Bullet : Sprite
    {
        public bool isEnemyBullet;
        Vector2 velocity;

        public Bullet(bool isEnemyBullet, Vector2 velocity) : base("circle.png", true, true)
        {
            this.isEnemyBullet = isEnemyBullet;
            this.velocity = velocity;
            SetScaleXY(0.1f, 0.1f);
        }

        public void Update()
        {
            float delta = Time.deltaTime / 1000f;

            x += velocity.x * delta * 60;
            y += velocity.y * delta * 60;
        }

    }
}
