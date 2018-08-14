using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Logowanie : Form
    {
        public Logowanie()
        {
            InitializeComponent();
        }

        private void loginBtn_Click (object sender, EventArgs e)
        {
            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionStrin);

            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from Uzytkownicy where nazwauzytkownika ='" + loginTxt.Text + "' and haslo = '" + passwordTxt.Text + "'",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                var MainShow = new Main();
                MainShow.Show();
                loginClass.loginMain = loginTxt.Text;
            }
            else
            {
                MessageBox.Show("Niepoprawna nazwa użytkownika lub hasło");
                loginTxt.Text = String.Empty;
                passwordTxt.Text = String.Empty;
            }
        }
        private void logowanieExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
