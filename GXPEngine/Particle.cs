﻿using System;
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
        float velocityAcceleration;
        float rotationSpeed;
        float rotationAcceleration;
        float scaling;
        float scalingAcceleration;
        float alphaModulation;

        float spawnTime;
        float lifeTime;

        public Particle(
            string imagePath,float lifeTime,
            Vector2 velocity, Vector2 gravity,
            float velocityAcceleration, float rotationSpeed, 
            float rotationAcceleration, float scaling,
            float scalingAcceleration, float alphaModulation) : base(imagePath, true, false)
        {
            this.velocity = velocity;
            this.gravity = gravity;
            this.rotationSpeed = rotationSpeed;
            this.velocityAcceleration = velocityAcceleration;
            this.rotationAcceleration = rotationAcceleration;
            this.scaling = scaling;
            this.scalingAcceleration = scalingAcceleration;
            this.alphaModulation = alphaModulation;

            this.lifeTime = lifeTime * 1000f;
            this.spawnTime = Time.now;
        }

        public void Update()
        {
            float delta = Time.deltaTime / 1000f;
            
            velocity += gravity * delta * 60;
            velocity += velocity.Normalized() * velocityAcceleration * delta * 60;
            
            x += velocity.x * delta * 60;
            y += velocity.y * delta * 60;
            
            rotationSpeed += rotationAcceleration * delta * 60;
            rotation += rotationSpeed * delta * 60;
            
            scaling += scalingAcceleration * delta * 60;
            scale += scaling * delta * 60;
            scale = Mathf.Max(scale, 0);
            
            alpha += alphaModulation * delta * 60;
            alpha = Mathf.Clamp(alpha, 0, 1);
            
            if (Time.now - spawnTime > lifeTime) LateDestroy();
        }


    }
}
