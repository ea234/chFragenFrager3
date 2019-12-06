using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class clsLernFrabrik
    {
        private List<clsTypAbfrage> m_abfrage_vector = null;

        /*
         * Datensatzzeiger zum navigieren durch die Fragensitzung.
         */ 
        private int m_daten_satz_zeiger = 0;

        /**
         * @return den Index des Datensatzzeigers
         */
        public int getDatenSatzZeiger()
        {
            return m_daten_satz_zeiger;
        }

        /**
         * @return die Vektorinstanz. Ist diese noch nicht vorhanden, wird diese erstellt.
         */
        private List<clsTypAbfrage> getVektor()
        {
            /*
             * Pruefung: Vektor noch nicht erstellt ?
             */
            if ( m_abfrage_vector == null )
            {
                /*
                 * Ist die Vektorinstanz noch null, wird eine neue 
                 * Instanz der Klasse Liste mit Instanzen vom 
                 * Typ "clsTypAbfrage" erstellt und der Vektorvariablen
                 * zugewiesen.
                 */
                m_abfrage_vector = new List< clsTypAbfrage >();
            }

            return m_abfrage_vector;
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
            if ( m_abfrage_vector != null )
            {
                return m_abfrage_vector.Count;
            }

            return 0;
        }

        /**
         * Loescht alle Elemente im Vektor und stellt die Vektorinstanz auf null.
         */
        public void clear()
        {
            /*
             * Pruefung: Variable "m_abfrage_vector" ungleich null ?
             *
             * Ist der Vektor nicht vorhanden, sind auch keine Elemente zum loeschen vorhanden
             */
            if ( m_abfrage_vector != null )
            {
                int aktueller_index = 0;

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
                        m_abfrage_vector[ aktueller_index ].clear();

                        /*
                         * Nachdem die Klasseninstanz fuer sich gecleared wurde, wird  
                         * die Vektorposition auf null gestellt.
                         */
                        m_abfrage_vector[ aktueller_index ] = null;
                    }
                    catch ( Exception err_inst )
                    {
                        // keine Fehlerbehandlung
                    }

                    /*
                     * Am Ende der While-Schleife wird der Indexzaehler erhoeht.
                     */
                    aktueller_index++;
                }
            }

            /*
             * Am Funktionsende wird die Vektorinstanz auf null gestellt.
             */
            m_abfrage_vector = null;
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
                    m_daten_satz_zeiger = 1;
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
         * Setzt den Datensatzzeiger auf das letzte Element.
         *
         * @return TRUE wenn der Datensatzzeiger auf das letzte Element gestellt werden konntet, sonst FALSE
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
             * Pruefung: Daten im Vektor vorhanden ?
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
             * Pruefung: Daten im Vektor vorhanden ?
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

        /**
         * Versucht die Instanz aus dem Parameter dem Vektor hinzuzufuegen.
         * 
         * Ist die Parameter-Instanz "null" wird nichts hinzugefuegt und FALSE zurueckgegeben.
         * 
         * @return TRUE wenn die Instanz dem Vektor hinzugefuegt werden konnte, sonst FALSE 
         */
        public bool addClsTypAbfrage( clsTypAbfrage pClsTypAbfrage )
        {
            /*
             * Pruefung: Parameterinstanz ungleich "null" ?
             */
            if ( pClsTypAbfrage != null )
            {
                try
                {
                    /*
                     * Ist die Parameterinstanz vorhanden, wird diese dem Vektor hinzugefuegt. 
                     */
                    getVektor().Add( pClsTypAbfrage );

                    /*
                     * Der Aufrufder bekommt TRUE zurueck, da die Parameterinstanz hinzugefuegt wurde.
                     */
                    return true;
                }
                catch ( Exception err_inst )
                {
                    System.Console.WriteLine( "Fehler: errAddClsTypAbfrage " + err_inst.Message);
                }
            }

            /*
             * Vorgaberueckgabe ist FALSE (Fehler oder Parameterinstanz nicht gesetzt)
             */
            return false;
        }

        /**
         * Erstellt mit den Parameterangaben eine Fragensitzung nach 
         * Reihenfolge der Fragen im Fragenkatalog.
         * 
         * @return TRUE wenn die Fragensitzung erstellt werden konnte, sonst FALSE
         */
        private bool erstelleFragenSitzungReihenfolge( int pAnzahlGesamtFragen, int pAnzahlAbfragen, int pUntergrenze, int pObergrenze )
        {
            bool fkt_ergebnis        = false;

            int index_fragen_katalog = 0;
            int index_ab             = 0;
            int index_bis            = 0;

            bool knz_rueckwaerts     = false;

            if ( pUntergrenze > pObergrenze )
            {
                index_ab  = pObergrenze;
                index_bis = pUntergrenze;
            }
            else
            {
                index_ab  = pUntergrenze;
                index_bis = pObergrenze;
            }

            if ( index_ab < 0 )
            {
                index_ab = 0;
            }

            if ( index_bis > pAnzahlGesamtFragen )
            {
                index_bis = pAnzahlGesamtFragen - 1;
            }

            clsTypAbfrage inst_cls_abfrage = null;

            if ( knz_rueckwaerts )
            {
                index_fragen_katalog = index_bis;

                while ( index_fragen_katalog >= index_ab )
                {
                    inst_cls_abfrage = new clsTypAbfrage();

                    inst_cls_abfrage.setIndexFragenKatalog( index_fragen_katalog );

                    inst_cls_abfrage.reset();

                    addClsTypAbfrage( inst_cls_abfrage );

                    index_fragen_katalog--;
                }
            }
            else
            {
                index_fragen_katalog = index_ab;

                while ( index_fragen_katalog <= index_bis )
                {
                    inst_cls_abfrage = new clsTypAbfrage();

                    inst_cls_abfrage.setIndexFragenKatalog( index_fragen_katalog );

                    inst_cls_abfrage.reset();

                    addClsTypAbfrage( inst_cls_abfrage );

                    index_fragen_katalog++;
                }
            }

            fkt_ergebnis = true;

            return fkt_ergebnis;
        }


        /**
         * Erstellt mit den Parameterangaben eine Fragensitzung mit einer
         * zufaelligen Reihenfolge der Fragen aus dem Fragenkatalog.
         * 
         * @return TRUE wenn die Fragensitzung erstellt werden konnte, sonst FALSE
         */
        private bool erstelleZufallsFragenSitzung( int pAnzahlGesamtFragen, int pAnzahlAbfragen, int pUntergrenze, int pObergrenze )
        {
            int    index_fragen_katalog          = 0;
            int    index_lern_fabrik             = 0;
            int    zaehler_such_schleife         = 0;
            int    zaehler_reset_vorh_string     = 0;
            String str_bereits_vorhandene_ids = "";
            bool   knz_naechster_wert_gefunden  = false;

            Random inst_zufallsgenerator = new Random();

            clsTypAbfrage inst_cls_abfrage = null;

            zaehler_reset_vorh_string = pAnzahlGesamtFragen;

            index_lern_fabrik = 1;

            while ( ( index_lern_fabrik <= pAnzahlAbfragen ) && ( index_lern_fabrik < 3212 ) )
            {
                /* 
                 * Uebersteigt die Anzahl der Zufallsfragen die Gesamtanzahl aller Fragen,
                 * wird der String mit den bereits aufgenommenen Fragennummern wieder 
                 * auf einen Leerstring gestellt.
                 * 
                 * Ansonsten wuerde die Funktion 100 mal versuchen eine neue Fragennummer
                 * zu ermitteln, welches nicht gehen wuerde, da schon alle Grundfragen im
                 * String vorhanden waeren. Der Prozess wird beschleunigt.
                 */
                if ( index_lern_fabrik > zaehler_reset_vorh_string )
                {
                    str_bereits_vorhandene_ids = "";

                    zaehler_reset_vorh_string = index_lern_fabrik + pAnzahlGesamtFragen;
                }

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
                while ( ( ( knz_naechster_wert_gefunden == false ) && ( zaehler_such_schleife < 1000 ) ) )
                {
                    /*
                     * Neuer Fragenindex
                     * 
                     * Es wird per Zufall ein Index zwischen der Ober- und Untergrenze ausgewaehlt
                     */
                    index_fragen_katalog = inst_zufallsgenerator.Next( 0, pObergrenze );

                    if ( ( pUntergrenze > 0 ) && ( index_fragen_katalog < pUntergrenze ) )
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
                    else if ( ( pObergrenze > 0 ) && ( index_fragen_katalog > pObergrenze ) )
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
                    else
                    {
                        /*
                         * Vermeidung doppelter Fragen
                         *
                         * Es werden alle schon einmal benutzten Indexe in einer Stringvariable 
                         * kommasepariert gespeichert.
                         * 
                         * Es wird geprueft, ob der aktuelle Index sich bereits in diesem 
                         * Stringspeicher befindet. Die Funktion "IndexOf" muss -1 fuer 
                         * "nicht gefunden" zurueck liefern.
                         *
                         * Ist der Index enthalten, ist die Variable "knz_naechster_wert_gefunden" FALSE.
                         * Ist der Index nicht enthalten, ist die Variable "knz_naechster_wert_gefunden" TRUE.
                         *
                         * Die Variable "knz_naechster_wert_gefunden" steuert diese Suchschleife.
                         *
                         * Die Pruefung auf "Index schon vorhanden" wird nur 100 mal gemacht. 
                         * Beim 101 Durchlauf wird der gefundene Fragenindex ungeprueft uebernommen.
                         * Die Variable  "knz_naechster_wert_gefunden" wird dann auf TRUE gestellt.
                         */
                        if ( zaehler_such_schleife < 100 )
                        {
                            knz_naechster_wert_gefunden = str_bereits_vorhandene_ids.IndexOf( "," + index_fragen_katalog + "," ) == -1;
                        }
                        else
                        {
                            knz_naechster_wert_gefunden = true;
                        }
                    }

                    /*
                     * Am Schleifenende wid der Schleifenzaehler um eins hochgezaehlt. 
                     */
                    zaehler_such_schleife = zaehler_such_schleife + 1;
                }

                /* 
                 * Pruefung: Neuen Frageindex gefunden?
                 */
                if ( knz_naechster_wert_gefunden )
                {
                    /*
                     * Ist der aktuelle Fragenindex in Ordnung, wird eine neue Instanz 
                     * der Klasse "clsTypAbfrage" erstellt. 
                     * 
                     * Diese Instanz bekommt den Fragenindex aus dem Fragenkatalog zugewiesen.
                     * 
                     * Auf die Zaehlervariablen wird ein Reset gemacht.
                     */
                    inst_cls_abfrage = new clsTypAbfrage();

                    inst_cls_abfrage.setIndexFragenKatalog( index_fragen_katalog );

                    inst_cls_abfrage.reset();

                    /*
                     * Die Instanz wird dem Vektor fuer die Abfragen hinzugefuegt.
                     */
                    addClsTypAbfrage( inst_cls_abfrage );

                    /*
                     * Der neu aufgenommene Fragenindex wird in der Speichervariablen 
                     * fuer die schon benutzten Fragenindexe aufgenommen.
                     */
                    str_bereits_vorhandene_ids = str_bereits_vorhandene_ids + "," + index_fragen_katalog + ",";
                }

                /* 
                 * Der Index fuer die Lernfabrik wird um 1 erhoeht
                 */
                index_lern_fabrik++;
            }

            return true;
        }

        /**
         * Erstellt mit den Parameterangaben eine Fragensitzung und 
         * liefert zurueck, ob die Erstellung erfolgreich war.
         * 
         * @return TRUE wenn die Fragensitzung erstellt werden konnte, sonst FALSE
         */
        public bool initAbfrageSitzung( bool pKnzModusZufaellig, int pAnzahlGesamtFragen, int pAnzahlFragen, int pUntergrenze, int pObergrenze )
        {
            bool fkt_ergebnis = false;
            int  temp_index    = 0;
            int  anzahl_fragen = pAnzahlFragen;
            int  index_ab      = pUntergrenze;
            int  index_bis     = pObergrenze;

            /* 
             * Pruefung: Anzahl Fragen negativ ?
             * Ist der Parameter "pAnzahlFragen" kleiner 0, wird die Anzahl der 
             * insgesamt aufzunehmenden Fragen auf die Gesamtanzahl der Fragen 
             * gestellt.
             */
            if ( pAnzahlFragen < 0 )
            {
                anzahl_fragen = pAnzahlGesamtFragen;
            }

            /* 
             * Pruefung: "Index Ab" negativ ?
             * Wurde im Parameter "pUntergrenze" ein Wert kleiner 0 uebergeben,
             * wird der Untergrenzen-Index fuer die Ermittlung auf 0 gestellt.
             */
            if ( index_ab < 0 )
            {
                index_ab = 0;
            }

            /* 
             * Pruefung: "Index Bis" negativ ?
             * Wurde im Parameter "pObergrenze" ein Wert kleiner 0 uebergeben,
             * wird der Obergrenzen-Index fuer die Ermittlung auf die 
             * Gesamtanzahl aller Fragen gestellt.
             */
            if ( index_bis < 0 )
            {
                index_bis = pAnzahlGesamtFragen - 1;
            }

            /* 
             * Pruefung: Index-Ab groesser Index-Bis
             * 
             * Liegt der Untergrenzen-Index nach dem Obergrenzen-Index, wird 
             * die Reihenfolge wieder korrigiert.
             */
            if ( index_ab > index_bis )
            {
                temp_index = index_ab;
                index_ab   = index_bis;
                index_bis  = temp_index;
            }

            if ( anzahl_fragen > 0 )
            {
                /* 
                 * Der bisherige Vektor wird mit der "Clear"-Funktion geloescht.
                 */
                clear();

                /* 
                 * Der Datensatzzeiger wird auf 0 gestellt. (Erste Frage)
                 */
                m_daten_satz_zeiger = 0;

                /* 
                 * Erstellung Fragensitzung
                 * 
                 * Es wird geprueft, ob die Fragensitzung nach der Reihenfolge oder 
                 * nach einer zufaelligen Reihenfolge erstellt werden soll. 
                 * 
                 * Dem entsprechend werden die Funktionen fuer die Erstellung der 
                 * Fragensitzung aufgerufen.
                 * 
                 * Dessen Funktionsergebnis wird dem Aufrufer zurueckgegeben.
                 */
                if ( pKnzModusZufaellig )
                {
                    fkt_ergebnis = erstelleZufallsFragenSitzung( pAnzahlGesamtFragen, anzahl_fragen, index_ab, index_bis );
                }
                else
                {
                    fkt_ergebnis = erstelleFragenSitzungReihenfolge( pAnzahlGesamtFragen, anzahl_fragen, index_ab, index_bis );
                }
            }

            return fkt_ergebnis;
        }

        /**
         * @return den Index der abzufragenden Frage im Fragenkatalog, -1 wenn kein Index vorhanden ist
         */
        public int getAktAbfrageIndex()
        {
            /*
             * Pruefung: Datensatzzeiger in Arraygrenzen ?
             */
            if ( ( m_daten_satz_zeiger >= 0 ) && ( m_daten_satz_zeiger < getAnzahlFragen() ) ) 
            {
                /* 
                 * Im Abfragevektor wird mit dem Datensatzzeiger die aktuelle 
                 * Instanz der Klasse "clsAbfrageTyp" referenziert.
                 * 
                 * An dieser Instanz wird die Funktion "getIndexFragenKatalog" aufgerufen.
                 * 
                 * Das Ergebnis ist der Index der anzuzeigenden Frage im aktuellen Fragenkatalog
                 * der Frage erstellt worden sein kann.
                 */
                return m_abfrage_vector[ m_daten_satz_zeiger ].getIndexFragenKatalog();
            }

            /*
             * Vorgaberueckgabe ist -1
             */
            return -1;
        }

        /*
         * ################################################################################
         */
        public int getAbfrageIndexKorrekt( int pIndex )
        {
            if ( ( pIndex >= 0 ) && ( pIndex < getAnzahlFragen() ) )
            {
                /* 
                 * Abfrage auf "Anzahl korrekt beantwortet" eigentlich ueberfluessig,
                 * da das Kennzeichen "Korrekt beantwortet" nur durch eine Beantwortung
                 * der Frage erstellt worden sein kann.
                 */
                if ( m_abfrage_vector[ pIndex ].getAnzahlKorrektBeantwortet() > 0 )
                {
                    if ( m_abfrage_vector[ pIndex ].getKnzFrageLetzteAntwortKorrekt() )
                    {
                        return m_abfrage_vector[ pIndex ].getIndexFragenKatalog();
                    }
                }
            }

            return -1;
        }

        /*
         * ################################################################################
         */
        public int getAbfrageIndexFalsch( int pIndex )
        {
            if ( ( pIndex >= 0 ) && ( pIndex < getAnzahlFragen() ) )
            {
                if ( ( m_abfrage_vector[ pIndex ].getAnzahlFalschBeantwortet() > 0 ) && ( m_abfrage_vector[ pIndex ].getKnzFrageLetzteAntwortFalsch() ) )
                {
                    return m_abfrage_vector[ pIndex ].getIndexFragenKatalog();
                }
            }

            return -1;
        }

        /*
         * ################################################################################
         */
        public int getAbfrageIndex( int pIndex )
        {
            if ( ( pIndex >= 0 ) && ( pIndex < getAnzahlFragen() ) )
            {
                return m_abfrage_vector[ pIndex ].getIndexFragenKatalog();
            }

            return -1;
        }

        /*
         * ################################################################################
         */
        public void updateKnzGewaehlt( bool pKnzA, bool pKnzB, bool pKnzC, bool pKnzD, bool pKnzE, bool pKnzF, bool pKnzG, bool pKnzH )
        {
            if ( ( m_daten_satz_zeiger >= 0 ) && ( m_daten_satz_zeiger < getAnzahlFragen() ) )
            {
                m_abfrage_vector[ m_daten_satz_zeiger ].setKnzAntwortAGewaehlt( pKnzA );
                m_abfrage_vector[ m_daten_satz_zeiger ].setKnzAntwortBGewaehlt( pKnzB );
                m_abfrage_vector[ m_daten_satz_zeiger ].setKnzAntwortCGewaehlt( pKnzC );
                m_abfrage_vector[ m_daten_satz_zeiger ].setKnzAntwortDGewaehlt( pKnzD );
                m_abfrage_vector[ m_daten_satz_zeiger ].setKnzAntwortEGewaehlt( pKnzE );
                m_abfrage_vector[ m_daten_satz_zeiger ].setKnzAntwortFGewaehlt( pKnzF );
                m_abfrage_vector[ m_daten_satz_zeiger ].setKnzAntwortGGewaehlt( pKnzG );
                m_abfrage_vector[ m_daten_satz_zeiger ].setKnzAntwortHGewaehlt( pKnzH );
            }
        }

        /*
         * ################################################################################
         */
        public void updateZaehler( bool pKnzBeantwortet, bool pKnzKorrektBeantwortet, bool pKnzFalschBeantwortet )
        {
            if ( ( m_daten_satz_zeiger >= 0 ) && ( m_daten_satz_zeiger < getAnzahlFragen() ) )
            {
                if ( pKnzBeantwortet )
                {
                    m_abfrage_vector[ m_daten_satz_zeiger ].setKnzFrageLetzteAntwortKorrekt( pKnzKorrektBeantwortet );

                    m_abfrage_vector[ m_daten_satz_zeiger ].incAnzahlBeantwortetJa();

                    if ( pKnzKorrektBeantwortet )
                    {
                        m_abfrage_vector[ m_daten_satz_zeiger ].incAnzahlKorrektBeantwortet();
                    }

                    if ( pKnzFalschBeantwortet )
                    {
                        m_abfrage_vector[ m_daten_satz_zeiger ].incAnzahlFalschBeantwortet();
                    }

                    System.Console.WriteLine( m_abfrage_vector[ m_daten_satz_zeiger ].ToString() );
                }
                else
                {
                    m_abfrage_vector[ m_daten_satz_zeiger ].incAnzahlBeantwortetNein();

                    m_abfrage_vector[ m_daten_satz_zeiger ].setKnzFrageLetzteAntwortKorrekt( false );
                }
            }
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortAGewaehlt()
        {
            if ( ( m_daten_satz_zeiger >= 0 ) && ( m_daten_satz_zeiger < getAnzahlFragen() ) )
            {
                return m_abfrage_vector[ m_daten_satz_zeiger ].getKnzAntwortAGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortBGewaehlt()
        {
            if ( ( m_daten_satz_zeiger >= 0 ) && ( m_daten_satz_zeiger < getAnzahlFragen() ) )
            {
                return m_abfrage_vector[ m_daten_satz_zeiger ].getKnzAntwortBGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortCGewaehlt()
        {
            if ( ( m_daten_satz_zeiger >= 0 ) && ( m_daten_satz_zeiger < getAnzahlFragen() ) )
            {
                return m_abfrage_vector[ m_daten_satz_zeiger ].getKnzAntwortCGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortDGewaehlt()
        {
            if ( ( m_daten_satz_zeiger >= 0 ) && ( m_daten_satz_zeiger < getAnzahlFragen() ) )
            {
                return m_abfrage_vector[ m_daten_satz_zeiger ].getKnzAntwortDGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortEGewaehlt()
        {
            if ( ( m_daten_satz_zeiger >= 0 ) && ( m_daten_satz_zeiger < getAnzahlFragen() ) )
            {
                return m_abfrage_vector[ m_daten_satz_zeiger ].getKnzAntwortEGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortFGewaehlt()
        {
            if ( ( m_daten_satz_zeiger >= 0 ) && ( m_daten_satz_zeiger < getAnzahlFragen() ) )
            {
                return m_abfrage_vector[ m_daten_satz_zeiger ].getKnzAntwortFGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortGGewaehlt()
        {
            if ( ( m_daten_satz_zeiger >= 0 ) && ( m_daten_satz_zeiger < getAnzahlFragen() ) )
            {
                return m_abfrage_vector[ m_daten_satz_zeiger ].getKnzAntwortGGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortHGewaehlt()
        {
            if ( ( m_daten_satz_zeiger >= 0 ) && ( m_daten_satz_zeiger < getAnzahlFragen() ) )
            {
                return m_abfrage_vector[ m_daten_satz_zeiger ].getKnzAntwortHGewaehlt();
            }

            return false;
        }
    }
}
