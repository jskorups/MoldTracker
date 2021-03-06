﻿using System;
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
//ee



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
        List<string> wybraneInzynierCzas = new List<string>();
        //listy - cel
        List<string> wybraneCelProjekty = new List<string>();
        List<string> wybraneCelFormy = new List<string>();
        List<string> wybraneCelDetale = new List<string>();



        public Statistics()
        {
            InitializeComponent();
            wczytajProjekty();
            wczytajMaszyny();
            wczytajDetaleProjektyFormy();
            wczytajProjektyCzas();
            wczytajFormyCzas();
            wczytajDetaleCzas();
            wczytajInynierowCzas();

            wczytajProjektyCel();

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

        private void statisticsClosed(object sender, FormClosedEventArgs e)
        {
            Statistics._instance = null;
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

            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionStrin))
            {
              

                if (wybraneProjekty.Count > 0)
                {

                    connection.Open();
                    var sqlCommand = new SqlCommand();
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    var sql = "select proj.projektNazwa as 'Projekt', COUNT(prob.projektId) as 'Liczba prob' from Proby prob LEFT JOIN Projekt proj ON proj.projektId = prob.projektId where proj.projektNazwa in ({0}) and dzienStart between '" + dateTimePickerStatisticsOd.Value.ToDateTimeString() + "' and '" + dateTimePickerStatisticsDo.Value.ToDateTimeString() + "' and statusProby = 'Zakonczona'  group by proj.projektNazwa;";

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
                else if (wybraneProjekty.Count <= 0)
                {
                    MessageBox.Show("Nie wybrałeś żadnego projektu!");
                }
                
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

            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionStrin))

                if (wybraneMaszyny.Count  > 0)
                {
                    connection.Open();
                    var sqlCommand = new SqlCommand();
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    var sql = "select masz.maszynaNumer as 'Maszyna', COUNT(prob.maszynaId) as 'Liczba prob' from Proby prob LEFT JOIN Maszyna masz ON masz.maszynaId = prob.maszynaId where masz.maszynaNumer in ({0}) and dzienStart between '" + dateTimePickerMachinesOd.Value.ToDateTimeString() + "' and '" + dateTimePickerMachinesDo.Value.ToDateTimeString() + "' and statusProby = 'Zakonczona' group by masz.maszynaNumer;";

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
            else if (wybraneMaszyny.Count <= 0)
                {
                    MessageBox.Show("Nie wybrałeś żadnej maszyny!");
                }

            {
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

            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionStrin))
                if (wybraneDetale.Count > 0)
                {
                    connection.Open();
                    var sqlCommand = new SqlCommand();
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    var sql = "select det.detalnazwa as 'Detal', COUNT(prob.detalId) as 'Liczba prob' from Proby prob LEFT JOIN Detal_komplet det ON det.detalId = prob.detalId where det.detalNazwa in ({0}) and dzienStart between '" + dateTimePickerDetailAllOd.Value.ToDateTimeString() + "' and '" + dateTimePickerDetailAllDo.Value.ToDateTimeString() + "' and statusProby = 'Zakonczona' group by det.detalNazwa;";

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
                else if (wybraneDetale.Count <= 0)
                {
                    MessageBox.Show("Nie wybrałeś detali");
                }
            {
               
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
            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionStrin))
            {
                if (wybraneDetaleDlaProjektówCzas.Count > 0)
                {
                    connection.Open();
                    var sqlCommand = new SqlCommand();
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    var sql = "select proj.projektNazwa as 'Projekt' , sum(((DATEPART(hour, czasTrwania) * 3600) + (DATEPART(minute, czasTrwania) * 60) + DATEPART(second, czasTrwania))/3600) as 'Czas' from Proby prob left join Projekt proj on prob.projektId = proj.projektId where proj.projektNazwa in ({0}) and dzienStart between '" + dateTimePickerTimeProjectsOd.Value.ToDateTimeString()+ "' and '" + dateTimePickerTimeProjectsDo.Value.ToDateTimeString() + "' and statusProby = 'Zakonczona' group by proj.projektNazwa;";

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
                else if (wybraneDetaleDlaProjektówCzas.Count <= 0)
                {
                    MessageBox.Show(dateTimePickerStatisticsDo.Value.ToDateTimeString());
                }
                
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
        private void buttonChartTimeAllMolds_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxTimeMolds.SelectedItems.Count; i++)
            {
                wybraneFormyDlaProjektówCzas.Add(listBoxTimeMolds.GetItemText(listBoxTimeMolds.SelectedItems[i]));
            }
            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionStrin))
            {
                if (wybraneFormyDlaProjektówCzas.Count > 0)
                {
                    connection.Open();
                    var sqlCommand = new SqlCommand();
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    var sql = "select form.formaNazwa as 'Forma' , sum(((DATEPART(hour, czasTrwania) * 3600) + (DATEPART(minute, czasTrwania) * 60) + DATEPART(second, czasTrwania))/3600) as 'Czas' from Proby prob left join Forma form on prob.formaId = form.formaId where form.formaNazwa in ({0}) and dzienStart between '" + dateTimePickerMoldOd.Value.ToDateTimeString() + "' and '" + dateTimePickerMoldDo.Value.ToDateTimeString() + "' and prob.statusProby = 'Zakonczona' group by form.formaNazwa;";

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
                else if (wybraneFormyDlaProjektówCzas.Count <= 0)
                {
                    MessageBox.Show("Nie wybrałes żadnej formy!");
                }
                

                
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

            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionStrin))
            {
                if (wybraneDetaleDleFormCzas.Count > 0)
                {
                    connection.Open();
                    var sqlCommand = new SqlCommand();
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    var sql = "select det.detalNazwa as 'Detal' , sum(((DATEPART(hour, czasTrwania) * 3600) + (DATEPART(minute, czasTrwania) * 60) + DATEPART(second, czasTrwania))/3600) as 'Czas' from Proby prob left join Detal_komplet det on prob.detalId = det.detalId where det.detalNazwa in ({0}) and dzienStart between '" + detaleCzacOd.Value.ToDateTimeString() + "' and '" + detaleCzacDo.Value.ToDateTimeString() + "' and prob.statusProby = 'Zakonczona' group by det.detalNazwa;";
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
                else if (wybraneDetaleDleFormCzas.Count <= 0)
                {
                    MessageBox.Show("Nie wybrałes detali!");
                }
               
            }
        }
        #endregion
        #region Czas dla inżynierów

        public void wczytajInynierowCzas()
        {
            try
            {
                DataSet dI = sqlQuery.GetDataFromSql("select nazwisko from Uzytkownicy;");
                listBoxInzynierCzas.DataSource = dI.Tables[0];
                listBoxInzynierCzas.DisplayMember = "nazwisko";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void checkAllEnginners(object sender, EventArgs e)
        {
            if (checkBox26.Checked == true)
            {
                for (int i = 0; i < listBoxInzynierCzas.Items.Count; i++)
                    listBoxInzynierCzas.SetSelected(i, true);
            }
            else if (checkBox26.Checked == false)
            {
                for (int i = 0; i < listBoxInzynierCzas.Items.Count; i++)
                    listBoxInzynierCzas.SetSelected(i, false);
            }
        }
        //tutaj 
        private void inzynierowieChartButton_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < listBoxInzynierCzas.SelectedItems.Count; i++)
            {
                wybraneInzynierCzas.Add(listBoxInzynierCzas.GetItemText(listBoxInzynierCzas.SelectedItems[i]));
            }

            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;

            using (var connection = new SqlConnection(connectionStrin))
            {
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;

                var sql = "SELECT odpowiedzialny, sum(((DATEPART(hour, czasTrwania) * 3600) + (DATEPART(minute, czasTrwania) * 60) + DATEPART(second, czasTrwania))/3600) as Czas FROM Proby where odpowiedzialny in ({0}) and dzienStart between '" + detaleCzasInzynierOd.Value.ToDateTimeString() + "' and '" + detaleCzasInzynierDo.Value.ToDateTimeString() + "' and statusProby='Zakonczona' group by odpowiedzialny;";

                DataSet dI2 = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", wybraneInzynierCzas.Select(x => $"\'{x}\'"))));
                DataView source = new DataView(dI2.Tables[0]);

                chartCzasInzynier.DataSource = source;
                chartCzasInzynier.Series[0].XValueMember = "odpowiedzialny";
                chartCzasInzynier.Series[0].YValueMembers = "Czas";
                chartCzasInzynier.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chartCzasInzynier.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                chartCzasInzynier.DataBind();
                chartCzasInzynier.Update();
                wybraneInzynierCzas.Clear();
                connection.Close();
            }
        }
        #endregion

        #region Cel dla projektów

        public void wczytajProjektyCel()
        {
            try
            {
                DataSet dF = sqlQuery.GetDataFromSql("select * from Projekt");
                listBoxTargetProjects.DataSource = dF.Tables[0];
                listBoxTargetProjects.DisplayMember = "projektNazwa";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void checkBoxTargetProjects_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTargetProjects.Checked == true)
            {
                for (int i = 0; i < listBoxTargetProjects.Items.Count; i++)
                    listBoxTargetProjects.SetSelected(i, true);
            }
            else if (checkBoxTargetProjects.Checked == false)
            {
                for (int i = 0; i < listBoxTargetProjects.Items.Count; i++)
                    listBoxTargetProjects.SetSelected(i, false);
            }
        }
        private void buttonChartTargetAllProjects_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxTargetProjects.SelectedItems.Count; i++)
            {
                wybraneCelProjekty.Add(listBoxTargetProjects.GetItemText(listBoxTargetProjects.SelectedItems[i]));
            }
            string connectionStrin = ConfigurationManager.ConnectionStrings["MoldTracker.Properties.Settings.ConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionStrin))
            {
                connection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.Text;
                var sql = "select count(Proby.celId) as 'Suma celów', (Cel.celNazwa) as 'Cel Id' from Proby LEFT JOIN Cel ON Proby.celId = Cel.celId where Proby.projektId in (select projektId from Projekt where projektNazwa in ({0})) and dzienStart between '" + dateTimePickerTargetProjectsOd.Value.Date + "' and '" + dateTimePickerTargetProjectsDo.Value.Date + "' group by Cel.celNazwa;";
                DataSet dP = sqlQuery.GetDataFromSql(String.Format(sql, String.Join(",", wybraneCelProjekty.Select(x => $"\'{x}\'"))));
                DataView source = new DataView(dP.Tables[0]);
                chartTargetAllProjects.DataSource = source;
                chartTargetAllProjects.Series[0].XValueMember = "Cel Id";
                chartTargetAllProjects.Series[0].YValueMembers = "Suma celów";
                chartTargetAllProjects.ChartAreas[0].AxisX.Interval = 1;
                chartTargetAllProjects.ChartAreas[0].AxisY.Interval = 5;
                chartTargetAllProjects.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chartTargetAllProjects.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                chartTargetAllProjects.DataBind();
                chartTargetAllProjects.Update();
                wybraneCelProjekty.Clear();
                connection.Close();
            }
        }







        #endregion
        #region Cel dla form








        #endregion
        #region Cel dla detali
        #endregion


    }
}


