using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using myEngine.myProject.Arkanoid;

namespace myEngine
{
    public class Scene_Arkanoid : IScene 
    {
        //FIELDS
        public Game_Arkanoid game;

        //CONSTRUCTOR
        public Scene_Arkanoid()
        {
            game = new Game_Arkanoid();
        }

        //METHODS
        public override void Update()
        {
        }

    }
}
