using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class clsFragenKatalog
    {
        /* 
         * Vektor fuer die Speicherung der Instanzen der Klasse "clsFrage"
         */
        private List<clsFrage> m_frage_vector = null;

        /*
         * Datensatzzeiger zum navigieren durch den Fragenkatalog
         */
        private int m_daten_satz_zeiger = 0;

        /*
         * Speicher fuer den Dateinamen des Fragenkataloges
         */ 
        private String m_datei_name = "";

        /*
         * Kennzeichenvariable, ob sich der Fragenkatalog noch im Originalzustand befindet. 
         * 
         * Originalzustand = Einziger Fragenbestand aus einer geladenen XML-Datei
         * 
         * Werden Fragen aus einer anderen XML-Datei hinzugefuegt, ist es nicht mehr 
         * der originale Zustand, da nun mehr Fragen vorhanden sind. 
         * 
         * Dieses Kennzeichen ist beim Export des Fragenkataloges wichtig.
         * Ist es nicht mehr der Originalzustand, kann fuer den Loesungsbogen 
         * nicht mehr die originale Fragennummerierung genutzt werden.
         * Es kommen evtl. mehrfach dieselben Fragennummern vor, weswegen der 
         * Loesungsbogen nicht mehr eindeutig waere.
         * 
         * In so einem Fall, werden die Fragen einfach neu durchnummeriert.
         * Das erfolgt nur beim Export.
         */
        bool m_knz_original_zustand = true;

        /**
         * @return TRUE = Fragenkatalog liegt im Originalzustand vor, FALSE = es sind mehr Fragenkataloge zusammengefuegt worden
         */
        public bool getKnzOriginalZustand()
        {
            return m_knz_original_zustand;
        }

        /**
         * @param pKnzOriginalZustand das zu setzende Kennzeichen
         */
        public void setKnzOriginalZustand( bool pKnzOriginalZustand )
        {
            m_knz_original_zustand = pKnzOriginalZustand;
        }

        /** 
         * @return den aktuellen Wert des Datensatzzeigers
         */
        public int getDatenSatzZeiger() 
        { 
            return m_daten_satz_zeiger; 
        }

        /** 
         * @return den Dateinamen des Fragenkataloges
         */
        public String getDateiName()
        {
            return m_datei_name;
        }

        /**
         * @param pDateiName der Dateiname des Fragenkataloges
         */
        public void setDateiName( String pDateiName )
        {
            m_datei_name = pDateiName;
        }

        /** 
         * Gibt alle Resourcen frei und setzt die Variablen auf null.
         */
        public void clear()
        {
            /*
             * Pruefung: Variable "m_frage_vector" ungleich null?
             *
             * Ist der Vektor nicht vorhanden, sind auch keine Elemente zum loeschen vorhanden
             */
            if ( m_frage_vector != null )
            {
                int aktueller_index = 0;

                /*
                 * Anzahl der Fragen ermitteln
                 */ 
                int frage_vector_anzahl = getAnzahlFragen();

                /*
                 * While-Schleife ueber alle Vektor-Elemente
                 */
                while ( aktueller_index < frage_vector_anzahl )
                {
                    try
                    {
                        /*
                         * Bei jedem Vektor-Element wird die Funktion "clear" aufgerufen
                         *
                         * Eventuell auftretende Fehler werden abgefangen.
                         */
                        m_frage_vector[ aktueller_index ].clear();
                    }
                    catch ( Exception err_inst )
                    {
                        //
                    }

                    /*
                     * Am Ende der While-Schleife wird der Indexzaehler erhoeht.
                     */
                    aktueller_index++;
                }
            }

            m_datei_name = null;

            /*
             * Der Datensatzzeiger wird auf 0 gestellt.
             */
            m_daten_satz_zeiger = 0;

            /*
             * Am Funktionsende wird die Vektorinstanz auf null gestellt.
             */
            m_frage_vector = null;
        }

        /**
         * Versucht die Instanz aus dem Parameter dem Vektor hinzuzufuegen.
         * 
         * Ist die Parameter-Instanz "null" wird nichts hinzugefuegt und FALSE zurueckgegeben.
         * 
         * @param pClsFrage die hinzuzufuegende Instan
         * 
         * @return TRUE wenn die Instanz dem Vektor hinzugefuegt werden konnte, sonst FALSE 
         */
        public bool addFrage( clsFrage pClsFrage )
        {
            /*
             * Pruefung: Parameterinstanz ungleich "null" ?
             */
            if ( pClsFrage != null )
            {
                try
                {
                    /*
                     * Ist die Parameterinstanz vorhanden, wird diese dem Vektor hinzugefuegt. 
                     */
                    getVektor().Add( pClsFrage );

                    /*
                     * Bei der Frageninstanz wird die laufende Nummer gesetzt.
                     * 
                     * Die Nummer ist in diesem Fall, der Index der hinzugefuegten Frage.
                     * 
                     * Das ist gleichzeitig auch die Gesamtanzahl der Fragen.
                     */
                    pClsFrage.setLfdNummer( "" + getAnzahlFragen() );

                    /*
                     * Der Aufrufder bekommt TRUE zurueck 
                     */
                    return true;
                }
                catch ( Exception err_inst )
                {
                    System.Console.WriteLine( "Fehler: errAddFrage " + err_inst.Message );
                }
            }

            /*
             * Vorgaberueckgabe ist FALSE (Fehler oder Parameterinstanz nicht gesetzt)
             */
            return false;
        }

        /**
         * @return die Vektorinstanz. Ist diese noch nicht vorhanden, wird diese erstellt.
         */
        private List<clsFrage> getVektor()
        {
            /*
             * Pruefung: Vektor noch nicht erstellt ?
             */
            if ( m_frage_vector == null )
            {
                /*
                 * Ist die Vektorinstanz noch null, wird eine neue 
                 * Instanz der Klasse List mit Elementen der Klasse "clsFrage"
                 * erstellt und dem Vektor zugewiesen.
                 */
                m_frage_vector = new List<clsFrage>();
            }

            return m_frage_vector;
        }

        /**
         * @return  die Anzahl der im Vektor gespeicherten Elemente
         */
        public int getAnzahlFragen()
        {
            /*
             * Pruefung: Ist der Vektor vorhanden ?
             * 
             * Ist der Vektor vorhanden, bekommt der Aufrufer den Wert der Funktion "Count" zurueck.
             * 
             * Ist der Vektor noch nicht vorhanden, koennen noch keine Elemente
             * gespeichert worden sein. Der Aufrufer bekommt 0 zurueck.
             * 
             */
            if ( m_frage_vector != null )
            {
                return m_frage_vector.Count;
            }

            return 0;
        }

        /**
         * @return  eine Auflistung der gespeicherten Elemente
         */
        public String getDebugString()
        {
            String erg_str = "";
            String mycr = "\n";

            int aktueller_index = 0;
            int frage_vector_anzahl = getAnzahlFragen();

            while ( aktueller_index < frage_vector_anzahl )
            {
                erg_str = erg_str + mycr + m_frage_vector[ aktueller_index ].getNummer();

                aktueller_index++;
            }

            return erg_str;
        }

        /**
         * @param pIndex der Index
         * @return TRUE wenn der Datensatzzeiger gesetzt werden konnte, sonst false
         */
        public bool moveTo( int pIndex )
        {
            /*
             * Pruefung, ob Daten im Vektor vorhanden sind.
             */
            if ( getAnzahlFragen() > 0 )
            {
                /*
                 * Sind Vektordaten vorhanden, wird der interne Datensatzzeiger auf
                 * den Index aus dem Parameter "pIndex" gestellt.
                 *
                 * Liegt der Parameterwert aussserhalb der Vektorgrenzen, wird der
                 * Datensatzzeiger auf den ersten Index gestellt.
                 */
                if ( ( pIndex >= 0 ) && ( pIndex < getAnzahlFragen() ) )
                {
                    m_daten_satz_zeiger = pIndex;
                }
                else
                {
                    m_daten_satz_zeiger = 0;
                }

                /*
                 * Es wird TRUE zurueckgegeben, wenn alles OK ist
                 */
                return true;
            }

            /*
             * Es wird FALSE zurueckgegeben, wenn keine Daten vorhanden sind
             */
            return false;
        }

        /**
         * Setzt den Datensatzzeiger auf das erste Arrayelement.
         *
         * @return TRUE wenn der Datensatzzeiger auf das erste Arrayelement gestellt werden konnte, sonst FALSE
         */
        public bool moveFirst()
        {
            /*
             * Pruefung: Daten im Vektor vorhanden ?
             */
            if ( getAnzahlFragen() > 0 )
            {
                /*
                 * Ist die Vektoranzahl groesser 0, wird der Datensatzzeiger
                 * auf den ersten Speicherindex im Vektor gesetzt.
                 */
                m_daten_satz_zeiger = 0;

                /*
                 * Es wird TRUE zurueckgegeben
                 */
                return true;
            }

            /*
             * Ist die Vektoranzahl gleich 0, bekommt der Aufrufer FALSE zurueck
             */
            return false;
        }

        /**
         * Setzt den Datensatzzeiger auf das letzte Arrayelement.
         *
         * @return TRUE wenn der Datensatzzeiger auf das letzte Arrayelement gestellt werden konntet, sonst FALSE
         */
        public bool moveLast()
        {
            /*
             * Pruefung: Daten im Vektor vorhanden ?
             */
            if ( getAnzahlFragen() > 0 )
            {
                /*
                 * Ist die Vektoranzahl groesser 0, wird der Datensatzzeiger
                 * auf den letzten Speicherindex im Vektor gesetzt.
                 *
                 * Da die Speicherung im Vektor bei Arrayelement 0 beginnt, ist der
                 * letzte Speicherindex gleich dem Datenzaehler minus 1.
                 */
                m_daten_satz_zeiger = getAnzahlFragen() - 1;

                /*
                 * Es wird TRUE zurueckgegeben
                 */
                return true;
            }

            /*
             * Ist die Vektoranzahl gleich 0, bekommt der Aufrufer FALSE zurueck
             */
            return false;
        }

        /**
         * Setzt den Datensatzzeiger auf das naechste Arrayelement.
         *
         * @return TRUE wenn der Datensatzzeiger veraendert worden ist (dieser auf ein neues Arrayelement zeigt), sonst FALSE
         */
        public bool moveNext()
        {
            /*
             * Pruefung, ob Daten im Vektor vorhanden sind.
             */
            if ( getAnzahlFragen() > 0 )
            {
                /*
                 * "Auto Reset" = TRUE
                 *
                 * 1. Datensatzzeiger um eins erhoehen
                 *
                 * 2. Pruefung, ob das Vektorende ueberschritten worden ist.
                 *    Bei "ja" den Datensatzzeiger auf den ersten
                 *    Speicherindex im Vektor stellen
                 *
                 * 3. Funktionsergebnis auf TRUE stellen
                 */
                m_daten_satz_zeiger++;

                if ( m_daten_satz_zeiger >= getAnzahlFragen() )
                {
                    m_daten_satz_zeiger = 0;
                }

                return true;
            }

            return false;
        }

        /**
         * Setzt den Datensatzzeiger auf das vorherige Arrayelement.
         *
         * @return TRUE wenn der Datensatzzeiger veraendert worden ist (dieser auf ein neues Arrayelement zeigt), sonst FALSE
         */
        public bool movePrevious()
        {
            /*
             * Pruefung, ob Daten im Vektor vorhanden sind.
             */
            if ( getAnzahlFragen() > 0 )
            {
                /*
                 * "Auto Reset" = TRUE
                 *
                 * 1. Datensatzzeiger um eins reduzieren
                 *
                 * 2. Pruefung, ob der Datenzatzzeiger kleiner 0 ist.
                 *    Bei "ja" den Datensatzzeiger auf das letzte Arrayelement stellen.
                 *
                 * 3. Funktionsergebnis auf TRUE stellen
                 */
                m_daten_satz_zeiger--;

                if ( m_daten_satz_zeiger < 0 )
                {
                    m_daten_satz_zeiger = getAnzahlFragen() - 1;
                }

                return true;
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public clsFrage getAktuelleFrage()
        {
            if ( ( m_daten_satz_zeiger >= 0 ) && ( m_daten_satz_zeiger < getAnzahlFragen() ) )
            {
                return m_frage_vector[ m_daten_satz_zeiger ];
            }

            return null;
        }

        /**
         * @return die gespeicherte Instanz am uebergebenen Index, null bei Fehler
         */
        public clsFrage getIndex( int pIndex )
        {
            if ( m_frage_vector != null )
            {
                try
                {
                    return m_frage_vector[ pIndex ];
                }
                catch ( Exception err_inst )
                {
                    //
                }
            }

            return null;
        }

        /**
         * @return die maximale Anzahl der Antworten ueber alle Fragen
         */
        public int getAnzahlMaxVorhandeneAntworten()
        {
            int max_vorhanden = 0;

            int aktueller_index = 0;

            int frage_vector_anzahl = getAnzahlFragen();

            while ( aktueller_index < frage_vector_anzahl )
            {
                max_vorhanden = Math.Max( m_frage_vector[ aktueller_index ].getAnzahlKorrekteAntworten(), max_vorhanden ) ;

                aktueller_index++;
            }

            return max_vorhanden;
        }

        /**
         * @return die maximale Anzahl der korrekten Antworten ueber alle Fragen
         */
        public int getAnzahlMaxKorrekteAntworten()
        {
            int max_vorhanden = 0;

            int aktueller_index = 0;
            
            int frage_vector_anzahl = getAnzahlFragen();

            while ( aktueller_index < frage_vector_anzahl )
            {
                max_vorhanden = Math.Max( m_frage_vector[ aktueller_index ].getAnzahlKorrekteAntworten(), max_vorhanden );

                aktueller_index++;
            }

            return max_vorhanden;
        }

        /**
         * Ruft bei jeder Frage die Funkton "startAntwortReduktion" auf.
         */
        public void startAntwortReduktion(int pAnzahlFalscheAntwortenJeKorrekterAntwort)
        {
            /*
             * Pruefung: Variable "m_calc_zahlung_vector" ungleich null?
             *
             * Ist der Vektor nicht vorhanden, sind auch keine Elemente zum loeschen vorhanden
             */
            if ( m_frage_vector != null )
            {
                int aktueller_index = 0;

                int frage_vector_anzahl = this.getAnzahlFragen();

                /*
                 * While-Schleife ueber alle Vektor-Elemente
                 */
                while ( aktueller_index < frage_vector_anzahl )
                {
                    try
                    {
                        /*
                         * Bei jedem gesetztem Vektor-Element wird die Funktion "startAntwortReduktion" aufgerufen
                         *
                         * Eventuell auftretende Fehler werden abgefangen.
                         */
                        if ( m_frage_vector[ aktueller_index ] != null )
                        {
                            m_frage_vector[ aktueller_index ].startAntwortReduktion( pAnzahlFalscheAntwortenJeKorrekterAntwort );
                        }
                    }
                    catch ( Exception err_inst )
                    {
                        //
                    }

                    /*
                     * Am Ende der While-Schleife wird der Indexzaehler erhoeht.
                     */
                    aktueller_index++;
                }
            }
        }

        /**
         * Vertauscht die Reihenfolge der Fragen im Fragenkatalog.
         * 
         * @return TRUE wenn die Tauschfunktion durchlaufen worden ist, sonst FALSE (=keine Umstellungen)
         */
        public bool startFragenUmstellung()
        {
            /*
             * Ermittlung der Anzahl der vorhandenen Fragen 
             */
            int anzahl_fragen_vektor = getAnzahlFragen();

            /*
             * Pruefung: Anzahl Fragen zu klein ?
             *
             * Die Anzahl der Fragen muss mindestens 3 Fragen fuer eine Vertauschung sein.
             *
             * Sind es weniger Fragen, wird die Funktion mit FALSE verlassen.
             * 
             * Dieses ist auch dann der Fall, wenn kein Fragenvektor vorhanden ist. 
             * Es muss keine Pruefung auf eine Instanz des Vektors gemacht werden, 
             * da diese Pruefung indirekt in der Funktion "getAnzahlFragen" erfolgt.
             */
            if ( anzahl_fragen_vektor < 2 )
            {
                return false;
            }

            /*
             * Definition einer Ober- und Untergrenze fuer die Vertauschungen
             */
            int umstellung_index_obergrenze = anzahl_fragen_vektor;
            int umstellung_index_untergrenze = -1;

            int index_position_neu = 0;
            int index_position_alt = 0;

            int zaehler_such_schleife = 0;
            bool knz_naechster_wert_gefunden = false;

            /*
             * Temporaere Instanz fuer die Vertauschungen der Vektorpositionen
             */
            clsFrage temp_inst_frage = null;


            Random inst_zufallsgenerator = new Random();

            /*
             * While-Schleife ueber alle Fragen.
             *
             * Jede Frage wird einmal in der Position vertauscht.
             */
            while ( ( index_position_alt < anzahl_fragen_vektor ) && ( index_position_alt < 32123 ) )
            {
                /* 
                 * Das Kennzeichen fuer die erfolgreiche Ermittlung eines neuen 
                 * Fragenindexes wird fuer die neue Suche auf FALSE gestellt.
                 */
                knz_naechster_wert_gefunden = false;

                /* 
                 * Der Endlosschleifenverhinderungszaehler wird auf 0 gestellt.
                 */
                zaehler_such_schleife = 0;

                /* 
                 * While-Schleife fuer die Ermittlung eines Fragenindexes
                 */
                while ( ( knz_naechster_wert_gefunden == false ) && ( zaehler_such_schleife < 1000 ) )
                {
                    /*
                     * Neuer Fragenindex
                     * 
                     * Es wird per Zufall ein Index zwischen der Ober- und Untergrenze ausgewaehlt
                     * 
                     * Die Zufallszahl wird mit der Obergrenze multipliziert. 
                     * 
                     * Das stellt sicher, dass der Index nicht groesser als die Obergrenze sein kann.
                     */
                    index_position_neu = inst_zufallsgenerator.Next( 0, umstellung_index_obergrenze );

                    if ( ( umstellung_index_untergrenze > 0 ) && ( index_position_neu < umstellung_index_untergrenze ) )
                    {
                        /*
                         * Einhaltung Untergrenze
                         * 
                         * Ist die Untergrenze groesser als 0, darf der Index nicht 
                         * kleiner als der Index der Untergrenze sein.
                         * 
                         * Ist der Index kleiner als die Untegrenze, ist in diesem 
                         * Durchgang kein neuer gueltiger Fragenindex ermittelt worden.
                         */
                    }
                    else if ( ( umstellung_index_obergrenze >= 0 ) && ( index_position_neu > umstellung_index_obergrenze ) )
                    {
                        /*
                         * Einhaltung Obergrenze
                         * 
                         * Ist die Obergrenze groesser als 0, darf der Index nicht 
                         * groesser als der Index der Obergrenze sein.
                         * 
                         * Ist der Index kleiner als die Obergrenze, ist in diesem 
                         * Durchgang kein neuer gueltiger Fragenindex ermittelt worden.
                         */
                    }
                    else if ( ( m_frage_vector[ index_position_alt ] == null) || ( m_frage_vector[ index_position_neu ] == null ) )
                    {
                        /*
                         * Vermeidung, dass eine der gefundenen Indexpositonen auf "null" steht.
                         */
                    }
                    else
                    {
                        /*
                        * Pruefung: Tauschpaar gefunden ?
                        * 
                        * Ein Tauschpar ist gefunden, wenn 
                        * - die beiden Index-Positionen muessen groesser als 0 sein
                        * - die beiden Index-Positionen duerfen nicht gleich sein
                        */
                        knz_naechster_wert_gefunden = ( ( index_position_neu >= 0 ) && ( index_position_alt >= 0 ) && ( index_position_neu != index_position_alt ) );
                    }

                    /*
                    * Am Schleifenende wid der Schleifenzaehler um eins hochgezaehlt. 
                    */
                    zaehler_such_schleife = zaehler_such_schleife + 1;
                }

                /* 
                 * Pruefung: Neuen Frageindex gefunden?
                 *
                 * Nach der While-Schleife werde nochmals die Bedingungen fuer das
                 * Tauschpaar geprueft. Die Suchschleife koennte auch durch den 
                 * Endlosschleifenverhinderungszaehler beendet worden sein.
                 */
                if ( ( index_position_neu >= 0 ) && ( index_position_alt >= 0 ) && ( index_position_neu != index_position_alt ) )
                {
                    if ( ( m_frage_vector[ index_position_alt ] != null ) && ( m_frage_vector[ index_position_neu ] != null ) )
                    {
                        temp_inst_frage = m_frage_vector[ index_position_alt ];

                        m_frage_vector[ index_position_alt ] = m_frage_vector[ index_position_neu ];

                        m_frage_vector[ index_position_neu ] = temp_inst_frage;
                    }
                }

                /* 
                 * Der Index fuer die alte Position wird um 1 erhoeht
                 */
                index_position_alt = index_position_alt + 1;
            }

            /* 
             * Am Funktionsende wird die temporaere Instanz von "clsFrage"
             * auf "null" gestellt.
             */
            temp_inst_frage = null;

            /* 
             * Es wird TRUE zurueckgegeben, da die Tauschfuntkion durchlaufen worden ist.
             */
            return true;
        }
    }
}
