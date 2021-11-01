using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using myEngine.myProject.Snake;
using System.IO;
using Microsoft.Xna.Framework.Media;

namespace myEngine.myProject.Snake
{
    public class Game_Snake : EmptyObject
    {
        //FIELDS
        public static Grid grid;
        public Snake snake;

        Delay delay;
        Random random;

        //APPLE SPAWNER

        //CONSTRUCTOR
        public Game_Snake()
        {
            snake = new Snake();
            grid = new Grid(snake);

            random = new Random();

            delay = new Delay(200, () =>
            {
                grid.UpdateGrid();
            });

            delay = new Delay(1000, () =>
            {
                SpawnApple();
            });
        }

        public void SpawnApple()
        {
            bool hasSpawned = false;
            int max = 100;
            int i = 0;

            while(!hasSpawned && i<max)
            {
                int x = random.Next(0, grid.size);
                int y = random.Next(0, grid.size);

                if (grid.cells[x, y].type == CellType.empty)
                {
                    Vector2 position = grid.cells[x, y].sprite.transform.position;
                    grid.cells[x, y].Destroy();
                    grid.cells[x, y] = new Cell_Apple(position);
                    hasSpawned = true;
                }

                i++;
            }
        }

        //METHODS
        public override void Update()
        {
            if (Input.GetKeyDown(Keys.Enter))
            {
                World.GetWorldStats();
            }
        }
    }
}