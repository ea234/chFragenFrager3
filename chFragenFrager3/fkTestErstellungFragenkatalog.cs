using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class fkTestErstellungFragenkatalog
    {
        /* 
         * ################################################################################
         */
        public static clsFragenKatalog getTestFragenKatalog()
        {
            clsFragenKatalog test_fragen_katalog = new clsFragenKatalog();

            clsFrage aktuelle_frage = null;

            test_fragen_katalog.setDateiName( "TestFragenkatalog1.xls" );
            
            int fragen_nr = 1;
            
            int fragen_anzahl = 2;

            int fragen_zaehler = 1;

            while ( fragen_zaehler <= fragen_anzahl )
            {
                aktuelle_frage = new clsFrage();

                aktuelle_frage.setId( "ID1_" + fragen_zaehler + "_" + fragen_nr );
                aktuelle_frage.setNummer( "" + fragen_nr );
                aktuelle_frage.setText1( "Fragenblock 1 Nr. " + fragen_zaehler + " von " + fragen_anzahl );
                aktuelle_frage.setText2( "Fragentext 2 A" );

                aktuelle_frage.setAntwortA( getTestAntwort( true,               1, true, "A" ) );
                aktuelle_frage.setAntwortB( getTestAntwort( true,               2, false, "B" ) );
                aktuelle_frage.setAntwortC( getTestAntwort( true,               3, false, "C" ) );
                aktuelle_frage.setAntwortD( getTestAntwort( true,               4, false, "D" ) );
                aktuelle_frage.setAntwortE( getTestAntwort( fragen_zaehler > 1, 5, false, "E" ) );
                aktuelle_frage.setAntwortF( getTestAntwort( fragen_zaehler > 1, 6, false, "F"));
                aktuelle_frage.setAntwortH( getTestAntwort( fragen_zaehler > 1, 7, false, "G"));
                aktuelle_frage.setAntwortG( getTestAntwort( fragen_zaehler > 1, 8, false, "H"));

                aktuelle_frage.setText2("Anzahl Antworten: " + aktuelle_frage.getAnzahlVorhandeneAntworten() + "   korrekt " + aktuelle_frage.getAnzahlKorrekteAntworten() + "   falsch " + aktuelle_frage.getAnzahlFalscheAntworten());

                test_fragen_katalog.addFrage( aktuelle_frage );

                fragen_zaehler++; fragen_nr++;
            }

            fragen_anzahl = 7;

            fragen_zaehler = 1;

            while (fragen_zaehler <= fragen_anzahl)
            {
                aktuelle_frage = new clsFrage();

                aktuelle_frage.setId("ID2_" + fragen_zaehler + "_" + fragen_nr);
                aktuelle_frage.setNummer("" + fragen_nr);
                aktuelle_frage.setText1("Fragenblock 2 Nr. " + fragen_zaehler + " von " + fragen_anzahl);
                aktuelle_frage.setText2("Fragentext 2 B");

                aktuelle_frage.setAntwortA( getTestAntwort( true,                1, true,  "A" ) );
                aktuelle_frage.setAntwortB( getTestAntwort( true,                2, false, "B" ) ) ;
                aktuelle_frage.setAntwortC( getTestAntwort( fragen_zaehler >= 2, 3, false, "C" ) );
                aktuelle_frage.setAntwortD( getTestAntwort( fragen_zaehler >= 3, 4, false, "D" ) );
                aktuelle_frage.setAntwortE( getTestAntwort( fragen_zaehler >= 4, 5, false, "E" ) );
                aktuelle_frage.setAntwortF( getTestAntwort( fragen_zaehler >= 5, 6, false, "F" ) );
                aktuelle_frage.setAntwortH( getTestAntwort( fragen_zaehler >= 6, 7, false, "G" ) );
                aktuelle_frage.setAntwortG( getTestAntwort( fragen_zaehler >= 7, 8, false, "H" ) );

                aktuelle_frage.setText2("Anzahl Antworten: " + aktuelle_frage.getAnzahlVorhandeneAntworten() + "   korrekt " + aktuelle_frage.getAnzahlKorrekteAntworten() + "   falsch " + aktuelle_frage.getAnzahlFalscheAntworten());

                test_fragen_katalog.addFrage(aktuelle_frage);

                fragen_zaehler++; fragen_nr++;
            }

            fragen_anzahl = 6;

            fragen_zaehler = 1;

            while (fragen_zaehler <= fragen_anzahl)
            {
                aktuelle_frage = new clsFrage();

                aktuelle_frage.setId("ID2_" + fragen_zaehler + "_" + fragen_nr);
                aktuelle_frage.setNummer("" + fragen_nr);
                aktuelle_frage.setText1("Fragenblock 2 Nr. " + fragen_zaehler + " von " + fragen_anzahl);
                aktuelle_frage.setText2("Fragentext 2 B");

                aktuelle_frage.setAntwortA( getTestAntwort( true,                1, true,  "A" ) );
                aktuelle_frage.setAntwortB( getTestAntwort( true,                2, false, "B" ) );
                aktuelle_frage.setAntwortC( getTestAntwort( fragen_zaehler >= 2, 3, false, "C" ) );
                aktuelle_frage.setAntwortD( getTestAntwort( fragen_zaehler >= 3, 4, false, "D" ) );
                aktuelle_frage.setAntwortE( getTestAntwort( fragen_zaehler >= 4, 5, false, "E" ) );
                aktuelle_frage.setAntwortF( getTestAntwort( fragen_zaehler >= 5, 6, false, "F" ) );
                aktuelle_frage.setAntwortH( getTestAntwort( fragen_zaehler >= 6, 7, false, "G" ) );
                aktuelle_frage.setAntwortG( getTestAntwort( true,                8, false, "H" ) );

                aktuelle_frage.setText2("Anzahl Antworten: " + aktuelle_frage.getAnzahlVorhandeneAntworten() + "   korrekt " + aktuelle_frage.getAnzahlKorrekteAntworten() + "   falsch " + aktuelle_frage.getAnzahlFalscheAntworten());

                test_fragen_katalog.addFrage(aktuelle_frage);

                fragen_zaehler++; fragen_nr++;
            }


            fragen_anzahl = 4;

            fragen_zaehler = 1;

            while (fragen_zaehler <= fragen_anzahl)
            {
                aktuelle_frage = new clsFrage();

                aktuelle_frage.setId("ID2_" + fragen_zaehler + "_" + fragen_nr);
                aktuelle_frage.setNummer("" + fragen_nr);
                aktuelle_frage.setText1("Fragenblock 2 Nr. " + fragen_zaehler + " von " + fragen_anzahl);
                aktuelle_frage.setText2("Fragentext 2 B");

                aktuelle_frage.setAntwortA( getTestAntwort( true,                1, true,  "A" ) );
                aktuelle_frage.setAntwortB( getTestAntwort( true,                2, false, "B" ) );
                aktuelle_frage.setAntwortC( getTestAntwort( fragen_zaehler >= 4, 3, false, "C" ) );
                aktuelle_frage.setAntwortD( getTestAntwort( fragen_zaehler >= 3, 4, false, "D" ) );
                aktuelle_frage.setAntwortE( getTestAntwort( fragen_zaehler >= 2, 5, false, "E" ) );
                aktuelle_frage.setAntwortF( getTestAntwort( fragen_zaehler >= 3, 6, false, "F" ) );
                aktuelle_frage.setAntwortH( getTestAntwort( fragen_zaehler >= 4, 7, false, "G" ) );
                aktuelle_frage.setAntwortG( getTestAntwort( true,                8, false, "H" ) );

                aktuelle_frage.setText2("Anzahl Antworten: " + aktuelle_frage.getAnzahlVorhandeneAntworten() + "   korrekt " + aktuelle_frage.getAnzahlKorrekteAntworten() + "   falsch " + aktuelle_frage.getAnzahlFalscheAntworten());

                test_fragen_katalog.addFrage(aktuelle_frage);

                fragen_zaehler++; fragen_nr++;
            }

            fragen_anzahl = 2;
            
            fragen_zaehler = 0;

            while ( fragen_zaehler < fragen_anzahl )
            {
                aktuelle_frage = new clsFrage();

                aktuelle_frage.setId( "ID3_" + fragen_zaehler + "_" + fragen_nr );
                aktuelle_frage.setNummer( "" + fragen_nr );
                aktuelle_frage.setText1( "Fragenblock 3 Nr. " + fragen_zaehler + " von " + fragen_anzahl );
                aktuelle_frage.setText2( "Fragentext 2 C" );

                aktuelle_frage.setAntwortA( getTestAntwort( true, 1, false, "A" ) );
                aktuelle_frage.setAntwortC( getTestAntwort( true, 3, false, "C" ) );
                aktuelle_frage.setAntwortD( getTestAntwort( true, 4, true, "D" ) );
                aktuelle_frage.setAntwortF( getTestAntwort( true, 6, false, "F" ) );
                aktuelle_frage.setAntwortG( getTestAntwort( true, 8, false, "H" ) );

                aktuelle_frage.setText2("Anzahl Antworten: " + aktuelle_frage.getAnzahlVorhandeneAntworten() + "   korrekt " + aktuelle_frage.getAnzahlKorrekteAntworten() + "   falsch " + aktuelle_frage.getAnzahlFalscheAntworten());

                test_fragen_katalog.addFrage( aktuelle_frage );

                fragen_zaehler++; fragen_nr++;
            }

            fragen_anzahl = 2;
            
            fragen_zaehler = 0;

            while ( fragen_zaehler < fragen_anzahl )
            {
                aktuelle_frage = new clsFrage();

                aktuelle_frage.setId( "ID4_" + fragen_zaehler + "_" + fragen_nr );
                aktuelle_frage.setNummer( "" + fragen_nr );
                aktuelle_frage.setText1( "Fragenblock 4 Nr. " + fragen_zaehler + " von " + fragen_anzahl );
                aktuelle_frage.setText2( "Fragentext 2 D" );

                aktuelle_frage.setAntwortA( getTestAntwort( true, 1, false, "A" ) );
                aktuelle_frage.setAntwortC( getTestAntwort( true, 3, false, "C" ) );
                aktuelle_frage.setAntwortD( getTestAntwort( true, 2, true, "B" ) );

                aktuelle_frage.setText2("Anzahl Antworten: " + aktuelle_frage.getAnzahlVorhandeneAntworten() + "   korrekt " + aktuelle_frage.getAnzahlKorrekteAntworten() + "   falsch " + aktuelle_frage.getAnzahlFalscheAntworten());

                test_fragen_katalog.addFrage(aktuelle_frage);

                fragen_zaehler++; fragen_nr++;
            }

            fragen_anzahl = 2;

            fragen_zaehler = 0;

            while ( fragen_zaehler < fragen_anzahl )
            {
                aktuelle_frage = new clsFrage();

                aktuelle_frage.setId( "ID5_" + fragen_zaehler + "_" + fragen_nr );
                aktuelle_frage.setNummer( "" + fragen_nr );
                aktuelle_frage.setText1( "Fragenblock 5 Nr. " + fragen_zaehler + " von " + fragen_anzahl );
                aktuelle_frage.setText2( "Fragentext 2 E" );

                aktuelle_frage.setAntwortA( getTestAntwort( true, 1, false, "A" ) );
                aktuelle_frage.setAntwortC( getTestAntwort( true, 3, false, "C" ) );
                aktuelle_frage.setAntwortD( getTestAntwort( true, 4, true, "D" ) );
                aktuelle_frage.setAntwortF( getTestAntwort( true, 6, false, "F" ) );
                aktuelle_frage.setAntwortH( getTestAntwort( true, 7, true, "G" ) );
                aktuelle_frage.setAntwortG( getTestAntwort( true, 8, false, "H" ) );

                aktuelle_frage.setText2("Anzahl Antworten: " + aktuelle_frage.getAnzahlVorhandeneAntworten() + "   korrekt " + aktuelle_frage.getAnzahlKorrekteAntworten() + "   falsch " + aktuelle_frage.getAnzahlFalscheAntworten());

                test_fragen_katalog.addFrage( aktuelle_frage );

                fragen_zaehler++;
                fragen_nr++;
            }


            fragen_anzahl = 2;

            fragen_zaehler = 0;

            while ( fragen_zaehler < fragen_anzahl )
            {
                aktuelle_frage = new clsFrage();

                aktuelle_frage.setId( "ID6_" + fragen_zaehler + "_" + fragen_nr );
                aktuelle_frage.setNummer( "" + fragen_nr );
                aktuelle_frage.setText1( "Fragenblock 6 Nr. " + fragen_zaehler + " von " + fragen_anzahl );
                aktuelle_frage.setText2( "Fragentext 6 E" );

                aktuelle_frage.setAntwortA( getTestAntwort( true, 1, true, "A" ) );
                aktuelle_frage.setAntwortB( getTestAntwort( true, 2, false, "B" ) );
                aktuelle_frage.setAntwortC( getTestAntwort( true, 3, false, "C" ) );
                aktuelle_frage.setAntwortD( getTestAntwort( true, 4, true, "D" ) );
                aktuelle_frage.setAntwortE( getTestAntwort( true, 5, false, "E" ) );
                aktuelle_frage.setAntwortH( getTestAntwort( true, 7, true, "G" ) );
                aktuelle_frage.setAntwortG( getTestAntwort( true, 8, false, "H" ) );

                aktuelle_frage.setText2("Anzahl Antworten: " + aktuelle_frage.getAnzahlVorhandeneAntworten() + "   korrekt " + aktuelle_frage.getAnzahlKorrekteAntworten() + "   falsch " + aktuelle_frage.getAnzahlFalscheAntworten());

                test_fragen_katalog.addFrage( aktuelle_frage );

                fragen_zaehler++;
                fragen_nr++;
            }


            return test_fragen_katalog;
        }

        /* 
         * ################################################################################
         */
        private static clsAntwort getTestAntwort( bool pKnzAufnahme, int pNummer, bool pKnzKorrekt, String pAntwortBez )
        {
            if ( pKnzAufnahme == false )
            {
                return null;
            }

            clsAntwort aktuelle_antwort = new clsAntwort();

            aktuelle_antwort.setAntwortBez( "" + pNummer );
            aktuelle_antwort.setAntwortBez( "" + pNummer );
            aktuelle_antwort.setAntwortText( "Antwort Nr. " + pNummer + " " + pAntwortBez + ( pKnzKorrekt ? " RICHTIG " : "" ) );

            aktuelle_antwort.setKnzKorrekt( pKnzKorrekt );

            return aktuelle_antwort;
        }
    }
}
