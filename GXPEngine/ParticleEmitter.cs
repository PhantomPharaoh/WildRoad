using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class ParticleEmitter : GameObject
    {

        float moveDirectionAngle = 0;
        float minMoveSpeed = 0;
        float maxMoveSpeed = 0;
        float spreading = 0;
        float velocityAcceleration = 0;

        Vector2 gravity = Vector2.ZERO;

        float minRotationSpeed = 0;
        float maxRotationSpeed = 0;
        float rotationAcceleration = 0;
        float minInitialAngle = 0;
        float maxInitialAngle = 0;

        float minScaling = 0;
        float maxScaling = 0;
        float scalingAcceleration = 0;
        float minInitialScale = 1;
        float maxInitialScale = 1;

        float initialAlpha = 1;
        float alphaModulation = 0;

        float areaExtendsX = 0;
        float areaExtendsY = 0;

        Timer spawnTimer;
        Random random;

        string[] images;
        int amount = 0;
        float minLifeTime = 1;
        float maxLifeTime = 1;
        int amountCreated = 0;
        float minTimeBetween = 1;
        float maxTimeBetween = 1;

        /// <summary></summary>
        /// <param name="amount">if 0 it will continnue spawning them</param>
        /// <param name="minTimeBetween">minimum time between particles (shouldn't be 0 while amount is 0)</param>
        /// <param name="maxTimeBetween">maximum time between particles</param>
        /// <returns></returns>
        public ParticleEmitter(string[] images, int amount = 0, float minTimeBetween = 1, float maxTimeBetween = 1)
        {
            this.images = images;
            this.amount = amount;
            this.minTimeBetween = minTimeBetween;
            this.maxTimeBetween = maxTimeBetween;
            spawnTimer = new Timer(minTimeBetween, false);
            AddChild(spawnTimer);
            random = new Random();
        }

        public void Emit()
        {
            if (minTimeBetween == 0f && maxTimeBetween == 0f)
            {
                for (int i = 0; i < amount; i++)
                {
                    CreateParticle();
                }
            }
            else
            {
                spawnTimer.SetWaitTime(minTimeBetween + maxTimeBetween * (float)random.NextDouble());
                spawnTimer.Start();
                amountCreated = 0;
            }
        }

        public ParticleEmitter ConfigureMovement(
            float moveDirectionAngle, float minMoveSpeed,
            float maxMoveSpeed, float spreading,
            float velocityAcceleration)
        {
            this.moveDirectionAngle = moveDirectionAngle;
            this.minMoveSpeed = minMoveSpeed;
            this.maxMoveSpeed = maxMoveSpeed;
            this.spreading = spreading;
            this.velocityAcceleration = velocityAcceleration;
            return this;
        }
        public ParticleEmitter ConfigureGravity(Vector2 gravity)
        {
            this.gravity = gravity;
            return this;
        }
        public ParticleEmitter ConfigureScaling(
            float minScaling, float maxScaling,
            float scalingAcceleration, float minInitialScale, float maxInitialScale)
        {
            this.minScaling = minScaling;
            this.maxScaling = maxScaling;
            this.scalingAcceleration = scalingAcceleration;
            this.minInitialScale = minInitialScale;
            this.maxInitialScale = maxInitialScale;
            return this;
        }
        public ParticleEmitter ConfigureRotation(
            float minRotationSpeed, float maxRotationSpeed,
            float rotationAcceleration, float minInitialAngle, float maxInitialAngle)
        {
            this.minRotationSpeed = minRotationSpeed;
            this.maxRotationSpeed = maxRotationSpeed;
            this.rotationAcceleration = rotationAcceleration;
            this.minInitialAngle = minInitialAngle;
            this.maxInitialAngle = maxInitialAngle;
            return this;
        }
        public ParticleEmitter ConfigureAlpha(
            float initialAlpha, float alphaModulation)
        {
            this.initialAlpha = initialAlpha;
            this.alphaModulation = alphaModulation;
            return this;
        }
        public ParticleEmitter ConfigureLifeTime(
            float minLifeTime, float maxLifeTime)
        {
            this.minLifeTime = minLifeTime;
            this.maxLifeTime = maxLifeTime;
            return this;
        }
        public ParticleEmitter ConfigureSpawnArea(
            float areaExtendsX, float areaExtendsY)
        {
            this.areaExtendsX = areaExtendsX;
            this.areaExtendsY = areaExtendsY;
            return this;
        }

        public void Update()
        {

            if (spawnTimer.finishedThisFrame)
            {
                if (amountCreated < amount || amount == 0)
                {
                    CreateParticle();
                    amountCreated++;
                    spawnTimer.SetWaitTime(minTimeBetween + maxTimeBetween * (float)random.NextDouble());
                    spawnTimer.Start();
                }
            }


        }

        void CreateParticle()
        {
            int imageIndex = 0;
            if (images.Length > 1) imageIndex = random.Next(images.Length);

            Particle particle = new Particle(//passing the particle it's properties
                images[imageIndex],
                MathUtils.Map((float)random.NextDouble(), 0, 1, minLifeTime, maxLifeTime),
                Vector2.UP.Rotated(moveDirectionAngle, true).Rotated(MathUtils.Map((float)random.NextDouble(), 0, 1, -spreading, spreading)) * MathUtils.Map((float)random.NextDouble(), 0, 1, minMoveSpeed, maxMoveSpeed),
                gravity,
                velocityAcceleration,
                MathUtils.Map((float)random.NextDouble(), 0, 1, minRotationSpeed, maxRotationSpeed),
                rotationAcceleration,
                MathUtils.Map((float)random.NextDouble(), 0, 1, minScaling, maxScaling),
                scalingAcceleration,
                alphaModulation);

            particle.SetOrigin(particle.width/2, particle.height / 2);
            particle.rotation = MathUtils.Map((float)random.NextDouble(), 0, 1, minInitialAngle, maxInitialAngle);
            particle.scale = MathUtils.Map((float)random.NextDouble(), 0, 1, minInitialScale, maxInitialScale);
            particle.alpha = initialAlpha;
            particle.x += MathUtils.Map((float)random.NextDouble(), 0, 1, -areaExtendsX, areaExtendsX);
            particle.y += MathUtils.Map((float)random.NextDouble(), 0, 1, -areaExtendsY, areaExtendsY);

            AddChild(particle);
        }

    }
}
