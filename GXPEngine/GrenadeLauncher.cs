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

        Vector2 targetPosition = Vector2.ZERO;

        public GrenadeLauncher() : base()
        {

        }

        public override void StartShooting()
        {
            targetPosition = new Vector2(Globals.player.x, Globals.player.y);

            Sprite target = new Sprite("target.png", true, false);
            parent.AddChild(target);
            target.SetOrigin(target.width / 2, target.height / 2);
            target.SetScaleXY(0.2f, 0.2f);
            target.SetXY(targetPosition.x - parent.x, targetPosition.y - parent.y);


        }



    }
}
