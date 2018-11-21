using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Maintaining : Form
    {
        

        List<string> listaStatusów = new List<string>();

        public Maintaining()
        {

            //dateTimePickerMaintainOd.CustomFormat = "ddd dd MMM yyyy";

            //dateTimeTerminRealizacjiGodzinaStart.MinDate = DateTime.Parse("6:00:00");

            //dateTimeTerminRealizacjiGodzinaKoniec.CustomFormat = "HH:mm";
            //dateTimeTerminRealizacjiGodzinaKoniec.MinDate = DateTime.Parse("8:00:00");
            //dateTimeTerminRealizacjiGodzinaStart.ShowUpDown = true;
            //dateTimeTerminRealizacjiGodzinaKoniec.ShowUpDown = true;


            InitializeComponent();
            WczytajProbyZalogowanego();
       
        }
        private void setdatetime()
        {
            //dateTimePickerMaintainOd.
            dateTimePickerMaintainOd.Format = DateTimePickerFormat.Custom;
            dateTimePickerMaintainOd.CustomFormat = "yyyy-MM-dd";

            dateTimePickerMaintainDo.Format = DateTimePickerFormat.Custom;
            dateTimePickerMaintainDo.CustomFormat = "yyyy-MM-dd";
        }

        #region Wczytywanie prób zalogowanego
        private void WczytajProbyZalogowanego()
        {
            try
            {
                DataSet ds = sqlQuery.GetDataFromSql("select prob.probaId as 'Id próby', proj.projektNazwa as 'Nazwa projektu', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Maszyna', det.detalNazwa as 'Detal', celT.celNazwa as 'Cel',  statusProby as 'Status', dzienStart as 'Dzień', godzStart as 'Start', celRoz as  'Cel2', odpowiedzialny as 'Odpowiedzialny', czasTrwania as 'Czas' from Projekt proj, Forma form, proby prob, Maszyna masz, Detal_komplet det, Cel celT where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and prob.celId = celT.celId  and statusProby = 'Zaplanowana' and odpowiedzialny =(select nazwisko from Uzytkownicy where nazwauzytkownika = '" + loginClass.loginMain + "')");
                dataGridViewProbyLogged.DataSource = ds.Tables[0];
                this.dataGridViewProbyLogged.Columns["Cel"].Visible = false;
                this.dataGridViewProbyLogged.Columns["Cel2"].Visible = false;
                this.dataGridViewProbyLogged.Columns["Odpowiedzialny"].Visible = false;
                this.dataGridViewProbyLogged.Columns["Czas"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Swtórz plik - Menu Strip
        public void stwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string idProby = dataGridViewProbyLogged.SelectedCells[0].Value.ToString();
            string nazwaProjektu = dataGridViewProbyLogged.SelectedCells[1].Value.ToString().Replace(@"/", "-");
            string nazwaFormy = dataGridViewProbyLogged.SelectedCells[2].Value.ToString();
            string nazwaMaszyny = dataGridViewProbyLogged.SelectedCells[3].Value.ToString();
            string nazwaDetalu = dataGridViewProbyLogged.SelectedCells[4].Value.ToString();
            string nazwaCel = dataGridViewProbyLogged.SelectedCells[5].Value.ToString();
            string dzienStart = dataGridViewProbyLogged.SelectedCells[7].Value.ToString();
            string nazwaCel2 = dataGridViewProbyLogged.SelectedCells[9].Value.ToString();
            string odpowiedzialny = dataGridViewProbyLogged.SelectedCells[10].Value.ToString();
            string czasTrw = dataGridViewProbyLogged.SelectedCells[11].Value.ToString();

            if (dzienStart.Length <= 0)
            {
                MessageBox.Show("Nie można wykonać takiej akcji");
                return;
            }
            else
            {
                dzienStart = dzienStart.Replace(@"/", "_");
                dzienStart = dzienStart.Substring(0, dzienStart.Length - 9);
            }
            if (dataGridViewProbyLogged.SelectedCells.Count > 3)
            {
                try
                {
                   //string rok = System.DateTime.Now.Year.ToString();
                   // //string miesiac = System.DateTime.Now.Month.ToString();


                   // string monthName = DateTime.Today.ToString("MMMM");

                   // string folderyTermin = @"\\slssfil01\\Pub-MoldTracker\\Raport\\"+rok+"\\"+ monthName+ "";


                   // MessageBox.Show(folderyTermin);

                   // // string foldery = @"\\slssfil01\\Pub-MoldTracker\\Raport\\proba_template.xlsx";
                   // ////// bool czyIstnieje = File.Exists(foldery);

                   // // if (czyIstnieje == true)
                   // // {
                   // //     Microsoft.Office.Interop.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
                   // //     oXL.Visible = true;
                   // //     oXL.DisplayAlerts = false;
                   // //     oXL.Workbooks.Open(@"\\slssfil01\\Pub-MoldTracker\\Templates\\" + idProby + "_" + nazwaProjektu + "_" + nazwaFormy + "_" + dzienStart.Replace(@"/", "_") + ".xlsx");
                   // // }
                   // // else
                   // // {
                   // //     MessageBox.Show("Nie ma takiego pliku - Stwórz plik");
                    // }
                    string templateFilePath = @"\\slssfil01\\Pub-MoldTracker\\Templates\\proba_template.xlsx";

                    string newFilePath = @"\\slssfil01\\Pub-MoldTracker\\Raporty\\" + idProby + "_" + nazwaProjektu + "_" + nazwaFormy + "_" + dzienStart.Replace(@"/", "_") + ".xlsx";

                    File.Copy(@"" + templateFilePath + "", @"" + newFilePath + "");

                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook sheet = excel.Workbooks.Open(newFilePath);
                    Microsoft.Office.Interop.Excel.Worksheet x = excel.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;

                    x.Range["F5"].Value = nazwaProjektu;
                    x.Range["F6"].Value = nazwaDetalu;
                    x.Range["F7"].Value = nazwaFormy;
                    x.Range["F8"].Value = nazwaMaszyny;
                    x.Range["F9"].Value = nazwaCel;
                    x.Range["F10"].Value = nazwaCel2;
                    x.Range["AC6"].Value = dataGridViewProbyLogged.SelectedCells[7].Value.ToString().Remove(10);
                    x.Range["AC8"].Value = odpowiedzialny;
                    x.Range["Y2"].Value = idProby;
                    x.Range["AC2"].Value = System.DateTime.Today;
                    x.Range["AC5"].Value = czasTrw;

                    
                    sheet.Close(true, Type.Missing, Type.Missing);
                    excel.Quit();
                    MessageBox.Show("Pomyślnie stworzono plik");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Plik nie został stworzony");
                return;
            }


            WczytajProbyZalogowanego();

        }
        #endregion
        #region Otworz plik - Menu Strip
        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string idProby = dataGridViewProbyLogged.SelectedCells[0].Value.ToString();
            string nazwaProjektu = dataGridViewProbyLogged.SelectedCells[1].Value.ToString().Replace(@"/", "-");
            string nazwaFormy = dataGridViewProbyLogged.SelectedCells[2].Value.ToString();
            string nazwaDetalu = dataGridViewProbyLogged.SelectedCells[4].Value.ToString(); // zostaw kilka znaków początkowych
            string dzienStart = dataGridViewProbyLogged.SelectedCells[7].Value.ToString(); // zostaw kilka znaków początkowych


            if (dzienStart.Length <= 0)
            {
                MessageBox.Show("Nie można wykonać takiej akcji");
                return;
            }
            else
            {
                dzienStart = dzienStart.Replace(@"/", "_");
                dzienStart = dzienStart.Substring(0, dzienStart.Length - 9);
            }
            try
            {
                string newFilePath = @"\\slssfil01\\Pub-MoldTracker\\Raporty\\" + idProby + "_" + nazwaProjektu + "_" + nazwaFormy + "_" + dzienStart.Replace(@"/", "_") + ".xlsx";

                bool czyIstnieje = File.Exists(newFilePath);

                if (czyIstnieje == true)
                {
                    Microsoft.Office.Interop.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
                    oXL.Visible = true;
                    oXL.DisplayAlerts = true;
                    oXL.Workbooks.Open(@"\\slssfil01\\Pub-MoldTracker\\Raporty\\" + idProby + "_" + nazwaProjektu + "_" + nazwaFormy + "_" + dzienStart.Replace(@"/", "_") + ".xlsx");
                }
                else
                {
                    MessageBox.Show("Nie ma takiego pliku - Stwórz plik");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Zaznacz caly wiersz prawym
        public void dataGridViewProbyLogged_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int number;
            bool result = Int32.TryParse(dataGridViewProbyLogged.SelectedCells[0].Value.ToString(), out number);
            selectedDataGridmaintain.selectedId = number;

            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0 && result == true)
            {

                dataGridViewProbyLogged.CurrentCell = dataGridViewProbyLogged.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dataGridViewProbyLogged.Rows[e.RowIndex].Selected = true;
                maintainStripZaplanowana.Enabled = true;
                dataGridViewProbyLogged.Focus();

                if(dataGridViewProbyLogged.SelectedCells[6].Value.ToString() == "Zakonczona" && dataGridViewProbyLogged.SelectedRows.Count > 0)
                {

                    zakończPróbęToolStripMenuItem.Enabled = false;
                    stwórzToolStripMenuItem.Enabled = false;
                    Raporty.Enabled = true;
                    otwórzToolStripMenuItem.Enabled = true;
                    stwórzToolStripMenuItem.Enabled = true;
                    kontynuujPróbęToolStripMenuItem.Enabled = true;
                }
                else if (dataGridViewProbyLogged.SelectedCells[6].Value.ToString() == "Zaplanowana" && dataGridViewProbyLogged.SelectedRows.Count > 0)
                {
                    zakończPróbęToolStripMenuItem.Enabled = true;
                    kontynuujPróbęToolStripMenuItem.Enabled = false;
                    usuńPróbęToolStripMenuItem.Enabled = false;
                    otwórzToolStripMenuItem.Enabled = false;
                    stwórzToolStripMenuItem.Enabled = false;
                    Raporty.Enabled = false;
                    //zakończPróbęToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                maintainStripZaplanowana.Enabled = false;
                return;
            }
        }
        #endregion
        #region Otwieranie Zakonczenia próby - podaj czas
        private void zakończPróbęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var TrialEnd = new TrialEnd();
            TrialEnd.ShowDialog();
            WczytajProbyZalogowanego();
        }
        #endregion
        #region Dodawanie elementów do listy - Checkboxy
        private void maintainZakonczonaCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (maintainZakonczonaCheckbox.Checked == true)
            {
                listaStatusów.Add("Zakonczona");
            }
            else
            {
                listaStatusów.Remove("Zakonczona");
            }
        }
        private void maintainAnulowanaCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (maintainAnulowanaCheckbox.Checked == true)
            {
                listaStatusów.Add("Anulowana");
            }
            else
            {
                listaStatusów.Remove("Anulowana");
            }
        }
        private void maintainUsunietaCheckbox_CheckedChanged(object sender, EventArgs e)
        {

            if (maintainUsunietaCheckbox.Checked == true)
            {
                listaStatusów.Add("Usunieta");
            }
            else
            {
                listaStatusów.Remove("Usunieta");
            }
        }
        #endregion
        #region Button Pokaż
        public void maintainPokazButton_Click(object sender, EventArgs e)
        {
            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;

            using (var connection = new SqlConnection(connectionStrin))

            {
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;

                string dzienOd = dateTimePickerMaintainDo.Value.Date.ToString();
                string dzienDo = dateTimePickerMaintainDo.Value.Date.ToString();

                var sql = "select prob.probaId as 'Id próby', proj.projektNazwa as 'Nazwa projektu', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Maszyna', det.detalNazwa as 'Detal', celT.celNazwa as 'Cel',  statusProby as 'Status', dzienStart as 'Dzień', godzStart as 'Start', celRoz as  'Cel2', odpowiedzialny as 'Odpowiedzialny', czasTrwania as 'Czas' from Projekt proj, Forma form, proby prob, Maszyna masz, Detal_komplet det, Cel celT where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and prob.celId = celT.celId  and statusProby in ({0}) and odpowiedzialny = (select nazwisko from Uzytkownicy where nazwauzytkownika = '" + loginClass.loginMain + "') and dzienStart between '" + dateTimePickerMaintainOd.Value.ToDateTimeString() + "' and '" + dateTimePickerMaintainDo.Value.ToDateTimeString() + "'";

                listaStatusów.Add("Zaplanowana");

                DataSet dP = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", listaStatusów.Select(x => $"\'{x}\'"))));
                DataView source = new DataView(dP.Tables[0]);
                dataGridViewProbyLogged.DataSource = source;

            }
        }
        #endregion
        #region Usuwanie próby
        private void usuńPróbęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string idProby = dataGridViewProbyLogged.SelectedCells[0].Value.ToString();

            if (MessageBox.Show("Czy chcesz usunąć próbę?", "Potwierdź usunięcie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
                System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(connectionStrin);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = ("UPDATE Proby SET statusProby = 'Usunieta' WHERE probaId = @probaId ");
                cmd.Parameters.AddWithValue("@probaId", dataGridViewProbyLogged.SelectedCells[0].Value.ToString());

                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();

                WczytajProbyZalogowanego();
            }
            else
            {
                MessageBox.Show("Próba nie dodana");
                return;
            }
        }
        #endregion
        #region Kontynuowanie próby
        private void kontynuujPróbęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string idProby = dataGridViewProbyLogged.SelectedCells[0].Value.ToString();

            if (MessageBox.Show("Czy chcesz przywrócic próbę?", "Potwierdź przywrócenie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {

                string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;

                System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(connectionStrin);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = ("UPDATE Proby SET statusProby = 'Zaplanowana' WHERE probaId = @probaId ");
                cmd.Parameters.AddWithValue("@probaId", dataGridViewProbyLogged.SelectedCells[0].Value.ToString());

                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();


            }
            else
            {
                MessageBox.Show("Próba nie przywrócna");
                return;
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dateTimePickerMaintainDo.Value.ToDateTimeString());
            MessageBox.Show(dateTimePickerMaintainOd.Value.ToDateTimeString());
        }

     
    }
}


