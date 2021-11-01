using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace myEngine
{
    public class Player : Entity
    {
        private Vector2[] shipVertices = {
            new Vector2(0, 10),
            new Vector2(10, -10),
            new Vector2(4, -6),
            new Vector2(-4, -6),
            new Vector2(-10, -10)
        };

        private Vector2[] reactorVertices = {
            new Vector2(4, -6),
            new Vector2(-4, -6),
            new Vector2(0, -20),
        };

        //FIELDS
        private SoundEffect rocketSound;
        private SoundEffectInstance rocketSoundInstance;

        private bool isMoving;

        private ParticleEngine particleEngine;

        //CONSTRUCTOR
        public Player()
        {
            for (int i = 0; i < shipVertices.Length; i++)
            {
                shipVertices[i].Y += 4;
            }

            transform.scale.X = 2;

            color = Color.LightBlue;

            vertices = shipVertices;

            rocketSound = Ressources.Load<SoundEffect>("myContent/Audio/Explosion8");
            rocketSoundInstance = rocketSound.CreateInstance();

            isMoving = false;

            Delay delay = new Delay(100, () => reactorVertices[2] = new Vector2(Util.RandomBetween(-2, 2), Util.RandomBetween(-20, -14)));

            ParticleProfile pp = new ParticleProfile();
            pp.loopMode = true;
            pp.burstAmount = 10;
            pp.emissionRate = 10;
            pp.particle.Size = 0.1f;
            particleEngine = new ParticleEngine(pp, transform.position);

            Matrix m = Matrix.CreateScale(transform.scale.X) * Matrix.CreateRotationZ(MathHelper.ToRadians(transform.rotation)) * Matrix.CreateTranslation(transform.position.X, transform.position.Y, 0f);
            circleRadius = Util.FindCollisionRadius(Util.ConvertVerticesTransform(vertices, m));

            collider = new CircleCollider(transform, circleRadius);
            this.AddComponent(collider);
        }

        public void ApplyForce(float amount)
        {
            Vector2 forceDir = new Vector2(MathF.Cos(MathHelper.ToRadians(transform.rotation) + MathHelper.PiOver2), 
                                           MathF.Sin(MathHelper.ToRadians(transform.rotation) + MathHelper.PiOver2));
            velocity += forceDir * amount * Time.deltaTime;
        }

        //METHODS
        public override void Update()
        {
            if (Input.GetKey(Keys.E))
                transform.rotation -= 180 * Time.deltaTime;
            if (Input.GetKey(Keys.A))
                transform.rotation += 180 * Time.deltaTime;

            /*
            if(Input.GetKeyDown(Keys.Space))
            {
                ApplyForce(speed * 100);
            }
            */

            if (Input.GetKey(Keys.Space))
            {
                ApplyForce(speed);

                isMoving = true;

                particleEngine.isActive = false;

                //PLAY SOUND
                if (rocketSoundInstance.State != SoundState.Playing)
                {
                    rocketSoundInstance.Volume = 0.2f;
                    rocketSoundInstance.Play();
                }
            }
            else
            {
                isMoving = false;

                particleEngine.isActive = false;

                if (rocketSoundInstance.State == SoundState.Playing)
                    rocketSoundInstance.Stop();
            }

            Matrix m = Matrix.Identity * Matrix.CreateTranslation(0, -50, 0) * transform.GetMatrix();
            Util.DecomposeMatrix(m, particleEngine.transform);

            base.Update();
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            Shapes s = new Shapes(Engine.game);

            s.Begin(Scene_Asteroid.cam);

            if(isMoving)
            {
                Matrix m2 = Matrix.CreateScale(transform.scale.X) * Matrix.CreateRotationZ(MathHelper.ToRadians(transform.rotation)) * Matrix.CreateTranslation(transform.position.X, transform.position.Y, 0f);
                s.DrawPolygon(reactorVertices, m2, 2, Color.Orange);

                DrawGhost(reactorVertices, transform, Color.Orange);
            }
            
            s.End();

            base.Draw(sprite, matrix);
        }
    }
}
