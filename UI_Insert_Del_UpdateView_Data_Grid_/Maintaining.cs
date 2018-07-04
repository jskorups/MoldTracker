﻿using System;
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


namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Maintaining : Form
    {
        List<string> listaStatusów = new List<string>();
        public Maintaining()
        {
           
         

            InitializeComponent();
            WczytajProbyZalogowanego();

        }

        private void WczytajProbyZalogowanego()
        {
            try
            {
                DataSet ds = sqlQuery.GetDataFromSql("select prob.probaId as 'Id próby', proj.projektNazwa as 'Nazwa projektu', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Maszyna', det.detalNazwa as 'Detal', celT.celNazwa as 'Cel',  statusProby as 'Status', dzienStart as 'Dzień', godzStart as 'Start', celRoz as  'Cel2', odpowiedzialny as 'Odpowiedzialny' from Projekt proj, Forma form, proby prob, Maszyna masz, Detal_komplet det, Cel celT where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and prob.celId = celT.celId  and statusProby = 'Zaplanowana' and odpowiedzialny =(select nazwisko from Uzytkownicy where nazwauzytkownika = 'sgil')");
                dataGridViewProbyLogged.DataSource = ds.Tables[0];
                this.dataGridViewProbyLogged.Columns["Cel"].Visible = false;
                this.dataGridViewProbyLogged.Columns["Cel2"].Visible = false;
                this.dataGridViewProbyLogged.Columns["Odpowiedzialny"].Visible = false;
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
            string nazwaMaszyny = dataGridViewProbyLogged.SelectedCells[3].Value.ToString();
            string nazwaDetalu = dataGridViewProbyLogged.SelectedCells[4].Value.ToString();
            string nazwaCel = dataGridViewProbyLogged.SelectedCells[5].Value.ToString();
            string dzienStart = dataGridViewProbyLogged.SelectedCells[7].Value.ToString();
            string nazwaCel2 = dataGridViewProbyLogged.SelectedCells[9].Value.ToString();
            string odpowiedzialny = dataGridViewProbyLogged.SelectedCells[10].Value.ToString();

            

            if (dzienStart.Length <= 0 )
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

                
                    string templateFilePath = @"\\slssfil01\\Pub-MoldTracker\\Templates\\proba_template.xls";
                    string newFilePath = @"\\slssfil01\\Pub-MoldTracker\\Templates\\" + idProby + "_" + nazwaProjektu + "_" + nazwaFormy + "_" + dzienStart.Replace(@"/", "_") + ".xls";
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


                    //DataSet dP = sqlQuery.GetDataFromSql("select celNazwa from Cel where celId = (select celId from Proby where probaId = '"+idProby+"');");

                    //string costam = dP.Tables["celNazwa"].ToString();
                    //MessageBox.Show(costam);


                    sheet.Close(true, Type.Missing, Type.Missing);
                    excel.Quit();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                return;
            }
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
    
            string idProby = dataGridViewProbyLogged.SelectedCells[0].Value.ToString();
            string nazwaProjektu = dataGridViewProbyLogged.SelectedCells[1].Value.ToString().Replace(@"/", "-");
            string nazwaFormy = dataGridViewProbyLogged.SelectedCells[2].Value.ToString();
            string nazwaDetalu = dataGridViewProbyLogged.SelectedCells[4].Value.ToString(); // zostaw kilka znaków początkowych
            string dzienStart = dataGridViewProbyLogged.SelectedCells[7].Value.ToString(); // zostaw kilka znaków początkowych


            if (dzienStart.Length <= 0 ) {

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
                // string templateFilePath = @"\\slssfil01\\Pub-MoldTracker\\Templates\\proba_template.xls";
                string newFilePath = @"\\slssfil01\\Pub-MoldTracker\\Templates\\" +idProby + "_" +nazwaProjektu+ "_" +nazwaFormy+ "_"+dzienStart.Replace(@"/", "_")+".xls";
                // newFilePath = newFilePath.Replace
                //  File.Copy(@"" + templateFilePath + "", @"" + newFilePath.Replace(@"/", "_") + "");

                bool czyIstnieje = File.Exists(newFilePath);

                if (czyIstnieje == true)
                {
                    Microsoft.Office.Interop.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
                    oXL.Visible = true;
                    oXL.DisplayAlerts = false;
                    oXL.Workbooks.Open(@"\\slssfil01\\Pub-MoldTracker\\Templates\\" + idProby + "_" + nazwaProjektu + "_" + nazwaFormy + "_" + dzienStart.Replace(@"/", "_") + ".xls");
                }
                else{
                    MessageBox.Show("Nie ma takiego pliku - Stwórz plik");
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }

        //string x = "42";
        //int value;
        //if(int.TryParse(x, out value));


        public void dataGridViewProbyLogged_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
           // int number;
           // bool result = Int32.TryParse(dataGridViewProbyLogged.SelectedCells[0].Value.ToString(), out number);
           //// MessageBox.Show(number.ToString());

            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                dataGridViewProbyLogged.CurrentCell = dataGridViewProbyLogged.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dataGridViewProbyLogged.Rows[e.RowIndex].Selected = true;
                dataGridViewProbyLogged.Focus();
            }
            else
            {
              //  MessageBox.Show("Zaznacz cały wiersz");
                return;
            }
        }
        private void zakończPróbęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var TrialEnd = new TrialEnd();
            TrialEnd.ShowDialog();
            WczytajProbyZalogowanego();
        }

        private void maintainZakonczonaCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            listaStatusów.Add("Zakonczona");
        }
        private void maintainAnulowanaCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            listaStatusów.Add("Anulowana");
        }

        private void maintainUsunietaCheckbox_CheckedChanged(object sender, EventArgs e)
        {

            if (maintainUsunietaCheckbox.Checked == true)
            listaStatusów.Add("Usunieta");
        }

        public void maintainPokazButton_Click(object sender, EventArgs e)
        {
            

            using (var connection = new SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4"))
            {
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;
                var sql = "select prob.probaId as 'Id próby', proj.projektNazwa as 'Nazwa projektu', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Maszyna', det.detalNazwa as 'Detal', celT.celNazwa as 'Cel',  statusProby as 'Status', dzienStart as 'Dzień', godzStart as 'Start', celRoz as  'Cel2', odpowiedzialny as 'Odpowiedzialny' from Projekt proj, Forma form, proby prob, Maszyna masz, Detal_komplet det, Cel celT where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and prob.celId = celT.celId  and statusProby in ({0}) and odpowiedzialny = (select nazwisko from Uzytkownicy where nazwauzytkownika = 'sgil')";

                DataSet dP = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", listaStatusów.Select(x => $"\'{x}\'"))));
                DataView source = new DataView(dP.Tables[0]);
                dataGridViewProbyLogged.DataSource = source;

            }




            //DataSet ds = sqlQuery.GetDataFromSql("select prob.probaId as 'Id próby', proj.projektNazwa as 'Nazwa projektu', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Maszyna', det.detalNazwa as 'Detal', celT.celNazwa as 'Cel',  statusProby as 'Status', dzienStart as 'Dzień', godzStart as 'Start', celRoz as  'Cel2', odpowiedzialny as 'Odpowiedzialny' from Projekt proj, Forma form, proby prob, Maszyna masz, Detal_komplet det, Cel celT where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and prob.celId = celT.celId  and statusProby in ({0}) and odpowiedzialny =(select nazwisko from Uzytkownicy where nazwauzytkownika = 'sgil')");
            //dataGridViewProbyLogged.DataSource = ds.Tables[0];

        }


    }
}


