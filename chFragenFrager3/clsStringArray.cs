using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class clsStringArray
{
    private List<String> m_array_strings = null;
     
    /* 
     * ################################################################################
     */
    private List<String> getVektor()
    {
        if ( m_array_strings == null )
        {
            m_array_strings = new List<String>();
        }

        return m_array_strings;
    }

    /* 
     * ################################################################################
     */
    public int getAnzahlStrings()
    {
        if ( m_array_strings != null )
        {
            return m_array_strings.Count;
        }

        return 0;
    }

    /* 
     * ################################################################################
     */
    public void clear()
    {
        if ( m_array_strings != null )
        {
            int aktueller_index = 0;

            int frage_vector_anzahl = getAnzahlStrings();

            while ( aktueller_index < frage_vector_anzahl )
            {
                try
                {
                    m_array_strings[ aktueller_index ] = null;
                }
                catch ( Exception e )
                {
                    //
                }

                aktueller_index++;
            }
        }

        m_array_strings = null;
    }

    /*
     * ################################################################################
     */
    public void addString( String pString )
    {
        getVektor().Add( pString );
    }

    /*
     * ################################################################################
     */
    public String getString( int pIndex )
    {
        if ( ( pIndex > 0 ) && ( pIndex < getAnzahlStrings() ) )
        {
            return m_array_strings[ pIndex ];
        }

        return null;
    }

    /*
     * ################################################################################
     */
    public String getString( String pZeichenZeilenumbruch, bool pKnzLeerzeilenEntfernen )
    {
        String ergebnis_str = "";
        int aktueller_index = 0;

        while ( aktueller_index < getAnzahlStrings() )
        {
            if ( pKnzLeerzeilenEntfernen )
            {
                if ( m_array_strings[ aktueller_index ].Trim() != "" )
                {
                    ergebnis_str = ergebnis_str + m_array_strings[ aktueller_index ] + pZeichenZeilenumbruch;
                }
            }
            else
            {
                ergebnis_str = ergebnis_str + m_array_strings[ aktueller_index ] + pZeichenZeilenumbruch;
            }

            aktueller_index++;
        }

        return ergebnis_str;
    }

    /*
     * ################################################################################
     */
    public void doItSubstring( int pAbPosition )
    {
        int aktueller_index = 0;

        if ( pAbPosition > 0 )
        {
            while ( aktueller_index < getAnzahlStrings() )
            {
                if ( m_array_strings[ aktueller_index ].Length >= pAbPosition )
                {
                    int anzahl_r = m_array_strings[ aktueller_index ].Length - ( pAbPosition - 1 );

                    m_array_strings[ aktueller_index ] = fkString.right( m_array_strings[ aktueller_index ], anzahl_r ); // );
                }
                else
                {
                    m_array_strings[ aktueller_index ] = "";
                }

                aktueller_index++;
            }
        }
    }

    /* 
     * ################################################################################
     */
    private void QuickSort( int pIndexStart, int pIndexEnde, bool pKnzAufsteigend )
    {
        String temp_string = "";

        int index_start = pIndexStart;
        int index_ende  = pIndexEnde;

        String vergleichs_string_mitte = m_array_strings[ Convert.ToInt32( ( index_start + index_ende ) * 0.5 ) ];

        while ( index_start <= index_ende )
        {
            if ( pKnzAufsteigend )
            {
                while ( m_array_strings[ index_start ].CompareTo( vergleichs_string_mitte ) < 0 )
                {
                    index_start++;
                }

                while ( m_array_strings[ index_ende ].CompareTo( vergleichs_string_mitte ) > 0 )
                {
                    index_ende--;
                }
            }
            else
            {
                while ( m_array_strings[ index_start ].CompareTo( vergleichs_string_mitte ) > 0 )
                {
                    index_start++;
                }

                while ( m_array_strings[ index_ende ].CompareTo( vergleichs_string_mitte ) < 0 )
                {
                    index_ende--;
                }
            }

            if ( index_start <= index_ende )
            {
                temp_string = m_array_strings[ index_start ];

                m_array_strings[ index_start ] = m_array_strings[ index_ende ];

                m_array_strings[ index_ende ] = temp_string;

                index_start++;

                index_ende--;
            }
        }

        if ( pIndexStart < index_ende )
        {
            QuickSort( pIndexStart, index_ende, pKnzAufsteigend );
        }

        if ( index_start < pIndexEnde )
        {
            QuickSort( index_start, pIndexEnde, pKnzAufsteigend );
        }
    }

    /*
     * ################################################################################
     */
    public void startSortierungAufsteigend()
    {
        QuickSort( 1, m_array_strings.Count - 1, true );
    }
}