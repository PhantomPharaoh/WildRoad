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
            SetScaleXY(0.2f, 0.2f);
        }


    }
}
