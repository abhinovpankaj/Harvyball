using Microsoft.VisualBasic;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using System.Windows.Forms;
using System.Collections.Generic;
using FlatIcons;
using System.Runtime.InteropServices;
using System;
using Microsoft.Office.Tools;
//using Microsoft.Office.Core;
using System.Drawing;
using Harvyball.CustomControls;
using System.Net.Cache;
using System.Windows.Forms.Integration;
using Harvyball.Harvyballs;
using System.Linq;

namespace Harvyball
{
    public partial class ThisAddIn
    {

        PowerPoint.Application powerPointApp;
        private Dictionary<string,HarvyHost> openForms = new Dictionary<string, HarvyHost>();
        
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

       
        private void PowerPointApp_WindowSelectionChange(PowerPoint.Selection selectedShape)
        {

            if (selectedShape.Type == PowerPoint.PpSelectionType.ppSelectionShapes && selectedShape.ShapeRange.Count > 0)
            {
                PowerPoint.Shape shape = selectedShape.ShapeRange[1];
                
                if (Strings.Left(shape.Name, 3) == "hb~")
                {
                    HarvyHost selectedForm;
                    //foreach (Form form in openForms)
                    //{
                    //    form.Close();
                    //}
                    //openForms.Clear();
                    mod_HarveyBalls.HB_Name = shape.Id.ToString();
                    if (openForms.ContainsKey(shape.Name))
                    {
                        openForms.TryGetValue(shape.Name, out selectedForm);
                    }
                    else
                    {
                        selectedForm = new HarvyHost();
                        openForms.Add(shape.Name,selectedForm);
                    }
                    
                    int shapeScreenX = powerPointApp.ActiveWindow.PointsToScreenPixelsX((float)shape.Left);
                    int shapeScreenY = powerPointApp.ActiveWindow.PointsToScreenPixelsY((float)shape.Top);
                    selectedForm.Left = shapeScreenX-10;
                    selectedForm.Top = shapeScreenY-80;
                    selectedForm.Show();
                   
                }
                else
                {
                    foreach (var form in openForms)
                    {
                        form.Value.Hide();
                    }
                   // openForms.Clear();
                }

            }
            else
            {
                foreach (var form in openForms)
                {
                    form.Value.Hide();
                }
                // openForms.Clear();
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
