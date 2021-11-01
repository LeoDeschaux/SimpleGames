using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace myEngine.myProject.Arkanoid
{
    public class Game_Arkanoid : EmptyObject
    {
        //FIELDS
        public static Grid grid;
        public static Paddle paddle;
        public static Ball ball;

        //CONSTRUCTOR
        public Game_Arkanoid()
        {
            Settings.BACKGROUND_COLOR = new Color(50, 10, 40);

            paddle = new Paddle();
            grid = new Grid();

            ball = new Ball(paddle);
            paddle.isServing = true; 
        }

        //METHODS
        public override void Update()
        {
            if (Input.GetKeyDown(Keys.Enter))
                grid.DestroyAllBlocks();
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            //drawOrder = 500;
            //DrawSimpleShape.DrawRuller(Settings.Get_Screen_Center(), Color.Red, matrix);
        }

        public static void OnGameWin()
        {
            Console.WriteLine("GAME WINNED");
            Settings.GAME_SPEED = 0;
            PopUp p = new PopUp();
            //p.button.onButtonPressed += (object sender, EventArgs eventArgs) => Engine.sceneManager.ReloadScene();
            p.button.onButtonPressed.SetFunction(SceneManager.ReloadScene);
            
            //new ArgumentException("ERROR");
        }
    }
}
