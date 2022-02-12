using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class Car : Sprite
    {
        Sprite visibleCar;
        public float shakiness = 1f;
        public float stiffness = 0.2f;
        Random random;

        public Car(string texturePath, string hitboxPath) : base(hitboxPath, true, true)
        {
            SetOrigin(this.width/2, this.height/2);
            alpha = 0.5f;

            random = new Random();

            visibleCar = new Sprite(texturePath, true, false);
            AddChild(visibleCar);
            visibleCar.SetOrigin(visibleCar.width / 2, visibleCar.height / 2);
        }

        protected void Shake(float delta)
        {
            float appliedShake = shakiness * delta * 60;
            visibleCar.x += MathUtils.Map((float)random.NextDouble(), 0, 1, -appliedShake, appliedShake);
            visibleCar.y += MathUtils.Map((float)random.NextDouble(), 0, 1, -appliedShake, appliedShake);

            visibleCar.x = MathUtils.Lerp(visibleCar.x, 0, stiffness * delta * 60);
            visibleCar.y = MathUtils.Lerp(visibleCar.y, 0, stiffness * delta * 60);

            visibleCar.rotation += MathUtils.Map((float)random.NextDouble(), 0, 1, -appliedShake, appliedShake);
            visibleCar.rotation = MathUtils.Lerp(visibleCar.rotation, 0, stiffness * delta * 60);
        }

    }
}
