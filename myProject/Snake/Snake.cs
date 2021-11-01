using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myEngine.myProject.Snake;

namespace myEngine.myProject.Snake
{
    public class Snake : GameObject
    {
        //FIELDS
        public Vec2Int gridPosition;
        public Vec2Int direction;

        //
        public Sprite head;

        //
        public int length = 0;
        public Vec2Int[] previousPositions;
        public Vec2Int lastPosition;

        //CONSTRUCTOR
        public Snake()
        {
            gridPosition = new Vec2Int(0,0);
            lastPosition = gridPosition;

            direction = new Vec2Int(1,0);

            head = new Sprite();
            head.transform.position = new Vector2(1130, 500);
            head.dimension = new Vector2(50, 50);
            head.texture = Ressources.Load<Texture2D>("myContent/2D/Snake_Head");

            length = 0;
            previousPositions = new Vec2Int[length+1];
        }

        //METHODS
        public void OnSnakeMove()
        {
            lastPosition = gridPosition;

            for (int i = previousPositions.Length; i > previousPositions.Length - length; i--)
            {
                DestroyAtPos(previousPositions[i - 1]);
            }

            for (int i = length; i > 1; i--)
            {
                previousPositions[i-1] = previousPositions[i-2];
            }

            previousPositions[0] = lastPosition;

            for (int i = previousPositions.Length; i > previousPositions.Length - length; i--)
            {
                DrawSnakeAtPos(previousPositions[i-1]);
            }

            gridPosition += direction;

            PrintInfo();
        }

        public void PrintInfo()
        {
            Console.Clear();
            Console.WriteLine("Length: " + length);
            Console.WriteLine("previousPos.Length: " + previousPositions.Length);
        }

        public void DestroyAtPos(Vec2Int pos)
        {
            Game_Snake.grid.cells[pos.X, pos.Y].Destroy();
            Game_Snake.grid.cells[pos.X, pos.Y] = new Cell_Empty(Game_Snake.grid.cells[pos.X, pos.Y].sprite.transform.position);
        }

        public void DrawSnakeAtPos(Vec2Int pos)
        {
            Game_Snake.grid.cells[pos.X, pos.Y].Destroy();
            Game_Snake.grid.cells[pos.X, pos.Y] = new Cell_Body(Game_Snake.grid.cells[pos.X, pos.Y].sprite.transform.position, this);
        }

        public void OnEatApple()
        {
            length++;

            Vec2Int[] temp = previousPositions;
            previousPositions = new Vec2Int[length];
            Array.Copy(temp, previousPositions, length-1);
        }

        //DRAW & UPDATE
        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            //DrawSimpleShape.DrawRuller(Settings.Get_Screen_Center(), Color.Red);
            DrawSimpleShape.DrawRuller(head.transform.position, Color.Red);
        }

        public override void Update()
        {
            //head.transform.rotation += speed * Time.deltaTime;

            if (Input.GetKeyDown(Keys.Up))
            {
                direction = new Vec2Int(0,-1);
                head.transform.rotation = 270;
            }

            if (Input.GetKeyDown(Keys.Down))
            {
                direction = new Vec2Int(0,1);
                head.transform.rotation = 90;
            }

            if (Input.GetKeyDown(Keys.Left))
            {
                direction = new Vec2Int(-1,0);
                head.transform.rotation = 180;
            }

            if (Input.GetKeyDown(Keys.Right))
            {
                direction = new Vec2Int(1,0);
                head.transform.rotation = 0;
            }
        }
    }
}
