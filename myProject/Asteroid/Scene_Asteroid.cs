using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine
{
    public class Scene_Asteroid : IScene
    {
        //FIELDS
        Game_Asteroid game;

        public static Camera2D cam; //TODO: REMOVE THIS THING

        //CONSTRUCTOR
        public Scene_Asteroid()
        {
            Settings.BACKGROUND_COLOR = Color.Black;

            Scene_Asteroid.cam = this.camera;

            this.game = new Game_Asteroid();

            this.camControl.isActive = true;
        }

        //METHODS
    }
}
