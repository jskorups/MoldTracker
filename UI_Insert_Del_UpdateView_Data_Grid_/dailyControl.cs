using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    public partial class dailyControl : UserControl
    {
        private static dailyControl _instance;
        public static dailyControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new dailyControl();
                return _instance;
            }
        }

        public dailyControl()
        {
            InitializeComponent();
        }
    }
}
