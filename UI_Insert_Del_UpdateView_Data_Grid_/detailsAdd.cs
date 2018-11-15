using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class detailsAdd : Form
    {
        public detailsAdd()
        {
            InitializeComponent();
            fillProjects();
            BtnDetailsAdd_validation();
        }

        #region Ładowanie listy projektów
        public void fillProjects()
        {
            try
            {
                DataSet dP = sqlQuery.GetDataFromSql("select projektNazwa from Projekt");
                ComboProjectForDetailsAdd.DataSource = dP.Tables[0];
                //ComboMachineNameRemove.ValueMember = "maszynaId";
                ComboProjectForDetailsAdd.DisplayMember = "projektNazwa";
                ComboProjectForDetailsAdd.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie można załadować listy projektów");
            }
        }
        #endregion
        #region Ładowanie form dla projektu
        private void ComboProjectForDetailsAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            blokowaniePolTextowych();
            DataSet dF = sqlQuery.GetDataFromSql("SELECT formaNazwa FROM Forma INNER JOIN Projekt ON Forma.FK_projektId = Projekt.projektId where projekt.projektNazwa = '" + ComboProjectForDetailsAdd.Text + "'");
            comboBoxFormaDetailsADD.DataSource = dF.Tables[0];
            comboBoxFormaDetailsADD.ValueMember = "formaNazwa";
            comboBoxFormaDetailsADD.SelectedIndex = -1;

        }
        #endregion

        private void comboBoxFormaDetailsADD_SelectedIndexChanged(object sender, EventArgs e)
        {
            blokowaniePolTextowych();
        }
        #region Uwalnianie przycisku dodania do bazy
        private void BtnDetailsAdd_validation()
        {
            bool detal1 = !string.IsNullOrEmpty(detailName1.Text) && !string.IsNullOrEmpty(detailSap1.Text) && !string.IsNullOrEmpty(detailColor1.Text) && !string.IsNullOrEmpty(detailMaterial1.Text);
            bool detal2 = !string.IsNullOrEmpty(detailName2.Text) && !string.IsNullOrEmpty(detailSap2.Text) && !string.IsNullOrEmpty(detailColor2.Text) && !string.IsNullOrEmpty(detailMaterial2.Text);
            bool detal3 = !string.IsNullOrEmpty(detailName3.Text) && !string.IsNullOrEmpty(detailSap3.Text) && !string.IsNullOrEmpty(detailColor3.Text) && !string.IsNullOrEmpty(detailMaterial3.Text);
            bool detal4 = !string.IsNullOrEmpty(detailName4.Text) && !string.IsNullOrEmpty(detailSap4.Text) && !string.IsNullOrEmpty(detailColor4.Text) && !string.IsNullOrEmpty(detailMaterial4.Text);


            if (string.IsNullOrEmpty(detailName1.Text) && string.IsNullOrEmpty(detailName2.Text) && string.IsNullOrEmpty(detailName3.Text) && string.IsNullOrEmpty(detailName4.Text))
            {
                BtnDetailsAddd.Enabled = false;
                BtnDetailsAddd.BackColor = Color.LightGray;
            }
            else if (detal1 == true || detal2 == true || detal3 == true || detal4 == true )
            {
                BtnDetailsAddd.Enabled = true;
                BtnDetailsAddd.BackColor = Color.Lime;
            }
            else if (detal1 == false || detal2 == false || detal3 == false || detal4 == false)
            {
                BtnDetailsAddd.Enabled = false;
                BtnDetailsAddd.BackColor = Color.LightGray;
            }

            // podanie bez name w texboxaxh dla 1,2,3,4
            else if (string.IsNullOrEmpty(detailName1.Text) && (!string.IsNullOrEmpty(detailSap1.Text) || !string.IsNullOrEmpty(detailColor1.Text) || !string.IsNullOrEmpty(detailColor1.Text) || !string.IsNullOrEmpty(detailColor1.Text)))
            {
                BtnDetailsAddd.Enabled = false;
                BtnDetailsAddd.BackColor = Color.LightGray;
            }
            else if (string.IsNullOrEmpty(detailName2.Text) && (!string.IsNullOrEmpty(detailSap2.Text) || !string.IsNullOrEmpty(detailColor2.Text) || !string.IsNullOrEmpty(detailColor2.Text) || !string.IsNullOrEmpty(detailColor2.Text)))
            {
                BtnDetailsAddd.Enabled = false;
                BtnDetailsAddd.BackColor = Color.LightGray;
            }
            else if (string.IsNullOrEmpty(detailName3.Text) && (!string.IsNullOrEmpty(detailSap3.Text) || !string.IsNullOrEmpty(detailColor3.Text) || !string.IsNullOrEmpty(detailColor3.Text) || !string.IsNullOrEmpty(detailColor3.Text)))
            {
                BtnDetailsAddd.Enabled = false;
                BtnDetailsAddd.BackColor = Color.LightGray;
            }
            else if (string.IsNullOrEmpty(detailName4.Text) && (!string.IsNullOrEmpty(detailSap4.Text) || !string.IsNullOrEmpty(detailColor4.Text) || !string.IsNullOrEmpty(detailColor4.Text) || !string.IsNullOrEmpty(detailColor4.Text)))
            {
                BtnDetailsAddd.Enabled = false;
                BtnDetailsAddd.BackColor = Color.LightGray;
            }

        }
        #endregion
        #region Blokowanie pol tekstowych

        private void blokowaniePolTextowych()
        {
            if (string.IsNullOrEmpty(ComboProjectForDetailsAdd.Text) || string.IsNullOrEmpty(comboBoxFormaDetailsADD.Text))
            {
                detailName1.Enabled = false;
                detailName2.Enabled = false;
                detailName3.Enabled = false;
                detailName4.Enabled = false;
            }
            else if (!string.IsNullOrEmpty(ComboProjectForDetailsAdd.Text)  && !string.IsNullOrEmpty(comboBoxFormaDetailsADD.Text))
            {
                detailName1.Enabled = true;
                detailName2.Enabled = true;
                detailName3.Enabled = true;
                detailName4.Enabled = true;
            }
        }
        #endregion
        #region Zmiana tekstu w textboxach
        #region detal 1
        private void detailName1_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }
        private void detailSap1_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }

        private void detailMaterial1_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }

        private void detailColor1_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }
        #endregion
        #region detal 2
        private void detailName2_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }
        private void detailSap2_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }

        private void detailMaterial2_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }

        private void detailColor2_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }
        #endregion
        #region detal 3
        private void detailName3_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }
        private void detailSap3_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }

        private void detailMaterial3_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }

        private void detailColor3_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }
        #endregion
        #region detal 4
        private void detailName4_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }
        private void detailSap4_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }

        private void detailMaterial4_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }

        private void detailColor4_TextChanged(object sender, EventArgs e)
        {
            BtnDetailsAdd_validation();
        }
        #endregion

    

        #endregion
        #region Obługa przycisków

        private void addProjectFromAddDetails_Click_1(object sender, EventArgs e)
        {
            var newProj= new ProjectAdd();
            newProj.ShowDialog();
        }

        private void addMoldFromAddDetails_Click_1(object sender, EventArgs e)
        {
            var newMold = new moldAdd();
            newMold.ShowDialog();
        }
        private void BtnMoldAddClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void BtnDetailsAddd_Click(object sender, EventArgs e)
        {
                    try
                    {
                        string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
                        System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(connectionStrin);
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;

                        sqlConnection1.Open();
                        cmd.CommandText = "select detalNazwa from Detal_komplet where detalNazwa = @detal1 or detalNazwa = @detal2 or detalNazwa = @detal3 or detalNazwa = @detal4 and Forma =  @Forma";
                        cmd.Parameters.AddWithValue("@detal1", detailName1.Text.ToString());
                        cmd.Parameters.AddWithValue("@detal2", detailName2.Text.ToString());
                        cmd.Parameters.AddWithValue("@detal3", detailName3.Text.ToString());
                        cmd.Parameters.AddWithValue("@detal4", detailName4.Text.ToString());
                        cmd.Parameters.AddWithValue("@Forma", comboBoxFormaDetailsADD.ToString());
                        cmd.Connection = sqlConnection1;
                        SqlDataReader reader = cmd.ExecuteReader();
                        
                                if (reader.HasRows)
                                {
                                    MessageBox.Show("Już istnieje");
                                    return;
                                }
                                else if (!reader.HasRows)
                                {
                                    try
                                    {
                                        using (sqlConnection1)
                                        {
                                            reader.Close();
                                            System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand();
                                            cmd1.CommandType = System.Data.CommandType.Text;
                                            cmd1.CommandText = "insert into Detal_komplet(detalNazwa, detalSAP, detalMaterial, detalKolor, FK_formaId, Forma) values" +
                                "( @detalNazwa1, @detalSap1, @detalMaterial1, @detalKolor1, (select formaId from Forma where formaNazwa = @formaNazwa), @formaNazwa), " +
                                "( @detalNazwa2, @detalSap2, @detalMaterial2, @detalKolor2, (select formaId from Forma where formaNazwa = @formaNazwa), @formaNazwa), " +
                                "( @detalNazwa3, @detalSap3, @detalMaterial3, @detalKolor3, (select formaId from Forma where formaNazwa = @formaNazwa), @formaNazwa) , " +
                                "( @detalNazwa4, @detalSap4, @detalMaterial4, @detalKolor4, (select formaId from Forma where formaNazwa = @formaNazwa), @formaNazwa))";

                                            cmd1.Parameters.AddWithValue("@detalNazwa1", detailName1.Text.ToString());
                                            cmd1.Parameters.AddWithValue("@detalSap1", detailSap1.Text.ToString());
                                            cmd1.Parameters.AddWithValue("@detalMaterial1", detailMaterial1.Text.ToString());
                                            cmd1.Parameters.AddWithValue("@detalKolor1", detailColor1.Text.ToString());

                                            cmd1.Parameters.AddWithValue("@detalNazwa2", detailName2.Text.ToString());
                                            cmd1.Parameters.AddWithValue("@detalSap2", detailSap2.Text.ToString());
                                            cmd1.Parameters.AddWithValue("@detalMaterial2", detailMaterial2.Text.ToString());
                                            cmd1.Parameters.AddWithValue("@detalKolor2", detailColor2.Text.ToString());

                                            cmd1.Parameters.AddWithValue("@detalNazwa3", detailName3.Text.ToString());
                                            cmd1.Parameters.AddWithValue("@detalSap3", detailSap3.Text.ToString());
                                            cmd1.Parameters.AddWithValue("@detalMaterial3", detailMaterial3.Text.ToString());
                                            cmd1.Parameters.AddWithValue("@detalKolor3", detailColor3.Text.ToString());

                                            cmd1.Parameters.AddWithValue("@detalNazwa4", detailName4.Text.ToString());
                                            cmd1.Parameters.AddWithValue("@detalSap4", detailSap4.Text.ToString());
                                            cmd1.Parameters.AddWithValue("@detalMaterial4", detailMaterial4.Text.ToString());
                                            cmd1.Parameters.AddWithValue("@detalKolor4", detailColor4.Text.ToString());


                                            cmd1.Parameters.AddWithValue("@formaNazwa", comboBoxFormaDetailsADD.Text.ToString());


                                            cmd1.Connection = sqlConnection1;
                                            cmd1.ExecuteNonQuery();
                                            MessageBox.Show("Nowy detal został dodany!");
                                            sqlConnection1.Close();
                                            this.Close();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Nie można załadowac do bazy danych");
                                    }
                                }
                     }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Blad 2001 ");
                    }
                }








        //else
        //{
        //    MessageBox.Show("Nie wybrałes projektu!");
        //}
    }
        }
//    }
//}
