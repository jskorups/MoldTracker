﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Configuration;
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
            ustawBtn();
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
                    string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
                    System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(connectionStrin);
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;



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
                            cmd1.Parameters.AddWithValue("@nowyProjekt", TextBoxProjectNameAdd.Text.ToString().ToUpper());
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
        private void ustawBtn()
        {
            if (String.IsNullOrEmpty(TextBoxProjectNameAdd.Text))
            {
                BtnProjectAdd.Enabled = false;
                BtnProjectAdd.BackColor = Color.LightGray;
            }
            else if (!String.IsNullOrEmpty(TextBoxProjectNameAdd.Text))
            {
                BtnProjectAdd.Enabled = true;
                BtnProjectAdd.BackColor = Color.Lime;
            }
        }
        private void TextBoxProjectNameAdd_TextChanged(object sender, EventArgs e)
        {
            ustawBtn();
        }
    }
    }

