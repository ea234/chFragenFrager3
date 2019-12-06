using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class fkString
{
    /** Alle Zeichenfolgen, welchen einen wahren boolschen Wert ergeben koennen. Durch Komma getrennt. ",true,yes,ja,y,j,1,on,+,ein,wahr,ok," */
    private static  String STR_WERTE_BOOLEAN_TRUE        = ",true,yes,ja,y,j,1,on,+,ein,wahr,ok,";

    private static  String STR_WERT_BOOLEAN_TRUE         = ",true,";

    /** Alle Zeichenfolgen, welchen einen nicht wahren boolschen Wert ergeben koennen. Durch Komma getrennt. ",false,no,nein,n,0,off,-,aus,falsch," */
    private static  String STR_WERTE_BOOLEAN_FALSE       = ",false,no,nein,n,0,off,-,aus,falsch,";

    private static  String STR_WERT_BOOLEAN_FALSE        = ",false,";

    /*
     * ################################################################################
     */
    public static String trim( String pString )
    {
        if ( pString == null )
        {
            return null;
        }

        return pString.Trim();
    }

    /**
     * <pre>
     * Gleichnamige Version aus Visual-Basic.
     * 
     * FkString.InStr( 0    , "ABC.DEF.GHI.KLM",  "ABC" )   =  0
     * FkString.InStr( 0    , "ABC.DEF.GHI.KLM",  "GHI" )   =  8
     * FkString.InStr( 1    , "ABC.DEF.GHI.KLM",  "ABC" )   = -1
     * FkString.InStr( 0    , null,               "ABC" )   = -1
     * FkString.InStr( 0    , "ABC.DEF.GHI.KLM",  "XYZ" )   = -1
     * FkString.InStr( 0    , "ABC.DEF.GHI.KLM",  null  )   = -1
     * FkString.InStr( 100  , "ABC.DEF.GHI.KLM",  "ABC" )   = -1
     * FkString.InStr( -10  , "ABC.DEF.GHI.KLM",  "ABC" )   =  0
     * </pre>
     * 
     * @param pStartPosition die Position ab welcher mit der Suche begonnen werden soll
     * @param pZuDurchsuchenderString der zu durchsuchende String 
     * @param pSuchString der String, welcher gesucht wird
     * @return die eventuell gefundene Position, im Fehlerfall -1
     */
    public static int indexOf( String pZuDurchsuchenderString, String pSuchString, int pStartPosition )
    {
        if ( pZuDurchsuchenderString == null )
        {
            return -1;
        }

        if ( pSuchString == null )
        {
            return -1;
        }

        if ( pStartPosition < 0 )
        {
            return -1;
        }

        if ( pStartPosition >= pZuDurchsuchenderString.Length )
        {
            return -1;
        }

        return pZuDurchsuchenderString.IndexOf( pSuchString, pStartPosition );
    }

    /**
     * Schneidet Anzahl-Stellen von dem uebergebenen String ab und gibt diesen zurueck.
     * 
     * @param pString der Quellstring
     * @param pAnzahlStellen die Anzahl der von links abzuschneidenden Stellen
     * @return den sich ergebenden String, Leerstring wenn die Anzahl der Stellen negativ ist oder pString null ist
     */
    public static String left( String pString, int pAnzahlStellen )
    {
        if ( pString == null )
        {
            return "";
        }

        if ( pAnzahlStellen <= 0 )
        {
            return "";
        }

        if ( pAnzahlStellen < pString.Length )
        {
            return pString.Substring( 0, pAnzahlStellen );
        }

        return pString;
    }
 
    /**
     * <pre>
     * Schneidet die Anzahl-Stellen von dem uebergebenen String ab und gibt diesen zurueck.
     * 
     * Uebersteigt die Anazhl der abzuschneidenden Stellen die Stringlaenge, wird der 
     * Quellstring insgesamt zurueckgegeben.
     * 
     * Ist die Anzahl der abzuschneidenden Stellen negativ oder 0, wird ein Leerstring zurueckgegeben.
     * 
     * FkString.right( "ABC.DEF.GHI.JKL",  7 ) = "GHI.JKL"
     * FkString.right( "ABC.DEF.GHI.JKL", 20 ) = "ABC.DEF.GHI.JKL" = Anzahl Stellen uebersteigt Stringlaenge
     * FkString.right( "ABC.DEF.GHI.JKL",  0 ) = ""                = 0 Stellen abschneiden = Leerstring
     * FkString.right( "ABC.DEF.GHI.JKL", -7 ) = ""                = negative Anzahl       = Leerstring
     * 
     * </pre>
     * 
     * @param pString der Quellstring
     * @param pAnzahlStellen die Anzahl der von rechts abzuschneidenden Stellen
     * @return der ermittelte Teilstring
     */
    public static String right( String pString, int pAnzahlStellen )
    {
        if ( ( pString != null ) && ( pAnzahlStellen > 0 ) )
        {
            /*
             * Die Ab-Position ist die Laenge des Eingabestrings, abzueglich der von 
             * rechts abzuschneidenden Stellen. Die Ab-Postion darf aber nicht negativ
             * werden. Die minimale Ab-Position ist der Stringanfang (Position 0).
             * 
             * Die Bis-Position ist die laenge des Eingabestrings.
             */
            return pString.Substring( Math.Max( 0, pString.Length - pAnzahlStellen ), pAnzahlStellen );
        }

        /*
         * pString nicht gesetzt oder pAnzahlStellen < 0
         */
        return "";
    }

    /**
     * <pre>
     * Stellt die Zeichen der Eingabe zufaellig um.   
     *
     * Ist "pString" gleich null, wird null zurueckgegeben.
     * 
     * Ist die Laenge von "pString" gleich 1, wird "pString" zurueckgegeben.
     *
     * Ist "pAnzahlDurchlaeufe" kleiner 1, wird ein Vertauschungsdurchlauf gemacht.
     * 
     * Eingabe: 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz
     * Ausgabe: Z9j7LmPHIKA58JdgWfaFv2SCklGDYpstRXq0o1u6iVcNnr4eByzhMUEb3TOwxQ
     * </pre>
     * 
     * @param pAnzahlDurchlaeufe die Anzahl der Schleifendurchlaeufe
     * @param pString die umzustellende Eingabezeichenfolge
     * @return die Eingabezeichenfolge mit zufaelliger Umstellung der Zeichenpositionen
     */
    public static String getRandomUmgestellt( int pAnzahlDurchlaeufe, String pString )
    {
        /*
         * Pruefung: pString gleich "null" ?
         *
         * Ist pString nicht gesetzt, wird "null" zurueckgegeben.
         *
         */
        if (pString == null)
        {
            return null;
        }

        /*
         * Pruefung: Laenge gleich 1 Zeichen ?
         *
         * Ist die Laenge von "pString" gleich 1, wird keine Umstellung gemacht.
         * Es gibt keine sinnvolle Umstellung. Der Aufrufer bekommt "pString" zurueck.
         */
        if ( pString.Length == 1 )
        {
            return pString;
        }

        /*
         * Keine negativen Durchlaeufe und mindestens ein Durchlauf
         */
        if ( pAnzahlDurchlaeufe < 1 )
        {
            pAnzahlDurchlaeufe = 1;
        }

        /*
         * Die vertauschungen werden in einem Array durchgefuehrt
         */
        var array_ergebnis = pString.ToCharArray();

        Random random_instanz = new Random();

        /*
         * Hilfsvariable fuer den Tausch
         */
        char temp_char = ' ';

        int anzahl_tausch_operationen = pString.Length;

        int random_zahl_grenze = pString.Length - 1;

        int tausch_position_1 = 0;

        int tausch_position_2 = 0;
        /*
         * Verhinderung einer Endlosschleife
         */
        int zaehler = 0;

        int nr_durchlauf = 1;

        /*
         * While-Schleife fuer die Vertauschungslaeufe.
         * Die While-Schleife laeuft solange, wie der Zaehler fuer die  Durchlaeufe noch
         * nicht die Anzahl aus dem Parameter "pAnzahlDurchlaeufe" erreicht hat.
         */
        while ( nr_durchlauf <= pAnzahlDurchlaeufe )
        {
            /*
             * Jeder Tauschdurchlauf startet beim ersten Zeichen des Strings.
             * Die Tausposition 1 wird auf 0 gestellt.
             */
            int nr_tausch_operation = 1;

            /*
             * Pruefung: Stringlaenge gleich 2 Zeichen ?
             *
             * Hat der zu vertauschende String nur 2 Zeichen, wird die
             * Tauschposition 2 auf das zweite Zeichen eingestellt und
             * die Positionen werden vertauscht.
             *
             * Sind mehr Zeichen im zu vertauschenden String vorhanden,
             * wird eine While-Schleife gestartet.
             */
            if (anzahl_tausch_operationen == 2)
            {
                tausch_position_2 = 1;

                temp_char = array_ergebnis[tausch_position_1];

                array_ergebnis[tausch_position_1] = array_ergebnis[tausch_position_2];

                array_ergebnis[tausch_position_2] = temp_char;

                /*
                 * Per Zufall wird bestimmt, ob es noch einen weiteren Tausch-Durchlauf geben soll.
                 *
                 * Es wird eine Zufallszahl zwischen 0 und 100 erstellt.
                 * Ist die Zufallszahl groesser 50, gibt es keinen zweiten Durchlauf
                 *
                 * Soll es keinen weiteren Durchlauf mehr geben, wird der Durchlaufzaehler auf
                 * die Maximalanzahl der Durchlaeufe gesetzt.
                 */
                if (random_instanz.Next(100) > 50)
                {
                    nr_durchlauf = pAnzahlDurchlaeufe + 1;
                }
            }
            else
            {
                /*
                 * While-Schleife fuer jedes Zeichen der Eingabe.
                 * Jede Position des Eingabestrings wird einmal vertauscht.
                 */
                while ( nr_tausch_operation < anzahl_tausch_operationen )
                {
                    zaehler = 0;

                    try
                    {
                        /*
                         * Position 1 der Vertauschungen ist der Index der inneren While-Schleife
                         *
                         * Position 2 der Vertauschungen wird per Zufall gewaehlt
                         */
                        tausch_position_1 = nr_tausch_operation;

                        tausch_position_2 = random_instanz.Next( random_zahl_grenze );

                        /*
                         * Mit einer dritten While-Schleife wird verhindert, dass die
                         * beiden Tauschpositionen gleich sind.
                         *
                         * In der While-Schleife wird die Tauschpositon 2 neu vergeben,
                         * sollte diese gleich der ersten Tauschpositon sein.
                         *
                         * Es wird 10 mal versucht, unterschiedliche Tauschpositionen zu beommen
                         */
                        while ( ( tausch_position_2 == tausch_position_1 ) && ( zaehler < 10 ) )
                        {
                            tausch_position_2 = random_instanz.Next( random_zahl_grenze );

                            zaehler++;
                        }

                        /*
                         * Pruefung: Tauschpositionen unterschiedlich ?
                         *
                         * Sind die Tauschpositionen unterschiedlich, wird die Vertauschung gemacht.
                         *
                         * Sind die Tauschpoistionen gleich, wird keine Vertauschung gemacht.
                         */
                        if ( tausch_position_2 != tausch_position_1 )
                        {
                            temp_char = array_ergebnis[ tausch_position_1 ];

                            array_ergebnis[ tausch_position_1 ] = array_ergebnis[ tausch_position_2 ];

                            array_ergebnis[ tausch_position_2 ] = temp_char;
                        }
                    }
                    catch (Exception abgf_fehler)
                    {
                        // nicht vorhandener Index 
                    }

                    /*
                     * Am Ende der zweiten While-Schleife wird die Tauschposition 1 um
                     * eine Position weitergestellt.
                     */
                    nr_tausch_operation++;
                }
            }

            /*
             * Am Ende der ersten While-Schleife wird der Durchlaufzaehler um 1 erhoeht
             */
            nr_durchlauf++;
        }

        /*
         * Am Ende der Funktion wird dem Aufrufer der umgestellte String zurueckgegeben.
         */
        return new String(array_ergebnis);
    }


    /**
     * <pre>
     * MID-Funktion
     * Liefert einen String, die eine festgelegte Zeichenanzahl aus einer Zeichenfolge enthaelt.
     * 
     * </pre>
     * 
     * @param pString der Quellstring, aus welchem herausgeschnitten werden soll
     * @param pAbPosition die Position, ab welcher abgeschnitten werden soll
     * @param pLaenge die Laenge der abzuschneidenden Zeichen 
     * @return einen String 
     */
    public static String Mid( String pString, int pAbPosition, int pLaenge )
    {
        if ( pString == null )
        {
            return "";
        }

        try
        {
            if ( pLaenge < 0 )
            {
                return pString.Substring( pAbPosition );
            }

            return pString.Substring( pAbPosition, pLaenge );
        }
        catch ( Exception abgf_fehler )
        {
        }

        return "";

        /*
         * int trenn_pos_ab = (int) ( );
         * int trenn_laenge = (int) ( );
         * 
         * if ( trenn_laenge < 0 )
         * {
         *   ergebnis_mid = pEingabe.substring( trenn_pos_ab );
         * }
         * else if ( laenge_eingabe < trenn_pos_ab + trenn_laenge ) 
         * {
         *   ergebnis_mid = pEingabe.substring( trenn_pos_ab );
         * }
         * else
         * {
         *   ergebnis_mid = pEingabe.substring( trenn_pos_ab, trenn_pos_ab + trenn_laenge );
         * }
         */
    }

    /*
     * ################################################################################
     */
    public static int MidC( String pString, int pAbPosition, int pLaenge )
    {
        return Convert.ToInt32( Mid( pString, pAbPosition, pLaenge ) );
    }

    /**
     * <pre>
     * Ermittelt einen Boolschen Wert aus dem uebergebenen String unabhaengig von der
     * Gross/Kleinschreibung. Bei null wird der Vorgabewert zurueckgegeben.
     * 
     * TRUE - 1, j, y, +, t, yes, true, ja, ein, an 
     * FALSE - 0, n, -, f, no, false, nein, aus, 
     * </pre>
     * @param pString der Wert, welcher entweder einen Zustand true oder false beschreibt
     * @param pVorgabeWert der Vorgabewert fuer keine Uebereinstimmung
     * @return true oder false
     */
    public static bool getBoolean( String pString, bool pVorgabeWert )
    {
        if ( ( pString != null ) && ( pString.Length > 0 ) )
        {
            if ( STR_WERT_BOOLEAN_TRUE.IndexOf( "," + pString.ToLower() ) >= 0 )
            {
                return true;
            }

            if ( STR_WERT_BOOLEAN_FALSE.IndexOf( "," + pString.ToLower() ) >= 0 )
            {
                return false;
            }

            /*
             * Damit keine Teilzeichenfolgen gefunden werden, wird ein Komma 
             * vor die Eingabe gestellt. Die Uebereinstimmung muss also mit einem
             * Komma beginnen. 
             */
            if ( STR_WERTE_BOOLEAN_TRUE.IndexOf( "," + pString.ToLower() ) >= 0 )
            {
                return true;
            }

            if ( STR_WERTE_BOOLEAN_FALSE.IndexOf( "," + pString.ToLower() ) >= 0 )
            {
                return false;
            }
        }

        return pVorgabeWert;
    }
  
    /**
     * <pre>
     * Formatiert einen Text Spaltenweise
     * 
     * 
     * 
     * String eingabe_string = "12345 67890 12345 67890 12345 67890 12345 67890 ";
     * 
     * String ausgabe_string = FkStringText.getStringMaxCols( eingabe_string, 13, "...", "\n" );
     * 
     * System.out.println( ausgabe_string );
     * 
     *                                10        20        30        40        50   
     *                       123456789012345678901234567890123456789012345678901234
     *                     
     * Eingabestring       = 12345 67890 12345 67890 12345 67890 12345 67890 
     * 
     * Von     0 bis    17 = 12345 67890 12345
     * Von    18 bis    35 = 67890 12345 67890
     * Von    36 bis    49 = 12345 67890 
     * 
     * 12345 67890 12345
     * ...67890 12345 67890
     * ...12345 67890 
     * 
     * </pre>
     * 
     * @param pEingabe der zu formartierende Tex
     * @param pAnzahlZeichenJeZeile die Anzahl der Zeichen je Zeile
     * @param pEinzug der Einzug, welcher ab Zeile 2 hinzugefuegt wird
     * @param pNewLineZeichen das zu benutzende New-Line-Zeichen (ab Zeile 2)
     * @return den formartierten Text
     */
    public static String getStringMaxCols( String pEingabe, int pAnzahlZeichenJeZeile, String pEinzug, String pNewLineZeichen )
    {
      char char_leer_zeichen  = ' ';
      char char_zeilenumbruch = '\n';

      String str_ergebnis = "";
      String my_cr = "";
      String neue_zeile = "";

      int trenn_position_ab = -1;

      int trenn_position_bis_init = -1;
      int trenn_position_bis_plus = -1;
      int trenn_position_bis_minus = -1;

      int laenge_eingabe = 0;
      int zaehler = 0;

      /* 
       * Pruefung: Parameter pAnzahlZeichenJeZeile kleiner gleich 10?
       * Ist der Parameter kleiner der Mindesspaltenanzahl von 10, wird die
       * Anzahl der der Spalten auf die Vorgabe von 10 Stellen gesetzt.
       */
      if ( pAnzahlZeichenJeZeile <= 10 )
      {
        pAnzahlZeichenJeZeile = 10;
      }

      /* 
       * Pruefung: Ist die Eingabe "null" ?
       * 
       * Ist die Eingabe "null" ist das Ergebnis ein Leerstring.
       */
      if ( pEingabe != null )
      {
        /* 
         * Pruefung: Laenge Eingabe kleiner als Max-Spaltenanzahl ?
         * 
         * Ist die Eingabe kuerzer als die maximale Spaltennazahl, ist das Ergebnis
         * gleich der Eingabestring, da dieser nicht ueber die Max-Spalten hinaus geht.
         */
        if ( pEingabe.Length <= pAnzahlZeichenJeZeile )
        {
          str_ergebnis = pEingabe;
        }
        else
        {
            var max_str_pos = pEingabe.Length - 1;

          var akt_char = char_leer_zeichen;

          /* 
           * Ist die Eingabe laenger als die maximale Spaltenanzahl wird die
           * Verkleinerungsschleife gestartet.
           */
          str_ergebnis = "";

          laenge_eingabe = pEingabe.Length;

          /* 
           * Die Schleife laeuft solange wie
           * ... die Bis-Position noch kleiner der Laenge der Eingabe ist.
           * ... der Endlosschleifenverhinderungszaehler kleiner 32123 ist.
           */
          while ( ( ( trenn_position_bis_plus < laenge_eingabe ) && ( zaehler < 32123 ) ) )
          {
            /* 
             * Startposition
             * 
             * Die Position ab welcher die neue Zeile aus der Eingabe herausgetrennt wird, liegt
             * ein Zeichen hinter der letzten Bis-Trennposition. 
             * 
             * Die Bis-Position wurde bei der Deklaration mit "-1" initialisiert. 
             * Bei der ersten Zeile wird daraus eine Ab-Position von 0. 
             */
            trenn_position_ab = trenn_position_bis_plus + 1;

            /*
             * White-Space am Start werden mit einer While-Schleife uebersprungen. 
             */
            akt_char = pEingabe[ trenn_position_ab ];

            while ( ( trenn_position_ab < max_str_pos ) && ( ( akt_char == char_leer_zeichen ) || ( akt_char == char_zeilenumbruch ) ) )
            {
              trenn_position_ab++;

              akt_char = pEingabe[ trenn_position_ab ];
            }

            /*
             * End-Positon
             * 
             * Es wird die naechste initialie Trennposition berechnet.
             * 
             * Diese berechnet sich aus der Startposition zuzueglich der Anzahl von  
             * Zeichen aus dem Parameter "pAnzahlZeichenJeZeile".
             */
            trenn_position_bis_init = trenn_position_ab + pAnzahlZeichenJeZeile;

            trenn_position_bis_plus = trenn_position_bis_init;

            if ( trenn_position_bis_plus > max_str_pos )
            {
              /*
               * Bis-Position nach Stringende
               * Liegt die berechnete BIS-Position nach dem Stringende, ist die "neue_zeile"
               * gleich dem Rest des Eingabestrings ab der AB-Position. 
               */
              neue_zeile = pEingabe.Substring( trenn_position_ab );
            }
            else
            {
              /*
               * Suche nach dem naechsten Leer- oder Newline-Zeichen ab der Bis-Position
               * 
               * Ist das Zeichen an der Bis-Positon schon ein Trennzeichen, wird die 
               * Schleife nicht gestartet.
               * 
               * Befindet sich an der Bis-Position ein anderes Zeichen, wird die Suchschleife gestartet.
               * 
               * Die Suche in der Schleife bewegt sich zum Stringende hin.
               */
              akt_char = pEingabe[ trenn_position_bis_plus ];

              while ( ( trenn_position_bis_plus < laenge_eingabe ) && ( ( akt_char != char_leer_zeichen ) && ( akt_char != char_zeilenumbruch ) ) )
              {
                trenn_position_bis_plus++;

                akt_char = pEingabe[ trenn_position_bis_plus ];
              }

              /*
               * Flattersatz vermeiden
               * 
               * Liegt die naechste Trennposition mehr als 5 Zeichen hinter der initialen
               * Startposition wird in einer weiteren Suchschleife, das letzte Trennzeichen
               * ab der Bis-Position gesucht.
               */
              if ( ( trenn_position_bis_plus - trenn_position_bis_init ) > 5 )
              {
                /*
                 * Startwert fuer die Suchschleife ist die berechnete initiale Trennpositon.
                 */
                trenn_position_bis_minus = trenn_position_bis_init;
                
                /*
                 * Suche nach dem letztem Leer- oder Newline-Zeichen von der Bis-Position
                 * 
                 * Die Suche in der Schleife bewegt sich zur Startposition hin.
                 */
                akt_char = pEingabe[ trenn_position_bis_minus ];

                while ( ( trenn_position_bis_minus > trenn_position_ab ) && ( ( akt_char != char_leer_zeichen ) && ( akt_char != char_zeilenumbruch ) ) )
                {
                  trenn_position_bis_minus--;

                  akt_char = pEingabe[ trenn_position_bis_minus ];
                }

                /*
                 * Trennpositionsermittlung
                 * 
                 * Von der initialen Bis-Position werden die Zeichen bis zum naechsten Trennzeichen gezaehlt.
                 *  
                 * Dieses einmal in Richtung Startposition und in Richtung Stringende.
                 * 
                 * Es wird die kleinere Position fuer das Trennen genommen. 
                 */
                if ( ( trenn_position_bis_init - trenn_position_bis_minus ) < ( trenn_position_bis_plus - trenn_position_bis_init ) )
                {
                  trenn_position_bis_plus = trenn_position_bis_minus;
                }
              }

              if ( trenn_position_bis_init > max_str_pos )
              {
                /*
                 * Bis-Position nach Stringende
                 * Liegt die berechnete BIS-Position nach dem Stringende, ist die "neue_zeile"
                 * gleich dem Rest des Eingabestrings ab der AB-Position. 
                 */
                neue_zeile = pEingabe.Substring( trenn_position_ab );
              }
              else
              {
                /*
                 * Teilstring liegt im String
                 * Liegt die berechnete BIS-Position vor dem Stringende, ist die "neue_zeile"
                 * gleich dem Teilstring ab der AB-Position bis zur Bis-Position.
                 */              
                neue_zeile = pEingabe.Substring( trenn_position_ab, ( trenn_position_bis_plus - trenn_position_ab ) );
              }
            }

            if ( neue_zeile != null )
            {
              str_ergebnis = str_ergebnis + my_cr + neue_zeile.Trim();
            }

            my_cr = pNewLineZeichen + pEinzug;

            zaehler = zaehler + 1;
          }
        }
      }

      return str_ergebnis;
    }


}