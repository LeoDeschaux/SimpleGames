using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace myEngine
{
    public class Game_Asteroid
    {
        //FIELDS
        Player player;
        Asteroid_Spawner spawner;
        //UI

        //CONSTRUCTOR
        public Game_Asteroid()
        {
            this.player = new Player();
            this.spawner = new Asteroid_Spawner(10);

            //Asteroid a = new Asteroid(Vector2.Zero);
        }

        //METHODS
    }
}
