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
            this.btnHarvey = this.Factory.CreateRibbonButton();
            this.btnSaveSelected = this.Factory.CreateRibbonButton();
            this.btnSendSelected = this.Factory.CreateRibbonButton();
            this.GrpLibrary = this.Factory.CreateRibbonGroup();
            this.BtnLibrary = this.Factory.CreateRibbonButton();
            this.btnDownloadTemplates = this.Factory.CreateRibbonButton();
            this.btnSaveTemplate = this.Factory.CreateRibbonButton();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btnPainterSettings = this.Factory.CreateRibbonSplitButton();
            this.toggleButton1 = this.Factory.CreateRibbonToggleButton();
            this.menu1 = this.Factory.CreateRibbonMenu();
            this.tBtnAll = this.Factory.CreateRibbonToggleButton();
            this.tBtnWidth = this.Factory.CreateRibbonToggleButton();
            this.tBtnHeight = this.Factory.CreateRibbonToggleButton();
            this.toggleButton2 = this.Factory.CreateRibbonToggleButton();
            this.tbtnMultiPainter = this.Factory.CreateRibbonToggleButton();
            this.splitButton1 = this.Factory.CreateRibbonSplitButton();
            this.tBTnMatchSlide = this.Factory.CreateRibbonButton();
            this.tbtnMatchFirst = this.Factory.CreateRibbonButton();
            this.tbtnMatchLast = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.GrpTools.SuspendLayout();
            this.GrpLibrary.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.GrpTools);
            this.tab1.Groups.Add(this.GrpLibrary);
            this.tab1.Groups.Add(this.group1);
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
            // btnHarvey
            // 
            this.btnHarvey.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnHarvey.Image = ((System.Drawing.Image)(resources.GetObject("btnHarvey.Image")));
            this.btnHarvey.Label = "Harvery Ball";
            this.btnHarvey.Name = "btnHarvey";
            this.btnHarvey.ShowImage = true;
            this.btnHarvey.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnHarvey_Click);
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
            // group1
            // 
            this.group1.Items.Add(this.btnPainterSettings);
            this.group1.Items.Add(this.tbtnMultiPainter);
            this.group1.Items.Add(this.splitButton1);
            this.group1.Label = "Painter";
            this.group1.Name = "group1";
            // 
            // btnPainterSettings
            // 
            this.btnPainterSettings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnPainterSettings.Items.Add(this.toggleButton1);
            this.btnPainterSettings.Items.Add(this.menu1);
            this.btnPainterSettings.Items.Add(this.toggleButton2);
            this.btnPainterSettings.Label = "Painter Settings";
            this.btnPainterSettings.Name = "btnPainterSettings";
            this.btnPainterSettings.OfficeImageId = "PasteSourceFormatting";
            // 
            // toggleButton1
            // 
            this.toggleButton1.Label = "Paint Size";
            this.toggleButton1.Name = "toggleButton1";
            this.toggleButton1.ShowImage = true;
            this.toggleButton1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButton1_Click);
            // 
            // menu1
            // 
            this.menu1.Image = global::Harvyball.Properties.Resources.icon;
            this.menu1.Items.Add(this.tBtnAll);
            this.menu1.Items.Add(this.tBtnWidth);
            this.menu1.Items.Add(this.tBtnHeight);
            this.menu1.Label = "Image Options";
            this.menu1.Name = "menu1";
            this.menu1.ShowImage = true;
            // 
            // tBtnAll
            // 
            this.tBtnAll.Label = "All";
            this.tBtnAll.Name = "tBtnAll";
            this.tBtnAll.ShowImage = true;
            this.tBtnAll.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.tBtnAll_Click);
            // 
            // tBtnWidth
            // 
            this.tBtnWidth.Label = "Width";
            this.tBtnWidth.Name = "tBtnWidth";
            this.tBtnWidth.ShowImage = true;
            this.tBtnWidth.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.tBtnWidth_Click);
            // 
            // tBtnHeight
            // 
            this.tBtnHeight.Label = "Height";
            this.tBtnHeight.Name = "tBtnHeight";
            this.tBtnHeight.ShowImage = true;
            this.tBtnHeight.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.tBtnHeight_Click);
            // 
            // toggleButton2
            // 
            this.toggleButton2.Label = "Paint Position";
            this.toggleButton2.Name = "toggleButton2";
            this.toggleButton2.ShowImage = true;
            this.toggleButton2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButton2_Click);
            // 
            // tbtnMultiPainter
            // 
            this.tbtnMultiPainter.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.tbtnMultiPainter.Label = "Multi Painter";
            this.tbtnMultiPainter.Name = "tbtnMultiPainter";
            this.tbtnMultiPainter.OfficeImageId = "PasteSourceFormatting";
            this.tbtnMultiPainter.ShowImage = true;
            this.tbtnMultiPainter.SuperTip = resources.GetString("tbtnMultiPainter.SuperTip");
            this.tbtnMultiPainter.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButton5_Click);
            // 
            // splitButton1
            // 
            this.splitButton1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.splitButton1.Items.Add(this.tBTnMatchSlide);
            this.splitButton1.Items.Add(this.tbtnMatchFirst);
            this.splitButton1.Items.Add(this.tbtnMatchLast);
            this.splitButton1.Label = "Match to Slide";
            this.splitButton1.Name = "splitButton1";
            this.splitButton1.OfficeImageId = "PasteSourceFormatting";
            this.splitButton1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.splitButton1_Click);
            // 
            // tBTnMatchSlide
            // 
            this.tBTnMatchSlide.Label = "Match to Slide";
            this.tBTnMatchSlide.Name = "tBTnMatchSlide";
            this.tBTnMatchSlide.ShowImage = true;
            this.tBTnMatchSlide.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.tBTnMatchSlide_Click);
            // 
            // tbtnMatchFirst
            // 
            this.tbtnMatchFirst.Label = "Match to First";
            this.tbtnMatchFirst.Name = "tbtnMatchFirst";
            this.tbtnMatchFirst.ShowImage = true;
            this.tbtnMatchFirst.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.tbtnMatchFirst_Click);
            // 
            // tbtnMatchLast
            // 
            this.tbtnMatchLast.Label = "Match to Last";
            this.tbtnMatchLast.Name = "tbtnMatchLast";
            this.tbtnMatchLast.ShowImage = true;
            this.tbtnMatchLast.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.tbtnMatchLast_Click);
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
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
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
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton btnPainterSettings;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButton1;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu1;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton tBtnAll;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton tBtnWidth;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButton2;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton tbtnMultiPainter;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton splitButton1;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton tBtnHeight;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton tBTnMatchSlide;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton tbtnMatchFirst;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton tbtnMatchLast;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
