using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine.myProject.Snake;

namespace myEngine.myProject.Snake
{
    

    public class Grid : EmptyObject
    {
        //FIELDS
        public Cell[,] cells;

        public int size { get; private set; } = 9;
        float spriteSize = 50;

        private Snake snake;
        private Apple_Spawner spawner;

        //CONSTRUCTOR
        public Grid(Snake snake)
        {
            cells = new Cell[size, size];
            InitCells();

            this.snake = snake;
            InitPlayerPos();

            spawner = new Apple_Spawner();
        }

        //METHODS
        public void UpdateGrid()
        {
            SetNextPlayerPos();
        }

        private void InitPlayerPos()
        {
            Vec2Int pos = snake.gridPosition;

            if (pos.X < 0 || pos.Y < 0 || pos.X >= size || pos.Y >= size)
                return;

            Vector2 position = cells[pos.X, pos.Y].sprite.transform.position;
            cells[pos.X, pos.Y].Destroy();
            cells[pos.X, pos.Y] = new Cell_Head(position, snake);
        }

        private void SetNextPlayerPos()
        {
            Vec2Int nextPos = snake.gridPosition + snake.direction;

            if (nextPos.X < 0 || nextPos.Y < 0 || nextPos.X >= size || nextPos.Y >= size)
            {
                Console.WriteLine("GAME OVER - PlAYER OUT OF GRID");
                return;
            }

            Vector2 nextSpritePos = cells[nextPos.X, nextPos.Y].sprite.transform.position;

            //RESET PREVIOUS CELL
            cells[snake.gridPosition.X, snake.gridPosition.Y].Destroy();
            cells[snake.gridPosition.X, snake.gridPosition.Y] = new Cell_Empty(cells[snake.gridPosition.X, snake.gridPosition.Y].sprite.transform.position);

            snake.OnSnakeMove();

            if(cells[snake.gridPosition.X, snake.gridPosition.Y].type == CellType.apple)
            {
                //EAT APPLE
                cells[snake.gridPosition.X, snake.gridPosition.Y].Destroy();
                cells[snake.gridPosition.X, snake.gridPosition.Y] = new Cell_Empty(nextSpritePos);

                snake.OnEatApple();
            }

            if (cells[snake.gridPosition.X, snake.gridPosition.Y].type == CellType.body)
            {
                Console.WriteLine("GAME OVER - HEAD TOUCHED BODY");
            }

            //MOVE PLAYER
            cells[snake.gridPosition.X, snake.gridPosition.Y].Destroy();
            cells[snake.gridPosition.X, snake.gridPosition.Y] = new Cell_Head(nextSpritePos, snake);
        }

        private void InitCells()
        {
            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                {
                    Vector2 start = new Vector2(Settings.GetScreenCenter().X - ((size * spriteSize * 1.1f) / 2), Settings.GetScreenCenter().Y - ((size * spriteSize * 1.1f)) / 2);
                    start = new Vector2(start.X + ((spriteSize * 1.1f) / 2), start.Y + ((spriteSize * 1.1f) / 2));

                    Vector2 position = new Vector2(start.X + (x * spriteSize * 1.1f), start.Y + (y * spriteSize * 1.1f));

                    cells[x, y] = new Cell_Empty(position);
                }
        }
    }
}
