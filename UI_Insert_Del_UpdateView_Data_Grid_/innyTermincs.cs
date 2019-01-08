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
    public partial class innyTermincs : Form
    {
        public string innyTerminDzien
        {
            get
            {
                return dateTimeInnyDzien.Value.ToString();
            }
        }
        public DateTime innyTerminStart
        {
            get
            {
                return dateTimeInnyStart.Value;
            }

        }
        public DateTime innyTerminKoniec
        {
            get
            {
                return dateTimeInnyKoniec.Value;
            }
        }

        public innyTermincs()
        {
            InitializeComponent();
            #region - Format czasu dla DateTimePickera



            dateTimeInnyStart.Format = DateTimePickerFormat.Custom;
            dateTimeInnyStart.CustomFormat = "HH:mm";
            dateTimeInnyStart.MinDate = DateTime.Parse("6:00:00");

            dateTimeInnyKoniec.Format = DateTimePickerFormat.Custom;
            dateTimeInnyKoniec.CustomFormat = "HH:mm";
            dateTimeInnyKoniec.MinDate = DateTime.Parse("8:00:00");

            dateTimeInnyDzien.Format = DateTimePickerFormat.Custom;
            dateTimeInnyDzien.CustomFormat = "dd-MM-yyyy";

            dateTimeInnyStart.ShowUpDown = true;
            dateTimeInnyKoniec.ShowUpDown = true;
            dateTimeInnyDzien.ShowUpDown = true;

            #endregion
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


     
    }
}
