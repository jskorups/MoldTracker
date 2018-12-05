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
using System.Configuration;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Profil : Form
    {
        public Profil()
        {
            InitializeComponent();

            Image image1 = null;

            using (FileStream stream = new FileStream(@"C:\test\" + loginClass.loginMain + ".jpg", FileMode.Open))
            {
                image1 = Image.FromStream(stream);
            }
            pictureBox2.Image = image1;
        }


        private void Profil_Load(object sender, EventArgs e)
{

    string sql = "select imie, nazwisko, stanowisko, poziomUprawnien, nazwauzytkownika from Uzytkownicy where nazwauzytkownika = '" + loginClass.loginMain + "'";


    string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
    using (SqlConnection conn = new SqlConnection(connectionStrin))
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

    }
}
