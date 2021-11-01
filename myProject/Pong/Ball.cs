using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;

using myEngine.myProject.Pong;
using myEngine;

namespace zCurrentProject
{
    public class Ball : GameObject
    {
        //FIELDS
        public static int sizeX = 30, sizeY = 30;
        public Sprite sprite;
        public Trail trail;

        private float speed = 12*60;

        public BallState ballState;
        private Vector2 direction;

        //COMPONENTS
        Collider2D collider;

        //SOUNDS
        SoundEffect ball_hit_wall;
        SoundEffect ball_hit_raquette;

        public enum BallState
        {
            idle,
            moving
        }

        //CONSTRCUTOR
        public Ball(Transform anchorPos)
        {
            this.transform = anchorPos;

            sprite = new Sprite(new Vector2(transform.position.X/2, transform.position.Y), new Vector2(sizeX, sizeY));
            sprite.color = Color.Yellow;
            sprite.transform = this.transform;

            ballState = BallState.idle;

            trail = new Trail(sprite.transform);
            trail.maxPoints = 3;
            trail.drawOrder = -900;

            AddComponent(new BoxCollider(sprite));
            //collider = new Collider2D(sprite);

            //LOAD SOUNDS
            ball_hit_wall = Ressources.Load<SoundEffect>("myContent/Audio/BallHit_V2");
            ball_hit_raquette = Ressources.Load<SoundEffect>("myContent/Audio/BallHit_V1"); 
        }

        //METHODS
        public void FireBall(int direction)
        {
            if(direction != 0 && ballState == BallState.idle)
            {
                Vector2 currentPos = transform.position;
                transform = new Transform();
                transform.position = currentPos;
                sprite.transform = this.transform;

                this.direction.X = direction;
                this.direction.Y = 0;

                ballState = BallState.moving;

                trail.transform = this.transform;
                GetComponent<BoxCollider>().transformTarget = this.transform;
                OnBallChangeDirection(false);
            }

            ballState = BallState.moving;
        }

        //UPDATE & DRAW
        public override void Update()
        {
            if (ballState == BallState.moving)
            {
                if (transform.position.X > Settings.SCREEN_WIDTH - sprite.GetRectangle().Width/2)
                {
                    direction.X = -1;
                    OnBallChangeDirection(true);
                }

                if (transform.position.X < 0 + sprite.GetRectangle().Width/2)
                {
                    direction.X = 1;
                    OnBallChangeDirection(true);
                }

                if (transform.position.Y > Settings.SCREEN_HEIGHT - sprite.GetRectangle().Height/2)
                {
                    direction.Y = -1;
                    OnBallChangeDirection(true);
                }

                if (transform.position.Y < 0 + sprite.GetRectangle().Height/2)
                {
                    direction.Y = 1;
                    OnBallChangeDirection(true);
                }

                transform.position += direction * speed * Time.deltaTime;
            }
        }
        
        public override void OnCollision(Collider2D other)
        {
            if(other.gameObject is Player)
            {
                //REBOND HORIZONTAL
                if (transform.position.X > ((Player)other.gameObject).raquette.transform.position.X)
                    direction.X = 1;
                if (transform.position.X < ((Player)other.gameObject).raquette.transform.position.X)
                    direction.X = -1;

                //VERTICAL
                float raquettePosY = ((Player)other.gameObject).raquette.transform.position.Y;
                float raquetteDimY = ((Player)other.gameObject).raquette.sprite.GetRectangle().Height;

                if (transform.position.Y < (raquettePosY - (raquetteDimY / 2)) + raquetteDimY / 3)
                    direction.Y = -1;
                else if ((transform.position.Y > (raquettePosY - (raquetteDimY / 2)) + raquetteDimY / 3) &&
                        transform.position.Y < (raquettePosY - (raquetteDimY / 2)) + (raquetteDimY / 3) * 2)
                    direction.Y = 0;
                else if (transform.position.Y > (raquettePosY - (raquetteDimY / 2)) + (raquetteDimY / 3) * 2)
                    direction.Y = 1;

                OnBallChangeDirection(false);
            }
            else
            {
                other.gameObject.Destroy();

                if (direction.X == 1)
                    Scene_Pong.game.player2.OnPlayerGetRemovedStock();
                else if (direction.X == -1)
                    Scene_Pong.game.player1.OnPlayerGetRemovedStock();

            }
        }

        private void OnBallChangeDirection(bool isAWall)
        {
            trail.AddPoints();

            ParticleProfile pp = new ParticleProfile();
            pp.burstMode = true;
            pp.duration = 0.1f;

            ParticleEngine p = new ParticleEngine(pp, transform.position);

            if (isAWall)
                AudioSource.PlaySoundEffect(ball_hit_wall);
            else
                AudioSource.PlaySoundEffect(ball_hit_raquette);
        }
    }
}
