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
using System.Configuration;
using System.Data.Sql;




namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class Statistics : Form
    {
        private static Statistics _instance;

        List<string> wybraneProjekty = new List<string>();
        List<string> wybraneMaszyny = new List<string>();
        List<string> wybraneDetale = new List<string>();
        List<string> wybraneDetaleDlaProjektów = new List<string>();
        List<string> wybraneDetaleDlaProjektówCzas = new List<string>();
        List<string> wybraneFormyDlaProjektówCzas = new List<string>();
        List<string> wybraneDetaleDleFormCzas = new List<string>();


        public Statistics()
        {
            InitializeComponent();
            wczytajProjekty();
            wczytajMaszyny();
            wczytajDetaleProjektyFormy();
            wczytajProjektyCzas();
            wczytajFormyCzas();
            wczytajDetaleCzas();
            //wczytajDetaleDlaProjektu();
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
            using (var connection = //new SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True"))
            new SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4"))
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
            //new SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4"))
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
            if (string.IsNullOrEmpty(comboProjektDetaleWszystkie.Text))
            {
                DataSet dD = sqlQuery.GetDataFromSql("select * from Detal_komplet");
                listBoxDetaleWszystkie.DataSource = dD.Tables[0];
                listBoxDetaleWszystkie.DisplayMember = "detalNazwa";
                comboFormaDetaleWszystkie.Enabled = false;
            }

            else
            {
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

            if (string.IsNullOrEmpty(comboFormaDetaleWszystkie.Text))
            {
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
        private void detaleAllChart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxDetaleWszystkie.SelectedItems.Count; i++)
            {
                wybraneDetale.Add(listBoxDetaleWszystkie.GetItemText(listBoxDetaleWszystkie.SelectedItems[i]));
            }
            using (var connection = new SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True"))
            //new SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4"))
            {
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;
                var sql = "select det.detalnazwa as 'Detal', COUNT(prob.detalId) as 'Liczba prob' from Proby prob LEFT JOIN Detal_komplet det ON det.detalId = prob.detalId where det.detalNazwa in ({0}) and dzienStart between '" + dateTimePickerDetailAllOd.Value.Date + "' and '" + dateTimePickerDetailAllDo.Value.Date + "' group by det.detalNazwa;";

                DataSet dP = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", wybraneDetale.Select(x => $"\'{x}\'"))));
                DataView source = new DataView(dP.Tables[0]);
                chartDetaleWszystkie.DataSource = source;
                chartDetaleWszystkie.Series[0].XValueMember = "Detal";
                chartDetaleWszystkie.Series[0].YValueMembers = "Liczba prob";
                chartDetaleWszystkie.ChartAreas[0].AxisX.Interval = 1;
                chartDetaleWszystkie.ChartAreas[0].AxisY.Interval = 1;
                chartDetaleWszystkie.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chartDetaleWszystkie.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                chartDetaleWszystkie.DataBind();
                chartDetaleWszystkie.Update();
                wybraneDetale.Clear();
                connection.Close();
            }
        }
        #endregion
        #region Czas dla projektów

        public void wczytajProjektyCzas()
        {
            try
            {
                DataSet dP = sqlQuery.GetDataFromSql("select * from Projekt");
                listBoxTimeProjects.DataSource = dP.Tables[0];
                listBoxTimeProjects.DisplayMember = "projektNazwa";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void timeProjectsCheckAllProjects(object sender, EventArgs e)
        {
            if (checkBoxAllProjectsTime.Checked == true)
            {
                for (int i = 0; i < listBoxTimeProjects.Items.Count; i++)
                    listBoxTimeProjects.SetSelected(i, true);
            }
            else if (checkBoxAllProjectsTime.Checked == false)
            {
                for (int i = 0; i < listBoxTimeProjects.Items.Count; i++)
                    listBoxTimeProjects.SetSelected(i, false);
            }
        }

        private void buttonChartTimeAllProjects_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxTimeProjects.SelectedItems.Count; i++)
            {
                wybraneDetaleDlaProjektówCzas.Add(listBoxTimeProjects.GetItemText(listBoxTimeProjects.SelectedItems[i]));
            }
            using (var connection = new SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True"))
            //new SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4"))
            {
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;
                var sql = "select proj.projektNazwa as 'Projekt' , SUM(czasTrw)as 'Czas' from Proby prob left join Projekt proj on prob.projektId = proj.projektId where proj.projektNazwa in ({0}) and dzienStart between '" + dateTimePickerTimeProjectsOd.Value.Date + "' and '" + dateTimePickerTimeProjectsDo.Value.Date + "' group by proj.projektNazwa;";

                DataSet dP = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", wybraneDetaleDlaProjektówCzas.Select(x => $"\'{x}\'"))));
                DataView source = new DataView(dP.Tables[0]);
                chartTimeAllProjects.DataSource = source;
                chartTimeAllProjects.Series[0].XValueMember = "Projekt";
                chartTimeAllProjects.Series[0].YValueMembers = "Czas";
                chartTimeAllProjects.ChartAreas[0].AxisX.Interval = 1;
                chartTimeAllProjects.ChartAreas[0].AxisY.Interval = 5;
                chartTimeAllProjects.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chartTimeAllProjects.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                chartTimeAllProjects.DataBind();
                chartTimeAllProjects.Update();
                wybraneDetaleDlaProjektówCzas.Clear();
                connection.Close();
            }
        }

        #endregion
        #region Czas dla form

        public void wczytajFormyCzas()
        {
            try
            {
                DataSet dF = sqlQuery.GetDataFromSql("select * from Forma");
                listBoxTimeMolds.DataSource = dF.Tables[0];
                listBoxTimeMolds.DisplayMember = "formaNazwa";

                DataSet dP = sqlQuery.GetDataFromSql("select * from Projekt");
                comboBoxTimeMolds.DataSource = dP.Tables[0];
                comboBoxTimeMolds.ValueMember = "projektNazwa";
                comboBoxTimeMolds.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void timeMoldsCheckAllProjects(object sender, EventArgs e)
        {
            if (checkBoxAllMoldsTime.Checked == true)
            {
                for (int i = 0; i < listBoxTimeMolds.Items.Count; i++)
                    listBoxTimeMolds.SetSelected(i, true);
            }
            else if (checkBoxAllMoldsTime.Checked == false)
            {
                for (int i = 0; i < listBoxTimeMolds.Items.Count; i++)
                    listBoxTimeMolds.SetSelected(i, false);
            }
        }
        private void showMoldsProjects(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxTimeMolds.Text))
            {
                DataSet dD = sqlQuery.GetDataFromSql("select * from Forma");
                listBoxTimeMolds.DataSource = dD.Tables[0];
                listBoxTimeMolds.DisplayMember = "formaNazwa";
            }
            else
            {
                DataSet dD2 = sqlQuery.GetDataFromSql("select formaNazwa from Forma where FK_projektId = (select projektId from Projekt where projektNazwa = '" + comboBoxTimeMolds.Text + "')");
                listBoxTimeMolds.DataSource = dD2.Tables[0];
                listBoxTimeMolds.DisplayMember = "detalNazwa";
            }
        }
        private void buttonChartTimeAllMold_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxTimeMolds.SelectedItems.Count; i++)
            {
                wybraneFormyDlaProjektówCzas.Add(listBoxTimeMolds.GetItemText(listBoxTimeMolds.SelectedItems[i]));
            }
            using (var connection = new SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True"))
            //new SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4"))
            {
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;
                var sql = "select form.formaNazwa as 'Forma' , SUM(czasTrw)as 'Czas' from Proby prob left join Forma form on prob.formaId = form.formaId where form.formaNazwa in ({0}) and dzienStart between '" + dateTimePickerMoldOd.Value.Date + "' and '" + dateTimePickerMoldDo.Value.Date + "' group by form.formaNazwa;";

                DataSet dP = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", wybraneFormyDlaProjektówCzas.Select(x => $"\'{x}\'"))));
                DataView source = new DataView(dP.Tables[0]);
                chartTimeMolds.DataSource = source;
                chartTimeMolds.Series[0].XValueMember = "Forma";
                chartTimeMolds.Series[0].YValueMembers = "Czas";
                chartTimeMolds.ChartAreas[0].AxisX.Interval = 1;
                if (string.IsNullOrEmpty(comboBoxTimeMolds.Text))
                {
                    chartTimeMolds.ChartAreas[0].AxisX.Title = "Formy";
                }
                else
                {
                    chartTimeMolds.ChartAreas[0].AxisX.Title = "Formy dla projektu " + comboBoxTimeMolds.Text + "";
                }
                chartTimeMolds.ChartAreas[0].AxisY.Interval = 5;
                chartTimeMolds.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chartTimeMolds.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                chartTimeMolds.DataBind();
                chartTimeMolds.Update();
                wybraneFormyDlaProjektówCzas.Clear();
                connection.Close();
            }
        }
        #endregion
        #region Czas dla detali

     
        public void wczytajDetaleCzas()
        {
            try
            {
                DataSet dP = sqlQuery.GetDataFromSql("select * from Projekt");
                comboBoxCzasDetaleDlaProjektu.DataSource = dP.Tables[0];
                comboBoxCzasDetaleDlaProjektu.ValueMember = "projektNazwa";
                comboBoxCzasDetaleDlaProjektu.SelectedIndex = -1;

                DataSet dD = sqlQuery.GetDataFromSql("select * from Detal_komplet");
                listBoxDetaleCzas.DataSource = dD.Tables[0];
                listBoxDetaleCzas.DisplayMember = "detalNazwa";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // wybieranie wszytskich detali
        private void checkAllDetailsTime(object sender, EventArgs e)
        {
            if (checkBoxDetailsTime.Checked == true)
            {
                for (int i = 0; i < listBoxDetaleCzas.Items.Count; i++)
                    listBoxDetaleCzas.SetSelected(i, true);
            }
            else if (checkBoxDetailsTime.Checked == false)
            {
                for (int i = 0; i < listBoxDetaleCzas.Items.Count; i++)
                    listBoxDetaleCzas.SetSelected(i, false);
            }
        }

        ////zawezanie detali dla projektu
        private void showDetailsTimeProject(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxCzasDetaleDlaProjektu.Text))
            {
                DataSet dD = sqlQuery.GetDataFromSql("select * from Detal_komplet");
                listBoxDetaleCzas.DataSource = dD.Tables[0];
                listBoxDetaleCzas.DisplayMember = "detalNazwa";
                comboBoxCzasDetaleDlaFormy.Enabled = false;
            }

            else
            {
                // ładowanie dla projektu
                comboBoxCzasDetaleDlaFormy.Enabled = true;
                DataSet dD2 = sqlQuery.GetDataFromSql("select detalNazwa from Detal_komplet where Forma in(select formaNazwa from Forma where FK_projektId = (select projektId from Projekt where projektNazwa = '" + comboBoxCzasDetaleDlaProjektu.Text + "'))");
                listBoxDetaleCzas.DataSource = dD2.Tables[0];
                listBoxDetaleCzas.DisplayMember = "detalNazwa";

                //ładowanie form dla wybranego projektu
                DataSet dF = sqlQuery.GetDataFromSql("SELECT formaNazwa FROM Forma INNER JOIN Projekt ON Forma.FK_projektId = Projekt.projektId where projekt.projektNazwa = '" + comboBoxCzasDetaleDlaProjektu.Text + "'");
                comboBoxCzasDetaleDlaFormy.DataSource = dF.Tables[0];
                comboBoxCzasDetaleDlaFormy.ValueMember = "formaNazwa";
                comboBoxCzasDetaleDlaFormy.SelectedIndex = -1;
            }
        }
        private void showDetailsTimeMolds(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(comboBoxCzasDetaleDlaFormy.Text))
            {

                DataSet dD = sqlQuery.GetDataFromSql("select detalNazwa from Detal_komplet where Forma in(select formaNazwa from Forma where FK_projektId = (select projektId from Projekt where projektNazwa = '" + comboBoxCzasDetaleDlaProjektu.Text + "'))");
                listBoxDetaleCzas.DataSource = dD.Tables[0];
                listBoxDetaleCzas.DisplayMember = "detalNazwa";
            }
            else
            {
                DataSet dD = sqlQuery.GetDataFromSql("select detalNazwa from Detal_komplet where Forma =  '" + comboBoxCzasDetaleDlaFormy.Text + "'");
                listBoxDetaleCzas.DataSource = dD.Tables[0];
                listBoxDetaleCzas.DisplayMember = "detalNazwa";
            }
        }
        private void detaleTimeChart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxDetaleCzas.SelectedItems.Count; i++)
            {
                wybraneDetaleDleFormCzas.Add(listBoxDetaleCzas.GetItemText(listBoxDetaleCzas.SelectedItems[i]));
            }
            using (var connection = new SqlConnection("Data Source=DESKTOP-7CV4P8D\\KUBALAP;Initial Catalog=MoldTracker;Integrated Security=True"))
            //new SqlConnection("Data Source=SLSVMDB01;Initial Catalog=MoldTracker;User Id=MoldTracker;Password=P1r4m1d4"))
            {
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;
                var sql = "select det.detalNazwa as 'Detal' , SUM(czasTrw)as 'Czas' from Proby prob left join Detal_komplet det on prob.detalId = det.detalId where det.detalNazwa in ({0}) and dzienStart between '" + detaleCzacOd.Value.Date + "' and '" + detaleCzacDo.Value.Date + "' group by det.detalNazwa;";

                DataSet dP = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", wybraneDetaleDleFormCzas.Select(x => $"\'{x}\'"))));
                DataView source = new DataView(dP.Tables[0]);
                chartCzasDetale.DataSource = source;
                chartCzasDetale.Series[0].XValueMember = "Detal";
                chartCzasDetale.Series[0].YValueMembers = "Czas";
                chartCzasDetale.ChartAreas[0].AxisX.Interval = 1;
                chartCzasDetale.ChartAreas[0].AxisY.Interval = 5;
                chartCzasDetale.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chartCzasDetale.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                chartCzasDetale.DataBind();
                chartCzasDetale.Update();
                wybraneDetaleDleFormCzas.Clear();
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


