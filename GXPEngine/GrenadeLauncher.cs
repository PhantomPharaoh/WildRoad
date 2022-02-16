using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class GrenadeLauncher : Enemy
    {

        Timer grenadeTravelTimer;
        Sprite grenade;
        Sprite target;

        public GrenadeLauncher() : base()
        {
            grenadeTravelTimer = new Timer(2.9f, false);
            AddChild(grenadeTravelTimer);
        }

        public override void StartShooting()
        {
            shooting = true;

            target = new Sprite("target.png", true, false);
            Globals.bulletHolder.AddChild(target);
            target.SetOrigin(target.width / 2, target.height / 2);
            target.SetScaleXY(0.2f, 0.2f);
            target.SetXY(Globals.player.x, Globals.player.y);
            
            grenade = new Sprite("circle.png", true, false);
            grenade.SetOrigin(grenade.width / 2, grenade.height / 2);
            Globals.bulletHolder.AddChild(grenade);
            Vector2 globalLauncherPos = TransformPoint(0, 0);
            grenade.SetXY(globalLauncherPos.x, globalLauncherPos.y);
            grenade.SetScaleXY(0.2f, 0.2f);
            grenade.AddChild(new Tween(Tween.Property.x, grenade.x, target.x, 3, Tween.Curves.Linear));
            grenade.AddChild(new Tween(Tween.Property.y, grenade.y, target.y, 3, Tween.Curves.Linear));
            grenade.AddChild(new Tween(Tween.Property.scale, 0.2f, 1, 3, Tween.Curves.SinBounce));
            grenadeTravelTimer.Start();

        }

        public void Update()
        {
            if (grenadeTravelTimer.finishedThisFrame)
            {
                grenade.LateDestroy();
                target.LateDestroy();
                shooting = false;
            }
        }


    }
}
