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

        public GrenadeLauncher() : base()
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
            if (shootCooldownTimer.finishedThisFrame)
            {
                shooting = false;
            }
        }


    }
}
