using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

// https://stackoverflow.com/questions/9088227/using-getopenfilename-instead-of-openfiledialog

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;

    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;

    public String file = null;
    public int maxFile = 0;

    public String fileTitle = null;
    public int maxFileTitle = 0;

    public String initialDir = null;

    public String title = null;

    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;

    public String defExt = null;

    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;

    public String templateName = null;

    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}

public class fkCommonDialog
{
    [DllImport("Comdlg32.dll", CharSet = CharSet.Auto)]
    private static extern bool GetOpenFileName([In, Out] OpenFileName inst_open_filename);

    [DllImport("Comdlg32.dll", CharSet = CharSet.Auto)]
    private static extern bool GetSaveFileName([In, Out] OpenFileName inst_open_filename);

    /*
     * ################################################################################
     */
    public static String getOpenName( String pDateiFilterAuswaehlbar, String pDateiFilterStart, String pAnfangsVerzeichnis, String pAnfangsDateiName, String pDialogTitel )
    {
        OpenFileName inst_open_filename = new OpenFileName();

        inst_open_filename.structSize = Marshal.SizeOf( inst_open_filename );

        inst_open_filename.filter = pDateiFilterAuswaehlbar;

        inst_open_filename.file = new String( new char[256] );
        inst_open_filename.maxFile = inst_open_filename.file.Length;

        inst_open_filename.fileTitle = new String( new char[64] );
        inst_open_filename.maxFileTitle = inst_open_filename.fileTitle.Length;

        inst_open_filename.initialDir = pAnfangsVerzeichnis;
        inst_open_filename.title = pDialogTitel;
        inst_open_filename.defExt = pDateiFilterStart;

        String datei_name_gewaehlt = null;

        if (fkCommonDialog.GetOpenFileName(inst_open_filename))
        {
            datei_name_gewaehlt = inst_open_filename.file;
        }

        return datei_name_gewaehlt;
    }

    /*
     * ################################################################################
     */
    public static String getSaveName( String pDateiFilterAuswaehlbar, String pDateiFilterStart, String pAnfangsVerzeichnis, String pAnfangsDateiName, String pDialogTitel )
    {
        OpenFileName inst_open_filename = new OpenFileName();

        inst_open_filename.structSize = Marshal.SizeOf( inst_open_filename );

        inst_open_filename.filter = pDateiFilterAuswaehlbar;

        inst_open_filename.file = new String( new char[256] );
        inst_open_filename.maxFile = inst_open_filename.file.Length;

        inst_open_filename.fileTitle = new String( new char[64] );
        inst_open_filename.maxFileTitle = inst_open_filename.fileTitle.Length;

        inst_open_filename.initialDir = pAnfangsVerzeichnis;
        inst_open_filename.title = pDialogTitel;
        inst_open_filename.defExt = pDateiFilterStart;

        String datei_name_gewaehlt = null;

        if ( fkCommonDialog.GetSaveFileName( inst_open_filename ) )
        {
            datei_name_gewaehlt = inst_open_filename.file;

            //Console.WriteLine( "Selected file with full path: {0}", inst_open_filename.file);
            //Console.WriteLine( "Selected file name: {0}", inst_open_filename.fileTitle);
            //Console.WriteLine( "Offset from file name: {0}", inst_open_filename.fileOffset);
            //Console.WriteLine( "Offset from file extension: {0}", inst_open_filename.fileExtension);
        }

        return datei_name_gewaehlt;
    }
}