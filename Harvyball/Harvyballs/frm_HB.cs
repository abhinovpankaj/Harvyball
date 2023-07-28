using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.PowerPoint;
using System.Drawing;
using Point = System.Drawing.Point;

namespace Harvyball
{
    public partial class frm_HB : Form
    {
        public frm_HB()
        {
            InitializeComponent();
        }
        private void txt_percent_MouseDown(object sender, MouseEventArgs e)
        {
            if (Conversion.Val(txt_percent.Text) > 0d)
            {
                txt_percent.Text = (Conversion.Val(txt_percent.Text) - 1d).ToString();
            }
        }

        private void frm_HB_Activated(object sender, EventArgs e)
        {
            try
            {
                Presentation presentation = Globals.ThisAddIn.Application.ActivePresentation;
                DocumentWindow activeWindow = presentation.Windows[1];
                Shape selectedShape = activeWindow.Selection.ShapeRange[1];
                double zoom = activeWindow.View.Zoom / 100d;

                Shape w_shp = activeWindow.Selection.ShapeRange[1];
                Shape h_shp = w_shp.GroupItems[1];


                Top = (int)Math.Round((double)mod_HarveyBalls.ConvertPixelsToPoints(activeWindow.PointsToScreenPixelsY(0f), "Y") + (double)w_shp.Top * zoom + 50d);
                Left = (int)Math.Round((double)mod_HarveyBalls.ConvertPixelsToPoints(activeWindow.PointsToScreenPixelsX(0f), "X") - Width * 0.1d + (double)(w_shp.Left + w_shp.Width) * zoom + 200d);

                double adjustmentsItem2 = (double)h_shp.Adjustments[2];
                if (adjustmentsItem2 > -180 && adjustmentsItem2 < -90)
                {
                    txt_percent.Value = (decimal)((360d + adjustmentsItem2 + 90d) / 3.6d);
                }
                else
                {
                    txt_percent.Value = (decimal)((adjustmentsItem2 + 90d) / 3.6d);
                }
                mod_HarveyBalls.HB_Name = w_shp.Id.ToString();
                w_shp.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txt_percent_ValueChanged(object sender, EventArgs e)
        {
            if (Information.IsNumeric(txt_percent.Text))
            {
                int percent = Convert.ToInt32(txt_percent.Value);
                if (percent >= 0d & percent <= 100d)
                {
                    mod_HarveyBalls.set_HB_Percent(percent);
                }
            }
        }

        private void btnPickColor_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = true;
            MyDialog.ShowHelp = true;
            if (MyDialog.ShowDialog() == DialogResult.OK)
                mod_HarveyBalls.set_HB_Color(MyDialog.Color);
            frm_HB.ActiveForm.Hide();
        }
    }
}
