using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

using System.Windows.Forms.DataVisualization.Charting;


namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Statistics : Form
    {
        public Statistics()
        {
            InitializeComponent();
            wczytajProjekty();
            chart6.Series["Series1"].Points.AddXY("Peter", 1000);
            chart6.Series["Series1"].Points.AddXY("John", 5000);
            chart6.Series["Series1"].Points.AddXY("Tan", 1500);
            chart6.Series["Series1"].Points.AddXY("Lucy", 7000);
            chart6.Series["Series1"].Points.AddXY("Peter", 1000);
            chart6.Series["Series1"].Points.AddXY("John", 5000);
            chart6.Series["Series1"].Points.AddXY("Tan", 1500);
            chart6.Series["Series1"].Points.AddXY("Lucy", 7000);

        }

        public void wczytajProjekty ()
        {
             try
            {
                DataSet dP = sqlQuery.GetDataFromSql("select * from Projekt");
                listBox1.DataSource = dP.Tables[0];
                listBox1.ValueMember = "projektNazwa";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}


        private void checkAllProjects(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                    listBox1.SetSelected(i, true);
            }
            else if (checkBox1.Checked == false )
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                    listBox1.SetSelected(i, false);
            }
        }

        private void listBoxClick(object sender, EventArgs e)
        {

        }
    }
}
