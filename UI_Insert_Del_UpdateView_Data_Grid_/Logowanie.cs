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
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True");
            //new SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4");
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
        //private void passwordTxt_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (loginTxt.Text == "jakub" && passwordTxt.Text == "jama")
        //        {
        //            this.Hide();
        //            var MainShow = new Main();
        //            MainShow.Show();
                   
        //        }
        //        else
        //        {
        //            MessageBox.Show("Niepoprawna nazwa użytkownika lub hasło");
        //            loginTxt.Text = String.Empty;
        //            passwordTxt.Text = String.Empty;
        //        }
        //    }
        //}

        private void logowanieExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
