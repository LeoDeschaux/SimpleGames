using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

using myEngine.myProject.Pong;
using myEngine;

namespace zCurrentProject
{
    public enum GameMode
    {
        PvP,
        PvAI
    }

    public class Game_Pong : EmptyObject
    {
        //FIELDS
        public Player player1;
        public Player player2;
        public Ball ball;

        public TargetSpawner targetSpawner;

        //SAVE
        private string lastWinner = "none";
        private string lastWinnerKey = "LastWinner";

        //CONSTRUCTOR
        public Game_Pong(GameMode gameMode)
        {
            if(gameMode == GameMode.PvP)
            {
                player1 = new Player_Human(new Vector2(160, Settings.SCREEN_HEIGHT / 2), PlayerIndex.One, new InputProfile(PlayerIndex.One, InputMode.defaultKeyboard));
                player2 = new Player_Human(new Vector2(1080, Settings.SCREEN_HEIGHT / 2), PlayerIndex.Two, new InputProfile(PlayerIndex.Two, InputMode.defaultGamePad, 1));
            }
            else
            {
                player1 = new Player_Human(new Vector2(160, Settings.SCREEN_HEIGHT / 2), PlayerIndex.One, new InputProfile(PlayerIndex.One, InputMode.defaultKeyboard));
                player2 = new Player_AI(PlayerIndex.Two);
            }

            player1.SetInGamePlayerPosition(InGamePlayerPosition.Left);
            player2.SetInGamePlayerPosition(InGamePlayerPosition.Right);

            ServeTheBall();

            targetSpawner = new TargetSpawner();
        }

        private void ServeTheBall()
        {
            if (SaveSystem_RunTime.data.ContainsKey(lastWinnerKey))
                lastWinner = SaveSystem_RunTime.data[lastWinnerKey];

            if (lastWinner != "none")
            {
                if (lastWinner == "One")
                {
                    ball = new Ball(player1.anchorPoint);
                    player1.isHoldingTheBall = true;
                }
                else if (lastWinner == "Two")
                {
                    ball = new Ball(player2.anchorPoint);
                    player2.isHoldingTheBall = true;
                }
            }
            else
            {
                ball = new Ball(player1.anchorPoint);
                player1.isHoldingTheBall = true;
            }
        }

        public void OnGameOver(PlayerIndex looser)
        {
            Player player = Scene_Pong.game.player1;
            if (looser == player.playerIndex)
                player = Scene_Pong.game.player2;

            player.score++;

            SaveSystem_RunTime.data.Remove(lastWinnerKey);
            SaveSystem_RunTime.data.Add(lastWinnerKey, looser.ToString());

            SaveScore();
            ReloadScene();
        }

        private void SaveScore()
        {
            player1.SaveScore();
            player2.SaveScore();
        }

        //UPDATE & DRAW
        public override void Update()
        {
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix matrix)
        {
        }

        static Delay delay;
        public static void ReloadScene()
        {
            delay = new Delay(2000, () =>
            {
                SceneManager.ReloadScene();
            });
        }
    }
}