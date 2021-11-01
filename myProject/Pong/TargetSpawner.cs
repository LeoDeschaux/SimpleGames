using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using myEngine.myProject.Pong;
using myEngine;

namespace zCurrentProject
{
    public class TargetSpawner : EmptyObject
    {
        //FIELDS
        Rectangle zoneRectangle;

        public Target target;

        private static Random random;

        private bool isActive = false;

        //CONSTRUCTOR
        public TargetSpawner()
        {
            random = new Random();

            Vector2 position = Settings.GetScreenCenter();
            Vector2 dimension = new Vector2(500, Settings.SCREEN_HEIGHT - 100);
            zoneRectangle = new Rectangle((int)(position.X - (dimension.X / 2)), (int)(position.Y - (dimension.Y / 2)), (int)dimension.X, (int)dimension.Y);
        }

        Delay delay;
        public void SpawnNewTarget()
        {
            if(delay != null)
                delay.Destroy();

            delay = new Delay(2000, () =>
            {
                SpawnTarget();
            });
        }

        public void Start()
        {
            isActive = true;
            SpawnNewTarget();
        }

        //METHODS
        public void SpawnTarget()
        {
            if (!isActive)
                return;

            if (target == null || target.disposed)
            {
                target = new Target();
                target.transform.position = new Vector2(zoneRectangle.X + (zoneRectangle.Width * (float)random.NextDouble()),
                                                        zoneRectangle.Y + (zoneRectangle.Height * (float)random.NextDouble()));
                target.sprite.transform.position = target.transform.position;
                target.sprite.drawOrder = 500;
            }
        }

        //UPDATE & DRAW
        public override void Update()
        {
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
        }
    }
}
