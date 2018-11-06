using Microsoft.VisualBasic;
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
    public partial class Form1 : Form
    {

        TextBox txtLog;
        GanttChart ganttChart1;
        GanttChart ganttChart2;
        GanttChart ganttChart3;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SaveImageToolStripMenuItem.Click += new EventHandler(SaveImageToolStripMenuItem_Click);

            txtLog = new TextBox();
            txtLog.Dock = DockStyle.Fill;
            txtLog.Multiline = true;
            txtLog.Enabled = false;
            txtLog.ScrollBars = ScrollBars.Horizontal;
            tableLayoutPanel1.Controls.Add(txtLog, 0, 3);

            //first Gantt Chart
            ganttChart1 = new GanttChart();
            ganttChart1.AllowChange = false;
            ganttChart1.Dock = DockStyle.Fill;
            ganttChart1.FromDate = new DateTime(2015, 12, 12, 0, 0, 0);
            ganttChart1.ToDate = new DateTime(2015, 12, 24, 0, 0, 0);
            tableLayoutPanel1.Controls.Add(ganttChart1, 0, 1);

            ganttChart1.MouseMove += new MouseEventHandler(ganttChart1.GanttChart_MouseMove);
            ganttChart1.MouseMove += new MouseEventHandler(GanttChart1_MouseMove);
            ganttChart1.MouseDragged += new MouseEventHandler(ganttChart1.GanttChart_MouseDragged);
            ganttChart1.MouseLeave += new EventHandler(ganttChart1.GanttChart_MouseLeave);
            ganttChart1.ContextMenuStrip = ContextMenuGanttChart1;

            List<BarInformation> lst1 = new List<BarInformation>();

            lst1.Add(new BarInformation("Row 1", new DateTime(2015, 12, 12), new DateTime(2015, 12, 16), Color.Aqua, Color.Khaki, 0));
            lst1.Add(new BarInformation("Row 2", new DateTime(2015, 12, 13), new DateTime(2015, 12, 20), Color.AliceBlue, Color.Khaki, 1));
            lst1.Add(new BarInformation("Row 3", new DateTime(2015, 12, 14), new DateTime(2015, 12, 24), Color.Violet, Color.Khaki, 2));
            lst1.Add(new BarInformation("Row 2", new DateTime(2015, 12, 21), new DateTime(2015, 12, 22, 12, 0, 0), Color.Yellow, Color.Khaki, 1));
            lst1.Add(new BarInformation("Row 1", new DateTime(2015, 12, 17), new DateTime(2015, 12, 24), Color.LawnGreen, Color.Khaki, 0));

            foreach (BarInformation bar in lst1)
            {
                ganttChart1.AddChartBar(bar.RowText, bar, bar.FromTime, bar.ToTime, bar.Color, bar.HoverColor, bar.Index);
            }

            //Second Gantt Chart
            ganttChart2 = new GanttChart();
            ganttChart2.Dock = DockStyle.Fill;
            ganttChart2.AllowChange = true;
            ganttChart2.AllowManualEditBar = true;
            ganttChart2.FromDate = new DateTime(2015, 12, 24, 17, 0, 0);
            ganttChart2.ToDate = new DateTime(2015, 12, 24, 22, 0, 0);
            tableLayoutPanel1.Controls.Add(ganttChart2, 0, 2);

            ganttChart2.MouseMove += new MouseEventHandler(ganttChart2.GanttChart_MouseMove);
            ganttChart2.MouseMove += new MouseEventHandler(GanttChart2_MouseMove);
            ganttChart2.MouseDragged += new MouseEventHandler(ganttChart2.GanttChart_MouseDragged);
            ganttChart2.MouseLeave += new EventHandler(ganttChart2.GanttChart_MouseLeave);
            ganttChart2.BarChanged += new EventHandler(ganttChart2_BarChanged);
            ganttChart2.ContextMenuStrip = ContextMenuGanttChart1;

            List<BarInformation> lst2 = new List<BarInformation>();

            Random rand = new Random();
            int numberOfRowsToAdd = rand.Next(1, 2);

            for (int i = 0; i <= numberOfRowsToAdd; i++)
            {
                int startHour = rand.Next(17, 21);
                int endHour = rand.Next(startHour, 21);
                int startMinute = rand.Next(1, 59);
                int endMinute = rand.Next(startMinute, 59);

                if (startHour == 0) startHour = 17;
                if (endHour == 0) endHour = 21;

                if (startHour == endHour)
                {
                    if (startHour == 17)
                    {
                        endHour += 1;
                    }
                    else
                    {
                        startHour -= 1;
                    }
                }

                if (endHour >= 22)
                {
                    endHour = 22;
                    endMinute = rand.Next(1, 59);
                }

                lst2.Add(new BarInformation("Row " + (i + 1), new DateTime(2015, 12, 24, startHour, startMinute, 0), new DateTime(2015, 12, 24, endHour, endMinute, 0), Color.Maroon, Color.Khaki, i));
            }

            foreach (BarInformation bar in lst2)
            {
                ganttChart2.AddChartBar(bar.RowText, bar, bar.FromTime, bar.ToTime, bar.Color, bar.HoverColor, bar.Index);
            }

            //third Gantt Chart
            ganttChart3 = new GanttChart();
            ganttChart3.AllowChange = false;
            ganttChart3.Dock = DockStyle.Fill;
            ganttChart3.FromDate = new DateTime(2015, 1, 1, 0, 0, 0);
            ganttChart3.ToDate = new DateTime(2015, 12, 31, 0, 0, 0);
            tableLayoutPanel1.Controls.Add(ganttChart3, 0, 0);

            ganttChart3.MouseMove += new MouseEventHandler(ganttChart3.GanttChart_MouseMove);
            ganttChart3.MouseDragged += new MouseEventHandler(ganttChart3.GanttChart_MouseDragged);
            ganttChart3.MouseLeave += new EventHandler(ganttChart3.GanttChart_MouseLeave);
            //ganttChart3.ToolTip.Draw += new DrawToolTipEventHandler(ganttChart3.ToolTipText_Draw);
            //ganttChart3.ToolTip.Popup += new PopupEventHandler(ganttChart3.ToolTipText_Popup);
            ganttChart3.ContextMenuStrip = ContextMenuGanttChart1;

            List<BarInformation> lst3 = new List<BarInformation>();

            lst3.Add(new BarInformation("Row 1", new DateTime(2015, 1, 1), new DateTime(2015, 5, 1), Color.Gray, Color.LightGray, 0));
            lst3.Add(new BarInformation("Row 2", new DateTime(2015, 1, 1), new DateTime(2015, 7, 1), Color.Gray, Color.LightGray, 1));
            lst3.Add(new BarInformation("Row 3", new DateTime(2015, 5, 1), new DateTime(2015, 8, 1), Color.Gray, Color.LightGray, 2));
            lst3.Add(new BarInformation("Row 2", new DateTime(2015, 10, 1), new DateTime(2015, 12, 1), Color.Gray, Color.LightGray, 3));
            lst3.Add(new BarInformation("Row 2", new DateTime(2015, 10, 1), new DateTime(2015, 12, 1), Color.Gray, Color.LightGray, 3));
            lst3.Add(new BarInformation("Row 2", new DateTime(2015, 10, 1), new DateTime(2015, 12, 1), Color.Gray, Color.LightGray, 3));
            lst3.Add(new BarInformation("Row 1", new DateTime(2015, 8, 1), new DateTime(2016, 01, 1), Color.Gray, Color.LightGray, 4));
            lst3.Add(new BarInformation("Row 1", new DateTime(2016, 1, 1), new DateTime(2016, 01, 1), Color.Gray, Color.LightGray, 4));

            foreach (BarInformation bar in lst3)
            {
                ganttChart3.AddChartBar(bar.RowText, bar, bar.FromTime, bar.ToTime, bar.Color, bar.HoverColor, bar.Index);
            }
        }

        private void GanttChart1_MouseMove(Object sender, MouseEventArgs e)
        {
            List<string> toolTipText = new List<string>();

            if (ganttChart1.MouseOverRowText.Length > 0)
            {
                BarInformation val = (BarInformation)ganttChart1.MouseOverRowValue;
                toolTipText.Add("[b]Date:");
                toolTipText.Add("From ");
                toolTipText.Add(val.FromTime.ToLongDateString() + " - " + val.FromTime.ToString("HH:mm"));
                toolTipText.Add("To ");
                toolTipText.Add(val.ToTime.ToLongDateString() + " - " + val.ToTime.ToString("HH:mm"));
            }
            else
            {
                toolTipText.Add("");
            }

            ganttChart1.ToolTipTextTitle = ganttChart1.MouseOverRowText;
            ganttChart1.ToolTipText = toolTipText;

        }

        private void GanttChart2_MouseMove(Object sender, MouseEventArgs e)
        {
            List<string> toolTipText = new List<string>();

            if (ganttChart2.MouseOverRowText != null && ganttChart2.MouseOverRowText != "" && ganttChart2.MouseOverRowValue != null)
            {
                object obj = ganttChart2.MouseOverRowValue;
                string typ = obj.GetType().ToString();
                if (typ.ToLower().Contains("barinformation"))
                {
                    BarInformation val = (BarInformation)ganttChart2.MouseOverRowValue;
                    toolTipText.Add("[b]Time:");
                    toolTipText.Add("From " + val.FromTime.ToString("HH:mm"));
                    toolTipText.Add("To " + val.ToTime.ToString("HH:mm"));
                }
                else if (typ.ToLower() == "string")
                {
                    toolTipText.Add(ganttChart2.MouseOverRowValue.ToString());
                }
            }
            else
            {
                toolTipText.Add("");
            }

            ganttChart2.ToolTipTextTitle = ganttChart2.MouseOverRowText;
            ganttChart2.ToolTipText = toolTipText;
        }

        private void ganttChart2_BarChanged(object sender, EventArgs e)
        {
            BarInformation b = sender as BarInformation;
            txtLog.Text += String.Format("\r\n{0} has changed", b.RowText);
        }

        private void SaveImageToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            Control chart = null;
            if (menuItem != null)
            {
                ContextMenuStrip calendarMenu = menuItem.Owner as ContextMenuStrip;

                if (calendarMenu != null)
                {
                    chart = calendarMenu.SourceControl;
                }
            }

            SaveImage(chart);
        }

        private void SaveImage(Control control)
        {
            GanttChart gantt = control as GanttChart;
            string filePath = Interaction.InputBox("Where to save the file?", "Save image", "C:\\Temp\\GanttChartTester.jpg");
            if (filePath.Length == 0)
                return;
            gantt.SaveImage(filePath);
            Interaction.MsgBox("Picture saved", MsgBoxStyle.Information);
        }

    }
}
