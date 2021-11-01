using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Arkanoid
{
    public class Block : GameObject
    {
        //FIELDS
        Sprite sprite;
        public int maxLife = 3;
        public int currentLife;

        //CONSTRUCTOR
        public Block(Vector2 startPos, Vector2 dimension)
        {
            transform.position = startPos;

            sprite = new Sprite();
            sprite.transform = this.transform;
            sprite.color = Color.LightGreen;
            sprite.dimension = new Vector2(dimension.X, dimension.Y);

            AddComponent(new BoxCollider(sprite));

            currentLife = maxLife;

        }

        //METHODS
        public void OnHit()
        {
            currentLife--;

            if (currentLife == 2)
                sprite.color = Color.Orange;
            else if (currentLife == 1)
                sprite.color = Color.Red;

            if (currentLife <= 0)
                Destroy();
        }

        public override void OnDestroy()
        {
            Game_Arkanoid.grid.OnBlockDestroyed();
            sprite.Destroy();
        }

    }
}
