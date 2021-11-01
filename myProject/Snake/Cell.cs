using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace myEngine.myProject.Snake
{
    public enum CellType
    {
        empty,
        head,
        body,
        apple
    }

    public static class CellColors
    {
        public static Color empty = Color.White;
        public static Color head = Color.Green;
        public static Color body = Color.Green;
        public static Color apple = Color.Red;
    }

    public abstract class Cell : EmptyObject
    {
        //FIELDS
        public Vec2Int position;
        public CellType type;
        public Sprite sprite;
        public Color color;
        public int orderInLayer = -500;

        public static float dimension = 50;

        //CONSTRUCTOR 

        //METHODS
        public override void OnDestroy()
        {
            sprite.Destroy();
        }
    }

    public class Cell_Empty : Cell
    {
        //FIELDS

        //CONSTRUCTOR 
        public Cell_Empty(Vector2 position)
        {
            sprite = new Sprite();
            sprite.transform.position = position;
            sprite.dimension = new Vector2(dimension, dimension);
            sprite.drawOrder = orderInLayer;

            this.color = CellColors.empty;
            this.type = CellType.empty;

            sprite.color = color;
        }

        //METHODS

    }

    public class Cell_Head : Cell
    {
        //FIELDS

        //CONSTRUCTOR 
        public Cell_Head(Vector2 position, Snake snake)
        {
            sprite = new Sprite();
            sprite.transform.position = position;
            sprite.dimension = new Vector2(dimension, dimension);

            sprite.drawOrder = 500;

            this.color = CellColors.head;
            this.type = CellType.head;

            //sprite.color = color;

            sprite.texture = snake.head.texture;
            sprite.transform.rotation = snake.head.transform.rotation;
        }

        //METHODS

    }

    public class Cell_Body : Cell
    {
        //FIELDS

        //CONSTRUCTOR 
        public Cell_Body(Vector2 position, Snake snake)
        {
            sprite = new Sprite();
            sprite.transform.position = position;
            sprite.dimension = new Vector2(dimension, dimension);

            sprite.drawOrder = 500;

            this.color = CellColors.body;
            this.type = CellType.body;

            sprite.color = color;

            //sprite.texture = snake.head.texture;
            //sprite.transform.rotation = snake.head.transform.rotation;
        }

        //METHODS
    }

    public class Cell_Apple : Cell
    {
        //FIELDS

        //CONSTRUCTOR 
        public Cell_Apple(Vector2 position)
        {
            sprite = new Sprite();
            sprite.transform.position = position;
            sprite.dimension = new Vector2(dimension, dimension);
            sprite.drawOrder = orderInLayer;

            this.color = CellColors.apple;
            this.type = CellType.apple;

            sprite.color = color;
        }

        //METHODS

    }
}
