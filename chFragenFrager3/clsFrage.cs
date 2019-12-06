using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class clsFrage
    {
        /*
         * Die eindeutige ID im Fragenkatalog
         */
        private String m_id = "";

        /*
         * Die Fragen-Nummer im Fragenkatalog
         */
        private String m_nummer = "";

        /*
         * Die laufende Nummer der Frage im aktuellen Fragenkatalog.
         * 
         * Diese Nummer wird beim hinzufuegen einer Frage zum Fragenkatalog gesetzt.
         * 
         * Die laufende Nummer wird als String gespeichdert, da es ansonsten 
         * zu Problemen beim Fragenexport kommt. (Stichwort fehlende Typisierung)
         */
        private String m_lfd_nummer = "";

        /*
         * Geltungsbereich
         * Zuordnung der Frage zu Bereichen, in welchen die Frage zur gueltig ist.
         * 
         * Im Fragenkatalog fuer den Luftfahrerschein, gibt es Geltungsbereiche fuer
         * 
         * Ballon, Hubschrauber, Segelflugzeuge und Motorflugzeuge.
         */
        private String m_geltungsbereich = "";

        /*
         * Fragetext
         * Fragetext 1 wird oben angezeigt und leitet die Frage ein.
         * Ein eventuell zweiter Fragentext wird unter den Antworten angezeigt.
         */
        private String m_text_1 = "";
        private String m_text_2 = "";

        /*
         * Eine Bemerkung zur Frage.
         */
        private String m_bemerkung = "";

        /*
         * Verweise auf die Dateinamen von Bildern. 
         * 
         * In den Bildern duerfen keine Pfadangaben stehen.
         * 
         * Es wird ein Resourcenverzeichnis im Fragenkatalog defniert, aus 
         * welchem dann diese Bilder geladen werden.
         */
        private String m_bild_1 = null;
        private String m_bild_2 = "";
        private String m_bild_3 = "";
        private String m_bild_4 = "";

        /*
         * Die aktuelle Vertauschreihenfolge
         * Der String fuer die Umstellung wird in der Klasse gespeichert, 
         * um bei jeder Vertauschung eine schon vertauschte Grundmenge 
         * zu haben.
         */
        private String m_str_vertausch_reihenfolge = "12345678";

        /*
         * Antworten
         * Die Membervariablen "m_antwort_<a-h>" speichern die Antworten der Frage
         * in der originaeren Reihenfolge.
         * 
         * Ist die entsprechende Variable "null", ist an der Stelle 
         * der Reihenfolge keine Antwort vorhanden.
         */
        private clsAntwort m_antwort_a = null;
        private clsAntwort m_antwort_b = null;
        private clsAntwort m_antwort_c = null;
        private clsAntwort m_antwort_d = null;
        private clsAntwort m_antwort_e = null;
        private clsAntwort m_antwort_f = null;
        private clsAntwort m_antwort_g = null;
        private clsAntwort m_antwort_h = null;

        /*
         * Antwort Aktiv Felder
         * 
         * Es koennen Antworten auf "inaktiv" gestellt werden.
         * Mit diesen Feldern wird das erscheinen von falschen 
         * Antworten in der Oberflaeche ung im Fragenexport 
         * gesteuert bzw. beeeinflusst.
         * 
         * Es soll der Reduzierung von falschen Antworten dienen.
         */
        private bool m_antwort_aktiv_a = true;
        private bool m_antwort_aktiv_b = true;
        private bool m_antwort_aktiv_c = true;
        private bool m_antwort_aktiv_d = true;
        private bool m_antwort_aktiv_e = true;
        private bool m_antwort_aktiv_f = true;
        private bool m_antwort_aktiv_g = true;
        private bool m_antwort_aktiv_h = true;

        /*
         * UI-Positionen
         * Die Membervariablen "m_ui_position_antwort_<a-h>" speichern die 
         * Position der Antwort auf der Benutzeroberflaeche.
         * 
         * Bei den UI-Positionen wird nicht darauf geachtet, ob die Frage.
         * auch eine entsprechende Instanz der Klasse Antwort hat.
         * 
         * Es gibt 8 UI-Positionen. 
         */
        private int m_ui_position_antwort_a = 1;
        private int m_ui_position_antwort_b = 2;
        private int m_ui_position_antwort_c = 3;
        private int m_ui_position_antwort_d = 4;
        private int m_ui_position_antwort_e = 5;
        private int m_ui_position_antwort_f = 6;
        private int m_ui_position_antwort_g = 7;
        private int m_ui_position_antwort_h = 8;

        public void startAntwortReduktion( int pAnzahlFalscheAntwortenJeKorrekterAntwort )
        {
            /*  
             * Aufruf der Funktion, um alle Antwortfelder auf "aktiv" zu stellen.
             */
            resetAntwortKnzAktiv();

            /*  
             * Mit der Anzahl der falschen Antworten, wird der Ausblendstring ermittelt.
             */
            String ausblend_str = null;
         
            /*  
             * Pruefung: Ausblendanzahl korrekt ?
             * 
             * Die Anzahl der Fragen, welche je korrekter Antwort drinbleiben sollen
             * muss groesser als 0 und kleiner 8 sein.
             * 
             */
            if ( ( pAnzahlFalscheAntwortenJeKorrekterAntwort > 0 ) && ( pAnzahlFalscheAntwortenJeKorrekterAntwort < 8 ) )
            {
                /*  
                 * Es wird die Anzahl der korrekten Antworten ermittelt.
                 */
                int anzahl_korrekte_antworten = getAnzahlKorrekteAntworten(); 

                /*  
                 * Anzahl der falschen Antworten berechnen, welche drin bleiben sollen
                 */
                int anzahl_falscher_antworten_die_bleiben = anzahl_korrekte_antworten * pAnzahlFalscheAntwortenJeKorrekterAntwort;

                /*  
                 * Es wird die Anzahl der falschen Antworten ermittelt.
                 */
                int anzahl_falsche_antworten = getAnzahlFalscheAntworten(); 

                /*  
                 * Die Anzahl der Antworten, die bleiben, muss kleiner als die 
                 * Gesamtanzahl der falschen Antworten sein.
                 * 
                 * Ist die Anzahl der bleibenden Antworten groesser als die 
                 * Gesamtanzahl der falschen Antworten, dann bleiben eh alle 
                 * falschen Antworten erhalten.
                 */
                if ( anzahl_falscher_antworten_die_bleiben < anzahl_falsche_antworten )
                {
                    int anzahl_antworten_raus = anzahl_falsche_antworten - anzahl_falscher_antworten_die_bleiben;

                    /* 
                     * Erstellung Ausblendstring 
                     * 
                     * Fuer jede Antwort, die "inaktiv" werden soll, gibt es eine "1".
                     * 
                     * Fuer jede Antwort, die "aktiv" bleiben soll, gibt es eine "0"
                     */
                    ausblend_str = fkString.left( "11111111", anzahl_antworten_raus ) + fkString.left( "00000000", anzahl_falscher_antworten_die_bleiben ) ;

                    /* 
                     * Der Ausblendstring wird per Zufall umgestellt.
                     */
                    ausblend_str = fkString.getRandomUmgestellt( 20, ausblend_str );

                    /*
                     * 1 korrekte Antwort * 2 je drin bleiben = 2 bleiben drin
                     * 
                     * Gibt es 3 falsche Antworten 
                     * 
                     * 2 bleiben drin < 3 falsche Antworten = 1 Antwort muss raus
                     * 
                     * ------------------------------------------------------------------
                     * 2 korrekte Antwort * 2 je drin bleiben = 4 bleiben drin
                     * 
                     * Gibt es 3 falsche Antworten 
                     * 
                     * 4 bleiben drin < 3 falsche Antworten = 0 Antworten raus
                     * 
                     ********************************************************************** 
                     * 
                     * 1 korrekte Antwort * 1 je drin bleiben = 1 falsche Antwort bleibt drin
                     * 
                     * Gibt es 3 falsche Antworten 
                     * 
                     * 1 bleibt drin < 3 falsche Antworten = 2 Antworten muessen raus
                     * 
                     * ------------------------------------------------------------------
                     * 3 korrekte Antwort * 1 je drin bleiben = 3 bleiben drin
                     * 
                     * Gibt es 5 falsche Antworten 
                     * 
                     * 3 bleiben drin < 5 falsche Antworten = 2 Antworten muessen raus
                     * 
                     *  anzahl_falsche_antworten - anzahl_falscher_antworten_die_bleiben = anzahl_antworten_raus
                     */

                    setAntwortAktivFelderByAusblendString( ausblend_str );
                }
            }
        }

        public void setAntwortAktivFelderByAusblendString( String pAusblendString )
        {
            /*
             * Pruefung: Ausblendstring gesetzt ?
             * 
             * Ist der Ausblendstring nicht gesetzt, wird keine Inaktivierung 
             * von Antworten gemacht. Die Aktiv-Felder behalten ihren Wert.
             */ 
            if ( pAusblendString != null )
            {
                /* 
                 * Die Variable "index_ausblendstring" gibt die derzeitige Position im
                 * Ausblendmuster an und bekommt eine 0 als Startwert.
                 */
                int index_ausblendstring = 0;

                if ( m_antwort_a != null )
                {
                    if ( m_antwort_a.getKnzKorrekt() == false )
                    {
                        m_antwort_aktiv_a = pAusblendString[ index_ausblendstring ] == '0';

                        index_ausblendstring++;
                    }
                }


                if ( m_antwort_b != null )
                {
                    if ( m_antwort_b.getKnzKorrekt() == false )
                    {
                        m_antwort_aktiv_b = pAusblendString[ index_ausblendstring ] == '0';

                        index_ausblendstring++;
                    }
                }


                if ( m_antwort_c != null )
                {
                    if ( m_antwort_c.getKnzKorrekt() == false )
                    {
                        m_antwort_aktiv_c = pAusblendString[ index_ausblendstring ] == '0';

                        index_ausblendstring++;
                    }
                }


                if ( m_antwort_d != null )
                {
                    if ( m_antwort_d.getKnzKorrekt() == false )
                    {
                        m_antwort_aktiv_d = pAusblendString[ index_ausblendstring ] == '0';

                        index_ausblendstring++;
                    }
                }


                if ( m_antwort_e != null )
                {
                    if ( m_antwort_e.getKnzKorrekt() == false )
                    {
                        m_antwort_aktiv_e = pAusblendString[ index_ausblendstring ] == '0';

                        index_ausblendstring++;
                    }
                }


                if ( m_antwort_f != null )
                {
                    if ( m_antwort_f.getKnzKorrekt() == false )
                    {
                        m_antwort_aktiv_f = pAusblendString[ index_ausblendstring ] == '0';

                        index_ausblendstring++;
                    }
                }


                if ( m_antwort_g != null )
                {
                    if ( m_antwort_g.getKnzKorrekt() == false )
                    {
                        m_antwort_aktiv_g = pAusblendString[ index_ausblendstring ] == '0';

                        index_ausblendstring++;
                    }
                }


                if ( m_antwort_h != null )
                {
                    if ( m_antwort_h.getKnzKorrekt() == false )
                    {
                        m_antwort_aktiv_h = pAusblendString[ index_ausblendstring ] == '0';

                        index_ausblendstring++;
                    }
                }
            } //  pAusblendString != null 
        }

        /**
         * Fuehrt einen Reset auf die Aktiv-Felder der Antworten aus.
         * Setzt alle Antworten auf Aktiv = true.
         */
        public void resetAntwortKnzAktiv()
        {
            m_antwort_aktiv_a = true;
            m_antwort_aktiv_b = true;
            m_antwort_aktiv_c = true;
            m_antwort_aktiv_d = true;
            m_antwort_aktiv_e = true;
            m_antwort_aktiv_f = true;
            m_antwort_aktiv_g = true;
            m_antwort_aktiv_h = true;
        }


        /**
         * Vertauscht die Antwortpositionen
         */
        public bool startAntwortReihenfolgeUmstellen()
        {
            bool fkt_ergebnis = true;

            /* 
             * Vertauschen der Antwortreihenfolge
             * 
             * Fuer jede Antwort gibt es auf der Oberflaeche eine Position von 1 bis 8.
             * 
             * Im Normalfall ist Antwort A auf Position 1 und so weiter.
             * 
             * In dieser Funktion wird jeder Antwort eine neue UI-Position zugewiesen.
             * 
             * Die Grundmenge von "12345678" steht jeweeils fuer eine UI-Position.
             * 
             * Diese Grundmenge wird per Zufall umgestellt.
             * 
             * Danach bekommt Antwort A die UI-Position von dem ersten Zeichen und 
             * und B vom zweiten Zeichen der umgestellten Grundmenge. Das wird fuer 
             * alle Antworten gemacht.
             */

            m_str_vertausch_reihenfolge = fkString.getRandomUmgestellt( 5, m_str_vertausch_reihenfolge );

            //System.Console.WriteLine( m_str_vertausch_reihenfolge );

            /* 
             * Index-Position Neu
             * Aus der Grundmenge wird fuer jede Antwort die neue Index-Position gelesen
             */
            m_ui_position_antwort_a = fkString.MidC( m_str_vertausch_reihenfolge, 0, 1 );
            m_ui_position_antwort_b = fkString.MidC( m_str_vertausch_reihenfolge, 1, 1 );
            m_ui_position_antwort_c = fkString.MidC( m_str_vertausch_reihenfolge, 2, 1 );
            m_ui_position_antwort_d = fkString.MidC( m_str_vertausch_reihenfolge, 3, 1 );
            m_ui_position_antwort_e = fkString.MidC( m_str_vertausch_reihenfolge, 4, 1 );
            m_ui_position_antwort_f = fkString.MidC( m_str_vertausch_reihenfolge, 5, 1 );
            m_ui_position_antwort_g = fkString.MidC( m_str_vertausch_reihenfolge, 6, 1 );
            m_ui_position_antwort_h = fkString.MidC( m_str_vertausch_reihenfolge, 7, 1 );

            /*
             * FALSCH:  m_str_vertausch_reihenfolge[ 0 ] liefert den ASCII-Code des Zeichens
             */
            //m_ui_position_antwort_a = m_str_vertausch_reihenfolge[ 0 ];


            return fkt_ergebnis;
        }

        /**
         * Fuehrt einen Reset auf die UI-Positionen der Antworten durch.
         * Antwort A erscheint fest auf UI-Position 1, usw
         */
        public void resetAntwortIndexPosition()
        {
            m_ui_position_antwort_a = 1;
            m_ui_position_antwort_b = 2;
            m_ui_position_antwort_c = 3;
            m_ui_position_antwort_d = 4;
            m_ui_position_antwort_e = 5;
            m_ui_position_antwort_f = 6;
            m_ui_position_antwort_g = 7;
            m_ui_position_antwort_h = 8;
        }

        /**
         * Liefert fuer die angegebene UI-Position, die dort angezeigte Antwort zurureck.
         * 
         * Ist die UI-Position nicht 1 bis 8 wird null zurueckgegeben.
         * 
         * Ist die Antwort an der UI-Position nicht "aktiv" wird null zurueckgeben.
         * 
         * @param pUiPosition die abgefragte UI-Position (1 - 8)
         * @return die angezeigte Antwort an der UI-Position, oder null wenn dort keine Antwort vorhanden ist
         */
        public clsAntwort getAntwortAnUiPosition(int pUiPosition)
        {
            if ( pUiPosition == m_ui_position_antwort_a ) { return ( m_antwort_aktiv_a ? m_antwort_a : null ); }
            if ( pUiPosition == m_ui_position_antwort_b ) { return ( m_antwort_aktiv_b ? m_antwort_b : null ); }
            if ( pUiPosition == m_ui_position_antwort_c ) { return ( m_antwort_aktiv_c ? m_antwort_c : null ); }
            if ( pUiPosition == m_ui_position_antwort_d ) { return ( m_antwort_aktiv_d ? m_antwort_d : null ); }
            if ( pUiPosition == m_ui_position_antwort_e ) { return ( m_antwort_aktiv_e ? m_antwort_e : null ); }
            if ( pUiPosition == m_ui_position_antwort_f ) { return ( m_antwort_aktiv_f ? m_antwort_f : null ); }
            if ( pUiPosition == m_ui_position_antwort_g ) { return ( m_antwort_aktiv_g ? m_antwort_g : null ); }
            if ( pUiPosition == m_ui_position_antwort_h ) { return ( m_antwort_aktiv_h ? m_antwort_h : null ); }

            return null;
        }

        /**
         * Schaut nach, welche Antwort an der UI-Position angezeigt wird und 
         * gibt im Erfolgsfall das entsprechende Kennzeichenfeld der Antwort
         * zurueck.
         * 
         * Wird keine Uebereinstimmung gefunden, wird FALSE zurueckgegeben.
         * 
         * Mit dieser Funktion werden die einmal getroffenen Antwortauswahlen,
         * den neuen UI-Positionen zugewiesen.
         * 
         * 
         * @param pUiPosition die abgefragte UI-Position (1 - 8)
         * @param pKnzAntwortA Kennzeichenfeld, ob Antwort A schon einmal ausgewaehlt war
         * @param pKnzAntwortB Kennzeichenfeld, ob Antwort B schon einmal ausgewaehlt war
         * @param pKnzAntwortC Kennzeichenfeld, ob Antwort C schon einmal ausgewaehlt war
         * @param pKnzAntwortD Kennzeichenfeld, ob Antwort D schon einmal ausgewaehlt war
         * @param pKnzAntwortE Kennzeichenfeld, ob Antwort E schon einmal ausgewaehlt war
         * @param pKnzAntwortF Kennzeichenfeld, ob Antwort F schon einmal ausgewaehlt war
         * @param pKnzAntwortG Kennzeichenfeld, ob Antwort G schon einmal ausgewaehlt war
         * @param pKnzAntwortH Kennzeichenfeld, ob Antwort H schon einmal ausgewaehlt war
         * @return das Kennzeichenfeld der Antwort, wenn die Antwort an der UI-Position angezeigt wird, sonst FALSE
         */
        public bool istUiPositionChecked( int pUiPosition, bool pKnzAntwortA, bool pKnzAntwortB, bool pKnzAntwortC, bool pKnzAntwortD, bool pKnzAntwortE, bool pKnzAntwortF, bool pKnzAntwortG, bool pKnzAntwortH )
        {
            if ( pUiPosition == m_ui_position_antwort_a ) { return pKnzAntwortA; }
            if ( pUiPosition == m_ui_position_antwort_b ) { return pKnzAntwortB; }
            if ( pUiPosition == m_ui_position_antwort_c ) { return pKnzAntwortC; }
            if ( pUiPosition == m_ui_position_antwort_d ) { return pKnzAntwortD; }
            if ( pUiPosition == m_ui_position_antwort_e ) { return pKnzAntwortE; }
            if ( pUiPosition == m_ui_position_antwort_f ) { return pKnzAntwortF; }
            if ( pUiPosition == m_ui_position_antwort_g ) { return pKnzAntwortG; }
            if ( pUiPosition == m_ui_position_antwort_h ) { return pKnzAntwortH; }

            return false;
        }

        /**
         * Liefert die Anzahl der korrekten Antworten zurueck.
         * 
         * Bei jeder vorhandenen und korrekten Antwort wird ein Zaehler erhoeht.
         * 
         * @return die anzahl der insgesamt korrekten Antworten
         */
        public int getAnzahlKorrekteAntworten()
        {
            int anzahl_antworten_korrekt = 0;

            anzahl_antworten_korrekt += ( m_antwort_a != null ? m_antwort_a.getKnzKorrektInt() : 0 );
            anzahl_antworten_korrekt += ( m_antwort_b != null ? m_antwort_b.getKnzKorrektInt() : 0 );
            anzahl_antworten_korrekt += ( m_antwort_c != null ? m_antwort_c.getKnzKorrektInt() : 0 );
            anzahl_antworten_korrekt += ( m_antwort_d != null ? m_antwort_d.getKnzKorrektInt() : 0 );
            anzahl_antworten_korrekt += ( m_antwort_e != null ? m_antwort_e.getKnzKorrektInt() : 0 );
            anzahl_antworten_korrekt += ( m_antwort_f != null ? m_antwort_f.getKnzKorrektInt() : 0 );
            anzahl_antworten_korrekt += ( m_antwort_g != null ? m_antwort_g.getKnzKorrektInt() : 0 );
            anzahl_antworten_korrekt += ( m_antwort_h != null ? m_antwort_h.getKnzKorrektInt() : 0 );

            return anzahl_antworten_korrekt;
        }

        public int getAnzahlFalscheAntworten()
        {
            int anzahl_antworten_falsch = 0;

            anzahl_antworten_falsch += ( m_antwort_a != null ? m_antwort_a.getKnzFalschInt() : 0 );
            anzahl_antworten_falsch += ( m_antwort_b != null ? m_antwort_b.getKnzFalschInt() : 0 );
            anzahl_antworten_falsch += ( m_antwort_c != null ? m_antwort_c.getKnzFalschInt() : 0 );
            anzahl_antworten_falsch += ( m_antwort_d != null ? m_antwort_d.getKnzFalschInt() : 0 );
            anzahl_antworten_falsch += ( m_antwort_e != null ? m_antwort_e.getKnzFalschInt() : 0 );
            anzahl_antworten_falsch += ( m_antwort_f != null ? m_antwort_f.getKnzFalschInt() : 0 );
            anzahl_antworten_falsch += ( m_antwort_g != null ? m_antwort_g.getKnzFalschInt() : 0 );
            anzahl_antworten_falsch += ( m_antwort_h != null ? m_antwort_h.getKnzFalschInt() : 0 );

            return anzahl_antworten_falsch;
        }


        /**
         * Liefert die Anzahl der vorhandenen Antworten zurueck
         * 
         * Bei jeder vorhandenen Antwort wird ein Zaehler erhoeht.
         * 
         * @return die Anzahl der insgesamt korrekten Antworten
         */
        public int getAnzahlVorhandeneAntworten()
        {
            int anzahl_antworten_vorhanden = 0;

            anzahl_antworten_vorhanden += ( m_antwort_a != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( m_antwort_b != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( m_antwort_c != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( m_antwort_d != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( m_antwort_e != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( m_antwort_f != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( m_antwort_g != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( m_antwort_h != null ? 1 : 0 );

            return anzahl_antworten_vorhanden;
        }

        /**
         * Liefert die Information zurueck, ob Antwort A mit der Checkbox ausgewaehlt wurde.
         * 
         * @return den Wert des Kennzeichenfeldes der UI-Position, oder FALSE wenn die UI-Position nicht belegt ist.
         */
        public bool istAntwortAChecked( bool pKnzChekboxUiPos1, bool pKnzChekboxUiPos2, bool pKnzChekboxUiPos3, bool pKnzChekboxUiPos4, bool pKnzChekboxUiPos5, bool pKnzChekboxUiPos6, bool pKnzChekboxUiPos7, bool pKnzChekboxUiPos8 )
        {
            /*  
             * Es kommen alle Kennzeichenfelder der Checkboxen rein (TRUE gewaehlt, FALSE nicht gewaehlt)
             * 
             * Es wird geprueft, an welcher UI-Position Antwort A steht. 
             * Es wird die Kennzeichenvariable zurueckgegeben, an dessen UI-Position Antwort A steht.
             * (Welche Position bin ich und was ist meine Kennzeichenvariable)
             * 
             * Steht Antwort A auf Position 1, wird die Kennzeichenvariable fuer 1 zurueck gegeben.
             * Steht Antwort A auf Position 2, wird die Kennzeichenvariable fuer 2 zurueck gegeben.
             * 
             * Ist Antwort A unbelegt (Position 9) wird FALSE zurueckgegeben.
             */  
            if ( m_ui_position_antwort_a == 1 ) { return pKnzChekboxUiPos1; }
            if ( m_ui_position_antwort_a == 2 ) { return pKnzChekboxUiPos2; }
            if ( m_ui_position_antwort_a == 3 ) { return pKnzChekboxUiPos3; }
            if ( m_ui_position_antwort_a == 4 ) { return pKnzChekboxUiPos4; }
            if ( m_ui_position_antwort_a == 5 ) { return pKnzChekboxUiPos5; }
            if ( m_ui_position_antwort_a == 6 ) { return pKnzChekboxUiPos6; }
            if ( m_ui_position_antwort_a == 7 ) { return pKnzChekboxUiPos7; }
            if ( m_ui_position_antwort_a == 8 ) { return pKnzChekboxUiPos8; }

            return false;
       }

        /**
         * Liefert die Information zurueck, ob Antwort B mit der Checkbox ausgewaehlt wurde.
         * 
         * @return den Wert des Kennzeichenfeldes der UI-Position, oder FALSE wenn die UI-Position nicht belegt ist.
         */
        public bool istAntwortBChecked( bool pKnzChekboxUiPos1, bool pKnzChekboxUiPos2, bool pKnzChekboxUiPos3, bool pKnzChekboxUiPos4, bool pKnzChekboxUiPos5, bool pKnzChekboxUiPos6, bool pKnzChekboxUiPos7, bool pKnzChekboxUiPos8 )
        {
            if ( m_ui_position_antwort_b == 1 ) { return pKnzChekboxUiPos1; }
            if ( m_ui_position_antwort_b == 2 ) { return pKnzChekboxUiPos2; }
            if ( m_ui_position_antwort_b == 3 ) { return pKnzChekboxUiPos3; }
            if ( m_ui_position_antwort_b == 4 ) { return pKnzChekboxUiPos4; }
            if ( m_ui_position_antwort_b == 5 ) { return pKnzChekboxUiPos5; }
            if ( m_ui_position_antwort_b == 6 ) { return pKnzChekboxUiPos6; }
            if ( m_ui_position_antwort_b == 7 ) { return pKnzChekboxUiPos7; }
            if ( m_ui_position_antwort_b == 8 ) { return pKnzChekboxUiPos8; }

            return false;
        }

        /**
         * Liefert die Information zurueck, ob Antwort C mit der Checkbox ausgewaehlt wurde.
         * 
         * @return den Wert des Kennzeichenfeldes der UI-Position, oder FALSE wenn die UI-Position nicht belegt ist.
         */
        public bool istAntwortCChecked( bool pKnzChekboxUiPos1, bool pKnzChekboxUiPos2, bool pKnzChekboxUiPos3, bool pKnzChekboxUiPos4, bool pKnzChekboxUiPos5, bool pKnzChekboxUiPos6, bool pKnzChekboxUiPos7, bool pKnzChekboxUiPos8 )
        {
            if ( m_ui_position_antwort_c == 1 ) { return pKnzChekboxUiPos1; }
            if ( m_ui_position_antwort_c == 2 ) { return pKnzChekboxUiPos2; }
            if ( m_ui_position_antwort_c == 3 ) { return pKnzChekboxUiPos3; }
            if ( m_ui_position_antwort_c == 4 ) { return pKnzChekboxUiPos4; }
            if ( m_ui_position_antwort_c == 5 ) { return pKnzChekboxUiPos5; }
            if ( m_ui_position_antwort_c == 6 ) { return pKnzChekboxUiPos6; }
            if ( m_ui_position_antwort_c == 7 ) { return pKnzChekboxUiPos7; }
            if ( m_ui_position_antwort_c == 8 ) { return pKnzChekboxUiPos8; }

            return false;
        }

        /**
         * Liefert die Information zurueck, ob Antwort D mit der Checkbox ausgewaehlt wurde.
         * 
         * @return den Wert des Kennzeichenfeldes der UI-Position, oder FALSE wenn die UI-Position nicht belegt ist.
         */
        public bool istAntwortDChecked( bool pKnzChekboxUiPos1, bool pKnzChekboxUiPos2, bool pKnzChekboxUiPos3, bool pKnzChekboxUiPos4, bool pKnzChekboxUiPos5, bool pKnzChekboxUiPos6, bool pKnzChekboxUiPos7, bool pKnzChekboxUiPos8 )
        {
            if ( m_ui_position_antwort_d == 1 ) { return pKnzChekboxUiPos1; }
            if ( m_ui_position_antwort_d == 2 ) { return pKnzChekboxUiPos2; }
            if ( m_ui_position_antwort_d == 3 ) { return pKnzChekboxUiPos3; }
            if ( m_ui_position_antwort_d == 4 ) { return pKnzChekboxUiPos4; }
            if ( m_ui_position_antwort_d == 5 ) { return pKnzChekboxUiPos5; }
            if ( m_ui_position_antwort_d == 6 ) { return pKnzChekboxUiPos6; }
            if ( m_ui_position_antwort_d == 7 ) { return pKnzChekboxUiPos7; }
            if ( m_ui_position_antwort_d == 8 ) { return pKnzChekboxUiPos8; }

            return false;
        }

        /**
         * Liefert die Information zurueck, ob Antwort E mit der Checkbox ausgewaehlt wurde.
         * 
         * @return den Wert des Kennzeichenfeldes der UI-Position, oder FALSE wenn die UI-Position nicht belegt ist.
         */
        public bool istAntwortEChecked(bool pKnzChekboxUiPos1, bool pKnzChekboxUiPos2, bool pKnzChekboxUiPos3, bool pKnzChekboxUiPos4, bool pKnzChekboxUiPos5, bool pKnzChekboxUiPos6, bool pKnzChekboxUiPos7, bool pKnzChekboxUiPos8)
        {
            if ( m_ui_position_antwort_e == 1 ) { return pKnzChekboxUiPos1; }
            if ( m_ui_position_antwort_e == 2 ) { return pKnzChekboxUiPos2; }
            if ( m_ui_position_antwort_e == 3 ) { return pKnzChekboxUiPos3; }
            if ( m_ui_position_antwort_e == 4 ) { return pKnzChekboxUiPos4; }
            if ( m_ui_position_antwort_e == 5 ) { return pKnzChekboxUiPos5; }
            if ( m_ui_position_antwort_e == 6 ) { return pKnzChekboxUiPos6; }
            if ( m_ui_position_antwort_e == 7 ) { return pKnzChekboxUiPos7; }
            if ( m_ui_position_antwort_e == 8 ) { return pKnzChekboxUiPos8; }

            return false;
        }

        /**
         * Liefert die Information zurueck, ob Antwort F mit der Checkbox ausgewaehlt wurde.
         * 
         * @return den Wert des Kennzeichenfeldes der UI-Position, oder FALSE wenn die UI-Position nicht belegt ist.
         */
        public bool istAntwortFChecked( bool pKnzChekboxUiPos1, bool pKnzChekboxUiPos2, bool pKnzChekboxUiPos3, bool pKnzChekboxUiPos4, bool pKnzChekboxUiPos5, bool pKnzChekboxUiPos6, bool pKnzChekboxUiPos7, bool pKnzChekboxUiPos8 )
        {
            if ( m_ui_position_antwort_f == 1 ) { return pKnzChekboxUiPos1; }
            if ( m_ui_position_antwort_f == 2 ) { return pKnzChekboxUiPos2; }
            if ( m_ui_position_antwort_f == 3 ) { return pKnzChekboxUiPos3; }
            if ( m_ui_position_antwort_f == 4 ) { return pKnzChekboxUiPos4; }
            if ( m_ui_position_antwort_f == 5 ) { return pKnzChekboxUiPos5; }
            if ( m_ui_position_antwort_f == 6 ) { return pKnzChekboxUiPos6; }
            if ( m_ui_position_antwort_f == 7 ) { return pKnzChekboxUiPos7; }
            if ( m_ui_position_antwort_f == 8 ) { return pKnzChekboxUiPos8; }

            return false;
        }

        /**
         * Liefert die Information zurueck, ob Antwort G mit der Checkbox ausgewaehlt wurde.
         * 
         * @return den Wert des Kennzeichenfeldes der UI-Position, oder FALSE wenn die UI-Position nicht belegt ist.
         */
        public bool istAntwortGChecked( bool pKnzChekboxUiPos1, bool pKnzChekboxUiPos2, bool pKnzChekboxUiPos3, bool pKnzChekboxUiPos4, bool pKnzChekboxUiPos5, bool pKnzChekboxUiPos6, bool pKnzChekboxUiPos7, bool pKnzChekboxUiPos8 )
        {
            if ( m_ui_position_antwort_g == 1 ) { return pKnzChekboxUiPos1; }
            if ( m_ui_position_antwort_g == 2 ) { return pKnzChekboxUiPos2; }
            if ( m_ui_position_antwort_g == 3 ) { return pKnzChekboxUiPos3; }
            if ( m_ui_position_antwort_g == 4 ) { return pKnzChekboxUiPos4; }
            if ( m_ui_position_antwort_g == 5 ) { return pKnzChekboxUiPos5; }
            if ( m_ui_position_antwort_g == 6 ) { return pKnzChekboxUiPos6; }
            if ( m_ui_position_antwort_g == 7 ) { return pKnzChekboxUiPos7; }
            if ( m_ui_position_antwort_g == 8 ) { return pKnzChekboxUiPos8; }

            return false;
        }

        /**
         * Liefert die Information zurueck, ob Antwort H mit der Checkbox ausgewaehlt wurde.
         * 
         * @return den Wert des Kennzeichenfeldes der UI-Position, oder FALSE wenn die UI-Position nicht belegt ist.
         */
        public bool istAntwortHChecked( bool pKnzChekboxUiPos1, bool pKnzChekboxUiPos2, bool pKnzChekboxUiPos3, bool pKnzChekboxUiPos4, bool pKnzChekboxUiPos5, bool pKnzChekboxUiPos6, bool pKnzChekboxUiPos7, bool pKnzChekboxUiPos8 )
        {
            if ( m_ui_position_antwort_h == 1 ) { return pKnzChekboxUiPos1; }
            if ( m_ui_position_antwort_h == 2 ) { return pKnzChekboxUiPos2; }
            if ( m_ui_position_antwort_h == 3 ) { return pKnzChekboxUiPos3; }
            if ( m_ui_position_antwort_h == 4 ) { return pKnzChekboxUiPos4; }
            if ( m_ui_position_antwort_h == 5 ) { return pKnzChekboxUiPos5; }
            if ( m_ui_position_antwort_h == 6 ) { return pKnzChekboxUiPos6; }
            if ( m_ui_position_antwort_h == 7 ) { return pKnzChekboxUiPos7; }
            if ( m_ui_position_antwort_h == 8 ) { return pKnzChekboxUiPos8; }

            return false;
        }
        
        /*
         * Funktionen zum setzen der Antworten 
         */

        public void setAntwortA( clsAntwort pAntwortA ) { m_antwort_a = pAntwortA; }
        public void setAntwortB( clsAntwort pAntwortB ) { m_antwort_b = pAntwortB; }
        public void setAntwortC( clsAntwort pAntwortC ) { m_antwort_c = pAntwortC; }
        public void setAntwortD( clsAntwort pAntwortD ) { m_antwort_d = pAntwortD; }
        public void setAntwortE( clsAntwort pAntwortE ) { m_antwort_e = pAntwortE; }
        public void setAntwortF( clsAntwort pAntwortF ) { m_antwort_f = pAntwortF; }
        public void setAntwortG( clsAntwort pAntwortG ) { m_antwort_g = pAntwortG; }
        public void setAntwortH( clsAntwort pAntwortH ) { m_antwort_h = pAntwortH; }

        /*
         * Funktionen zum holen der Antworten nach normaler Reihenfolge
         */

        public clsAntwort getAntwortA() { return m_antwort_a; }
        public clsAntwort getAntwortB() { return m_antwort_b; }
        public clsAntwort getAntwortC() { return m_antwort_c; }
        public clsAntwort getAntwortD() { return m_antwort_d; }
        public clsAntwort getAntwortE() { return m_antwort_e; }
        public clsAntwort getAntwortF() { return m_antwort_f; }
        public clsAntwort getAntwortG() { return m_antwort_g; }
        public clsAntwort getAntwortH() { return m_antwort_h; }

        /*
         * Funktionen zum Abfragen, ob eine Antwort vorhanden ist nach normaler Reihenfolge
         */

        public bool hasAntwortA() { return m_antwort_a != null; }
        public bool hasAntwortB() { return m_antwort_b != null; }
        public bool hasAntwortC() { return m_antwort_c != null; }
        public bool hasAntwortD() { return m_antwort_d != null; }
        public bool hasAntwortE() { return m_antwort_e != null; }
        public bool hasAntwortF() { return m_antwort_f != null; }
        public bool hasAntwortG() { return m_antwort_g != null; }
        public bool hasAntwortH() { return m_antwort_h != null; }

        /*
         * Funktionen zum holen des Antwort-Textes
         */

        public String getAntwortAText() { return ( m_antwort_a != null ? m_antwort_a.getAntwortText() : "" ); }
        public String getAntwortBText() { return ( m_antwort_b != null ? m_antwort_b.getAntwortText() : "" ); }
        public String getAntwortCText() { return ( m_antwort_c != null ? m_antwort_c.getAntwortText() : "" ); }
        public String getAntwortDText() { return ( m_antwort_d != null ? m_antwort_d.getAntwortText() : "" ); }
        public String getAntwortEText() { return ( m_antwort_e != null ? m_antwort_e.getAntwortText() : "" ); }
        public String getAntwortFText() { return ( m_antwort_f != null ? m_antwort_f.getAntwortText() : "" ); }
        public String getAntwortGText() { return ( m_antwort_g != null ? m_antwort_g.getAntwortText() : "" ); }
        public String getAntwortHText() { return ( m_antwort_h != null ? m_antwort_h.getAntwortText() : "" ); }

        /*
         * 
         */

        public bool getAntwortAKorrekt() { return ( m_antwort_a != null ? m_antwort_a.getKnzKorrekt() : false ); }
        public bool getAntwortBKorrekt() { return ( m_antwort_b != null ? m_antwort_b.getKnzKorrekt() : false ); }
        public bool getAntwortCKorrekt() { return ( m_antwort_c != null ? m_antwort_c.getKnzKorrekt() : false ); }
        public bool getAntwortDKorrekt() { return ( m_antwort_d != null ? m_antwort_d.getKnzKorrekt() : false ); }
        public bool getAntwortEKorrekt() { return ( m_antwort_e != null ? m_antwort_e.getKnzKorrekt() : false ); }
        public bool getAntwortFKorrekt() { return ( m_antwort_f != null ? m_antwort_f.getKnzKorrekt() : false ); }
        public bool getAntwortGKorrekt() { return ( m_antwort_g != null ? m_antwort_g.getKnzKorrekt() : false ); }
        public bool getAntwortHKorrekt() { return ( m_antwort_h != null ? m_antwort_h.getKnzKorrekt() : false ); }

        /*
         * 
         */

        public bool getAntwortAFalsch() { return ( m_antwort_a != null ? m_antwort_a.getKnzFalsch() : false ); }
        public bool getAntwortBFalsch() { return ( m_antwort_b != null ? m_antwort_b.getKnzFalsch() : false ); }
        public bool getAntwortCFalsch() { return ( m_antwort_c != null ? m_antwort_c.getKnzFalsch() : false ); }
        public bool getAntwortDFalsch() { return ( m_antwort_d != null ? m_antwort_d.getKnzFalsch() : false ); }
        public bool getAntwortEFalsch() { return ( m_antwort_e != null ? m_antwort_e.getKnzFalsch() : false ); }
        public bool getAntwortFFalsch() { return ( m_antwort_f != null ? m_antwort_f.getKnzFalsch() : false ); }
        public bool getAntwortGFalsch() { return ( m_antwort_g != null ? m_antwort_g.getKnzFalsch() : false ); }
        public bool getAntwortHFalsch() { return ( m_antwort_h != null ? m_antwort_h.getKnzFalsch() : false ); }

        /*
         * Funktionen zum Abfragen, ob an einer UI-Position eine Antwort vorhanden ist.
         * 
         * Ist an der Position eine Antwort definiert, wird TRUE zurueckgegeben.
         * Ist an der Position keine Antwort definiert, wird FALSE zurueckgegeben.
         */

        public bool hasUiPositionAntwort1() { return getAntwortAnUiPosition( 1 ) != null; }
        public bool hasUiPositionAntwort2() { return getAntwortAnUiPosition( 2 ) != null; }
        public bool hasUiPositionAntwort3() { return getAntwortAnUiPosition( 3 ) != null; }
        public bool hasUiPositionAntwort4() { return getAntwortAnUiPosition( 4 ) != null; }
        public bool hasUiPositionAntwort5() { return getAntwortAnUiPosition( 5 ) != null; }
        public bool hasUiPositionAntwort6() { return getAntwortAnUiPosition( 6 ) != null; }
        public bool hasUiPositionAntwort7() { return getAntwortAnUiPosition( 7 ) != null; }
        public bool hasUiPositionAntwort8() { return getAntwortAnUiPosition( 8 ) != null; }
        
        /*
         * Funktionen zum Abfragen des Antworttextes an einer UI-Position.
         * 
         * Ist an der Position keine Antwort definiert, wird ein Leerstring zurueckgegeben.
         */

        public String getUiPositionAntwort1Text() { return ( getAntwortAnUiPosition( 1 ) != null ? getAntwortAnUiPosition( 1 ).getAntwortText() : "" ); }
        public String getUiPositionAntwort2Text() { return ( getAntwortAnUiPosition( 2 ) != null ? getAntwortAnUiPosition( 2 ).getAntwortText() : "" ); }
        public String getUiPositionAntwort3Text() { return ( getAntwortAnUiPosition( 3 ) != null ? getAntwortAnUiPosition( 3 ).getAntwortText() : "" ); }
        public String getUiPositionAntwort4Text() { return ( getAntwortAnUiPosition( 4 ) != null ? getAntwortAnUiPosition( 4 ).getAntwortText() : "" ); }
        public String getUiPositionAntwort5Text() { return ( getAntwortAnUiPosition( 5 ) != null ? getAntwortAnUiPosition( 5 ).getAntwortText() : "" ); }
        public String getUiPositionAntwort6Text() { return ( getAntwortAnUiPosition( 6 ) != null ? getAntwortAnUiPosition( 6 ).getAntwortText() : "" ); }
        public String getUiPositionAntwort7Text() { return ( getAntwortAnUiPosition( 7 ) != null ? getAntwortAnUiPosition( 7 ).getAntwortText() : "" ); }
        public String getUiPositionAntwort8Text() { return ( getAntwortAnUiPosition( 8 ) != null ? getAntwortAnUiPosition( 8 ).getAntwortText() : "" ); }

        public String getUiPositionAntwort1Bez() { return ( getAntwortAnUiPosition( 1 ) != null ? getAntwortAnUiPosition( 1 ).getAntwortBez() : "" ); }
        public String getUiPositionAntwort2Bez() { return ( getAntwortAnUiPosition( 2 ) != null ? getAntwortAnUiPosition( 2 ).getAntwortBez() : "" ); }
        public String getUiPositionAntwort3Bez() { return ( getAntwortAnUiPosition( 3 ) != null ? getAntwortAnUiPosition( 3 ).getAntwortBez() : "" ); }
        public String getUiPositionAntwort4Bez() { return ( getAntwortAnUiPosition( 4 ) != null ? getAntwortAnUiPosition( 4 ).getAntwortBez() : "" ); }
        public String getUiPositionAntwort5Bez() { return ( getAntwortAnUiPosition( 5 ) != null ? getAntwortAnUiPosition( 5 ).getAntwortBez() : "" ); }
        public String getUiPositionAntwort6Bez() { return ( getAntwortAnUiPosition( 6 ) != null ? getAntwortAnUiPosition( 6 ).getAntwortBez() : "" ); }
        public String getUiPositionAntwort7Bez() { return ( getAntwortAnUiPosition( 7 ) != null ? getAntwortAnUiPosition( 7 ).getAntwortBez() : "" ); }
        public String getUiPositionAntwort8Bez() { return ( getAntwortAnUiPosition( 8 ) != null ? getAntwortAnUiPosition( 8 ).getAntwortBez() : "" ); }


        /*
         * Funktionen zum Abfragen, ob eine Antwort an einer UI-Position korrekt ist.
         */

        public bool getUiPositionAntwort1Korrekt() { return ( getAntwortAnUiPosition( 1 ) != null ? getAntwortAnUiPosition( 1 ).getKnzKorrekt() : false ); }
        public bool getUiPositionAntwort2Korrekt() { return ( getAntwortAnUiPosition( 2 ) != null ? getAntwortAnUiPosition( 2 ).getKnzKorrekt() : false ); }
        public bool getUiPositionAntwort3Korrekt() { return ( getAntwortAnUiPosition( 3 ) != null ? getAntwortAnUiPosition( 3 ).getKnzKorrekt() : false ); }
        public bool getUiPositionAntwort4Korrekt() { return ( getAntwortAnUiPosition( 4 ) != null ? getAntwortAnUiPosition( 4 ).getKnzKorrekt() : false ); }
        public bool getUiPositionAntwort5Korrekt() { return ( getAntwortAnUiPosition( 5 ) != null ? getAntwortAnUiPosition( 5 ).getKnzKorrekt() : false ); }
        public bool getUiPositionAntwort6Korrekt() { return ( getAntwortAnUiPosition( 6 ) != null ? getAntwortAnUiPosition( 6 ).getKnzKorrekt() : false ); }
        public bool getUiPositionAntwort7Korrekt() { return ( getAntwortAnUiPosition( 7 ) != null ? getAntwortAnUiPosition( 7 ).getKnzKorrekt() : false ); }
        public bool getUiPositionAntwort8Korrekt() { return ( getAntwortAnUiPosition( 8 ) != null ? getAntwortAnUiPosition( 8 ).getKnzKorrekt() : false ); }



        /**
         * Liefert die Anzahl der korrekten aktiven Antworten zurueck.
         * 
         * Bei jeder vorhandenen und korrekten Antwort wird ein Zaehler erhoeht.
         * 
         * @return die Anzahl der insgesamt korrekten aktiven Antworten
         */
        public int getUiAnzahlKorrekteAntworten()
        {
            int anzahl_antworten_korrekt = 0;

            anzahl_antworten_korrekt += ( getUiPositionAntwort1Korrekt() ? 1 : 0 );
            anzahl_antworten_korrekt += ( getUiPositionAntwort2Korrekt() ? 1 : 0 );
            anzahl_antworten_korrekt += ( getUiPositionAntwort3Korrekt() ? 1 : 0 );
            anzahl_antworten_korrekt += ( getUiPositionAntwort4Korrekt() ? 1 : 0 );
            anzahl_antworten_korrekt += ( getUiPositionAntwort5Korrekt() ? 1 : 0 );
            anzahl_antworten_korrekt += ( getUiPositionAntwort6Korrekt() ? 1 : 0 );
            anzahl_antworten_korrekt += ( getUiPositionAntwort7Korrekt() ? 1 : 0 );
            anzahl_antworten_korrekt += ( getUiPositionAntwort8Korrekt() ? 1 : 0 );

            return anzahl_antworten_korrekt;
        }

        /**
         * Liefert die Anzahl der vorhandenen aktiven Antworten zurueck
         * 
         * Bei jeder vorhandenen Antwort wird ein Zaehler erhoeht.
         * 
         * @return die Anzahl der insgesamt vorhandenen aktiven Antworten
         */
        public int getUiAnzahlVorhandeneAntworten()
        {
            int anzahl_antworten_vorhanden = 0;

            anzahl_antworten_vorhanden += ( getAntwortAnUiPosition( 1 ) != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( getAntwortAnUiPosition( 2 ) != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( getAntwortAnUiPosition( 3 ) != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( getAntwortAnUiPosition( 4 ) != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( getAntwortAnUiPosition( 5 ) != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( getAntwortAnUiPosition( 6 ) != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( getAntwortAnUiPosition( 7 ) != null ? 1 : 0 );
            anzahl_antworten_vorhanden += ( getAntwortAnUiPosition( 8 ) != null ? 1 : 0 );

            return anzahl_antworten_vorhanden;
        }

        public String getLfdNummer() { return m_lfd_nummer; }
        public void setLfdNummer(String pLfdNummer) { m_lfd_nummer = pLfdNummer; }
        
        public String getBemerkung() { return m_bemerkung; }
        public String getBild1() { return m_bild_1; }
        public String getBild2() { return m_bild_2; }
        public String getBild3() { return m_bild_3; }
        public String getBild4() { return m_bild_4; }
        public String getGeltungsbereich() { return m_geltungsbereich; }
        public String getId() { return m_id; }
        public String getNummer() { return m_nummer; }
        public String getText1() { return m_text_1; }
        public String getText2() { return m_text_2; }
        public String getStrVertauschReihenfolge() { return m_str_vertausch_reihenfolge; }
        
        public bool hasText2() { return ( m_text_2 != null ) && ( m_text_2.Length > 0 ); }
        public void setBemerkung( String pBemerkung ) { m_bemerkung = pBemerkung; }
        public void setBild1(String pBild1) { m_bild_1 = pBild1; }
        public void setBild2( String pBild2 ) { m_bild_2 = pBild2; }
        public void setBild3( String pBild3 ) { m_bild_3 = pBild3; }
        public void setBild4( String pBild4 ) { m_bild_4 = pBild4; }
        public void setGeltungsbereich( String pGeltungsbereich ) { m_geltungsbereich = pGeltungsbereich; }
        public void setId( String pId ) { m_id = pId; }
        public void setNummer( String pNummer ) { m_nummer = pNummer; }
        public void setText1( String pText1 ) { m_text_1 = pText1; }
        public void setText2( String pText2 ) { m_text_2 = pText2; }
 
        /* 
         * ################################################################################
         */
        public void clear()
        {
            m_id = "";
            m_nummer = "";
            m_geltungsbereich = "";
            m_text_1 = "";
            m_text_2 = "";
            m_bemerkung = "";
            m_bild_1 = "";
            m_bild_2 = "";
            m_bild_3 = "";
            m_bild_4 = "";

            if ( m_antwort_a != null ) { m_antwort_a.clear(); }
            if ( m_antwort_b != null ) { m_antwort_b.clear(); }
            if ( m_antwort_c != null ) { m_antwort_c.clear(); }
            if ( m_antwort_d != null ) { m_antwort_d.clear(); }
            if ( m_antwort_e != null ) { m_antwort_e.clear(); }
            if ( m_antwort_f != null ) { m_antwort_f.clear(); }
            if ( m_antwort_g != null ) { m_antwort_g.clear(); }
            if ( m_antwort_h != null ) { m_antwort_h.clear(); }

            m_antwort_a = null;
            m_antwort_b = null;
            m_antwort_c = null;
            m_antwort_d = null;
            m_antwort_e = null;
            m_antwort_f = null;
            m_antwort_g = null;
            m_antwort_h = null;
        }

        public String toString()
        {
            String str_buffer = "";
            String mycr = "";

            mycr = "\n";

            str_buffer = str_buffer + mycr + "---------- clsFrage ----------";

            str_buffer = str_buffer + mycr + "m_id              >" + m_id + "<";
            str_buffer = str_buffer + mycr + "m_nummer          >" + m_nummer + "<";
            str_buffer = str_buffer + mycr + "m_geltungsbereich >" + m_geltungsbereich + "<";
            str_buffer = str_buffer + mycr + "m_text_1          >" + m_text_1 + "<";
            str_buffer = str_buffer + mycr + "m_text_2          >" + m_text_2 + "<";
            str_buffer = str_buffer + mycr + "m_bemerkung       >" + m_bemerkung + "<";
            str_buffer = str_buffer + mycr + "m_bild_1          >" + m_bild_1 + "<";
            str_buffer = str_buffer + mycr + "m_bild_2          >" + m_bild_2 + "<";
            str_buffer = str_buffer + mycr + "m_bild_3          >" + m_bild_3 + "<";
            str_buffer = str_buffer + mycr + "m_bild_4          >" + m_bild_4 + "<";

            if ( m_antwort_a == null )
            {
                str_buffer = str_buffer + mycr + "m_antwort_a     >nothing<";
            }
            else
            {
                str_buffer = str_buffer + mycr + "m_antwort_a     >" + m_antwort_a.toString() + "<";
            }

            if ( m_antwort_b == null )
            {
                str_buffer = str_buffer + mycr + "m_antwort_b     >nothing<";
            }
            else
            {
                str_buffer = str_buffer + mycr + "m_antwort_b     >" + m_antwort_b.toString() + "<";
            }

            if ( m_antwort_c == null )
            {
                str_buffer = str_buffer + mycr + "m_antwort_c     >nothing<";
            }
            else
            {
                str_buffer = str_buffer + mycr + "m_antwort_c     >" + m_antwort_c.toString() + "<";
            }

            if ( m_antwort_d == null )
            {
                str_buffer = str_buffer + mycr + "m_antwort_d     >nothing<";
            }
            else
            {
                str_buffer = str_buffer + mycr + "m_antwort_d     >" + m_antwort_d.toString() + "<";
            }

            if ( m_antwort_e == null )
            {
                str_buffer = str_buffer + mycr + "m_antwort_e     >nothing<";
            }
            else
            {
                str_buffer = str_buffer + mycr + "m_antwort_e     >" + m_antwort_e.toString() + "<";
            }

            if ( m_antwort_f == null )
            {
                str_buffer = str_buffer + mycr + "m_antwort_f     >nothing<";
            }
            else
            {
                str_buffer = str_buffer + mycr + "m_antwort_f     >" + m_antwort_f.toString() + "<";
            }

            if ( m_antwort_g == null )
            {
                str_buffer = str_buffer + mycr + "m_antwort_g     >nothing<";
            }
            else
            {
                str_buffer = str_buffer + mycr + "m_antwort_g     >" + m_antwort_g.toString() + "<";
            }

            if ( m_antwort_h == null )
            {
                str_buffer = str_buffer + mycr + "m_antwort_h     >nothing<";
            }
            else
            {
                str_buffer = str_buffer + mycr + "m_antwort_h     >" + m_antwort_h.toString() + "<";
            }

            return str_buffer;
        }
    }
}
