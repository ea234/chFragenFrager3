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
    public partial class chFFBildanzeige : Form
    {
        public chFFBildanzeige()
        {
            InitializeComponent();
        }

        public void setImage( Image pImage )
        {
            m_imgBild.Image = pImage;
            m_imgBild.Width = pImage.Width;
            m_imgBild.Height = pImage.Height;
        }

        private void chFFBildanzeige_FormClosing( object sender, FormClosingEventArgs e )
        {
            fkIniDatei.writeIniBildPosition(this.Top, this.Left);
        }

        private void chFFBildanzeige_Load( object sender, EventArgs e )
        {

        }

    }
}
