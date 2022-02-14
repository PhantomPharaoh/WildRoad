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

        public Bullet(bool isEnemyBullet, Vector2 velocity) : base("bullet.png", true, true)
        {
            SetOrigin(this.width / 2, this.height / 2);
            this.isEnemyBullet = isEnemyBullet;
            this.velocity = velocity;
            SetScaleXY(0.4f, 0.7f);

            Sprite glow = new Sprite("circle_05.png", true, false);
            AddChild(glow);
            glow.SetOrigin(glow.width/2, glow.height/2);
            glow.SetScaleXY(0.15f, 0.15f);
            glow.blendMode = BlendMode.LIGHTING;
            //glow.alpha = 0.01f;
        }

        public void Update()
        {
            float delta = Time.deltaTime / 1000f;

            x += velocity.x * delta * 60;
            y += velocity.y * delta * 60;

            rotation = -velocity.AngleTo(Vector2.DOWN, true);

            if (y > game.height + 10) LateDestroy();
        }

    }
}
