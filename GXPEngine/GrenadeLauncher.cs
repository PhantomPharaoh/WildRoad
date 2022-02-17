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

        Timer shootCooldownTimer;

        public GrenadeLauncher() : base("grenadelaunchercar.png", "enemy_hitbox.png", "grenadecar_white.png", 3, 1, 3)
        {
            shootCooldownTimer = new Timer(2, false);
            AddChild(shootCooldownTimer);
        }

        public override void StartShooting()
        {
            shooting = true;

            Grenade grenade = new Grenade(TransformPoint(0, 0), new Vector2(Globals.player.x, Globals.player.y), 2);
            Globals.bulletHolder.AddChild(grenade);

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
            }

            if (isDestroyed)
            {
                Explosion deathExplosion = new Explosion();
                Globals.bulletHolder.AddChild(deathExplosion);
                deathExplosion.SetXY(TransformPoint(0, 0).x, TransformPoint(0, 0).y);
                Globals.score += 40;

                LateDestroy();
            }
        }


    }
}
