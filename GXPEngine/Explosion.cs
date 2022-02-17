using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class Explosion : AnimationSprite
    {

        Timer explosionTimer;
        public bool hitPlayer = false;

        Sound explosionSound;

        public Explosion() : base("explosion.png", 3, 3, 8, true, true)
        {
            SetOrigin(this.width/2, this.height/2);
            explosionTimer = new Timer(0.5f, true);
            AddChild(explosionTimer);
            explosionSound = new Sound("explosion.wav");
            explosionSound.Play();
        }

        public void Update()
        {
            Animate(0.25f);

            if (explosionTimer.finishedThisFrame)
            {
                LateDestroy();
            }
        }



    }
}
