namespace WindowsFormsApplication1
{
    partial class chFFAuswahlFragen
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_txtAbFragenNr = new System.Windows.Forms.TextBox();
            this.m_scbrAbFragenNr = new System.Windows.Forms.HScrollBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_AnzahlAusTextBox = new System.Windows.Forms.RadioButton();
            this.m_rbAnzahl80 = new System.Windows.Forms.RadioButton();
            this.m_rbAnzahl60 = new System.Windows.Forms.RadioButton();
            this.m_rbAnzahl40 = new System.Windows.Forms.RadioButton();
            this.m_rbAnzahl20 = new System.Windows.Forms.RadioButton();
            this.m_lblAnzahlFragen = new System.Windows.Forms.Label();
            this.m_lblAb = new System.Windows.Forms.Label();
            this.m_btnAbbrehen = new System.Windows.Forms.Button();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_txtAnzahlFragen = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtAbFragenNr
            // 
            this.m_txtAbFragenNr.Font = new System.Drawing.Font( "Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this.m_txtAbFragenNr.Location = new System.Drawing.Point( 60, 21 );
            this.m_txtAbFragenNr.MaxLength = 10;
            this.m_txtAbFragenNr.Name = "m_txtAbFragenNr";
            this.m_txtAbFragenNr.Size = new System.Drawing.Size( 100, 25 );
            this.m_txtAbFragenNr.TabIndex = 1;
            // 
            // m_scbrAbFragenNr
            // 
            this.m_scbrAbFragenNr.Location = new System.Drawing.Point( 207, 22 );
            this.m_scbrAbFragenNr.Name = "m_scbrAbFragenNr";
            this.m_scbrAbFragenNr.Size = new System.Drawing.Size( 330, 25 );
            this.m_scbrAbFragenNr.TabIndex = 2;
            this.m_scbrAbFragenNr.Scroll += new System.Windows.Forms.ScrollEventHandler( this.m_scbrAbFragenNr_Scroll );
            // 
            // panel3
            // 
            this.panel3.Controls.Add( this.m_AnzahlAusTextBox );
            this.panel3.Controls.Add( this.m_rbAnzahl80 );
            this.panel3.Controls.Add( this.m_rbAnzahl60 );
            this.panel3.Controls.Add( this.m_rbAnzahl40 );
            this.panel3.Controls.Add( this.m_rbAnzahl20 );
            this.panel3.Location = new System.Drawing.Point( 157, 69 );
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size( 318, 29 );
            this.panel3.TabIndex = 15;
            // 
            // m_AnzahlAusTextBox
            // 
            this.m_AnzahlAusTextBox.AutoSize = true;
            this.m_AnzahlAusTextBox.Font = new System.Drawing.Font( "Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this.m_AnzahlAusTextBox.Location = new System.Drawing.Point( 232, 4 );
            this.m_AnzahlAusTextBox.Name = "m_AnzahlAusTextBox";
            this.m_AnzahlAusTextBox.Size = new System.Drawing.Size( 79, 21 );
            this.m_AnzahlAusTextBox.TabIndex = 4;
            this.m_AnzahlAusTextBox.Text = "Vorgabe";
            this.m_AnzahlAusTextBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.m_AnzahlAusTextBox.UseVisualStyleBackColor = true;
            // 
            // m_rbAnzahl80
            // 
            this.m_rbAnzahl80.AutoSize = true;
            this.m_rbAnzahl80.Font = new System.Drawing.Font( "Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this.m_rbAnzahl80.Location = new System.Drawing.Point( 175, 4 );
            this.m_rbAnzahl80.Name = "m_rbAnzahl80";
            this.m_rbAnzahl80.Size = new System.Drawing.Size( 42, 21 );
            this.m_rbAnzahl80.TabIndex = 3;
            this.m_rbAnzahl80.Text = "80";
            this.m_rbAnzahl80.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.m_rbAnzahl80.UseVisualStyleBackColor = true;
            // 
            // m_rbAnzahl60
            // 
            this.m_rbAnzahl60.AutoSize = true;
            this.m_rbAnzahl60.Font = new System.Drawing.Font( "Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this.m_rbAnzahl60.Location = new System.Drawing.Point( 118, 4 );
            this.m_rbAnzahl60.Name = "m_rbAnzahl60";
            this.m_rbAnzahl60.Size = new System.Drawing.Size( 42, 21 );
            this.m_rbAnzahl60.TabIndex = 2;
            this.m_rbAnzahl60.Text = "60";
            this.m_rbAnzahl60.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.m_rbAnzahl60.UseVisualStyleBackColor = true;
            // 
            // m_rbAnzahl40
            // 
            this.m_rbAnzahl40.AutoSize = true;
            this.m_rbAnzahl40.Font = new System.Drawing.Font( "Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this.m_rbAnzahl40.Location = new System.Drawing.Point( 60, 4 );
            this.m_rbAnzahl40.Name = "m_rbAnzahl40";
            this.m_rbAnzahl40.Size = new System.Drawing.Size( 42, 21 );
            this.m_rbAnzahl40.TabIndex = 1;
            this.m_rbAnzahl40.Text = "40";
            this.m_rbAnzahl40.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.m_rbAnzahl40.UseVisualStyleBackColor = true;
            // 
            // m_rbAnzahl20
            // 
            this.m_rbAnzahl20.AutoSize = true;
            this.m_rbAnzahl20.Checked = true;
            this.m_rbAnzahl20.Font = new System.Drawing.Font( "Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this.m_rbAnzahl20.Location = new System.Drawing.Point( 3, 4 );
            this.m_rbAnzahl20.Name = "m_rbAnzahl20";
            this.m_rbAnzahl20.Size = new System.Drawing.Size( 42, 21 );
            this.m_rbAnzahl20.TabIndex = 0;
            this.m_rbAnzahl20.TabStop = true;
            this.m_rbAnzahl20.Text = "20";
            this.m_rbAnzahl20.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.m_rbAnzahl20.UseVisualStyleBackColor = true;
            // 
            // m_lblAnzahlFragen
            // 
            this.m_lblAnzahlFragen.Font = new System.Drawing.Font( "Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this.m_lblAnzahlFragen.Location = new System.Drawing.Point( 13, 74 );
            this.m_lblAnzahlFragen.Name = "m_lblAnzahlFragen";
            this.m_lblAnzahlFragen.Size = new System.Drawing.Size( 155, 29 );
            this.m_lblAnzahlFragen.TabIndex = 14;
            this.m_lblAnzahlFragen.Text = "Anzahl Fragen";
            // 
            // m_lblAb
            // 
            this.m_lblAb.Font = new System.Drawing.Font( "Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this.m_lblAb.Location = new System.Drawing.Point( 12, 21 );
            this.m_lblAb.Name = "m_lblAb";
            this.m_lblAb.Size = new System.Drawing.Size( 34, 29 );
            this.m_lblAb.TabIndex = 12;
            this.m_lblAb.Text = "Ab";
            // 
            // m_btnAbbrehen
            // 
            this.m_btnAbbrehen.Font = new System.Drawing.Font( "Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this.m_btnAbbrehen.Location = new System.Drawing.Point( 217, 127 );
            this.m_btnAbbrehen.Name = "m_btnAbbrehen";
            this.m_btnAbbrehen.Size = new System.Drawing.Size( 187, 45 );
            this.m_btnAbbrehen.TabIndex = 11;
            this.m_btnAbbrehen.Text = "Abbrechen";
            this.m_btnAbbrehen.UseVisualStyleBackColor = true;
            this.m_btnAbbrehen.Click += new System.EventHandler( this.m_btnSchliessen_Click );
            // 
            // m_btnOK
            // 
            this.m_btnOK.Font = new System.Drawing.Font( "Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this.m_btnOK.Location = new System.Drawing.Point( 16, 127 );
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size( 187, 45 );
            this.m_btnOK.TabIndex = 16;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler( this.m_btnOK_Click );
            // 
            // m_txtAnzahlFragen
            // 
            this.m_txtAnzahlFragen.Font = new System.Drawing.Font( "Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this.m_txtAnzahlFragen.Location = new System.Drawing.Point( 490, 73 );
            this.m_txtAnzahlFragen.MaxLength = 10;
            this.m_txtAnzahlFragen.Name = "m_txtAnzahlFragen";
            this.m_txtAnzahlFragen.Size = new System.Drawing.Size( 100, 25 );
            this.m_txtAnzahlFragen.TabIndex = 17;
            // 
            // chFFAuswahlFragen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 676, 187 );
            this.Controls.Add( this.m_txtAnzahlFragen );
            this.Controls.Add( this.m_btnOK );
            this.Controls.Add( this.panel3 );
            this.Controls.Add( this.m_lblAnzahlFragen );
            this.Controls.Add( this.m_lblAb );
            this.Controls.Add( this.m_btnAbbrehen );
            this.Controls.Add( this.m_scbrAbFragenNr );
            this.Controls.Add( this.m_txtAbFragenNr );
            this.Name = "chFFAuswahlFragen";
            this.Text = "chFFAuswahlFragen";
            this.Load += new System.EventHandler( this.chFFAuswahlFragen_Load );
            this.panel3.ResumeLayout( false );
            this.panel3.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtAbFragenNr;
        private System.Windows.Forms.HScrollBar m_scbrAbFragenNr;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton m_rbAnzahl20;
        private System.Windows.Forms.Label m_lblAnzahlFragen;
        private System.Windows.Forms.Label m_lblAb;
        private System.Windows.Forms.Button m_btnAbbrehen;
        private System.Windows.Forms.RadioButton m_rbAnzahl80;
        private System.Windows.Forms.RadioButton m_rbAnzahl60;
        private System.Windows.Forms.RadioButton m_rbAnzahl40;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.RadioButton m_AnzahlAusTextBox;
        private System.Windows.Forms.TextBox m_txtAnzahlFragen;
    }
}