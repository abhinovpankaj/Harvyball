namespace Harvyball
{
    partial class frm_HB
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
            this.txt_percent = new System.Windows.Forms.NumericUpDown();
            this.btnPickColor = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txt_percent)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_percent
            // 
            this.txt_percent.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_percent.Location = new System.Drawing.Point(39, 15);
            this.txt_percent.Name = "txt_percent";
            this.txt_percent.Size = new System.Drawing.Size(64, 24);
            this.txt_percent.TabIndex = 7;
            this.txt_percent.ValueChanged += new System.EventHandler(this.txt_percent_ValueChanged);
            // 
            // btnPickColor
            // 
            this.btnPickColor.BackColor = System.Drawing.Color.Brown;
            this.btnPickColor.Location = new System.Drawing.Point(109, 12);
            this.btnPickColor.Name = "btnPickColor";
            this.btnPickColor.Size = new System.Drawing.Size(39, 30);
            this.btnPickColor.TabIndex = 6;
            this.btnPickColor.UseVisualStyleBackColor = false;
            this.btnPickColor.Click += new System.EventHandler(this.btnPickColor_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(13, 18);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(20, 17);
            this.Label1.TabIndex = 5;
            this.Label1.Text = "%";
            // 
            // frm_HB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(165, 55);
            this.Controls.Add(this.txt_percent);
            this.Controls.Add(this.btnPickColor);
            this.Controls.Add(this.Label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_HB";
            this.Text = "HB Form";
            this.Load += new System.EventHandler(this.frm_HB_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.txt_percent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.NumericUpDown txt_percent;
        internal System.Windows.Forms.Button btnPickColor;
        internal System.Windows.Forms.Label Label1;
    }
}