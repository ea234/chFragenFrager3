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
    public partial class chFFAuswahlFragen : Form
    {
        private bool m_knz_ok = false;


        public chFFAuswahlFragen()
        {
            InitializeComponent();
        }

        /*
         * ################################################################################
         */
        private void m_btnSchliessen_Click( object sender, EventArgs e )
        {
            m_knz_ok = false;

            this.Hide();
        }

        private void chFFAuswahlFragen_Load( object sender, EventArgs e )
        {

        }

        /*
         * ################################################################################
         */
        public void setMaxAnzahlFragen( int pAnzahlFragen )
        {
            m_scbrAbFragenNr.Maximum = ( pAnzahlFragen < 0 ? 100 : pAnzahlFragen );
        }

        /*
         * ################################################################################
         */
        public void setDialogNummerAb( String pNummerAb )
        {
            if ( fkZahl.checkInteger( pNummerAb, 10 ) ) 
            {
                m_txtAbFragenNr.Text = pNummerAb;
            }
            else
            {
                m_txtAbFragenNr.Text = "1";
            }
        }

        /*
         * ################################################################################
         */
        public void setDialogAnzahlFragen( String pAnzahlFragen )
        {
            int int_zahl = fkZahl.getInteger( pAnzahlFragen, 40 );

            if ( int_zahl == 20 )
            {
                m_rbAnzahl20.Checked = true;
            }
            else if ( int_zahl == 40 )
            {
                m_rbAnzahl40.Checked = true;
            }
            else if ( int_zahl == 60 )
            {
                m_rbAnzahl60.Checked = true;
            }
            else if ( int_zahl == 80 )
            {
                m_rbAnzahl80.Checked = true;
            }
            else
            {
                m_txtAnzahlFragen.Text = "" + int_zahl;
                m_AnzahlAusTextBox.Checked = true;
            }
        }

        /* 
         * ################################################################################
         */
        public  int getDialogNummerAb() 
        {
            return fkZahl.getInteger( m_txtAbFragenNr.Text, 1 );
        }

        /* 
         * ################################################################################
         */
        private void m_scbrAbFragenNr_Scroll( object sender, ScrollEventArgs e )
        {
            m_txtAbFragenNr.Text = "" + m_scbrAbFragenNr.Value;
        }

        /* 
         * ################################################################################
         */
        private void m_btnOK_Click( object sender, EventArgs e )
        {
            m_knz_ok = true;

            this.Hide();
        }

        /* 
         * ################################################################################
         */
        public bool isOK()
        {
            return m_knz_ok;
        }

        /* 
         * ################################################################################
         */
        public int getAnzahlFragen()
        {
            if ( m_rbAnzahl20.Checked ) 
            {
                return 20;               
            }

            if ( m_rbAnzahl40.Checked ) 
            {
                return 40;              
            }

            if ( m_rbAnzahl60.Checked ) 
            {
                return 60;             
            }

            if ( m_rbAnzahl80.Checked ) 
            {
                return 80;             
            }

            if ( m_AnzahlAusTextBox.Checked ) 
            {
                if ( fkZahl.checkInteger( m_txtAnzahlFragen.Text, 10 ) )
                {
                    return fkZahl.getInteger( m_txtAnzahlFragen.Text, 1 ) ;  
                }
            }

            return -1;
        }
    }
}
