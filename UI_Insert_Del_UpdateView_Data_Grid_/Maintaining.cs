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
                DataSet ds = sqlQuery.GetDataFromSql("select proj.projektNazwa as 'Nazwa projektu', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Maszyna', det.detalNazwa as 'Detal', statusProby as 'Status', dzienStart as 'Dzień', godzStart as 'Godzina' from Projekt proj, Forma form, proby prob, Maszyna masz, Detal_komplet det where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId and odpowiedzialny =(select nazwisko from Uzytkownicy where nazwauzytkownika = '" + loginClass.loginMain + "')");
                dataGridViewProbyLogged.DataSource = ds.Tables[0];

                //foreach (DataGridViewRow row in dataGridViewProbyLogged.Rows)
                //{
                //    foreach (DataGridViewCell cell in row.Cells)
                //    {
                //        if (cell.Value.ToString() == "Zaplanowana")
                //        {
                //            cell.Style.BackColor = Color.Orange;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           


        }
    }



}


