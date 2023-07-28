using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;

namespace Harvyball
{
    public partial class Frm_SaveSlides : Form
    {
        public Frm_SaveSlides()
        {
            InitializeComponent();
        }

        private void cmd_Save_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void cmd_Cancel_Click(object sender, EventArgs e)
        {
            txt_attachment.Text = "";
            Dispose();
        }
        private void frm_SaveSlides_Activated(object sender, EventArgs e)
        {
            txt_attachment.Text = mod_SaveSendSlides.proposed_name;
            opt_selected.Checked = true;
            chk_as_pdf.Checked = false;
        }

        private void opt_all_CheckedChanged(object sender, EventArgs e)
        {
            string strPresentation = Globals.ThisAddIn.Application.ActivePresentation.Name;
            int IntPosition = Strings.InStrRev(strPresentation, ".") - 1;
            if (opt_all.Checked == true)
            {
                txt_attachment.Text = strPresentation.Substring(1, IntPosition);
            }
        }
    }
}
