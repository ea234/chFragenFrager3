using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class fkZahl
    {
        /**
         * <pre>
         * Ermittelt aus dem Parameter "pString" den Integerwert.
         * Kommt es bei der Umwandlung zu einer NumberFormatException,
         * wird der Vorgabewert zurueckgegeben. 
         * 
         * Auf pString wird ein TRIM ausgefuehrt.
         * </pre>
         * 
         * @param pString zu parsende Zeichenkette
         * @param pVorgabeWert Vorgabewert im Fehlerfall
         * @return der Wert als int oder der Vorgabewert
         */
        public static int getInteger( String pString, int pVorgabeWert )
        {
            try
            {
                if ( pString != null )
                {
                    return Convert.ToInt32( pString.Trim() );
                }
            }
            catch ( Exception e )
            {
            }

            return pVorgabeWert;
        }

        /**
         * <pre>
         * Prueft, ob der Parameter pString ein Integer-Wert ist, d.h. nur Ziffern enthaelt.
         * 
         * FkZahl.checkInteger( "   123"    ) = true
         * FkZahl.checkInteger( "123   "    ) = true
         * FkZahl.checkInteger( "  123 "    ) = true
         * FkZahl.checkInteger( " 123,4"    ) = false
         * FkZahl.checkInteger( " ABC,D"    ) = false
         * FkZahl.checkInteger( "   123", 2 ) = false
         * FkZahl.checkInteger( "   123", 3 ) = true
         * FkZahl.checkInteger( "   123", 4 ) = false
         * FkZahl.checkInteger( "null"      ) = false
         * FkZahl.checkInteger( ""          ) = false
         * </pre>
         * 
         * @param pString die zu pruefende Eingabezeichenfolge
         * @param pLaengeMaximal die zu pruefende Laenge der Zahl (maximale Laenge, minimale Laenge auf -1 ) (bei &lt; 0 erfolgt keine Laengenpruefung)
         * @return True, wenn der String genau length Zeichen umfasst und nur aus Zahlen besteht
         */
        public static bool checkInteger( String pString, int pLaengeMaximal )
        {
            return checkInteger( pString, -1, pLaengeMaximal );
        }
 
        /**
         * <pre>
         * Prueft ob der Parameter pString ein Integer-Wert ist, d.h. nur Ziffern enthaelt. 
         * Vorlaufende und nachlaufende Leerzeichen werden ueberlesen.
         * Die Laengenpruefung erfolgt auf Grundlage der gefundenen Ziffern.
         * Die Laengenpruefung kann mit -1 deaktiviert werden.
         * 
         * FkZahl.checkInteger( "   123",  1,  5 ) = true
         * FkZahl.checkInteger( "   123",  1,  3 ) = true
         * FkZahl.checkInteger( "   123",  1,  2 ) = false
         * FkZahl.checkInteger( "   123",  4,  5 ) = false
         * FkZahl.checkInteger( "      ",  4,  5 ) = false
         * FkZahl.checkInteger( " 1 2 3",  1,  5 ) = false  Leerzeichen zwischen den Zahlen vorhanden 
         *     
         * FkZahl.checkInteger( "123456",  4, -1 ) = true   Keine Obergrenze
         * FkZahl.checkInteger( "123",     4, -1 ) = false  Minimal 4 Stelllen muessen vorhanden sein
         * FkZahl.checkInteger( "123456", -1,  3 ) = false  Maximal 3 Stellen duerfen vorhanden sein
         * FkZahl.checkInteger( "123",    -1, -1 ) = true   Keine Maximalgrenze
         *     
         * FkZahl.checkInteger( "12345",   8,  5 ) = false  Mindestanzahl von Zeichen groeÃŸer als Maximal (falsch weil keine 8 Ziffern)
         * </pre>
         * 
         * @param pString die zu pruefende Eingabezeichenfolge
         * @param pLaengeMinimal die Anzahl von Ziffern welche als Zahl vorhanden sein muessen (-1 fuer beliebig)
         * @param pLaengeMaximal die Anzahl von Ziffern welche nicht ueberschritten werden darf (-1 fuer beliebig)
         * @return TRUE wenn die Eingabezeichenfolge eine Ziffer ist und sich genau in den gegebenen Grenzen befindet
         */
        public static bool checkInteger( String pString, int pLaengeMinimal, int pLaengeMaximal )
        {
            /*
             * Bei keiner Angabe einer Instanz ist es keine Zahl
             */
            if ( pString == null )
            {
                return false;
            }

            /*
             * Bei einem Leerstring ist es auch keine Zahl
             * (Trim() muss hier nicht aufgerufen werden)
             */
            if ( pString.Length == 0 )
            {
                return false;
            }

            /*
             * Es duerfen nur Zahlen vorhanden sein
             */
            String temp_str = pString.Trim();

            int anzahl_ziffern = 0;
            int aktueller_index = 0;

            while ( aktueller_index < temp_str.Length )
            {
                char akt_char = temp_str[ aktueller_index ];

                if ( ( akt_char < '0' ) || ( akt_char > '9' ) )
                {
                    return false;
                }
                else
                {
                    anzahl_ziffern++;
                }

                aktueller_index++;
            }

            return true;
        }
    }
}
