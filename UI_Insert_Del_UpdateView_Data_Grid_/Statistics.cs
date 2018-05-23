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
        List<string> wybraneProjekty = new List<string>();
        public Statistics()
        {
            InitializeComponent();
            wczytajProjekty();
            //listBox1.SelectedIndex = 0;

            // fakechart
            chart6.Series["Series1"].Points.AddXY("Peter", 1000);
            chart6.Series["Series1"].Points.AddXY("John", 5000);
            chart6.Series["Series1"].Points.AddXY("Tan", 1500);
        
        }

        public void wczytajProjekty ()
        {
             try
            {
                DataSet dP = sqlQuery.GetDataFromSql("select * from Projekt");
                listBox1.DataSource = dP.Tables[0];
                listBox1.DisplayMember = "projektNazwa";
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

        private void button1_Click(object sender, EventArgs e)
        {

            string str = string.Empty;
            foreach (object selectedItem in listBox1.SelectedItems)
            {
                str += listBox1.GetItemText(listBox1.SelectedItem) + Environment.NewLine;
            }
            MessageBox.Show(str);

            
            for (int i = 0; i <= listBox1.SelectedItems.Count; i++)
            {
                MessageBox.Show(listBox1.GetItemText(listBox1.SelectedItems[i]) + Environment.NewLine);
                //textboxes[i].Text = "Item: " + listBox1.Items[i].ToString();
            }
        }

        }
    }

