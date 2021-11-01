using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine
{
    public abstract class Entity : GameObject
    {
        //FIELDS
        protected Vector2[] vertices;

        protected float speed = 50;
        protected float maxSpeed = 100;

        protected Vector2 velocity;

        protected float rotationVelocity;

        protected Color color;

        protected float circleRadius;
        protected CircleCollider collider;
        protected Color circleColor = Color.White;

        //CONSTRUCTOR


        //METHODS
        public override void Update()
        {
            this.transform.position += velocity * Time.deltaTime;
            this.transform.rotation += rotationVelocity * 0.1f * Time.deltaTime;

            if (transform.position.X > Settings.SCREEN_WIDTH / 2) { transform.position.X -= Settings.SCREEN_WIDTH; }
            if (transform.position.X < -Settings.SCREEN_WIDTH / 2) { transform.position.X += Settings.SCREEN_WIDTH; }

            if (transform.position.Y > Settings.SCREEN_HEIGHT / 2) { transform.position.Y -= Settings.SCREEN_HEIGHT; }
            if (transform.position.Y < -Settings.SCREEN_HEIGHT / 2) { transform.position.Y += Settings.SCREEN_HEIGHT; }
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            Shapes s = new Shapes(Engine.game);

            s.Begin(Scene_Asteroid.cam);

            Matrix m = Matrix.CreateScale(transform.scale.X) * Matrix.CreateRotationZ(MathHelper.ToRadians(transform.rotation)) * Matrix.CreateTranslation(transform.position.X, transform.position.Y, 0f);
            s.DrawPolygon(vertices, m, 2, color);

            circleRadius = Util.FindCollisionRadius(Util.ConvertVerticesTransform(vertices, m));
            s.DrawCircle(transform.position, circleRadius, 12, 2, circleColor);

            s.End();

            //DrawGhost(vertices, transform, color);
            //DrawGhostCircle(circleRadius, circleColor);

            circleColor = Color.LightGreen;
        }

        protected void DrawGhost(Vector2[] vertices, Transform transform, Color color)
        {
            Shapes s = new Shapes(Engine.game);
            s.Begin(Scene_Asteroid.cam);

            Matrix m2 = Matrix.CreateScale(transform.scale.X) * Matrix.CreateRotationZ(MathHelper.ToRadians(transform.rotation)) * Matrix.CreateTranslation(transform.position.X + Settings.SCREEN_WIDTH, transform.position.Y, 0f);
            s.DrawPolygon(vertices, m2, 2, color);
            Matrix m3 = Matrix.CreateScale(transform.scale.X) * Matrix.CreateRotationZ(MathHelper.ToRadians(transform.rotation)) * Matrix.CreateTranslation(transform.position.X - Settings.SCREEN_WIDTH, transform.position.Y, 0f);
            s.DrawPolygon(vertices, m3, 2, color);

            Matrix m4 = Matrix.CreateScale(transform.scale.X) * Matrix.CreateRotationZ(MathHelper.ToRadians(transform.rotation)) * Matrix.CreateTranslation(transform.position.X, transform.position.Y + Settings.SCREEN_HEIGHT, 0f);
            s.DrawPolygon(vertices, m4, 2, color);
            Matrix m5 = Matrix.CreateScale(transform.scale.X) * Matrix.CreateRotationZ(MathHelper.ToRadians(transform.rotation)) * Matrix.CreateTranslation(transform.position.X, transform.position.Y - Settings.SCREEN_HEIGHT, 0f);
            s.DrawPolygon(vertices, m5, 2, color);

            s.End();
        }

        protected void DrawGhostCircle(float circleRadius, Color color)
        {
            Shapes s = new Shapes(Engine.game);
            s.Begin(Scene_Asteroid.cam);

            Vector2 pos = new Vector2(transform.position.X + Settings.SCREEN_WIDTH, transform.position.Y);
            s.DrawCircle(pos, circleRadius, 12, 2, color);

            pos = new Vector2(transform.position.X - Settings.SCREEN_WIDTH, transform.position.Y);
            s.DrawCircle(pos, circleRadius, 12, 2, color);

            pos = new Vector2(transform.position.X, transform.position.Y + Settings.SCREEN_HEIGHT);
            s.DrawCircle(pos, circleRadius, 12, 2, color);

            pos = new Vector2(transform.position.X, transform.position.Y - Settings.SCREEN_HEIGHT);
            s.DrawCircle(pos, circleRadius, 12, 2, color);

            s.End();
        }

        public override void OnCollision(Collider2D other)
        {
            ((Entity)other.gameObject).circleColor = Color.Red;
        }
    }
}
