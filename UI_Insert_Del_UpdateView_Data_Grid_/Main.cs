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
using System.Timers;

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
                DataSet ds = sqlQuery.GetDataFromSql("select top 15 proj.projektNazwa as 'Nazwa projektu', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Maszyna', det.detalNazwa as 'Detal',dzienStart as 'Dzień', godzStart as 'Godzina', statusProby as 'Status' from Projekt proj, Forma form, proby prob, Maszyna masz, Detal_komplet det where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId order by probaId desc; ");
                ostatnioDodaneGrid.DataSource = ds.Tables[0];   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Kolorwanie komorek
        private void colouringCells(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in ostatnioDodaneGrid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString() == "Zaplanowana")
                    {
                        cell.Style.BackColor = Color.Yellow;
                        
                    }
                    else if (cell.Value.ToString() == "Anulowana")
                    {
                        cell.Style.BackColor = Color.Red;
                    }
                    else if (cell.Value.ToString() == "Zakonczona")
                    {
                        cell.Style.BackColor = Color.LimeGreen;
                    }
                    else if (cell.Value.ToString() == "Usunieta")
                    {
                        cell.Style.BackColor = Color.Violet;
                    }
                }
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
           // var planning = new Form1();
            //planning.Show();
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
        private void statisticsBtn_Click(object sender, EventArgs e)
        {
            var statistic = new Statistics();
            statistic.Show();
        }

        #endregion

        private void zamkniecieMain(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //private void buttonMsg_Click(object sender, EventArgs e)
        ////{
        ////    var maintaing = new MSG();
        //    maintaing.Show();
        //}
    }
}
