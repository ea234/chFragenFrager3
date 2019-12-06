
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

public class fkDocumentOpen
{
    [DllImport("shell32.dll", EntryPoint = "ShellExecute")]
    public static extern long ShellExecute(int hwnd, string cmd, string file, string param1, string param2, int swmode);

    [DllImport("kernel32", CharSet = CharSet.Auto)]
    public static extern Int32 GetSystemDirectory(String Buffer, Int32 BufferLength);

    private static int SE_ERR_NOASSOC = 31;
    private static int SE_ERR_NOTFOUND = 2;

    public static long documentOpen( String pDateiName )
    {
        String system_directory = "";
        long return_wert = SE_ERR_NOTFOUND;
        int hwnd_desktop_window = 0;
        int focus_konstante = 0;

        if ( pDateiName.Trim() != null )
        { 
            focus_konstante = 1;

            hwnd_desktop_window = 0;

            // GetDesktopWindow();
            //' public static extern long ShellExecute(int hwnd, string cmd, string file, string param1, string param2, int swmode);
            //https://dotnet-snippets.de/snippet/shellexecute/1000

            return_wert = ShellExecute( hwnd_desktop_window, "open", pDateiName, "", "", focus_konstante );

            if ( return_wert == SE_ERR_NOASSOC )
            {
                system_directory = new String( ' ', 260 );// Space( 260 );

                return_wert = GetSystemDirectory( system_directory, 260 );

                system_directory = system_directory.Trim();

                return_wert = ShellExecute( hwnd_desktop_window, "", "RUNDLL32.EXE", "shell32.dll,OpenAs_RunDLL " + pDateiName, system_directory, focus_konstante );
            }
        }

        return return_wert;
    }
}

