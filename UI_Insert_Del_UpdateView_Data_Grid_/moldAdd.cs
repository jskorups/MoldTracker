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
    public partial class moldAdd : Form
    {
        public moldAdd()
        {
            InitializeComponent();
            fillMold();
        }

        private void BtnMoldAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(moldTextBox1.Text) && String.IsNullOrEmpty(moldTextBox2.Text) && String.IsNullOrEmpty(moldTextBox3.Text) && String.IsNullOrEmpty(moldTextBox4.Text))
            {
                MessageBox.Show("Nie wpisałeś nic!");
                ComboProjectForMoldAdd.SelectedIndex = -1;
                moldTextBox1.Clear();
                moldTextBox2.Clear();
                moldTextBox4.Clear();
                moldTextBox1.Clear();
                return;
            }
            else
            {
                if (ComboProjectForMoldAdd.Text.Length > 1)
                {
                    try
                    {
                        System.Data.SqlClient.SqlConnection sqlConnection1 =
                        new System.Data.SqlClient.SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4");
                        //new System.Data.SqlClient.SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True");

                        sqlConnection1.Open();
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select formaNazwa from Forma where formaNazwa = @forma1 or formaNazwa = @forma2 and FK_projektId = (select proj.projektId from Projekt proj where proj.projektNazwa = @projekt)";
                        cmd.Parameters.AddWithValue("@forma1", moldTextBox1.Text.ToString());
                        cmd.Parameters.AddWithValue("@forma2", moldTextBox2.Text.ToString());
                        cmd.Parameters.AddWithValue("@forma3", moldTextBox3.Text.ToString());
                        cmd.Parameters.AddWithValue("@forma4", moldTextBox4.Text.ToString());
                        cmd.Parameters.AddWithValue("@projekt", ComboProjectForMoldAdd.ToString());
                        cmd.Connection = sqlConnection1;
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            MessageBox.Show("Już istnieje");
                            ComboProjectForMoldAdd.SelectedIndex = -1;
                            return;
                        }
                        else
                        {
                            try
                            {
                                using (sqlConnection1)
                                {
                                    reader.Close();
                                    System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand();
                                    cmd1.CommandType = System.Data.CommandType.Text;
                                    cmd1.CommandText = "insert into Forma(formaNazwa, FK_projektId) values( @forma1,(select projektId from Projekt where projektNazwa = @projekt)),(@forma2,(select projektId from Projekt where projektNazwa = @projekt)),(@forma3,(select projektId from Projekt where projektNazwa = @projekt)),(@forma4,(select projektId from Projekt where projektNazwa = @projekt))";
                                    cmd1.Parameters.AddWithValue("@forma1", moldTextBox1.Text.ToString());
                                    cmd1.Parameters.AddWithValue("@forma2", moldTextBox2.Text.ToString());
                                    cmd1.Parameters.AddWithValue("@forma3", moldTextBox3.Text.ToString());
                                    cmd1.Parameters.AddWithValue("@forma4", moldTextBox4.Text.ToString());
                                    cmd1.Parameters.AddWithValue("@projekt",ComboProjectForMoldAdd.Text.ToString());
                                    cmd1.Connection = sqlConnection1;
                                    cmd1.ExecuteNonQuery();
                                    MessageBox.Show("Nowy projekt został dodany!");
                                    sqlConnection1.Close();
                                    this.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Blad 2001 ");
                    }
                }
                else
                {
                    MessageBox.Show("Nie wybrałes projektu!");
                }
            }
        }
        public void fillMold()
        {
            try
            {
                DataSet dP = sqlQuery.GetDataFromSql("select projektNazwa from Projekt");
                ComboProjectForMoldAdd.DataSource = dP.Tables[0];
                //ComboMachineNameRemove.ValueMember = "maszynaId";
                ComboProjectForMoldAdd.DisplayMember = "projektNazwa";
                ComboProjectForMoldAdd.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie można załadować listy projektów");
            }
        }
        private void BtnMoldAddClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void addProjectFromAddMold_Click(object sender, EventArgs e)
        {
            var projectAdd = new ProjectAdd();
            projectAdd.Show();
        }
    }
}
