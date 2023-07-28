namespace Harvyball
{
    partial class Frm_SaveSlides
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmd_Cancel = new System.Windows.Forms.Button();
            this.cmd_Save = new System.Windows.Forms.Button();
            this.chk_as_pdf = new System.Windows.Forms.CheckBox();
            this.opt_all = new System.Windows.Forms.RadioButton();
            this.opt_selected = new System.Windows.Forms.RadioButton();
            this.Label2 = new System.Windows.Forms.Label();
            this.txt_attachment = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmd_Cancel
            // 
            this.cmd_Cancel.Location = new System.Drawing.Point(487, 105);
            this.cmd_Cancel.Name = "cmd_Cancel";
            this.cmd_Cancel.Size = new System.Drawing.Size(105, 41);
            this.cmd_Cancel.TabIndex = 15;
            this.cmd_Cancel.Text = "Cancel";
            this.cmd_Cancel.UseVisualStyleBackColor = true;
            this.cmd_Cancel.Click += new System.EventHandler(this.cmd_Cancel_Click);
            // 
            // cmd_Save
            // 
            this.cmd_Save.Location = new System.Drawing.Point(376, 105);
            this.cmd_Save.Name = "cmd_Save";
            this.cmd_Save.Size = new System.Drawing.Size(105, 41);
            this.cmd_Save.TabIndex = 14;
            this.cmd_Save.Text = "Save";
            this.cmd_Save.UseVisualStyleBackColor = true;
            this.cmd_Save.Click += new System.EventHandler(this.cmd_Save_Click);
            // 
            // chk_as_pdf
            // 
            this.chk_as_pdf.AutoSize = true;
            this.chk_as_pdf.Location = new System.Drawing.Point(506, 56);
            this.chk_as_pdf.Name = "chk_as_pdf";
            this.chk_as_pdf.Size = new System.Drawing.Size(90, 17);
            this.chk_as_pdf.TabIndex = 13;
            this.chk_as_pdf.Text = "Save As PDF";
            this.chk_as_pdf.UseVisualStyleBackColor = true;
            // 
            // opt_all
            // 
            this.opt_all.AutoSize = true;
            this.opt_all.Location = new System.Drawing.Point(181, 58);
            this.opt_all.Name = "opt_all";
            this.opt_all.Size = new System.Drawing.Size(67, 17);
            this.opt_all.TabIndex = 12;
            this.opt_all.TabStop = true;
            this.opt_all.Text = "All Slides";
            this.opt_all.UseVisualStyleBackColor = true;
            this.opt_all.CheckedChanged += new System.EventHandler(this.opt_all_CheckedChanged);
            // 
            // opt_selected
            // 
            this.opt_selected.AutoSize = true;
            this.opt_selected.Location = new System.Drawing.Point(66, 58);
            this.opt_selected.Name = "opt_selected";
            this.opt_selected.Size = new System.Drawing.Size(98, 17);
            this.opt_selected.TabIndex = 11;
            this.opt_selected.TabStop = true;
            this.opt_selected.Text = "Selected Slides";
            this.opt_selected.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(14, 60);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(38, 13);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "Save :";
            // 
            // txt_attachment
            // 
            this.txt_attachment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_attachment.Location = new System.Drawing.Point(12, 24);
            this.txt_attachment.Name = "txt_attachment";
            this.txt_attachment.Size = new System.Drawing.Size(584, 26);
            this.txt_attachment.TabIndex = 9;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 8);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(69, 13);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "Save Name :";
            // 
            // Frm_SaveSlides
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 163);
            this.Controls.Add(this.cmd_Cancel);
            this.Controls.Add(this.cmd_Save);
            this.Controls.Add(this.chk_as_pdf);
            this.Controls.Add(this.opt_all);
            this.Controls.Add(this.opt_selected);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txt_attachment);
            this.Controls.Add(this.Label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_SaveSlides";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Save Slides";
            this.Load += new System.EventHandler(this.frm_SaveSlides_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button cmd_Cancel;
        internal System.Windows.Forms.Button cmd_Save;
        internal System.Windows.Forms.CheckBox chk_as_pdf;
        internal System.Windows.Forms.RadioButton opt_all;
        internal System.Windows.Forms.RadioButton opt_selected;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txt_attachment;
        internal System.Windows.Forms.Label Label1;
    }
}