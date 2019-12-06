using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class clsAnwFragenFrager
    {
        /*
         * Programmmodus beim Start des Programmes 
         */
        private int MODUS_KEINE_FRAGEN_GELADEN = 1;

        /*
         * Programmmodus, wenn ein Fragenkatalog geladen ist.
         */
        private int MODUS_FRAGEN_KATALOG_ANZEIGEN = 2;

        /*
         * Programmmodus, wenn eine Fragensitzung aktiv ist.
         */
        private int MODUS_ABFRAGEN_LERNFRABRIK = 3;

        /*
         * Speichert den Programmmodus
         */
        private int m_modus = 0;

        /*
         * Instanz fuer die Klasse "clsFragenKatalog"
         * Speichert die Fragen des aktuell geladenen Fragenkataloges
         */
        private clsFragenKatalog m_fragen_katalog = null;

        /*
         * Instanz fuer die Klasse "clsLernFabrik"
         * Speichert die abzufragenden Fragen aus dem Fragenkatalog.
         */
        private clsLernFrabrik m_lern_fabrik = null;

        /*
         * Speichert den Pfad auf die Bild-Dateien.
         */
        private String m_aktuelles_bild_verzeichnis = null;

        /**
         * @return die Instanz fuer die Lernfabrik
         */
        public clsLernFrabrik getLernFrabrik()
        {
            /*
             * Pruefung: Instanz der Lernfabrik vorhanden ?
             */
            if ( m_lern_fabrik == null )
            {
                /*
                 * Ist die Instanz noch null, wird eine neue Instanz
                 * der Klasse ^"clsLernFrabrik" erstellt und der
                 * Membervariablen "m_lern_fabrik" zugewiesen.
                 */
                m_lern_fabrik = new clsLernFrabrik();
            }

            return m_lern_fabrik;
        }

        /**
         * @return die Instanz fuer den Fragenkatalog
         */
        public clsFragenKatalog getFragenKatalog()
        {
            /*
             * Pruefung: Instanz des Fragenkataloges vorhanden ?
             */
            if ( m_fragen_katalog == null )
            {
                /*
                 * Ist die Instanz noch null, wird eine neue 
                 * Instanz der Klasse clsFragenKatalog erstellt und   
                 * der Membervariablen "m_fragen_katalog" zugewiesen.
                 */
                m_fragen_katalog = new clsFragenKatalog();
            }

            return m_fragen_katalog;
        }

        /**
         * @return die Anzahl der Fragen im aktuellen Fragenkatalog. Ist kein Fragenkatalog geladen -1
         */
        public int getAnzahlFragen()
        {
            /*
             * Pruefung: Fragenkatalog-Instanz vorhanden ?
             * 
             * Ist die Fragenkatalog-Instanz vorhanden, wird 
             * dem Aufrufer das Funktionsergebnis der Funktion 
             * "getAnzahlFragen" zurueckgegeben.
             * 
             * Ist kein Fragenkatalog vorhanden, bekommt der 
             * Aufrufder -1 zurueck.
             */
            if ( m_fragen_katalog != null )
            {
                m_fragen_katalog.getAnzahlFragen();
            }

            return -1;
        }

        /**
         * @return TRUE wenn der Fragenkatalog geparst werden konnte, sonst FALSE
         */
        public bool ladeXmlFragenKatalog( String pXmlDateiName, bool pKnzClearFragenkatalog )
        {
            bool fkt_ergebnis = false;

            /*
             * Pruefung: Soll der bestehende Fragenkatalog geloescht werden.
             */
            if ( pKnzClearFragenkatalog )
            {
                /*
                 * Es wird die "clear"-Funktion dieser Klasse gerufen
                 * um einen sauberen Variablen-Stand zu bekommen.
                 */
                clear();
            }
            else
            {
                /*
                 * Wird zu einem bestehendem Fragenkatalog ein weiterer
                 * Fragenkatalog hinzugefuegt, wird dass Kennzeichen 
                 * fuer den Originalzustand auf FALSE gesetzt.
                 * 
                 * (Export Fragenkatalog -> Fragennummer ist dann laufende Nummer)
                 * 
                 */
                getFragenKatalog().setKnzOriginalZustand( false );
            }

            /*
             * Es wird eine Instanz der Klasse "clsFkFragenKatalogLaden" erstellt.
             */
            clsFkFragenKatalogLaden inst_cls_fragen_lader = new clsFkFragenKatalogLaden();

            bool knz_fragenkatalog_geladen = false;

            try
            {
                /*
                 * Es wird die Funktion zum Auswaehlen und Parsen der XML-Daten aufgerufen.
                 * 
                 * Das Ergebnis wird in der Variabeln "knz_fragenkatalog_geladen" gespeichert.
                 * 
                 * Wird beim Parsen eine Exception ausgeloest wird das Funktionsergebnis 
                 * auf FALSE gestellt. Die Datei konnte nicht korrekt geparst werden.
                 */
                knz_fragenkatalog_geladen = inst_cls_fragen_lader.startImportXmlDatei( pXmlDateiName, getFragenKatalog() );
            }
            catch ( Exception err_inst )
            {
                Console.WriteLine( "Fehler: errLaden1\n" + err_inst.Message + "\n" + err_inst.StackTrace );
            }

            inst_cls_fragen_lader = null;

            if ( knz_fragenkatalog_geladen )
            {
                try
                {
                    String currentDirectory = Path.GetDirectoryName( pXmlDateiName );

                    String fullPathOnly = Path.GetFullPath( currentDirectory );

                    m_aktuelles_bild_verzeichnis = fullPathOnly + "\\";
                }
                catch ( Exception err_inst )
                {
                    Console.WriteLine( "Fehler: errLaden2\n" + err_inst.Message + "\n" + err_inst.StackTrace );
                }

                /*
                 * Der Modus wird auf "Fragenkatalog anzeigen" gesetzt.
                 */
                m_modus = MODUS_FRAGEN_KATALOG_ANZEIGEN;

                /*
                 * Das Funktionsergebnis wird auf TRUE gestellt, da jetzt 
                 * alle Aktionen zum laden des Fragenkataloges erledigt sind.
                 */
                fkt_ergebnis = true;
            }

            return fkt_ergebnis;
        }

        /**
         * @return TRUE wenn der Testfragenkatalog erstellt werden konnte, sonst FALSE
         */
        public bool erstelleTestFragenKatalog()
        {
            bool fkt_ergebnis = false;

            /*
             * Es wird die "clear"-Funktion dieser Klasse gerufen
             * um einen sauberen Variablen-Stand zu bekommen.
             */
            clear();

            /*
             * Es wird die Funktion zum erstellen des Testfragenkataloges aufgerufen.
             * 
             * Wird beim Erstellen eine Exception ausgeloest wird 
             * das Funktionsergebnis  auf FALSE gestellt. 
             */
            m_fragen_katalog = fkTestErstellungFragenkatalog.getTestFragenKatalog();

            m_modus = MODUS_FRAGEN_KATALOG_ANZEIGEN;

            fkt_ergebnis = true;

            return fkt_ergebnis;
        }

        /**
         * @return den Pfad auf das aktuelle Verzeichnis fuer Bilder.
         */
        public String getAktuellesBildVerzeichnis()
        {
            return m_aktuelles_bild_verzeichnis;
        }

        /**
         * @param pAktuellesBildVerzeichnis das zu verwendende Bildverzeichnis
         */
        public void setMaktuellesBildVerzeichnis( String pMaktuellesBildVerzeichnis )
        {
            m_aktuelles_bild_verzeichnis = pMaktuellesBildVerzeichnis;
        }

        /**
         * Initialisiert die Anwendungsklasse
         */
        public bool initAnwFragenFrager()
        {
            m_fragen_katalog = null;

            m_lern_fabrik = null;

            m_modus = MODUS_KEINE_FRAGEN_GELADEN;

            return true;
        }

        /**
         * @return TRUE wenn der Modus gleich MODUS_ABFRAGEN_LERNFRABRIK ist, sonst FALSE
         */
        public bool istModusAbfragen()
        {
            return ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK );
        }

        /**
         * @return TRUE wenn der Modus gleich MODUS_FRAGEN_KATALOG_ANZEIGEN ist, sonst FALSE
         */
        public bool istModusFragenKatalogAnzeigen()
        {
            return ( m_modus == MODUS_FRAGEN_KATALOG_ANZEIGEN );
        }

        /**
         * Setzt den Modus auf MODUS_FRAGEN_KATALOG_ANZEIGEN
         */
        public void setModusFragenKatalogAnzeigen()
        {
            m_modus = MODUS_FRAGEN_KATALOG_ANZEIGEN;
        }

        /**
         * Gibt die benutzten Resourcen wieder frei.
         *
         * Ruft die Funktion "clear" beim Fragenkatalog und bei
         * der Lernfabrik auf.
         *
         * Setzt anschliessend alle Membervariablen auf null.
         */
        public void clear()
        {
            /*
             * Ist eine Instanz des Fragenkataloges vorhanden, wird 
             * bei dieser die Funktion "clear" aufgerufen.
             * 
             * Das sorgt fuer die Resourcen-Freigabe der Instanz.
             */
            if ( m_fragen_katalog != null )
            {
                m_fragen_katalog.clear();
            }

            /*
             * Ist eine Instanz der Lernfabrik vorhanden, wird 
             * bei dieser ebenfalls die Funktion "clear" aufgerufen.
             */
            if ( m_lern_fabrik != null )
            {
                m_lern_fabrik.clear();
            }

            /*
             * Es werden die Variablen des Fragenkataloges, der 
             * Lernfabrik und des Bildverzeichnisses auf 
             * "null" gestellt.
             */
            m_fragen_katalog = null;

            m_lern_fabrik = null;

            m_aktuelles_bild_verzeichnis = null;

            /*
             * Der Modus wird auf "MODUS_KEINE_FRAGEN_GELADEN" gestellt.
             */
            m_modus = MODUS_KEINE_FRAGEN_GELADEN;
        }

        /**
         * Ruft je nach Modus die gleichnamige Funktion im Fragenkatalog
         * oder in der Lernfrabrik auf und gibt dessen Funktionsergebnis 
         * zurueck.
         * 
         * Ist der Modus "MODUS_KEINE_FRAGEN_GELADEN" wird FALSE zurueckgegeben.
         * 
         * @return TRUE wenn zur ersten Frage verzweigt werden konnte, sonst FALSE
         */
        public bool moveFirst()
        {
            if ( m_modus == MODUS_FRAGEN_KATALOG_ANZEIGEN )
            {
                return getFragenKatalog().moveFirst();
            }

            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().moveFirst();
            }

            return false;
        }

        /**
         * Ruft je nach Modus die gleichnamige Funktion im Fragenkatalog
         * oder in der Lernfrabrik auf und gibt dessen Funktionsergebnis 
         * zurueck.
         * 
         * Ist der Modus "MODUS_KEINE_FRAGEN_GELADEN" wird FALSE zurueckgegeben.
         * 
         * @return TRUE wenn zur letzten Frage verzweigt werden konnte, sonst FALSE
         */
        public bool moveLast()
        {
            if ( m_modus == MODUS_FRAGEN_KATALOG_ANZEIGEN )
            {
                return getFragenKatalog().moveLast();
            }

            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().moveLast();
            }

            return false;
        }

        /**
         * Ruft je nach Modus die gleichnamige Funktion im Fragenkatalog
         * oder in der Lernfrabrik auf und gibt dessen Funktionsergebnis 
         * zurueck.
         * 
         * Ist der Modus "MODUS_KEINE_FRAGEN_GELADEN" wird FALSE zurueckgegeben.
         * 
         * @return TRUE wenn zur naechsten Frage verzweigt werden konnte, sonst FALSE
         */
        public bool moveNext()
        {
            if ( m_modus == MODUS_FRAGEN_KATALOG_ANZEIGEN )
            {
                return getFragenKatalog().moveNext();
            }

            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().moveNext();
            }

            return false;
        }

        /**
         * Ruft je nach Modus die gleichnamige Funktion im Fragenkatalog
         * oder in der Lernfrabrik auf und gibt dessen Funktionsergebnis 
         * zurueck.
         * 
         * Ist der Modus "MODUS_KEINE_FRAGEN_GELADEN" wird FALSE zurueckgegeben.
         * 
         * @return TRUE wenn zur vorigen Frage verzweigt werden konnte, sonst FALSE
         */
        public bool movePrevious()
        {
            if ( m_modus == MODUS_FRAGEN_KATALOG_ANZEIGEN )
            {
                return getFragenKatalog().movePrevious();
            }

            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().movePrevious();
            }

            return false;
        }

        /**
         * Gibt je nach Modus die aktuelle Frage aus dem Fragenkatalog
         * oder der Lernfabrik zurueck. 
         * 
         * Ist der Modus "MODUS_KEINE_FRAGEN_GELADEN" wird null zurueckgegeben
         * 
         * @return eine Instanz der Klasse "clsFrage" wenn es eine aktuelle Frage gibt, sonst null
         */
        public clsFrage getAktFrage()
        {
            if ( m_modus == MODUS_FRAGEN_KATALOG_ANZEIGEN )
            {
                return getFragenKatalog().getAktuelleFrage();
            }

            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getFragenKatalog().getIndex( getLernFrabrik().getAktAbfrageIndex() );
            }

            return null;
        }

        /**
         * Gibt je nach Modus die Bezeichnung fuer die Anzeige der
         * "Anzahl Fragen" zurueck.
         * 
         * @return einen Text fuer die Anzahl der Fragen
         */
        public String getAnzahlLabel()
        {
            if ( m_modus == MODUS_FRAGEN_KATALOG_ANZEIGEN )
            {
                return "" + getFragenKatalog().getAnzahlFragen();
            }
            else if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().getAnzahlFragen() + " von " + getFragenKatalog().getAnzahlFragen();
            }
            else
            {
                return "0";
            }
        }

        /**
         * Gibt je nach Modus den Text fuer die Statuszeile zurueck.
         * 
         * @return einen Text fuer die Anzeige im Status-Text
         */
        public String getTextStatusFragenIndex()
        {
            if ( m_modus == MODUS_FRAGEN_KATALOG_ANZEIGEN )
            {
                return "MODUS_FRAGEN_KATALOG_ANZEIGEN Index " + getFragenKatalog().getDatenSatzZeiger();
            }
            else if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return "MODUS_FRAGEN_KATALOG_ANZEIGEN Index " + getLernFrabrik().getDatenSatzZeiger() + " = " + getLernFrabrik().getAktAbfrageIndex();
            }
            else
            {
                return "MODUS_KEINE_FRAGEN_GELADEN";
            }
        }

        /**
         * Wechselt in den Modus "MODUS_FRAGEN_KATALOG_ANZEIGEN", wenn ein
         * Fragenkatalog vorhanden ist. Es wird TRUE zurueckgegeben.
         * 
         * Ist kein Fragenkatalog geladen, wird der Modus auf
         * "MODUS_KEINE_FRAGEN_GELADEN" gesetzt und es wird 
         * FALSE zurueckgegeben.
         *
         * @return TRUE wenn in den Modus "MODUS_FRAGEN_KATALOG_ANZEIGEN" gewechselt werden konnte, sonst FALSE.
         */
        public bool startAnzeigeFragenKatalog()
        {
            bool fkt_ergebnis = false;

            m_modus = MODUS_KEINE_FRAGEN_GELADEN;

            if ( m_fragen_katalog != null )
            {
                if ( m_fragen_katalog.getAnzahlFragen() > 0 )
                {
                    m_modus = MODUS_FRAGEN_KATALOG_ANZEIGEN;

                    fkt_ergebnis = true;
                }
            }

            return fkt_ergebnis;
        }

        /**
         * Initialisiert eine Fragensitzung mit den Parameterangaben.
         * 
         * Wechselt in den Modus "MODUS_ABFRAGEN_LERNFRABRIK".
         *
         * Ist kein Fragenkatalog geladen, wird FALSE zurueckgegeben.
         *
         * @return TRUE wenn in den Modus "MODUS_ABFRAGEN_LERNFRABRIK" gewechselt werden konnte, sonst FALSE.
         */
        public bool startAbfrageSitzung( bool pKnzModusZufaellig, int pAnzahlFragen, int pUntergrenze, int pObergrenze )
        {
            if ( m_fragen_katalog != null )
            {
                try
                {
                    if ( getLernFrabrik().initAbfrageSitzung( pKnzModusZufaellig, getFragenKatalog().getAnzahlFragen(), pAnzahlFragen, pUntergrenze, pObergrenze ) )
                    {
                        m_modus = MODUS_ABFRAGEN_LERNFRABRIK;

                        return true;
                    }
                }
                catch ( Exception err_inst )
                {
                    Console.WriteLine( "Fehler: errStartAbfrageSitzung\n" + err_inst.Message ); // + "" + err_inst.StackTrace );
                }
            }

            return false;
        }

        /**
         * Exportiert den Fragenkatalog mit den Parameterkennzeichen
         *
         * Ist kein Fragenkatalog geladen, wird nichts exportiert und FALSE zurueckgegeben.
         *
         * @return TRUE wenn der Export erfolgreich war, sonst FALSE
         */
        public bool exportFrageBogenFragenKatalog( bool pKnzExportiereAntworten, bool pKnzExportiereKorrekteAntworten, bool pKnzExportiereFalscheAntworten, bool pKnzExportiereAntwortBezeichnung, bool pKnzAntwortReihenfolgeUmstellen )
        {
            bool fkt_ergebnis = false; 

            if ( m_modus != MODUS_KEINE_FRAGEN_GELADEN )
            {
                fkt_ergebnis = fkExportFrageBogen.startExportFbFragenKatalog( getFragenKatalog(), pKnzExportiereAntworten, pKnzExportiereKorrekteAntworten, pKnzExportiereFalscheAntworten, pKnzExportiereAntwortBezeichnung, pKnzAntwortReihenfolgeUmstellen );
            }

            return fkt_ergebnis;
        }

        /**
         * Exportiert die aktuelle Fragensitzung mit den Parameterkennzeichen
         *
         * Ist kein Fragenkatalog geladen, wird nichts exportiert und FALSE zurueckgegeben.
         *
         * @return TRUE wenn der Export erfolgreich war, sonst FALSE
         */
        public bool exportFrageBogenLernFabrik( int pExportModus, bool pKnzExportiereAntworten, bool pKnzExportiereKorrekteAntworten, bool pKnzExportiereFalscheAntworten, bool pKnzExportiereAntwortBezeichnung, bool pKnzAntwortReihenfolgeUmstellen ) 
        {
            bool fkt_ergebnis = false;

            if ( m_modus != MODUS_KEINE_FRAGEN_GELADEN )
            {
                fkt_ergebnis = fkExportFrageBogen.startExportFbLernFabrik( getFragenKatalog(), getLernFrabrik(), pExportModus, pKnzExportiereAntworten, pKnzExportiereKorrekteAntworten, pKnzExportiereFalscheAntworten, pKnzExportiereAntwortBezeichnung, pKnzAntwortReihenfolgeUmstellen );
            }

            return fkt_ergebnis;
        }

        /*
         * ################################################################################
         */
        public bool exportFrageBogenLernFabrikXml( int pExportModus )
        {
            bool fkt_ergebnis = false;

            if ( m_modus != MODUS_KEINE_FRAGEN_GELADEN )
            {
                fkt_ergebnis = fkExportXmlFragenKatalog.startExportXmlLernFabrik( getFragenKatalog(), getLernFrabrik(), pExportModus );
            }

            return fkt_ergebnis;
        }

        /*
         * ################################################################################
         */
        public void updateKnzGewaehlt( bool pKnzA, bool pKnzB, bool pKnzC, bool pKnzD, bool pKnzE, bool pKnzF, bool pKnzG, bool pKnzH )
        {
            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                getLernFrabrik().updateKnzGewaehlt( pKnzA, pKnzB, pKnzC, pKnzD, pKnzE, pKnzF, pKnzG, pKnzH );
            }
        }

        /*
         * ################################################################################
         */
        public void updateZaehler( bool pKnzBeantwortet, bool pKnzKorrektBeantwortet, bool pKnzFalschBeantwortet )
        {
            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                getLernFrabrik().updateZaehler( pKnzBeantwortet, pKnzKorrektBeantwortet, pKnzFalschBeantwortet );
            }
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortAGewaehlt()
        {
            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().getKnzAntwortAGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortBGewaehlt()
        {
            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().getKnzAntwortBGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortCGewaehlt()
        {
            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().getKnzAntwortCGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortDGewaehlt()
        {
            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().getKnzAntwortDGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortEGewaehlt()
        {
            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().getKnzAntwortEGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortFGewaehlt()
        {
            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().getKnzAntwortFGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortGGewaehlt()
        {
            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().getKnzAntwortGGewaehlt();
            }

            return false;
        }

        /*
         * ################################################################################
         */
        public bool getKnzAntwortHGewaehlt()
        {
            if ( m_modus == MODUS_ABFRAGEN_LERNFRABRIK )
            {
                return getLernFrabrik().getKnzAntwortHGewaehlt();
            }

            return false;
        }

        /**
         * Leitet die Anforderung an die Klasse "clsFragenKatalog" weiter.
         */
        public void startAntwortReduktion( int pAnzahlFalscheAntwortenJeKorrekterAntwort )
        {
            /*
             * Pruefung: Fragenkatalog vorhanden ?
             *
             * Ist ein Fragenkatalog vorhanden, wird bei diesem die 
             * Funktion "startAntwortReduktion" aufgerufen.
             */
            if ( m_fragen_katalog != null )
            {
                m_fragen_katalog.startAntwortReduktion( pAnzahlFalscheAntwortenJeKorrekterAntwort );
            }
        }

                /**
         * Leitet die Anforderung an die Klasse "clsFragenKatalog" weiter.
         */
        public void startFragenUmstellung()
        {
            /*
             * Pruefung: Fragenkatalog vorhanden ?
             *
             * Ist ein Fragenkatalog vorhanden, wird bei diesem die 
             * Funktion "startAntwortReduktion" aufgerufen.
             */
            if ( m_fragen_katalog != null )
            {
                m_fragen_katalog.startFragenUmstellung();
            }
        }
    }
}
