using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Sudoku
{
    public class Button_UI : EmptyObject
    {
        //FIELDS
        public Button button;
        private static Vector2 dimension = new Vector2(240, 70);
        Sprite overlaySprite;

        //CONSTRUCTOR
        public Button_UI(int index)
        {
            button = new Button();
            button.sprite.dimension = dimension;
            button.transform.position = new Vector2(Settings.SCREEN_WIDTH - (button.sprite.dimension.X * 0.6f),
                                                        (button.sprite.dimension.Y * 1.1f) + (button.sprite.dimension.Y * 1.1f * index));
            button.sprite.transform.position = button.transform.position;

            button.text.alignment = Alignment.Center;
            button.text.s = "Function F" + index;
            button.isActive = false;

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
        public void SetButtonActive()
        {
            this.button.isActive = true;
            overlaySprite.texture = Ressources.Load<Texture2D>("myContent/2D/border");
        }

        public void SetButtonInactive()
        {
            this.button.isActive = false;
            overlaySprite.texture = Ressources.Load<Texture2D>("myContent/2D/locked");
        }
    }
}
