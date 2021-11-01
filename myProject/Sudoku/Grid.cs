using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Sudoku
{
    public class Grid : EmptyObject
    {
        //FIELDS
        public Cell[,] cells;
        public string[,] answers;

        int rows = 9, collumns = 9;
        int spriteSizeX = 76, spriteSizeY = 76;
        public static int marginX = 2, marginY = 2;
        int offSetX = 0, offSetY = 0;

        int currentBlocksLeft;
        int totalBlocks;

        GridOverlay overlay;
        Vector2 startPoint;
        Vector2 endPoint;

        //CONSTRUCTOR
        public Grid()
        {
            Cell.Load();

            totalBlocks = rows * collumns;
            currentBlocksLeft = totalBlocks;

            cells = new Cell[collumns, rows];
            answers = new string[collumns, rows];

            Vector2 start = new Vector2(Settings.GetScreenCenter().X - ((collumns * (spriteSizeX + marginX)) / 2),
                                                Settings.GetScreenCenter().Y - ((rows * (spriteSizeY + marginY)) / 2));
            start = new Vector2(start.X + ((spriteSizeX + marginX) / 2), start.Y + ((spriteSizeY + marginY) / 2));

            for (int y = 0; y < rows; y++)
                for (int x = 0; x < collumns; x++)
                {
                    Vector2 position = new Vector2(offSetX + start.X + (x * (spriteSizeX + marginX)), offSetY + start.Y + (y * (spriteSizeY + marginY)));

                    cells[x, y] = new Cell(position, new Vector2(spriteSizeX, spriteSizeY), x, y);
                    cells[x, y].name = "" + x + ", " + y;

                    answers[x, y] = cells[x, y].button.text.s;
                }

            startPoint = new Vector2(start.X - (spriteSizeX / 2), start.Y - (spriteSizeY / 2));
            endPoint = new Vector2((spriteSizeX * rows) + (marginX*(rows-1)), (spriteSizeY * collumns) + (marginY*(collumns-1)));
            overlay = new GridOverlay(startPoint, endPoint);
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            DrawSimpleShape.DrawRectangleFull(startPoint, endPoint, Color.Black, null);
        }
    }
}
