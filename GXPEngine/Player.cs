using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Player : Sprite
    {

        const float steerSpeed = 150f;

        public Player() : base("car_1.png", true, true)
        {
            SetOrigin(this.width/2, this.height/2);
            SetScaleXY(1.5f, 1.5f);
        }

        public void Update()
        {
            float delta = Time.deltaTime / 1000f;

            int inputDirection = 0;
            if (Input.GetKey(Key.RIGHT)) inputDirection += 1;
            if (Input.GetKey(Key.LEFT)) inputDirection -= 1;

            x += inputDirection * steerSpeed * delta;

            x = Mathf.Clamp(x, game.width*0.4f, game.width*0.6f);
        }

    }
}
