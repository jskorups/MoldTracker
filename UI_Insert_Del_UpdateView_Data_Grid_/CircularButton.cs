using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace UI_Insert_Del_UpdateView_Data_Grid_
{
    class CircularButton : Button
    {
        protected override void OnPaint(PaintEventArgs prevent)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(prevent);
        }
        

        }
    }

