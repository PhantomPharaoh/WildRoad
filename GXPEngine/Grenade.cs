using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class Grenade : AnimationSprite
    {

        Timer travelTimer;
        Sprite target;
        Explosion explosion;

        public Grenade(Vector2 startPos, Vector2 targetPos, float travelTime) : base("grenade.png", 3, 2, 5, true, false)
        {
            target = new Sprite("target.png", true, false);
            Globals.bulletHolder.AddChild(target);
            target.SetOrigin(target.width / 2, target.height / 2);
            target.SetScaleXY(0.1f, 0.1f);
            target.SetXY(Globals.player.x, Globals.player.y);


            SetOrigin(this.width / 2, this.height / 2);
            x = startPos.x;
            y = startPos.y;
            SetScaleXY(0.3f, 0.3f);


            AddChild(new Tween(Tween.Property.x, startPos.x, targetPos.x, travelTime, Tween.Curves.Linear));
            AddChild(new Tween(Tween.Property.y, startPos.y, targetPos.y, travelTime, Tween.Curves.Linear));
            AddChild(new Tween(Tween.Property.scale, 0.3f, 1.2f, travelTime, Tween.Curves.SinBounce));

            travelTimer = new Timer(travelTime * 0.99f, true);
            AddChild(travelTimer);



        }

        public void Update()
        {

            Animate(0.25f);

            if (travelTimer.finishedThisFrame)
            {
                explosion = new Explosion();
                Globals.bulletHolder.AddChild(explosion);
                explosion.SetXY(target.x, target.y);

                LateDestroy();
                target.LateDestroy();
            }


        }


    }
}
