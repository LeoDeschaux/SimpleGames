using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace myEngine.myProject.MineSweeper
{
    public class GridUtils
    {
        public static void SetBombs(Grid grid, int amount)
        {
            for (int i = 0; i < amount; i++)
                SetBomb(grid);
        }

        private static void SetBomb(Grid grid)
        {
            Random random = new Random();

            Cell[] c = GetEmptyCells(grid);

            //Console.WriteLine(c.Length);

            if (c.Length == 0)
            {
                Console.WriteLine("NO EMPTY CELLS LEFT");
                return;
            }

            int index = random.Next(0, c.Length);

            Vector2 pos = c[index].button.transform.position;
            Vector2 dim = c[index].button.sprite.dimension;
            int x = c[index].x;
            int y = c[index].y;

            /*
            DOES NOT WORK FOR SOME REASON..
            c[index].Destroy();
            c[index] = new Cell_Bomb(pos, dim, x, y);
            */

            grid.cells[x, y].Destroy();
            grid.cells[x, y] = new Cell_Bomb(pos, dim, x, y);
        }

        public static Cell[] GetEmptyCells(Grid grid)
        {
            List<Cell> c = new List<Cell>();

            for (int y = 0; y < grid.rows; y++)
                for (int x = 0; x < grid.collumns; x++)
                {
                    if (grid.cells[x, y] is Cell_Empty && grid.cells[x,y].isRevealed == false)
                    {
                        c.Add(grid.cells[x, y]);
                    }
                }

            return c.ToArray();
        }

        public static int GetNumberOfBombAround(Grid grid, Cell cell)
        {
            int amount = 0;

            for (int y = cell.y-1; y < cell.y+2; y++)
                for (int x = cell.x-1; x < cell.x+2; x++)
                {
                    if (y >= 0 && x >= 0 && y < grid.rows && x < grid.collumns)
                    {
                        if (grid.cells[x, y] is Cell_Bomb)
                            amount++;
                    }
                }

            return amount;
        }

        public static void ShowCellsAround(Grid grid, Cell cell)
        {
            if (GetNumberOfBombAround(grid, cell) == 0)
            {
                for (int y = cell.y - 1; y < cell.y + 2; y++)
                    for (int x = cell.x - 1; x < cell.x + 2; x++)
                    {
                        if (y >= 0 && x >= 0 && y < grid.rows && x < grid.collumns)
                        {
                            if (grid.cells[x, y] is Cell_Empty && !grid.cells[x, y].isRevealed)
                            {
                                grid.cells[x, y].OnClic();
                            }
                        }
                    }
            }
        }

        public static int GetRevealedCells(Grid grid)
        {
            int amount = 0;

            for (int y = 0; y < grid.rows; y++)
                for (int x = 0; x < grid.collumns; x++)
                {
                    if (grid.cells[x,y].isRevealed)
                        amount++;
                }

            return amount;
        }

        public static void ShowEveryCells(Grid grid)
        {
            for (int y = 0; y < grid.rows; y++)
                for (int x = 0; x < grid.collumns; x++)
                {
                    grid.cells[x, y].ShowCell();
                }
        }

        public static int GetBombsLeft(Grid grid)
        {
            int amount = 0;

            for (int y = 0; y < grid.rows; y++)
                for (int x = 0; x < grid.collumns; x++)
                {
                    if (grid.cells[x, y] is Cell_Bomb)
                        amount++;
                }

            return amount;
        }

        public static int GetHiddenCellsLeft(Grid grid)
        {
            int amount = 0;

            for (int y = 0; y < grid.rows; y++)
                for (int x = 0; x < grid.collumns; x++)
                {
                    if (grid.cells[x, y].isRevealed == false)
                        amount++;
                }

            return amount;
        } 

        public static int GetEmptyCellsLeft(Grid grid)
        {
            int amount = 0;

            for (int y = 0; y < grid.rows; y++)
                for (int x = 0; x < grid.collumns; x++)
                {
                    if (grid.cells[x, y].isRevealed == false && grid.cells[x,y] is Cell_Empty)
                        amount++;
                }

            return amount;
        }

        public static void SetEveryCellsToDisabled(Grid grid)
        {
            for (int y = 0; y < grid.rows; y++)
                for (int x = 0; x < grid.collumns; x++)
                {
                    grid.cells[x, y].button.isActive = false;
                }
        }
    }
}
