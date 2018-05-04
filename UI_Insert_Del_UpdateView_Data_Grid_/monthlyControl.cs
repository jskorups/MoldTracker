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
    public partial class monthlyControl : UserControl
    {
        private static monthlyControl _instance;
        public static monthlyControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new monthlyControl();
                return _instance;
            }
        }
        public monthlyControl()
        {
            InitializeComponent();
        }
    }
}
