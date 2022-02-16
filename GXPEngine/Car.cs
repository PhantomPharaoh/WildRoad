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
        protected bool doEmitSparks = false;
        protected Vector2 sparksPosition = Vector2.ZERO;
        public float shakiness = 1f;
        public float stiffness = 0.2f;
        protected Random random;
        Sprite hitIndicator;
        ParticleEmitter sparks;

        public Car(string texturePath, string hitboxPath, string hitIdicatorPath, int cols, int rows, int frames) : base(hitboxPath, true, true)
        {
            SetOrigin(this.width/2, this.height/2);
            alpha = 0f;

            random = new Random();

            visibleCar = new AnimationSprite(texturePath, cols, rows, frames, true, false);
            AddChild(visibleCar);
            visibleCar.SetOrigin(visibleCar.width / 2, visibleCar.height / 2);

            hitIndicator = new Sprite(hitIdicatorPath, true, false);
            visibleCar.AddChild(hitIndicator);
            hitIndicator.SetOrigin(hitIndicator.width / 2, hitIndicator.height / 2);
            hitIndicator.alpha = 0;

            sparks = new ParticleEmitter(
                new string[] { "spark.png" }, 20, 0, 0)
                .ConfigureGravity(Vector2.UP * 0.1f)
                .ConfigureMovement(0, 1, 2, 180, 0)
                .ConfigureAlpha(1, -0.01f)
                .ConfigureScaling(0, 0, 0, 0.3f, 0.3f)
                .ConfigureLifeTime(2f, 2f);
            AddChild(sparks);
        }

        protected void Shake(float delta)//call this every frame
        {
            float appliedShake = shakiness;// * delta * 60;
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

        protected void EmitSparks()
        {
            if (doEmitSparks)
            {
                sparks.x = sparksPosition.x;
                sparks.y = sparksPosition.y;
                sparks.Emit();
                doEmitSparks = false;
            }
        }

    }
}
