using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Sudoku
{
    public class Cell : GameObject
    {
        //FIELDS
        public int posX, posY;
        public Button button;

        bool isCellSelected = false;
        bool finishedSpawned = false;

        public bool isSealled = false;

        public Sprite overlaySprite;

        private static Texture2D cursor;
        private static Texture2D border;

        public Color color = Color.White;

        //CONSTRUCTOR
        public Cell(Vector2 position, Vector2 dimension, int posX, int posY)
        {
            transform.position = position;
            this.posX = posX;
            this.posY = posY;

            button = new Button();
            button.transform = this.transform;
            button.sprite.transform = this.transform;
            button.text.transform = this.transform;

            button.defaultColor = color;
            button.hoverColor = Color.White;
            button.sprite.dimension = new Vector2(dimension.X, dimension.Y);

            button.text.s = "";
            button.text.alignment = Alignment.Center;

            button.onButtonPressed.SetFunction(OnClic);
            button.onHoverEnter.SetFunction(OnHoverEnter);
            button.onHoverExit.SetFunction(OnHoverExit);

            button.isActive = false;

            //CURSOR
            overlaySprite = new Sprite();
            overlaySprite.texture = cursor;
            overlaySprite.transform.position = this.transform.position;
            overlaySprite.dimension = this.button.sprite.dimension;
            overlaySprite.color = Color.Blue;
            overlaySprite.drawOrder = 600;
            overlaySprite.isVisible = false;
        }

        public static void Load()
        {
            cursor = Ressources.Load<Texture2D>("myContent/2D/cursor");
            border = Ressources.Load<Texture2D>("myContent/2D/border");
        }

        //METHODS
        public void Init(string number)
        {
            button.text.s = number;
            isSealled = true;
            button.isActive = false;
            overlaySprite.isVisible = false;

            SetColor(Color.LightGray);
        }

        public void SetCellEmpty()
        {
            isSealled = false;
            button.isActive = true;
            button.text.s = "";

            SetColor(Color.White);
        }

        public void SetColor(Color color)
        {
            this.color = color;

            button.defaultColor = color;
            button.hoverColor = color;
            button.disabledColor = color;
        }

        public void OnHoverEnter()
        {
            if(isCellSelected == false)
            {
                overlaySprite.isVisible = true;
                overlaySprite.texture = cursor;
            }
        }

        public void OnHoverExit()
        {
            if (isCellSelected == false)
            {
                overlaySprite.isVisible = false;
            }
        }

        public void OnClic()
        {
            Game_Sudoku.menu.SubscribeCell(this);
        }

        public void SetCellActive()
        {
            isCellSelected = true;
            //SetColor(Color.White);
            
            overlaySprite.isVisible = true;
            overlaySprite.texture = border;
        }

        public void SetCellToDefault()
        {
            isCellSelected = false;
            SetColor(color);

            overlaySprite.isVisible = false;
        }
    }
}
