using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class clsAntwort
    {
        private long m_id = 0;
        private String m_antwort_bez = "";
        private String m_antwort_text = "";
        private String m_bemerkung = "";
        private bool m_knz_korrekt = false;

        public long getId() 
        {
            return m_id; 
        }
        
        public void setId( long pId ) 
        {
            m_id = pId; 
        }

        public String getAntwortBez() 
        { 
            return m_antwort_bez; 
        }

        public void setAntwortBez( String pAntwortBez ) 
        { 
            m_antwort_bez = pAntwortBez; 
        }

        public String getAntwortText() 
        { 
            return m_antwort_text; 
        }

        public void setAntwortText( String pAntwortText ) 
        { 
            m_antwort_text = pAntwortText; 
        }

        public String getBemerkung() 
        { 
            return m_bemerkung; 
        }
        
        public void setBemerkung( String pBemerkung ) 
        { 
            m_bemerkung = pBemerkung; 
        }

        public int getKnzKorrektInt()
        {
            return ( m_knz_korrekt ? 1 : 0 );
        }

        public int getKnzFalschInt()
        {
            return ( m_knz_korrekt ? 0 : 1 );
        }

        public bool getKnzKorrekt() 
        { 
            return m_knz_korrekt; 
        }

        public bool getKnzFalsch() 
        { 
            return m_knz_korrekt == false; 
        }

        public void setKnzKorrekt( bool pKnzKorrekt ) 
        { 
            m_knz_korrekt = pKnzKorrekt; 
        }

        public String toString()
        {
            return m_antwort_text;
        }

        public void clear()
        {
            m_id = 0;
            m_antwort_bez = null;
            m_antwort_text = null;
            m_bemerkung = null;
            m_knz_korrekt = false; 
        }
    }
}
