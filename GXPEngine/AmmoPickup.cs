﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class AmmoPickup : Sprite
    {
        public AmmoPickup() : base("circle.png", true, true)
        {
            SetOrigin(this.width / 2, this.height / 2);
            SetScaleXY(0.3f, 0.3f);
        }

        public void Update()
        {
            float delta = Time.deltaTime / 1000f;

            y += 3 * delta * 60;

            if (y > game.height + 100)
            {
                LateDestroy();
            }
        }

    }
}
