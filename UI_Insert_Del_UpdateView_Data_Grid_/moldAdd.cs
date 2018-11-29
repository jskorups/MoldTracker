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
    public partial class moldAdd : Form
    {
        public moldAdd()
        {
            InitializeComponent();
            fillMold();
            walidacjaMoldAdd();
            blokowaniePolTextowych();
        }
        #region Dodanie form do bazy - przycisk dodaj
        private void BtnMoldAdd_Click(object sender, EventArgs e)
        {
            //sprawdzenie pustych pól
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
                        string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
                        System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(connectionStrin);
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        sqlConnection1.Open();

                        cmd.CommandText = "select formaNazwa from Forma where formaNazwa = @forma1 or formaNazwa = @forma2 or formaNazwa = @forma3 or formaNazwa = @forma4 and FK_projektId = (select proj.projektId from Projekt proj where proj.projektNazwa = @projekt)";
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
                            moldTextBox1.Text = String.Empty;
                            moldTextBox2.Text = String.Empty;
                            moldTextBox3.Text = String.Empty;
                            moldTextBox4.Text = String.Empty;
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
                                    cmd1.Parameters.AddWithValue("@projekt", ComboProjectForMoldAdd.Text.ToString());
                                    cmd1.Connection = sqlConnection1;
                                    cmd1.ExecuteNonQuery();
                                    MessageBox.Show("Nowa forma została dodana!");
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
        #endregion
        #region Ładowanie projektów do comboboxa
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
        #endregion
        #region Obsługa przycisków
        private void BtnMoldAddClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void addProjectFromAddMold_Click(object sender, EventArgs e)
        {
            var projectAdd = new ProjectAdd();
            projectAdd.Show();
        }
        #endregion
        #region Uwalnianie przycisku dodania do bazy danych
        private void walidacjaMoldAdd()
        {
            if (String.IsNullOrEmpty(ComboProjectForMoldAdd.Text) && String.IsNullOrEmpty(moldTextBox1.Text) && String.IsNullOrEmpty(moldTextBox2.Text) && String.IsNullOrEmpty(moldTextBox3.Text) && String.IsNullOrEmpty(moldTextBox4.Text))
            {
                BtnMoldAdd.Enabled = false;
                BtnMoldAdd.BackColor = Color.LightGray;
            }
            else if (moldTextBox1.Text.Length <= 0 && moldTextBox2.Text.Length <= 0 && moldTextBox3.Text.Length <= 0 && moldTextBox4.Text.Length <= 0)
            {
                BtnMoldAdd.Enabled = false;
                BtnMoldAdd.BackColor = Color.LightGray;
            }
            else if ((!String.IsNullOrEmpty(moldTextBox1.Text) || !String.IsNullOrEmpty(moldTextBox2.Text) || !String.IsNullOrEmpty(moldTextBox3.Text) || !String.IsNullOrEmpty(moldTextBox4.Text)))
            {
                BtnMoldAdd.Enabled = true;
                BtnMoldAdd.BackColor = Color.Lime;
            }
        }
        #endregion
        #region Combobox index change
        private void ComboProjectForMoldAdd_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            walidacjaMoldAdd();
            blokowaniePolTextowych();
            moldTextBox1.Text = String.Empty;
            moldTextBox2.Text = String.Empty;
            moldTextBox3.Text = String.Empty;
            moldTextBox4.Text = String.Empty;
        }
        #endregion
        #region Blokowanie pol tesktowych
        private void blokowaniePolTextowych()
        {
            if (ComboProjectForMoldAdd.Text.Length <= 0)
            {
                moldTextBox1.Enabled = false;
                moldTextBox2.Enabled = false;
                moldTextBox3.Enabled = false;
                moldTextBox4.Enabled = false;
            }
            else
            {
                moldTextBox1.Enabled = true;
                moldTextBox2.Enabled = true;
                moldTextBox3.Enabled = true;
                moldTextBox4.Enabled = true;
            }
        }

        #endregion
        #region Zmiana tekstu w textboxach
        private void moldTextBox1_TextChanged(object sender, EventArgs e)
        {
            walidacjaMoldAdd();
        }

        private void moldTextBox2_TextChanged(object sender, EventArgs e)
        {
            walidacjaMoldAdd();
        }

        private void moldTextBox3_TextChanged(object sender, EventArgs e)
        {
            walidacjaMoldAdd();
        }

        private void moldTextBox4_TextChanged(object sender, EventArgs e)
        {
            walidacjaMoldAdd();
        }
        #endregion

        private void addProjectFromMoldAdd_Click(object sender, EventArgs e)
        {
            var newProj = new ProjectAdd();
            newProj.ShowDialog();
        }
    }
}
