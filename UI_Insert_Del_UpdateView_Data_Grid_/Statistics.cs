using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Statistics : Form
    {
        public Statistics()
        {
            InitializeComponent();
            chart6.Series["Series1"].Points.AddXY("Peter", 1000);
            chart6.Series["Series1"].Points.AddXY("John", 5000);
            chart6.Series["Series1"].Points.AddXY("Tan", 1500);
            chart6.Series["Series1"].Points.AddXY("Lucy", 7000);
            chart6.Series["Series1"].Points.AddXY("Peter", 1000);
            chart6.Series["Series1"].Points.AddXY("John", 5000);
            chart6.Series["Series1"].Points.AddXY("Tan", 1500);
            chart6.Series["Series1"].Points.AddXY("Lucy", 7000);

        }

        private void checkedAllProjects(object sender, EventArgs e)
        {
            listBox1.
        }
    }
}
