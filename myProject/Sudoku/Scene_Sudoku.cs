using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using myEngine.myProject.Sudoku;

namespace myEngine
{
    public class Scene_Sudoku : IScene
    {
        //FIELDS
        Game_Sudoku game;

        //CONSTRUCTOR
        public Scene_Sudoku()
        {
            Settings.BACKGROUND_COLOR = Color.Beige;
            game = new Game_Sudoku();
        }

        //METHODS
        
    }
}
