using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine
{
    public class Asteroid : Entity
    {

        //CONSTRUCTOR
        public Asteroid(Vector2 position)
        {
            const int minPoints = 6;
            const int maxPoints = 12;

            this.transform.position = position;
            
            Random random = new Random();

            int points = random.Next(minPoints, maxPoints);

            vertices = new Vector2[points];

            float deltaAngle = MathHelper.TwoPi / (float)points;
            float angle = 0f;

            float minDist = 12f;
            float maxDist = 18;

            for (int i = 0; i < points; i++)
            {
                float dist = Util.RandomBetween(random, minDist, maxDist);

                float x = MathF.Cos(angle) * dist;
                float y = MathF.Sin(angle) * dist;

                vertices[i] = new Vector2(x, y);

                angle += deltaAngle;
            }

            float dirX = (float)(random.NextDouble()*2) - 1;
            float dirY = (float)(random.NextDouble()*2) - 1; 
            velocity = new Vector2(dirX, dirY) * speed;

            rotationVelocity = (random.Next(0, 2)*2) - 1;

            speed = (float)(random.NextDouble() * maxSpeed);
            speed = MathHelper.Clamp(speed, 1, 45);

            transform.scale.X = Util.RandomBetween(random, 2, 8);

            this.color = Color.Brown;

            Matrix m = Matrix.CreateScale(transform.scale.X) * Matrix.CreateRotationZ(MathHelper.ToRadians(transform.rotation)) * Matrix.CreateTranslation(transform.position.X, transform.position.Y, 0f);
            circleRadius = Util.FindCollisionRadius(Util.ConvertVerticesTransform(vertices, m));

            collider = new CircleCollider(transform, circleRadius);
            this.AddComponent(collider);
        }

        //METHODS
        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            base.Draw(sprite, matrix);
        }
    }
}
