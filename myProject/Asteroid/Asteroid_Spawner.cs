using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine
{
    public class Asteroid_Spawner : EmptyObject
    {
        //FIELDS
        int max = 5;

        Random random;

        //CONSTRUCTOR
        public Asteroid_Spawner(int amount)
        {
            this.max = amount;

            random = new Random(0);

            for (int i = 0; i < max; i++)
            {
                float x = random.Next(0, Settings.SCREEN_WIDTH);
                float y = random.Next(0, Settings.SCREEN_HEIGHT);

                Asteroid a = new Asteroid(new Vector2(x, y));
            }
        }

        //METHODS
    }
}
