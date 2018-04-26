using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (loginTxt.Text == "jakub" && passwordTxt.Text == "select from uzytkownicy")
            {
                this.Hide();
                var MainShow = new Main();
                MainShow.Show();    
            }
            else
            {
                MessageBox.Show("Niepoprawna nazwa użytkownika lub hasło");
                loginTxt.Text = String.Empty;
                passwordTxt.Text = String.Empty;
            }
        }
        private void passwordTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (loginTxt.Text == "jakub" && passwordTxt.Text == "jama")
                {
                    this.Hide();
                    var MainShow = new Main();
                    MainShow.Show();
                }
                else
                {
                    MessageBox.Show("Niepoprawna nazwa użytkownika lub hasło");
                    loginTxt.Text = String.Empty;
                    passwordTxt.Text = String.Empty;
                }
            }
        }

        private void logowanieExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
