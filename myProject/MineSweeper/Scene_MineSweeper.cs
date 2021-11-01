using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

using myEngine.myProject.MineSweeper;

namespace myEngine
{
    public class Scene_MineSweeper : IScene
    {
        //FIELDS
        public static Game_MineSweeper game;

        //CONSTRUCTOR
        public Scene_MineSweeper()
        {
            Settings.BACKGROUND_COLOR = Color.CornflowerBlue;

            game = new Game_MineSweeper();
        }

        //METHODS

    }
}
