using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

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

            DataSet dpro = sqlQuery.GetDataFromSql(cmd.CommandText = "select projektId, projektNazwa from Projekt;");
            comboProjektSearch.DataSource = dpro.Tables[0];
            comboProjektSearch.ValueMember = "projektId";
            comboProjektSearch.DisplayMember = "projektNazwa";
            comboProjektSearch.SelectedIndex = -1;

            DataSet dform = sqlQuery.GetDataFromSql(cmd.CommandText = "select formaId, formaNazwa from Forma;");
            comboFormaSearch.DataSource = dform.Tables[0];
            comboFormaSearch.ValueMember = "formaId";
            comboFormaSearch.DisplayMember = "formaNazwa";
            comboFormaSearch.SelectedIndex = -1;

            DataSet dmasz = sqlQuery.GetDataFromSql(cmd.CommandText = "select  maszynaId, maszynaNumer from Maszyna;");
            comboMaszynaSearch.DataSource = dmasz.Tables[0];
            comboMaszynaSearch.ValueMember = "maszynaNumer";
            comboFormaSearch.DisplayMember = "maszynaId";
            comboMaszynaSearch.SelectedIndex = -1;

            DataSet ddet = sqlQuery.GetDataFromSql(cmd.CommandText = "select detalId, detalNazwa from Detal_komplet;");
            comboDetalSearch.DataSource = ddet.Tables[0];
            comboDetalSearch.ValueMember = "detalId";
            comboDetalSearch.DisplayMember = "detalNazwa";
            comboDetalSearch.SelectedIndex = -1;

            DataSet dcel = sqlQuery.GetDataFromSql(cmd.CommandText = "select celId, celNazwa from Cel;");
            comboCelSearch.DataSource = dcel.Tables[0];
            comboCelSearch.ValueMember = "celId";
            comboCelSearch.DisplayMember = "celNazwa";
            comboCelSearch.SelectedIndex = -1;
        }
        #endregion
        public void button1_Click(object sender, EventArgs e)
        {

            string query = "select prob.probaId as 'Id próby' , proj.projektNazwa as 'Nazwa projektu', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Numer maszyny', det.detalNazwa as 'Nazwa detalu', prob.godzStart as 'Godzina', prob.dzienStart as 'Dzien', ce.celNazwa as 'Cel próby', prob.statusProby as 'Status próby' from proby prob, projekt proj, forma form, Maszyna masz, Detal_komplet det, cel ce where prob.projektId = proj.projektId and prob.formaId = form.formaId and prob.maszynaId = masz.maszynaId and prob.detalId = det.detalId and prob.celId =  ce.celId and prob.dzienStart between '" + dateTimeOd.Value.Date + "' and '" + dateTimeDo.Value.Date + "'";

            if (comboProjektSearch.Text.Length > 1)
            {
                query = "select prob.probaId as 'Id próby' , proj.projektNazwa as 'Nazwa projektu', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Numer maszyny', det.detalNazwa as 'Nazwa detalu', prob.godzStart as 'Godzina', prob.dzienStart as 'Dzien', ce.celNazwa as 'Cel próby', prob.statusProby as 'Status próby' from proby prob, projekt proj, forma form, Maszyna masz, Detal_komplet det, cel ce where prob.projektId = proj.projektId and prob.formaId = form.formaId and prob.maszynaId = masz.maszynaId and prob.detalId = det.detalId and prob.celId =  ce.celId and proj.projektId = '" + comboProjektSearch.SelectedValue + "'";
            }
            if (comboFormaSearch.Text.Length > 1)
            {
                query += " and form.formaId = '" + comboFormaSearch.SelectedValue + "' ";
            }
            if (comboMaszynaSearch.Text.Length > 1)
            {
                query += " and masz.maszynaNumer = '" + comboMaszynaSearch.SelectedValue + "' ";
            }
            if (comboDetalSearch.Text.Length > 1)
            {
                query += " and det.detalId = '" + comboDetalSearch.SelectedValue + "' ";
            }
            if (comboCelSearch.Text.Length > 1)
            {
                query += " and ce.celId = '" + comboCelSearch.SelectedValue + "' ";
            }
            if (zaplanowanaCheckBox.Checked)
            {
                query += " and prob.statusProby = 'Zaplanowana' ";
            }
            if (zakonczonaCheckBox.Checked)
            {
                query += " and prob.statusProby = 'Zakonczona'";
            }
            if (anulowanaCheckBox.Checked)
            {
                query += " and prob.statusProby = 'Anulowana'";
            }
            if (usunietaCheckBox.Checked)
            {
                query += " and prob.statusProby = 'Usunięta'";
            }
            DataSet dP = sqlQuery.GetDataFromSql(query);
            searchGrid.DataSource = dP.Tables[0];
        }

        private void zaplanowanaClick(object sender, EventArgs e)
        {
            anulowanaCheckBox.Checked = false;
            usunietaCheckBox.Checked = false;
            zakonczonaCheckBox.Checked = false;
        }

        private void wykonanaClick(object sender, EventArgs e)
        {
            anulowanaCheckBox.Checked = false;
            usunietaCheckBox.Checked = false;
            zaplanowanaCheckBox.Checked = false;
        }
        private void anulowanaClick(object sender, EventArgs e)
        {
            zakonczonaCheckBox.Checked = false;
            usunietaCheckBox.Checked = false;
            zaplanowanaCheckBox.Checked = false;
        }

        private void usunietaClick(object sender, EventArgs e)
        {
            zakonczonaCheckBox.Checked = false;
            anulowanaCheckBox.Checked = false;
            zaplanowanaCheckBox.Checked = false;
        }

        private void detailSearchClear_Click(object sender, EventArgs e)
        {
            comboProjektSearch.SelectedIndex = -1;
            comboCelSearch.SelectedIndex = -1;
            comboDetalSearch.SelectedIndex = -1;
            comboFormaSearch.SelectedIndex = -1;
            comboMaszynaSearch.SelectedIndex = -1;
        }

        private void statusSearchClear_Click(object sender, EventArgs e)
        {
            zaplanowanaCheckBox.Checked = false;
            anulowanaCheckBox.Checked = false;
            usunietaCheckBox.Checked = false;
            zakonczonaCheckBox.Checked = false;
        }


        #region Otwieranie Folderu
        private void otwórzFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(@"C:\drivers"))
            {
                Process.Start(@"C:\drivers");
            }
            else
            {
                MessageBox.Show("Podana lokalizacja nie istnieje. Skontaktuj sie z administratorem");
            }
              
        }
        #endregion

        #region Otwieranie Excela
        private void otworzExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string partialName = searchGrid.SelectedCells[0].Value.ToString() + "_";
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"\\slssfil01\\Pub-MoldTracker\\Raporty\\");
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(partialName + "*.*");

            if (filesInDir.Length > 0)
            {
                foreach (FileInfo foundFile in filesInDir)
                {
                    string fullName = foundFile.FullName;

                    Microsoft.Office.Interop.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
                    oXL.Visible = true;
                    oXL.DisplayAlerts = false;
                    oXL.Workbooks.Open(fullName);
                }
            }
            else
            {
                MessageBox.Show("Nie można odnalezc pliku");
            }
            #endregion
        }

        //private void wykonanaCheckBox_CheckedChanged(object sender, EventArgs e)
        //{

        //}
    }
}
    

