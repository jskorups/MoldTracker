using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;



namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Statistics : Form
    {
        private static Statistics _instance;

        List<string> wybraneProjekty = new List<string>();
        List<string> wybraneMaszyny = new List<string>();
        List<string> wybraneDetale = new List<string>();


        public Statistics()
        {
            InitializeComponent();
            wczytajProjekty();
            wczytajMaszyny();
            wczytajDetaleProjektyFormy();
            wczytajDetaleDlaProjektu();
            listBox1.SelectedIndex = -1;
        }

        public Statistics instance
        {
            get
            {
                if (Statistics._instance == null)
                {
                    Statistics._instance = new Statistics();
                }
                return Statistics._instance;
            }
        }


        #region Tab Projekty
        public void wczytajProjekty()
        {
            try
            {
                DataSet dP = sqlQuery.GetDataFromSql("select * from Projekt");
                listBox1.DataSource = dP.Tables[0];
                listBox1.DisplayMember = "projektNazwa";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void checkAllProjects(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                    listBox1.SetSelected(i, true);
            }
            else if (checkBox1.Checked == false)
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                    listBox1.SetSelected(i, false);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                wybraneProjekty.Add(listBox1.GetItemText(listBox1.SelectedItems[i]));
            }
            using (var connection = new SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True"))
            {
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;
                var sql = "select proj.projektNazwa as 'Projekt', COUNT(prob.projektId) as 'Liczba prob' from Proby prob LEFT JOIN Projekt proj ON proj.projektId = prob.projektId where proj.projektNazwa in ({0}) and dzienStart between '" + dateTimePickerStatisticsOd.Value.Date + "' and '" + dateTimePickerStatisticsDo.Value.Date + "' group by proj.projektNazwa;";

                DataSet dP = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", wybraneProjekty.Select(x => $"\'{x}\'"))));
                DataView source = new DataView(dP.Tables[0]);
                chart6.DataSource = source;
                chart6.Series[0].XValueMember = "Projekt";
                chart6.Series[0].YValueMembers = "Liczba prob";
                chart6.ChartAreas[0].AxisX.Interval = 1;
                chart6.ChartAreas[0].AxisY.Interval = 1;
                chart6.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chart6.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                chart6.DataBind();
                chart6.Update();
                wybraneProjekty.Clear();
                connection.Close();
            }
        }
        #endregion
        #region Tab Maszyny
        public void wczytajMaszyny()
        {
            try
            {
                DataSet dP = sqlQuery.GetDataFromSql("select * from Maszyna");
                listBoxMaszynyAll.DataSource = dP.Tables[0];
                listBoxMaszynyAll.DisplayMember = "maszynaNumer";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void checkAllMachines(object sender, EventArgs e)
        {
            if (checkBoxAllMaszyny.Checked == true)
            {
                for (int i = 0; i < listBoxMaszynyAll.Items.Count; i++)
                    listBoxMaszynyAll.SetSelected(i, true);
            }
            else if (checkBoxAllMaszyny.Checked == false)
            {
                for (int i = 0; i < listBoxMaszynyAll.Items.Count; i++)
                    listBoxMaszynyAll.SetSelected(i, false);
            }
        }
        private void maszynyChartButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxMaszynyAll.SelectedItems.Count; i++)
            {
                wybraneMaszyny.Add(listBoxMaszynyAll.GetItemText(listBoxMaszynyAll.SelectedItems[i]));
            }
            using (var connection = new SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True"))
            {
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;
                var sql = "select masz.maszynaNumer as 'Maszyna', COUNT(prob.maszynaId) as 'Liczba prob' from Proby prob LEFT JOIN Maszyna masz ON masz.maszynaId = prob.maszynaId where masz.maszynaNumer in ({0}) and dzienStart between '" + dateTimePickerMachinesOd.Value.Date + "' and '" + dateTimePickerMachinesDo.Value.Date + "' group by masz.maszynaNumer;";

                DataSet dP = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", wybraneMaszyny.Select(x => $"\'{x}\'"))));
                DataView source = new DataView(dP.Tables[0]);
                chartMaszyny.DataSource = source;
                chartMaszyny.Series[0].XValueMember = "Maszyna";
                chartMaszyny.Series[0].YValueMembers = "Liczba prob";
                chartMaszyny.ChartAreas[0].AxisX.Interval = 1;
                chartMaszyny.ChartAreas[0].AxisY.Interval = 1;
                chartMaszyny.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chartMaszyny.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                chartMaszyny.DataBind();
                chartMaszyny.Update();
                wybraneMaszyny.Clear();
                connection.Close();
            }
        }
        #endregion
        #region Detale / Wszystkie

        public void wczytajDetaleProjektyFormy()
        {
            try
            {
                DataSet dP = sqlQuery.GetDataFromSql("select * from Projekt");
                comboProjektDetaleWszystkie.DataSource = dP.Tables[0];
                comboProjektDetaleWszystkie.ValueMember = "projektNazwa";
                comboProjektDetaleWszystkie.SelectedIndex = -1;

                DataSet dD = sqlQuery.GetDataFromSql("select * from Detal_komplet");
                listBoxDetaleWszystkie.DataSource = dD.Tables[0];
                listBoxDetaleWszystkie.DisplayMember = "detalNazwa";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // wybieranie wszytskich detali
        private void checkAllDetails(object sender, EventArgs e)
        {
            if (checkBoxDetaleWszystkie.Checked == true)
            {
                for (int i = 0; i < listBoxDetaleWszystkie.Items.Count; i++)
                    listBoxDetaleWszystkie.SetSelected(i, true);
            }
            else if (checkBoxDetaleWszystkie.Checked == false)
            {
                for (int i = 0; i < listBoxDetaleWszystkie.Items.Count; i++)
                    listBoxDetaleWszystkie.SetSelected(i, false);
            }
        }

        ////zawezanie detali dla projektu
        private void showDetailsProject(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboProjektDetaleWszystkie.Text)) {
                DataSet dD = sqlQuery.GetDataFromSql("select * from Detal_komplet");
                listBoxDetaleWszystkie.DataSource = dD.Tables[0];
                listBoxDetaleWszystkie.DisplayMember = "detalNazwa";
                comboFormaDetaleWszystkie.Enabled = false;
            }

            else {
                // ładowanie dla projektu
                comboFormaDetaleWszystkie.Enabled = true;
                DataSet dD2 = sqlQuery.GetDataFromSql("select detalNazwa from Detal_komplet where Forma in(select formaNazwa from Forma where FK_projektId = (select projektId from Projekt where projektNazwa = '" + comboProjektDetaleWszystkie.Text + "'))");
                listBoxDetaleWszystkie.DataSource = dD2.Tables[0];
                listBoxDetaleWszystkie.DisplayMember = "detalNazwa";

                //ładowanie form dla wybranego projektu
                DataSet dF = sqlQuery.GetDataFromSql("SELECT formaNazwa FROM Forma INNER JOIN Projekt ON Forma.FK_projektId = Projekt.projektId where projekt.projektNazwa = '" + comboProjektDetaleWszystkie.Text + "'");
                comboFormaDetaleWszystkie.DataSource = dF.Tables[0];
                comboFormaDetaleWszystkie.ValueMember = "formaNazwa";
                comboFormaDetaleWszystkie.SelectedIndex = -1;
            }
        }
        private void showMoldProject(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(comboFormaDetaleWszystkie.Text)){

                DataSet dD = sqlQuery.GetDataFromSql("select detalNazwa from Detal_komplet where Forma in(select formaNazwa from Forma where FK_projektId = (select projektId from Projekt where projektNazwa = '" + comboProjektDetaleWszystkie.Text + "'))");
                listBoxDetaleWszystkie.DataSource = dD.Tables[0];
                listBoxDetaleWszystkie.DisplayMember = "detalNazwa";
            }
            else
            {
                DataSet dD = sqlQuery.GetDataFromSql("select detalNazwa from Detal_komplet where Forma =  '" + comboFormaDetaleWszystkie.Text + "'");
                listBoxDetaleWszystkie.DataSource = dD.Tables[0];
                listBoxDetaleWszystkie.DisplayMember = "detalNazwa";
            }
        }


        private void detaleWszytskieChartButton(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxDetaleWszystkie.SelectedItems.Count; i++)
            {
                wybraneDetale.Add(listBoxDetaleWszystkie.GetItemText(listBoxDetaleWszystkie.SelectedItems[i]));
            }
            using (var connection = new SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True"))
            {
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;
                var sql = "select det.detalnazwa as 'Detal', COUNT(prob.detalId) as 'Liczba prob' from Proby prob LEFT JOIN Detal_koplet det masz ON det.detalId = prob.detalId where det.detalNazwa in ({0}) and dzienStart between '" + dateTimePickerMachinesOd.Value.Date + "' and '" + dateTimePickerMachinesDo.Value.Date + "' group by det.detalNazwa;";

                DataSet dP = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", wybraneMaszyny.Select(x => $"\'{x}\'"))));
                DataView source = new DataView(dP.Tables[0]);
                chartMaszyny.DataSource = source;
                chartMaszyny.Series[0].XValueMember = "Detal";
                chartMaszyny.Series[0].YValueMembers = "Liczba prob";
                chartMaszyny.ChartAreas[0].AxisX.Interval = 1;
                chartMaszyny.ChartAreas[0].AxisY.Interval = 1;
                chartMaszyny.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chartMaszyny.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                chartMaszyny.DataBind();
                chartMaszyny.Update();
                wybraneMaszyny.Clear();
                connection.Close();
            }
        }



        #endregion
        #region Detale / dla Projektów/Projektu

        public void wczytajDetaleDlaProjektu()
        {
            try
            {
                DataSet dP = sqlQuery.GetDataFromSql("select * from Projekt");
                listBoxDetaleWszystkieProjekt.DataSource = dP.Tables[0];
                listBoxDetaleWszystkieProjekt.DisplayMember = "projektNazwa";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void checkAllDetailsProjekty(object sender, EventArgs e)
        {
            if (checkBoxDetaleWszystkieProjekty.Checked == true)
            {
                for (int i = 0; i < listBoxDetaleWszystkieProjekt.Items.Count; i++)
                    listBoxDetaleWszystkieProjekt.SetSelected(i, true);
            }
            else if (checkBoxDetaleWszystkieProjekty.Checked == false)
            {
                for (int i = 0; i < listBoxDetaleWszystkieProjekt.Items.Count; i++)
                    listBoxDetaleWszystkieProjekt.SetSelected(i, false);
            }
        }


 
        private void detaleWszytskieProjektyChartButton(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxDetaleWszystkieProjekt.SelectedItems.Count; i++)
            {
                wybraneMaszyny.Add(listBoxDetaleWszystkieProjekt.GetItemText(listBoxDetaleWszystkieProjekt.SelectedItems[i]));
            }
            using (var connection = new SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True"))
            {
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;
                var sql = "select det.detalnazwa as 'Detal', COUNT(prob.detalId) as 'Liczba prob' from Proby prob LEFT JOIN Detal_koplet det masz ON det.detalId = prob.detalId where det.detalNazwa in ({0}) and dzienStart between '" + dateTimePickerMachinesOd.Value.Date + "' and '" + dateTimePickerMachinesDo.Value.Date + "' group by det.detalNazwa;";

                DataSet dP = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", wybraneMaszyny.Select(x => $"\'{x}\'"))));
                DataView source = new DataView(dP.Tables[0]);
                chartMaszyny.DataSource = source;
                chartMaszyny.Series[0].XValueMember = "Detal";
                chartMaszyny.Series[0].YValueMembers = "Liczba prob";
                chartMaszyny.ChartAreas[0].AxisX.Interval = 1;
                chartMaszyny.ChartAreas[0].AxisY.Interval = 1;
                chartMaszyny.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chartMaszyny.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                chartMaszyny.DataBind();
                chartMaszyny.Update();
                wybraneMaszyny.Clear();
                connection.Close();
            }
        }
        #endregion

        private void statisticsClosed(object sender, FormClosedEventArgs e)
        {
            Statistics._instance = null;
        }
    }
}

