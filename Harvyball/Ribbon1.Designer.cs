namespace Harvyball
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ribbon1));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.GrpTools = this.Factory.CreateRibbonGroup();
            this.btnSaveSelected = this.Factory.CreateRibbonButton();
            this.btnSendSelected = this.Factory.CreateRibbonButton();
            this.GrpLibrary = this.Factory.CreateRibbonGroup();
            this.btnHarvey = this.Factory.CreateRibbonButton();
            this.BtnLibrary = this.Factory.CreateRibbonButton();
            this.btnDownloadTemplates = this.Factory.CreateRibbonButton();
            this.btnSaveTemplate = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.GrpTools.SuspendLayout();
            this.GrpLibrary.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.GrpTools);
            this.tab1.Groups.Add(this.GrpLibrary);
            this.tab1.Label = "HB";
            this.tab1.Name = "tab1";
            // 
            // GrpTools
            // 
            this.GrpTools.Items.Add(this.btnHarvey);
            this.GrpTools.Items.Add(this.btnSaveSelected);
            this.GrpTools.Items.Add(this.btnSendSelected);
            this.GrpTools.Label = "Tools";
            this.GrpTools.Name = "GrpTools";
            // 
            // btnSaveSelected
            // 
            this.btnSaveSelected.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSaveSelected.Label = "Save Selected";
            this.btnSaveSelected.Name = "btnSaveSelected";
            this.btnSaveSelected.OfficeImageId = "FileSaveAs";
            this.btnSaveSelected.ShowImage = true;
            this.btnSaveSelected.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSaveSelected_Click);
            // 
            // btnSendSelected
            // 
            this.btnSendSelected.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSendSelected.Label = "Send Selected";
            this.btnSendSelected.Name = "btnSendSelected";
            this.btnSendSelected.OfficeImageId = "AttachItem";
            this.btnSendSelected.ShowImage = true;
            this.btnSendSelected.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSendSelected_Click);
            // 
            // GrpLibrary
            // 
            this.GrpLibrary.Items.Add(this.BtnLibrary);
            this.GrpLibrary.Items.Add(this.btnDownloadTemplates);
            this.GrpLibrary.Items.Add(this.btnSaveTemplate);
            this.GrpLibrary.Label = "Insert";
            this.GrpLibrary.Name = "GrpLibrary";
            // 
            // btnHarvey
            // 
            this.btnHarvey.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnHarvey.Image = ((System.Drawing.Image)(resources.GetObject("btnHarvey.Image")));
            this.btnHarvey.Label = "Harvery Ball";
            this.btnHarvey.Name = "btnHarvey";
            this.btnHarvey.ShowImage = true;
            this.btnHarvey.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnHarvey_Click);
            // 
            // BtnLibrary
            // 
            this.BtnLibrary.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.BtnLibrary.Image = ((System.Drawing.Image)(resources.GetObject("BtnLibrary.Image")));
            this.BtnLibrary.Label = "Library";
            this.BtnLibrary.Name = "BtnLibrary";
            this.BtnLibrary.ShowImage = true;
            this.BtnLibrary.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.BtnLibrary_Click);
            // 
            // btnDownloadTemplates
            // 
            this.btnDownloadTemplates.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnDownloadTemplates.Image = ((System.Drawing.Image)(resources.GetObject("btnDownloadTemplates.Image")));
            this.btnDownloadTemplates.Label = "Download Templates";
            this.btnDownloadTemplates.Name = "btnDownloadTemplates";
            this.btnDownloadTemplates.ShowImage = true;
            this.btnDownloadTemplates.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnDownloadTemplates_Click);
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSaveTemplate.Image = global::Harvyball.Properties.Resources.folder_8833321;
            this.btnSaveTemplate.Label = "Save Template";
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.ShowImage = true;
            this.btnSaveTemplate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSaveTemplate_Click);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.GrpTools.ResumeLayout(false);
            this.GrpTools.PerformLayout();
            this.GrpLibrary.ResumeLayout(false);
            this.GrpLibrary.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup GrpTools;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnHarvey;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSaveSelected;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSendSelected;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup GrpLibrary;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnLibrary;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnDownloadTemplates;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSaveTemplate;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
