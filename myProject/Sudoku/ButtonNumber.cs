using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Sudoku
{
    public class ButtonNumber : GameObject
    {
        //FIELDS
        public Button button;
        public string number;

        //BUTTON PROPERTIES
        Vector2 dimension = new Vector2(70, 70);
        Vector2 startPos = new Vector2(50, 50);
        Color userInputColor = new Color(80, 80, 255);

        Sprite overlaySprite;

        Event onClic;

        //CONSTRUCTOR
        public ButtonNumber(int posX, int posY, string number)
        {
            button = new Button();

            this.number = number;

            Vector2 offSet = new Vector2(startPos.X, startPos.Y);
            Vector2 position = new Vector2(posX * (dimension.X + Grid.marginX), posY * (dimension.Y + Grid.marginY));
            button.transform.position = this.transform.position + offSet + position;
            button.sprite.transform = button.transform;
            button.text.transform = button.transform;

            button.sprite.dimension = dimension;
            button.drawOrder = this.drawOrder + 10;
            button.sprite.drawOrder = button.drawOrder + 11;
            button.text.drawOrder = button.drawOrder + 12;
            button.text.s = number;
            button.text.alignment = Alignment.Center;

            button.defaultColor = Color.White;
            button.isActive = false;

            button.onButtonPressed.SetFunction(OnClic);

            //LOCK SPRITE
            overlaySprite = new Sprite();
            overlaySprite.texture = Ressources.Load<Texture2D>("myContent/2D/locked");
            overlaySprite.transform.position = this.button.sprite.transform.position;
            overlaySprite.dimension = this.button.sprite.dimension;
            overlaySprite.color = Color.Blue;
            overlaySprite.drawOrder = this.drawOrder + 200;
            overlaySprite.isVisible = true;
        }

        //METHODS
        public void SetCell(Cell cell)
        {
            button.onButtonPressed.SetFunction(OnClic);
            this.cell = cell;
        }

        public void RemoveCell()
        {
            this.cell = null;
        }

        public void SetButtonActive(Event onClic)
        {
           this.button.isActive = true;
            overlaySprite.texture = Ressources.Load<Texture2D>("myContent/2D/border");
            this.onClic = onClic;
        }

        public void SetButtonInactive()
        {
            this.button.isActive = false;
            overlaySprite.texture = Ressources.Load<Texture2D>("myContent/2D/locked");
        }

        Cell cell;
        private void OnClic()
        {
            if(cell != null)
            {
                cell.button.text.s = this.number;
                onClic?.Invoke();
                //cell.button.text.color = userInputColor;

                /*
                if (GridUtils.isCellValid(Game_Sudoku.grid, cell))
                    cell.SetColor(Color.Green);
                else
                    cell.SetColor(Color.Red);
                */
            }
        }

        public override void OnDestroy()
        {
            button.Destroy();
        }
    }
}
