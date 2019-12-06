using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class clsFkFragenKatalogLaden
    {
        /* 
         * XML-Tag-Konstanten fuer den Fragenkatalog
         */
        private String TAG_FRAGENKATALOG = "FragenKatalog";
        private String TAG_FRAGENKATALOGBEZEICHNUNG = "KatalogBezeichnung";

        /* 
         * XML-Tag-Konstanten fuer die Fragen
         */
        private String XML_TAG_FRAGE = "FRAGE";
        private String XML_TAG_BEMERKUNG = "BEMERKUNG";
        private String XML_TAG_BILD_1 = "BILD_1";
        private String XML_TAG_BILD_2 = "BILD_2";
        private String XML_TAG_BILD_3 = "BILD_3";
        private String XML_TAG_BILD_4 = "BILD_4";
        private String XML_TAG_GELTUNGSBEREICH = "GELTUNGSBEREICH";
        private String XML_TAG_ID = "ID";
        private String XML_TAG_NUMMER = "NUMMER";
        private String XML_TAG_TEXT_1 = "TEXT_1";
        private String XML_TAG_TEXT_2 = "TEXT_2";

        /* 
         * XML-Tag-Konstanten fuer die Antworten
         */
        private String XML_TAG_ANTWORT = "Antwort";
        private String XML_TAG_ANTWORT_TEXT = "Text";
        private String XML_TAG_ANTWORT_BEZEICHNUNG = "Bezeichnung";
        private String XML_TAG_ANTWORT_BEMERKUNG = "Bemerkung";
        private String XML_TAG_ANTWORT_KORREKT = "Korrekt";

        /*
         * Konstante fuer einen Leerstring
         */
        private String LEERSTRING = "";

        /*
         * Maximale Anzahl der zu lesenden XML-Klammern
         */
        private int MAX_ANZAHL_XML_KLAMMER_LESEN = 515;

        /* 
         * PARAMETER: pXmlDateiName  = Pfad und Dateiname auf die zu parsende XML-Datei
         * PARAMETER: pFragenKatalog  = der Fragenkatalog, in welchem die Fragen gespeichert werden sollen
         * 
         * RETURN   : TRUE sofern die Datei gelesen und geparst werden konnte, sonst false
         */
        public bool startImportXmlDatei( String pXMLDatei, clsFragenKatalog pFragenKatalog )
        {
            bool   knz_xml_datei_geparst = false;
            int    xml_node_zaehler      = 0;
            String xml_datei_inhalt      = "";
            String akt_root              = "";

            /* 
             * Pruefung: Instanz fuer die Speicherung der Fragen vorhanden ?
             * 
             * Ist keine Speicherinstanzt uebergeben, bekommt der Aufrufer FALSE zurueck.
             */
            if ( pFragenKatalog == null )
            {
                // Keine Instanz fuer die Speicherung vorhanden

                return false;
            }

            /* 
             * Dateiinhalt der XML-Datei in die Variable xml_datei_inhalt laden.
             */
            xml_datei_inhalt = getXmlDateiInhalt( pXMLDatei );

            pFragenKatalog.setDateiName( pXMLDatei );

            /* 
             * Pruefung: Konnte die XML-Datei geladen werden?
             * Wenn die XML-Datei nicht geladen werden konnte, gibt es einen Leerstring zurueck.
             * 
             * Die Datei kann nur dann verarbeitet werden, wenn es einen Dateiinhalt zum
             * verarbeiten gibt.
             */
            if ( xml_datei_inhalt != "" )
            {
                /* 
                 * Den Klammerzaehler auf 1 stellen.
                 */
                xml_node_zaehler = 1;

                /* 
                 * Erste XML-Klammern "FRAGE" ermitteln.
                 */
                akt_root = getTagString( xml_datei_inhalt, XML_TAG_FRAGE, xml_node_zaehler );

                /* 
                 * While-Schleife
                 * Die While-Schleife laeuft solange wie:
                 * ... es noch XML-Klammern gibt, die Variable "akt_root" kein Leerstring ist.
                 * ... der Index-Zaehler noch nicht die Maximalgrenze der einzulesenden Objekte erreicht hat.
                 */
                while ( ( akt_root != "" ) && ( xml_node_zaehler < MAX_ANZAHL_XML_KLAMMER_LESEN ) )
                {
                    /* 
                     * Aufruf der Parse-Funktion fuer ein Element der Klasse "clsFrage".
                     * Die Ergenibsinstanz wird dem Vektor hinzugefuegt.
                     */
                    pFragenKatalog.addFrage( parseClsFrage( akt_root ) );

                    /* 
                     * Index-Zaehler um 1 erhoehen
                     */
                    xml_node_zaehler++;

                    /* 
                     * Naechste XML-Klammer "FRAGE" holen.
                     */
                    akt_root = getTagString( xml_datei_inhalt, XML_TAG_FRAGE, xml_node_zaehler );
                }

                knz_xml_datei_geparst = true;
            }
            else
            {
                /* 
                 * Fehlerfall: XML-Datei konnte nicht geparst werden.
                 */
                knz_xml_datei_geparst = false;
            }

            return knz_xml_datei_geparst;
        }

        /* 
         * ################################################################################
         */
        private String getXmlDateiInhalt( String pDateiNamne )
        {
            String datei_inhalt = "";

            string datei_zeile;

            // Read the file and display it line by line.  
            System.IO.StreamReader file_input = new System.IO.StreamReader( @pDateiNamne );

            while ( ( datei_zeile = file_input.ReadLine() ) != null )
            {
                datei_inhalt = datei_inhalt + datei_zeile;
            }

            file_input.Close();

            file_input = null;

            return datei_inhalt;
        }

        /**
         * <pre>
         * Parst eine Instanz der Klasse "clsFrage".
         *
         * Ist der Parameter "pRootFrage" gleich null, wird null zurueckgegeben.
         *
         * </pre>
         *
         * @param pRootFrage  XML-Rootelement mit den zu parsenden Daten 
         * @return eine Instanz mit den geparsten Daten, oder null im Fehlefall
         */
        private clsFrage parseClsFrage( String pRootFrage )
        {
            /* 
             * Pruefung: Parameter "pBeanRootFrage" gesetzt?
             */
            if ( pRootFrage == null )
            {
                return null;
            }
            else if ( pRootFrage != "" )
            {
                /*
                 * Ist der Parameter "pRootFrage" gesetzt, wird 
                 * eine neue Instanz der Klasse "clsFrage" erstellt.
                 */
                clsFrage inst_frage = new clsFrage();

                /*
                 * Die XML-Daten werden in die neue Instanz uebertragen
                 */
                inst_frage.setId(     getTagString( pRootFrage, XML_TAG_ID, -1 ) );
                inst_frage.setNummer( getTagString( pRootFrage, XML_TAG_NUMMER, -1 ) );
                inst_frage.setGeltungsbereich( getTagString( pRootFrage, XML_TAG_GELTUNGSBEREICH, -1 ) );
                inst_frage.setText1( getTagString( pRootFrage, XML_TAG_TEXT_1, -1 ) );
                inst_frage.setText2( getTagString( pRootFrage, XML_TAG_TEXT_2, -1 ) );
                inst_frage.setBemerkung( getTagString( pRootFrage, XML_TAG_BEMERKUNG, -1 ) );
                inst_frage.setBild1( getTagString( pRootFrage, XML_TAG_BILD_1, -1 ) );
                inst_frage.setBild2( getTagString( pRootFrage, XML_TAG_BILD_2, -1 ) );
                inst_frage.setBild3( getTagString( pRootFrage, XML_TAG_BILD_3, -1 ) );
                inst_frage.setBild4( getTagString( pRootFrage, XML_TAG_BILD_4, -1 ) );

                /* 
                 * Hier werden nun die Antworten zu der Frage hinzugefuegt.
                 * Es wurde auf eine Plausipruefung verzichtet, welche sicherstellt, das Antwort
                 * A auch Antwort A ist. Es werden 8 Antworten gelesen.
                 */
                inst_frage.setAntwortA( parseClsAntwort( getTagString( pRootFrage, XML_TAG_ANTWORT, 1 ) ) );
                inst_frage.setAntwortB( parseClsAntwort( getTagString( pRootFrage, XML_TAG_ANTWORT, 2 ) ) );
                inst_frage.setAntwortC( parseClsAntwort( getTagString( pRootFrage, XML_TAG_ANTWORT, 3 ) ) );
                inst_frage.setAntwortD( parseClsAntwort( getTagString( pRootFrage, XML_TAG_ANTWORT, 4 ) ) );
                inst_frage.setAntwortE( parseClsAntwort( getTagString( pRootFrage, XML_TAG_ANTWORT, 5 ) ) );
                inst_frage.setAntwortF( parseClsAntwort( getTagString( pRootFrage, XML_TAG_ANTWORT, 6 ) ) );
                inst_frage.setAntwortG( parseClsAntwort( getTagString( pRootFrage, XML_TAG_ANTWORT, 7 ) ) );
                inst_frage.setAntwortH( parseClsAntwort( getTagString( pRootFrage, XML_TAG_ANTWORT, 8 ) ) );

                /* 
                 * Die erstellte Ergebnisinstanz "clsFrage" als Funktionsergebnis setzen
                 */
                return inst_frage;
            }

            return null;
        }

        /**
         * <pre>
         * Parst eine Instanz der Klasse "clsAntwort".
         *
         * Ist der Parameter "pRootAntwort" gleich null, wird null zurueckgegeben.
         *
         * </pre>
         *
         * @param pRootAntwort XML-Rootelement mit den zu parsenden Daten 
         * @return eine Instanz mit den geparsten Daten, oder null im Fehlefall
         */
        private clsAntwort parseClsAntwort(String pRootAntwort)
        {
            /* 
             * Pruefung: Parameter "pRootAntwort" gesetzt?
             * 
             * Ist der Parameter "pRootAntwort" null, wird null zurueckgegeben.
             * Die zu parsende Frage hat an der entsprechenden Postion dann keine Antwort.
             * 
             * Ist der Parameter "pRootAntwort" ungleich einem Leerstring, wird die 
             * Antwort geparst und es wird eine neue Instanz der Klasse "clsAntwort" 
             * zurueckgegeben.
             * 
             * Ist der Parameter "pRootAntwort" gleich einem Leerstring, wird ebenfalls
             * null an den Aufrufer zurueckgegeben.
             */
            if (pRootAntwort == null)
            {
                return null;
            }
            else if ( pRootAntwort != "" )
            {
                clsAntwort inst_cls_antwort = new clsAntwort();

                inst_cls_antwort.setAntwortBez(  getTagString( pRootAntwort, XML_TAG_ANTWORT_BEZEICHNUNG, -1 ) );
                inst_cls_antwort.setAntwortText( getTagString( pRootAntwort, XML_TAG_ANTWORT_TEXT,        -1 ) );
                inst_cls_antwort.setBemerkung(   getTagString( pRootAntwort, XML_TAG_ANTWORT_BEMERKUNG,   -1 ) );
                inst_cls_antwort.setKnzKorrekt(  getTagString( pRootAntwort, XML_TAG_ANTWORT_KORREKT,     -1 ) == "1" );

                return inst_cls_antwort;
            }

            return null;
        }

        /**
         * <pre>
         * Liefert den Inhalt des XML-Tags
         *
         * Bedingung ist, dass die XML-Klammern so gebildet sind:
         * 
         * xml_tag_start = "<"  + pXmlTagName + ">"
         * xml_tag_ende  = "</" + pXmlTagName + ">"
         * 
         * Ein XML-End-Tag wie "<XML-TAG />" wird nicht erkannt.
         *
         * </pre>
         * 
         * @param pXmlString XML-Eingabestring
         * @param pXmlTagName XML-Tag-Name
         * @param pXmlTagIndex Tag-Index  (kleiner 1 ergibt das erste Element, max 32123)
         * @param pPositionAb Optionale Angabe, ab welcher Position im XML-String mit der Suche gestartet werden soll (-1 gleich Startposition 0)
         * @return der Wert des XML-Tags, null wenn das XML-Tag selber oder der XML-Tag-Index nicht existiert oder es zu einem Fehler kam
         */
        public String getTagString( String pXmlString, String pXmlTagName, int pXmlTagIndex )
        {
            int pPositionAb = 0;
            int position_start = 0; // Speichert die Startposition fuer die Rueckgabe
            int position_ende = 1; // Speichert die naechste Position des Trennzeichens ab der Startposition
            int position_temp_start = 1; // Position eines eventuellen Starttags zwischen Position-Start und Position-Ende

            /*
             * Der Rueckgabewert wird mit null als Vorgaberueckgabe versehen
             */
            String ergebnis_xml_wert = null;

            /*
             * Pruefung: Parameter gesetzt?
             * Der XML-String muss vorhanden sein.
             * Der XML-Tag-Name muss vorhanden und darf kein Leerstring sein. 
             */
            if ( ( pXmlString != null ) && ( pXmlTagName != null ) && ( pXmlTagName.Trim().Length > 0 ) )
            {
                try
                {
                    int xml_tag_index_zaehler = 0; // Zaehler fuer die XML-Tag Suchschleife 
                    int xml_tag_index_gesucht = 1; // Der zu suchende XML-Tag-Index (das wievielte Tag soll es sein)

                    /*
                     * Pruefung: pXmlTagIndex uebergeben?
                     * 
                     * Ist im Parameter "pXmlTagIndex" ein Wert zwischen 0 und 32123 uebergeben worden,
                     * wird dieser Wert in der Varaiblen "xml_tag_index_gesucht" gespeichert.
                     * 
                     * Liegt der Parameter-Wert ausserhalb der Grenzen, wird der Parameter "pXmlTagIndex" ignoriert.
                     * Es wird die erste XML-Klammer mit dem Tag-Namen gesucht
                     */
                    if ( ( pXmlTagIndex > 0 ) && ( pXmlTagIndex <= 32123 ) )
                    {
                        xml_tag_index_gesucht = pXmlTagIndex;
                    }

                    String xml_tag_start = "<" + pXmlTagName + ">";
                    String xml_tag_ende = "</" + pXmlTagName + ">";

                    /*
                     * Position Starttag
                     */
                    if ( pPositionAb > 0 )
                    {
                        position_start = pXmlString.IndexOf( xml_tag_start, pPositionAb );
                    }
                    else
                    {
                        position_start = pXmlString.IndexOf( xml_tag_start );
                    }

                    /*
                     * Suchschleife XML-Tag-Position
                     * 
                     * Die While-Schleife laeuft solange wie:
                     * ... der Ergebnis-Index noch nicht gefunden wurde
                     * ... die XML-Tag-Startposition groesser gleich 0 ist
                     * ... die XML-Tag-Endposition groesser 0 ist
                     */
                    while ( ( xml_tag_index_zaehler < xml_tag_index_gesucht ) && ( position_start >= 0 ) && ( position_ende > 0 ) )
                    {
                        /*
                         * XML-Tag-Index-Suchzaehler erhoehen.
                         */
                        xml_tag_index_zaehler++;

                        /*
                         * Pruefung: Nochmaliger Durchlauf?
                         * Soll nicht die erste XML-Klammer zurueckgegeben werden, wird in einem 
                         * weiteren Durchlauf die naechste Startposition des XML-Tags gesucht.
                         * 
                         * Die Positionssuche startet ab der aktuellen Endposition plus 1.
                         */
                        if ( xml_tag_index_zaehler > 1 )
                        {
                            position_start = position_ende + 1;

                            position_start = pXmlString.IndexOf( xml_tag_start, position_start );
                        }

                        /*
                         * Pruefung: Startposition gefunden?
                         * Wurde kein Starttag gefunden, muss auch keine End-Position gesucht werden.
                         * 
                         * Wurde ein Starttag fefunden, wird nach dem Endtag gesucht.
                         */
                        if ( position_start >= 0 )
                        {
                            /* 
                             * End-Xml-Tag suchen
                             * Das End-Xml-Tag wird ab der Startposition der Rueckgabe gesucht.
                             * Der Suchstring ist jetzt das Muster fuer ein XML-Endtag.
                             */
                            position_ende = pXmlString.IndexOf( xml_tag_ende, position_start );

                            /*
                             * Pruefung: XML-Endtag gefunden?
                             */
                            if ( position_ende == -1 )
                            {
                                /*
                                 * Kein XML-Endtag vorhanden
                                 * Es gibt 2 Moeglichkeiten:
                                 * 
                                 * 1. Keine Rueckgabe
                                 *    
                                 * 2. Reststring ab dem Starttag
                                 */
                                position_ende = pXmlString.Length;
                            }
                            else
                            {
                                /*
                                 * XML-Endtag wurde gefunden
                                 * 
                                 * Pruefung: Geschachtelte XML-Tags vorhanden?
                                 */
                                position_temp_start = position_start + 1;

                                while ( ( position_temp_start > position_start ) && ( position_temp_start < position_ende ) )
                                {
                                    /*
                                     * Suche ein Starttag zwischen dem oben gefundenen Start und dem 
                                     * gefundenen Endtag. 
                                     * 
                                     * Es darf dazwischen kein weiteres Starttag sein. 
                                     * 
                                     * Wird ein Starttag gefunden, ist das erste Endtag ungueltig. 
                                     * Es muss ein neues Endtag, ab der gerade gefundenen End-Tag-Position, gesucht werden.
                                     */
                                    position_temp_start = pXmlString.IndexOf( xml_tag_start, position_temp_start + 1 );

                                    if ( ( position_temp_start > 0 ) && ( position_temp_start < position_ende ) )
                                    {
                                        position_ende = pXmlString.IndexOf( xml_tag_ende, position_ende + 1 );
                                    }
                                }
                            }
                        }
                    }

                    if ( ( position_start >= 0 ) && ( position_ende > position_start ) )
                    {
                        /* 
                         * Anpassung Startposition
                         * Damit die Startposition auf das erste Zeichen der Rueckgabe zeigt,
                         * wird zu der Startposition erstens die Laenge des Suchstringes
                         * hinzuaddiert. Zweitens kommen noch 2 Zeichen fuer die eckigen 
                         * Klammern hinzu.
                         */
                        position_start = position_start + pXmlTagName.Length + 2;

                        if ( position_ende > position_start )
                        {
                            /* 
                             * Ergebnissstring setzen
                             * Der Ergebnisstring ist der Teilstring ab der Startposition 
                             * bis zur Endposition. 
                             */
                            ergebnis_xml_wert = fkString.trim( pXmlString.Substring( position_start, position_ende - position_start ) );
                        }
                    }
                }
                catch ( Exception e )
                {
                    ergebnis_xml_wert = null;
                }
            }

            return ergebnis_xml_wert;
        }
    }
}
