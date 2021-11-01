using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Arkanoid
{
    public class PopUp : GameObject
    {
        //FIELDS
        public Sprite background;
        public Button button;

        //CONSTRUCTOR
        public PopUp()
        {
            transform.position = Settings.GetScreenCenter();
            
            background = new Sprite();
            background.color = Color.Black;
            background.dimension = new Vector2(500, 300);
            background.transform = this.transform;

            button = new Button();
            button.sprite.dimension = new Vector2(200, 80);

            button.defaultColor = Color.White;
            button.hoverColor = Color.Yellow;
            button.onClicColor = Color.Red;

            button.text.s = "Rejouer";
            button.text.alignment = Alignment.Center;

            button.text.transform = this.transform;
            button.sprite.transform = this.transform;

            background.drawOrder = 488;
            button.sprite.drawOrder = 499;
            button.text.drawOrder = 500;

        }

        //METHODS

    }
}
