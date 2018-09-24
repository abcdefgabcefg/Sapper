using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sapper
{
    class Field
    {
        private Cell[,] cells;
        public Field(Size area)
        {
            cells = new Cell[9, 9];
            int x = 0, y = 0;
            // 1 - dinstance between cells
            int wight = (area.Width - 8) / 9, height = (area.Height - 8) / 9;
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    cells[row, column] = new Cell(new Point(x, y), new Size(wight, height));
                    x += wight + 1;
                    cells[row, column].View.MouseClick += MouseClickHandler;
                }
                y += height + 1;
                x = 0;
            }
             SetMines();
             SetNumberMinesNear(); 

        }
        private void SetMines()
        {
            for (int time = 1; time <= 10; time++)
            {
                // for do-while condition
                bool isMine = true;
                // loop do-while is, because one cell can have only one mine
                do
                {
                    int row = new Random().Next(9), column = new Random().Next(9);
                    if (!cells[row, column].IsMine)
                    {
                        cells[row, column].IsMine = true;
                        isMine = false;
                    }
                }
                while (isMine);
            }
        }
        private void SetNumberMinesNear()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                   if (!cells[row, column].IsMine)
                    {
                        int numberMinesNear = 0;
                        if (row - 1 >= 0)
                        {
                            if (cells[row - 1, column].IsMine)
                                numberMinesNear++;
                        }
                        if (row - 1 >= 0 & column + 1 < 9)
                        {
                            if (cells[row - 1, column + 1].IsMine)
                                numberMinesNear++;
                        }
                        if (column + 1 < 9)
                        {
                            if (cells[row, column + 1].IsMine)
                                numberMinesNear++;
                        }
                        if (row + 1 < 9 & column + 1 < 9)
                        {
                            if (cells[row + 1, column + 1].IsMine)
                                numberMinesNear++;
                        }
                        if (row + 1 < 9)
                        {
                            if (cells[row + 1, column].IsMine)
                                numberMinesNear++;
                        }
                        if (row + 1 < 9 & column - 1 >= 0)
                        {
                            if (cells[row + 1, column - 1].IsMine)
                                numberMinesNear++;
                        }
                        if (column - 1 >= 0)
                        {
                            if (cells[row, column - 1].IsMine)
                                numberMinesNear++;
                        }
                        if (row - 1 >= 0 & column - 1 >= 0)
                        {
                            if (cells[row - 1, column - 1].IsMine)
                                numberMinesNear++;
                        }
                        if (numberMinesNear == 1)
                        {
                            cells[row, column].IsMinePichure = new Bitmap(Image.FromFile("one.jpg"),
                                cells[row, column].View.Size.Width, cells[row, column].View.Size.Height);
                        }
                        if (numberMinesNear == 2)
                        {
                            cells[row, column].IsMinePichure = new Bitmap(Image.FromFile("two.jpg"),
                             cells[row, column].View.Size.Width, cells[row, column].View.Size.Height);
                        }
                        if (numberMinesNear == 3)
                        {
                            cells[row, column].IsMinePichure = new Bitmap(Image.FromFile("three.jpg"),
                             cells[row, column].View.Size.Width, cells[row, column].View.Size.Height);
                        }
                        if (numberMinesNear == 4)
                        {
                            cells[row, column].IsMinePichure = new Bitmap(Image.FromFile("four.jpg"),
                             cells[row, column].View.Size.Width, cells[row, column].View.Size.Height);
                        }
                        if (numberMinesNear == 5)
                        {
                            cells[row, column].IsMinePichure = new Bitmap(Image.FromFile("five.jpg"),
                             cells[row, column].View.Size.Width, cells[row, column].View.Size.Height);
                        }
                        if (numberMinesNear == 6)
                        {
                            cells[row, column].IsMinePichure = new Bitmap(Image.FromFile("six.jpg"),
                             cells[row, column].View.Size.Width, cells[row, column].View.Size.Height);
                        }
                        if (numberMinesNear == 7)
                        {
                            cells[row, column].IsMinePichure = new Bitmap(Image.FromFile("seven.jpg"),
                             cells[row, column].View.Size.Width, cells[row, column].View.Size.Height);
                        }
                        if (numberMinesNear == 8)
                        {
                            cells[row, column].IsMinePichure = new Bitmap(Image.FromFile("eight.jpg"),
                             cells[row, column].View.Size.Width, cells[row, column].View.Size.Height);
                        }
                    }
                   
                }
            }
        }

      
        private Index Search(Panel panel)
        {
            Index index = new Index(-1, -1);
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (panel == cells[row, column].View)
                    {
                        index = new Index(row, column);
                        return index;
                    }
                }
            }
            return index;
        }

        // for working whith API
        public Panel[,] GetPanels()
        {
            Panel[,] panels = new Panel[9,9];
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    panels[row, column] = cells[row, column].View;
                }
            }
            return panels;
        }
        private void MouseClickHandler(object sender, MouseEventArgs e)
        {
            Index index = Search((Panel)sender);
            int row = index.row, column = index.column;
            if (cells[index.row, index.column].Click(e))
            {
                if (row - 1 >= 0)
                    cells[row - 1, column].Open();
                if (row - 1 >= 0 & column + 1 < 9)
                    cells[row - 1, column + 1].Open();
                if (column + 1 < 9)
                    cells[row, column + 1].Open();
                if (row + 1 < 9 & column + 1 < 9)
                    cells[row + 1, column + 1].Open();
                if (row + 1 < 9)
                    cells[row + 1, column].Open();
                if (row + 1 < 9 & column - 1 >= 0)
                    cells[row + 1, column - 1].Open();
                if (column - 1 >= 0)
                    cells[row, column - 1].Open();
                if (row - 1 >= 0 & column - 1 >= 0)
                    cells[row - 1, column - 1].Open();
            }
            int numberUnknown = 0;
            for (row = 0; row < 9; row++)
            {
                for (column = 0; column < 9; column++)
                {
                    if (cells[row, column].State == 0)
                    {
                        numberUnknown++;
                    }
                    
                }
            }
            
            if (numberUnknown == 0)
            {
                
                AfterGameWindow window = new AfterGameWindow();
                window.ShowDialog();
            }
            

        }
        private class Index
        {
            public int row;
            public int column;
            public Index(int row, int column)
            {
                this.row = row;
                this.column = column;
            }
        }
    }
}
