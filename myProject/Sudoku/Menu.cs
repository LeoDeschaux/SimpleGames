using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Sudoku
{
    public class Menu : EmptyObject
    {
        //FIELDS
        ButtonNumber[] buttons;
        private Cell cellRef;

        //CONSTRUCTOR
        public Menu()
        {
            buttons = new ButtonNumber[10];

            //SHOW CHOICE
            buttons[0] = new ButtonNumber(0, 0, "1");
            buttons[1] = new ButtonNumber(0, 1, "2");
            buttons[2] = new ButtonNumber(0, 2, "3");

            buttons[3] = new ButtonNumber(0, 3, "4");
            buttons[4] = new ButtonNumber(0, 4, "5");
            buttons[5] = new ButtonNumber(0, 5, "6");

            buttons[6] = new ButtonNumber(0, 6, "7");
            buttons[7] = new ButtonNumber(0, 7, "8");
            buttons[8] = new ButtonNumber(0, 8, "9");

            buttons[9] = new ButtonNumber(1, 0, "");
            Text t = new Text();
            t.s = "E";
            t.alignment = Alignment.Center;
            t.drawOrder = buttons[9].button.drawOrder + 20;
            t.transform = buttons[9].button.transform;


            laDouille = new Button();
            laDouille.sprite.dimension = new Vector2(Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT);
            laDouille.sprite.transform.position = Settings.GetScreenCenter();
            laDouille.defaultColor = Color.Transparent;
            laDouille.hoverColor = Color.Transparent;
            laDouille.onClicColor = Color.Transparent;

            laDouille.drawOrder = -999;
            laDouille.sprite.drawOrder = -999;

            laDouille.text.s = "";

            laDouille.onButtonPressed.SetFunction(OnClicOutSide);
        }

        Button laDouille;

        //METHODS
        public void OnClic()
        {
            if (cellRef.button.text.s != "")
                buttons[9].SetButtonActive(new Event(OnClic));
            else
                buttons[9].SetButtonInactive();  
        }

        public void OnClicOutSide()
        {
            UnSubscribePreviousCell();
        }

        public void SubscribeCell(Cell cell)
        {
            UnSubscribePreviousCell();

            cellRef = cell;
            cellRef.SetCellActive();
            SetMenuActive();
        }

        public void UnSubscribePreviousCell()
        {
            cellRef?.SetCellToDefault();
            SetMenuInactive();
        }

        private void SetMenuActive()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetCell(cellRef);
                buttons[i].SetButtonActive(new Event(OnClic));
            }

            buttons[9].SetButtonInactive();
            if (cellRef.button.text.s != "")
                buttons[9].SetButtonActive(new Event(OnClic));
        }

        public void SetMenuInactive()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].RemoveCell();
                buttons[i].SetButtonInactive();
            }

            buttons[9].SetButtonInactive();
        }

        public override void OnDestroy()
        {
            for (int i = 0; i < 9; i++)
                buttons[i].Destroy();
        }
    }
}
