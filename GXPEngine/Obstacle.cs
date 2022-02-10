using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Obstacle : Sprite
    {

        public Obstacle() : base("circle.png")
        {
            SetOrigin(this.width/2, this.height/2);
            SetScaleXY(0.4f, 0.4f);
        }

        public void Update()
        {
            float delta = Time.deltaTime / 1000f;

            y += Globals.scrollSpeed * game.height * delta;

            if (y > game.height + 100) LateDestroy();
        }



    }
}
