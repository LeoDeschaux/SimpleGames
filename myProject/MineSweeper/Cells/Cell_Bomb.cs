using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.MineSweeper
{
    public class Cell_Bomb : Cell
    {
        //FIELDS

        //CONSTRUCTOR

        public Cell_Bomb(Vector2 position, Vector2 dimension, int x, int y) : base(position, dimension, x, y)
        {
            //button.defaultColor = Color.Pink;
            //button.hoverColor = Color.Red;
            //button.disabledColor = Color.Pink;

            button.onButtonPressed.SetFunction(OnClic);
        }

        //METHODS
        public override void OnClic()
        {
            ShowCell();
            Game_MineSweeper.OnGameLoose();
        }

        public override void ShowCell()
        {
            base.ShowCell();
            button.sprite.texture = Ressources.Load<Texture2D>("myContent/2D/Mine");
        }
    }
}
