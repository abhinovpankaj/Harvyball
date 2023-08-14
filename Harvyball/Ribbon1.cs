
using Harvyball.Properties;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Web.Caching;
using System.Windows;
using System.Windows.Forms.Integration;
using Forms = System.Windows.Forms;

namespace Harvyball
{
    public partial class Ribbon1
    {
        Microsoft.Office.Tools.CustomTaskPane taskPane;
        private LibraryUserControl libraryUserControl;
        
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            //btnSaveTemplate.Enabled= false;.
            label_match_to_Latest_Selected = "Match to First";
            Globals.ThisAddIn.Application.WindowSelectionChange += Application_WindowSelectionChange;
            menu1.Enabled = Settings.Default.flg_chk_PS;
            toggleButton1.Checked= Settings.Default.flg_chk_PS;
            toggleButton2.Checked= Settings.Default.flg_chk_PP;
            tBtnAll.Checked= Settings.Default.flg_chk_PS_all;
            tBtnHeight.Checked = Settings.Default.flg_chk_PS_height;
            tBtnWidth.Checked = Settings.Default.flg_chk_PS_width;
        }

        

        private void btnHarvey_Click(object sender, RibbonControlEventArgs e)
        {
            mod_HarveyBalls.do_Create_HB();
        }

        private void btnSaveSelected_Click(object sender, RibbonControlEventArgs e)
        {
            mod_SaveSendSlides.SaveSlides();
        }

        private void btnSendSelected_Click(object sender, RibbonControlEventArgs e)
        {
            mod_SaveSendSlides.SendSlides();
        }

        private void BtnLibrary_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (taskPane != null)
                {
                    if (!taskPane.Visible)
                    {
                        libraryUserControl.ClearUI();
                        taskPane.Visible = true;
                    }

                    return;
                }

                libraryUserControl = new LibraryUserControl();

                ElementHost host = new ElementHost()
                {
                    Dock = Forms.DockStyle.Fill,
                    Child = libraryUserControl,
                };

                Forms.UserControl containerUC = new Forms.UserControl();
                containerUC.Controls.Add(host);

                taskPane = Globals.ThisAddIn.CustomTaskPanes.Add(containerUC, "Library");
                taskPane.Width = 650;
                taskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight;
                taskPane.Visible = true;

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        private void btnDownloadTemplates_Click(object sender, RibbonControlEventArgs e)
        {
            string TEMPLATE_ZIP_URL = "https://f769dd93-7542-4334-a13c-45498a1592a3.usrfiles.com/archives/f769dd_dd1494b7e8c54bacb5bd0d5216305ad5.zip";
            try
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string deckmate = Path.Combine(appDataPath, "deckmate");
                string templatePath = Path.Combine(deckmate, "Templates");
                string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                const string zipFileName = "Template_D1_001.zip";

                if (Directory.Exists(templatePath) == false)
                {
                    Directory.CreateDirectory(templatePath);
                }
                bool isDirectoryEmpty = !Directory.EnumerateDirectories(templatePath).Any();
                if (isDirectoryEmpty)
                {
                    if (File.Exists(Path.Combine(downloadPath, zipFileName)))
                    {
                        File.Delete(Path.Combine(downloadPath, zipFileName));
                    }
                    // Set the security protocol to TLS 1.2
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    // Create a new instance of WebClient
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(TEMPLATE_ZIP_URL, Path.Combine(downloadPath, zipFileName));
                    ZipFile.ExtractToDirectory(Path.Combine(downloadPath, zipFileName), templatePath);
                }
                MessageBox.Show("Template library downloaded successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void btnSaveTemplate_Click(object sender, RibbonControlEventArgs e)
        {
            Helpers.Templates.SaveTemplates saveTemplates = new Helpers.Templates.SaveTemplates();
            saveTemplates.Savepptastemplates();
        }

        #region Painter
        public  bool auto_select;   //   used when selection is done by VBA
        public  bool flag_pressed;  //   flag that controls togle pressed
        public  bool flag_match_to; //   flag that controls enabled state of Split Match to Slide , First , Last
        public  string label_match_to; //   flag that controls label of Split Match to Slide , First , Last
        public  bool flag_match_to_visible; //    controls visible state of Split Match to First , Last
        public  string label_match_to_Latest_Selected;

        bool flg_chk_PS;
        bool flg_chk_PS_all;
        bool flg_chk_PS_width;
        bool flg_chk_PS_height;
        bool flg_chk_PP;

        Shape src_shp;
        Slide src_sld;
        int Currentindex;
        int src_shp_id;
        float PgSlideWidth;
        float PgSlideHeight;

        #endregion



        private void toggleButton5_Click(object sender, RibbonControlEventArgs e)
        {
            flag_pressed = !flag_pressed;
            if (tbtnMultiPainter.Checked)
            {
                if (Globals.ThisAddIn.Application.ActiveWindow.Selection.Type==PpSelectionType.ppSelectionShapes)
                {
                    if (Globals.ThisAddIn.Application.ActiveWindow.Selection.ShapeRange.Count >= 1)
                    {
                        flag_pressed = true;
                        Currentindex = Globals.ThisAddIn.Application.ActiveWindow.View.Slide.SlideIndex;
                        src_sld = Globals.ThisAddIn.Application.ActivePresentation.Slides[Currentindex];
                        src_shp = Globals.ThisAddIn.Application.ActiveWindow.Selection.ShapeRange[1];
                        src_shp_id = src_shp.Id;

                        Settings.Default.src_shp_id = src_shp_id;
                        Settings.Default.Currentindex = Currentindex;
                        
                    }
                }
                else if(Globals.ThisAddIn.Application.ActiveWindow.Selection.Type == PpSelectionType.ppSelectionNone)
                {
                    flag_pressed = true;
                    src_shp_id = 0;
                    Settings.Default.src_shp_id = src_shp_id;
                    Settings.Default.Currentindex = Currentindex;
                }
            }
            else
            {
                flag_pressed = true;
                src_shp_id = 0;
                Settings.Default.src_shp_id = 0;
                Settings.Default.Currentindex = 0;
            }
            Settings.Default.Save();
        }

        private void splitButton1_Click(object sender, RibbonControlEventArgs e)
        {
            ShapeRange shp_Rng;
            Shape X_Shp;
            float H1, W1, T1, L1;
            long p;
            if (Currentindex == 0)
                Currentindex = Properties.Settings.Default.Currentindex;

            flg_chk_PP = Properties.Settings.Default.flg_chk_PP;
            flg_chk_PS = Properties.Settings.Default.flg_chk_PS;

            if (flg_chk_PS) 
            {
                flg_chk_PS_all = Properties.Settings.Default.flg_chk_PS_all;
                flg_chk_PS_width = Properties.Settings.Default.flg_chk_PS_width;
                flg_chk_PS_height = Properties.Settings.Default.flg_chk_PS_height;
            }
            if (label_match_to == "Match to Slide")
            {
                PgSlideWidth = Globals.ThisAddIn.Application.ActivePresentation.PageSetup.SlideWidth;
                PgSlideHeight = Globals.ThisAddIn.Application.ActivePresentation.PageSetup.SlideHeight;
                X_Shp = Globals.ThisAddIn.Application.ActiveWindow.Selection.ShapeRange[1];

                if (IsPictureShape(X_Shp)) 
                {
                    X_Shp.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                    if( X_Shp.Width <= X_Shp.Height)
                    {
                        X_Shp.Width = PgSlideWidth;
                        X_Shp.Left = 0;
                        X_Shp.Top = -0.5f * (X_Shp.Height - PgSlideHeight);
                    }
                    else
                    {
                        X_Shp.Height = PgSlideHeight;
                        X_Shp.Top = 0;
                        X_Shp.Left = -0.5f * (X_Shp.Width - PgSlideWidth);
                    }

                    X_Shp.PictureFormat.Crop.ShapeTop = 0;
                    X_Shp.PictureFormat.Crop.ShapeLeft = 0;
                    X_Shp.PictureFormat.Crop.ShapeHeight = PgSlideHeight;
                    X_Shp.PictureFormat.Crop.ShapeWidth = PgSlideWidth;
                    X_Shp.PictureFormat.Crop.ShapeHeight = PgSlideHeight;
                    X_Shp.PictureFormat.Crop.ShapeWidth = PgSlideWidth;
                    Globals.ThisAddIn.Application.CommandBars.ExecuteMso("PictureFillCrop");                
           
                }
                else
                {
                    X_Shp.LockAspectRatio =  Microsoft.Office.Core.MsoTriState.msoFalse;
                    X_Shp.Width = PgSlideWidth;
                    X_Shp.Height = PgSlideHeight;
                    X_Shp.Top = 0;
                    X_Shp.Left = 0;
                }
            }
            else if (label_match_to == "Match to First")
            {
                shp_Rng = Globals.ThisAddIn.Application.ActiveWindow.Selection.ShapeRange;
                src_shp = shp_Rng[1];
                for (int i = 2; i <= shp_Rng.Count; i++)
                {
                    X_Shp = shp_Rng[i];
                    H1 = src_shp.Height;
                    W1 = src_shp.Width;
                    T1 = src_shp.Top;
                    L1 = src_shp.Left;
                    src_shp.PickUp();
                    X_Shp.Apply();

                    if (IsPictureShape(X_Shp))
                    {
                        X_Shp.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                        X_Shp.Select(Microsoft.Office.Core.MsoTriState.msoTrue);


                        if (flg_chk_PS)
                        {   
                            if (flg_chk_PS_all)
                            {
                                X_Shp.PictureFormat.Crop.ShapeHeight = H1;
                                X_Shp.PictureFormat.Crop.ShapeWidth = W1;
                                Globals.ThisAddIn.Application.CommandBars.ExecuteMso("PictureFillCrop");
                            }
                            else if (flg_chk_PS_width)
                            {
                                X_Shp.PictureFormat.CropRight = 0;
                                X_Shp.PictureFormat.CropLeft = 0;
                                X_Shp.PictureFormat.CropTop = 0;
                                X_Shp.PictureFormat.CropBottom = 0;
                                X_Shp.PictureFormat.Crop.ShapeWidth = W1;
                                if (X_Shp.PictureFormat.Crop.PictureWidth < W1)
                                {
                                    X_Shp.PictureFormat.Crop.PictureWidth = W1;
                                }
                                X_Shp.PictureFormat.Crop.PictureOffsetX = 0;
                            }
                            else if (flg_chk_PS_height)
                            {
                                X_Shp.PictureFormat.CropRight = 0;
                                X_Shp.PictureFormat.CropLeft = 0;
                                X_Shp.PictureFormat.CropTop = 0;
                                X_Shp.PictureFormat.CropBottom = 0;
                                X_Shp.PictureFormat.Crop.ShapeHeight = H1;
                                if (X_Shp.PictureFormat.Crop.PictureHeight < H1)
                                {
                                    X_Shp.PictureFormat.Crop.PictureHeight = H1;
                                }

                                X_Shp.PictureFormat.Crop.PictureOffsetY = 0;
                            }
                               
                        }

                        
                    }
                    else
                    {
                        if (flg_chk_PS)
                        {
                            X_Shp.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoFalse;
                            if (flg_chk_PS_all)
                            {
                                X_Shp.Width = W1;
                                X_Shp.Height = H1;
                            }
                            else if (flg_chk_PS_width)
                            {
                                X_Shp.Width = W1;
                               

                            }
                            else if (flg_chk_PS_height)
                            {

                                X_Shp.Height = H1;
                                
                            }
                        }
                    }

                    if (flg_chk_PP)
                    {
                        X_Shp.Top = T1;
                        X_Shp.Left = L1;
                    }
                }
            }
            else if (label_match_to == "Match to Last")
            {
                shp_Rng = Globals.ThisAddIn.Application.ActiveWindow.Selection.ShapeRange;
                src_shp = shp_Rng[shp_Rng.Count];
                for (int i = 1; i <= shp_Rng.Count-1; i++)
                {
                    X_Shp = shp_Rng[i];
                    H1 = src_shp.Height;
                    W1 = src_shp.Width;
                    T1 = src_shp.Top;
                    L1 = src_shp.Left;
                    src_shp.PickUp();
                    X_Shp.Apply();

                    if (IsPictureShape(X_Shp))
                    {
                        X_Shp.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                        X_Shp.Select(Microsoft.Office.Core.MsoTriState.msoTrue);


                        if (flg_chk_PS)
                        {
                            if (flg_chk_PS_all)
                            {
                                X_Shp.PictureFormat.Crop.ShapeHeight = H1;
                                X_Shp.PictureFormat.Crop.ShapeWidth = W1;
                                Globals.ThisAddIn.Application.CommandBars.ExecuteMso("PictureFillCrop");
                            }
                            else if (flg_chk_PS_width)
                            {                                
                                X_Shp.PictureFormat.CropRight = 0;
                                X_Shp.PictureFormat.CropLeft = 0;
                                X_Shp.PictureFormat.CropTop = 0;
                                X_Shp.PictureFormat.CropBottom = 0;
                                X_Shp.PictureFormat.Crop.ShapeWidth = W1;
                                if (X_Shp.PictureFormat.Crop.PictureWidth < W1)
                                {
                                    X_Shp.PictureFormat.Crop.PictureWidth = W1;
                                }
                                X_Shp.PictureFormat.Crop.PictureOffsetX = 0;
                                
                            }
                            else if (flg_chk_PS_height)
                            {
                                X_Shp.PictureFormat.CropRight = 0;
                                X_Shp.PictureFormat.CropLeft = 0;
                                X_Shp.PictureFormat.CropTop = 0;
                                X_Shp.PictureFormat.CropBottom = 0;
                                X_Shp.PictureFormat.Crop.ShapeHeight = H1;
                                if (X_Shp.PictureFormat.Crop.PictureHeight < H1)
                                {
                                    X_Shp.PictureFormat.Crop.PictureHeight = H1;
                                }

                                X_Shp.PictureFormat.Crop.PictureOffsetY=0;
                            }
                        }

                        
                    }
                    else
                    {
                        if (flg_chk_PS)
                        {
                            X_Shp.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoFalse;
                            if (flg_chk_PS_all)
                            {
                                X_Shp.Width = W1;
                                X_Shp.Height = H1;
                            }
                            else if (flg_chk_PS_width)
                            {
                                X_Shp.Width = W1;

                            }
                            else if (flg_chk_PS_height)
                            {
                                X_Shp.Height = H1;
                            }
                        }
                    }
                    if (flg_chk_PP)
                    {
                        X_Shp.Top = T1;
                        X_Shp.Left = L1;
                    }
                }
            }
    
        }
        private void Application_WindowSelectionChange(Selection my_Selection)
        {
            try
            {
                if (auto_select == false && flag_pressed == true)
                {
                    if (my_Selection.Type == PpSelectionType.ppSelectionNone)
                    {
                        if (flag_pressed == true)
                        {
                            flag_pressed = false;
                            tbtnMultiPainter.Checked=false;

                        }
                    }
                    else if (my_Selection.Type == PpSelectionType.ppSelectionShapes)
                    {
                        do_Multi_Painter(my_Selection.ShapeRange);
                    }
                }
                else if (auto_select == false)
                {
                    if (my_Selection.Type == PpSelectionType.ppSelectionShapes)
                    {
                        flag_match_to = true;
                        if (my_Selection.ShapeRange.Count > 1)
                        {
                            label_match_to = label_match_to_Latest_Selected;
                            flag_match_to_visible = true;
                        }
                        else
                        {
                            label_match_to = "Match to Slide";
                            flag_match_to_visible = false;
                        }
                    }
                    else
                    {
                        label_match_to = "Match to Slide";
                        flag_match_to = false;
                    }
                    splitButton1.Enabled = flag_match_to;
                    splitButton1.Label = label_match_to;
                    tbtnMatchFirst.Visible = flag_match_to_visible;
                    tbtnMatchLast.Visible = flag_match_to_visible;
                    tBTnMatchSlide.Visible = !flag_match_to_visible;
                                  
                }


            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        
        void do_Multi_Painter(Microsoft.Office.Interop.PowerPoint.ShapeRange shp_Rng)
        {
            try
            {

                float H1;
                float W1;
                float T1;
                float L1;

                if (src_shp_id == 0) 
                    src_shp_id = Properties.Settings.Default.src_shp_id;
                if (Currentindex == 0) 
                    Currentindex = Properties.Settings.Default.Currentindex;
                
                flg_chk_PP = Properties.Settings.Default.flg_chk_PP;
                flg_chk_PS = Properties.Settings.Default.flg_chk_PS;

                if (flg_chk_PS)
                {
                    flg_chk_PS_all = Properties.Settings.Default.flg_chk_PS_all;
                    flg_chk_PS_width = Properties.Settings.Default.flg_chk_PS_width;
                    flg_chk_PS_height = Properties.Settings.Default.flg_chk_PS_height;
                }
                if (src_shp_id == 0)
                {

                    PgSlideWidth = Globals.ThisAddIn.Application.ActivePresentation.PageSetup.SlideWidth;
                    PgSlideHeight = Globals.ThisAddIn.Application.ActivePresentation.PageSetup.SlideHeight;

                    foreach (Shape X_Shp in shp_Rng)
                    {

                        //   test if the shape has pictureformat crop
                        if (IsPictureShape(X_Shp))
                        {

                            X_Shp.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;

                            if (X_Shp.Width <= X_Shp.Height)
                            {
                                X_Shp.Width = PgSlideWidth;
                                X_Shp.Left = 0;
                                X_Shp.Top = -0.5f * (X_Shp.Height - PgSlideHeight);
                            }
                            else
                            {

                                X_Shp.Height = PgSlideHeight;
                                X_Shp.Top = 0;
                                X_Shp.Left = -0.5f * (X_Shp.Width - PgSlideWidth);

                            }

                            //   set crop shape height and width
                            X_Shp.PictureFormat.Crop.ShapeTop = 0;
                            X_Shp.PictureFormat.Crop.ShapeLeft = 0;

                            //   set crop shape height and width
                            X_Shp.PictureFormat.Crop.ShapeHeight = PgSlideHeight;
                            X_Shp.PictureFormat.Crop.ShapeWidth = PgSlideWidth;

                            //   set crop shape height and width
                            X_Shp.PictureFormat.Crop.ShapeHeight = PgSlideHeight;
                            X_Shp.PictureFormat.Crop.ShapeWidth = PgSlideWidth;


                            Globals.ThisAddIn.Application.CommandBars.ExecuteMso("PictureFillCrop");

                        }
                        else
                        {
                            X_Shp.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoFalse;

                            X_Shp.Width = PgSlideWidth;
                            X_Shp.Height = PgSlideHeight;
                            X_Shp.Top = 0;
                            X_Shp.Left = 0;
                        }
                        break;

                    }
                }
                else
                {

                    if (src_sld == null) 
                        src_sld = Globals.ThisAddIn.Application.ActivePresentation.Slides[Currentindex];

                    if (src_shp == null)
                    {
                        auto_select = true;
                        src_shp = GetShapebyID(src_sld, src_shp_id);
                        auto_select = false;
                    }
                    if (src_shp == null)
                    {
                        flag_pressed = false;
                        this.RibbonUI.InvalidateControl("tbtnMultiPainter");
                        return;
                    }

                    H1 = src_shp.Height;
                    W1 = src_shp.Width;
                    T1 = src_shp.Top;
                    L1 = src_shp.Left;

                    foreach (Shape X_Shp in shp_Rng)
                    {                        
                        src_shp.PickUp();                        
                        X_Shp.Apply();
        
                        if (IsPictureShape(X_Shp))
                        {

                            X_Shp.LockAspectRatio =  Microsoft.Office.Core.MsoTriState.msoTrue;
                            if (flg_chk_PS == true)                            
                            {
                                if (flg_chk_PS_all)
                                {
                                    //   set crop shape height and width
                                    X_Shp.PictureFormat.Crop.ShapeHeight = H1;
                                    X_Shp.PictureFormat.Crop.ShapeWidth = W1;

                                    Globals.ThisAddIn.Application.CommandBars.ExecuteMso("PictureFillCrop");                                    
                                }
                                else if (flg_chk_PS_width)
                                {
                                    X_Shp.PictureFormat.CropRight = 0;
                                    X_Shp.PictureFormat.CropLeft = 0;
                                    X_Shp.PictureFormat.CropTop = 0;
                                    X_Shp.PictureFormat.CropBottom = 0;
                                    X_Shp.PictureFormat.Crop.ShapeWidth = W1;
                                    if (X_Shp.PictureFormat.Crop.PictureWidth<W1)
                                    {
                                        X_Shp.PictureFormat.Crop.PictureWidth = W1;
                                    }
                                    
                                    X_Shp.PictureFormat.Crop.PictureOffsetX = 0;
                                }
                                else if (flg_chk_PS_height)
                                {
                                    X_Shp.PictureFormat.CropRight = 0;
                                    X_Shp.PictureFormat.CropLeft = 0;
                                    X_Shp.PictureFormat.CropTop = 0;
                                    X_Shp.PictureFormat.CropBottom = 0;
                                    X_Shp.PictureFormat.Crop.ShapeHeight = H1;
                                    if (X_Shp.PictureFormat.Crop.PictureHeight <H1)
                                    {
                                        X_Shp.PictureFormat.Crop.PictureHeight = H1;
                                    }
                                    
                                    X_Shp.PictureFormat.Crop.PictureOffsetY = 0;
                                }
                            }
                        }
                        else
                        {
                            //   Paint Size = true
                            if (flg_chk_PS == true)
                            {
                                X_Shp.LockAspectRatio =  Microsoft.Office.Core.MsoTriState.msoFalse;

                                if (flg_chk_PS_all)
                                {
                                    X_Shp.Width = W1;
                                    X_Shp.Height = H1;
                                }
                                else if (flg_chk_PS_width)
                                {
                                    X_Shp.Width = W1;
                                }
                                else if (flg_chk_PS_height)
                                {
                                    X_Shp.Height = H1;
                                }
                            }

                        }
                        //IF Paint Position = TRUE
                        if (flg_chk_PP == true)
                        {
                            X_Shp.Top = T1;
                            X_Shp.Left = L1;
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        Shape GetShapebyID(Slide z_slide, int shape_id)
        {
            Shape foundShape =null ;
            try
            {           
                foreach (Shape z_Shp in z_slide.Shapes)
                {
                    if (z_Shp.Id== shape_id)
                    {
                        foundShape = z_Shp;
                        break;
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return foundShape  ;
        }
        bool IsPictureShape (Microsoft.Office.Interop.PowerPoint.Shape m_shp)
        {
            bool test_valid_shape = false;
            try
            {               
                bool ret_bool;
                if (m_shp.Type == Microsoft.Office.Core.MsoShapeType.msoPicture)
                {
                   ret_bool = Convert.ToBoolean(true);
                }
                else
                {
                    ret_bool = Convert.ToBoolean(false);
                }
                test_valid_shape = ret_bool;

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return test_valid_shape;
        }

        private void tbtnMatchLast_Click(object sender, RibbonControlEventArgs e)
        {
            label_match_to = "Match to Last";
            label_match_to_Latest_Selected = "Match to Last";
            splitButton1.Label = label_match_to;
        }

        private void tbtnMatchFirst_Click(object sender, RibbonControlEventArgs e)
        {
            label_match_to = "Match to First";
            label_match_to_Latest_Selected = "Match to First";
            splitButton1.Label = label_match_to;
        }

        private void tBTnMatchSlide_Click(object sender, RibbonControlEventArgs e)
        {
            label_match_to= "Match to Slide";
            label_match_to_Latest_Selected = "Match to Slide";
            splitButton1.Label = label_match_to;
        }

        private void toggleButton1_Click(object sender, RibbonControlEventArgs e)
        {
            Settings.Default.flg_chk_PS = toggleButton1.Checked;
            flg_chk_PS= toggleButton1.Checked; 
            if (flg_chk_PS)
            {
                menu1.Enabled = true;
            }
            else
            {
                menu1.Enabled = false;
            }
            Settings.Default.Save();
        }

        private void toggleButton2_Click(object sender, RibbonControlEventArgs e)
        {
            Settings.Default.flg_chk_PP = toggleButton2.Checked;
            Settings.Default.Save();
        }

        private void tBtnAll_Click(object sender, RibbonControlEventArgs e)
        {
            Settings.Default.flg_chk_PS_all = tBtnAll.Checked;
            if (tBtnAll.Checked)
            {
                tBtnWidth.Checked = false;
                tBtnHeight.Checked = false;
                Settings.Default.flg_chk_PS_width = false;
                Settings.Default.flg_chk_PS_height = false;
            }
            else
            {
                tBtnWidth.Checked = true;
                tBtnHeight.Checked = false;
                Settings.Default.flg_chk_PS_width = true;
                Settings.Default.flg_chk_PS_height = false;
            }
            Settings.Default.Save();
        }

        private void tBtnWidth_Click(object sender, RibbonControlEventArgs e)
        {
            Settings.Default.flg_chk_PS_width = tBtnWidth.Checked;
            if (tBtnWidth.Checked)
            {
                tBtnAll.Checked = false;
                tBtnHeight.Checked = false;
                Settings.Default.flg_chk_PS_height = false;
                Settings.Default.flg_chk_PS_all = false;
            }
            else
            {
                tBtnAll.Checked = false;
                tBtnHeight.Checked = true;
                Settings.Default.flg_chk_PS_height = true;
                Settings.Default.flg_chk_PS_all = false;
            }
            Settings.Default.Save();
        }

        private void tBtnHeight_Click(object sender, RibbonControlEventArgs e)
        {
            Settings.Default.flg_chk_PS_height = tBtnHeight.Checked;
            if (tBtnHeight.Checked)
            {
                tBtnAll.Checked = false;
                tBtnWidth.Checked = false;
                Settings.Default.flg_chk_PS_width = false;
                Settings.Default.flg_chk_PS_all = false;
            }
            else
            {
                tBtnAll.Checked = true;
                tBtnWidth.Checked = false;
                Settings.Default.flg_chk_PS_width = false;
                Settings.Default.flg_chk_PS_all = true;
            }
            Settings.Default.Save();
        }
    }
}

