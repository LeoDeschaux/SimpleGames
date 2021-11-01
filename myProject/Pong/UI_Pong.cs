using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using myEngine.myProject.Pong;
using myEngine;

namespace zCurrentProject
{
    public class UI_Pong : UI
    {
        //FIELDS
        Text scorePlayer1;
        Text scorePlayer2;

        Sprite[] livesContainerPlayer1;
        Sprite[] livesPlayer1;

        Sprite[] livesContainerPlayer2;
        Sprite[] livesPlayer2;


        //CONCSTRUCTOR
        public UI_Pong()
        {
            Color c = new Color(30, 30, 30);

            //SET PLAYER SCORE
            scorePlayer1 = new Text();
            scorePlayer1.color = c;
            scorePlayer1.s = Scene_Pong.game.player1.score.ToString();
            scorePlayer1.fontSize = 300;
            scorePlayer1.transform.position = new Vector2(Settings.SCREEN_WIDTH/4, (Settings.SCREEN_HEIGHT / 2) - 100);
            scorePlayer1.alignment = Alignment.Center;

            scorePlayer1.drawOrder = -1000;

            //SET AI SCORE
            scorePlayer2 = new Text();
            scorePlayer2.color = c;
            scorePlayer2.s = Scene_Pong.game.player1.score.ToString();
            scorePlayer2.fontSize = 300;
            scorePlayer2.transform.position = new Vector2((Settings.SCREEN_WIDTH / 4)*3, (Settings.SCREEN_HEIGHT / 2) - 100);
            scorePlayer2.alignment = Alignment.Center;

            scorePlayer2.drawOrder = -1000;

            //SET PLAYER LIVES
            livesContainerPlayer1 = new Sprite[3];
            livesPlayer1 = new Sprite[3];
            for (int i = 0; i < 3; i++)
            {
                livesContainerPlayer1[i] = new Sprite(new Vector2((int)((150) + 30*1.2*i), 650), new Vector2(30, 30));
                livesContainerPlayer1[i].color = Color.White;
                livesContainerPlayer1[i].drawOrder = 450;

                livesPlayer1[i] = new Sprite(livesContainerPlayer1[i].transform.position, livesContainerPlayer1[i].dimension*0.8f);
                livesPlayer1[i].drawOrder = 500;
                livesPlayer1[i].color = Color.HotPink;
            }

            //SET PLAYER LIVES
            livesContainerPlayer2 = new Sprite[3];
            livesPlayer2 = new Sprite[3];
            for (int i = 0; i < 3; i++)
            {
                livesContainerPlayer2[i] = new Sprite(new Vector2((int)((1100) + 30 * 1.2 * i), 650), new Vector2(30, 30));
                livesContainerPlayer2[i].color = Color.White;
                livesContainerPlayer2[i].drawOrder = 450;

                livesPlayer2[i] = new Sprite(livesContainerPlayer2[i].transform.position, livesContainerPlayer2[i].dimension * 0.8f);
                livesPlayer2[i].drawOrder = 500;
                livesPlayer2[i].color = Color.HotPink;
            }

            UpdateUI();
        }

        public void UpdateUI()
        {
            scorePlayer1.s = Scene_Pong.game.player1.score.ToString();
            scorePlayer2.s = Scene_Pong.game.player2.score.ToString();
        }

        public override void Update()
        {
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix matrix)
        {
        }

        //METHODS
        public void RemoveLife(Player player)
        {
            if (player.lives < 0)
                return;

            if ((int)player.playerIndex == 0)
            {
                Particle p = new Particle(DrawSimpleShape.GetTexture(10, 10));
                p.Color = Color.HotPink;
                p.Speed = 10;
                p.TTL = 0.5f;

                ParticleProfile pp = new ParticleProfile(p);
                pp.burstMode = true;
                pp.burstAmount = 50;

                int i = Math.Clamp(player.lives, 0, 2);
                ParticleEngine pe = new ParticleEngine(pp, livesPlayer1[i].transform.position);
                livesPlayer1[i].Destroy();
            }
            else if((int)player.playerIndex == 1)
            {
                Particle p = new Particle(DrawSimpleShape.GetTexture(10, 10));
                p.Color = Color.HotPink;
                p.Speed = 10;
                p.TTL = 0.5f;

                ParticleProfile pp = new ParticleProfile(p);
                pp.burstMode = true;
                pp.burstAmount = 50;

                int i = Math.Clamp(player.lives, 0, 2);
                ParticleEngine pe = new ParticleEngine(pp, livesPlayer2[i].transform.position);
                livesPlayer2[i].Destroy();
            }

            UpdateUI();
        }

        //PAUSE MENU
        Sprite pauseMenu_BG;
        Text pause;
        Button mainMenu;

        public void OnPauseMenuCalled()
        {
            bool b = true;

            if (pauseMenu_BG != null && pause != null)
                if (pauseMenu_BG.disposed && pause.disposed)
                    b = true;
                else
                    b = false;

            if (b)
            {
                pauseMenu_BG = new Sprite(Settings.GetScreenCenter(), new Vector2(Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT));
                pauseMenu_BG.color = new Color(0, 0, 0, 200);
                pauseMenu_BG.drawOrder = 600;

                pause = new Text();
                pause.s = "=";
                pause.fontSize = 500;
                pause.color = Color.White;
                pause.drawOrder = 700;
                //pause.transform.position = new Vector2(900, 200);
                pause.transform.position = new Vector2(Settings.SCREEN_WIDTH/2 + 45, Settings.SCREEN_HEIGHT/2 - 50);
                pause.alignment = Alignment.Center;
                pause.transform.rotation = MathHelper.ToRadians(90);

                //BUTTON
                mainMenu = new Button();
                mainMenu.isVisible = true;

                mainMenu.sprite.transform.position = new Vector2(Settings.SCREEN_WIDTH / 2, 600);
                mainMenu.sprite.dimension = new Vector2(500, 100);
                mainMenu.sprite.drawOrder = 700;

                mainMenu.text.s = "Exit to Menu";
                mainMenu.text.drawOrder = 800;
                mainMenu.text.color = Color.Black;
                mainMenu.text.transform.position = new Vector2(Settings.SCREEN_WIDTH/2, 600);
                mainMenu.text.alignment = Alignment.Center;
                mainMenu.text.fontSize = 65;

                mainMenu.onButtonPressed.SetFunction(QuitGame);

                Settings.GAME_SPEED = 0;
            }
            else
            {
                pauseMenu_BG.Destroy();
                pause.Destroy();
                mainMenu.Destroy();

                Settings.GAME_SPEED = 1;
            }
        }

        private void QuitGame()
        {
            SceneManager.ChangeScene(typeof(Scene_MainMenu));
        }
    }
}
