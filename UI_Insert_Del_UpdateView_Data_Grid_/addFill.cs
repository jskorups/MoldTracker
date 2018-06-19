using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class addFill : Form
    {
 

        public addFill()
        {
            InitializeComponent();
            cmboFormaDetalSap();
            sprawdzenieCombosow();

        #region - Format czasu dla DateTimePickera
            dateTimeTerminRealizacjiGodzina.Format = DateTimePickerFormat.Custom;
            dateTimeTerminRealizacjiGodzina.CustomFormat = "HH:mm";
            dateTimeTerminRealizacjiGodzina.ShowUpDown = true;

            txtKolor.ReadOnly = true;
            txtMaterial.ReadOnly = true;
            txtSapDetalu.ReadOnly = true;

            #endregion
        }
        #region - Wczytywanie danych z bazy danych do comboboxów i textboxów
        public void cmboFormaDetalSap()
        {
            try
            {

                DataSet dP = sqlQuery.GetDataFromSql("select * from Projekt");
                comboProjekt.DataSource = dP.Tables[0];
                comboProjekt.ValueMember = "projektNazwa";
                comboProjekt.SelectedIndex = -1;

                DataSet dMasz = sqlQuery.GetDataFromSql("select * from Maszyna");
                comboMaszyna.DataSource = dMasz.Tables[0];
                comboMaszyna.ValueMember = "maszynaNumer";
                comboMaszyna.SelectedIndex = -1;


                DataSet dCel = sqlQuery.GetDataFromSql("select * from Cel");
                comboCel.DataSource = dCel.Tables[0];
                comboCel.ValueMember = "celNazwa";
                comboCel.SelectedIndex = -1;

                DataSet dOdpow = sqlQuery.GetDataFromSql("select * from Uzytkownicy");
                comboOdpowiedzialny.DataSource = dOdpow.Tables[0];
                comboOdpowiedzialny.ValueMember = "nazwisko";
                comboOdpowiedzialny.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region - Button - dodawanie proby

        //czas nie moze byc wczesniejszy niz obecny
        //czas nie moze byc zerowy - ustawic czas na zerowy aby zabezpieczyc sie przed niedodaniem
        //nie moze byc pustych pól w odpowienich miejscach

        public void sprawdzenieCombosow()
        {
            if (!string.IsNullOrEmpty(comboProjekt.Text) && !string.IsNullOrEmpty(comboTrwanie.Text) && !string.IsNullOrEmpty(comboForma.Text) && !string.IsNullOrEmpty(comboMaszyna.Text) && !string.IsNullOrEmpty(comboDetal.Text) && !string.IsNullOrEmpty(comboCel.Text) && !string.IsNullOrEmpty(comboOdpowiedzialny.Text))
            {
                dodajProbeBtn.BackColor = System.Drawing.Color.Lime;
                dodajProbeBtn.Enabled = true;
            }
            else
            {
                dodajProbeBtn.Enabled = false;
                dodajProbeBtn.BackColor = System.Drawing.Color.DarkGray;
            }
        }
        private void dodajProbeBtn_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Czy chcesz dodać próbę?", "Potwierdź próbęe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Data.SqlClient.SqlConnection sqlConnection1 =
                new System.Data.SqlClient.SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4");
                //new System.Data.SqlClient.SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True");

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "INSERT into Proby (projektId,formaId,maszynaId,detalId, celId, godzStart, dzienStart, czasTrw, celRoz, statusProby, odpowiedzialny) select Projekt.projektId, Forma.formaId, Maszyna.maszynaId, Detal_komplet.detalId, Cel.celId, @godzStart ,convert(date, @dzienStart, 103), @Trwanie, @celRoz, 'Zaplanowana', @odpowiedzialny from Projekt, "
                    + "Forma,Maszyna,Detal_komplet,Cel where "
                    + " projektNazwa = @projectNazwa and formaNazwa = @formaNazwa and maszynaNumer = @maszynaNumer "
                    + " and detalNazwa = @detalNazwa and celNazwa = @celNazwa";


                cmd.Parameters.AddWithValue("@projectNazwa", comboProjekt.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@formaNazwa", comboForma.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@maszynaNumer", comboMaszyna.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@detalNazwa", comboDetal.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@celNazwa", comboCel.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@godzStart", dateTimeTerminRealizacjiGodzina.Value.ToShortTimeString());
                cmd.Parameters.AddWithValue("@dzienStart", SqlDbType.Date).Value = dateTimeTerminRealizacjiDzien.Value.Date;
                cmd.Parameters.AddWithValue("@celRoz", richTexCel.Text.ToString());
                cmd.Parameters.AddWithValue("@Trwanie", comboTrwanie.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@odpowiedzialny", comboOdpowiedzialny.SelectedValue.ToString());



                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                this.Close();

            }
            else
            {
                MessageBox.Show("Próba nie dodana");
                return;
            }
        }
        #endregion
        #region - Blokowanie i odblokowywanie comboboxów
        public void comboblocking()
        {

            comboForma.Enabled = false;
            comboDetal.Enabled = false;
            comboCel.Enabled = false;
            comboOdpowiedzialny.Enabled = false;
            dateTimeTerminRealizacjiDzien.Enabled = false;
            dateTimeTerminRealizacjiGodzina.Enabled = false;
        }
        public void comborealising()
        {

            comboForma.Enabled = true;
            comboDetal.Enabled = true;
            comboCel.Enabled = true;
            comboOdpowiedzialny.Enabled = true;
            dateTimeTerminRealizacjiDzien.Enabled = true;
            dateTimeTerminRealizacjiGodzina.Enabled = true;
        }
        public void comboczyszczenie()
        {

        }

        public void comboLogic()
        {
            if (comboProjekt.SelectedIndex == -1)
            {
                comboblocking();
            }
            else if (comboProjekt.SelectedIndex != -1)
            {
                comborealising();

            }
        }
        public void czyszcenieTextBoxow()
        {
            txtKolor.Text = string.Empty;
            txtMaterial.Text = string.Empty;
            txtSapDetalu.Text = string.Empty;
        }
        #endregion - logika
        #region - Zmiana comboboxów przy wyborze pierwszego comboboxa (projektu)

        private void comboProjekt_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboLogic();
            czyszcenieTextBoxow();
            sprawdzenieCombosow();
            comboMaszyna.SelectedIndex = -1;
            comboDetal.SelectedIndex = -1;
            comboCel.SelectedIndex = -1;



            DataSet dF = sqlQuery.GetDataFromSql("SELECT formaNazwa FROM Forma INNER JOIN Projekt ON Forma.FK_projektId = Projekt.projektId where projekt.projektNazwa = '" + comboProjekt.Text + "'");
            comboForma.DataSource = dF.Tables[0];
            comboForma.ValueMember = "formaNazwa";
            comboForma.SelectedIndex = -1;

        }
        #endregion
        #region - Button wyczysc comboboxy
        private void btnClear_Click(object sender, EventArgs e)
        {

            comboDetal.SelectedIndex = -1;
            comboProjekt.SelectedIndex = -1;
            comboForma.SelectedIndex = -1;
            comboCel.SelectedIndex = -1;
            comboMaszyna.SelectedIndex = -1;
            comboTrwanie.SelectedIndex = -1;
            comboOdpowiedzialny.SelectedIndex = -1;
            txtKolor.Text = "";
            txtMaterial.Text = "";
            txtSapDetalu.Text = "";
            richTexCel.Text = "";
            dateTimeTerminRealizacjiDzien.Value = new DateTime(2000, 01, 01);
            dateTimeTerminRealizacjiGodzina.Value = DateTimePicker.MinimumDateTime;
        }
        #endregion
        #region - Wczytanie detalu po zmianie Formy
        private void comboForma_SelectedIndexChanged(object sender, EventArgs e)
        {
            sprawdzenieCombosow();
            DataSet dDet = sqlQuery.GetDataFromSql("SELECT detalNazwa FROM Detal_komplet INNER JOIN Forma ON Detal_komplet.FK_formaId = Forma.formaId where forma.formaNazwa = '" + comboForma.Text + "'");
            comboDetal.DataSource = dDet.Tables[0];
            comboDetal.ValueMember = "detalNazwa";
            comboDetal.SelectedIndex = -1;
            comboMaszyna.SelectedIndex = -1;
            comboCel.SelectedIndex = -1;

        }
        #endregion
        #region - Uzupelnianie textboxow
        private void uzupelnianieTextboxow(object sender, EventArgs e)
        {
            DataSet dDet2 = sqlQuery.GetDataFromSql("select detalSap, detalMaterial, detalKolor from Detal_komplet where detalNazwa = '" + comboDetal.Text + "'");

            if (dDet2.Tables[0].Rows.Count > 0) {

                txtSapDetalu.Text = dDet2.Tables[0].Rows[0].ItemArray[0].ToString(); //
                txtMaterial.Text = dDet2.Tables[0].Rows[0].ItemArray[1].ToString();
                txtKolor.Text = dDet2.Tables[0].Rows[0].ItemArray[2].ToString();
            }
            else
            {
                return;
            }

        }
        #endregion
        private void zmianaWartosciFormy(object sender, EventArgs e)
        {
            czyszcenieTextBoxow();
        }
        #region - toolTip dla comboDetal
        public void toolTipDetal()
        {
            ToolTip tipDetal = new ToolTip();
            tipDetal.SetToolTip(this.comboDetal, comboDetal.Text);
        }

        private void tool(object sender, EventArgs e)
        {
            toolTipDetal();
        }
        #endregion
        #region - Limitowanie liczby znakow      
        private void textLimit(int limitZnakow, RichTextBox textBox)
        {
            if (textBox.Text.Length > limitZnakow)
            {
                textBox.ReadOnly = true;
                MessageBox.Show("Maksymalna ilosc znakow to 50");
            }
            else if (textBox.Text.Length <= limitZnakow)
            {
                textBox.ReadOnly = false;
            }
        }
        private void limitText(object sender, EventArgs e)
        {
            textLimit(100, richTexCel);
        }

        private void klawiszRich(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                richTexCel.ReadOnly = false;
            }
            else
            {
                textLimit(50, richTexCel);
            }
        }
        #endregion

        private void comboCel_SelectedIndexChanged(object sender, EventArgs e)
        {
            sprawdzenieCombosow();
        }

private void txtKolor_TextChanged(object sender, EventArgs e)
        {
            List<int> czasTrwanie = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            comboTrwanie.DataSource = czasTrwanie;
            comboTrwanie.SelectedIndex = -1;
        }

    }
}
