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
using System.Data.SqlClient;


namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Statistics : Form
    {
        List<string> wybraneProjekty = new List<string>();

        public Statistics()
        {
            InitializeComponent();
            wczytajProjekty();
            listBox1.SelectedIndex = -1;
           

            //// fakechart
            //chart6.Series["Series1"].Points.AddXY("Peter", 1000);
            //chart6.Series["Series1"].Points.AddXY("John", 5000);
            //chart6.Series["Series1"].Points.AddXY("Tan", 1500);
        
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

            

            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                wybraneProjekty.Add(listBox1.GetItemText(listBox1.SelectedItems[i]));
            }
            foreach (string item in wybraneProjekty)
            comboBox1.DataSource = wybraneProjekty;
            //wybraneProjekty.Clear();

            using (var connection = new SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True")) 
            {
                connection.Open();

                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;

                var sql = "select proj.projektNazwa as 'Projekt', COUNT(prob.projektId) as 'Liczba prob' from Proby prob LEFT JOIN Projekt proj ON proj.projektId = prob.projektId where proj.projektNazwa in ({0}) and dzienStart between '2018-05-09' and '2018-05-22' group by proj.projektNazwa;";
                
                DataSet dP = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", wybraneProjekty.Select(x => $"\'{x}\'"))));
                dataGridView1.DataSource = dP.Tables[0];


                //var idList = new List<int> { 100, 50, 40, 20, 10 };
                ////var idParameterList = new List<string>();
                //var index = 0;
                ////foreach (var id in idList)
                ////{
                ////    var paramName = "@idParam" + index;
                ////    sqlCommand.Parameters.AddWithValue(paramName, id);
                ////    idParameterList.Add(paramName);
                ////    index++;
                ////}

                ///*new List<String> { "sebastian", "kuuro", "svenbit" };*/
                //var nameList = wybraneProjekty;
                //var nameParameter = new List<string>();
                //index = 0; // Reset the index
                //foreach (var name in nameList)
                //{

                //    sqlCommand.Parameters.Add(name);
                //    nameParameter.Add(name);
                //    index++;
                //}




                //using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                //{
                //    DataTable resultTable = new DataTable();
                //    //resultTable.Load(sqlReader);
                //    DataView myDataView = new DataView(resultTable);
                //    dataGridView1.DataSource = myDataView;
                //}

                connection.Close();
            }

        }  
            
    }
    }

