using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Sudoku
{
    public static class GridUtils
    {
        public static void CreateNewGrid(Grid grid)
        {
            GenerateGrid(grid);
            RemoveRandomCells(grid, 46);
        }

        public static void ShowAnwser(Grid grid)
        {
            CheckGrid(grid);

            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                {
                    grid.cells[x, y].overlaySprite.isVisible = false;

                    if (grid.cells[x, y].isSealled)
                        continue;

                    if (grid.cells[x, y].button.text.s == grid.answers[x, y])
                        grid.cells[x, y].SetColor(Color.Green);
                    else
                    {
                        grid.cells[x, y].button.text.s = grid.answers[x, y];
                        grid.cells[x, y].SetColor(Color.Red);
                    }

                    grid.cells[x, y].button.isActive = false;

                }
        }

        public static void SetGridToAnwser(Grid grid)
        {
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                {
                    grid.cells[x, y].overlaySprite.isVisible = false;

                    if (grid.cells[x, y].isSealled)
                        continue;

                    grid.cells[x, y].button.text.s = grid.answers[x, y];

                    grid.cells[x, y].button.isActive = false;

                }
        }

        public static void RemoveRandomCells(Grid grid, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                RemoveRandomCell(grid);
            }
        }

        public static void RemoveRandomCell(Grid grid)
        {
            Random random = new Random();

            if (IsGridEmpty(grid))
                throw new ArgumentException("[la grid est vide]");

            Cell[] c = GetSealledCells(grid);
            int index = random.Next(0, c.Length);
            c[index].SetCellEmpty();
        }

        public static Cell[] GetSealledCells(Grid grid)
        {
            List<Cell> c = new List<Cell>();

            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                {
                    if (grid.cells[x, y].isSealled)
                        c.Add(grid.cells[x, y]);
                }

            return c.ToArray();
        }

        public static bool IsGridEmpty(Grid grid)
        {
            bool b = true;

            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                {
                    if (grid.cells[x, y].isSealled)
                        b = false;
                }

            return b;
        }

        public static bool IsGridComplete(Grid grid)
        {
            bool b = true;

            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                {
                    if (grid.cells[x, y].button.text.s != grid.answers[x, y])
                        b = false;
                }

            return b;
        }

        static Delay delay;
        static int myX = 0;
        static int myY = 0;
        public static void CheckFunc(Grid grid)
        {
            isCellValid(grid, grid.cells[myX, myY]);

            if (myY < 9)
            {
                //DO
                if (myX < 8)
                {
                    myX++;
                }
                else
                {
                    if(myY != 8)
                    {
                        myX = 0;
                        myY++;
                    }
                }
            }
        }

        public static void SetCellsColorToWhite(Grid grid)
        {
            //SHOW SELECTED CELLS
            for (int j = 0; j < 9; j++)
            {
                Cell[] cells = GetSquare(grid, j);

                for (int k = 0; k < 9; k++)
                {
                    cells[k].button.defaultColor = Color.White;
                }
            }
        }

        public static void ColorCells(Cell[] cells)
        {
            for (int i = 0; i < 9; i++)
            {
                cells[i].button.defaultColor = Color.Blue;
            }
        }

        public static void CheckGrid(Grid grid)
        {
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                {
                    grid.cells[x, y].overlaySprite.isVisible = false;

                    if (grid.cells[x, y].isSealled)
                        continue;

                    if (isCellValid(grid, grid.cells[x, y]))
                        grid.cells[x, y].SetColor(Color.Green);
                    else
                        grid.cells[x, y].SetColor(Color.Red);
                }
        }

        public static bool isCellValid(Grid grid, Cell cell)
        {
            bool isValid = true;

            //CHECK ROW
            Cell[] row = GetRow(grid, cell.posY);
            for (int i = 0; i < 9; i++)
            {
                if (row[i].button.text.s == cell.button.text.s && i != cell.posX)
                {
                    isValid = false;
                }
            }

            //CHECK COLLUMNS
            Cell[] collumn = GetCollumn(grid, cell.posX);
            for (int i = 0; i < 9; i++)
            {
                if (collumn[i].button.text.s == cell.button.text.s && i != cell.posY)
                {
                    isValid = false;
                }
            }

            //CHECK SQUARE
            Cell[] square = GetSquare(grid, FindSquareFromCellPosition(cell));
            for (int i = 0; i < 9; i++)
            {
                if (square[i].button.text.s == cell.button.text.s && (square[i].posX != cell.posX && square[i].posY != cell.posY))
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        public static int FindSquareFromCellPosition(Cell cell)
        {
            int x = cell.posX / 3;
            int y = cell.posY / 3;

            int square = (x + (y * 3));
            
            return square;
        }

        public static void ColorSquares(Grid grid)
        {
            //SHOW SELECTED CELLS
            for (int j = 0; j < 9; j++)
            {
                Cell[] cells = GetSquare(grid, j);

                for (int k = 0; k < 9; k++)
                {
                    cells[k].button.defaultColor = new Color(100 + (j * 20), 100 + (j * 20), 100 + (j * 20));
                }
            }
        }

        public static void GenerateGrid(Grid grid)
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i < 9; i++)
                numbers.Add(i + 1);

            //GENERATE sequence
            Random random = new Random();
            int[] sequence = new int[9];
            for (int i = 0; i < 9; i++)
            {
                int index = random.Next(0, numbers.Count);
                sequence[i] = numbers[index];
                numbers.RemoveAt(index);
            }

            int j = 0;
            for (int y = 0; y < 9; y++)
            {
                if (y == 0)
                    j += 0;
                else if (y == 3 || y == 6)
                    j += 1;
                else
                    j += 3;

                for (int x = 0; x < 9; x++)
                {
                    string number = "" + sequence[(x+j)%9];
                    grid.cells[x, y].Init(number);
                    grid.answers[x, y] = grid.cells[x, y].button.text.s;
                }
            }
        }

        public static Cell[] GetRow(Grid grid, int index)
        {
            Cell[] cells = new Cell[9];

            for (int i = 0; i < 9; i++)
            {
                cells[i] = grid.cells[i, index];
            }

            return cells;
        }

        public static Cell[] GetCollumn(Grid grid, int index)
        {
            Cell[] cells = new Cell[9];

            for (int i = 0; i < 9; i++)
            {
                cells[i] = grid.cells[index, i];
            }

            return cells;
        }

        public static Cell[] GetSquare(Grid grid, int index)
        {
            Cell[] cells = new Cell[9];

            if (index < 0 || index >= 9)
                throw new ArgumentException("[unvalid argument exception (index)]");

            int x = (index % 3) * 3;
            int y = (index / 3) * 3;

            cells[0] = grid.cells[x, y];
            cells[1] = grid.cells[x+1, y];
            cells[2] = grid.cells[x+2, y];

            cells[3] = grid.cells[x, y+1];
            cells[4] = grid.cells[x+1, y+1];
            cells[5] = grid.cells[x+2, y+1]; 

            cells[6] = grid.cells[x, y+2];
            cells[7] = grid.cells[x+1, y+2];
            cells[8] = grid.cells[x+2, y+2];

            return cells;
        }

        public static void GetDiagonal(Grid grid)
        {
            throw new NotImplementedException();
        }
    }
}
