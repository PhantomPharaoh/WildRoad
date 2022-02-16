using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class Grenade : Sprite
    {

        Timer travelTimer;
        Sprite target;
        Explosion explosion;

        public Grenade(Vector2 startPos, Vector2 targetPos, float travelTime) : base("circle.png", true, false)
        {
            target = new Sprite("target.png", true, false);
            Globals.bulletHolder.AddChild(target);
            target.SetOrigin(target.width / 2, target.height / 2);
            target.SetScaleXY(0.2f, 0.2f);
            target.SetXY(Globals.player.x, Globals.player.y);


            SetOrigin(this.width / 2, this.height / 2);
            x = startPos.x;
            y = startPos.y;
            SetScaleXY(0.2f, 0.2f);


            AddChild(new Tween(Tween.Property.x, startPos.x, targetPos.x, travelTime, Tween.Curves.Linear));
            AddChild(new Tween(Tween.Property.y, startPos.y, targetPos.y, travelTime, Tween.Curves.Linear));
            AddChild(new Tween(Tween.Property.scale, 0.2f, 1, travelTime, Tween.Curves.SinBounce));

            travelTimer = new Timer(travelTime * 0.99f, true);
            AddChild(travelTimer);



        }

        public void Update()
        {

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
