using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace myEngine.myProject.Sudoku
{
    public class Game_Sudoku : EmptyObject
    {
        //FIELDS
        public static Grid grid;
        public static Menu menu;
        public static UI_Sudoku ui;

        //CONSTRUCTOR
        public Game_Sudoku()
        {
            grid = new Grid();
            menu = new Menu();
            ui = new UI_Sudoku(grid);
        }

        //METHODS
        public override void Update()
        {
        }

        public static void OnGridIsComplete()
        {
            Console.WriteLine("GRID IS COMPLETE");
            PopUp p = new PopUp("             GRID COMPLETED !\nPress the button below to play again");
            p.button.onButtonPressed.SetFunction(SceneManager.ReloadScene);
        }
    }
}
