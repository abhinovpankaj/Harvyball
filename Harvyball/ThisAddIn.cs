using Microsoft.VisualBasic;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using System.Windows.Forms;
using System.Collections.Generic;
using FlatIcons;

namespace Harvyball
{
    public partial class ThisAddIn
    {
        PowerPoint.Application powerPointApp;
        private List<Form> openForms = new List<Form>();
        
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            APIHelper.SaveAccessTokenAsync("C2EN7ZA0ckcloGzJEdKpaz6YfoqoFOGGELFk4kaOPpacXNXv");

            powerPointApp = Globals.ThisAddIn.Application;
            powerPointApp.WindowSelectionChange += PowerPointApp_WindowSelectionChange;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }
        // Event handler for the WindowSelectionChange event
        private void PowerPointApp_WindowSelectionChange(PowerPoint.Selection sel)
        {

            if (sel.Type == PowerPoint.PpSelectionType.ppSelectionShapes && sel.ShapeRange.Count > 0)
            {
                PowerPoint.Shape shape = sel.ShapeRange[1];
                
                if (Strings.Left(shape.Name, 3) == "hb~")
                {
                    foreach (Form form in openForms)
                    {
                        form.Close();
                    }
                    openForms.Clear();
                    Form frmHB = new frm_HB();
                    Form frmHB1 = new frm_HB();
                    mod_HarveyBalls.HB_Name = shape.Id.ToString();
                    float zoom = powerPointApp.ActiveWindow.View.Zoom / 100;
                    var pointX = mod_HarveyBalls.ConvertPixelsToPoints(shape.Left, "X");
                    var pointY = mod_HarveyBalls.ConvertPixelsToPoints(shape.Top, "Y");
                    int pX = powerPointApp.ActiveWindow.PointsToScreenPixelsY(shape.Left);
                    int pY = powerPointApp.ActiveWindow.PointsToScreenPixelsX((shape.Top * zoom) + 50f);
                    int X = powerPointApp.ActiveWindow.PointsToScreenPixelsY(pointX);
                    int Y = powerPointApp.ActiveWindow.PointsToScreenPixelsX(pointY);
                    //X = (int)Math.Round((double)mod_HarveyBalls.ConvertPixelsToPoints(shape.Left,"X")) ;

                    frmHB.Left = pY;
                    frmHB.Top = pX;//new System.Drawing.Point(pX, pY);
                    frmHB.Show();
                    openForms.Add(frmHB);
                }
                else
                {
                    foreach (Form form in openForms)
                    {
                        form.Close();
                    }
                    openForms.Clear();
                }

            }
            else
            {
                foreach (Form form in openForms)
                {
                    form.Close();
                }
                openForms.Clear();
            }
        }
        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
