using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class ProjectAdd : Form
    {
        public ProjectAdd()
        {
            InitializeComponent();
        }

        private void BtnProjectAddClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnProjectAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TextBoxProjectNameAdd.Text))
            {
                MessageBox.Show("Nie wpisałeś nic!");
                TextBoxProjectNameAdd.Clear();
                return;
            }
            else
            {
                try
                {
                    System.Data.SqlClient.SqlConnection sqlConnection1 =
                    //new System.Data.SqlClient.SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4");
                    new System.Data.SqlClient.SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True");

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select * from Projekt where projektNazwa = @nowyProjekt";
                    cmd.Parameters.AddWithValue("@nowyProjekt", TextBoxProjectNameAdd.Text.ToString());
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        MessageBox.Show("Już istnieje");
                        TextBoxProjectNameAdd.Clear();
                        return;
                    }
                    else
                    {
                        using (sqlConnection1)
                        {
                            System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand();
                            cmd1.CommandType = System.Data.CommandType.Text;
                            reader.Close();
                            cmd1.CommandText = "insert into Projekt(projektNazwa) values(@nowyProjekt)";
                            cmd1.Parameters.AddWithValue("@nowyProjekt", TextBoxProjectNameAdd.Text.ToString());
                            cmd1.Connection = sqlConnection1;
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Nowy projekt został dodany!");
                            sqlConnection1.Close();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
