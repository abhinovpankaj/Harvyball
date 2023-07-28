
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace Harvyball
{
    public class mod_SaveSendSlides
    {
        public static string proposed_name;
        private static frm_Send frmSend;
        private static Frm_SaveSlides frmSave;

        public static void SendSlides()
        {
            string sld_selected;
            string attachment_path;
            bool as_pdf;
            bool all_slides;
            long x;
            const int ppSelectionSlides = 1;

            Presentation presentation = Globals.ThisAddIn.Application.ActivePresentation;
            DocumentWindow activeWindow = presentation.Application.ActiveWindow;
            Slide activeSlide = (Slide)activeWindow.View.Slide;
            SlideRange slideRange = Globals.ThisAddIn.Application.ActiveWindow.Selection.SlideRange;
            int Currentindex = activeSlide.SlideIndex;

            if (presentation.Path.ToString() == "")
            {
                MessageBox.Show("please save presentation first ...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (((int)activeWindow.Selection.Type) != ppSelectionSlides)
            {
                Slide slide = (Slide)activeWindow.View.Slide;
                presentation.Slides[slide.SlideIndex].Select();
            }
            else if (activeWindow.Selection.SlideRange.Count == 0)
            {
                MessageBox.Show("There is no slide selected ...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            proposed_name = Strings.Left(presentation.Name.ToString(), Strings.InStrRev(presentation.Name.ToString(), ".") - 1);
            sld_selected = "";

            foreach (Slide x_Sld in slideRange)
            {
                sld_selected = sld_selected + x_Sld.SlideNumber + ", ";
            }

            slideRange.Select();
            x = activeWindow.Selection.SlideRange.Count;

            if (Strings.Len(sld_selected) > 0)
            {
                sld_selected = Strings.Left(sld_selected, Strings.Len(sld_selected) - 2);
            }
            
            proposed_name = proposed_name + " Slides " + sld_selected;
            frmSend = new frm_Send();
            frmSend.ShowDialog();
            proposed_name = frmSend.txt_attachment.Text;
            as_pdf = frmSend.chk_as_pdf.Checked;
            all_slides = frmSend.opt_all.Checked;
            frmSend.Close();

            if (Strings.Len(proposed_name) > 0)
            {
                string PathSep = presentation.Path + PathSeparator(presentation.Path).ToString();

                if (as_pdf == true)
                {
                    if (all_slides == true)
                    {
                        attachment_path = Export_AllSlides_as_PDF(presentation, PathSep, proposed_name + ".pdf");
                    }
                    else
                    {
                        attachment_path = Export_SelectedSlides_as_PDF(presentation, PathSep, proposed_name + ".pdf", x);
                    }
                }
                else if (all_slides == true)
                {
                    Select_all_slides();
                    attachment_path = Create_SeparatePresentation(activeWindow.Selection.SlideRange, PathSep + proposed_name + ".pptx");
                }
                else
                {
                    attachment_path = Create_SeparatePresentation(activeWindow.Selection.SlideRange, PathSep + proposed_name + ".pptx");
                }
                if (File_Exists(attachment_path))
                {
                    OpenOutlookeMail(attachment_path);
                }
            }
            activeWindow.ViewType = PpViewType.ppViewNormal;
        }

        public static void OpenOutlookeMail(string attachmentPath)
        {
            Outlook.Application outlookApp = null;
            try
            {
                outlookApp = new Outlook.Application();
                Outlook.MailItem emailItem = outlookApp.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;

                if (emailItem != null)
                {
                    emailItem.Attachments.Add(attachmentPath);
                    emailItem.Display();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error opening Outlook email: " + ex.Message);
            }
            finally
            {
                if (outlookApp != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(outlookApp);
                    outlookApp = null;
                }
            }
        }
        public static bool File_Exists(string file_path)
        {
            bool ret_bool;
            string ret_str;

            ret_str = FileSystem.Dir(file_path);

            if (Strings.Len(ret_str) > 0)
            {
                ret_bool = true;
            }
            else
            {
                ret_bool = false;
            }

            if (Information.Err().Number != 0)
                Information.Err().Clear();

            return ret_bool;

        }
        public static void SaveSlides()
        {
            string sld_selected;
            bool as_pdf;
            bool all_slides;
            long x;
            const int ppSelectionSlides = 1;

            Presentation presentation = Globals.ThisAddIn.Application.ActivePresentation;
            PowerPoint.DocumentWindow activeWindow = Globals.ThisAddIn.Application.ActiveWindow;

            if (presentation.Path.ToString() == "")
            {
                MessageBox.Show("please save presentation first ...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string strPresentationName = presentation.Name;

            int intPosition = Strings.InStrRev(strPresentationName, ".") - 1;

            if (((int)activeWindow.Selection.Type) != ppSelectionSlides)
            {
                Slide slide = (Slide)activeWindow.View.Slide;
                presentation.Slides[slide.SlideIndex].Select();
            }
            else if (activeWindow.Selection.SlideRange.Count == 0)
            {
                Interaction.MsgBox("There is no slide selected ...", Constants.vbInformation);
                return;
            }
            if (intPosition < 1)
            {
                intPosition = strPresentationName.Length;
            }
                
            proposed_name = strPresentationName.Substring(0, intPosition);

            sld_selected = "";

            foreach (Slide x_Sld in activeWindow.Selection.SlideRange)
                sld_selected = sld_selected + x_Sld.SlideNumber + ", ";

            activeWindow.Selection.SlideRange.Select();
            x = ((long)(ulong)activeWindow.Selection.SlideRange.Count);

            if (Strings.Len(sld_selected) > 0)
                sld_selected = Strings.Left(sld_selected, Strings.Len(sld_selected) - 2);

            proposed_name = proposed_name + " Slides " + sld_selected;
            frmSave = new Frm_SaveSlides();
            frmSave.ShowDialog();
            proposed_name = frmSave.txt_attachment.Text;
            as_pdf = frmSave.chk_as_pdf.Checked;
            all_slides = frmSave.opt_all.Checked;
            frmSave.Close();
            string PathSep = presentation.Path + PathSeparator(presentation.Path).ToString();
            if (Strings.Len(proposed_name) > 0)
            {
                if (as_pdf == true)
                {
                    if (all_slides == true)
                    {
                        Export_AllSlides_as_PDF(presentation, PathSep, proposed_name + ".pdf");
                    }
                    else
                    {
                        Export_SelectedSlides_as_PDF(presentation, PathSep, proposed_name + ".pdf", x);
                    }
                }
                else if (all_slides == true)
                {
                    Select_all_slides();
                    Create_SeparatePresentation(activeWindow.Selection.SlideRange, PathSep + proposed_name + ".pptx");
                }
                else
                {
                   Create_SeparatePresentation(activeWindow.Selection.SlideRange, PathSep + proposed_name + ".pptx");
                }
            }
            activeWindow.ViewType = PpViewType.ppViewNormal;
        }
        public static string Create_SeparatePresentation(SlideRange sldrng, string path_to_save)
        {
            string Create_SeparatePresentationRet = string.Empty;
            Presentation NewPPT = null;
            Presentation OldPPT = Globals.ThisAddIn.Application.ActivePresentation;
            PowerPoint.DocumentWindow activeWindows = Globals.ThisAddIn.Application.ActiveWindow;
            SlideRange ppslr = sldrng;
            Slide Old_sld;
            Slide New_sld;

            try
            {
                NewPPT = Globals.ThisAddIn.Application.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoTrue);

                // Align Page Setup
                NewPPT.PageSetup.SlideHeight = OldPPT.PageSetup.SlideHeight;
                NewPPT.PageSetup.SlideOrientation = OldPPT.PageSetup.SlideOrientation;
                NewPPT.PageSetup.SlideSize = OldPPT.PageSetup.SlideSize;
                NewPPT.PageSetup.SlideWidth = OldPPT.PageSetup.SlideWidth;

                for (int i = 1; i <= ppslr.Count; i++)
                {
                    Old_sld = ppslr[i];
                    Old_sld.Copy();
                    NewPPT.Slides.Paste();
                    New_sld = (Slide)activeWindows.View.Slide; //Globals.ThisAddIn.Application.ActivePresentation.Slides.AddSlide(1, Old_sld.CustomLayout);
                    New_sld.Design = Old_sld.Design;
                    New_sld.ColorScheme = Old_sld.ColorScheme;
                    New_sld.FollowMasterBackground = Old_sld.FollowMasterBackground;
                }

                if (path_to_save.Contains("//"))
                    path_to_save = GetDocLocalPath(path_to_save);

                if (path_to_save.Contains("\\"))
                    path_to_save = GetDocLocalPath(path_to_save);

                NewPPT.SaveAs(path_to_save);
                NewPPT.Save();
                NewPPT.Close();

                if (File.Exists(path_to_save))
                {
                    Create_SeparatePresentationRet = path_to_save;
                }
                else
                {
                    Create_SeparatePresentationRet = string.Empty;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error creating separate presentation: " + ex.Message);
            }
            finally
            {
                if (NewPPT != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(NewPPT);
                }
            }

            return Create_SeparatePresentationRet;
        }
        public static string Export_SelectedSlides_as_PDF(Presentation pres, string folder_path, string fileName, long no_slides)
        {

            string ret_str;
            const int ppFixedFormatTypePDF = 2;
            const int ppFixedFormatIntentPrint = 2;
            const int ppPrintCurrent = 3;
            const int ppPrintSelection = 3;

            ret_str = folder_path + fileName;

            if (Strings.InStr(ret_str, "//") > 0)
                ret_str = GetDocLocalPath(ret_str);

            if (no_slides == 1L)
            {
                pres.ExportAsFixedFormat(Path: ret_str, FixedFormatType: (PpFixedFormatType)ppFixedFormatTypePDF, Intent: (PpFixedFormatIntent)ppFixedFormatIntentPrint, IncludeDocProperties: false, DocStructureTags: false, RangeType: (PpPrintRangeType)ppPrintCurrent);
            }
            else
            {
                pres.ExportAsFixedFormat(Path: ret_str, FixedFormatType: (PpFixedFormatType)ppFixedFormatTypePDF, Intent: (PpFixedFormatIntent)ppFixedFormatIntentPrint, IncludeDocProperties: false, DocStructureTags: false, RangeType: (PpPrintRangeType)ppPrintSelection);

            }

            if (Information.Err().Number != 0)
            {
                ret_str = "";
                MessageBox.Show("Please make sure there is no other PDF document opened using same name :" + Constants.vbLf + folder_path + fileName, "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Presentation saved as PDF: " + fileName, "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            return ret_str;

        }

        public static string Export_AllSlides_as_PDF(Presentation pres, string folder_path, string fileName)
        {
            string ret_str;

            const int ppFixedFormatTypePDF = 2;
            const int ppFixedFormatIntentPrint = 2;

            ret_str = folder_path + fileName;
            if (Strings.InStr(ret_str, "//") > 0)
                ret_str = GetDocLocalPath(ret_str); // GetLocalPath(ret_str)

            // Save the presentation as a PDF file in the same folder
            pres.ExportAsFixedFormat(Path: ret_str, FixedFormatType: (PpFixedFormatType)ppFixedFormatTypePDF, Intent: (PpFixedFormatIntent)ppFixedFormatIntentPrint, IncludeDocProperties: false, DocStructureTags: false);

            if (Information.Err().Number != 0)
            {
                ret_str = "";
                MessageBox.Show("Please make sure there is no other PDF document opened using same name :" + Constants.vbLf + folder_path + fileName, "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Presentation saved as PDF: " + fileName, "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            return ret_str;

        }
        private static string GetDocLocalPath(string docPath)
        {
            const string strcOneDrivePart = "https://d.docs.live.net/";
            string strRetVal;
            int bytSlashPos;
            string vtemp;
            string strRegPath;

            strRetVal = docPath + @"\";
            if ((Strings.Left(Strings.LCase(docPath), Strings.Len(strcOneDrivePart)) ?? "") == strcOneDrivePart) // yep, it's the OneDrive path
            {
                //bytSlashPos = Strings.InStr(Strings.Len(strcOneDrivePart) + 1, strRetVal, "/");
                //strRetVal = RegKeyRead(@"HKEY_CURRENT_USER\Environment\OneDrive") + strRetVal;
                //strRetVal = Strings.Replace(strRetVal, "/", @"\"); // slashes in the right direction
                //strRetVal = Strings.Replace(strRetVal, "%20", " "); // a space is a space once more

                bytSlashPos = strRetVal.IndexOf('/', strcOneDrivePart.Length+1)+1;
                strRetVal = docPath.Substring(bytSlashPos); //Strings.Mid(docPath, bytSlashPos);
                strRegPath = Registry.GetValue(@"HKEY_CURRENT_USER\Environment\", "OneDrive", "").ToString();
                strRetVal = strRegPath + "\\" + strRetVal;
                strRetVal = strRetVal.Replace("/", @"\");
                strRetVal = strRetVal.Replace("%20", " ");
            }
            vtemp = strRetVal.Substring(strRetVal.Length - 1);
            if (vtemp == "\\")
            {
                strRetVal = strRetVal.Substring(0, strRetVal.Length - 1);
            }
            return strRetVal;

        }
        public static void Select_all_slides()
        {
            long[] my_ARRAY;
            long nCounter;
            try
            {
                Presentation presentation = Globals.ThisAddIn.Application.ActivePresentation;
                PowerPoint.DocumentWindow activeWindow = Globals.ThisAddIn.Application.ActiveWindow;

                SlideRange rng_Slides;

                activeWindow.Selection.Unselect();
                activeWindow.ViewType = PpViewType.ppViewOutline;
                my_ARRAY = new long[(presentation.Slides.Count + 1)];
                nCounter = 0L;
                foreach (Slide X_slide in presentation.Slides)
                {
                    nCounter ++;
                    my_ARRAY[(int)nCounter] = X_slide.SlideIndex;
                }

                Array.Resize(ref my_ARRAY, (int)(nCounter + 1));
                rng_Slides = presentation.Slides.Range(my_ARRAY);
                rng_Slides.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private static string PathSeparator(string vPath)
        {
                string ret_str;

                ret_str = @"\";
                if (vPath.ToLower().Substring(1,4).ToString() == "http")
                    ret_str = "/";
            return ret_str;
        }
    }
}
