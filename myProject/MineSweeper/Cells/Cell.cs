using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.MineSweeper
{
    public abstract class Cell : EmptyObject
    {
        //FIELDS
        public Button button;
        public int x;
        public int y;

        public bool isRevealed = false;
        private bool isMarked = false;

        //CONSTRUCTOR
        public Cell(Vector2 position, Vector2 dimension, int x, int y)
        {
            this.x = x;
            this.y = y;

            button = new Button();
            button.transform.position = position;
            button.sprite.dimension = dimension;
            button.sprite.transform = button.transform;
            button.text.transform = button.transform;

            button.disabledColor = Color.White;

            button.text.s = "";
            button.text.drawOrder = button.sprite.drawOrder + 1000;

            button.sprite.texture = Ressources.Load<Texture2D>("myContent/2D/Cell");
        }

        //METHODS
        public override void Update()
        {
            if (Input.GetMouseDown(MouseButtons.Right) && button.sprite.GetRectangle().Contains(Mouse.position))
                OnLeftClic();
        }

        public abstract void OnClic();

        public virtual void OnLeftClic()
        {
            if (isRevealed)
                return;

            if (!isMarked)
            {
                isMarked = true;
                button.sprite.texture = Ressources.Load<Texture2D>("myContent/2D/Crossed");
            }
            else
            {
                isMarked = false;
                button.sprite.texture = Ressources.Load<Texture2D>("myContent/2D/Cell");
            }
        }

        public virtual void ShowCell()
        {
            isRevealed = true;

            Game_MineSweeper.grid.OnShowCell(this);

            button.isActive = false;
            button.disabledColor = Color.White;
        }

        public override void OnDestroy()
        {
            button.Destroy();
        }
    }
}
