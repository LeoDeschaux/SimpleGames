using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Sudoku
{
    public class GridOverlay : EmptyObject
    {
        //FIELDS
        Rectangle rectangle;
        float thickness = 5;
        Color color = Color.Black;

        //CONSTRUCTOR
        public GridOverlay(Vector2 startPoint, Vector2 endPoint)
        {
            rectangle = new Rectangle(
                (int)startPoint.X,
                (int)startPoint.Y,
                (int)endPoint.X,
                (int)endPoint.Y);

            this.drawOrder = 500;
        }

        //METHODS
        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            DrawSimpleShape.DrawRectangle(rectangle, 0, color, thickness: thickness);

            //VERTICAL
            DrawSimpleShape.DrawLine(new Vector2(rectangle.X + ((float)rectangle.Width / (float)3), rectangle.Y), 
                                     new Vector2(rectangle.X + ((float)rectangle.Width / (float)3), rectangle.Y+rectangle.Height),
                                     color,
                                     thickness: thickness);

            DrawSimpleShape.DrawLine(new Vector2(rectangle.X + (((float)rectangle.Width / (float)3)*2), rectangle.Y),
                                     new Vector2(rectangle.X + (((float)rectangle.Width / (float)3)*2), rectangle.Y+rectangle.Height),
                                     color,
                                     thickness: thickness);

            //HORIZONTAL
            DrawSimpleShape.DrawLine(new Vector2(rectangle.X, rectangle.Y + ((float)rectangle.Height / (float)3)),
                                     new Vector2(rectangle.X+rectangle.Width, rectangle.Y + ((float)rectangle.Height / (float)3)),
                                     color,
                                     thickness: thickness);

            DrawSimpleShape.DrawLine(new Vector2(rectangle.X, rectangle.Y + (((float)rectangle.Height/(float)3) * 2)),
                                     new Vector2(rectangle.X + rectangle.Width, rectangle.Y + (((float)rectangle.Height / (float)3) * 2)),
                                     color,
                                     thickness: thickness);
        }
    }
}
