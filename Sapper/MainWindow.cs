using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sapper
{
    public partial class MainWindow : Form
    {
        private Field field;
        public MainWindow()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            field = new Field(this.ClientSize);
            Panel[,] panels = field.GetPanels();
            foreach(Panel panel in panels)
            {
                this.Controls.Add(panel);
            } 
        }
    }
}
