using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class RocketLauncher : Enemy
    {

        Timer shootCooldownTimer;
        Sprite shootIndicator;

        public RocketLauncher() : base("rocketlaunchercar.png", "player_hitbox.png", "rocketcar_white.png", 3, 1, 3)
        {
            shootCooldownTimer = new Timer(3.1f, false);
            AddChild(shootCooldownTimer);
            shootIndicator = new Sprite("rocketIndicator.png", true, false);
            AddChild(shootIndicator);
            shootIndicator.SetOrigin(shootIndicator.width/2, 0);
            shootIndicator.rotation = 180;
            shootIndicator.alpha = 0;
        }

        public override void StartShooting()
        {
            shooting = true;

            shootIndicator.alpha = 0;
            shootIndicator.AddChild(new Tween(Tween.Property.scale, 1, 8, 3f, Tween.Curves.ExpBounce));
            shootIndicator.AddChild(new Tween(Tween.Property.alpha, 0, 1, 3f, Tween.Curves.SinDamp));

            shootCooldownTimer.Start();
        }

        public void Update()
        {
            Shake(Time.deltaTime / 1000f);
            HitAnimation();
            EmitSparks();
            visibleCar.Animate();

            if (shootCooldownTimer.finishedThisFrame)
            {
                shooting = false;
                shootIndicator.alpha = 0;

                Rocket rocket = new Rocket();
                Globals.bulletHolder.AddChild(rocket);
                rocket.SetXY(TransformPoint(0,0).x, TransformPoint(0, 0).y);

            }
        }

    }
}
