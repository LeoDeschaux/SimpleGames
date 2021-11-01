using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Arkanoid
{
    public class Grid
    {
        //FIELDS
        Block[,] blocks;
        int sizeX = 8, sizeY = 5;
        int spriteSizeX = 100, spriteSizeY = 40;
        int marginX = 10, marginY = 10;
        int offSetX = 0, offSetY = -150;

        int currentBlocksLeft;
        int totalBlocks;

        //CONSTRUCTOR
        public Grid()
        {
            totalBlocks = sizeX * sizeY;
            currentBlocksLeft = totalBlocks;

            blocks = new Block[sizeX, sizeY];

            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                {
                    Vector2 start = new Vector2(Settings.GetScreenCenter().X - ((sizeX * (spriteSizeX + marginX)) / 2),
                                                Settings.GetScreenCenter().Y - ((sizeY * (spriteSizeY + marginY)) / 2));
                    start = new Vector2(start.X + ((spriteSizeX + marginX) / 2), start.Y + ((spriteSizeY + marginY) / 2));

                    Vector2 position = new Vector2(offSetX + start.X + (x * (spriteSizeX + marginX)), offSetY + start.Y + (y * (spriteSizeY + marginY)));

                    blocks[x, y] = new Block(position, new Vector2(spriteSizeX, spriteSizeY));
                    blocks[x, y].name = "" + x + ", " + y;
                    
                }
        }

        public void OnBlockDestroyed()
        {
            currentBlocksLeft--;
            if (currentBlocksLeft <= 0)
                Game_Arkanoid.OnGameWin();
        }

        public void DestroyAllBlocks()
        {
            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                    blocks[x, y].Destroy();
        }
    }
}
