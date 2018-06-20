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
using 


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
                DataSet ds = sqlQuery.GetDataFromSql("select prob.probaId as 'Id próby', proj.projektNazwa as 'Nazwa projektu', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Maszyna', det.detalNazwa as 'Detal', statusProby as 'Status', dzienStart as 'Dzień', godzStart as 'Godzina' from Projekt proj, Forma form, proby prob, Maszyna masz, Detal_komplet det where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and statusProby = 'Zaplanowana' and odpowiedzialny =(select nazwisko from Uzytkownicy where nazwauzytkownika = 'sgil')");
                dataGridViewProbyLogged.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        public void stwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show(dataGridViewProbyLogged.SelectedCells[0].Value.ToString());
            MessageBox.Show(dataGridViewProbyLogged.SelectedCells[1].Value.ToString());
            MessageBox.Show(dataGridViewProbyLogged.SelectedCells[2].Value.ToString());
            MessageBox.Show(dataGridViewProbyLogged.SelectedCells[3].Value.ToString());

            try
            {
                FileInfo newFile = new FileInfo(@"\slssfil01\Pub-MoldTracker\Pliki_proby\test1");
                FileInfo template = new FileInfo(@"\\slssfil01\Pub-MoldTracker\Templates\\proba_template.xlsx");

                using (ExcelPackage xlPackage = new ExcelPackage(newFile, template))
                {

                    //Added This part
                    foreach (ExcelWorksheet aworksheet in xlPackage.Workbook.Worksheets)
                    {
                        aworksheet.Cell(1, 1).Value = aworksheet.Cell(1, 1).Value;
                    }

                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets["My Data"];

                    excel cell = worksheet.Cell(5, 1);
                    cell.Value = "15";

                    //worksheet.Cell(5, 1).Value = "Soap";

                    xlPackage.Save();
                    //Response.Write("Excel file created successfully");
                }

            }
            catch (Exception ex)
            {
                //Response.WriteFile(ex.InnerException.ToString());
            }




        }
        private void dataGridViewProbyLogged_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridViewProbyLogged.CurrentCell = dataGridViewProbyLogged.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dataGridViewProbyLogged.Rows[e.RowIndex].Selected = true;
                dataGridViewProbyLogged.Focus();
            }
            else
            {
                return;
            }
        }
    }
}


