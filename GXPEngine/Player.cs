using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Player : Sprite
    {

        public Player() : base("square.png")
        {
            SetOrigin(this.width/2, this.height/2);
        }



    }
}
