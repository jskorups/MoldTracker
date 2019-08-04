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
using System.Configuration;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class machineAdd : Form
    {
        public machineAdd()
        {
            InitializeComponent();
            ustawBtn();
        }

        private void BtnMachineAddClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMachineAdd_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(TextBoxMachineNameAdd.Text, out parsedValue))
            {
                MessageBox.Show("Tutaj mozesz wprowadzic tylko numer maszyny :) ");
                TextBoxMachineNameAdd.Clear();
                return;
            }
            else
            {
                try
                {

                    string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(connectionStrin);
                    
                   // new System.Data.SqlClient.SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True");

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select * from Maszyna where maszynaNumer = @nowaMaszyna";
                    cmd.Parameters.AddWithValue("@nowaMaszyna", Convert.ToInt32(TextBoxMachineNameAdd.Text.ToString()));
                    cmd.Connection = con;
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        MessageBox.Show("Już istnieje");
                        TextBoxMachineNameAdd.Clear();
                        return;
                    }
                    else
                    {
                        using (con)
                        {
                            System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand();
                            cmd1.CommandType = System.Data.CommandType.Text;
                            reader.Close();
                            cmd1.CommandText = "insert into Maszyna(maszynaNumer) values(@nowaMaszyna)";
                            cmd1.Parameters.AddWithValue("@nowaMaszyna", Convert.ToInt32(TextBoxMachineNameAdd.Text.ToString()));
                            cmd1.Connection = con;
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Nowa maszyna została dodana!");
                            con.Close();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Brak połączenia z bazą");
                }
            }
        }
        private void ustawBtn()
        {
            if (String.IsNullOrEmpty(TextBoxMachineNameAdd.Text))
            {
                BtnMachineAdd.Enabled = false;
                BtnMachineAdd.BackColor = Color.LightGray;
            }
            else if (!String.IsNullOrEmpty(TextBoxMachineNameAdd.Text))
            {
                BtnMachineAdd.Enabled = true;
                BtnMachineAdd.BackColor = Color.Lime;
            }
        }

        private void TextBoxMachineNameAdd_TextChanged(object sender, EventArgs e)
        {
            ustawBtn();
        }
    }
}

