using System;
using System.Collections.Generic;
using System.Text;

using myEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zCurrentProject
{
    public class Scene_Default : IScene
    {
        //FIELDS

        //CONSTRUCTOR
        public Scene_Default()
        {
            Settings.BACKGROUND_COLOR = Color.Pink;

            Text t = new Text();
            t.s = "SCENE_DEFAULT";
            t.color = Color.White;

            /*
            Sprite s = new Sprite();
            s.texture = Ressources.Load<Texture2D>("yourContent/char");
            s.transform.position = Settings.Get_Screen_Center();
            */
        }

        //METHODS
    }
}
