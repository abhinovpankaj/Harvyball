using Microsoft.Office.Tools.Ribbon;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
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
            //btnSaveTemplate.Enabled= false;
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
    }
}
