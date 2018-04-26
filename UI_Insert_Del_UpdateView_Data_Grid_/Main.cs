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
using System.Threading;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{

    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            wczytajDoGridView_Click(null, null);
        }
        private void wczytajDoGridView_Click(object sender, EventArgs e)
        {
           WczytajOstatnieProby();
        }

        #region - Wczytanie danych o ostanich dziesieciu probach  z bazy danych
        private void WczytajOstatnieProby()
        {
            try
            {
                DataSet ds = sqlQuery.GetDataFromSql("select top 14 probaid,projektId,formaId,maszynaId,detalId from Proby;");

                ostatnioDodaneGrid.DataSource = ds.Tables[0];
                ostatnioDodaneGrid.Columns["probaId"].Width = 30;
                ostatnioDodaneGrid.Columns["projektId"].Width = 40;
                ostatnioDodaneGrid.Columns["formaId"].Width = 50;
                ostatnioDodaneGrid.Columns["maszynaId"].Width = 30;
                ostatnioDodaneGrid.Columns["detalId"].Width = 100;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region - Button wyloguj się
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Czy chcesz zostać wylogowany?", "Wyloguj się", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
                Logowanie fl = new Logowanie();
                fl.Show();
            }
        }
        #endregion
        #region - Button - odswiezanie Datagrid - ostanie proby
        private void button6_Click(object sender, EventArgs e)
        {
            WczytajOstatnieProby();
        }
        #endregion
        #region - Buttons - przyciski otwierajace moduły oraz profil/ustawienia/wiadomosci

        private void addBtn_Click(object sender, EventArgs e)
        {
            var add = new addFill();
            add.ShowDialog();
            WczytajOstatnieProby();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            var search = new Search();
            search.Show();
        }
        private void planningBtn_Click(object sender, EventArgs e)
        {
            var planning = new Planning();
            planning.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var assets = new Assets();
            assets.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var Prof = new Profil();
            Prof.Show();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            var Ust = new Ustawienia();
            Ust.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var maintaing = new Maintaining();
            maintaing.Show();
        }
        #endregion


    }
}
