using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Profil : Form
    {
        public Profil()
        {
            InitializeComponent();
        }

        private void Profil_Load(object sender, EventArgs e)
        {
            //DataSet ds = sqlQuery.GetDataFromSql("select imie, nazwisko, stanowisko, poziomUprawnien, nazwauzytkownika from Uzytkownicy where nazwauzytkownika = 'jskor'");
            //label11.
            //MessageBox.Show("niema");

            //string name = null;
            //string department = null;

            string sql = "select imie, nazwisko, stanowisko, poziomUprawnien, nazwauzytkownika from Uzytkownicy where nazwauzytkownika = '"+loginClass.loginMain+"'";
            string connString = "Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        labelUzytkownik.Text = reader[4] as string;
                        labelImie.Text = reader[0] as string;
                        labelNazwisko.Text = reader[1] as string;
                        labelStanowisko.Text = reader[2] as string;
                        labelUprawnienia.Text = reader[3] as string;

                        //break for single row or you can continue if you have multiple rows...
                        break;
                    }
                }
                conn.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (wgrajZdjecieDialog.ShowDialog() == DialogResult.OK);
        }
    }
}
