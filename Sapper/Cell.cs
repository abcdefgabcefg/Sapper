using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Sapper
{
    class Cell
    {
        // 0 - unknown, 1 - mined, -1 - blow up, 2 - opened
        public int State { get; set; }
        public Panel View { get; set; }
        public bool IsMine { get; set; }
        public Bitmap IsMinePichure { get; set; }
        public Cell (Point coordinates, Size size)
        {
            View = new Panel();
            View.Location = coordinates;
            View.Size = size;
            IsMine = false;
            View.BackColor = Color.Blue;
            State = 0;
        }
        public int NumberMineNear { get; set; }
        public bool Click(MouseEventArgs e)
        {
            bool isOpen = false;
            if (State != 1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    View.BackgroundImage = new Bitmap(Image.FromFile("flag.jpg"), View.Size.Width,
                       View.Size.Height);
                    State = 1;
                }
                if (e.Button == MouseButtons.Left)
                {
                  
                    if (IsMinePichure != null)
                        View.BackgroundImage = IsMinePichure;
                    else
                        View.BackColor = Color.White;
                    isOpen = true;
                    State = 2;
                    if (IsMine)
                    {
                        View.BackgroundImage = new Bitmap(Image.FromFile("mine.jpg"),
                            View.Size.Width, View.Size.Height);
                        AfterGameWindow loseWindow = new AfterGameWindow();
                        loseWindow.ShowDialog();
                        isOpen = false;
                        State = -1;
                    }
                }
            }
           else
            {
                if (State == 1 
                    & e.Button == MouseButtons.Right)
                {
                    View.BackgroundImage = null;
                    View.BackColor = Color.Blue;
                    State = 0;
                }
            }
            return isOpen;
        }
        public void Open()
        {
            if (!IsMine)
            {
                if (IsMinePichure != null)
                    View.BackgroundImage = IsMinePichure;
                else
                    View.BackColor = Color.White;
                State = 2;
            }
            
        }
    }
}
