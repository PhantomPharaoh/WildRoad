using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Enemy : Car
    {

        public Enemy() : base("car_2.png", "square.png")
        {
            SetOrigin(this.width / 2, this.height / 2);
            SetScaleXY(1.3f, 1.3f);
        }

        public void Update()
        {
            Shake(Time.deltaTime / 1000f);
        }

    }
}
