namespace WindowsFormsApplication1
{
    partial class chFF3DialogExportDetail
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
            this.m_lblDateiName = new System.Windows.Forms.Label();
            this.m_btnExportStart = new System.Windows.Forms.Button();
            this.m_btnSchliessen = new System.Windows.Forms.Button();
            this.m_lblLoesungsbogen = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_rbLoesungsbogenNein = new System.Windows.Forms.RadioButton();
            this.m_rbLoesungsbogenJa = new System.Windows.Forms.RadioButton();
            this.m_rbExportAntwortKorrektNein = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_rbExportAntwortKorrektJa = new System.Windows.Forms.RadioButton();
            this.m_lblExportAntwortenKorrekt = new System.Windows.Forms.Label();
            this.m_rbExportAntwortFalschNein = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_rbExportAntwortFalschJa = new System.Windows.Forms.RadioButton();
            this.m_lblExportAntwortenFalsch = new System.Windows.Forms.Label();
            this.m_btnDateiAuswahl = new System.Windows.Forms.Button();
            this.m_txtExportDateiname = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblDateiName
            // 
            this.m_lblDateiName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblDateiName.Location = new System.Drawing.Point(12, 9);
            this.m_lblDateiName.Name = "m_lblDateiName";
            this.m_lblDateiName.Size = new System.Drawing.Size(106, 29);
            this.m_lblDateiName.TabIndex = 0;
            this.m_lblDateiName.Text = "Dateiname";
            // 
            // m_btnExportStart
            // 
            this.m_btnExportStart.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnExportStart.Location = new System.Drawing.Point(15, 179);
            this.m_btnExportStart.Name = "m_btnExportStart";
            this.m_btnExportStart.Size = new System.Drawing.Size(187, 45);
            this.m_btnExportStart.TabIndex = 2;
            this.m_btnExportStart.Text = "Start Export";
            this.m_btnExportStart.UseVisualStyleBackColor = true;
            // 
            // m_btnSchliessen
            // 
            this.m_btnSchliessen.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnSchliessen.Location = new System.Drawing.Point(15, 230);
            this.m_btnSchliessen.Name = "m_btnSchliessen";
            this.m_btnSchliessen.Size = new System.Drawing.Size(187, 45);
            this.m_btnSchliessen.TabIndex = 3;
            this.m_btnSchliessen.Text = "Schliessen";
            this.m_btnSchliessen.UseVisualStyleBackColor = true;
            this.m_btnSchliessen.Click += new System.EventHandler(this.m_btnSchliessen_Click);
            // 
            // m_lblLoesungsbogen
            // 
            this.m_lblLoesungsbogen.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblLoesungsbogen.Location = new System.Drawing.Point(12, 147);
            this.m_lblLoesungsbogen.Name = "m_lblLoesungsbogen";
            this.m_lblLoesungsbogen.Size = new System.Drawing.Size(155, 29);
            this.m_lblLoesungsbogen.TabIndex = 4;
            this.m_lblLoesungsbogen.Text = "Lösungsbogen";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_rbLoesungsbogenNein);
            this.panel1.Controls.Add(this.m_rbLoesungsbogenJa);
            this.panel1.Location = new System.Drawing.Point(156, 142);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 29);
            this.panel1.TabIndex = 5;
            // 
            // m_rbLoesungsbogenNein
            // 
            this.m_rbLoesungsbogenNein.AutoSize = true;
            this.m_rbLoesungsbogenNein.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_rbLoesungsbogenNein.Location = new System.Drawing.Point(61, 3);
            this.m_rbLoesungsbogenNein.Name = "m_rbLoesungsbogenNein";
            this.m_rbLoesungsbogenNein.Size = new System.Drawing.Size(55, 21);
            this.m_rbLoesungsbogenNein.TabIndex = 1;
            this.m_rbLoesungsbogenNein.TabStop = true;
            this.m_rbLoesungsbogenNein.Text = "Nein";
            this.m_rbLoesungsbogenNein.UseVisualStyleBackColor = true;
            // 
            // m_rbLoesungsbogenJa
            // 
            this.m_rbLoesungsbogenJa.AutoSize = true;
            this.m_rbLoesungsbogenJa.Checked = true;
            this.m_rbLoesungsbogenJa.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_rbLoesungsbogenJa.Location = new System.Drawing.Point(3, 3);
            this.m_rbLoesungsbogenJa.Name = "m_rbLoesungsbogenJa";
            this.m_rbLoesungsbogenJa.Size = new System.Drawing.Size(41, 21);
            this.m_rbLoesungsbogenJa.TabIndex = 0;
            this.m_rbLoesungsbogenJa.TabStop = true;
            this.m_rbLoesungsbogenJa.Text = "Ja";
            this.m_rbLoesungsbogenJa.UseVisualStyleBackColor = true;
            // 
            // m_rbExportAntwortKorrektNein
            // 
            this.m_rbExportAntwortKorrektNein.AutoSize = true;
            this.m_rbExportAntwortKorrektNein.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_rbExportAntwortKorrektNein.Location = new System.Drawing.Point(61, 3);
            this.m_rbExportAntwortKorrektNein.Name = "m_rbExportAntwortKorrektNein";
            this.m_rbExportAntwortKorrektNein.Size = new System.Drawing.Size(55, 21);
            this.m_rbExportAntwortKorrektNein.TabIndex = 1;
            this.m_rbExportAntwortKorrektNein.TabStop = true;
            this.m_rbExportAntwortKorrektNein.Text = "Nein";
            this.m_rbExportAntwortKorrektNein.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_rbExportAntwortKorrektNein);
            this.panel2.Controls.Add(this.m_rbExportAntwortKorrektJa);
            this.panel2.Location = new System.Drawing.Point(156, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(136, 29);
            this.panel2.TabIndex = 7;
            // 
            // m_rbExportAntwortKorrektJa
            // 
            this.m_rbExportAntwortKorrektJa.AutoSize = true;
            this.m_rbExportAntwortKorrektJa.Checked = true;
            this.m_rbExportAntwortKorrektJa.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_rbExportAntwortKorrektJa.Location = new System.Drawing.Point(3, 3);
            this.m_rbExportAntwortKorrektJa.Name = "m_rbExportAntwortKorrektJa";
            this.m_rbExportAntwortKorrektJa.Size = new System.Drawing.Size(41, 21);
            this.m_rbExportAntwortKorrektJa.TabIndex = 0;
            this.m_rbExportAntwortKorrektJa.TabStop = true;
            this.m_rbExportAntwortKorrektJa.Text = "Ja";
            this.m_rbExportAntwortKorrektJa.UseVisualStyleBackColor = true;
            // 
            // m_lblExportAntwortenKorrekt
            // 
            this.m_lblExportAntwortenKorrekt.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblExportAntwortenKorrekt.Location = new System.Drawing.Point(12, 69);
            this.m_lblExportAntwortenKorrekt.Name = "m_lblExportAntwortenKorrekt";
            this.m_lblExportAntwortenKorrekt.Size = new System.Drawing.Size(155, 29);
            this.m_lblExportAntwortenKorrekt.TabIndex = 6;
            this.m_lblExportAntwortenKorrekt.Text = "korrekte Antworten";
            // 
            // m_rbExportAntwortFalschNein
            // 
            this.m_rbExportAntwortFalschNein.AutoSize = true;
            this.m_rbExportAntwortFalschNein.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_rbExportAntwortFalschNein.Location = new System.Drawing.Point(61, 3);
            this.m_rbExportAntwortFalschNein.Name = "m_rbExportAntwortFalschNein";
            this.m_rbExportAntwortFalschNein.Size = new System.Drawing.Size(55, 21);
            this.m_rbExportAntwortFalschNein.TabIndex = 1;
            this.m_rbExportAntwortFalschNein.TabStop = true;
            this.m_rbExportAntwortFalschNein.Text = "Nein";
            this.m_rbExportAntwortFalschNein.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_rbExportAntwortFalschNein);
            this.panel3.Controls.Add(this.m_rbExportAntwortFalschJa);
            this.panel3.Location = new System.Drawing.Point(156, 101);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(136, 29);
            this.panel3.TabIndex = 9;
            // 
            // m_rbExportAntwortFalschJa
            // 
            this.m_rbExportAntwortFalschJa.AutoSize = true;
            this.m_rbExportAntwortFalschJa.Checked = true;
            this.m_rbExportAntwortFalschJa.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_rbExportAntwortFalschJa.Location = new System.Drawing.Point(3, 3);
            this.m_rbExportAntwortFalschJa.Name = "m_rbExportAntwortFalschJa";
            this.m_rbExportAntwortFalschJa.Size = new System.Drawing.Size(41, 21);
            this.m_rbExportAntwortFalschJa.TabIndex = 0;
            this.m_rbExportAntwortFalschJa.TabStop = true;
            this.m_rbExportAntwortFalschJa.Text = "Ja";
            this.m_rbExportAntwortFalschJa.UseVisualStyleBackColor = true;
            // 
            // m_lblExportAntwortenFalsch
            // 
            this.m_lblExportAntwortenFalsch.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblExportAntwortenFalsch.Location = new System.Drawing.Point(12, 106);
            this.m_lblExportAntwortenFalsch.Name = "m_lblExportAntwortenFalsch";
            this.m_lblExportAntwortenFalsch.Size = new System.Drawing.Size(155, 29);
            this.m_lblExportAntwortenFalsch.TabIndex = 8;
            this.m_lblExportAntwortenFalsch.Text = "falsche Antworten";
            // 
            // m_btnDateiAuswahl
            // 
            this.m_btnDateiAuswahl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnDateiAuswahl.Location = new System.Drawing.Point(156, 2);
            this.m_btnDateiAuswahl.Name = "m_btnDateiAuswahl";
            this.m_btnDateiAuswahl.Size = new System.Drawing.Size(42, 30);
            this.m_btnDateiAuswahl.TabIndex = 10;
            this.m_btnDateiAuswahl.Text = "D";
            this.m_btnDateiAuswahl.UseVisualStyleBackColor = true;
            this.m_btnDateiAuswahl.Click += new System.EventHandler(this.m_btnDateiAuswahl_Click);
            // 
            // m_txtExportDateiname
            // 
            this.m_txtExportDateiname.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtExportDateiname.Location = new System.Drawing.Point(204, 4);
            this.m_txtExportDateiname.MaxLength = 500;
            this.m_txtExportDateiname.Name = "m_txtExportDateiname";
            this.m_txtExportDateiname.Size = new System.Drawing.Size(448, 25);
            this.m_txtExportDateiname.TabIndex = 11;
            this.m_txtExportDateiname.Text = "m_txtExportDateiname";
            // 
            // chFF3DialogExportDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 287);
            this.Controls.Add(this.m_txtExportDateiname);
            this.Controls.Add(this.m_btnDateiAuswahl);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.m_lblExportAntwortenFalsch);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.m_lblExportAntwortenKorrekt);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_lblLoesungsbogen);
            this.Controls.Add(this.m_btnSchliessen);
            this.Controls.Add(this.m_btnExportStart);
            this.Controls.Add(this.m_lblDateiName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "chFF3DialogExportDetail";
            this.Text = "Export Fragebogen";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblDateiName;
        private System.Windows.Forms.Button m_btnExportStart;
        private System.Windows.Forms.Button m_btnSchliessen;
        private System.Windows.Forms.Label m_lblLoesungsbogen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton m_rbLoesungsbogenNein;
        private System.Windows.Forms.RadioButton m_rbLoesungsbogenJa;
        private System.Windows.Forms.RadioButton m_rbExportAntwortKorrektNein;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton m_rbExportAntwortKorrektJa;
        private System.Windows.Forms.Label m_lblExportAntwortenKorrekt;
        private System.Windows.Forms.RadioButton m_rbExportAntwortFalschNein;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton m_rbExportAntwortFalschJa;
        private System.Windows.Forms.Label m_lblExportAntwortenFalsch;
        private System.Windows.Forms.Button m_btnDateiAuswahl;
        private System.Windows.Forms.TextBox m_txtExportDateiname;
    }
}