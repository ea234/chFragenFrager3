using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class fkExportXmlFragenKatalog
    {
        private static String XML_TAG_FRAGENKATALOG                  = "FragenKatalog";
        private static String XML_TAG_FRAGENKATALOG_BEZEICHNUNG      = "KatalogBezeichnung";
        private static String XML_TAG_FRAGENKATALOG_DATUM_ERSTELLUNG = "KatalogDatumErstellung";

        private static String TAG_FRAGENKATALOG                      = "FragenKatalog";
        private static String TAG_FRAGENKATALOGBEZEICHNUNG           = "KatalogBezeichnung";

        /* 
         * Konstanten fuer die XML-Klammer "FRAGE"
         */
        private static String XML_TAG_FRAGE                          = "FRAGE";
        private static String XML_TAG_BEMERKUNG                      = "BEMERKUNG";
        private static String XML_TAG_BILD_1                         = "BILD_1";
        private static String XML_TAG_BILD_2                         = "BILD_2";
        private static String XML_TAG_BILD_3                         = "BILD_3";
        private static String XML_TAG_BILD_4                         = "BILD_4";
        private static String XML_TAG_GELTUNGSBEREICH                = "GELTUNGSBEREICH";
        private static String XML_TAG_ID                             = "ID";
        private static String XML_TAG_NUMMER                         = "NUMMER";
        private static String XML_TAG_TEXT_1                         = "TEXT_1";
        private static String XML_TAG_TEXT_2                         = "TEXT_2";

        /* 
         * Konstanten fuer die XML-Klammer "ANTWORT"
         */
        private static String XML_TAG_ANTWORT                        = "ANTWORT";
        private static String XML_TAG_ANTWORT_BEZ                    = "ANTWORT_BEZ";
        private static String XML_TAG_ANTWORT_TEXT                   = "ANTWORT_TEXT";
        private static String XML_TAG_KNZ_KORREKT                    = "KNZ_KORREKT";
        private static String XML_TAG_ANTWORT_BEZEICHNUNG            = "Bezeichnung";
        private static String XML_TAG_ANTWORT_BEMERKUNG              = "Bemerkung";
        private static String XML_TAG_ANTWORT_KORREKT                = "Korrekt";

        /*
         * ################################################################################
         */
        public static bool startExportXmlLernFabrik( clsFragenKatalog pFrageKatalog, clsLernFrabrik pLernFrabrik, int pExportModus )
        {
            bool fkt_ergebnis = false;

            if ( pFrageKatalog == null )
            {
                return fkt_ergebnis;
            }

            if ( pLernFrabrik == null )
            {
                return fkt_ergebnis;
            }

            if ( pFrageKatalog.getAnzahlFragen() == 0 )
            {
                return fkt_ergebnis;
            }

            if ( pLernFrabrik.getAnzahlFragen() == 0 )
            {
                return fkt_ergebnis;
            }

            /* 
             * Letzter Dateiname
             * Aus der INI-Datei wird der Name der letzte Exportname geholt.
             * Dieser Name erscheint dann als Vorauswahl in der Dialog-Box.
             */
            String datei_name = fkIniDatei.readIniDateiName( "DATEI_NAME_EXPORT_XML_LERN_FABRIK" );

            /* 
             * Dateifilter
             * Die zur Auswahl stehenden Datei-Erweiterungen werden als String initialisiert.
             */
            String datei_filter = "XML-Datei ( *.xml )\0*.xml\0alle Dateien ( *.* )\0*.*\0";

            /* 
             * Aufruf der Dateiauswahl-Dialog-Box
             */
            datei_name = fkCommonDialog.getSaveName( datei_filter, "xml", "c:\\", datei_name, "Exportdatei wählen" );

            if ( datei_name != null )
            {
                fkIniDatei.writeIniDateiName( "DATEI_NAME_EXPORT_XML_LERN_FABRIK", datei_name );

                fkt_ergebnis = exportXmlLernFabrik( pFrageKatalog, pLernFrabrik, pExportModus, datei_name );
            }

            return fkt_ergebnis;
        }

        /*
         * ################################################################################
         */
        private static bool exportXmlLernFabrik( clsFragenKatalog pFragenKatalog, clsLernFrabrik pLernFrabrik, int pExportModus, String pDateiName )
        {
            clsFrage temp_frage = null;

            String string_datei_inhalt = "";

            string_datei_inhalt += "<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?>\n";
            string_datei_inhalt += getStartTag( XML_TAG_FRAGENKATALOG );
            string_datei_inhalt += getTag( XML_TAG_FRAGENKATALOG_BEZEICHNUNG, "unbenannt" );
            string_datei_inhalt += getTag( XML_TAG_FRAGENKATALOG_DATUM_ERSTELLUNG, DateTime.Now.ToString( "dd.MM.yyyy HH:mm:ss" ) );

            bool fkt_ergebnis = true;

            int index_fragen_katalog = 0;

            int index_lern_fabrik = 0;

            while ( ( index_lern_fabrik < pLernFrabrik.getAnzahlFragen() ) )
            {
                if ( pExportModus == fkExportFrageBogen.EXPORT_LERN_FABRIK_KORREKT )
                {
                    index_fragen_katalog = pLernFrabrik.getAbfrageIndexKorrekt( index_lern_fabrik );
                }
                else if ( pExportModus == fkExportFrageBogen.EXPORT_LERN_FABRIK_FALSCH )
                {
                    index_fragen_katalog = pLernFrabrik.getAbfrageIndexFalsch( index_lern_fabrik );
                }
                else
                {
                    index_fragen_katalog = pLernFrabrik.getAbfrageIndex( index_lern_fabrik );
                }

                if ( index_fragen_katalog >= 0 )
                {
                    try
                    {
                        temp_frage = pFragenKatalog.getIndex( index_fragen_katalog );

                        if ( temp_frage != null )
                        {
                            string_datei_inhalt += getXmlFrage( temp_frage );
                        }
                    }
                    catch ( Exception err_inst )
                    {
                        Console.WriteLine( "Fehler: exportXmlLernFabrik\n" + err_inst.Message + "\n\n" + err_inst.StackTrace );
                    }
                }

                index_lern_fabrik = index_lern_fabrik + 1;
            }


            string_datei_inhalt += getEndTag( XML_TAG_FRAGENKATALOG );

            System.IO.File.WriteAllText( pDateiName, string_datei_inhalt );

            return fkt_ergebnis;
        }

        /*
         * ################################################################################
         */
        private static String getXmlFrage( clsFrage pFrage )
        {
            String  xml_string = "";

            xml_string += getStartTag( XML_TAG_FRAGE );

            if ( pFrage != null )
            {
                xml_string += getTag( XML_TAG_ID, pFrage.getId() );
                xml_string += getTag( XML_TAG_NUMMER, pFrage.getNummer() );
                xml_string += getTag( XML_TAG_GELTUNGSBEREICH, pFrage.getGeltungsbereich() );
                xml_string += getTag( XML_TAG_TEXT_1, pFrage.getText1() );
                xml_string += getTag( XML_TAG_TEXT_2, pFrage.getText2() );
                xml_string += getTag( XML_TAG_BEMERKUNG, pFrage.getBemerkung() );
                xml_string += getTag( XML_TAG_BILD_1, pFrage.getBild1() );
                xml_string += getTag( XML_TAG_BILD_2, pFrage.getBild2() );
                xml_string += getTag( XML_TAG_BILD_3, pFrage.getBild3() );
                xml_string += getTag( XML_TAG_BILD_4, pFrage.getBild4() );

                xml_string += getXmlAntwort( pFrage.getAntwortA() );
                xml_string += getXmlAntwort( pFrage.getAntwortB() );
                xml_string += getXmlAntwort( pFrage.getAntwortC() );
                xml_string += getXmlAntwort( pFrage.getAntwortD() );
                xml_string += getXmlAntwort( pFrage.getAntwortE() );
                xml_string += getXmlAntwort( pFrage.getAntwortF() );
                xml_string += getXmlAntwort( pFrage.getAntwortG() );
                xml_string += getXmlAntwort( pFrage.getAntwortH() );
            }

            xml_string += getEndTag( XML_TAG_FRAGE );

            return xml_string;
        }

        /* ####################################################################################################
         * 
         * Erstellt die XML-Repraesentation fuer die Bean-Klasse "clsAntwort"
         * 
         * PARAMETER: pAntwort       = Eine Instanz der Klase "clsAntwort"
         * 
         * RETURN   : XML-Repraesentation der Beanklasse
         */
        private static String getXmlAntwort( clsAntwort pAntwort )
        {
            String xml_string = "";

            if ( pAntwort != null )
            {
                xml_string += getStartTag( XML_TAG_ANTWORT );

                xml_string += getTag( XML_TAG_ID, "" + pAntwort.getId() );
                xml_string += getTag( XML_TAG_ANTWORT_BEZ, pAntwort.getAntwortBez() );
                xml_string += getTag( XML_TAG_ANTWORT_TEXT, pAntwort.getAntwortText() );
                xml_string += getTag( XML_TAG_KNZ_KORREKT, "" + pAntwort.getKnzKorrekt() );
                xml_string += getTag( XML_TAG_BEMERKUNG, pAntwort.getBemerkung() );

                xml_string += getEndTag( XML_TAG_ANTWORT );
            }

            return xml_string;
        }

        /*
         * ################################################################################
         */
        private static String getStartTag( String pTagName )
        {
            return "<" + pTagName + ">\n";
        }

        /* 
         * ################################################################################
         */
        private static String getEndTag( String pTagName )
        {
            return "</" + pTagName + ">\n";
        }

        /*
         * ################################################################################
         */
        private static String getTag( String pTagName, String pTagWert )
        {
            return "<" + pTagName + ">" + pTagWert + "</" + pTagName + ">\n";
        }
    }
}
