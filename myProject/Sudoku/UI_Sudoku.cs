using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Sudoku
{
    public class UI_Sudoku : EmptyObject
    {
        //FIELDS
        public Button_UI[] buttons;
         
        //CONSTRUCTOR
        public UI_Sudoku(Grid grid)
        {
            buttons = new Button_UI[3];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button_UI(i);
            }

            SetButton(0, "New Grid", () => OnNewGrid(grid));
            SetButton(1, "CHEAT", () => Cheat(grid));
            SetButton(2, "Show Answer", () => OnShowAnswer(grid));

            buttons[0].SetButtonActive();
        }

        private void OnNewGrid(Grid grid)
        {
            GridUtils.CreateNewGrid(grid);
            buttons[1].SetButtonActive();
            buttons[2].SetButtonActive();
        }

        public void Cheat(Grid grid)
        {
            //GridUtils.CheckGrid(grid);
            GridUtils.SetGridToAnwser(grid);
        }

        private void OnShowAnswer(Grid grid)
        {
            if (GridUtils.IsGridComplete(grid))
                Game_Sudoku.OnGridIsComplete();
            else
                Console.WriteLine("DOMMAGE");

            GridUtils.ShowAnwser(grid);
            
            buttons[1].SetButtonInactive();
            buttons[2].SetButtonInactive();

            Game_Sudoku.menu.SetMenuInactive();
        }

        //METHODS
        public void SetButton(int num, string text, Action function)
        {
            buttons[num].button.text.s = text;
            buttons[num].button.onButtonPressed = new Event(function.Invoke);
        }
    }
}