using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Enemy : Sprite
    {

        public Enemy() : base("car_1.png", true, true)
        {
            SetOrigin(this.width / 2, this.height / 2);
            SetScaleXY(1.5f, 1.5f);
        }



    }
}
