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
        protected AnimationSprite visibleCar;
        protected bool doHitAnimation = false;
        public float shakiness = 1f;
        public float stiffness = 0.2f;
        Random random;
        Sprite hitIndicator;

        public Car(string texturePath, string hitboxPath, string hitIdicatorPath, int cols, int rows, int frames) : base(hitboxPath, true, true)
        {
            SetOrigin(this.width/2, this.height/2);
            alpha = 0.5f;

            random = new Random();

            visibleCar = new AnimationSprite(texturePath, cols, rows, frames, true, false);
            AddChild(visibleCar);
            visibleCar.SetOrigin(visibleCar.width / 2, visibleCar.height / 2);

            hitIndicator = new Sprite(hitIdicatorPath, true, false);
            visibleCar.AddChild(hitIndicator);
            hitIndicator.SetOrigin(hitIndicator.width / 2, hitIndicator.height / 2);
            hitIndicator.alpha = 0;
        }

        protected void Shake(float delta)//call this every frame
        {
            float appliedShake = shakiness * delta * 60;
            visibleCar.x += MathUtils.Map((float)random.NextDouble(), 0, 1, -appliedShake, appliedShake);
            visibleCar.y += MathUtils.Map((float)random.NextDouble(), 0, 1, -appliedShake, appliedShake);

            visibleCar.x = MathUtils.Lerp(visibleCar.x, 0, stiffness * delta * 60);
            visibleCar.y = MathUtils.Lerp(visibleCar.y, 0, stiffness * delta * 60);

            visibleCar.rotation += MathUtils.Map((float)random.NextDouble(), 0, 1, -appliedShake, appliedShake);
            visibleCar.rotation = MathUtils.Lerp(visibleCar.rotation, 0, stiffness * delta * 60);
        }

        protected void HitAnimation()//call this every frame
        {
            if (doHitAnimation)
            {
                hitIndicator.AddChild(new Tween(Tween.Property.alpha, 1, 0, 0.3f, Tween.Curves.EaseOut));
                doHitAnimation = false;
            }
        }

    }
}
