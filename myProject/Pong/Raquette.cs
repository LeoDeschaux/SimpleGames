using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using myEngine.myProject.Pong;

namespace myEngine.myProject.Pong
{
    public class Raquette : GameObject
    {
        //FIELDS
        public Sprite sprite;
        public Collider2D collider;

        //CONSTRUCTOR
        public Raquette()
        {
            sprite = new Sprite(new Vector2(0, 0), new Vector2(40, 150));
            sprite.transform = this.transform;

            //collider = new Collider2D(sprite);
        }

        //METHODS

        //UPDATE & DRAW
        public override void Update()
        {
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix matrix)
        {
        }
    }
}