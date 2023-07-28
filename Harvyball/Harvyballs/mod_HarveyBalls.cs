using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Harvyball
{
    public struct ChooseColorStruc
    {
        public long lStructSize;
        public long hwndOwner;
        public long hInstance;
        public long rgbResult;
        public long lpCustColors;
        public long flags;
        public long lCustData;
        public long lpfnHook;
        public string lpTemplateName;
    }
    public struct udtRECT
    {
        public long Left;
        public long Top;
        public long Right;
        public long Bottom;
    }

    public static class mod_HarveyBalls
    {
        [DllImport("user32", EntryPoint = "FindWindowA")]
        private static extern long FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32", EntryPoint = "GetWindowLongA")]
        private static extern long GetWindowLong(long hWnd, long nIndex);
        [DllImport("user32", EntryPoint = "SetWindowLongA")]
        private static extern long SetWindowLong(long hWnd, long nIndex, long dwNewLong);
        [DllImport("user32")]
        private static extern long DrawMenuBar(long hWnd);
        [DllImport("user32")]
        private static extern long GetSystemMetrics(long Index);
        [DllImport("user32")]
        private static extern long GetDC(long hWnd);
        [DllImport("user32")]
        private static extern long ReleaseDC(long hWnd, long hDC);
        [DllImport("gdi32")]
        private static extern long GetDeviceCaps(long hDC, long Index);
        [DllImport("user32")]
        private static extern long GetWindowRect(long hWnd, ref udtRECT lpRect);
        [DllImport("comdlg32.dll", EntryPoint = "ChooseColorA")]
        private static extern long ChooseColor(ChooseColorStruc pChoosecolor);

        [DllImport("user32", EntryPoint = "GetWindowTextA")]
        private static extern long GetWindowText(long hWnd, string lpString, long cch);
        [DllImport("user32", EntryPoint = "GetWindowTextLengthA")]
        private static extern long GetWindowTextLength(long hWnd);
        [DllImport("user32")]
        private static extern long GetWindow(long hWnd, long wCmd);
        [DllImport("user32")]
        private static extern bool IsWindowVisible(long hWnd);
        [DllImport("user32", EntryPoint = "GetClassNameA")]
        private static extern long GetClassName(long hWnd, string lpClassName, long nMaxCount);
        [DllImport("user32")]
        private static extern long BringWindowToTop(long hWnd);
        [DllImport("user32.dll")]
        public static extern int GetKeyState(long nVirtKey);

        private const float W1 = 25f;
        private const float H1 = 25f;

        public static string HB_Name;
        public static Color sel_rgb;
        public static string sel_color;
        public static bool flag_hb_sel;
        public static float ppt_win_L;
        public static float ppt_win_T;
        public static Dictionary<string, string> dict_accent = new Dictionary<string, string>();
        public static Form frmHB;

        //private static long Currentindex;
        //private static object X_slide;

        private static long Slide_Height;
        private static long Slide_Width;
        private static float L1;
        private static float T1;

        public static void do_Create_HB()
        {
            try
            {
                Presentation presentation = Globals.ThisAddIn.Application.ActivePresentation;
                PowerPoint.DocumentWindow activeWindow = presentation.Application.ActiveWindow;
                PowerPoint.Slide activeSlide = (Slide)activeWindow.View.Slide;
                int slideIndex = activeSlide.SlideIndex;
                if (slideIndex == 0)
                    return;
                Slide Pslide = presentation.Slides[slideIndex];

                PowerPoint.Shape hShape;
                PowerPoint.Shape b_shp;
                PowerPoint.Shape w_shp;
                var vArray = new string[3];

                Slide_Width = (long)Math.Round(presentation.PageSetup.SlideWidth);
                Slide_Height = (long)Math.Round(presentation.PageSetup.SlideHeight);

                L1 = (float)((double)(Slide_Width - W1) * 0.5d);
                T1 = (float)((double)(Slide_Height - H1) * 0.5d);

                hShape = Pslide.Shapes.AddShape(MsoAutoShapeType.msoShapePie, L1, T1, W1, H1);
                hShape.Line.Visible = MsoTriState.msoFalse; // msoFalse
                hShape.Fill.Visible = MsoTriState.msoTrue;
                hShape.Adjustments[1] = -90;
                hShape.Adjustments[2] = 0f;   // 25%

                b_shp = Pslide.Shapes.AddShape(MsoAutoShapeType.msoShapeOval, L1, T1, W1, H1);
                b_shp.Line.Visible = MsoTriState.msoTrue;
                b_shp.Fill.Visible = MsoTriState.msoFalse;
                b_shp.Line.ForeColor = hShape.Fill.ForeColor;

                vArray[0] = hShape.Name;
                vArray[1] = b_shp.Name;
                w_shp = Pslide.Shapes.Range(vArray).Group();
                w_shp.LockAspectRatio = (MsoTriState)Conversion.Int(true);
                w_shp.Name = "hb~" + Strings.Format(DateTime.Now, "hhmmss");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static float ConvertPixelsToPoints(float sngPixels, string sXorY)
        {
            float ConvertPixelsToPointsRet = default;
            long hDC;
            try
            {
                hDC = GetDC(0L);
                if (sXorY == "X")
                    ConvertPixelsToPointsRet = (float)((double)sngPixels * (72d / GetDeviceCaps(hDC, 88L)));
                if (sXorY == "Y")
                    ConvertPixelsToPointsRet = (float)((double)sngPixels * (72d / GetDeviceCaps(hDC, 90L)));
                ReleaseDC(0L, hDC);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return ConvertPixelsToPointsRet;
        }
        public static void set_HB_Percent(int val)
        {
            try
            { 
            PowerPoint.Shape h_shp;
            PowerPoint.Shape w_shp;

            Presentation presentation = Globals.ThisAddIn.Application.ActivePresentation;
            DocumentWindow activeWindow = presentation.Application.ActiveWindow;
            Slide activeSlide = (Slide)activeWindow.View.Slide;
            int Currentindex = activeSlide.SlideIndex;

            Slide Pslide = presentation.Slides[Currentindex];

            w_shp = (PowerPoint.Shape)get_shape_by_id(Pslide, HB_Name);
            h_shp = w_shp.GroupItems[1];

            if (val == 0)
            {
                h_shp.Adjustments[2] = -90;   // 45
                h_shp.Fill.Visible = MsoTriState.msoFalse;
            }
            else
            {
                h_shp.Fill.Visible = MsoTriState.msoTrue;
                h_shp.Adjustments[2] = (float)(-90 + 3.6d * val);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static object get_shape_by_id(Slide m_sld, string m_id)
        {
            object get_shape_by_idRet = default;

            try
            { 
            PowerPoint.Shape ret_shp;
            ret_shp = null;
            foreach (PowerPoint.Shape m_shp in m_sld.Shapes)
            {
                if ((m_shp.Id.ToString() ?? "") == (m_id ?? ""))
                {
                    ret_shp = m_shp;
                    break;
                }
            }
            get_shape_by_idRet = ret_shp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return get_shape_by_idRet;
        }
        public static void get_btn_back_color(Button w_btn)
        {
            try
            {

                PowerPoint.Application pptApp = Globals.ThisAddIn.Application;
                // Presentation presentation = Globals.ThisAddIn.Application.ActivePresentation;
                //Slide pptSlide = presentation.Slides[presentation.SlideShowWindow.View.Slide.SlideIndex];

                if (pptApp.ActiveWindow.Selection.Type == PpSelectionType.ppSelectionShapes)
                {
                    PowerPoint.Shape selectedShape = pptApp.ActiveWindow.Selection.ShapeRange[1];
                    //string shapeName = selectedShape.Name;
                    Color fillColor = w_btn.BackColor;

                    selectedShape.Fill.ForeColor.RGB = fillColor.ToArgb();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        
       
        public static void set_HB_Color(Color ColorCode)
        {
            try
            {

                PowerPoint.Shape h_shp;
                PowerPoint.Shape w_shp;
                PowerPoint.Shape b_shp;
                
                Presentation presentation = Globals.ThisAddIn.Application.ActivePresentation;
                DocumentWindow activeWindow = presentation.Application.ActiveWindow;
                Slide activeSlide = (Slide)activeWindow.View.Slide;
                int Currentindex = activeSlide.SlideIndex;

                if (Currentindex == 0)
                    return;

                Slide Pslide = presentation.Slides[Currentindex];

                w_shp = (PowerPoint.Shape)get_shape_by_id(Pslide, HB_Name);
                h_shp = w_shp.GroupItems[1];
                b_shp = w_shp.GroupItems[2];

                if (ColorCode == null)
                {
                    h_shp.Fill.Visible = MsoTriState.msoFalse;
                }
                else
                {

                    h_shp.Fill.Visible = MsoTriState.msoTrue;
                    h_shp.Fill.ForeColor.RGB = ColorTranslator.ToOle(ColorCode);
                    h_shp.Fill.ForeColor.RGB = ColorTranslator.ToOle(ColorCode);
                    b_shp.Line.ForeColor.RGB = ColorTranslator.ToOle(ColorCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
  
    }
}
