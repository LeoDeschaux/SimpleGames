using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace myEngine.myProject.MineSweeper
{
    public class Game_MineSweeper : EmptyObject
    {
        //FIELDS
        public static Grid grid;
        UI_MineSweeper ui;

        private static bool isGameOver = false;

        //CONSTRUCTOR
        public Game_MineSweeper()
        {
            grid = new Grid();
            ui = new UI_MineSweeper();

            isGameOver = false;
        }

        public override void Update()
        {
        }

        public static void OnGameLoose()
        {
            isGameOver = true;
            GridUtils.ShowEveryCells(grid);
            GridUtils.SetEveryCellsToDisabled(grid);

            PopUp p = new PopUp("GAME OVER");
            p.text.transform.position += new Vector2(0, 50);
            p.background.color = new Color(0, 0, 0, 180);
            p.button.onButtonPressed.SetFunction(SceneManager.ReloadScene);
        }

        public static void OnGameWin()
        {
            if (isGameOver)
                return;

            GridUtils.SetEveryCellsToDisabled(grid);

            PopUp p = new PopUp("YOU WIN !");
            p.text.transform.position += new Vector2(0, 50);
            p.background.color = new Color(0, 0, 0, 180);
            p.button.onButtonPressed.SetFunction(SceneManager.ReloadScene);
        }
    }
}
