using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class clsTypAbfrage
    {
        private int m_index_fragen_katalog       = 0;
        private int m_anzahl_beantwortet_ja      = 0;
        private int m_anzahl_beantwortet_nein    = 0;
        private int m_anzahl_falsch_beantwortet  = 0;
        private int m_anzahl_korrekt_beantwortet = 0;

        private bool m_knz_antwort_a_gewaehlt = false; 
        private bool m_knz_antwort_b_gewaehlt = false; 
        private bool m_knz_antwort_c_gewaehlt = false; 
        private bool m_knz_antwort_d_gewaehlt = false; 
        private bool m_knz_antwort_e_gewaehlt = false; 
        private bool m_knz_antwort_f_gewaehlt = false; 
        private bool m_knz_antwort_g_gewaehlt = false; 
        private bool m_knz_antwort_h_gewaehlt = false; 

        private bool m_knz_frage_letzte_antwort_korrekt = false;

        /** 
         * @return den Index der Frage im Fragenkatalog
         */
        public int getIndexFragenKatalog()
        {
            return m_index_fragen_katalog;
        }

        /** 
         * @param pIndexFragenKatalog der Index der Frage im Fragenkatalog
         */
        public void setIndexFragenKatalog( int pIndexFragenKatalog )
        {
            m_index_fragen_katalog = pIndexFragenKatalog;
        }

        /**
         * @return den Wert, wie oft diese Frage beantwortet worden ist
         */
        public int getAnzahlBeantwortetJa()
        {
            return m_anzahl_beantwortet_ja;
        }

        /* 
         * ################################################################################
         */
        public void incAnzahlBeantwortetJa()
        {
            m_anzahl_beantwortet_ja++;
        }

        /* 
         * ################################################################################
         */
        public int getAnzahlBeantwortetNein()
        {
            return m_anzahl_beantwortet_nein;
        }

        /* 
         * ################################################################################
         */
        public void incAnzahlBeantwortetNein()
        {
            m_anzahl_beantwortet_nein++;
        }

        /* 
         * ################################################################################
         */
        public int getAnzahlFalschBeantwortet()
        {
            return m_anzahl_falsch_beantwortet;
        }

        /* 
         * ################################################################################
         */
        public void incAnzahlFalschBeantwortet()
        {
            m_anzahl_falsch_beantwortet++;
        }

        /* 
         * ################################################################################
         */
        public int getAnzahlKorrektBeantwortet()
        {
            return m_anzahl_korrekt_beantwortet;
        }

        /* 
         * ################################################################################
         */
        public void incAnzahlKorrektBeantwortet()
        {
            m_anzahl_korrekt_beantwortet++;
        }

        /* 
         * ################################################################################
         */
        public bool getKnzAntwortAGewaehlt()
        {
            return m_knz_antwort_a_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public void setKnzAntwortAGewaehlt(bool pKnzAntwortA_gewaehlt)
        {
            m_knz_antwort_a_gewaehlt = pKnzAntwortA_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public bool getKnzAntwortBGewaehlt()
        {
            return m_knz_antwort_b_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public void setKnzAntwortBGewaehlt( bool pKnzAntwortB_gewaehlt )
        {
            m_knz_antwort_b_gewaehlt = pKnzAntwortB_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public bool getKnzAntwortCGewaehlt()
        {
            return m_knz_antwort_c_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public void setKnzAntwortCGewaehlt( bool pKnzAntwortC_gewaehlt )
        {
            m_knz_antwort_c_gewaehlt = pKnzAntwortC_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public bool getKnzAntwortDGewaehlt()
        {
            return m_knz_antwort_d_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public void setKnzAntwortDGewaehlt( bool pKnzAntwortD_gewaehlt )
        {
            m_knz_antwort_d_gewaehlt = pKnzAntwortD_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public bool getKnzAntwortEGewaehlt()
        {
            return m_knz_antwort_e_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public void setKnzAntwortEGewaehlt( bool pKnzAntwortE_gewaehlt )
        {
            m_knz_antwort_e_gewaehlt = pKnzAntwortE_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public bool getKnzAntwortFGewaehlt()
        {
            return m_knz_antwort_f_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public void setKnzAntwortFGewaehlt( bool pKnzAntwortF_gewaehlt )
        {
            m_knz_antwort_f_gewaehlt = pKnzAntwortF_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public bool getKnzAntwortGGewaehlt()
        {
            return m_knz_antwort_g_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public void setKnzAntwortGGewaehlt( bool pKnzAntwortG_gewaehlt )
        {
            m_knz_antwort_g_gewaehlt = pKnzAntwortG_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public bool getKnzAntwortHGewaehlt()
        {
            return m_knz_antwort_h_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public void setKnzAntwortHGewaehlt( bool pKnzAntwortH_gewaehlt )
        {
            m_knz_antwort_h_gewaehlt = pKnzAntwortH_gewaehlt;
        }

        /* 
         * ################################################################################
         */
        public bool getKnzFrageLetzteAntwortKorrekt()
        {
            return m_knz_frage_letzte_antwort_korrekt;
        }

        /* 
         * ################################################################################
         */
        public bool getKnzFrageLetzteAntwortFalsch()
        {
            return ( m_knz_frage_letzte_antwort_korrekt == false );
        }

        /* 
         * ################################################################################
         */
        public void setKnzFrageLetzteAntwortKorrekt( bool pKnzFrageLetzteAntwortKorrekt )
        {
            m_knz_frage_letzte_antwort_korrekt = pKnzFrageLetzteAntwortKorrekt;
        }

        /* 
         * ################################################################################
         */
        public void reset()
        {
            m_anzahl_beantwortet_ja = 0;

            m_anzahl_beantwortet_nein = 0;

            m_anzahl_falsch_beantwortet = 0;

            m_anzahl_korrekt_beantwortet = 0;

            m_knz_antwort_a_gewaehlt = false;

            m_knz_antwort_b_gewaehlt = false;

            m_knz_antwort_c_gewaehlt = false;

            m_knz_antwort_d_gewaehlt = false;

            m_knz_antwort_e_gewaehlt = false;

            m_knz_antwort_f_gewaehlt = false;

            m_knz_antwort_g_gewaehlt = false;

            m_knz_antwort_h_gewaehlt = false;

            m_knz_frage_letzte_antwort_korrekt = false;
        }

        /* 
         * ################################################################################
         */
        public void clear()
        {
            m_index_fragen_katalog = 0;

            reset();
        }

        /* 
         * ################################################################################
         */
        public String ToStringX()
        {
            String temp_str = "";

            temp_str += "\nm_index_fragen_katalog             =>" + m_index_fragen_katalog + "<";

            temp_str += "\nm_anzahl_beantwortet_ja            =>" + m_anzahl_beantwortet_ja + "<";

            temp_str += "\nm_anzahl_beantwortet_nein          =>" + m_anzahl_beantwortet_nein + "<";

            temp_str += "\nm_anzahl_falsch_beantwortet        =>" + m_anzahl_falsch_beantwortet + "<";

            temp_str += "\nm_anzahl_korrekt_beantwortet       =>" + m_anzahl_korrekt_beantwortet + "<";

            temp_str += "\nm_knz_antwort_a_gewaehlt           =>" + m_knz_antwort_a_gewaehlt + "<";

            temp_str += "\nm_knz_antwort_b_gewaehlt           =>" + m_knz_antwort_b_gewaehlt + "<";

            temp_str += "\nm_knz_antwort_c_gewaehlt           =>" + m_knz_antwort_c_gewaehlt + "<";

            temp_str += "\nm_knz_antwort_d_gewaehlt           =>" + m_knz_antwort_d_gewaehlt + "<";

            temp_str += "\nm_knz_antwort_e_gewaehlt           =>" + m_knz_antwort_e_gewaehlt + "<";

            temp_str += "\nm_knz_antwort_f_gewaehlt           =>" + m_knz_antwort_f_gewaehlt + "<";

            temp_str += "\nm_knz_antwort_g_gewaehlt           =>" + m_knz_antwort_g_gewaehlt + "<";

            temp_str += "\nm_knz_antwort_h_gewaehlt           =>" + m_knz_antwort_h_gewaehlt + "<";

            temp_str += "\nm_knz_frage_letzte_antwort_korrekt =>" + m_knz_frage_letzte_antwort_korrekt + "<";

            return temp_str;
        }
    }
}
