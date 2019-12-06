using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

public class fkIniDatei
{
    private static int zaehler_x = 0;

    public static int capacity = 512;

    [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW",   SetLastError=true, CharSet=CharSet.Unicode, ExactSpelling=true,   CallingConvention=CallingConvention.StdCall)]
    private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnString, int nSize, string lpFilename);

    [DllImport("KERNEL32.DLL", EntryPoint="WritePrivateProfileStringW",   SetLastError=true, CharSet=CharSet.Unicode, ExactSpelling=true,   CallingConvention=CallingConvention.StdCall)]
    private static extern int WritePrivateProfileString( string lpAppName, string lpKeyName, string lpString, string lpFilename);

    /*
     * ################################################################################
     */
    public static String readIniDateiName( String pIniKey )
    {
        var str_ini = new StringBuilder( capacity );

        int anzahl = GetPrivateProfileString( "DATEI_NAMEN", "" + pIniKey, "", str_ini, str_ini.Capacity, getAnwIniDateiName() );

        return str_ini.ToString();
    }

    /*
     * ################################################################################
     */
    public static int writeIniDateiName( String pIniKey, String pIniValue )
    {
        return WritePrivateProfileString( "DATEI_NAMEN", pIniKey, pIniValue, getAnwIniDateiName() );
    }

    /*
     * ################################################################################
     */
    public static int writeIniBildPosition( int pBildTop, int pBildLeft )
    {
        WritePrivateProfileString( "OPTIONEN", "BILD_POSITION_TOP", "" + pBildTop, getAnwIniDateiName() );

        WritePrivateProfileString( "OPTIONEN", "BILD_POSITION_LEFT", "" + pBildLeft, getAnwIniDateiName() );

        return 1;
    }

    /*
     * ################################################################################
     */
    public static int reatIniInt( String pIniSection, String pIniKey, int pDefaultValue )
    {
        try
        {
            var str_ini = new StringBuilder( capacity );

            int anzahl = GetPrivateProfileString( pIniSection, pIniKey, "" + pDefaultValue, str_ini, str_ini.Capacity, getAnwIniDateiName() );

            return Convert.ToInt32( str_ini.ToString() );
        }
        catch ( Exception err_inst )
        {
            //
        }

        return pDefaultValue;
    }

    /*
     * ################################################################################
     */
    public static String readIniString( String pIniSection, String pIniKey, String pDefaultValue )
    {
        try
        {
            var str_ini = new StringBuilder( capacity );

            int anzahl = GetPrivateProfileString( pIniSection, pIniKey, pDefaultValue, str_ini, str_ini.Capacity, getAnwIniDateiName() );

            return str_ini.ToString();
        }
        catch ( Exception err_inst )
        {
            //
        }

        return pDefaultValue;
    }

    /*
     * ################################################################################
     */
    public static void writeIniString( String pIniSection, String pIniKey, String pValue )
    {
        try
        {
            WritePrivateProfileString( pIniSection, pIniKey, pValue, getAnwIniDateiName() );
        }
        catch ( Exception err_inst )
        {
            //
        }
    }

    /*
     * ################################################################################
     */
    public static void writeIniBoolean( String pIniSection, String pIniKey, bool pValue )
    {
        try
        {
            WritePrivateProfileString( pIniSection, pIniKey, "" + pValue, getAnwIniDateiName() );
        }
        catch ( Exception err_inst )
        {
            //
        }
    }

    /*
     * ################################################################################
     */
    public static bool readIniBoolean( String pIniSection, String pIniKey, bool pDefaultValue )
    {
        try
        {
            var str_ini = new StringBuilder( capacity );

            int anzahl = GetPrivateProfileString( pIniSection, pIniKey, "" + pDefaultValue, str_ini, str_ini.Capacity, getAnwIniDateiName() );

            return fkString.getBoolean( str_ini.ToString(), pDefaultValue );
        }
        catch ( Exception err_inst )
        {
            //
        }

        return pDefaultValue;
    }

    /*
     * ################################################################################
     */
    public static int reatIniBildTop()
    {
        return reatIniInt( "OPTIONEN", "BILD_POSITION_TOP", -1 );
    }

    /*
     * ################################################################################
     */
    public static int reatIniBildLeft()
    {
        return reatIniInt( "OPTIONEN", "BILD_POSITION_LEFT", -1 );
    }

    /*
     * ################################################################################
     */
    public static String getAnwIniDateiName()
    {
        String app_path = getAppPath();

        if ( fkString.right( app_path, 1 ) == "\\" )
        {
            return app_path + "vbFragenFrager.ini";
        }
        else
        {
            return app_path + "\\vbFragenFrager.ini";
        }
    }

    /*
     * ################################################################################
     */
    public static String getAppPath()
    {
        /*
         * https://stackoverflow.com/questions/837488/how-can-i-get-the-applications-path-in-a-net-console-application
         */

        // to get the location the assembly is executing from 
        //(not necessarily where the it normally resides on disk)
        // in the case of the using shadow copies, for instance in NUnit tests, 
        // this will be in a temp directory.
        string path_location = System.Reflection.Assembly.GetExecutingAssembly().Location;

        //To get the location the assembly normally resides on disk or the install directory
        string path_code_base = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

        //once you have the path you get the directory with:
        var path_app = System.IO.Path.GetDirectoryName( path_location );

        return path_app;
    }
}