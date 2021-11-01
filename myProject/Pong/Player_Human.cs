using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

using myEngine.myProject.Pong;
using myEngine;

namespace zCurrentProject
{
    public class Player_Human : Player
    {
        //FIELDS
        private PlayerInput Input;

        //CONSTRUCTOR
        public Player_Human(Vector2 startPos, PlayerIndex playerIndex, InputProfile inputProfile)
        {
            Input = new PlayerInput(inputProfile);
            this.playerIndex = playerIndex;

            raquette.transform.position = startPos;
            
            name = "Player_Human_Default_Name";

            LoadScore();
        }

        //METHODS
        

        //UPDATE & DRAW
        public override void Update()
        {
            base.Update();

            bool pressingDown = (Input.GetButton(myButtons.LeftAxisDown) || Input.GetButton(myButtons.DPadDown));
            if (pressingDown && raquette.transform.position.Y < (Settings.SCREEN_HEIGHT - raquette.sprite.GetRectangle().Height / 2))
            {
                raquette.transform.position = new Vector2(raquette.transform.position.X, raquette.transform.position.Y + (speed * Time.deltaTime));
            }

            bool pressingUp = (Input.GetButton(myButtons.LeftAxisUp) || Input.GetButton(myButtons.DPadUp));
            if (pressingUp && raquette.transform.position.Y > (0 + raquette.sprite.GetRectangle().Height / 2))
            {
                raquette.transform.position = new Vector2(raquette.transform.position.X, raquette.transform.position.Y - (speed * Time.deltaTime));
            }

            if(Input.GetButtonDown(myButtons.ButtonA) && Scene_Pong.game.ball.ballState != Ball.BallState.moving && isHoldingTheBall)
            {
                FireBall(ballDirection);
            }
        }
    }
}