namespace WindowsFormsApplication1
{
    partial class chFFBildanzeige
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_imgBild = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_imgBild)).BeginInit();
            this.SuspendLayout();
            // 
            // m_imgBild
            // 
            this.m_imgBild.Location = new System.Drawing.Point(1, 1);
            this.m_imgBild.Name = "m_imgBild";
            this.m_imgBild.Size = new System.Drawing.Size(548, 209);
            this.m_imgBild.TabIndex = 0;
            this.m_imgBild.TabStop = false;
            // 
            // chFFBildanzeige
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 318);
            this.Controls.Add(this.m_imgBild);
            this.Name = "chFFBildanzeige";
            this.Text = "chFFBildanzeige";
            this.Load += new System.EventHandler(this.chFFBildanzeige_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.chFFBildanzeige_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.m_imgBild)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox m_imgBild;
    }
}