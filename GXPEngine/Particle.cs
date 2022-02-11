using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class Particle : Sprite
    {
        Vector2 velocity;
        Vector2 gravity;
        float velocityDamp;//between 0 and 1
        float rotationSpeed;
        float rotationDamp;
        float scaling;
        float scalingDamp;
        float alphaModulation;

        float spawnTime;
        float lifeTime;

        public Particle(
            string imagePath,float lifeTime,
            Vector2 velocity, Vector2 gravity,
            float velocityDamp, float rotationSpeed, 
            float rotationDamp, float scaling,
            float scalingDamp, float alphaModulation) : base(imagePath, true, false)
        {
            this.velocity = velocity;
            this.gravity = gravity;
            this.rotationSpeed = rotationSpeed;
            this.velocityDamp = velocityDamp;
            this.rotationDamp = rotationDamp;
            this.scaling = scaling;
            this.scalingDamp = scalingDamp;
            this.alphaModulation = alphaModulation;

            this.lifeTime = lifeTime * 1000f;
            this.spawnTime = Time.now;
        }

        public void Update()
        {
            float delta = Time.deltaTime / 1000f;
            
            velocity += gravity * delta * 60;
            //velocity = MathUtils.Lerp();
            
            x += velocity.x * delta * 60;
            y += velocity.y * delta * 60;
            
            //rotationSpeed *= 1 - rotationDamp;
            rotation += rotationSpeed * delta * 60;
            
            //scaling *= 1 - scalingDamp * delta * 60;
            scale += scaling * delta * 60;
            scale = Mathf.Max(scale, 0);
            
            alpha += alphaModulation * delta * 60;
            alpha = Mathf.Clamp(alpha, 0, 1);
            Console.WriteLine(alpha);
            
            if (Time.now - spawnTime > lifeTime) LateDestroy();
        }


    }
}
