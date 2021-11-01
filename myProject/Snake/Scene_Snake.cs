using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

using ImGuiNET;
using myEngine;

using myEngine.myProject.Snake;

namespace myEngine
{
    public class Scene_Snake : IScene
    {
        //FIELDS
        Game_Snake game;

        //TODO: convert UI to seperate class

        //CONSTRUCTOR
        public Scene_Snake()
        {
            Text t = new Text();
            t.s = "Snake_Game";
            t.color = Color.White;

            game = new Game_Snake();
        }

        System.Numerics.Vector2 wSize = new System.Numerics.Vector2(300, 200);
        System.Numerics.Vector2 position;

        public override void DrawGUI()
        {
            return;

            ImGui.Begin("Rotation");
            ImGui.SetWindowSize(wSize);

            ImGui.Text("Params");
            //ImGui.SliderFloat("Scale", ref game.player.head.dimension.X, 0, 2, string.Empty);

            //System.Numerics.Vector2 position = new System.Numerics.Vector2(game.player.head.transform.position.X, game.player.head.transform.position.Y);

            position = new System.Numerics.Vector2(game.snake.head.transform.position.X, game.snake.head.transform.position.Y);

            ImGui.DragFloat2("Transform Position", ref position);

            //ImGui.InputFloat("PosX", ref game.player.head.transform.position.X, 0, 1280, string.Empty);
            //ImGui.InputFloat("PoY", ref game.player.head.transform.position.Y, 0, 720, string.Empty);

            ImGui.SliderFloat("dimensionX", ref game.snake.head.dimension.X, 0, 200, string.Empty);
            ImGui.SliderFloat("dimensionY", ref game.snake.head.dimension.Y, 0, 200, string.Empty);

            ImGui.SliderFloat("ScaleX", ref game.snake.head.transform.scale.X, 0, 2, string.Empty);
            ImGui.SliderFloat("ScaleY", ref game.snake.head.transform.scale.Y, 0, 2, string.Empty);

            ImGui.End();

            game.snake.head.transform.position.X = position.X;
            game.snake.head.transform.position.Y = position.Y;
        }

        //METHODS
        public override void Update()
        {

        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {

        }
    }
}