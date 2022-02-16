using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Obstacle : Sprite
    {

        public List<GameObject> collidedWith = new List<GameObject>();

        public Obstacle(string texturePath) : base(texturePath, true, true)
        {
            SetOrigin(this.width/2, this.height/2);
            SetScaleXY(0.07f, 0.07f);
        }

        public void Update()
        {
            float delta = Time.deltaTime / 1000f;

            y += Globals.scrollSpeed * game.height * delta;

            if (y > game.height + 100) LateDestroy();
        }



    }
}
