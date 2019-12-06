using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class fkExportFrageBogen
    {
        public static int EXPORT_LERN_FABRIK_ALLES   = 1;
        public static int EXPORT_LERN_FABRIK_KORREKT = 2;
        public static int EXPORT_LERN_FABRIK_FALSCH  = 3;

        private static bool KNZ_LOESUNGSBOGEN_VERSON_1 = false;
        private static int  VORGABE_ANZ_STELLEN = 74;
        private static int  ANZ_STELLEN_FRAGENNR = 8;
        private static int  ANZ_STELLEN_ANTWORTBEZEICHNUNG = 8;

        private static String LEERZEICHEN = " ";
        private static String NEW_LINE = "\n";
        private static String STR_TRENN_STRING = " - ";
        private static String ALTERNATIVE_ANTWORT_BEZEICHNUNG = "#)";
        private static String ABSTAND_FBEZ_ANTWORT = " ";
        private static String ABSTAND_FNR_FRAGE = " ";
        private static String VORGABE_KORREKT_MARKIERUNG = "**";

        private static String LOESUNGSBOGEN_ANTWORT_A = "A";
        private static String LOESUNGSBOGEN_ANTWORT_B = "B";
        private static String LOESUNGSBOGEN_ANTWORT_C = "C";
        private static String LOESUNGSBOGEN_ANTWORT_D = "D";
        private static String LOESUNGSBOGEN_ANTWORT_E = "E";
        private static String LOESUNGSBOGEN_ANTWORT_F = "F";
        private static String LOESUNGSBOGEN_ANTWORT_G = "G";
        private static String LOESUNGSBOGEN_ANTWORT_H = "H";

        private static String m_loesungsbogen_antwort_a = "";
        private static String m_loesungsbogen_antwort_b = "";
        private static String m_loesungsbogen_antwort_c = "";
        private static String m_loesungsbogen_antwort_d = "";
        private static String m_loesungsbogen_antwort_e = "";
        private static String m_loesungsbogen_antwort_f = "";
        private static String m_loesungsbogen_antwort_g = "";
        private static String m_loesungsbogen_antwort_h = "";

        private static bool m_knz_loesungsbogen_version_1 = false;

        private static bool knz_use_lfd_nr = false;

        private static int m_max_anzahl_vorhandene_antworten = 0;
        private static int m_max_anzahl_korrekte_antworten = 0;

        private static String m_einzug_frage = null;
        private static String m_einzug_antwort = null;

        /* 
         * ################################################################################
         */
        private static void initLoesungsbogenVar( bool pKnzLoesungsbogenVersion1, clsFragenKatalog pFragenKatalog )
        {
            m_knz_loesungsbogen_version_1 = pKnzLoesungsbogenVersion1;

            if ( m_knz_loesungsbogen_version_1 )
            {
                m_loesungsbogen_antwort_a = LOESUNGSBOGEN_ANTWORT_A;
                m_loesungsbogen_antwort_b = LOESUNGSBOGEN_ANTWORT_B;
                m_loesungsbogen_antwort_c = LOESUNGSBOGEN_ANTWORT_C;
                m_loesungsbogen_antwort_d = LOESUNGSBOGEN_ANTWORT_D;
                m_loesungsbogen_antwort_e = LOESUNGSBOGEN_ANTWORT_E;
                m_loesungsbogen_antwort_f = LOESUNGSBOGEN_ANTWORT_F;
                m_loesungsbogen_antwort_g = LOESUNGSBOGEN_ANTWORT_G;
                m_loesungsbogen_antwort_h = LOESUNGSBOGEN_ANTWORT_H;
            }
            else
            {
                m_loesungsbogen_antwort_a = "X";
                m_loesungsbogen_antwort_b = "X";
                m_loesungsbogen_antwort_c = "X";
                m_loesungsbogen_antwort_d = "X";
                m_loesungsbogen_antwort_e = "X";
                m_loesungsbogen_antwort_f = "X";
                m_loesungsbogen_antwort_g = "X";
                m_loesungsbogen_antwort_h = "X";
            }

            m_max_anzahl_vorhandene_antworten = pFragenKatalog.getAnzahlMaxVorhandeneAntworten();

            m_max_anzahl_korrekte_antworten = pFragenKatalog.getAnzahlMaxKorrekteAntworten();

            knz_use_lfd_nr = pFragenKatalog.getKnzOriginalZustand() == false;

            m_loesungsbogen_antwort_a = LOESUNGSBOGEN_ANTWORT_A;
            m_loesungsbogen_antwort_b = LOESUNGSBOGEN_ANTWORT_B;
            m_loesungsbogen_antwort_c = LOESUNGSBOGEN_ANTWORT_C;
            m_loesungsbogen_antwort_d = LOESUNGSBOGEN_ANTWORT_D;
            m_loesungsbogen_antwort_e = LOESUNGSBOGEN_ANTWORT_E;
            m_loesungsbogen_antwort_f = LOESUNGSBOGEN_ANTWORT_F;
            m_loesungsbogen_antwort_g = LOESUNGSBOGEN_ANTWORT_G;
            m_loesungsbogen_antwort_h = LOESUNGSBOGEN_ANTWORT_H;

            m_einzug_frage   = fkString.right( "                                                  ", ANZ_STELLEN_FRAGENNR           ) + ABSTAND_FNR_FRAGE;
            m_einzug_antwort = fkString.right( "                                                  ", ANZ_STELLEN_ANTWORTBEZEICHNUNG ) + ABSTAND_FBEZ_ANTWORT;
        }

        /* 
         * ################################################################################
         */
        public static bool startExportFbFragenKatalog(clsFragenKatalog pFrageKatalog, bool pKnzExportiereAntworten, bool pKnzExportiereKorrekteAntworten, bool pKnzExportiereFalscheAntworten, bool pKnzExportiereAntwortBezeichnung, bool pKnzAntwortReihenfolgeUmstellen)
        {
            bool fkt_ergebnis = false;

            /*
             * Pruefung: Parameter "pFragenKatalog" gleich "null" ?
             *
             * Wurde kein Fragenkatalog uebergeben, wird die Funktion mit FALSE verlassen.
             */
            if ( pFrageKatalog == null )
            {
                return fkt_ergebnis;
            }

            /*
             * Pruefung: Sind Fragen vorhanden ?
             * 
             * Sind im Fragenkatalog keine Fragen vorhanden, wird die Funktion mit FALSE verlassen.
             */
            if ( pFrageKatalog.getAnzahlFragen() == 0 )
            {
                return fkt_ergebnis;
            }

            /* 
             * Letzter Dateiname
             * Aus der INI-Datei wird der Name der letzte Exportname geholt.
             * Dieser Name erscheint dann als Vorauswahl in der Dialog-Box.
             */
            String datei_name = fkIniDatei.readIniDateiName( "DATEI_NAME_EXPORT_FRAGENKATALOG" );

            /* 
             * Dateifilter
             * Die zur Auswahl stehenden Datei-Erweiterungen werden als String initialisiert.
             */
            String datei_filter = "TXT-Datei (*.txt)\0*.txt\0alle Dateien (*.*)\0*.*\0";

            /* 
             * Aufruf der Dateiauswahl-Dialog-Box
             */
            //datei_name = "c:\\Daten\\fragenkatalog.txt";// fkCommonDialog.getSaveName(datei_filter, "txt", "c:\\", datei_name, "Exportdatei wählen");
            datei_name = fkCommonDialog.getSaveName( datei_filter, "txt", "c:\\", datei_name, "Exportdatei wählen" );

            if ( datei_name != null )
            {
                /* 
                 * Aufruf der Initialisierungsfunktion
                 */
                initLoesungsbogenVar( KNZ_LOESUNGSBOGEN_VERSON_1, pFrageKatalog );

                fkIniDatei.writeIniDateiName( "DATEI_NAME_EXPORT_FRAGENKATALOG", datei_name );

                /* 
                 * Aufruf der Exportfunktion fuer den Fragenkatalog
                 */
                fkt_ergebnis = exportTextFragenKatalog( pFrageKatalog, datei_name, pKnzExportiereAntworten, pKnzExportiereKorrekteAntworten, pKnzExportiereFalscheAntworten, pKnzExportiereAntwortBezeichnung, pKnzAntwortReihenfolgeUmstellen );
            }

            return fkt_ergebnis;
        }

        /*
         * ################################################################################
         */
        private static bool exportTextFragenKatalog(clsFragenKatalog pFragenKatalog, String pDateiName, bool pKnzExportiereAntworten, bool pKnzExportiereKorrekteAntworten, bool pKnzExportiereFalscheAntworten, bool pKnzExportiereAntwortBezeichnung, bool pKnzAntwortReihenfolgeUmstellen)
        {
            clsStringArray lb_reihenfolge = new clsStringArray();

            lb_reihenfolge.addString( "000000000000000000000000Dummyzeile" );

            clsFrage temp_frage = null;

            String string_datei_inhalt = "";

            string_datei_inhalt += "Export Abfrage Sitzung " + DateTime.Now.ToString( "dd.MM.yyyy HH:mm:ss" ) + " - " + pFragenKatalog.getDateiName() + NEW_LINE;

            bool pKnzErstelleLoesungsbogen = true;

            bool fkt_ergebnis = true;

            int index_fragen_katalog = 0;

            /*
             * While-Schleife ueber alle Fragen im Fragenkatalog.
             */
            while ( index_fragen_katalog < pFragenKatalog.getAnzahlFragen() )
            {
                try
                {
                    /* 
                     * Frage am aktuellem Index aus dem Fragenkatalog holen.
                     */
                    temp_frage = pFragenKatalog.getIndex( index_fragen_katalog );

                    /* 
                     * Pruefung: Frage gesetzt ?
                     * 
                     * Ist an der aktuellen Indexpositon keine Frage vorhanden, ist das Ergebnis
                     * der Funktion "getIndes" gleich "null". In so einem Fall wird mit dem
                     * naechsten Index weiter gemacht.
                     *
                     * Ist eine Frage vorhanden, wird diese exportiert.
                     */
                    if ( temp_frage != null )
                    {
                        string_datei_inhalt += getFrageExportString( temp_frage, NEW_LINE, pKnzExportiereAntworten, pKnzExportiereKorrekteAntworten, pKnzExportiereFalscheAntworten, pKnzExportiereAntwortBezeichnung, pKnzAntwortReihenfolgeUmstellen );

                        lb_reihenfolge.addString( fkString.right( "000000000000000000000" + ( knz_use_lfd_nr ? temp_frage.getLfdNummer() : temp_frage.getNummer() ), 20 ) + getKorrektStringFrageX( temp_frage ) );
                    }
                }
                catch ( Exception err_inst )
                {
                    Console.WriteLine( "Fehler: errExportTextFragenKatalog\n" + err_inst.Message + "\n\n" + err_inst.StackTrace );
                }

                /* 
                 * Index der Fragen im Fragenkatalog um eins erhoehen und mit 
                 * der naechsten Frage weitermachen.
                 */
                index_fragen_katalog++;
            }

            /*
             * Erstellung des Loesungsbogens
             * Es wird die Funktion fuer die Erstellung des Loesungsbogens aufgerufen.
             */
            string_datei_inhalt += getSringLoesungsbogen( pKnzErstelleLoesungsbogen, lb_reihenfolge );

            /*
             * Stringarray mit den Daten fuer den Loesungsbogen "clearen"
             * und anschliessend auf "null" stellen.
             */
            lb_reihenfolge.clear();

            lb_reihenfolge = null;

            /*
             * Der erstellte Fragenkatalog wird in der Datei gespeichert
             */
            System.IO.File.WriteAllText( pDateiName, string_datei_inhalt );

            return fkt_ergebnis;
        }

        /* 
         * ################################################################################
         */
        public static bool startExportFbLernFabrik( clsFragenKatalog pFrageKatalog, clsLernFrabrik pLernFrabrik, int pExportModus, bool pKnzExportiereAntworten, bool pKnzExportiereKorrekteAntworten, bool pKnzExportiereFalscheAntworten, bool pKnzExportiereAntwortBezeichnung, bool pKnzAntwortReihenfolgeUmstellen )
        {
            bool fkt_ergebnis = false;

            /*
             * Pruefung: Parameter "pFragenKatalog" gleich "null" ?
             *
             * Wurde kein Fragenkatalog uebergeben, wird die Funktion mit FALSE verlassen.
             */
            if ( pFrageKatalog == null )
            {
                return fkt_ergebnis;
            }

            /*
             * Pruefung: Parameter "pLernFrabrik" gleich "null" ?
             *
             * Wurde keine Lernfrabrik uebergeben, wird die Funktion mit FALSE verlassen.
             */
            if ( pLernFrabrik == null )
            {
                return fkt_ergebnis;
            }

            /*
             * Pruefung: Sind Fragen im Fragenkatalog vorhanden ?
             * 
             * Sind im Fragenkatalog keine Fragen vorhanden, wird die Funktion mit FALSE verlassen.
             */
            if ( pFrageKatalog.getAnzahlFragen() == 0 )
            {
                return fkt_ergebnis;
            }

            /*
             * Pruefung: Sind Fragen in der Lernfabrik vorhanden ?
             * 
             * Sind in der Lernfabrik keine Fragen vorhanden, wird die Funktion mit FALSE verlassen.
             */
            if ( pLernFrabrik.getAnzahlFragen() == 0 )
            {
                return fkt_ergebnis;
            }

            /* 
             * Letzter Dateiname
             * Aus der INI-Datei wird der Name der letzte Exportname geholt.
             * Dieser Name erscheint dann als Vorauswahl in der Dialog-Box.
             */
            String datei_name = fkIniDatei.readIniDateiName( "DATEI_NAME_EXPORT_FB_LERN_FABRIK" );

            /* 
             * Dateifilter
             * Die zur Auswahl stehenden Datei-Erweiterungen werden als String initialisiert.
             */
            String datei_filter = "TXT-Datei ( *.txt )\0*.txt\0alle Dateien ( *.* )\0*.*\0";

            /* 
             * Aufruf der Dateiauswahl-Dialog-Box
             */
            datei_name = fkCommonDialog.getSaveName( datei_filter, "txt", "c:\\", datei_name, "Exportdatei wählen" );

            if ( datei_name != null )
            {
                /* 
                 * Aufruf der Initialisierungsfunktion
                 */
                initLoesungsbogenVar( KNZ_LOESUNGSBOGEN_VERSON_1, pFrageKatalog );

                fkIniDatei.writeIniDateiName( "DATEI_NAME_EXPORT_FB_LERN_FABRIK", datei_name );

                fkt_ergebnis = exportTextLernFabrik( pFrageKatalog, pLernFrabrik, pExportModus, datei_name, pKnzExportiereAntworten, pKnzExportiereKorrekteAntworten, pKnzExportiereFalscheAntworten, pKnzExportiereAntwortBezeichnung, pKnzAntwortReihenfolgeUmstellen );
            }

            return fkt_ergebnis;
        }

        /*
         * ################################################################################
         */
        private static bool exportTextLernFabrik(clsFragenKatalog pFragenKatalog, clsLernFrabrik pLernFrabrik, int pExportModus, String pDateiName, bool pKnzExportiereAntworten, bool pKnzExportiereKorrekteAntworten, bool pKnzExportiereFalscheAntworten, bool pKnzExportiereAntwortBezeichnung, bool pKnzAntwortReihenfolgeUmstellen)
        {
            clsStringArray lb_reihenfolge = new clsStringArray();

            lb_reihenfolge.addString( "000000000000000000000000Dummyzeile" );

            clsFrage temp_frage = null;
            String string_datei_inhalt = "";

            string_datei_inhalt += "Export Abfrage Sitzung " + DateTime.Now.ToString( "dd.MM.yyyy HH:mm:ss" ) + " - " + pFragenKatalog.getDateiName() + NEW_LINE;

            bool fkt_ergebnis = true;
            int index_fragen_katalog = 0;
            int index_lern_fabrik = 0;

            /*
             * While-Schleife ueber alle Fragen der Lernfabrik
             */
            while ( index_lern_fabrik < pLernFrabrik.getAnzahlFragen() )
            {
                /* 
                 * Bestimmung: Index Fragenkatalog
                 * 
                 * Es gibt 3 unterschiedliche Exportarten:
                 * - alle korrekt beantworteten Fragen
                 * - alle falsch beantworteten Fragen
                 * - alle Fragen
                 * 
                 * Je nach Exportmodus wird die entsprechende Funktion in der Lernfabrik aufgerufen, 
                 * welche den Index der naechsten zu exportierenden Frage ermittelt.
                 */
                if ( pExportModus == EXPORT_LERN_FABRIK_KORREKT )
                {
                    index_fragen_katalog = pLernFrabrik.getAbfrageIndexKorrekt( index_lern_fabrik );
                }
                else if ( pExportModus == EXPORT_LERN_FABRIK_FALSCH )
                {
                    index_fragen_katalog = pLernFrabrik.getAbfrageIndexFalsch( index_lern_fabrik );
                }
                else
                {
                    index_fragen_katalog = pLernFrabrik.getAbfrageIndex( index_lern_fabrik );
                }

                // System.Console.WriteLine(  "index_fragen_katalog =>" + index_fragen_katalog + "<  index_lern_fabrik =>" + index_lern_fabrik + "<" );

                /* 
                 * Pruefung: Index im Fragenkatalog vorhanden ?
                 * 
                 * Der Index fuer die Frage aus dem Fragenkatalog muss groesser gleich 0 sein.
                 * 
                 * Ist der Index kleiner als 0, wird nichts exportiert. 
                 */
                if ( index_fragen_katalog >= 0 )
                {
                    try
                    {
                        /* 
                         * Aus dem Fragenkatalog wird die Frage am ermittelten Index geholt.
                         */
                        temp_frage = pFragenKatalog.getIndex( index_fragen_katalog );

                        /* 
                         * Ist eine Frage vorhanden, wird diese exportiert.
                         */
                        if ( temp_frage != null )
                        {
                            string_datei_inhalt += getFrageExportString( temp_frage, NEW_LINE, pKnzExportiereAntworten, pKnzExportiereKorrekteAntworten, pKnzExportiereFalscheAntworten, pKnzExportiereAntwortBezeichnung, pKnzAntwortReihenfolgeUmstellen );

                            lb_reihenfolge.addString( fkString.right( "000000000000000000000" + ( knz_use_lfd_nr ? temp_frage.getLfdNummer() : temp_frage.getNummer() ), 20) + getKorrektStringFrageX( temp_frage ) );
                        }
                    }
                    catch ( Exception err_inst )
                    {
                        Console.WriteLine( "Fehler: errExportTextLernFabrik\n" + err_inst.Message + "\n\n" + err_inst.StackTrace );
                    }
                }

                /* 
                 * Es wird der Index fuer die Lernfabrik um eins erhoeht und mit der 
                 * naechsten Frage aus der Lernsitzung weitergemacht.
                 */
                index_lern_fabrik++;
            }

            /*
             * Erstellung des Loesungsbogens
             * Es wird die Funktion fuer die Erstellung des Loesungsbogens aufgerufen.
             */
            bool pKnzErstelleLoesungsbogen = true;

            string_datei_inhalt += getSringLoesungsbogen( pKnzErstelleLoesungsbogen, lb_reihenfolge );

            /*
             * Stringarray mit den Daten fuer den Loesungsbogen "clearen"
             * und anschliessend auf "null" stellen.
             */
            lb_reihenfolge.clear();

            lb_reihenfolge = null;

            /*
             * Der erstellte Fragenkatalog wird in der Datei gespeichert
             */
            System.IO.File.WriteAllText(pDateiName, string_datei_inhalt);

            return fkt_ergebnis;
        }

        /*
         * ################################################################################
         */
        public static String getClipBoardExportString( clsFrage pFrage )
        {
            return getFrageExportString( pFrage, "\n\r", true, true, true, true , true );
        }

        /* 
         * ################################################################################
         */
        public static String getClipBoardTestString()
        {
            String j_str = " Punkt im Bereich der Profilnase, an dem sich die Luft staut und sich die Stroemung nach oben und unten teilt";

            return fkString.getStringMaxCols( j_str, 74, "### - Einzug - ####", "\n\r" );
        }

        /*
         * ################################################################################
         */
        private static String getFrageExportString(clsFrage pFrage, String pNewLineZeichen, bool pKnzExportiereAntworten, bool pKnzExportiereKorrekteAntworten, bool pKnzExportiereFalscheAntworten, bool pKnzExportiereAntwortBezeichnung, bool pKnzAntwortReihenfolgeUmstellen)
        {
            bool pKnzMarkiereAntwortKorrekt = true;

            String frage_export_string = "";

            int max_anzahl_spalten = 0;

            String antwort_bezeichnung = null;

            max_anzahl_spalten = 75;

            /* 
             * Pruefung: Ist die Eingabe "null" ?
             * 
             * Ist die Eingabe "null" ist das Ergebnis ein Leerstring.
             */
            if ( pFrage == null )
            {
                frage_export_string = "";
            }
            else
            {
                /* 
                 * Aufbereitung Fragenexport
                 *
                 * Zuerst kommen 2 New-Line Zeichen fuer den Abstand
                 */
                frage_export_string += pNewLineZeichen + pNewLineZeichen;

                /* 
                 * Fragen-Nummer mit der Breite fuer die Frangennummer.
                 */
                frage_export_string += getStringRight( ( knz_use_lfd_nr ? pFrage.getLfdNummer() : pFrage.getNummer() ), ANZ_STELLEN_FRAGENNR );

                /* 
                 * Abstand zwischen Frage-Nummer und dem Fragentext
                 */
                frage_export_string += ABSTAND_FNR_FRAGE;

                /* 
                 * Der Fragentext als Block formatiert mit dem abschliessendem New-Line-Zeichen
                 */
                frage_export_string += fkString.getStringMaxCols( pFrage.getText1(), max_anzahl_spalten, m_einzug_frage, pNewLineZeichen ) + pNewLineZeichen;

                /* 
                 * Auswertung: Antwortreihenfolge-Umstellung
                 * 
                 * Soll die Antwortreihenfolge umgestellt werden, wird an der Frage die Funktion 
                 * fuer die Umstellung der Antwortreihenfolge aufgerufen. 
                 * 
                 * Soll die Antwortreihenfolge nicht umgestellt werden, wird an der Frage 
                 * die Funktion fuer den Reset der Antwortreihenfolge aufgerufen. 
                 */
                
                if ( pKnzAntwortReihenfolgeUmstellen )
                {
                    pFrage.startAntwortReihenfolgeUmstellen();
                }
                else
                {
                    pFrage.resetAntwortIndexPosition();
                }

                /* 
                 * Export der Antworten:
                 * 1. Kennzeichenermittlung, ob die Antwort aktiv ist
                 * 2. Kennzeichenermittlung, ob die Antwort exportiert werden soll
                 * 3. Start des Exportes der Antworten
                 * 
                 * Ermittlung Kennzeichen Antwort aktiv
                 * 
                 * Eine Antwort an einer UI-Position ist aktiv, wenn:
                 *
                 * - eine Antwort an der UI-Position vorhanden ist
                 *   (mit gleichzeitiger Pruefung, ob diese Antwort auch "Aktiv" ist.)
                 *
                 * - Antworten exportiert werden sollen
                 */
                bool knz_antwort_1_aktiv = pFrage.hasUiPositionAntwort1() && pKnzExportiereAntworten;
                bool knz_antwort_2_aktiv = pFrage.hasUiPositionAntwort2() && pKnzExportiereAntworten;
                bool knz_antwort_3_aktiv = pFrage.hasUiPositionAntwort3() && pKnzExportiereAntworten;
                bool knz_antwort_4_aktiv = pFrage.hasUiPositionAntwort4() && pKnzExportiereAntworten;
                bool knz_antwort_5_aktiv = pFrage.hasUiPositionAntwort5() && pKnzExportiereAntworten;
                bool knz_antwort_6_aktiv = pFrage.hasUiPositionAntwort6() && pKnzExportiereAntworten;
                bool knz_antwort_7_aktiv = pFrage.hasUiPositionAntwort7() && pKnzExportiereAntworten;
                bool knz_antwort_8_aktiv = pFrage.hasUiPositionAntwort8() && pKnzExportiereAntworten;

                /* 
                 * Ermittlung Kennzeichen Antwort exportieren
                 * 
                 * Eine Antwort an einer UI-Position wird exportiert, wenn:
                 *
                 * - die Antwort aktiv ist
                 * 
                 * - die Antwort korrekt ist und korrekte Antworten exportiert werden sollen
                 * 
                 *   oder
                 * 
                 *   die Antwort falsch ist und falsche Antworten exportiert werden sollen
                 */
                bool knz_antwort_1_exportieren = knz_antwort_1_aktiv && ( pFrage.getUiPositionAntwort1Korrekt() ? pKnzExportiereKorrekteAntworten : pKnzExportiereFalscheAntworten );
                bool knz_antwort_2_exportieren = knz_antwort_2_aktiv && ( pFrage.getUiPositionAntwort2Korrekt() ? pKnzExportiereKorrekteAntworten : pKnzExportiereFalscheAntworten );
                bool knz_antwort_3_exportieren = knz_antwort_3_aktiv && ( pFrage.getUiPositionAntwort3Korrekt() ? pKnzExportiereKorrekteAntworten : pKnzExportiereFalscheAntworten );
                bool knz_antwort_4_exportieren = knz_antwort_4_aktiv && ( pFrage.getUiPositionAntwort4Korrekt() ? pKnzExportiereKorrekteAntworten : pKnzExportiereFalscheAntworten );
                bool knz_antwort_5_exportieren = knz_antwort_5_aktiv && ( pFrage.getUiPositionAntwort5Korrekt() ? pKnzExportiereKorrekteAntworten : pKnzExportiereFalscheAntworten );
                bool knz_antwort_6_exportieren = knz_antwort_6_aktiv && ( pFrage.getUiPositionAntwort6Korrekt() ? pKnzExportiereKorrekteAntworten : pKnzExportiereFalscheAntworten );
                bool knz_antwort_7_exportieren = knz_antwort_7_aktiv && ( pFrage.getUiPositionAntwort7Korrekt() ? pKnzExportiereKorrekteAntworten : pKnzExportiereFalscheAntworten );
                bool knz_antwort_8_exportieren = knz_antwort_8_aktiv && ( pFrage.getUiPositionAntwort8Korrekt() ? pKnzExportiereKorrekteAntworten : pKnzExportiereFalscheAntworten );

                /* 
                 * Erstellung der Strings fuer die Antworten
                 */
                if (knz_antwort_1_exportieren)
                {
                    antwort_bezeichnung = ( pKnzExportiereAntwortBezeichnung ? pFrage.getUiPositionAntwort1Bez() : ALTERNATIVE_ANTWORT_BEZEICHNUNG );

                    frage_export_string += pNewLineZeichen + getStringRight( ( pFrage.getUiPositionAntwort1Korrekt() && pKnzMarkiereAntwortKorrekt ? VORGABE_KORREKT_MARKIERUNG : LEERZEICHEN ) + LEERZEICHEN + antwort_bezeichnung, ANZ_STELLEN_ANTWORTBEZEICHNUNG) + ABSTAND_FBEZ_ANTWORT + fkString.getStringMaxCols( pFrage.getUiPositionAntwort1Text(), max_anzahl_spalten, m_einzug_antwort, pNewLineZeichen) + pNewLineZeichen;
                }


                if (knz_antwort_2_exportieren)
                {
                    antwort_bezeichnung = ( pKnzExportiereAntwortBezeichnung ? pFrage.getUiPositionAntwort2Bez() : ALTERNATIVE_ANTWORT_BEZEICHNUNG );

                    frage_export_string += pNewLineZeichen + getStringRight( ( pFrage.getUiPositionAntwort2Korrekt() && pKnzMarkiereAntwortKorrekt ? VORGABE_KORREKT_MARKIERUNG : LEERZEICHEN ) + LEERZEICHEN + antwort_bezeichnung, ANZ_STELLEN_ANTWORTBEZEICHNUNG) + ABSTAND_FBEZ_ANTWORT + fkString.getStringMaxCols( pFrage.getUiPositionAntwort2Text(), max_anzahl_spalten, m_einzug_antwort, pNewLineZeichen) + pNewLineZeichen;
                }


                if (knz_antwort_3_exportieren)
                {
                    antwort_bezeichnung = ( pKnzExportiereAntwortBezeichnung ? pFrage.getUiPositionAntwort3Bez() : ALTERNATIVE_ANTWORT_BEZEICHNUNG);

                    frage_export_string += pNewLineZeichen + getStringRight( ( pFrage.getUiPositionAntwort3Korrekt() && pKnzMarkiereAntwortKorrekt ? VORGABE_KORREKT_MARKIERUNG : LEERZEICHEN ) + LEERZEICHEN + antwort_bezeichnung, ANZ_STELLEN_ANTWORTBEZEICHNUNG) + ABSTAND_FBEZ_ANTWORT + fkString.getStringMaxCols( pFrage.getUiPositionAntwort3Text(), max_anzahl_spalten, m_einzug_antwort, pNewLineZeichen) + pNewLineZeichen;
                }


                if (knz_antwort_4_exportieren)
                {
                    antwort_bezeichnung = ( pKnzExportiereAntwortBezeichnung ? pFrage.getUiPositionAntwort4Bez() : ALTERNATIVE_ANTWORT_BEZEICHNUNG );

                    frage_export_string += pNewLineZeichen + getStringRight( ( pFrage.getUiPositionAntwort4Korrekt() && pKnzMarkiereAntwortKorrekt ? VORGABE_KORREKT_MARKIERUNG : LEERZEICHEN ) + LEERZEICHEN + antwort_bezeichnung, ANZ_STELLEN_ANTWORTBEZEICHNUNG) + ABSTAND_FBEZ_ANTWORT + fkString.getStringMaxCols( pFrage.getUiPositionAntwort4Text(), max_anzahl_spalten, m_einzug_antwort, pNewLineZeichen) + pNewLineZeichen;
                }


                if (knz_antwort_5_exportieren)
                {
                    antwort_bezeichnung = ( pKnzExportiereAntwortBezeichnung ? pFrage.getUiPositionAntwort5Bez() : ALTERNATIVE_ANTWORT_BEZEICHNUNG );

                    frage_export_string += pNewLineZeichen + getStringRight( ( pFrage.getUiPositionAntwort5Korrekt() && pKnzMarkiereAntwortKorrekt ? VORGABE_KORREKT_MARKIERUNG : LEERZEICHEN ) + LEERZEICHEN + antwort_bezeichnung, ANZ_STELLEN_ANTWORTBEZEICHNUNG) + ABSTAND_FBEZ_ANTWORT + fkString.getStringMaxCols( pFrage.getUiPositionAntwort5Text(), max_anzahl_spalten, m_einzug_antwort, pNewLineZeichen) + pNewLineZeichen;
                }


                if (knz_antwort_6_exportieren)
                {
                    antwort_bezeichnung = ( pKnzExportiereAntwortBezeichnung ? pFrage.getUiPositionAntwort6Bez() : ALTERNATIVE_ANTWORT_BEZEICHNUNG );

                    frage_export_string += pNewLineZeichen + getStringRight( ( pFrage.getUiPositionAntwort6Korrekt() && pKnzMarkiereAntwortKorrekt ? VORGABE_KORREKT_MARKIERUNG : LEERZEICHEN ) + LEERZEICHEN + antwort_bezeichnung, ANZ_STELLEN_ANTWORTBEZEICHNUNG) + ABSTAND_FBEZ_ANTWORT + fkString.getStringMaxCols( pFrage.getUiPositionAntwort6Text(), max_anzahl_spalten, m_einzug_antwort, pNewLineZeichen) + pNewLineZeichen;
                }


                if (knz_antwort_7_exportieren)
                {
                    antwort_bezeichnung = ( pKnzExportiereAntwortBezeichnung ? pFrage.getUiPositionAntwort7Bez() : ALTERNATIVE_ANTWORT_BEZEICHNUNG );

                    frage_export_string += pNewLineZeichen + getStringRight( ( pFrage.getUiPositionAntwort7Korrekt() && pKnzMarkiereAntwortKorrekt ? VORGABE_KORREKT_MARKIERUNG : LEERZEICHEN ) + LEERZEICHEN + antwort_bezeichnung, ANZ_STELLEN_ANTWORTBEZEICHNUNG) + ABSTAND_FBEZ_ANTWORT + fkString.getStringMaxCols( pFrage.getUiPositionAntwort7Text(), max_anzahl_spalten, m_einzug_antwort, pNewLineZeichen) + pNewLineZeichen;
                }


                if (knz_antwort_8_exportieren)
                {
                    antwort_bezeichnung = ( pKnzExportiereAntwortBezeichnung ? pFrage.getUiPositionAntwort8Bez() : ALTERNATIVE_ANTWORT_BEZEICHNUNG );

                    frage_export_string += pNewLineZeichen + getStringRight( ( pFrage.getUiPositionAntwort8Korrekt() && pKnzMarkiereAntwortKorrekt ? VORGABE_KORREKT_MARKIERUNG : LEERZEICHEN ) + LEERZEICHEN + antwort_bezeichnung, ANZ_STELLEN_ANTWORTBEZEICHNUNG) + ABSTAND_FBEZ_ANTWORT + fkString.getStringMaxCols( pFrage.getUiPositionAntwort8Text(), max_anzahl_spalten, m_einzug_antwort, pNewLineZeichen) + pNewLineZeichen;
                }


                if ( pFrage.hasText2() )
                {
                    frage_export_string += pNewLineZeichen + getStringRight( " ", ANZ_STELLEN_FRAGENNR ) + ABSTAND_FBEZ_ANTWORT + fkString.getStringMaxCols( pFrage.getText2(), max_anzahl_spalten, m_einzug_antwort, pNewLineZeichen ) + pNewLineZeichen;
                }
            }

            bool knz_set_trennzeile = true;

            if ( knz_set_trennzeile )
            {
                String str_trennzeile = "\n--------------------------------------------------------------------------------------------------------------\n";

                frage_export_string += pNewLineZeichen + str_trennzeile;
            }

            /*
             * Nach jeder exportierten Frage, wird "DoEvents" aufgerufen.
             */
            //Application.DoEvents();

            return frage_export_string;
        }

        /*
         * ################################################################################
         */
        private static String getKorrektStringFrageX( clsFrage pFrage )
        {
            String str_korrekte_antworten = "";
            String str_fragen_nummer = "";

            if ( pFrage == null )
            {
                str_fragen_nummer = "";
                str_korrekte_antworten = "";
            }
            else
            {
                str_fragen_nummer = ( knz_use_lfd_nr ? pFrage.getLfdNummer() : pFrage.getNummer() );

                str_korrekte_antworten = "";

                if ( pFrage.getAntwortAKorrekt() )
                {
                    str_korrekte_antworten = str_korrekte_antworten + m_loesungsbogen_antwort_a;
                }
                else
                {
                    str_korrekte_antworten = str_korrekte_antworten + " ";
                }

                if ( pFrage.getAntwortBKorrekt() )
                {
                    str_korrekte_antworten = str_korrekte_antworten + m_loesungsbogen_antwort_b;
                }
                else
                {
                    str_korrekte_antworten = str_korrekte_antworten + " ";
                }

                if ( pFrage.getAntwortCKorrekt() )
                {
                    str_korrekte_antworten = str_korrekte_antworten + m_loesungsbogen_antwort_c;
                }
                else
                {
                    str_korrekte_antworten = str_korrekte_antworten + " ";
                }

                if ( pFrage.getAntwortDKorrekt() )
                {
                    str_korrekte_antworten = str_korrekte_antworten + m_loesungsbogen_antwort_d;
                }
                else
                {
                    str_korrekte_antworten = str_korrekte_antworten + " ";
                }

                if ( pFrage.getAntwortEKorrekt() )
                {
                    str_korrekte_antworten = str_korrekte_antworten + m_loesungsbogen_antwort_e;
                }
                else
                {
                    str_korrekte_antworten = str_korrekte_antworten + " ";
                }

                if ( pFrage.getAntwortFKorrekt() )
                {
                    str_korrekte_antworten = str_korrekte_antworten + m_loesungsbogen_antwort_f;
                }
                else
                {
                    str_korrekte_antworten = str_korrekte_antworten + " ";
                }

                if ( pFrage.getAntwortGKorrekt() )
                {
                    str_korrekte_antworten = str_korrekte_antworten + m_loesungsbogen_antwort_g;
                }
                else
                {
                    str_korrekte_antworten = str_korrekte_antworten + " ";
                }

                if ( pFrage.getAntwortHKorrekt() )
                {
                    str_korrekte_antworten = str_korrekte_antworten + m_loesungsbogen_antwort_h;
                }
                else
                {
                    str_korrekte_antworten = str_korrekte_antworten + " ";
                }
            }

            if ( m_knz_loesungsbogen_version_1 )
            {
                str_korrekte_antworten = str_korrekte_antworten.Trim();
            }

            return getStringRight( str_fragen_nummer, ANZ_STELLEN_FRAGENNR ) + STR_TRENN_STRING + getStringLeft( str_korrekte_antworten, m_max_anzahl_vorhandene_antworten );
        }

        /*
         * ################################################################################
         */
        private static String getStringRight( String pString, int pMaxLen )
        {
            if ( pString == null )
            {
                return new String( ' ', pMaxLen );
            }

            if ( pString.Length >= pMaxLen )
            {
                return pString;
            }

            return new String( ' ', pMaxLen - pString.Length ) + pString;
        }

        /*
         * ################################################################################
         */
        private static String getStringLeft( String pString, int pMaxLen )
        {
            if ( pString == null )
            {
                return new String( ' ', pMaxLen );
            }

            if ( pString.Length >= pMaxLen )
            {
                return pString;
            }

            return pString + new String( ' ', pMaxLen - pString.Length );
        }

        private static String getSringLoesungsbogen( bool pKnzErstelleLoesungsbogen, clsStringArray pStringArrayLoesungsbogen )
        {
            String string_loesungsbogen = "";

            try
            {
                if ( pKnzErstelleLoesungsbogen ) 
                {
                    /* 
                     * Sortierung Loesungsbogen
                     * Die exportierten Fragen muessen nicht in der Reihenfolge des Fragenkataloges 
                     * kommen. Somit ergibt sich das Problem, dass der Loesungsbogen auch nicht 
                     * in einer sortierten Reihenfolge erstellt worden ist.
                     *
                     * Der Loesungsbogen wird hier aufsteigend sortiert.
                     */
                    pStringArrayLoesungsbogen.startSortierungAufsteigend();

                    /* 
                     * Die ersten 21 Stellem im Loesungsbogen sind nur fuer die Sortierung 
                     * aufgenommen worden. Diese Zeichen werden hier geloescht.
                     */
                    pStringArrayLoesungsbogen.doItSubstring( 21 );

                    /* 
                     * Spaltenbreite 1
                     * Jede Frage im Loesungsbogen vereinamt eine Anzahl von Spalten.
                     * Das ist die Fragennummer, zuzueglich der Trennzeichenbreite und
                     * der Anzahl der maxmimal vorhandenen Antworten.
                     */
                    int spalten_breite_1 = ANZ_STELLEN_FRAGENNR + STR_TRENN_STRING.Length + m_max_anzahl_vorhandene_antworten;

                    /* 
                     * Anzahl der Fragen in einer Reihe des Loesungsbogens.
                     * Die Breite des Loesungsbogens durch die Spaltenbreite 1.
                     */
                    int lb_anzahl_spalten = Convert.ToInt32( VORGABE_ANZ_STELLEN / spalten_breite_1 );

                    /* 
                     * Anzahl der benoetigten Zeilen
                     * Die Anzahl der benoetigten Zeilen ist gleich der Anzahl der Fragen dividiert
                     * durch die Anzahl der Loesungsbogenspalten (lb_anzahl_spalten). Das Ergebnis
                     * der Division wird um 0.9 erhoeht, um auf die naechsthoehere Zahl zu kommen.
                     * Dieses Ergebnis wird mit CINT auf den Ganzzahlteil beschraenkt.
                     * 
                     * Die Anzahl der Fragen ist hier gleich der Anzahl der im Loesungsbogen
                     * enthaltenen Fragen. Dieses kann variieren und ist nicht gleich der
                     * Anzahl der Fragen aus der Lernfabrik.
                     * 
                     * Die Anzahl der Fragen ergibt sich aus der Anzahl der gespeicherten Loesungen.
                     * (= Anzahl der Zeilen aus lb_reihenfolge)
                     */
                    int lb_anzahl_zeilen = Convert.ToInt32( ( pStringArrayLoesungsbogen.getAnzahlStrings() / lb_anzahl_spalten ) + 0.9 );

                    int lb_zeilen_zaehler = 1;
                    int lb_spalten_zaehler = 1;

                    String lb_ausgabe_string = "";
                    String akt_string = null;

                    string_loesungsbogen += NEW_LINE;

                    string_loesungsbogen += NEW_LINE + NEW_LINE + "Loesungsbogen: (" + pStringArrayLoesungsbogen.getAnzahlStrings() + ")" + NEW_LINE;

                    while ( ( lb_zeilen_zaehler <= lb_anzahl_zeilen ) )
                    {
                        lb_ausgabe_string = "";

                        lb_spalten_zaehler = 1;

                        while ( lb_spalten_zaehler <= lb_anzahl_spalten )
                        {
                            akt_string = pStringArrayLoesungsbogen.getString( lb_zeilen_zaehler + ( lb_spalten_zaehler - 1 ) * lb_anzahl_zeilen );

                            if ( akt_string != null )
                            {
                                lb_ausgabe_string += akt_string;
                            }

                            lb_spalten_zaehler = lb_spalten_zaehler + 1;
                        }

                        string_loesungsbogen += lb_ausgabe_string + NEW_LINE;

                        lb_zeilen_zaehler = lb_zeilen_zaehler + 1;
                    }
                }
            }
            catch ( Exception err_inst )
            {
                Console.WriteLine( "Fehler: z\n" + err_inst.Message + "\n\n" + err_inst.StackTrace );
            }

            return string_loesungsbogen;
        }

    }
}