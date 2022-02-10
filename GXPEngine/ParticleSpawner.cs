using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class ParticleSpawner : GameObject
    {

        float moveDirectionAngle = 0;
        float minMoveSpeed = 0;
        float maxMoveSpeed = 0;
        float minSpreading = 0;
        float maxSpreading = 0;
        float velocityDamp = 0;//between 0 and 1

        Vector2 gravity = Vector2.ZERO;

        float minRotationSpeed = 0;
        float maxRotationSpeed = 0;
        float rotationDamp = 0;
        float minInitialAngle = 0;
        float maxInitialAngle = 0;

        float minScaling = 0;
        float maxScaling = 0;
        float scalingDamp = 0;
        float minInitialScale = 1;
        float maxInitialScale = 1;

        float initialAlpha = 1;
        float alphaModulation = 0;

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
        public ParticleSpawner(string[] images, int amount = 0, float minTimeBetween = 1, float maxTimeBetween = 1)
        {
            this.images = images;
            this.amount = amount;
            this.minTimeBetween = minTimeBetween;
            this.maxTimeBetween = maxTimeBetween;
            spawnTimer = new Timer(minTimeBetween, false);
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

        public ParticleSpawner ConfigureMovement(
            float moveDirectionAngle, float minMoveSpeed,
            float maxMoveSpeed, float minSpreading,
            float maxSpreading, float velocityDamp)
        {
            this.moveDirectionAngle = moveDirectionAngle;
            this.minMoveSpeed = minMoveSpeed;
            this.maxMoveSpeed = maxMoveSpeed;
            this.minSpreading = minSpreading;
            this.maxSpreading = maxSpreading;
            this.velocityDamp = velocityDamp;
            return this;
        }
        public ParticleSpawner ConfigureGravity(Vector2 gravity)
        {
            this.gravity = gravity;
            return this;
        }
        public ParticleSpawner ConfigureScaling(
            float minScaling, float maxScaling,
            float scalingDamp, float minInitialScale, float maxInitialScale)
        {
            this.minScaling = minScaling;
            this.maxScaling = maxScaling;
            this.scalingDamp = scalingDamp;
            this.minInitialScale = minInitialScale;
            this.maxInitialScale = maxInitialScale;
            return this;
        }
        public ParticleSpawner ConfigureRotation(
            float minRotationSpeed, float maxRotationSpeed,
            float rotationDamp, float minInitialAngle, float maxInitialAngle)
        {
            this.minRotationSpeed = minRotationSpeed;
            this.maxRotationSpeed = maxRotationSpeed;
            this.rotationDamp = rotationDamp;
            this.minInitialAngle = minInitialAngle;
            this.maxInitialAngle = maxInitialAngle;
            return this;
        }
        public ParticleSpawner ConfigureAlpha(
            float initialAlpha, float alphaModulation)
        {
            this.initialAlpha = initialAlpha;
            this.alphaModulation = alphaModulation;
            return this;
        }
        public ParticleSpawner ConfigureLifeTime(
            float minLifeTime, float maxLifeTime)
        {
            this.minLifeTime = minLifeTime;
            this.maxLifeTime = maxLifeTime;
            return this;
        }

        public void Update()
        {

            if (spawnTimer.finishedThisFrame && amountCreated < amount)
            {
                CreateParticle();
                amountCreated++;
                spawnTimer.SetWaitTime(minTimeBetween + maxTimeBetween * (float)random.NextDouble());
                spawnTimer.Start();
            }


        }

        void CreateParticle()
        {
            int imageIndex = 0;
            if (images.Length > 1) imageIndex = random.Next(images.Length);

            Particle particle = new Particle(//passing the particle it's properties
                images[imageIndex],
                minLifeTime + maxLifeTime * (float)random.NextDouble(),
                Vector2.UP.Rotated(minInitialAngle + maxInitialAngle * (float)random.NextDouble(), true),
                gravity,
                velocityDamp,
                minRotationSpeed + maxRotationSpeed * (float)random.NextDouble(),
                rotationDamp,
                minScaling + maxScaling * (float)random.NextDouble(),
                scalingDamp,
                alphaModulation);

            particle.rotation = minInitialAngle + maxInitialAngle * (float)random.NextDouble();
            particle.scale = maxInitialScale + maxInitialScale * (float)random.NextDouble();
            particle.alpha = initialAlpha;

            AddChild(particle);
        }

    }
}
