using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class Rocket : AnimationSprite
    {


        public Rocket() : base("rocket.png", 3, 1, 3, true, true)
        {
            SetOrigin(this.width / 2, this.height / 2);
            SetScaleXY(2, 2);
        }

        public void Update()
        {
            float delta = Time.deltaTime / 1000f;
            Animate();

            y -= 10 * delta * 60;
            if (y < -100)
            {
                LateDestroy();
            }

        }



    }
}
