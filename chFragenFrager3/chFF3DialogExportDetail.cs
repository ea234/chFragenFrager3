using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class chFF3DialogExportDetail : Form
    {
        public chFF3DialogExportDetail()
        {
            InitializeComponent();
        }

        private void m_btnSchliessen_Click( object sender, EventArgs e )
        {
            this.Hide();
        }

        private void m_btnDateiAuswahl_Click( object sender, EventArgs e )
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Multiselect = false,
                InitialDirectory = "c:\\",
                Title = "Exportdatei wählen"
            };

             if ( ofd.ShowDialog() == DialogResult.OK ) 
             {
                 m_txtExportDateiname.Text = ofd.FileName;
             
             }

             ofd = null;

        }
    }
}
