using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;

namespace Harvyball.Helpers.Templates
{
    internal class SaveTemplates
    {
        private readonly Microsoft.Office.Interop.PowerPoint.Application thisApplication = Globals.ThisAddIn.Application;
        private readonly Presentation thisPresentation = Globals.ThisAddIn.Application.ActivePresentation;
        private readonly DocumentWindow thisWindow = Globals.ThisAddIn.Application.ActiveWindow;
        public void Savepptastemplates()
        {
            try 
            { 
                string SearchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "deckmate", "Templates", "Saved Templates");
                if (Directory.Exists(SearchPath) == false)
                    Directory.CreateDirectory(SearchPath);

                DirectoryInfo directoryInfo = new DirectoryInfo(SearchPath);
                FileInfo[] files = directoryInfo.GetFiles();
                string strfile = string.Empty;
                if (files.Length == 0)
                {
                    strfile = "Template0.pptx";
                }
                else
                {
                    var orderedFiles = files.OrderByDescending(file => file.Name);
                    strfile = orderedFiles.First().Name;
                }
                string pattern = @"\d+";
                Match match = Regex.Match(strfile, pattern);

                if (match.Success)
                {
                    string numberAsString = match.Value;

                    if (int.TryParse(numberAsString, out int number))
                    {
                        number += 1;
                        string filename = "Template" + number.ToString();
                        string filenamepath = SearchPath + "\\" + filename + ".pptx";
                        string pngFilename = SearchPath + "\\" + filename + "_PNG";

                        if (Directory.Exists(pngFilename) == false)
                            Directory.CreateDirectory(pngFilename);

                        Selection selection = thisApplication.ActiveWindow.Selection;
                        Presentation newPresentation = thisApplication.Presentations.Add(MsoTriState.msoTrue);
                        
                        int slideIndex = 1;
                        foreach (Slide slide in selection.SlideRange)
                        {
                            slide.Copy();
                            newPresentation.Slides.Paste();
                            Slide vslide = thisPresentation.Slides[slide.SlideIndex];
                            vslide.Select();
                            float slideWidth = thisPresentation.PageSetup.SlideWidth;
                            float slideHeight = thisPresentation.PageSetup.SlideHeight;
                            float scaleFactor = 2.0f; // Adjust the scale factor as needed for better resolution
                            int imageWidth = (int)(slideWidth * scaleFactor);
                            int imageHeight = (int)(slideHeight * scaleFactor);

                            // Export the slide as PNG
                            string pngFileName = Path.Combine(pngFilename, filename + "_Slide_" + $"{slideIndex}.png");
                            vslide.Export(pngFileName, "PNG", imageWidth, imageHeight);
                            slideIndex++;
                        }

                        newPresentation.SaveAs(filenamepath, PpSaveAsFileType.ppSaveAsOpenXMLPresentation);
                        //SaveSlideAsPng(SearchPath, filename);
                        newPresentation.Close();

                        // Release resources
                        if (newPresentation != null) Marshal.ReleaseComObject(newPresentation);
                        MessageBox.Show("Slide(s) saved in Saved Templates");
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
