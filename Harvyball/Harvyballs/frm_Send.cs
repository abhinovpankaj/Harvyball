using System;
using System.Windows.Forms;

namespace Harvyball
{
    public partial class frm_Send : Form
    {
        public frm_Send()
        {
            InitializeComponent();
        }

        private void cmd_Cancel_Click(object sender, EventArgs e)
        {
            txt_attachment.Text = "";
            Close();
        }
        private void frm_Send_Activated(object sender, EventArgs e)
        {
            txt_attachment.Text = mod_SaveSendSlides.proposed_name;
            opt_selected.Checked = true;
            chk_as_pdf.Checked = false;
        }
        private void cmd_Send_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void opt_all_Checked(object sender, EventArgs e)
        {
            string strPresentation = Globals.ThisAddIn.Application.ActivePresentation.Name.ToString();
            int IntPosition = strPresentation.LastIndexOf(".") - 1; //InStrRev(strPresentation, ".") - 1

            if (opt_all.Checked == true)
            {
                txt_attachment.Text = strPresentation.Substring(1, IntPosition);
            }
        }

        private void cmd_Cancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void cmd_Save_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
