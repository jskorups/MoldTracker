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
                DataSet ds = sqlQuery.GetDataFromSql("select proj.projektNazwa as 'Nazwa projektu', form.formaNazwa as 'Forma', masz.maszynaNumer as 'Maszyna', det.detalNazwa as 'Detal',dzienStart as 'Dzień', godzStart as 'Godzina' from Projekt proj, Forma form, proby prob, Maszyna masz, Detal_komplet det where proj.projektId = prob.projektId and form.formaId = prob.formaId and masz.maszynaId = prob.maszynaId and prob.detalId = det.detalId; ");

                ostatnioDodaneGrid.DataSource = ds.Tables[0];



                //DataSet ds = sqlQuery.GetDataFromSql("select top 14 probaid,projektId,formaId,maszynaId,detalId from Proby;");

                //ostatnioDodaneGrid.DataSource = ds.Tables[0];
                //ostatnioDodaneGrid.Columns["probaId"].Width = 30;
                //ostatnioDodaneGrid.Columns["projektId"].Width = 40;
                //ostatnioDodaneGrid.Columns["formaId"].Width = 50;
                //ostatnioDodaneGrid.Columns["maszynaId"].Width = 30;
                //ostatnioDodaneGrid.Columns["detalId"].Width = 100;

                //SELECT Projekt, Forma, Detal_komplet
                //from Projekt
                //right join Proby
                //on Proby.projektId = Projekt.projektId;
                //right join provy
                //on prob

                // System.Data.SqlClient.SqlConnection sqlConnection1 =
                //// new System.Data.SqlClient.SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4");
                //  new System.Data.SqlClient.SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True");

                // System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                // cmd.CommandType = System.Data.CommandType.Text;
                // cmd.CommandText = "INSERT Proby (projektId,formaId,maszynaId,detalId, celId, godzStart, dzienStart, czasTrw, celRoz, statusProby) select Projekt.projektId, Forma.formaId, Maszyna.maszynaId, Detal_komplet.detalId, Cel.celId, @godzStart ,convert(date, @dzienStart, 103), @Trwanie, @celRoz, 'Zaplanowana' from Projekt, "
                //     + "Forma,Maszyna,Detal_komplet,Cel where "
                //     + " projektNazwa = @projectNazwa and formaNazwa = @formaNazwa and maszynaNumer = @maszynaNumer "
                //     + " and detalNazwa = @detalNazwa and celNazwa = @celNazwa ";




                //cmd.Parameters.AddWithValue("@projectNazwa", comboProjekt.SelectedValue.ToString());
                //cmd.Parameters.AddWithValue("@formaNazwa", comboForma.SelectedValue.ToString());
                //cmd.Parameters.AddWithValue("@maszynaNumer", comboMaszyna.SelectedValue.ToString());
                //cmd.Parameters.AddWithValue("@detalNazwa", comboDetal.SelectedValue.ToString());
                //cmd.Parameters.AddWithValue("@celNazwa", comboCel.SelectedValue.ToString());
                //cmd.Parameters.AddWithValue("@godzStart", dateTimeTerminRealizacjiGodzina.Value.ToShortTimeString());
                //cmd.Parameters.AddWithValue("@dzienStart", SqlDbType.Date).Value = dateTimeTerminRealizacjiDzien.Value.Date;
                //cmd.Parameters.AddWithValue("@celRoz", richTexCel.Text.ToString());
                //cmd.Parameters.AddWithValue("@Trwanie", comboTrwanie.SelectedValue.ToString());

                //cmd.Connection = sqlConnection1;
                //sqlConnection1.Open();
                //cmd.ExecuteNonQuery();
                //sqlConnection1.Close();
                //this.Close();


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
