using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Arkanoid
{
    public class Paddle : GameObject
    {
        //FIELDS
        Sprite sprite;
        float speed = 800;

        public bool isServing = false;

        //CONSTRUCTOR
        public Paddle()
        {
            sprite = new Sprite();
            sprite.color = Color.White;
            sprite.dimension = new Vector2(200, 40);

            //START POS
            Vector2 startPos = Settings.GetScreenCenter();
            startPos += new Vector2(0, 300);
            this.transform.position = startPos;
            sprite.transform = this.transform;

            AddComponent(new BoxCollider(sprite));
        }

        //METHODS
        public override void Update()
        {
            if (Input.GetKey(Keys.Left) && transform.position.X > (0 + sprite.dimension.X / 2))
                this.transform.position -= new Vector2(speed * Time.deltaTime, 0);
            else if (Input.GetKey(Keys.Right) && transform.position.X < (Settings.SCREEN_WIDTH - sprite.dimension.X / 2))
                this.transform.position += new Vector2(speed * Time.deltaTime, 0);

            if(Input.GetKeyDown(Keys.Space) && isServing)
            {
                Game_Arkanoid.ball.Serve();
                isServing = false;
            }
        }
    }
}
