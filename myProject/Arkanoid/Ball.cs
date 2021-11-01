using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Arkanoid
{
    public class Ball : GameObject
    {
        //FIELDS
        Sprite sprite;

        bool hasBeenShot = false;
        Paddle paddle;

        Vector2 direction;
        float speed = 500;

        Collider2D collider;

        //CONSTRUCTOR
        public Ball(Paddle paddle)
        {
            this.paddle = paddle;

            sprite = new Sprite();
            sprite.color = Color.Yellow;
            sprite.transform = this.transform;
            sprite.dimension = new Vector2(30, 30);

            AddComponent(new BoxCollider(sprite));
        }

        int i = 0;

        //METHODS
        public override void Update()
        {
            if(hasBeenShot)
            {
                if (transform.position.X > Settings.SCREEN_WIDTH - sprite.GetRectangle().Width / 2)
                {
                    direction.X = -1;
                    OnBallChangeDirection(true);
                }

                if (transform.position.X < 0 + sprite.GetRectangle().Width / 2)
                {
                    direction.X = 1;
                    OnBallChangeDirection(true);
                }

                if (transform.position.Y > Settings.SCREEN_HEIGHT - sprite.GetRectangle().Height / 2)
                {
                    direction.Y = -1;
                    OnBallChangeDirection(true);
                }

                if (transform.position.Y < 0 + sprite.GetRectangle().Height / 2)
                {
                    direction.Y = 1;
                    OnBallChangeDirection(true);
                }

                transform.position += direction * speed * Time.deltaTime;
            }
        }

        public void OnBallChangeDirection(bool b)
        {
             
        }

        public override void LateUpdate()
        {
            if(!hasBeenShot)
            {
                Vector2 pos = new Vector2(paddle.transform.position.X, paddle.transform.position.Y - (sprite.dimension.Y * 2));
                this.transform.position = pos;
            }
        }

        public void Serve()
        {
            direction = new Vector2(0, -1);
            hasBeenShot = true;
        }

        public override void OnCollision(Collider2D other)
        {
            BoxCollider boxCollider = (BoxCollider)other;
            //REBOND HORIZONTAL
            if (boxCollider.gameObject is Paddle)
            {
                float raquettePosX = boxCollider.gameObject.transform.position.X;
                float raquetteDimX = boxCollider.rectangle.Width;

                if (transform.position.X < (raquettePosX - (raquetteDimX / 2)) + raquetteDimX / 3)
                    direction.X = -1;
                else if ((transform.position.X > (raquettePosX - (raquetteDimX / 2)) + raquetteDimX / 3) &&
                        transform.position.X < (raquettePosX - (raquetteDimX / 2)) + (raquetteDimX / 3) * 2)
                    direction.X = 0;
                else if (transform.position.X > (raquettePosX - (raquetteDimX / 2)) + (raquetteDimX / 3) * 2)
                    direction.X = 1;
            }
               

            //REBOND VERTICAL
            if (transform.position.Y > boxCollider.gameObject.transform.position.Y)
                direction.Y = 1;
            else if (transform.position.Y < boxCollider.gameObject.transform.position.Y)
                direction.Y = -1;

            OnBallChangeDirection(false);

            if ((boxCollider.gameObject is Block))
                ((Block)boxCollider.gameObject).OnHit();
        }
    }
}
