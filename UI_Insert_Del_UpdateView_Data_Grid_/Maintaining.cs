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


namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Maintaining : Form
    {
      
        public Maintaining()
        {

            InitializeComponent();
            WczytajProbyZalogowanego();
        }
        private void WczytajProbyZalogowanego()
        {
            try
            {
                DataSet ds = sqlQuery.GetDataFromSql("select prob.probaId as 'Id próby', proj.projektNazwa as 'Nazwa projektu', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Maszyna', det.detalNazwa as 'Detal', statusProby as 'Status', dzienStart as 'Dzień', godzStart as 'Start' from Projekt proj, Forma form, proby prob, Maszyna masz, Detal_komplet det where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and statusProby = 'Zaplanowana' and odpowiedzialny =(select nazwisko from Uzytkownicy where nazwauzytkownika = 'sgil')");
                dataGridViewProbyLogged.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void stwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string idProby = dataGridViewProbyLogged.SelectedCells[0].Value.ToString();
            string nazwaProjektu = dataGridViewProbyLogged.SelectedCells[1].Value.ToString().Replace(@"/", "-");
            string nazwaFormy = dataGridViewProbyLogged.SelectedCells[2].Value.ToString();
            string nazwaDetalu = (dataGridViewProbyLogged.SelectedCells[4].Value.ToString()); // zostaw kilka znaków początkowych
            string dzienStart = (dataGridViewProbyLogged.SelectedCells[6].Value.ToString()); // zostaw kilka znaków początkowych

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
                string templateFilePath = @"\\slssfil01\\Pub-MoldTracker\\Templates\\proba_template.xls";
                string newFilePath = @"\\slssfil01\\Pub-MoldTracker\\Templates\\" + idProby + "_" + nazwaProjektu + "_" + nazwaFormy + "_" + dzienStart.Replace(@"/", "_") + ".xls";
                File.Copy(@"" + templateFilePath + "", @"" + newFilePath + "");


                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook sheet = excel.Workbooks.Open(newFilePath);
                Microsoft.Office.Interop.Excel.Worksheet x = excel.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;


                x.Range["B14"].Value = idProby;

                sheet.Close(true, Type.Missing, Type.Missing);
                excel.Quit();

                //oXL.Visible = true;
                //oXL.DisplayAlerts = false;

                //oXL.Workbooks.Open(@"\\slssfil01\\Pub-MoldTracker\\Templates\\" +idProby + "_" + nazwaProjektu + "_" + nazwaFormy+ "_" + dzienStart.Replace(@"/", "_") + ".xls");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string idProby = dataGridViewProbyLogged.SelectedCells[0].Value.ToString();
            string nazwaProjektu = dataGridViewProbyLogged.SelectedCells[1].Value.ToString().Replace(@"/", "-");
            string nazwaFormy = dataGridViewProbyLogged.SelectedCells[2].Value.ToString();
            string nazwaDetalu = dataGridViewProbyLogged.SelectedCells[4].Value.ToString(); // zostaw kilka znaków początkowych
            string dzienStart = dataGridViewProbyLogged.SelectedCells[6].Value.ToString(); // zostaw kilka znaków początkowych


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
                string newFilePath = @"\\slssfil01\\Pub-MoldTracker\\Templates\\" + idProby + "_" + nazwaProjektu + "_" + nazwaFormy + "_" + dzienStart.Replace(@"/", "_") + ".xls";
                bool czyIstnieje = File.Exists(newFilePath);

                if (czyIstnieje == true)
                {
                    Microsoft.Office.Interop.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
                    oXL.Visible = true;
                    oXL.DisplayAlerts = false;
                    oXL.Workbooks.Open(@"\\slssfil01\\Pub-MoldTracker\\Templates\\" + idProby + "_" + nazwaProjektu + "_" + nazwaFormy + "_" + dzienStart.Replace(@"/", "_") + ".xls");
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
        private void dataGridViewProbyLogged_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                selectedDataGridmaintain.selectedId = Convert.ToInt16(dataGridViewProbyLogged.SelectedCells[0].Value);
                dataGridViewProbyLogged.CurrentCell = dataGridViewProbyLogged.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dataGridViewProbyLogged.Rows[e.RowIndex].Selected = true;
                dataGridViewProbyLogged.Focus();
            }
            else
            {
                return;
            }
        }

        private void zakończPróbęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var TrialEnd = new TrialEnd();
            TrialEnd.Show();



        }
    }
}


