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
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();

            #region wczytanie danych do comboboxow
            System.Data.SqlClient.SqlConnection sqlConnection1 =
               //new System.Data.SqlClient.SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4");
               new System.Data.SqlClient.SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True");

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            DataSet dpro = sqlQuery.GetDataFromSql(cmd.CommandText = "select projektNazwa from Projekt;");
            comboProjektSearch.DataSource = dpro.Tables[0];
            comboProjektSearch.ValueMember = "projektNazwa";
            comboProjektSearch.SelectedIndex = -1;

            DataSet dform = sqlQuery.GetDataFromSql(cmd.CommandText = "select formaNazwa from Forma;");
            comboFormaSearch.DataSource = dform.Tables[0];
            comboFormaSearch.ValueMember = "formaNazwa";
            comboFormaSearch.SelectedIndex = -1;

            DataSet dmasz = sqlQuery.GetDataFromSql(cmd.CommandText = "select maszynaNumer from Maszyna;");
            comboMaszynaSearch.DataSource = dmasz.Tables[0];
            comboMaszynaSearch.ValueMember = "maszynaNumer";
            comboMaszynaSearch.SelectedIndex = -1;

            DataSet ddet = sqlQuery.GetDataFromSql(cmd.CommandText = "select detalNazwa from Detal_komplet;");
            comboDetalSearch.DataSource = ddet.Tables[0];
            comboDetalSearch.ValueMember = "detalNazwa";
            comboDetalSearch.SelectedIndex = -1;

            DataSet dcel = sqlQuery.GetDataFromSql(cmd.CommandText = "select celNazwa from Cel;");
            comboCelSearch.DataSource = dcel.Tables[0];
            comboCelSearch.ValueMember = "celNazwa";
            comboCelSearch.SelectedIndex = -1;
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {

            string query = "select * from proby";


            if (comboProjektSearch.Text.Length > 1)
            {
                query = query + " where projektId = (select proj.projektId from Projekt proj where projektNazwa = '" + comboProjektSearch.Text + "')";
            }
            if (comboFormaSearch.Text.Length > 1)
            {
                if (comboProjektSearch.Text.Length > 1)
                {
                    query += " and formaId = (select form.formaID from Forma form where formaNazwa = '" + comboFormaSearch.Text.ToString() + "')";
                }
                else if (comboProjektSearch.Text.Length < 1)
                {
                    query += " where formaId = (select form.formaID from Forma form where formaNazwa = '" + comboFormaSearch.Text.ToString() + "')";
                }
            }
            if (comboMaszynaSearch.Text.Length > 0)
            {
                if (comboFormaSearch.Text.Length > 1 || comboProjektSearch.Text.Length > 1)
                {
                    query += " and maszynaId = (select masz.maszynaID from Maszyna masz where maszynaNumer = '" + comboMaszynaSearch.Text.ToString() + "')";
                }
                else
                {
                    query += " where maszynaId = (select masz.maszynaID from Maszyna masz where maszynaNumer = '" + comboMaszynaSearch.Text.ToString() + "')";
                }
            }


            DataSet dP = sqlQuery.GetDataFromSql(query);
            searchGrid.DataSource = dP.Tables[0];
        }
    }
}
