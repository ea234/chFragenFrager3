using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class frmMainFragenFrager : Form
    {
        /*
         * Ideenspeicher
         *
         * - Antwortreihenfolge beim Export umstellen
         *   Nur beim XML Laden und Speichern werden die Funktionen
         *   fuer den direkten Zugriff auf die Antworten genommen. 
         *
         *   Alle anderen Funktionen greifen nur mit den Positionsfunktionen
         *   auf die Antworten zu. Damit kann dann auch beim Export die 
         *   Antwortreihenfolge umgestellt werden
         *
         *
         * - Anzahl der falschen Antworten beschraenken
         *   Wenn immer 3 Antworten falsch sind, einfach 2 auslassen
         *
         *   In der Frage muss es eine entsprechende Funktion geben, 
         *   welcher zu jeder Antwort ein "aktiv"-Kennzeichen speichert.
         */

        /*
         * Checkbox-Konstanten fuer die Auswahlstati
         */
        private bool STATUS_CHECKED   = true;
        private bool STATUS_UNCHECKED = false;

        /*
         * Resize-Ausdehnungswerte
         * Die Resize-Funktion wird erst ab bestimmten Pixel-Werten aktiv. 
         * Diese werden mit diesen beiden Konstanten festgelegt.
         */
        private int FORM_HEIGHT_RESIZE = 500;
        private int FORM_WIDTH_RESIZE  = 800;

        /*
         * Nicht verstandene Resize-Abhaengigkeiten, werden durch einen 
         * festen Pixel-Wert von 30 ausgeglichen.
         */
        private const int ABSTAND_HEIGHT_X  = 30;

        /*
         * Farbkonstanten 
         */
        private Color FARBE_AKTUELL              = Color.FromArgb( 224, 224, 224 );
        private Color FARBE_GRAU                 = Color.FromArgb( 212, 208, 200 );
        private Color FARBE_GRAU_FRAME_FRAGEN    = Color.FromArgb( 233, 233, 233 );       
        private Color FARBE_TEXT_ANTWORT_ENABLED = Color.FromArgb(   0,   0,   0 );
        private Color FARBE_GRUEN                = Color.Green;
        private Color FARBE_ROT                  = Color.Red;
        private Color FARBE_TEXT                 = Color.Black;

        /*
         * Indexkonstanten fuer die Antworten
         */
        private const int INDEX_ANTWORT_1 = 1;
        private const int INDEX_ANTWORT_2 = 2;
        private const int INDEX_ANTWORT_3 = 3;
        private const int INDEX_ANTWORT_4 = 4;
        private const int INDEX_ANTWORT_5 = 5;
        private const int INDEX_ANTWORT_6 = 6;
        private const int INDEX_ANTWORT_7 = 7;
        private const int INDEX_ANTWORT_8 = 8;
        
        /*
         * Konstanten fuer die Navigation zwischen den Fragen
         */
        private const int ANZEIGE_ERSTE_FRAGE     = 0; // Ruecksprung auf die erste Frage aus dem Fragenkatalog oder der aktuellem Lernmenge
        private const int ANZEIGE_NAECHSTE_FRAGE  = 1;
        private const int ANZEIGE_VORHERIGE_FRAGE = 2;
        private const int ANZEIGE_LETZTE_FRAGE    = 3;
        private const int ANZEIGE_AKTUELLE_FRAGE  = 4; // Aktuelle Frage soll neu angezeigt werden, dieses kann bei einem Wechsel von Optionen der Fall sein
        private const int ANZEIGE_GEHE_ZU_FRAGE_X = 6; // gehe zur der angegebenen Frage, Index wird separat angegeben.

        /*
         * Kennzeichenfeld, ob die Antwortbezeichnung ausgegeben werden soll.
         */
        private bool m_knz_antwort_bezeichnung_anzeigen = true;

        /*
         * Kennzeichenfeld, ob die Antwortfelder in der Farbe bei einem 
         * Mouse-Move-Ereignis in der Farbe geaendert werden sollen.
         * 
         * Kennzeichen ist TRUE, wenn die Frage im geloestem Zustand
         * angezeigt wird und ein Markieren der Antworten verhindert
         * werden soll.
         */
        private bool m_ui_knz_farbmarkierung_antworten = true;

        /*
         * Kennzeichenfeld, ob bei einem Click auf die Antwort
         * die Checkbox gewaehlt werden soll.
         */
        private bool m_knz_antwort_set_checkbox_enabled = true;

        /*
         * Kennzeichenfeld, ob die Antwortreihenfolge umgestellt werden soll
         */
        private bool m_knz_antwort_reihenfolge_umstellen = true;

        /*
         * Kennzeichenfeld, ob der Button "OK" die Funktion 
         * von "Weiter zur naechsten Antwort" hat.
         */
        private bool m_knz_btn_ok_ist_naechste_frage = true;

        /*
         * Kennzeichenfeld, ob die Fragennummer ausgegeben werden soll.
         */
        private bool m_knz_fragen_nummer_anzeigen = true;

        /*
         * Kennzeichenfeld, ob der Fragentext angezeigt werden soll.
         */
        private bool m_knz_fragen_text_anzeigen = true;

        /*
         * Kennzeichenfeld, ob die bei einer korrekten Antwort, der 
         * Antworttext ausgegeben werden soll.
         * 
         * (Wegblendung von korrekten Antworten)
         */
        private bool m_knz_antwort_text_korrekt_anzeigen = true;

        /*
         * Kennzeichenfeld, ob die bei einer falschen Antwort, der 
         * Antworttext ausgegeben werden soll.
         * 
         * (Wegblendung von falschen Antworten)
         */
        private bool m_knz_antwort_text_falsch_anzeigen = true;
        private bool m_knz_korrekt_beantwortet_weiter    = true;
        private bool m_knz_korrekte_antwort_anzeigen     = true;
        private bool m_knz_rechts_click_ist_weiter       = true;
        private bool m_ui_knz_50_50_joker_durchgefuehrt  = true;
        private bool m_ui_knz_50_50_joker_moeglich       = true;
        private bool m_ui_knz_50_50_joker_immer_moeglich = true; // bei FALSE ist der Joker nur einmal moeglich 
        private bool m_ui_knz_loesen_moeglich            = true;
        
        private bool m_ui_resize_laeuft                  = false;

        private int m_anzahl_angezeigt                   = 0;
        private int m_anzahl_falsch_beantwortet          = 0;
        private int m_anzahl_fragen_beantwortet          = 0;
        private int m_anzahl_fragen_nicht_beantwortet    = 0;
        private int m_anzahl_korrekt_beantwortet         = 0;
        private int m_ui_abstand_fragen                  = 4;

        private double m_anzahl_prozent_korrekt          = 0.0;

        /*
         * Speichert die aktuell angezeigte Frage
         */
        private clsFrage m_aktuelle_frage = null;

        /*
         * Instanz fuer die FragenFrager-Anwendung
         * - speichert den Fragenkatalog
         * - speichert die aktuelle Fragensitzung
         */
        private clsAnwFragenFrager m_anw_fragen_frager = null;

        /*
         * Name des aktuell angezeigten Bildes
         */
        private String m_datei_name_aktuelles_bild = null;

        /** 
         * Liefert die Instanz der Fragen-Anwendung zurueck.
         */
        private clsAnwFragenFrager getAnwFragenFrager()
        {
            if ( m_anw_fragen_frager == null  ) 
            {
                m_anw_fragen_frager = new clsAnwFragenFrager();

                m_anw_fragen_frager.initAnwFragenFrager();

                m_anw_fragen_frager.setModusFragenKatalogAnzeigen();
            }

            return m_anw_fragen_frager;
        }

        /* 
         * ################################################################################
         */
        public frmMainFragenFrager()
        {
            InitializeComponent();
        }

        /* 
         * ################################################################################
         */
        private void frmMainFragenFrager_Load( object sender, EventArgs e )
        {
            m_frameStatusZeile.Left = 0;

            this.BackColor = FARBE_GRAU;

            m_frameFragen.BackColor = FARBE_GRAU_FRAME_FRAGEN;

            m_frameStatusZeile.BackColor = FARBE_GRAU;

            m_lblFrage1.BackColor = FARBE_GRAU;
            m_lblFrage2.BackColor = FARBE_GRAU;

            m_lblFrage2a.BackColor = FARBE_GRAU;
            m_lblFrageNr.BackColor = FARBE_GRAU;

            m_frameFragen.Left = 0;
            m_frameFragen.Top = m_menuBar.Top + m_menuBar.Height + 2;

            m_optAntwort1.Left = 2;

            m_lblFrage2.Left = m_lblFrage1.Left;
            m_lblFrage2a.Left = m_optAntwort1.Left;
            m_lblFrageNr.Left = m_optAntwort1.Left;

            m_optAntwort2.Left = m_optAntwort1.Left;
            m_optAntwort3.Left = m_optAntwort1.Left;
            m_optAntwort4.Left = m_optAntwort1.Left;
            m_optAntwort5.Left = m_optAntwort1.Left;
            m_optAntwort6.Left = m_optAntwort1.Left;
            m_optAntwort7.Left = m_optAntwort1.Left;
            m_optAntwort8.Left = m_optAntwort1.Left;

            m_optAntwort1.Width = m_lblAntwort1.Left - m_optAntwort1.Left;
            m_optAntwort2.Width = m_optAntwort1.Width;
            m_optAntwort3.Width = m_optAntwort1.Width;
            m_optAntwort4.Width = m_optAntwort1.Width;
            m_optAntwort5.Width = m_optAntwort1.Width;
            m_optAntwort6.Width = m_optAntwort1.Width;
            m_optAntwort7.Width = m_optAntwort1.Width;
            m_optAntwort8.Width = m_optAntwort1.Width;

            m_lblAntwort2.Left = m_lblAntwort1.Left;
            m_lblAntwort3.Left = m_lblAntwort1.Left;
            m_lblAntwort4.Left = m_lblAntwort1.Left;
            m_lblAntwort5.Left = m_lblAntwort1.Left;
            m_lblAntwort6.Left = m_lblAntwort1.Left;
            m_lblAntwort7.Left = m_lblAntwort1.Left;
            m_lblAntwort8.Left = m_lblAntwort1.Left;

            anwReadKennzeichenAusIni();

            m_mnuOptionenKnzFragenNrAnzeigen.Checked = m_knz_fragen_nummer_anzeigen;

            m_mnuOptionenKnzFragenTextAnzeigen.Checked = m_knz_fragen_text_anzeigen;

            m_mnuOptionenKnzAntwortTextKorrektAnzeigen.Checked = m_knz_antwort_text_korrekt_anzeigen;

            m_mnuOptionenKnzAntwortTextFalschAnzeigen.Checked = m_knz_antwort_text_falsch_anzeigen;

            m_mnuOptionenKnzKorrekteAntwAnzeigen.Checked = m_knz_korrekte_antwort_anzeigen;

            m_mnuOptionenKnzRechteMaustasteIstOk.Checked = m_knz_rechts_click_ist_weiter;

            m_mnuOptionenKnzAntwortbezeichnungAnzeigen.Checked = m_knz_antwort_bezeichnung_anzeigen;

            m_mnuOptionenKnzAntwortreihenfolgeUmstellen.Checked = m_knz_antwort_reihenfolge_umstellen;

            m_mnuOptionenKnz50JokerImmer.Checked = m_ui_knz_50_50_joker_immer_moeglich;

            m_ui_knz_50_50_joker_moeglich = m_ui_knz_50_50_joker_immer_moeglich;

            m_btn5050Joker.Enabled = m_ui_knz_50_50_joker_moeglich;

            m_lblFrageNr.Width = m_lblFrage1.Left;

            m_lblFrage2a.Text = "";

            m_lblGesamteFragen.Text = " --- ";

            Control control = (Control) sender;

            int me_height = control.Size.Height;

            int me_width = control.Size.Width;

            anwResizeUi( me_height, me_width );

            m_ui_resize_laeuft = false;

            anwSetKnzAntwortBezeichnungAnzeigen( m_knz_antwort_bezeichnung_anzeigen );

            anwSetAntwortenToGrau();

            anwSchriftAusIni();

            anwFrageAnzeigen( null );
        }

        /* 
         * ################################################################################
         */
        private void frmMainFragenFrager_Resize( object sender, EventArgs e )
        {
            Control control = (Control) sender;

            int me_height = control.Size.Height;

            int me_width = control.Size.Width;

            anwResizeUi( me_height, me_width ) ;
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort1_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_1 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort2_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_2 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort3_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_3 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort4_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_4 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort5_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_5 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort6_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_6 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort7_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_7 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort8_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_8 );
        }

        /* 
         * ################################################################################
         */
        private void m_optAntwort1_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_1 );
        }

        /* 
         * ################################################################################
         */
        private void m_optAntwort2_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_2 );
        }

        /* 
         * ################################################################################
         */
        private void m_optAntwort3_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_3 );
        }

        /* 
         * ################################################################################
         */
        private void m_optAntwort4_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_4 );
        }

        /* 
         * ################################################################################
         */
        private void m_optAntwort5_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_5 );
        }

        /* 
         * ################################################################################
         */
        private void m_optAntwort6_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_6 );
        }

        /* 
         * ################################################################################
         */
        private void m_optAntwort7_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_7 );
        }

        /* 
         * ################################################################################
         */
        private void m_optAntwort8_MouseMove( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwUiMarkiereAntwortMouseMove( INDEX_ANTWORT_8 );
        }

        /*
         * ################################################################################
         */
        private void m_btnTestLaden_Click( object sender, EventArgs e )
        {
            anwLadeLetztenFragenkatalog();
        }

        /* 
         * ################################################################################
         */
        private void m_btnMoveNext_Click( object sender, EventArgs e )
        {
            anwNaechsteFrage( ANZEIGE_NAECHSTE_FRAGE );
        }

        /* 
         * ################################################################################
         */
        private void m_btnMoveLast_Click( object sender, EventArgs e )
        {
            anwNaechsteFrage( ANZEIGE_LETZTE_FRAGE );
        }

        /* 
         * ################################################################################
         */
        private void m_btnMovePrev_Click (object sender, EventArgs e )
        {
            anwNaechsteFrage( ANZEIGE_VORHERIGE_FRAGE );
        }

        /* 
         * ################################################################################
         */
        private void m_btnMoveFirst_Click( object sender, EventArgs e )
        {
            anwNaechsteFrage( ANZEIGE_ERSTE_FRAGE );
        }
 
        /* 
         * ################################################################################
         */
        private void m_lblAntwort1_MouseDown( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwAntwortMouseDown( pMouseEventArgs, INDEX_ANTWORT_1 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort2_MouseDown( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwAntwortMouseDown( pMouseEventArgs, INDEX_ANTWORT_2 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort3_MouseDown( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwAntwortMouseDown( pMouseEventArgs, INDEX_ANTWORT_3 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort4_MouseDown( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwAntwortMouseDown( pMouseEventArgs, INDEX_ANTWORT_4 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort5_MouseDown( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwAntwortMouseDown( pMouseEventArgs, INDEX_ANTWORT_5 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort6_MouseDown( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwAntwortMouseDown( pMouseEventArgs, INDEX_ANTWORT_6 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort7_MouseDown( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwAntwortMouseDown( pMouseEventArgs, INDEX_ANTWORT_7 );
        }

        /* 
         * ################################################################################
         */
        private void m_lblAntwort8_MouseDown( object sender, MouseEventArgs pMouseEventArgs )
        {
            anwAntwortMouseDown( pMouseEventArgs, INDEX_ANTWORT_8 );
        }

        /*
         * ################################################################################
         */
        private void anwAntwortMouseDown( MouseEventArgs pMouseEventArgs, int pAntwort )
        {
            /*
             * Pruefung: Rechtsclick gleich weiter ?
             * 
             * Es wird zurerst das Kennzeichen geprueft, ob diese Funktion eingeschaltet ist.
             * 
             * Ist die Funktion eingeschaltet, wird am Mouse-Event nachgesehen, ob die rechte
             * Maustaste gedrueckt worden ist. Ist das so, wird die naechste Frage angezeigt.
             * 
             */
            if ( m_knz_rechts_click_ist_weiter )
            {
                if ( pMouseEventArgs.Button == System.Windows.Forms.MouseButtons.Right )
                {
                    anwNaechsteFrage( ANZEIGE_NAECHSTE_FRAGE );

                    return;
                }
            }

            /*
             * Pruefung: Click bedeutet "weiter" ?
             * 
             * Wurde die Frage vom Anwender per "Loesen"-Button aufgeloest, wurde 
             * die Variable "m_knz_btn_ok_ist_naechste_frage" auf true gestellt.
             * 
             * In diesem Zustand soll ein Click auf eine Antwort nicht die 
             * Antwort markieren, sondern es soll aus der reinen Anzeige der 
             * Frage weiter zur naechsten Frage verzweigt werden.
             */
            if ( m_knz_btn_ok_ist_naechste_frage )
            {
                anwNaechsteFrage( ANZEIGE_NAECHSTE_FRAGE );

                return;
            }

            /*
             * Pruefung: Soll ein Click die Antwort auswaehlen ?
             * 
             * Steht die Variable "m_knz_antwort_set_checkbox_enabled" auf TRUE, wird
             * bei einem Click auf die Antwort selbige ausgewaehlt.
             * 
             * Eine Markierung der Antwort wird nicht gemacht, wenn die 
             * Frage durch den Anwender "geloest" worden ist, und somit 
             * die Check-Boxen inaktiv sind.
             */
            if ( m_knz_antwort_set_checkbox_enabled )
            {
                anwCheckAntwortX( pAntwort );
            }
        }

        /*
         * ################################################################################
         */
        private void anwLadeLetztenFragenkatalog()
        {
            /*
             * Aus der INI-Datei wird der letzte Dateiname gelesen.
             * 
             * Das ist der INI-Key "DATEI_NAME_XML_FRAGENKATALOG".
             */
            String xml_datei_name = fkIniDatei.readIniDateiName( "DATEI_NAME_XML_FRAGENKATALOG" );

            /*
             * Ist kein letzter Fragenkatalog vorhanden, wird hier 
             * in diesem Fall ein fest hinterlegter Dateipfad genommen.
             * 
             * (Es sollte normalerweise eine Fehlermeldung kommen)
             */
            if (xml_datei_name == null)
            {
                xml_datei_name = @"C:\Daten\open_ppl\navigation\navigation.xml";
            }

            /*
             * Der Statustext wird auf "Laden" gestellt
             */
            m_lblStatusAnzeige1.Text = "Lade: " + xml_datei_name;

            Application.DoEvents();

            /*
             * Die Datei wird geladen
             */
            getAnwFragenFrager().ladeXmlFragenKatalog( xml_datei_name, true );

            /*
             * Es wird zur ersten Frage verzweigt.
             */
            anwNaechsteFrage( ANZEIGE_ERSTE_FRAGE );

            /*
             * Der Label fuer die Anzahl der Fragen wird neu gesetzt.
             */
            m_lblGesamteFragen.Text = getAnwFragenFrager().getAnzahlLabel();

            /*
             * Der Status-Text wird geloescht.
             */
            m_lblStatusAnzeige1.Text = "";
        }

        /**
         * Ausdehnungsanpassungen der UI-Form
         */
        private void anwResizeUi( int pFormHeight, int pFormWidth )
        {
            int control_heigth      = 0;
            int control_anzahl      = 0;
            int control_top_aktuell = 0;

            int me_height = ( pFormHeight - ABSTAND_HEIGHT_X ) - m_menuBar.Height;

            /*
             * Pruefung: Laeuft der Resize schon ?
             * Es werden keine Ausdehnungsberechnungen gemacht, wenn die Resize
             * Funktion noch von einem vorhergehenden Auruf laeuft.
             */ 
            if ( m_ui_resize_laeuft == false )
            {
                /*
                 * Kennzeichen "Resize laeuft" auf TRUE stellen
                 */
                m_ui_resize_laeuft = true;

                /*
                 * Pruefung: Formhoehe groesser als die Schwelle fuer ein Resize der Controls ?
                 * 
                 * Die Resize-Funktion soll erst ab einer gewissen Hoehe aktiv werden.
                 */
                if ( me_height > FORM_HEIGHT_RESIZE )
                {
                    /*
                     * Anzahl der vorhandenen Controls ermitteln. 
                     * Es gibt 8 Antwort-Positionen und eine Position fuer 
                     * dén ersten oberen Fragenlabel.
                     */
                    control_anzahl = ( m_optAntwort1.Visible ? 1 : 0 ) + ( m_optAntwort2.Visible ? 1 : 0 ) + 
                                     ( m_optAntwort3.Visible ? 1 : 0 ) + ( m_optAntwort4.Visible ? 1 : 0 ) +
                                     ( m_optAntwort5.Visible ? 1 : 0 ) + ( m_optAntwort6.Visible ? 1 : 0 ) + 
                                     ( m_optAntwort7.Visible ? 1 : 0 ) + ( m_optAntwort8.Visible ? 1 : 0 ) + 1;

                    /*
                     * Top-Position der Statuszeile (Buttonleiste)
                     * 
                     * Die Statuszeile soll am unteren Fenster angezeigt werden. 
                     * Dazu muss dessen Hoehe von der Formhoehe abgezogen werden.
                     * 
                     * Die Formhoehe behinhaltet auch die Hoehe der Menubar.
                     * Auf das Ergebnis wird die Hoehe der Menubar draufaddiert.
                     * Dadurch wird die Statuszeile im Fenster weiter unten angezeigt.
                     */
                    m_frameStatusZeile.Top = ( me_height - m_frameStatusZeile.Height ) + m_menuBar.Height;

                    /*
                     * Hoehe Fragenframe
                     * Die Hoehe des Fragenframes ist die Differenz zwischen der Statuszeile
                     * und dem aktuellem Frame-Fragen-Top.
                     */
                    m_frameFragen.Height = ( m_frameStatusZeile.Top - m_ui_abstand_fragen ) - m_frameFragen.Top;

                    /*
                     * Zwischenwert berechnen 
                     * = Hoehe bis zum Top-Startpunkt des (unteren) zweiten Fragenlabels, zuzueglich 
                     *   dessen Abstand zur vorhergehenden Frage.
                     */
                    control_heigth = m_frameFragen.Height - ( m_lblFrage2.Height + m_ui_abstand_fragen );

                    /*
                     * Hoehe eines Labels
                     * In der Variablen "control_heigth" steht nun die zu verteilende Resthoehe drin.
                     * 
                     * Von dieser Resthoehe werden nochmal die Abstaende der einzelnen Controls abgezogen.
                     * 
                     * Die dann verbleibende Resthoehe wird durch die Anzahl der angezeigten Controls geteilt.
                     */
                    control_heigth = Convert.ToInt32( ( control_heigth - ( ( control_anzahl + 1 ) * m_ui_abstand_fragen ) ) / ( control_anzahl ) );

                    /*
                     * Pruefung: Control-Heigt kleiner 10 ?
                     * 
                     * Es wird eine Mindesthoehe von 10 Pixeln gesetzt, sollte die berechnete 
                     * Hoehe kleiner 10 sein.
                     */ 
                    if ( control_heigth < 10 )
                    {
                        control_heigth = 10;
                    }

                    /*
                     * Oberer Fragenlabel
                     * Der obere Fragenlabel startet bei dem Abstand der Antworten zueinander.
                     * Die Hoehe des Controls wird angepasst.
                     */ 
                    m_lblFrage1.Top     = m_ui_abstand_fragen;
                    m_lblFrage1.Height  = control_heigth;

                    m_lblFrageNr.Top    = m_lblFrage1.Top;
                    m_lblFrageNr.Height = control_heigth;

                    control_top_aktuell = m_lblFrage1.Top + control_heigth + m_ui_abstand_fragen;

                    /*
                     * Resize der Antworten in der Hoehe
                     * Fuer jede Antwortposition wird geprueft, ob diese Antwort aktuell angezeigt wird.
                     * 
                     * Wird die Antwort angezeigt, werden die Top- und Height-Werte angepasst. 
                     * Anschliessend wird der "control_top_aktuell"-Wert neu berechnet.
                     */

                    if ( m_optAntwort1.Visible )
                    {
                        m_optAntwort1.Top    = control_top_aktuell;
                        m_optAntwort1.Height = control_heigth;

                        m_lblAntwort1.Top    = control_top_aktuell;
                        m_lblAntwort1.Height = control_heigth;

                        control_top_aktuell += control_heigth + m_ui_abstand_fragen;
                    }

                    if ( m_optAntwort2.Visible )
                    {
                        m_optAntwort2.Top    = control_top_aktuell;
                        m_optAntwort2.Height = control_heigth;

                        m_lblAntwort2.Top    = control_top_aktuell;
                        m_lblAntwort2.Height = control_heigth;

                        control_top_aktuell += control_heigth + m_ui_abstand_fragen;
                    }

                    if ( m_optAntwort3.Visible )
                    {
                        m_optAntwort3.Top    = control_top_aktuell;
                        m_optAntwort3.Height = control_heigth;

                        m_lblAntwort3.Top    = control_top_aktuell;
                        m_lblAntwort3.Height = control_heigth;

                        control_top_aktuell += control_heigth + m_ui_abstand_fragen;
                    }

                    if ( m_optAntwort4.Visible )
                    {
                        m_optAntwort4.Top    = control_top_aktuell;
                        m_optAntwort4.Height = control_heigth;

                        m_lblAntwort4.Top    = control_top_aktuell;
                        m_lblAntwort4.Height = control_heigth;

                        control_top_aktuell += control_heigth + m_ui_abstand_fragen;
                    }

                    if ( m_optAntwort5.Visible )
                    {
                        m_optAntwort5.Top    = control_top_aktuell;
                        m_optAntwort5.Height = control_heigth;

                        m_lblAntwort5.Top    = control_top_aktuell;
                        m_lblAntwort5.Height = control_heigth;

                        control_top_aktuell += control_heigth + m_ui_abstand_fragen;
                    }

                    if ( m_optAntwort6.Visible )
                    {
                        m_optAntwort6.Top    = control_top_aktuell;
                        m_optAntwort6.Height = control_heigth;

                        m_lblAntwort6.Top    = control_top_aktuell;
                        m_lblAntwort6.Height = control_heigth;

                        control_top_aktuell += control_heigth + m_ui_abstand_fragen;
                    }

                    if ( m_optAntwort7.Visible )
                    {
                        m_optAntwort7.Top    = control_top_aktuell;
                        m_optAntwort7.Height = control_heigth;

                        m_lblAntwort7.Top    = control_top_aktuell;
                        m_lblAntwort7.Height = control_heigth;

                        control_top_aktuell += control_heigth + m_ui_abstand_fragen;
                    }

                    if ( m_optAntwort8.Visible )
                    {
                        m_optAntwort8.Top    = control_top_aktuell;
                        m_optAntwort8.Height = control_heigth;

                        m_lblAntwort8.Top    = control_top_aktuell;
                        m_lblAntwort8.Height = control_heigth;

                        control_top_aktuell += control_heigth + m_ui_abstand_fragen;
                    }

                    /*
                     * Unterer Fragenlabel
                     */ 

                    m_lblFrage2.Top = control_top_aktuell;

                    m_lblFrage2a.Top = m_lblFrage2.Top;
                }

                if ( pFormWidth > FORM_WIDTH_RESIZE )
                {
                    m_frameFragen.Width = pFormWidth;

                    m_lblFrage1.Width = m_frameFragen.Width - m_lblFrage1.Left - m_ui_abstand_fragen;
                    m_lblFrage2.Width = m_lblFrage1.Width;

                    m_lblAntwort1.Width = m_frameFragen.Width - m_lblAntwort1.Left - m_ui_abstand_fragen;
                    m_lblAntwort2.Width = m_lblAntwort1.Width;
                    m_lblAntwort3.Width = m_lblAntwort1.Width;
                    m_lblAntwort4.Width = m_lblAntwort1.Width;
                    m_lblAntwort5.Width = m_lblAntwort1.Width;
                    m_lblAntwort6.Width = m_lblAntwort1.Width;
                    m_lblAntwort7.Width = m_lblAntwort1.Width;
                    m_lblAntwort8.Width = m_lblAntwort1.Width;

                    m_frameStatusZeile.Width = pFormWidth;
                }

                m_ui_resize_laeuft = false;
            }
        }

        /** 
         * Schaltet den Checkbox-Wert bei der UI-Position um.
         * Aus Selektiert wird nicht selektiert und andersrum.
         */
        private void anwCheckAntwortX( int pIndex )
        {
            /*
             * Ist keine aktuelle Frage vorhanden, wird von dieser Funktion keine
             * Aktion gemacht. Die Funktion wird verlassen.
             */
            if ( m_aktuelle_frage == null ) 
            {
                return;
            }

            /*
             * Check-Boxen 
             * Es wird anhand des Wertes aus dem Parameter "pIndex" diejenige Antwort
             * gesucht, bei welcher der Check-Box-Wert geaendert werden soll.
             * 
             * Bei der Antwort wird der Check-Box-Wert per Toggle umgeschaltet.
             */
            if ( pIndex == INDEX_ANTWORT_1 ) { m_optAntwort1.Checked = !m_optAntwort1.Checked; }
            if ( pIndex == INDEX_ANTWORT_2 ) { m_optAntwort2.Checked = !m_optAntwort2.Checked; }
            if ( pIndex == INDEX_ANTWORT_3 ) { m_optAntwort3.Checked = !m_optAntwort3.Checked; }
            if ( pIndex == INDEX_ANTWORT_4 ) { m_optAntwort4.Checked = !m_optAntwort4.Checked; }
            if ( pIndex == INDEX_ANTWORT_5 ) { m_optAntwort5.Checked = !m_optAntwort5.Checked; }
            if ( pIndex == INDEX_ANTWORT_6 ) { m_optAntwort6.Checked = !m_optAntwort6.Checked; }
            if ( pIndex == INDEX_ANTWORT_7 ) { m_optAntwort7.Checked = !m_optAntwort7.Checked; }
            if ( pIndex == INDEX_ANTWORT_8 ) { m_optAntwort8.Checked = !m_optAntwort8.Checked; }

            if ( m_aktuelle_frage.getUiAnzahlKorrekteAntworten() == 1 )
            {
                /*
                 * Hat die Frage nur eine korrekte Antwort, werden bei allen anderen  
                 * Antworten die Check-Boxen auf "nicht ausgewaehlt" gesetzt.
                 */
                if ( pIndex != INDEX_ANTWORT_1 ) { m_optAntwort1.Checked = STATUS_UNCHECKED; }
                if ( pIndex != INDEX_ANTWORT_2 ) { m_optAntwort2.Checked = STATUS_UNCHECKED; }
                if ( pIndex != INDEX_ANTWORT_3 ) { m_optAntwort3.Checked = STATUS_UNCHECKED; }
                if ( pIndex != INDEX_ANTWORT_4 ) { m_optAntwort4.Checked = STATUS_UNCHECKED; }
                if ( pIndex != INDEX_ANTWORT_5 ) { m_optAntwort5.Checked = STATUS_UNCHECKED; }
                if ( pIndex != INDEX_ANTWORT_6 ) { m_optAntwort6.Checked = STATUS_UNCHECKED; }
                if ( pIndex != INDEX_ANTWORT_7 ) { m_optAntwort7.Checked = STATUS_UNCHECKED; }
                if ( pIndex != INDEX_ANTWORT_8 ) { m_optAntwort8.Checked = STATUS_UNCHECKED; }
            }
        }

        /* 
         * ################################################################################
         */
        private void m_mnuAbfragemodusReihenfolge_Click( object sender, EventArgs e )
        {
            anwStartAbfrageModus( false, -1, -1, -1 );
        }

        /* 
         * ################################################################################
         */
        private void anwStartAbfrageModus( bool pKnzModusZufaellig, int pAnzahlFragen, int pUntergrenze, int pObergrenze )
        {
            /* 
             * Aufruf der Funktion fuer die Erstellung einer Abfragesitzung.
             * 
             * Der Funktion werden die Parameter fuer die Fragesitzung uebergeben.
             * 
             * Konnte die Fragensitzung erstellt werden, liefert die Funktion TRUE zurueck.
             * Es werden die Felder fuer die Abfragesitzung auf 0 gestellt und 
             * die erste Frage aus der Fragensitzung angezeigt.
             */
            if ( getAnwFragenFrager().startAbfrageSitzung( pKnzModusZufaellig, pAnzahlFragen, pUntergrenze, pObergrenze ) ) 
            {
                m_anzahl_angezeigt = 0;
                m_anzahl_fragen_beantwortet = 0;
                m_anzahl_fragen_nicht_beantwortet = 0;
                m_anzahl_falsch_beantwortet = 0;
                m_anzahl_korrekt_beantwortet = 0;
                m_anzahl_prozent_korrekt = 0;

                m_lblWerttProzentKorrekt.Text = "0";
                m_lblGesamteFragen.Text = getAnwFragenFrager().getAnzahlLabel();

                anwNaechsteFrage( ANZEIGE_ERSTE_FRAGE );
            }
        }

        /* 
         * ################################################################################
         */
        private void anwStoppAbfrageModus()
        {
            /* 
             * Stopp der aktuellen Fragesitzung und Rueckkehr in den 
             * Fragenanzeigemodus.
             */
            if ( getAnwFragenFrager().startAnzeigeFragenKatalog() )
            {
                m_lblGesamteFragen.Text = getAnwFragenFrager().getAnzahlLabel();

                anwNeueFrageAnzeigen( ANZEIGE_ERSTE_FRAGE );
            }
        }

        /* 
         * ################################################################################
         */
        private void m_mnuAbfragemodusZufall40_Click( object sender, EventArgs e )
        {
            /* 
             * Start einer Fragensitzung mit 40 zufaelligen Fragen aus dem gesamten Fragenkatalog.
             */
            anwStartAbfrageModus( true, 40, -1, -1 );
        }

        /* 
         * ################################################################################
         */
        private void m_mnuDateiEnde_Click( object sender, EventArgs e )
        {
            /*
             * Aufruf der Funktion "anwEnde" um alle Resourcen freizugeben
             */
            anwEnde();

            /*
             * Aufruf der "close" Funktion um das Fenster zu schliessen. 
             */
            Close();
        }

        /* 
         * ################################################################################
         */
        private void zufall80ToolStripMenuItem_Click( object sender, EventArgs e )
        {
            /* 
             * Start einer Fragensitzung mit 60 zufaelligen Fragen aus dem gesamten Fragenkatalog.
             */
            anwStartAbfrageModus( true, 60, -1, -1 );
        }

        /* 
         * ################################################################################
         */
        private void m_mnuAbfragemodusZufall80_Click( object sender, EventArgs e )
        {
            /* 
             * Start einer Fragensitzung mit 80 zufaelligen Fragen aus dem gesamten Fragenkatalog.
             */
            anwStartAbfrageModus( true, 80, -1, -1 );
        }

        /* 
         * ################################################################################
         */
        private void m_frameFragen_MouseDown( object sender, MouseEventArgs pMouseEventArgs )
        {
            if ( m_knz_rechts_click_ist_weiter )
            {
                if ( pMouseEventArgs.Button == System.Windows.Forms.MouseButtons.Right )
                {
                    anwNaechsteFrage( ANZEIGE_NAECHSTE_FRAGE );

                    return;
                }
            }
        }

        /* 
         * ################################################################################
         */
        private void m_lblFrage1_MouseDown( object sender, MouseEventArgs pMouseEventArgs )
        {
            if ( m_knz_rechts_click_ist_weiter )
            {
                if ( pMouseEventArgs.Button == System.Windows.Forms.MouseButtons.Right )
                {
                    anwNaechsteFrage( ANZEIGE_NAECHSTE_FRAGE );

                    return;
                }
            }
        }

        /* 
         * ################################################################################
         */
        private void m_lblFrageNr_MouseDown( object sender, MouseEventArgs pMouseEventArgs )
        {
            if ( m_knz_rechts_click_ist_weiter )
            {
                if ( pMouseEventArgs.Button == System.Windows.Forms.MouseButtons.Right )
                {
                    anwNaechsteFrage( ANZEIGE_NAECHSTE_FRAGE );

                    return;
                }
            }
        }

        /* 
         * ################################################################################
         */
        private void m_lblFrage2_MouseDown( object sender, MouseEventArgs pMouseEventArgs )
        {
            if ( m_knz_rechts_click_ist_weiter )
            {
                if ( pMouseEventArgs.Button == System.Windows.Forms.MouseButtons.Right )
                {
                    anwNaechsteFrage( ANZEIGE_NAECHSTE_FRAGE );

                    return;
                }
            }
        }

        /* 
         * ################################################################################
         */
        private void m_mnuOptionenKnzFragenNrAnzeigen_Click( object sender, EventArgs e )
        {
            m_knz_fragen_nummer_anzeigen = !m_knz_fragen_nummer_anzeigen;

            m_mnuOptionenKnzFragenNrAnzeigen.Checked = m_knz_fragen_nummer_anzeigen;

            anwFrageAnzeigen( m_aktuelle_frage );

            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_FRAGEN_NUMMER_ANZEIGEN", m_knz_fragen_nummer_anzeigen );
        }

        /* 
         * ################################################################################
         */
        private void m_mnuOptionenKnzKorrekteAntwAnzeigen_Click( object sender, EventArgs e )
        {
            m_knz_korrekte_antwort_anzeigen = !m_knz_korrekte_antwort_anzeigen;

            m_mnuOptionenKnzKorrekteAntwAnzeigen.Checked = m_knz_korrekte_antwort_anzeigen;

            anwNeueFrageAnzeigen( ANZEIGE_AKTUELLE_FRAGE );

            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_KORREKTE_ANTWORT_ANZEIGEN", m_knz_korrekte_antwort_anzeigen );
        }

        /* 
         * ################################################################################
         */
        private void m_mnuOptionenKnzRechteMaustasteIstOk_Click( object sender, EventArgs e )
        {
            m_knz_rechts_click_ist_weiter = !m_knz_rechts_click_ist_weiter;

            m_mnuOptionenKnzRechteMaustasteIstOk.Checked = m_knz_rechts_click_ist_weiter;

            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_RECHTS_CLICK_IST_WEITER", m_knz_rechts_click_ist_weiter );
        }

        /* 
         * ################################################################################
         */
        private void m_mnuOptionenKnzAntwortbezeichnungAnzeigen_Click( object sender, EventArgs e )
        {
            m_knz_antwort_bezeichnung_anzeigen = !m_knz_antwort_bezeichnung_anzeigen;

            m_mnuOptionenKnzAntwortbezeichnungAnzeigen.Checked = m_knz_antwort_bezeichnung_anzeigen;

            anwSetKnzAntwortBezeichnungAnzeigen( m_knz_antwort_bezeichnung_anzeigen );

            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_ANTWORT_BEZEICHNUNG_ANZEIGEN", m_knz_antwort_bezeichnung_anzeigen );
        }

        /* 
         * ################################################################################
         */
        private void toolStripMenuItem5_Click( object sender, EventArgs e )
        {
            m_knz_fragen_text_anzeigen = !m_knz_fragen_text_anzeigen;

            m_mnuOptionenKnzFragenTextAnzeigen.Checked = m_knz_fragen_text_anzeigen;

            anwFrageAnzeigen( m_aktuelle_frage );

            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_FRAGEN_TEXT_ANZEIGEN", m_knz_fragen_text_anzeigen );
        }

        /* 
         * ################################################################################
         */
        private void m_mnuOptionenKnzAntwortTextKorrektAnzeigen_Click( object sender, EventArgs e )
        {
            m_knz_antwort_text_korrekt_anzeigen = !m_knz_antwort_text_korrekt_anzeigen;

            m_mnuOptionenKnzAntwortTextKorrektAnzeigen.Checked = m_knz_antwort_text_korrekt_anzeigen;

            anwFrageAnzeigen( m_aktuelle_frage );

            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_ANTWORT_TEXT_KORREKT_ANZEIGEN", m_knz_antwort_text_korrekt_anzeigen );
        }

        /* 
         * ################################################################################
         */
        private void m_mnuOptionenKnzAntwortTextFalschAnzeigen_Click( object sender, EventArgs e )
        {
            m_knz_antwort_text_falsch_anzeigen = !m_knz_antwort_text_falsch_anzeigen;

            m_mnuOptionenKnzAntwortTextFalschAnzeigen.Checked = m_knz_antwort_text_falsch_anzeigen;

            anwFrageAnzeigen( m_aktuelle_frage );

            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_ANTWORT_TEXT_FALSCH_ANZEIGEN", m_knz_antwort_text_falsch_anzeigen );
        }

        /* 
         * ################################################################################
         */
        private void m_mnuOptionenResetOptionen_Click( object sender, EventArgs e )
        {
            /*
             * Kennzeichen auf die normalen Werte stellen
             */
            m_knz_antwort_bezeichnung_anzeigen = true;
            m_knz_antwort_set_checkbox_enabled = true;
            m_knz_antwort_reihenfolge_umstellen = true;
            m_knz_btn_ok_ist_naechste_frage = true;
            m_knz_fragen_nummer_anzeigen = true;
            m_knz_fragen_text_anzeigen = true;
            m_knz_antwort_text_korrekt_anzeigen = true;
            m_knz_antwort_text_falsch_anzeigen = true;
            m_knz_korrekt_beantwortet_weiter = true;
            m_knz_korrekte_antwort_anzeigen = true;
            m_knz_rechts_click_ist_weiter = true;
            m_ui_knz_50_50_joker_immer_moeglich = true;

            /*
             * Auswahlen im Menu entsprechend den Kennzeichen setzen
             */
            m_mnuOptionenKnzAntwortbezeichnungAnzeigen.Checked = m_knz_antwort_bezeichnung_anzeigen;
            m_mnuOptionenKnzFragenNrAnzeigen.Checked = m_knz_fragen_nummer_anzeigen;
            m_mnuOptionenKnzKorrekteAntwAnzeigen.Checked = m_knz_korrekte_antwort_anzeigen;
            m_mnuOptionenKnzRechteMaustasteIstOk.Checked = m_knz_rechts_click_ist_weiter;
            m_mnuOptionenKnzAntwortTextKorrektAnzeigen.Checked = m_knz_antwort_text_korrekt_anzeigen;
            m_mnuOptionenKnzAntwortTextFalschAnzeigen.Checked = m_knz_antwort_text_falsch_anzeigen;
            m_mnuOptionenKnzFragenTextAnzeigen.Checked = m_knz_fragen_text_anzeigen;
            m_mnuOptionenKnz50JokerImmer.Checked = m_ui_knz_50_50_joker_immer_moeglich;

            /*
             * 50%-Joker Einstellungen aktualisieren
             */
            m_ui_knz_50_50_joker_moeglich = m_ui_knz_50_50_joker_immer_moeglich;

            m_btn5050Joker.Enabled = m_ui_knz_50_50_joker_moeglich;

            /*
             * Kennzeicheneinstellungen in die Ini-Datei schreiben
             */
            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_50_PROZENT_JOKER_IMMER_MOEGLICH", m_ui_knz_50_50_joker_immer_moeglich );
            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_ANTWORT_BEZEICHNUNG_ANZEIGEN", m_knz_antwort_bezeichnung_anzeigen );
            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_FRAGEN_NUMMER_ANZEIGEN", m_knz_fragen_nummer_anzeigen );
            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_KORREKTE_ANTWORT_ANZEIGEN", m_knz_korrekte_antwort_anzeigen );
            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_RECHTS_CLICK_IST_WEITER", m_knz_rechts_click_ist_weiter );
            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_ANTWORT_TEXT_FALSCH_ANZEIGEN", m_knz_antwort_text_falsch_anzeigen );
            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_FRAGEN_TEXT_ANZEIGEN", m_knz_fragen_text_anzeigen );
            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_ANTWORT_TEXT_KORREKT_ANZEIGEN", m_knz_antwort_text_korrekt_anzeigen );

            anwSetKnzAntwortBezeichnungAnzeigen( m_knz_antwort_bezeichnung_anzeigen );

            /*
             * Aktuelle Frage mit den neuen Kennzeichenwerten anzeigen.
             */
            anwFrageAnzeigen( m_aktuelle_frage );
        }

        /* 
         * ################################################################################
         */
        private void m_mnuDateiXmlLaden_Click( object sender, EventArgs e )
        {
            m_lblGesamteFragen.Text = " --- ";

            /* 
             * Letzter Dateiname
             * Aus der INI-Datei wird der Name der zuletzt geoefnneten XML-Datei geladen.
             * Dieser Name erscheint dann als Vorauswahl in der Dialog-Box.
             */
            String datei_name = fkIniDatei.readIniDateiName( "DATEI_NAME_XML_FRAGENKATALOG" );

            /* 
             * Dateifilter
             * Die zur Auswahl stehenden Datei-Erweiterungen werden als String initialisiert.
             */
            String datei_filter = "TXT-Datei ( *.txt )\0*.txt\0XML-Datei ( *.xml )\0*.xml\0alle Dateien ( *.* )\0*.*\0";

            /* 
             * Aufruf der Dateiauswahl-Dialog-Box
             */
            datei_name = fkCommonDialog.getOpenName( datei_filter, "txt", "c:\\", datei_name, "Fragenkatalog laden" );

            /* 
             * Der Status-Text wird auf "Laden" gesetzt
             */
            m_lblStatusAnzeige1.Text = "Lade: " + datei_name;

            Application.DoEvents();

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                /* 
                 * Pruefung: Wurde eine Datei ausgewaehlt?
                 * Wurde keine Datei ausgewaehlt, ist der Dateiname ein Leerstring.
                 * Wurde eine Datei ausgewaehlt, ist der Dateiname gesetzt.
                 */
                if ( datei_name != null )
                {
                    if ( getAnwFragenFrager().ladeXmlFragenKatalog( datei_name, true ) )
                    {
                        /* 
                         * Konnte die Datei eingelesen werden, wird der Dateiname in der INI-Datei gespeichert
                         */
                        fkIniDatei.writeIniDateiName( "DATEI_NAME_XML_FRAGENKATALOG", datei_name );

                        m_lblGesamteFragen.Text = getAnwFragenFrager().getAnzahlLabel();
                        
                        anwNeueFrageAnzeigen( ANZEIGE_ERSTE_FRAGE );
                    }
                    else
                    {
                        MessageBox.Show( "Die XML-Datei \"" + datei_name + "\" konnte nicht geladen werden!", "Fehler Laden" );
                    }
                }
            }
            catch ( Exception err_inst )
            {
                Console.WriteLine( "Fehler: errXmlDateiLaden\n" + err_inst.Message );// + "" + err_inst.StackTrace );
            }

            Cursor.Current = Cursors.Default;

            m_lblStatusAnzeige1.Text = "";
        }

        /* 
         * ################################################################################
         */
        private void m_mnuDateiTestSaveFileName_Click( object sender, EventArgs e )
        {
            String xml_datei_name = fkCommonDialog.getSaveName( "XML\0*.xml\0Text\0*.txt\0Alle\0*.*\0", "txt", "c:\\", "w", "Speichen test" );
        }

        /* 
         * ################################################################################
         */
        private void m_mnuDateiTestAppPath_Click( object sender, EventArgs e )
        {
            Console.WriteLine( "App.Path {0}", fkIniDatei.getAppPath() );
            Console.WriteLine( "Ini-Datei {0}", fkIniDatei.getAnwIniDateiName() );
            Console.WriteLine( "Ini-Datei {0}", fkIniDatei.readIniDateiName("KeyName") );
        }

        /* 
         * ################################################################################
         */
        private void m_mnuAbfragemodusExportFragensitzungAlle_Click( object sender, EventArgs e )
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                bool knz_exportiere_antworten           = true;
                bool knz_exportiere_korrekte_antworten  = true;
                bool knz_exportiere_falsche_antworten   = true;
                bool knz_exportiere_antwort_bezeichnung = true;

                getAnwFragenFrager().exportFrageBogenLernFabrik( fkExportFrageBogen.EXPORT_LERN_FABRIK_ALLES, knz_exportiere_antworten, knz_exportiere_korrekte_antworten, knz_exportiere_falsche_antworten, knz_exportiere_antwort_bezeichnung, m_knz_antwort_reihenfolge_umstellen );
            }
            catch ( Exception err_inst )
            {
                Console.WriteLine( "Fehler: errExportFragensitzungAlle\n" + err_inst.Message );// + "" + err_inst.StackTrace );
            }

            Cursor.Current = Cursors.Default;
        }

        private chFFBildanzeige m_form_bild = null;

        /*
         * ################################################################################
         */
        private chFFBildanzeige getFormBildanzeige()
        {
            if ( m_form_bild == null )
            {
                m_form_bild = new chFFBildanzeige();
            }

            return m_form_bild;
        }

        /*
         * ################################################################################
         */
        private void anwUiBildAnzeigen( String pDateiName )
        {
            if (m_form_bild == null)
            {
                m_form_bild = new chFFBildanzeige();

                m_form_bild.StartPosition = FormStartPosition.Manual; 
            }


            int bild_top = fkIniDatei.reatIniBildTop();
           
            int bild_left = fkIniDatei.reatIniBildTop();

            System.Console.WriteLine( "bild_top  =>" + bild_top + "<");
            System.Console.WriteLine( "bild_left =>" + bild_left + "<");

            if ( bild_left > -1 )
            {
                m_form_bild.Left = bild_left;
            }
            else
            {
                bild_left = 0;
            }

            if ( bild_top > -1 )
            {
                m_form_bild.Top = bild_top;
            }
            else
            {
                bild_top = 0;
            }

            m_form_bild.Location = new Point( bild_top, bild_left );

            m_datei_name_aktuelles_bild = pDateiName;

            Image img_anzeige = Image.FromFile( getAnwFragenFrager().getAktuellesBildVerzeichnis() + m_datei_name_aktuelles_bild );

            m_form_bild.setImage( img_anzeige );

            m_form_bild.Width = img_anzeige.Width;
            m_form_bild.Height = img_anzeige.Height;

            m_form_bild.Show();

            m_form_bild.Text = "Bildanzeige [ " + pDateiName + " ]";
        }

        /*
         * ################################################################################
         */
        private  void anwUiBildAktuellAnzeigen()
        {
            if (m_datei_name_aktuelles_bild != null)
            {
                anwUiBildAnzeigen( m_datei_name_aktuelles_bild );
            }
        }

        /*
         * ################################################################################
         */
        private void anwUiBildUnload()
        {
            if ( m_form_bild != null )
            {
                fkIniDatei.writeIniBildPosition( m_form_bild.Top, m_form_bild.Left );

                m_form_bild.Hide();
            }

            m_form_bild = null;
        }

        /*
         * ################################################################################
         */
        private void anwShowExportDialog()
        {
            //chFF3DialogExportDetail m_form_c = new chFF3DialogExportDetail();

            //m_form_c.ShowDialog( this );

            //this.Show();
        }

        /*
         * ################################################################################
         */
        private void m_btnTextExportDialog_Click( object sender, EventArgs e )
        {
            anwShowExportDialog();
        }

        /*
         * ################################################################################
         */
        private void schriftauswahlToolStripMenuItem_Click( object sender, EventArgs e )
        {
            anwSchriftAuswahl();
        }

        /*
         * ################################################################################
         */
        private void m_mnuOptionenAktuelleFrageToClipboard_Click( object sender, EventArgs e )
        {
            if ( m_aktuelle_frage != null)
            {
                Clipboard.SetText( fkExportFrageBogen.getClipBoardExportString( getAnwFragenFrager().getAktFrage() ) ) ;
            }
        }

        /*
         * ################################################################################
         */
        private void m_btnUiFrageTestAnzeigen_Click_1( object sender, EventArgs e )
        {
            Clipboard.SetText(fkExportFrageBogen.getClipBoardTestString()); 
        }

        private bool isChecked1() { return ( m_optAntwort1.Visible ? m_optAntwort1.Checked : false) ; }
        private bool isChecked2() { return ( m_optAntwort2.Visible ? m_optAntwort2.Checked : false) ; }
        private bool isChecked3() { return ( m_optAntwort3.Visible ? m_optAntwort3.Checked : false) ; }
        private bool isChecked4() { return ( m_optAntwort4.Visible ? m_optAntwort4.Checked : false) ; }
        private bool isChecked5() { return ( m_optAntwort5.Visible ? m_optAntwort5.Checked : false) ; }
        private bool isChecked6() { return ( m_optAntwort6.Visible ? m_optAntwort6.Checked : false) ; }
        private bool isChecked7() { return ( m_optAntwort7.Visible ? m_optAntwort7.Checked : false) ; }
        private bool isChecked8() { return ( m_optAntwort8.Visible ? m_optAntwort8.Checked : false) ; }

        /*
         * ################################################################################
         */
        private void m_btnOK_Click( object sender, EventArgs e )
        {
            anwNaechsteFrage( ANZEIGE_NAECHSTE_FRAGE );
        }

        /*
         * ################################################################################
         */
        private void m_btn5050Joker_Click( object sender, EventArgs e )
        {
            anwUi5050Joker();
        }

        /*
         * ################################################################################
         */
        private void m_mnuDateiExportFragenkatalog_Click( object sender, EventArgs e )
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                bool knz_exportiere_antworten           = true;
                bool knz_exportiere_korrekte_antworten  = true;
                bool knz_exportiere_falsche_antworten   = true;
                bool knz_exportiere_antwort_bezeichnung = true;

                getAnwFragenFrager().exportFrageBogenFragenKatalog( knz_exportiere_antworten, knz_exportiere_korrekte_antworten, knz_exportiere_falsche_antworten, knz_exportiere_antwort_bezeichnung, m_knz_antwort_reihenfolge_umstellen );
            }
            catch ( Exception err_inst )
            {
                Console.WriteLine( "Fehler: errExportFrageBogenFragenKatalog\n" + err_inst.Message );// + "" + err_inst.StackTrace );
            }

            Cursor.Current = Cursors.Default;
        }

        /*
         * ################################################################################
         */
        private void m_mnuDateiFragenkatalogBearbeiten_Click( object sender, EventArgs e )
        {
            /* 
             * Letzter Dateiname
             * Aus der INI-Datei wird der Name der zuletzt geoefnneten XML-Datei geladen.             
             */
            String datei_name = fkIniDatei.readIniDateiName( "DATEI_NAME_XML_FRAGENKATALOG" );

            fkDocumentOpen.documentOpen( datei_name );
        }

        /*
         * ################################################################################
         */
        private void m_mnuDateiFragenkatalogVerzeichnisOpen_Click( object sender, EventArgs e )
        {
            /* 
             * Letzter Dateiname
             * Aus der INI-Datei wird der Name der zuletzt geoefnneten XML-Datei geladen.             
             */
            String datei_name = fkIniDatei.readIniDateiName( "DATEI_NAME_XML_FRAGENKATALOG" );

            /*
             * https://msdn.microsoft.com/de-de/library/system.io.path.getdirectoryname(v=vs.110).aspx
             */

            // directoryName = Path.GetDirectoryName(filePath);

            /*
             * https://stopbyte.com/t/how-can-i-get-the-parent-folder-of-file-in-c/270
             */
            string parent_directory_path = getParentDirPath( datei_name );

            fkDocumentOpen.documentOpen( parent_directory_path );
        }

        /*
         * ################################################################################
         */
        private string getParentDirPath( String path )
        {
            int index_pos = path.Trim( '/', '\\' ).LastIndexOfAny( new char[] { '\\', '/' } );

            if ( index_pos >= 0 )
            {
                return path.Remove( index_pos );
            }

            return null;
        }

        /*
         * ################################################################################
         */
        private void m_btnFrageLoesen_Click( object sender, EventArgs e )
        {
            anwUiKorrekteAntwortAnzeigen();
        }

        /*
         * ################################################################################
         */
        private void m_mnuAbfragemodusFragensitzungBeenden_Click( object sender, EventArgs e )
        {
            anwStoppAbfrageModus();
        }

        /*
         * ################################################################################
         */
        private void m_mnuDateiIniDateiOpen_Click( object sender, EventArgs e )
        {
            fkDocumentOpen.documentOpen( fkIniDatei.getAnwIniDateiName() );
        }

        /*
         * ################################################################################
         */
        private void m_mnuAbfragemodusExportFragensitzungFalsch_Click( object sender, EventArgs e )
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                bool knz_exportiere_antworten = true;
                bool knz_exportiere_korrekte_antworten = true;
                bool knz_exportiere_falsche_antworten = true;
                bool knz_exportiere_antwort_bezeichnung = true;

                getAnwFragenFrager().exportFrageBogenLernFabrik( fkExportFrageBogen.EXPORT_LERN_FABRIK_FALSCH, knz_exportiere_antworten, knz_exportiere_korrekte_antworten, knz_exportiere_falsche_antworten, knz_exportiere_antwort_bezeichnung, m_knz_antwort_reihenfolge_umstellen );
            }
            catch (Exception err_inst)
            {
                Console.WriteLine( "Fehler: errExportFragensitzungAlle\n" + err_inst.Message );
            }

            Cursor.Current = Cursors.Default;
        }

        /*
         * ################################################################################
         */
        private void m_mnuAbfragemodusExportFragensitzungKorrekt_Click( object sender, EventArgs e )
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                bool knz_exportiere_antworten = true;
                bool knz_exportiere_korrekte_antworten = true;
                bool knz_exportiere_falsche_antworten = true;
                bool knz_exportiere_antwort_bezeichnung = true;

                getAnwFragenFrager().exportFrageBogenLernFabrik( fkExportFrageBogen.EXPORT_LERN_FABRIK_KORREKT, knz_exportiere_antworten, knz_exportiere_korrekte_antworten, knz_exportiere_falsche_antworten, knz_exportiere_antwort_bezeichnung, m_knz_antwort_reihenfolge_umstellen);
            }
            catch (Exception err_inst)
            {
                Console.WriteLine( "Fehler: errExportFragensitzungAlle\n" + err_inst.Message );// + "" + err_inst.StackTrace );
            }

            Cursor.Current = Cursors.Default;
        }

        /*
         * ################################################################################
         */
        private void m_mnuAbfragemodusExportFragensitzungXmlAlle_Click( object sender, EventArgs e )
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                getAnwFragenFrager().exportFrageBogenLernFabrikXml( fkExportFrageBogen.EXPORT_LERN_FABRIK_ALLES );
            }
            catch ( Exception err_inst )
            {
                Console.WriteLine( "Fehler: errExportFragensitzungAlle\n" + err_inst.Message ); // + "" + err_inst.StackTrace );
            }

            Cursor.Current = Cursors.Default;
        }

        /*
         * ################################################################################
         */
        private void m_mnuAbfragemodusExportFragensitzungXmlRichtig_Click( object sender, EventArgs e )
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                getAnwFragenFrager().exportFrageBogenLernFabrikXml( fkExportFrageBogen.EXPORT_LERN_FABRIK_KORREKT );
            }
            catch (Exception err_inst)
            {
                Console.WriteLine( "Fehler: errExportFragensitzungAlle\n" + err_inst.Message ); // + "" + err_inst.StackTrace );
            }

            Cursor.Current = Cursors.Default;
        }

        /*
         * ################################################################################
         */
        private void m_mnuAbfragemodusExportFragensitzungXmlFalsch_Click( object sender, EventArgs e )
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                getAnwFragenFrager().exportFrageBogenLernFabrikXml( fkExportFrageBogen.EXPORT_LERN_FABRIK_FALSCH );
            }
            catch (Exception err_inst)
            {
                Console.WriteLine( "Fehler: errExportFragensitzungAlle\n" + err_inst.Message ); // + "" + err_inst.StackTrace );
            }

            Cursor.Current = Cursors.Default;
        }

        /* 
         * ################################################################################
         */
        private void m_btnTestAntwortReihenfolge_Click( object sender, EventArgs e )
        {
            if ( m_aktuelle_frage == null )
            {
                return;
            }

            /*
             * Bei der aktuellen Frage wird der Funktion fuer die 
             * Umstellung der Antworten aufgerufen. 
             */
            if ( m_aktuelle_frage.startAntwortReihenfolgeUmstellen() )
            {
                /*
                 * Nachdem die Antwortreihenfolge umgestellt ist, wird 
                 * die aktuelle Frage neu in die Controls geschrieben.
                 */ 
                anwFrageAnzeigen( m_aktuelle_frage );

                /*
                 * Wurde die Frage vom Anwender schon geloest, wird 
                 * die Frage nach der Antwortumstellung auch im 
                 * geloesten Zustand angezeigt. 
                 */
                if ( m_ui_knz_loesen_moeglich == false )
                {
                    m_ui_knz_loesen_moeglich = true;

                    anwUiKorrekteAntwortAnzeigen();
                }
                else
                {
                    /*
                     * Wurde die Frage noch nicht vom Anwender geloest, aber
                     * der 50P-Joker wurde schon durchgefuehrt, wird dieses 
                     * nach der Umstellung erneut gemacht. 
                     * 
                     * Es gibt keine Sicherung der alten ausgeblendeten Antworten.
                     * (optimierung: genau das einbauen)
                     */
                    if ( m_ui_knz_50_50_joker_durchgefuehrt )
                    {
                        anwUi5050Joker();
                    }
                }

                /*
                 * Zu Debug-Zwecken wird die Reihenfolge der UI-Positionen ausgegeben.
                 */
                m_lblStatusAnzeige1.Text = "Reihenfolge: " + m_aktuelle_frage.getStrVertauschReihenfolge();
            }
        }

        /* 
         * ################################################################################
         */
        private void m_mnuOptionenKnzAntwortreihenfolgeUmstellen_Click( object sender, EventArgs e )
        {
            m_knz_antwort_reihenfolge_umstellen = !m_knz_antwort_reihenfolge_umstellen;

            m_mnuOptionenKnzAntwortreihenfolgeUmstellen.Checked = m_knz_antwort_reihenfolge_umstellen;

            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_ANTWORT_REIHENFOLGE_UMSTELLEN", m_knz_antwort_reihenfolge_umstellen );

            if ( m_knz_antwort_reihenfolge_umstellen )
            {
                if ( m_aktuelle_frage != null )
                {
                    if ( m_aktuelle_frage.startAntwortReihenfolgeUmstellen() )
                    {
                        anwFrageAnzeigen( m_aktuelle_frage );
                    }
                }
            }            
        }


        /* 
         * ################################################################################
         */
        private void m_btnLoadTestFragenkatalog_Click( object sender, EventArgs e )
        {
            m_lblGesamteFragen.Text = " --- ";

            m_lblStatusAnzeige1.Text = "Erstelle Testfragenkatalog";

            Application.DoEvents();

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if ( getAnwFragenFrager().erstelleTestFragenKatalog() )
                {
                    m_lblGesamteFragen.Text = getAnwFragenFrager().getAnzahlLabel();

                    anwNeueFrageAnzeigen( ANZEIGE_ERSTE_FRAGE );
                }
            }
            catch ( Exception err_inst )
            {
                Console.WriteLine( "Fehler: errXmlDateiLaden\n" + err_inst.Message );// + "" + err_inst.StackTrace );
            }

            Cursor.Current = Cursors.Default;

            m_lblStatusAnzeige1.Text = "";
        }

        /* 
         * ################################################################################
         */
        private void anwNeueFrageAnzeigen( int pCommand )
        {
            /*
             * Navigation durch die Fragen
             * 
             * Mit dem Parameter "pCommand" wird angegeben, welche Funktion ausgefuehrt werden soll:
             * 
             * ANZEIGE_ERSTE_FRAGE     = gehe zur ersten Frage 
             * ANZEIGE_NAECHSTE_FRAGE  = gehe zur naechsten Frage
             * ANZEIGE_VORHERIGE_FRAGE = gehe zur vorigen Frage
             * ANZEIGE_LETZTE_FRAGE    = gehe zur letzten Frage
             * 
             * ANZEIGE_AKTUELLE_FRAGE  = zeige die aktuelle Frage mit den aktuellen UI-Einstellungen an
             *                           Aendern sich UI-Einstellungen, muss es eine Funktion geben, um 
             *                           die aktuelle Frage neu "rendern" zu koennen.
             */

            bool knz_reset_antwortenfelder = false;

            switch ( pCommand )
            {
                case ANZEIGE_ERSTE_FRAGE:

                    getAnwFragenFrager().moveFirst();

                    knz_reset_antwortenfelder = true;

                    break;

                case ANZEIGE_NAECHSTE_FRAGE:

                    getAnwFragenFrager().moveNext();

                    knz_reset_antwortenfelder = true;

                    break;

                case ANZEIGE_VORHERIGE_FRAGE:

                    getAnwFragenFrager().movePrevious();

                    knz_reset_antwortenfelder = true;

                    break;

                case ANZEIGE_LETZTE_FRAGE:

                    getAnwFragenFrager().moveLast();

                    knz_reset_antwortenfelder = true;

                    break;
            }

            /*
             * Reset Ui-Antwortpositionen
             * Ist eine aktuelle Frage vorhanden, werden in dieser Frage die
             * UI-Antwortpositionen wieder in den Normalzustand gebracht.
             */
            if ( m_aktuelle_frage != null )
            {
                m_aktuelle_frage.resetAntwortIndexPosition();
            }

            /*
             * Neue aktuelle Frage 
             * Aus der Anwendungsklasse wird die aktuelle Frage geholt und der 
             * Variablen "m_aktuelle_frage" zugewiesen.
             */
            m_aktuelle_frage = getAnwFragenFrager().getAktFrage();

            /*
             * Umstellung Antworten
             * Ist das Kennzeichen fuer die Antwortumstellung gesetzt, werden in 
             * der aktuellen Frage die Ui-Antwortpositionen neu vergeben.
             */
            if ( m_knz_antwort_reihenfolge_umstellen )
            {
                if ( m_aktuelle_frage != null )
                {
                    m_aktuelle_frage.startAntwortReihenfolgeUmstellen();
                }
            }

            m_lblStatusAnzeige1.Text = getAnwFragenFrager().getTextStatusFragenIndex();

            /*
             * Reset Steuerfelder und Freischaltung von temporaer disableten Buttons.
             */
            if ( knz_reset_antwortenfelder )
            {
                /*
                 * Die Antwortfelder werden fuer eine Auswahl wieder freigeschaltet
                 */
                anwEnableAntworten();

                anwSetAntwortenToGrau();

                /*
                 * Der Button "OK" bekommt wieder die Bezeichnung "OK"
                 */
                m_btnOK.Text = "OK";

                /*
                 * Die Buttons "Loesen" und "50 % Joker" werden freigeschaltet. 
                 */
                m_btnFrageLoesen.Enabled = true;

                m_btn5050Joker.Enabled = true;

                /*
                 * Die Kennzeichenvariable "m_knz_btn_ok_ist_naechste_frage" wird
                 * auf FALSE gestellt, da der Button OK jetzt keine Sonderrolle 
                 * mehr hat.
                 */
                m_knz_btn_ok_ist_naechste_frage = false;

                /*
                 * Die Kennzeichenvariable "m_ui_knz_farbmarkierung_antworten" wird
                 * auf TRUE gestellt, damit bei einem Mouse-Move-Ereignis
                 * die Antwortfelder wieder markiert werden.
                 */
                m_ui_knz_farbmarkierung_antworten = true;

                /*
                 * Die Kennzeichenvariable "m_ui_knz_loesen_moeglich" wird
                 * auf TRUE gestellt, da fuer die neue Frage ein Loesen 
                 * wieder moeglich ist.
                 */
                m_ui_knz_loesen_moeglich = true;

                /*
                 * "50 % Joker" wurde noch nicht durchgefuehrt. 
                 */
                m_ui_knz_50_50_joker_durchgefuehrt = false;
            }

            /*
             * Aufruf der Funktion zum Anzeigen der Frage
             */
            anwFrageAnzeigen( m_aktuelle_frage );
        }

        /* 
         * ################################################################################
         */
        private bool anwNaechsteFrage( int pCommand )
        {
            if ( getAnwFragenFrager().istModusFragenKatalogAnzeigen() )
            {
                /* 
                 * Modus Fragenkatalog anzeigen
                 * 
                 * Es wird die Funktion "anwNeueFrageAnzeigen" mit dem Command aufgerufen.
                 */
                anwNeueFrageAnzeigen( pCommand );
            }
            else if ( getAnwFragenFrager().istModusAbfragen() )
            {
                /* 
                 *     Modus Abfragen, Command "ANZEIGE_ERSTE_FRAGE"
                 *     Modus Abfragen, Command "ANZEIGE_VORHERIGE_FRAGE"
                 * und Modus Abfragen, Command "ANZEIGE_NAECHSTE_FRAGE" und m_knz_btn_ok_ist_naechste_frage = true
                 * 
                 * m_knz_btn_ok_ist_naechste_frage = Anzeige der Korrekten Antwort
                 *                                 = Die Frage wurde schon beantwortet, es wird in diesem 
                 *                                   Zusammenhang nur noch die korrekte Antwort angezeigt. 
                 *                                   Somit muss bei TRUE nur die naechste Frage angezeigt werden.
                 */
                if ( ( pCommand == ANZEIGE_ERSTE_FRAGE ) || ( pCommand == ANZEIGE_VORHERIGE_FRAGE ) || ( m_knz_btn_ok_ist_naechste_frage ) )
                {
                    m_knz_btn_ok_ist_naechste_frage = false;

                    m_btnOK.Text = "OK";

                    anwNeueFrageAnzeigen( pCommand );
                }
                else
                {
                    /* 
                     * Modus Abfragen, Command "ANZEIGE_NAECHSTE_FRAGE" und m_knz_btn_ok_ist_naechste_frage = false
                     */
                    bool knz_frage_korrekt_beantwortet = false;

                    bool knz_frage_beantwortet = false;

                    bool knz_frage_a_korrekt = false;
                    bool knz_frage_b_korrekt = false;
                    bool knz_frage_c_korrekt = false;
                    bool knz_frage_d_korrekt = false;
                    bool knz_frage_e_korrekt = false;
                    bool knz_frage_f_korrekt = false;
                    bool knz_frage_g_korrekt = false;
                    bool knz_frage_h_korrekt = false;

                    if ( m_aktuelle_frage == null )
                    {
                        /*
                         * Aufruf der Funktion "anwNeueFrageAnzeigen" mit dem 
                         * uebergebenen Kommando.
                         */
                        anwNeueFrageAnzeigen( pCommand );

                        /*
                         * Die Kennzeichenvariable fuer die Farbmarkieung der 
                         * Antworten bei Mouse-Move wird auf TRUE gestellt.
                         */
                        m_ui_knz_farbmarkierung_antworten = true;
                    }
                    else
                    {
                        /*
                         * Ermittlung, ob die Frage beantwortet wurde
                         * 
                         * Die Frage ist dann beantwortet, wenn mindestens eine Checkbox gewaehlt worden ist.
                         * 
                         * Es wird fuer jede UI-Position geprueft, ob es dort eine Antwort gibt.
                         * 
                         * Gibt es eine Antwort an der UI-Position, wird geprueft, ob die Checkbox gewaehlt worden ist.
                         */
                        knz_frage_beantwortet = false;

                        if ( m_aktuelle_frage.hasUiPositionAntwort1() ) { knz_frage_beantwortet = knz_frage_beantwortet || m_optAntwort1.Checked; }
                        if ( m_aktuelle_frage.hasUiPositionAntwort2() ) { knz_frage_beantwortet = knz_frage_beantwortet || m_optAntwort2.Checked; }
                        if ( m_aktuelle_frage.hasUiPositionAntwort3() ) { knz_frage_beantwortet = knz_frage_beantwortet || m_optAntwort3.Checked; }
                        if ( m_aktuelle_frage.hasUiPositionAntwort4() ) { knz_frage_beantwortet = knz_frage_beantwortet || m_optAntwort4.Checked; }
                        if ( m_aktuelle_frage.hasUiPositionAntwort5() ) { knz_frage_beantwortet = knz_frage_beantwortet || m_optAntwort5.Checked; }
                        if ( m_aktuelle_frage.hasUiPositionAntwort6() ) { knz_frage_beantwortet = knz_frage_beantwortet || m_optAntwort6.Checked; }
                        if ( m_aktuelle_frage.hasUiPositionAntwort7() ) { knz_frage_beantwortet = knz_frage_beantwortet || m_optAntwort7.Checked; }
                        if ( m_aktuelle_frage.hasUiPositionAntwort8() ) { knz_frage_beantwortet = knz_frage_beantwortet || m_optAntwort8.Checked; } 
                         
                        if ( knz_frage_beantwortet )
                        {
                            /*
                             * Die Anzahl der beantworteten Fragen wird um 1 erhoeht.
                             */
                            m_anzahl_fragen_beantwortet++;

                            /*
                             * Ermittlung: Einzelne Antwort an der UI-Position korrekt ausgewaehlt
                             * 
                             * Es wird bei der aktuellen Frage nachgefragt, ob die Antwort an der UI-Position korrekt ist.
                             * 
                             *    -> Ist die Antwort korrekt, muss auch die Checkbox gewaehlt worden sein
                             * 
                             *    -> Ist die Antwort falsch, darf die Checkbox nicht gewaehlt worden sein
                             * 
                             * Diese Pruefung wird fuer jede UI-Position gemacht und separat in einer Kennzeichenvariable gespeichert.
                             */
                            knz_frage_a_korrekt = ( m_aktuelle_frage.getUiPositionAntwort1Korrekt() ? m_optAntwort1.Checked : m_optAntwort1.Checked == false );
                            knz_frage_b_korrekt = ( m_aktuelle_frage.getUiPositionAntwort2Korrekt() ? m_optAntwort2.Checked : m_optAntwort2.Checked == false );
                            knz_frage_c_korrekt = ( m_aktuelle_frage.getUiPositionAntwort3Korrekt() ? m_optAntwort3.Checked : m_optAntwort3.Checked == false );
                            knz_frage_d_korrekt = ( m_aktuelle_frage.getUiPositionAntwort4Korrekt() ? m_optAntwort4.Checked : m_optAntwort4.Checked == false );
                            knz_frage_e_korrekt = ( m_aktuelle_frage.getUiPositionAntwort5Korrekt() ? m_optAntwort5.Checked : m_optAntwort5.Checked == false );
                            knz_frage_f_korrekt = ( m_aktuelle_frage.getUiPositionAntwort6Korrekt() ? m_optAntwort6.Checked : m_optAntwort6.Checked == false );
                            knz_frage_g_korrekt = ( m_aktuelle_frage.getUiPositionAntwort7Korrekt() ? m_optAntwort7.Checked : m_optAntwort7.Checked == false );
                            knz_frage_h_korrekt = ( m_aktuelle_frage.getUiPositionAntwort8Korrekt() ? m_optAntwort8.Checked : m_optAntwort8.Checked == false );

                            /*
                             * Ermittlung: Frage insgesamt korrekt beantwortet
                             * 
                             * Es wird in einer Kennzeichenvariable vermerkt, ob Frage korrekt beantwortet wurde.
                             * 
                             * Es muss jede einzelne Antwort an der UI-Position korrekt sein.
                             * 
                             * Alle Antwortkennzeichenfelder muessen auf TRUE stehen.
                             */
                            knz_frage_korrekt_beantwortet = knz_frage_a_korrekt && knz_frage_b_korrekt && knz_frage_c_korrekt && knz_frage_d_korrekt && knz_frage_e_korrekt && knz_frage_f_korrekt && knz_frage_g_korrekt && knz_frage_h_korrekt;

                            /*
                             * Zaehlervariablen aktualisieren
                             * 
                             * Wurde die Frage korrekt beantwortet, wird der Zaehler fuer die korrekt beantworteten Fragen erhoeht.
                             * 
                             * Wurde die Frage falsch beantwortet, wird der Zaehler fuer die falsch beantworteten Fragen erhoeht.
                             */
                            if ( knz_frage_korrekt_beantwortet )
                            {
                                m_anzahl_korrekt_beantwortet++;
                            }
                            else
                            {
                                m_anzahl_falsch_beantwortet++;
                            }

                            /*
                             * Zaehlervariablen in der Anwendungsklasse aktualisieren
                             */
                            getAnwFragenFrager().updateZaehler( knz_frage_beantwortet, knz_frage_korrekt_beantwortet, !knz_frage_korrekt_beantwortet );

                            /*
                             * Ermittlung: Antwort nach der normalen Reihenfolge korrekt ausgewaehlt
                             * 
                             * Es werden hier die Kennzeichenfelder ermittelt, welche Antwort nach der originalen Reihenfolge her beantwortet wurden.
                             * 
                             * Es wird hier nicht nach UI-Position, sondern nach Fragen-Position geschaut.
                             * 
                             * Dieses Vorgehen sorgt dafuer, dass eine Auswahl von z.B. Antwort A erhalten bleibt, wenn in 
                             * den Fragen vor und zurueck gegangen wird. 
                             * 
                             * Da die Antwortreihenfolge umgestellt werden kann, darf hier nicht nach UI-Positionen gegangen werden.
                             */
                            bool knz_antwort_a_checked = m_aktuelle_frage.istAntwortAChecked( m_optAntwort1.Checked, m_optAntwort2.Checked, m_optAntwort3.Checked, m_optAntwort4.Checked, m_optAntwort5.Checked, m_optAntwort6.Checked, m_optAntwort7.Checked, m_optAntwort8.Checked );
                            bool knz_antwort_b_checked = m_aktuelle_frage.istAntwortBChecked( m_optAntwort1.Checked, m_optAntwort2.Checked, m_optAntwort3.Checked, m_optAntwort4.Checked, m_optAntwort5.Checked, m_optAntwort6.Checked, m_optAntwort7.Checked, m_optAntwort8.Checked );
                            bool knz_antwort_c_checked = m_aktuelle_frage.istAntwortCChecked( m_optAntwort1.Checked, m_optAntwort2.Checked, m_optAntwort3.Checked, m_optAntwort4.Checked, m_optAntwort5.Checked, m_optAntwort6.Checked, m_optAntwort7.Checked, m_optAntwort8.Checked );
                            bool knz_antwort_d_checked = m_aktuelle_frage.istAntwortDChecked( m_optAntwort1.Checked, m_optAntwort2.Checked, m_optAntwort3.Checked, m_optAntwort4.Checked, m_optAntwort5.Checked, m_optAntwort6.Checked, m_optAntwort7.Checked, m_optAntwort8.Checked );
                            bool knz_antwort_e_checked = m_aktuelle_frage.istAntwortEChecked( m_optAntwort1.Checked, m_optAntwort2.Checked, m_optAntwort3.Checked, m_optAntwort4.Checked, m_optAntwort5.Checked, m_optAntwort6.Checked, m_optAntwort7.Checked, m_optAntwort8.Checked );
                            bool knz_antwort_f_checked = m_aktuelle_frage.istAntwortFChecked( m_optAntwort1.Checked, m_optAntwort2.Checked, m_optAntwort3.Checked, m_optAntwort4.Checked, m_optAntwort5.Checked, m_optAntwort6.Checked, m_optAntwort7.Checked, m_optAntwort8.Checked );
                            bool knz_antwort_g_checked = m_aktuelle_frage.istAntwortGChecked( m_optAntwort1.Checked, m_optAntwort2.Checked, m_optAntwort3.Checked, m_optAntwort4.Checked, m_optAntwort5.Checked, m_optAntwort6.Checked, m_optAntwort7.Checked, m_optAntwort8.Checked );
                            bool knz_antwort_h_checked = m_aktuelle_frage.istAntwortHChecked( m_optAntwort1.Checked, m_optAntwort2.Checked, m_optAntwort3.Checked, m_optAntwort4.Checked, m_optAntwort5.Checked, m_optAntwort6.Checked, m_optAntwort7.Checked, m_optAntwort8.Checked );

                            /*
                             * Die Kennzeichen, welche Antwort ausgewaehlt worden war, wird an die Fragensitzung weitergegeben.
                             */
                            getAnwFragenFrager().updateKnzGewaehlt( knz_antwort_a_checked, knz_antwort_b_checked, knz_antwort_c_checked, knz_antwort_d_checked, knz_antwort_e_checked, knz_antwort_f_checked, knz_antwort_g_checked, knz_antwort_h_checked );
                        }
                        else
                        {
                            /*
                             * Wurde die Frage nicht beantwortet, wird der Zaehler fuer die nicht beantworteten Fragen erhoeht
                             */
                            m_anzahl_fragen_nicht_beantwortet++;

                            getAnwFragenFrager().updateZaehler( knz_frage_beantwortet, false, false );

                            getAnwFragenFrager().updateKnzGewaehlt( false, false, false, false, false, false, false, false );
                        }

                        /*
                         * Pruefung: Weiter zur naechsten Frage?
                         * 
                         * Sind alle Auswertungen zur aktuellen Frage erledigt, wird hier geprueft, 
                         * ob sofort zur naechsten Frage weitergeschaltet wird oder nicht.
                         * 
                         * Soll die korrekte Antwort nach einer Frage NICHT angezeigt werden, wird zur naechsten Frage verzweigt.
                         * 
                         * Soll die korrekte Antwort angezeigt werden, folgen weitere Pruefungen:
                         * 
                         * - wurde die Frage falsch beantwortet wird die korrekte Antwort(en) angezeigt
                         * 
                         * - wurde die Frage beantwortet und das korrekt dann wird das Kennzeichen 
                         *   "m_knz_korrekt_beantwortet_weiter" ausgewertet. 
                         * 
                         *   - Steht diese Variable auf TRUE, wird zur naechsten Frage verzweigt.
                         *     Der Anwender moechte bei korrekt beantworteten Fragen gleich weiter
                         * 
                         *   - Steht diese Variable auf FALSE, werden die korrekten Antworten angezeigt
                         *     Die Frage an sich ist schon korrekt beantwortet, es werden aber alle 
                         *     korrekten Antworten gruen hinterlegt.
                         */
                        if ( m_knz_korrekte_antwort_anzeigen )
                        {
                            if ( knz_frage_beantwortet && knz_frage_korrekt_beantwortet && m_knz_korrekt_beantwortet_weiter )
                            {
                                anwNeueFrageAnzeigen( pCommand );
                            }
                            else
                            {
                                /*
                                 * Die Kennzeichenvariable fuer die Farbmarkieung wird auf FALSE 
                                 * gestellt, da jetzt die Frage im geloesten Zustand angezeigt 
                                 * wird und eine Veraenderung der Hintergrundfarbe bei den Antworten
                                 * nicht gemacht werden soll.
                                 */
                                m_ui_knz_farbmarkierung_antworten = false;

                                anwUiCheckAntwortNachOk( m_aktuelle_frage.getUiPositionAntwort1Korrekt(), m_optAntwort1, m_lblAntwort1 );
                                anwUiCheckAntwortNachOk( m_aktuelle_frage.getUiPositionAntwort2Korrekt(), m_optAntwort2, m_lblAntwort2 );
                                anwUiCheckAntwortNachOk( m_aktuelle_frage.getUiPositionAntwort3Korrekt(), m_optAntwort3, m_lblAntwort3 );
                                anwUiCheckAntwortNachOk( m_aktuelle_frage.getUiPositionAntwort4Korrekt(), m_optAntwort4, m_lblAntwort4 );
                                anwUiCheckAntwortNachOk( m_aktuelle_frage.getUiPositionAntwort5Korrekt(), m_optAntwort5, m_lblAntwort5 );
                                anwUiCheckAntwortNachOk( m_aktuelle_frage.getUiPositionAntwort6Korrekt(), m_optAntwort6, m_lblAntwort6 );
                                anwUiCheckAntwortNachOk( m_aktuelle_frage.getUiPositionAntwort7Korrekt(), m_optAntwort7, m_lblAntwort7 );
                                anwUiCheckAntwortNachOk( m_aktuelle_frage.getUiPositionAntwort8Korrekt(), m_optAntwort8, m_lblAntwort8 );

                                /*
                                 * Der OK-Button bekommt jetzt eine andere Funktion.
                                 *
                                 * Mit dem naechsten Click auf den OK-Button wird zur naechsten Frage gewechselt.
                                 * 
                                 * Die Kennzeichenvariable "m_knz_btn_ok_ist_naechste_frage" wird auf TRUE gestellt.
                                 * 
                                 * Die Buttonbeschriftung wird geaendert.
                                 */
                                m_knz_btn_ok_ist_naechste_frage = true;

                                m_btnOK.Text = "naechste Frage";
                            }
                        }
                        else
                        {
                            anwNeueFrageAnzeigen( pCommand );
                        }

                        /*
                         * Es wird der Zaehler fuer die "Anzahl der angezeigten Fragen" erhoeht.
                         */
                        m_anzahl_angezeigt++;

                        /*
                         * Prozentrechnung 
                         * 
                         * Ermittlung des Prozentwertes fuer "Anazhl korrekt beantwortet"
                         */
                        m_anzahl_prozent_korrekt = ( 100.0 * m_anzahl_korrekt_beantwortet ) / m_anzahl_fragen_beantwortet;

                        /*
                         * Zaehlerwerte in die UI-Controls schreiben
                         */
                        m_lblWerttProzentKorrekt.Text = String.Format( "{0:#00.000}", m_anzahl_prozent_korrekt ) + " %";

                        m_lblAnzahlNichtBeantwortetWert.Text = "" + m_anzahl_fragen_nicht_beantwortet;

                        m_lblRichtigBeantwortetWert.Text = "" + m_anzahl_korrekt_beantwortet;

                        m_lblFalschBeantwortetWert.Text = "" + m_anzahl_falsch_beantwortet;

                        m_lblAnzahlBeantwortetWert.Text = "" + m_anzahl_fragen_beantwortet;

                        m_lblAnzahlAngezeigtWert.Text = "" + m_anzahl_angezeigt;

                        m_lblStatusAnzeige1.Text = "";
                    }
                }
            }

            return true;
        }

        /*
         * ################################################################################
         */
        private void anwEnableAntworten()
        {
            m_optAntwort1.Enabled = true;
            m_optAntwort2.Enabled = true;
            m_optAntwort3.Enabled = true;
            m_optAntwort4.Enabled = true;
            m_optAntwort5.Enabled = true;
            m_optAntwort6.Enabled = true;
            m_optAntwort7.Enabled = true;
            m_optAntwort8.Enabled = true;

            m_lblAntwort1.Enabled = true;
            m_lblAntwort2.Enabled = true;
            m_lblAntwort3.Enabled = true;
            m_lblAntwort4.Enabled = true;
            m_lblAntwort5.Enabled = true;
            m_lblAntwort6.Enabled = true;
            m_lblAntwort7.Enabled = true;
            m_lblAntwort8.Enabled = true;
        }

        /*
         * ################################################################################
         */
        private void anwUiMarkiereAntwortMouseMove( int pIndexAntwortControl )
        {
            /*
             * Markierung der Antworten
             * 
             * Bei einem Mouse-Move-Ereignis sollen die UI-Controls der Antwort unter
             * dem Mouse-Cursor hervorgehoben werden.
             * 
             * Die Farbmarkierung wird ausgeschaltet, wenn die aktuelle Frage vom 
             * Anwender geloest worden ist. In diesem Fall stehen die Farben der
             * UI-Antwort-Controls fest und sollen durch ein Mouse-Move nicht 
             * veraendert werden.
             * 
             * Diese Funktion wird mit dem Antwortindex des Mouse-Move-Events gerufen.
             * 
             * Es werden alle Antworten in der Farbe geaendert, wobei diejenige
             * Antwort mit dem Index aus dem Parameter eine andere Farbe bekommt.
             * Es wird die Hintergrundfarbe geaendert.
             */
            if (m_ui_knz_farbmarkierung_antworten)
            {
                m_lblAntwort1.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_1 ? FARBE_AKTUELL : FARBE_GRAU );
                m_optAntwort1.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_1 ? FARBE_AKTUELL : FARBE_GRAU );

                m_lblAntwort2.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_2 ? FARBE_AKTUELL : FARBE_GRAU );
                m_optAntwort2.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_2 ? FARBE_AKTUELL : FARBE_GRAU );

                m_lblAntwort3.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_3 ? FARBE_AKTUELL : FARBE_GRAU );
                m_optAntwort3.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_3 ? FARBE_AKTUELL : FARBE_GRAU );

                m_lblAntwort4.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_4 ? FARBE_AKTUELL : FARBE_GRAU );
                m_optAntwort4.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_4 ? FARBE_AKTUELL : FARBE_GRAU );

                m_lblAntwort5.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_5 ? FARBE_AKTUELL : FARBE_GRAU );
                m_optAntwort5.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_5 ? FARBE_AKTUELL : FARBE_GRAU );

                m_lblAntwort6.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_6 ? FARBE_AKTUELL : FARBE_GRAU );
                m_optAntwort6.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_6 ? FARBE_AKTUELL : FARBE_GRAU );

                m_lblAntwort7.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_7 ? FARBE_AKTUELL : FARBE_GRAU );
                m_optAntwort7.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_7 ? FARBE_AKTUELL : FARBE_GRAU );

                m_lblAntwort8.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_8 ? FARBE_AKTUELL : FARBE_GRAU );
                m_optAntwort8.BackColor = ( pIndexAntwortControl == INDEX_ANTWORT_8 ? FARBE_AKTUELL : FARBE_GRAU );
            }
        }

        /*
         * ################################################################################
         */
        private void anwSetKnzAntwortBezeichnungAnzeigen( bool pStatus )
        {
            if ( pStatus )
            {
                m_optAntwort1.Text = "a)";
                m_optAntwort2.Text = "b)";
                m_optAntwort3.Text = "c)";
                m_optAntwort4.Text = "d)";
                m_optAntwort5.Text = "e)";
                m_optAntwort6.Text = "f)";
                m_optAntwort7.Text = "g)";
                m_optAntwort8.Text = "h)";
            }
            else
            {
                m_optAntwort1.Text = "";
                m_optAntwort2.Text = "";
                m_optAntwort3.Text = "";
                m_optAntwort4.Text = "";
                m_optAntwort5.Text = "";
                m_optAntwort6.Text = "";
                m_optAntwort7.Text = "";
                m_optAntwort8.Text = "";
            }
        }

        /*
         * ################################################################################
         */
        private void anwFrageAnzeigen( clsFrage pUiFrage )
        {
            try
            {
                /*
                 * Pruefung: Frage zum anzeigen vorhanden ?
                 */
                if ( pUiFrage == null )
                {
                    /*
                     * Ist die Frage aus dem Parameter "pUiFrage" gleich "null",
                     * werden Standard-Texte in den UI-Controls ausgegeben.
                     */

                    m_lblFrageNr.Text = "0";
                    m_lblFrage1.Text = "Keine Frage zum Anzeigen vorhanden!";

                    m_lblAntwort1.Text = "Antwort A";
                    m_lblAntwort2.Text = "Antwort B";
                    m_lblAntwort3.Text = "Antwort C";
                    m_lblAntwort4.Text = "Antwort D";
                    m_lblAntwort5.Text = "Antwort E";
                    m_lblAntwort6.Text = "Antwort F";
                    m_lblAntwort7.Text = "Antwort G";
                    m_lblAntwort8.Text = "Antwort H";

                    m_lblFrage2.Text = "-- Fragenkatalog laden --";
                    // m_lblGeltungsbereich.Text = "";
                }
                else
                {
                    /*
                     * 
                     */
                    if ( m_knz_fragen_nummer_anzeigen )
                    {
                        m_lblFrageNr.Text = pUiFrage.getNummer();
                    }
                    else 
                    {
                        m_lblFrageNr.Text = "";
                    }

                    if ( m_knz_fragen_text_anzeigen )
                    {
                        m_lblFrage1.Text = pUiFrage.getText1();
                        m_lblFrage2.Text = pUiFrage.getText2();
                    }
                    else 
                    {
                        m_lblFrage1.Text = "";
                        m_lblFrage2.Text = "";
                    }

                    /*
                     * Steuerung: Anzeige Controls Checkbox und Antwortentext nach dem vorhanden sein einer Antwort an der UI-Position
                     *
                     * Einblendung oder Ausblendung des Controls
                     */
                    m_lblAntwort1.Visible = pUiFrage.hasUiPositionAntwort1();
                    m_lblAntwort2.Visible = pUiFrage.hasUiPositionAntwort2();
                    m_lblAntwort3.Visible = pUiFrage.hasUiPositionAntwort3();
                    m_lblAntwort4.Visible = pUiFrage.hasUiPositionAntwort4();
                    m_lblAntwort5.Visible = pUiFrage.hasUiPositionAntwort5();
                    m_lblAntwort6.Visible = pUiFrage.hasUiPositionAntwort6();
                    m_lblAntwort7.Visible = pUiFrage.hasUiPositionAntwort7();
                    m_lblAntwort8.Visible = pUiFrage.hasUiPositionAntwort8();

                    m_optAntwort1.Visible = m_lblAntwort1.Visible;
                    m_optAntwort2.Visible = m_lblAntwort2.Visible;
                    m_optAntwort3.Visible = m_lblAntwort3.Visible;
                    m_optAntwort4.Visible = m_lblAntwort4.Visible;
                    m_optAntwort5.Visible = m_lblAntwort5.Visible;
                    m_optAntwort6.Visible = m_lblAntwort6.Visible;
                    m_optAntwort7.Visible = m_lblAntwort7.Visible;
                    m_optAntwort8.Visible = m_lblAntwort8.Visible;

                    int me_height = this.Height;

                    int me_width = this.Width;

                    anwResizeUi( me_height, me_width );

                    /*
                     * Kennzeichenermittlung: Anzeige Antwort-Text nach Kennzeichenfeldern
                     * 
                     * Es gibt 8 Kennzeichenfelder, welche angeben, ob der Antworttext 
                     * an einer UI-Positon angezeigt werden soll oder nicht.
                     *
                     * Diese Kennzeichenfelder sind von dem tatsaechlichen vorhandensein 
                     * einer Antwort an der UI-Position unabhaengig.
                     *
                     * Auswertung der Kennzeichen: Korrekte Antwort anzeigen 
                     *                             Falsche Antwort anzeigen
                     *
                     * Es wird bei jeder UI-Position angefragt, ob die Antwort auf der Position korrekt ist
                     * 
                     *     - Ist die Antwort an der UI-Position korrekt, haengt die Anzeige der Antwort 
                     *       vom Kennzeichenfeld "m_knz_antwort_text_korrekt_anzeigen" ab.
                     * 
                     *     - Ist die Antwort an der UI-Position falsch, haengt die Anzeige der Antwort 
                     *       vom Kennzeichenfeld "m_knz_antwort_text_falsch_anzeigen" ab.
                     * 
                     * Die beiden Kennzeichenfelder  "m_knz_antwort_text_korrekt_anzeigen" und "m_knz_antwort_text_falsch_anzeigen"
                     * stehen normalerweise auf TRUE, welches dazu fuehren wuerde, dass alle Antworten angezeigt wuerden.
                     * 
                     * Diese beiden Felder koennen aber auch auf FALSE gesetzt werden, was dann dazu fuehrt, dass 
                     * trotz einer Antwort auf der UI-Position, die Antwort eben nicht angezeigt wuerde. 
                     * 
                     */

                    bool knz_antwort_text_1_anzeigen = ( pUiFrage.getUiPositionAntwort1Korrekt() ? m_knz_antwort_text_korrekt_anzeigen : m_knz_antwort_text_falsch_anzeigen );
                    bool knz_antwort_text_2_anzeigen = ( pUiFrage.getUiPositionAntwort2Korrekt() ? m_knz_antwort_text_korrekt_anzeigen : m_knz_antwort_text_falsch_anzeigen );
                    bool knz_antwort_text_3_anzeigen = ( pUiFrage.getUiPositionAntwort3Korrekt() ? m_knz_antwort_text_korrekt_anzeigen : m_knz_antwort_text_falsch_anzeigen );
                    bool knz_antwort_text_4_anzeigen = ( pUiFrage.getUiPositionAntwort4Korrekt() ? m_knz_antwort_text_korrekt_anzeigen : m_knz_antwort_text_falsch_anzeigen );
                    bool knz_antwort_text_5_anzeigen = ( pUiFrage.getUiPositionAntwort5Korrekt() ? m_knz_antwort_text_korrekt_anzeigen : m_knz_antwort_text_falsch_anzeigen );
                    bool knz_antwort_text_6_anzeigen = ( pUiFrage.getUiPositionAntwort6Korrekt() ? m_knz_antwort_text_korrekt_anzeigen : m_knz_antwort_text_falsch_anzeigen );
                    bool knz_antwort_text_7_anzeigen = ( pUiFrage.getUiPositionAntwort7Korrekt() ? m_knz_antwort_text_korrekt_anzeigen : m_knz_antwort_text_falsch_anzeigen );
                    bool knz_antwort_text_8_anzeigen = ( pUiFrage.getUiPositionAntwort8Korrekt() ? m_knz_antwort_text_korrekt_anzeigen : m_knz_antwort_text_falsch_anzeigen );

                    /*
                     * Anzeige Antworttexte in Abhaengigkeit des Kennzeichens fuer die Anzeige an der UI-Position
                     * 
                     * Die Anzeige erfolgt in Abhaengigkeit zu den gesetzten Kennzeichenfeldern dieser Klasse
                     */
                    m_lblAntwort1.Text = ( knz_antwort_text_1_anzeigen ? pUiFrage.getUiPositionAntwort1Text() : "" );
                    m_lblAntwort2.Text = ( knz_antwort_text_2_anzeigen ? pUiFrage.getUiPositionAntwort2Text() : "" );
                    m_lblAntwort3.Text = ( knz_antwort_text_3_anzeigen ? pUiFrage.getUiPositionAntwort3Text() : "" );
                    m_lblAntwort4.Text = ( knz_antwort_text_4_anzeigen ? pUiFrage.getUiPositionAntwort4Text() : "" );
                    m_lblAntwort5.Text = ( knz_antwort_text_5_anzeigen ? pUiFrage.getUiPositionAntwort5Text() : "" );
                    m_lblAntwort6.Text = ( knz_antwort_text_6_anzeigen ? pUiFrage.getUiPositionAntwort6Text() : "" );
                    m_lblAntwort7.Text = ( knz_antwort_text_7_anzeigen ? pUiFrage.getUiPositionAntwort7Text() : "" );
                    m_lblAntwort8.Text = ( knz_antwort_text_8_anzeigen ? pUiFrage.getUiPositionAntwort8Text() : "" );

                    /*
                     * Abschnitt: Vorauswahl der Checkboxen
                     */

                    if ( ( getAnwFragenFrager().istModusFragenKatalogAnzeigen() ) && ( m_knz_korrekte_antwort_anzeigen ) )
                    {
                        /*
                         * Modus Fragenkatalog anzeigen und korrekte Antworten markieren eingeschaltet.
                         * 
                         * Es wird bei jeder UI-Position ermittelt, ob die dort angezeigte 
                         * Antwort korrekt ist. Mit diesem Wert wird die Funktion fuer 
                         * die Checkboxmarkierung aufgerufen.
                         */
                        m_optAntwort1.Checked = pUiFrage.getUiPositionAntwort1Korrekt();
                        m_optAntwort2.Checked = pUiFrage.getUiPositionAntwort2Korrekt();
                        m_optAntwort3.Checked = pUiFrage.getUiPositionAntwort3Korrekt();
                        m_optAntwort4.Checked = pUiFrage.getUiPositionAntwort4Korrekt();
                        m_optAntwort5.Checked = pUiFrage.getUiPositionAntwort5Korrekt();
                        m_optAntwort6.Checked = pUiFrage.getUiPositionAntwort6Korrekt();
                        m_optAntwort7.Checked = pUiFrage.getUiPositionAntwort7Korrekt();
                        m_optAntwort8.Checked = pUiFrage.getUiPositionAntwort8Korrekt();
                    }
                    else if ( getAnwFragenFrager().istModusAbfragen() )
                    {

                        /*
                         * Modus Abfragen 
                         * 
                         * Im Modus "Abfragen" werden die einmal gewaehlten Checkbox-Auswahlen wieder ausgewaehlt.
                         * 
                         * Es geht darum, dass bei einer Vertauschung der Antworten, die einmal 
                         * gewaehlte Antwort erhalten bleibt.
                         * 
                         * Es wird bei der Fragensitzung abgefragt, welche Antworten beim letzten 
                         * anzeigen der Frage ausgewaehlt worden waren. Diese Information wird 
                         * in 8 Kennzeichenfeldern gespeichert. 
                         */
                        bool knz_antwort_a_checked = getAnwFragenFrager().getKnzAntwortAGewaehlt();
                        bool knz_antwort_b_checked = getAnwFragenFrager().getKnzAntwortBGewaehlt();
                        bool knz_antwort_c_checked = getAnwFragenFrager().getKnzAntwortCGewaehlt();
                        bool knz_antwort_d_checked = getAnwFragenFrager().getKnzAntwortDGewaehlt();
                        bool knz_antwort_e_checked = getAnwFragenFrager().getKnzAntwortEGewaehlt();
                        bool knz_antwort_f_checked = getAnwFragenFrager().getKnzAntwortFGewaehlt();
                        bool knz_antwort_g_checked = getAnwFragenFrager().getKnzAntwortGGewaehlt();
                        bool knz_antwort_h_checked = getAnwFragenFrager().getKnzAntwortHGewaehlt();

                        /*
                         * Setzen der einmal ausgewaehlten Antwort(en).
                         * 
                         * Es wird die Funktion "istUiPositionChecked" der Frage aufgerufen.
                         * 
                         * Der Funktion werden alle 8 Kennzeichenfelder mitgegeben, welche angeben, ob 
                         * Antwort A bis H beim letzten anzeigen der Frage ausgewaehlt worden waren.
                         * 
                         * Die Funktion "istUiPositionChecked" ermittelt, welche Antwort auf der 
                         * Position X steht und gibt die entsprechende Kennzeichenvariable zurueck.
                         * 
                         * Beispiel:
                         *  - An UI-Position 4 wird die Antwort B angezeigt
                         *  - Dann muss die Kennzeichenvariable fuer Antwort B "knz_antwort_b_checked"
                         *    auf TRUE stehen, damit die Antwort markiert wird.
                         */
                        m_optAntwort1.Checked = m_aktuelle_frage.istUiPositionChecked( 1, knz_antwort_a_checked, knz_antwort_b_checked, knz_antwort_c_checked, knz_antwort_d_checked, knz_antwort_e_checked, knz_antwort_f_checked, knz_antwort_g_checked, knz_antwort_h_checked );
                        m_optAntwort2.Checked = m_aktuelle_frage.istUiPositionChecked( 2, knz_antwort_a_checked, knz_antwort_b_checked, knz_antwort_c_checked, knz_antwort_d_checked, knz_antwort_e_checked, knz_antwort_f_checked, knz_antwort_g_checked, knz_antwort_h_checked );
                        m_optAntwort3.Checked = m_aktuelle_frage.istUiPositionChecked( 3, knz_antwort_a_checked, knz_antwort_b_checked, knz_antwort_c_checked, knz_antwort_d_checked, knz_antwort_e_checked, knz_antwort_f_checked, knz_antwort_g_checked, knz_antwort_h_checked );
                        m_optAntwort4.Checked = m_aktuelle_frage.istUiPositionChecked( 4, knz_antwort_a_checked, knz_antwort_b_checked, knz_antwort_c_checked, knz_antwort_d_checked, knz_antwort_e_checked, knz_antwort_f_checked, knz_antwort_g_checked, knz_antwort_h_checked );
                        m_optAntwort5.Checked = m_aktuelle_frage.istUiPositionChecked( 5, knz_antwort_a_checked, knz_antwort_b_checked, knz_antwort_c_checked, knz_antwort_d_checked, knz_antwort_e_checked, knz_antwort_f_checked, knz_antwort_g_checked, knz_antwort_h_checked );
                        m_optAntwort6.Checked = m_aktuelle_frage.istUiPositionChecked( 6, knz_antwort_a_checked, knz_antwort_b_checked, knz_antwort_c_checked, knz_antwort_d_checked, knz_antwort_e_checked, knz_antwort_f_checked, knz_antwort_g_checked, knz_antwort_h_checked );
                        m_optAntwort7.Checked = m_aktuelle_frage.istUiPositionChecked( 7, knz_antwort_a_checked, knz_antwort_b_checked, knz_antwort_c_checked, knz_antwort_d_checked, knz_antwort_e_checked, knz_antwort_f_checked, knz_antwort_g_checked, knz_antwort_h_checked );
                        m_optAntwort8.Checked = m_aktuelle_frage.istUiPositionChecked( 8, knz_antwort_a_checked, knz_antwort_b_checked, knz_antwort_c_checked, knz_antwort_d_checked, knz_antwort_e_checked, knz_antwort_f_checked, knz_antwort_g_checked, knz_antwort_h_checked );
                    }
                    else
                    {
                        /*
                         * ELSE-Zweig
                         * Generell werden die Antworten auf "nicht gewaehlt" gestellt, wenn der Modus
                         * ein anderer als "Fragenkatalog anzeigen" oder "Abfragen" ist.
                         */
                        m_optAntwort1.Checked = false;
                        m_optAntwort2.Checked = false;
                        m_optAntwort3.Checked = false;
                        m_optAntwort4.Checked = false;
                        m_optAntwort5.Checked = false;
                        m_optAntwort6.Checked = false;
                        m_optAntwort7.Checked = false;
                        m_optAntwort8.Checked = false;
                    }

                    bool knz_debug_modus_x = false;

                    if ( knz_debug_modus_x )
                    {
                        m_optAntwort1.Checked = pUiFrage.getUiPositionAntwort1Korrekt();
                        m_optAntwort2.Checked = pUiFrage.getUiPositionAntwort2Korrekt();
                        m_optAntwort3.Checked = pUiFrage.getUiPositionAntwort3Korrekt();
                        m_optAntwort4.Checked = pUiFrage.getUiPositionAntwort4Korrekt();
                        m_optAntwort5.Checked = pUiFrage.getUiPositionAntwort5Korrekt();
                        m_optAntwort6.Checked = pUiFrage.getUiPositionAntwort6Korrekt();
                        m_optAntwort7.Checked = pUiFrage.getUiPositionAntwort7Korrekt();
                        m_optAntwort8.Checked = pUiFrage.getUiPositionAntwort8Korrekt();
                    }

                    String m_datei_name_aktuelles_bild = pUiFrage.getBild1();

                    if ( m_datei_name_aktuelles_bild != null )
                    {
                        anwUiBildAnzeigen( m_datei_name_aktuelles_bild );
                    }
                    else
                    {
                        anwUiBildUnload();
                    }
                }
            }
            catch ( Exception err_inst )
            {
                m_lblFrage1.Text = err_inst.Message;
            }
        }

        /*
         * ################################################################################
         */
        private void anwUiCheckAntwortNachOk( bool pKnzKorrekt, CheckBox pAntwortCheckBox, Label pAntwortLabel )
        {
            /*
             * Setzt die angegebenen UI-Controls gemaess dem Status "pKnzKorrekt".
             * 
             * Ist die Antwort korrekt, werden die Controls in der Hintergrundfarbe auf GRUEN gestellt.
             * 
             * Ist die Antwort falsch, bestimmt sich die Hintergrundfarbe nach 
             * dem Status der Check-Box:
             * 
             *  - Ist die Check-Box ausgewaehlt, wird die Hintergrundfarbe auf ROT gestellt.
             *    (der Anwender hat die Antwort falsch ausgewaehlt)
             * 
             *  - Ist die Checkbox nicht ausgewaehlt, wird die Hintergrundfarbe auf GRAU gestellt
             */
            if ( pKnzKorrekt )
            {
                pAntwortCheckBox.BackColor = FARBE_GRUEN;
                pAntwortLabel.BackColor    = FARBE_GRUEN;
            }
            else
            {
                if ( pAntwortCheckBox.Checked )
                {
                    pAntwortCheckBox.BackColor = FARBE_ROT;
                    pAntwortLabel.BackColor    = FARBE_ROT;
                }
                else
                {
                    pAntwortCheckBox.BackColor = FARBE_GRAU;
                    pAntwortLabel.BackColor    = FARBE_GRAU;
                }
            }
        }

        /*
         * ################################################################################
         */
        private void anwUiCheckAntwort( bool pKnzKorrekt, CheckBox pAntwortCheckBox, Label pAntwortLabel )
        {
            /*
             * Hilfsfunktion fuer den Button "Loesen"
             * 
             * 1. Setzt alle UI-Controls auf erstmal auf "Enabled"
             * 
             * 2. Setzt die Vordergrundfarbe auf den normalen Zustand
             * 
             * 3. Setzt nach dem Wert aus dem Parameter "pKnzKorrekt" den Status 
             *    fuer die Check-Box. 
             * 
             *    Setzt bei korrekten Antworten das Check-Box-Feld
             * 
             * 4. Setzt nach dem Wert aus dem Parameter "pKnzKorrekt" die 
             *    Hintergrundfarbe (GRUEN oder GRAU).
             * 
             * 5. Setzt alle UI-Controls mit einer falschen Antwort auf "Disabled"
             */
            pAntwortCheckBox.Enabled = true;
            pAntwortLabel.Enabled    = true;
           
             if ( pKnzKorrekt )
             {
                 pAntwortCheckBox.Checked   = STATUS_CHECKED;
                 pAntwortCheckBox.BackColor = FARBE_GRUEN;
                 pAntwortLabel.BackColor    = FARBE_GRUEN;
             }
             else
             {
                 pAntwortCheckBox.Checked   = STATUS_UNCHECKED;
                 pAntwortCheckBox.BackColor = FARBE_GRAU;
                 pAntwortLabel.BackColor    = FARBE_GRAU;

                 pAntwortCheckBox.Enabled = false;
                 pAntwortLabel.Enabled    = false;
             }
        }

        /*
         * ################################################################################
         */
        private void anwUiKorrekteAntwortAnzeigen()
        {
            /*
             * Ist keine aktuelle Frage vorhanden, kann auch nichts geloest werden.
             * 
             * Die Funktion wird verlassen.
             */
            if ( m_aktuelle_frage == null )
            {
                return;
            }

            /*
             * Pruefung: Loesen moeglich ?
             * Wurde die aktuelle Frage schon geloest, wird diese kein
             * weiteres mal geloest.
             */
            if ( m_ui_knz_loesen_moeglich )
            {
                /*
                 * Das Kennzeichenfeld fuer "Frage Loesen" wird auf FALSE gesetzt.
                 */
                m_ui_knz_loesen_moeglich = false;

                /*
                 * Das Kennzeichenfeld fuer den "50 % Joker" wird auf FALSE gesetzt.
                 */
                m_ui_knz_50_50_joker_moeglich = false;

                /*
                 * Fuer jede UI-Position wird bei der Frage nachgefragt, ob die 
                 * Antwort an der UI-Position korrekt ist.
                 *
                 * Mit dem Ergebnis der Funktion, wird die Funktion fuer das 
                 * Markieren der UI-Antworten aufgerufen, welche dann die 
                 * Antworten farbig markiert und die Checkbox auswaehlt.
                 */
                anwUiCheckAntwort( m_aktuelle_frage.getUiPositionAntwort1Korrekt(), m_optAntwort1, m_lblAntwort1 );
                anwUiCheckAntwort( m_aktuelle_frage.getUiPositionAntwort2Korrekt(), m_optAntwort2, m_lblAntwort2 );
                anwUiCheckAntwort( m_aktuelle_frage.getUiPositionAntwort3Korrekt(), m_optAntwort3, m_lblAntwort3 );
                anwUiCheckAntwort( m_aktuelle_frage.getUiPositionAntwort4Korrekt(), m_optAntwort4, m_lblAntwort4 );
                anwUiCheckAntwort( m_aktuelle_frage.getUiPositionAntwort5Korrekt(), m_optAntwort5, m_lblAntwort5 );
                anwUiCheckAntwort( m_aktuelle_frage.getUiPositionAntwort6Korrekt(), m_optAntwort6, m_lblAntwort6 );
                anwUiCheckAntwort( m_aktuelle_frage.getUiPositionAntwort7Korrekt(), m_optAntwort7, m_lblAntwort7 );
                anwUiCheckAntwort( m_aktuelle_frage.getUiPositionAntwort8Korrekt(), m_optAntwort8, m_lblAntwort8 );

                /*
                 * Der Button "OK" bekommt den Text "naechste Frage", da beim naechsten 
                 * Betaetigen dieses Schalters zur naechsten Frage verzweigt wird.
                 */
                m_btnOK.Text = "naechste Frage";

                /*
                 * Die Buttons "Loesen" und "50 % Joker" werden disabled.
                 * 
                 * Die Funktionen stehen nach dem Loesen der Frage nicht 
                 * mehr zur Verfuegung. 
                 *
                 * Diese Buttons werden bei der verzweigung zu naechsten 
                 * Frage wieder freigeschaltet.
                 */
                m_btn5050Joker.Enabled = false;

                m_btnFrageLoesen.Enabled = false;

                /*
                 * Der Button "OK" hat in diesem Moment eine neue Funktion bekommen.
                 * 
                 * Der Modus ist "Abfragen" und die Frage wurde geloest. 
                 *
                 * Beim naechsten Betaeigen des Buttons "OK" soll zur naechsten 
                 * Frage verzweigt werden. Dafuer ist die Kennzeichenvariable 
                 * "m_knz_btn_ok_ist_naechste_frage" zustaendig.
                 * 
                 * Diese Variable wird hier auf TRUE gestellt.
                 */
                m_knz_btn_ok_ist_naechste_frage = true;

                /*
                 * Die Frage wurde geloest. Es muss verhindert werden, dass 
                 * bei einem Mouse-Move-Ereignis die Antwortfelder farbig
                 * hervorgehoben werden. Dafuer ist die Kennzeichenvariable 
                 * "m_ui_knz_farbmarkierung_antworten" zustaendig.
                 * 
                 * Diese Variable wird hier auf FALSE gestellt.
                 */
                m_ui_knz_farbmarkierung_antworten = false;
            }
        }

        /*
         * ################################################################################
         */
        private void anwUi5050Joker()
        {
            m_ui_knz_50_50_joker_durchgefuehrt = true;
            m_ui_knz_50_50_joker_moeglich = true;
            /*
             * Pruefung: aktuelle Frage gleich "null" ?
             * 
             * Ist keine aktuelle Frage vorhanden, wird diese Funktion 
             * gleich wieder verlassen.
             */
            if ( m_aktuelle_frage == null )
            {
                return;
            }

            /*
             * Pruefung: 50-Prozent Joker noch moeglich ?
             * 
             * Die Ausblendung erfolgt nur, wenn die Varibale "m_ui_knz_50_50_joker_moeglich" auf TRUE steht.
             * 
             */
            if ( m_ui_knz_50_50_joker_moeglich )
            {
                /* 
                 * Ausblendreihenfolge
                 * 
                 * Es werden nur die falschen Antworten betrachtet.
                 * 
                 * Die korrekten Antworten sollen fuer eine Auswahl freigeschaltet bleiben.
                 * Daher werden korrekte Antworten nicht beruecksichtigt.
                 * 
                 * Es gibt einen Ausblendstring: 
                 * 
                 * Der Ausblendstring hat soviele Zeichen, wie es falsche Antworten gibt.
                 * 
                 * Der Ausblendstring bestimt die Ausblendreihenfolge
                 * 
                 * Der Ausblendstring wird per Zufall umgestellt.
                 * 
                 * Fuer jede falsche Antwort gibt es ein Zeichen ("1" oder "0").
                 *
                 * Eine "1" zeigt an, das die Antwort ausgeblendet werden soll.
                 * Eine "0" zeigt an, das die Antwort fuer die Auswahl freigeschaltet bleibt.
                 * 
                 * Bei einer ungeraden Zahl der falschen Antworten, wird die Anzahl der 
                 * auszublendenden Antworten um 1 erhoeht. 
                 * (Ausnahme bei einer falschen Antwort)
                 * 
                 * Damit werden die 50-Prozent der wegzublendenden Antworten gesteuert.
                 * 
                 * In dieser Version werden nur Ausblendungen gemacht, wenn die Anzahl
                 * der falschen Antworten zwischen 2 und 7 liegt.
                 * 
                 *    1 falsche Antwort   = 0        = 0 Antworten raus
                 *    2 falsche Antworten = 10       = 1 Antwort raus
                 *    3 falsche Antworten = 110      = 2 Antwort raus
                 *    4 falsche Antworten = 1100     = 2 Antworten raus
                 *    5 falsche Antworten = 11100    = 3 Antworten raus
                 *    6 falsche Antworten = 111000   = 3 Antworten raus
                 *    7 falsche Antworten = 1111000  = 4 Antworten raus
                 * 
                 * Der Ausblendstring ist initial "null". 
                 * 
                 * Der Ausblendstring wird nur gesetzt, wenn es Antworten zum ausblenden gibt.
                 * 
                 * Ist der Ausblendstring nach der Ermittlung immer noch "null", wird 
                 * keine Ausblendung von Antworten gemacht.
                 */

                /*  
                 * Von der aktuellen Frage, wird die Anzahl der falschen Antworten ermittelt.
                 */
                int anzahl_falsche_antworten = m_aktuelle_frage.getUiAnzahlVorhandeneAntworten() - m_aktuelle_frage.getUiAnzahlKorrekteAntworten();

                /*  
                 * Mit dem Wert der falschen Antworten, wird der Ausblendstring ermittelt.
                 */
                String ausblend_str = null;

                     if ( anzahl_falsche_antworten == 2 ) { ausblend_str = "10";      }
                else if ( anzahl_falsche_antworten == 3 ) { ausblend_str = "110";     }
                else if ( anzahl_falsche_antworten == 4 ) { ausblend_str = "1100";    }
                else if ( anzahl_falsche_antworten == 5 ) { ausblend_str = "11100";   }
                else if ( anzahl_falsche_antworten == 6 ) { ausblend_str = "111000";  }
                else if ( anzahl_falsche_antworten == 7 ) { ausblend_str = "1111000"; }

                /*  
                 * Pruefung: Ausblendstring gleich "null" ?
                 * 
                 * Ist der Ausblendstring nach der Ermittlung immer noch "null", wird 
                 * keine Ausblendung von UI-Elementen gemacht.
                 * 
                 * Ist der Ausblendstring gesetzt, wird in dieser Funktion weitergemacht.
                 */
                if (ausblend_str == null )
                {
                    m_lblStatusAnzeige1.Text = "Anzahl falsche Antworten = " + anzahl_falsche_antworten + " = Ausblend Str. " + ausblend_str + " = es gibt nichts zum ausblenden";
                }
                else
                {
                    /* 
                     * Der Ausblendstring wird per Zufall umgestellt.
                     */
                    ausblend_str = fkString.getRandomUmgestellt(20, ausblend_str);

                    m_lblStatusAnzeige1.Text = "Anzahl falsche Antworten = " + anzahl_falsche_antworten + " = Ausblend Str. " + ausblend_str;

                    /* 
                     * Ueber 8 boolsche Variablen wird die Ausblendung der Antworten gesteuert.
                     * Die Variablen werden mit "FALSE" ( =Antwort bleibt auswaehlbar ) initialisiert.
                     */
                    bool knz_antwort_a_raus = false;
                    bool knz_antwort_b_raus = false;
                    bool knz_antwort_c_raus = false;
                    bool knz_antwort_d_raus = false;
                    bool knz_antwort_e_raus = false;
                    bool knz_antwort_f_raus = false;
                    bool knz_antwort_g_raus = false;
                    bool knz_antwort_h_raus = false;

                    /* 
                     * Die Variable "index_ausblend_string" gibt die derzeitige Position im
                     * Ausblendmuster an und bekommt eine 1 als Startwert.
                     */
                    int index_ausblend_string = 0;

                    /* 
                     * Auszublendende Antworten ermitteln
                     *
                     * Es werden nur die Antworten bei sichtbaren UI-Checkboxen beruecksichtigt.
                     *
                     * Die If-Abfrage sorgt dafuer, dass nur die falschen Antworten beruecksichtigt werden. 
                     *
                     * Handelt es sich um eine falsche Antwort, wird im Ausblendstrings nachgesehen, ob die 
                     * Antwort ausgeblendet werden soll. Das ist der Fall, wenn an der akutellen Position 
                     * des Ausblendstrings eine 1 steht. 
                     *
                     * Der Index fuer den Ausblendstring wird bei jeder sichtbaren falschen Antwort um
                     * eine Position weitergestellt.
                     */
                    if ( m_optAntwort1.Visible )
                    {
                        if ( m_aktuelle_frage.getUiPositionAntwort1Korrekt() == false )
                        {
                            knz_antwort_a_raus = ausblend_str[ index_ausblend_string ] == '1';

                            index_ausblend_string++;
                        }
                    }

                    if ( m_optAntwort2.Visible )
                    {
                        if ( m_aktuelle_frage.getUiPositionAntwort2Korrekt() == false )
                        {
                            knz_antwort_b_raus = ausblend_str[ index_ausblend_string ] == '1';

                            index_ausblend_string++;
                        }
                    }

                    if ( m_optAntwort3.Visible )
                    {
                        if ( m_aktuelle_frage.getUiPositionAntwort3Korrekt() == false )
                        {
                            knz_antwort_c_raus = ausblend_str[ index_ausblend_string ] == '1';

                            index_ausblend_string++;
                        }
                    }

                    if ( m_optAntwort4.Visible )
                    {
                        if ( m_aktuelle_frage.getUiPositionAntwort4Korrekt() == false )
                        {
                            knz_antwort_d_raus = ausblend_str[ index_ausblend_string ] == '1';

                            index_ausblend_string++;
                        }
                    }

                    if ( m_optAntwort5.Visible )
                    {
                        if ( m_aktuelle_frage.getUiPositionAntwort5Korrekt() == false )
                        {
                            knz_antwort_e_raus = ausblend_str[ index_ausblend_string ] == '1';

                            index_ausblend_string++;
                        }
                    }

                    if ( m_optAntwort6.Visible )
                    {
                        if ( m_aktuelle_frage.getUiPositionAntwort6Korrekt() == false )
                        {
                            knz_antwort_f_raus = ausblend_str[ index_ausblend_string ] == '1';

                            index_ausblend_string++;
                        }
                    }

                    if ( m_optAntwort7.Visible )
                    {
                        if ( m_aktuelle_frage.getUiPositionAntwort7Korrekt() == false )
                        {
                            knz_antwort_g_raus = ausblend_str[ index_ausblend_string ] == '1';

                            index_ausblend_string++;
                        }
                    }

                    if ( m_optAntwort8.Visible )
                    {
                        if ( m_aktuelle_frage.getUiPositionAntwort8Korrekt() == false )
                        {
                            knz_antwort_h_raus = ausblend_str[ index_ausblend_string ] == '1';

                            index_ausblend_string++;
                        }
                    }

                    /* 
                     * Zum Abschluss werden die Antworten entweder aus- oder eingeblendet.
                     */
                    m_optAntwort1.Enabled = !knz_antwort_a_raus;
                    m_optAntwort2.Enabled = !knz_antwort_b_raus;
                    m_optAntwort3.Enabled = !knz_antwort_c_raus;
                    m_optAntwort4.Enabled = !knz_antwort_d_raus;
                    m_optAntwort5.Enabled = !knz_antwort_e_raus;
                    m_optAntwort6.Enabled = !knz_antwort_f_raus;
                    m_optAntwort7.Enabled = !knz_antwort_g_raus;
                    m_optAntwort8.Enabled = !knz_antwort_h_raus;

                    m_lblAntwort1.Enabled = !knz_antwort_a_raus;
                    m_lblAntwort2.Enabled = !knz_antwort_b_raus;
                    m_lblAntwort3.Enabled = !knz_antwort_c_raus;
                    m_lblAntwort4.Enabled = !knz_antwort_d_raus;
                    m_lblAntwort5.Enabled = !knz_antwort_e_raus;
                    m_lblAntwort6.Enabled = !knz_antwort_f_raus;
                    m_lblAntwort7.Enabled = !knz_antwort_g_raus;
                    m_lblAntwort8.Enabled = !knz_antwort_h_raus;

                    /*
                     * Kennzeichenvariable fuer den 50%-Joker mit dem Wert aus der 
                     * Variable "m_ui_knz_50_50_joker_immer_moeglich" setzen. 
                     * 
                     * Ist diese Variable TRUE, ist der 50%-Joker immer moeglich.
                     * Ist diese Variable FALSE, ist der 50%-Joker nur einmal moeglich.
                     */
                    m_ui_knz_50_50_joker_moeglich = m_ui_knz_50_50_joker_immer_moeglich;

                    /*
                     * Der 50%-Joker-Button wird entsprechend dem Wert aus der 
                     * Variablen "m_ui_knz_50_50_joker_moeglich" frei- oder 
                     * weggeschaltet.
                     */
                    m_btn5050Joker.Enabled = m_ui_knz_50_50_joker_moeglich;
                }

            }
        }

        /* 
         * ################################################################################
         */
        private void anwSchriftAuswahl()
        {
            String ini_font_name = "Arial";

            bool ini_font_knz_bold = false;

            float ini_font_groesse = 11;

            ini_font_name = fkIniDatei.readIniString( "OPTIONEN", "FONT_NAME", "Arial" );

            ini_font_knz_bold = ( fkIniDatei.readIniString( "OPTIONEN", "FONT_KNZ_BOLD", "FALSE" ) ).ToUpper() == "TRUE";

            ini_font_groesse = float.Parse( fkIniDatei.readIniString( "OPTIONEN", "FONT_SIZE", "11" ) );

            FontDialog font_dialog = new FontDialog()
            {
                ShowColor = false,
                MinSize = 8,
                MaxSize = 30,
            };

            font_dialog.Font = new Font( ini_font_name, ini_font_groesse, ( ini_font_knz_bold ? FontStyle.Bold : FontStyle.Regular ) );

            if ( font_dialog.ShowDialog() == DialogResult.OK )
            {
                Font label_font = font_dialog.Font;

                fkIniDatei.writeIniString( "OPTIONEN", "FONT_NAME", label_font.Name );

                fkIniDatei.writeIniString( "OPTIONEN", "FONT_SIZE", "" + label_font.Size );

                fkIniDatei.writeIniString( "OPTIONEN", "FONT_KNZ_BOLD", "" + label_font.Bold );

                anwSchriftToControls( label_font );
            }

            font_dialog = null;
        }

        /* 
         * ################################################################################
         */
        private void anwSchriftAusIni()
        {
            String ini_font_name = "Arial";
            bool ini_font_knz_bold = false;
            float ini_font_groesse = 11;

            ini_font_name = fkIniDatei.readIniString( "OPTIONEN", "FONT_NAME", "Arial" );

            ini_font_knz_bold = ( fkIniDatei.readIniString( "OPTIONEN", "FONT_KNZ_BOLD", "FALSE" ) ).ToUpper() == "TRUE";

            ini_font_groesse = fkIniDatei.reatIniInt( "OPTIONEN", "FONT_SIZE", 11 );

            Font label_font = new Font( ini_font_name, ini_font_groesse, ( ini_font_knz_bold ? FontStyle.Bold : FontStyle.Regular ) );

            anwSchriftToControls( label_font );
        }

        /* 
         * ################################################################################
         */
        private void anwSchriftToControls( Font label_font )
        {
            m_lblFrage1.Font = label_font;
            m_lblFrage2.Font = label_font;
            m_lblFrage2a.Font = label_font;
            m_lblFrageNr.Font = label_font;
            m_frameFragen.Font = label_font;
            m_lblAntwort1.Font = label_font;
            m_lblAntwort2.Font = label_font;
            m_lblAntwort3.Font = label_font;
            m_lblAntwort4.Font = label_font;
            m_lblAntwort5.Font = label_font;
            m_lblAntwort6.Font = label_font;
            m_lblAntwort7.Font = label_font;
            m_lblAntwort8.Font = label_font;
            m_optAntwort1.Font = label_font;
            m_optAntwort2.Font = label_font;
            m_optAntwort3.Font = label_font;
            m_optAntwort4.Font = label_font;
            m_optAntwort5.Font = label_font;
            m_optAntwort6.Font = label_font;
            m_optAntwort7.Font = label_font;
            m_optAntwort8.Font = label_font;
        }


        /*
         * ################################################################################
         */
        private void anwUiAntwortToGrau( CheckBox pAntwortCheckBox, Label pAntwortLabel )
        {
            pAntwortLabel.BackColor = FARBE_GRAU;

            pAntwortCheckBox.BackColor = FARBE_GRAU;

            pAntwortLabel.ForeColor = FARBE_TEXT_ANTWORT_ENABLED;

            pAntwortCheckBox.ForeColor = FARBE_TEXT_ANTWORT_ENABLED;
        }

        /* 
         * ################################################################################
         */
        private void anwSetAntwortenToGrau()
        {
            anwUiAntwortToGrau( m_optAntwort1, m_lblAntwort1 );
            anwUiAntwortToGrau( m_optAntwort2, m_lblAntwort2 );
            anwUiAntwortToGrau( m_optAntwort3, m_lblAntwort3 );
            anwUiAntwortToGrau( m_optAntwort4, m_lblAntwort4 );
            anwUiAntwortToGrau( m_optAntwort5, m_lblAntwort5 );
            anwUiAntwortToGrau( m_optAntwort6, m_lblAntwort6 );
            anwUiAntwortToGrau( m_optAntwort7, m_lblAntwort7 );
            anwUiAntwortToGrau( m_optAntwort8, m_lblAntwort8 );
         }

        /* 
         * ################################################################################
         */
        public void anwReadKennzeichenAusIni()
        {
            m_knz_fragen_nummer_anzeigen = fkIniDatei.readIniBoolean( "OPTIONEN", "KNZ_FRAGEN_NUMMER_ANZEIGEN", true );

            m_knz_fragen_text_anzeigen = fkIniDatei.readIniBoolean( "OPTIONEN", "KNZ_FRAGEN_TEXT_ANZEIGEN", true );
            
            m_knz_antwort_text_korrekt_anzeigen = fkIniDatei.readIniBoolean( "OPTIONEN", "KNZ_ANTWORT_TEXT_KORREKT_ANZEIGEN", true );
            
            m_knz_antwort_text_falsch_anzeigen = fkIniDatei.readIniBoolean( "OPTIONEN", "KNZ_ANTWORT_TEXT_FALSCH_ANZEIGEN", true );

            m_knz_korrekte_antwort_anzeigen = fkIniDatei.readIniBoolean( "OPTIONEN", "KNZ_KORREKTE_ANTWORT_ANZEIGEN", true );
            
            m_knz_rechts_click_ist_weiter = fkIniDatei.readIniBoolean( "OPTIONEN", "KNZ_RECHTS_CLICK_IST_WEITER", true );
            
            m_knz_antwort_bezeichnung_anzeigen = fkIniDatei.readIniBoolean( "OPTIONEN", "KNZ_ANTWORT_BEZEICHNUNG_ANZEIGEN", true );
            
            m_knz_antwort_reihenfolge_umstellen = fkIniDatei.readIniBoolean( "OPTIONEN", "KNZ_ANTWORT_REIHENFOLGE_UMSTELLEN", false );

            m_ui_knz_50_50_joker_immer_moeglich = fkIniDatei.readIniBoolean("OPTIONEN", "KNZ_50_PROZENT_JOKER_IMMER_MOEGLICH", true);
        }

        /* 
         * Gibt die Resourcen frei, beendet das Fenster aber nicht.
         */
        private void anwEnde()
        {
            /*
             * Pruefung: Anwendungsinstanz vorhanden?
             * 
             * Ist die Anwendungsinstanz vorhanden, wird dort die Funktion 
             * "Clear" aufgerufen, um die Resourcen freizugeben.
             */
            if ( m_anw_fragen_frager != null )
            {
                m_anw_fragen_frager.clear();
            }

            /*
             * Es werden die Instanz der Anwendung und der aktuellen Frage 
             * auf "null" gesetzt. 
             */
            m_anw_fragen_frager = null;

            m_aktuelle_frage = null;
        }

        /* 
         * ################################################################################
         */
        private void m_btnTestDialog_Click( object sender, EventArgs e )
        {
            anwStartLernsitzungDialogAuswahl();
        }

        /* 
         * ################################################################################
         */
        private void anwStartLernsitzungDialogAuswahl()
        {
            chFFAuswahlFragen my_dialog = new chFFAuswahlFragen();

            my_dialog.setDialogNummerAb( fkIniDatei.readIniString( "DIALOG_FRAGENAUSWAHL", "NUMMER_AB", "1" ) );

            my_dialog.setDialogAnzahlFragen(  fkIniDatei.readIniString( "DIALOG_FRAGENAUSWAHL", "ANZAHL_FRAGEN_TEXT", "30" ) );
            
            my_dialog.setMaxAnzahlFragen( getAnwFragenFrager().getAnzahlFragen() );

            my_dialog.ShowDialog();

            this.BringToFront();

            if ( my_dialog.isOK() )
            {
                int idialog_anzahl_fragen = my_dialog.getAnzahlFragen();

                if ( idialog_anzahl_fragen > 0 )
                {
                    int idialog_nummer_ab = my_dialog.getDialogNummerAb();

                    if ( idialog_nummer_ab > getAnwFragenFrager().getAnzahlFragen() )
                    {
                        idialog_nummer_ab = 1;
                    }

                    anwStartAbfrageModus( true, idialog_anzahl_fragen, idialog_nummer_ab, -1 );

                    fkIniDatei.writeIniString( "DIALOG_FRAGENAUSWAHL", "NUMMER_AB", "" + idialog_nummer_ab );
                    fkIniDatei.writeIniString( "DIALOG_FRAGENAUSWAHL", "ANZAHL_FRAGEN_TEXT", "" + idialog_anzahl_fragen );
                }
            }
            //if ( my_dialog.getDialogStatusBeendetMitOK ) {

            //    dialog_nummer_ab = my_dialog.getDialogNummerAb();
            //    dialog_anzahl_fragen = my_dialog.getDialogAnzahlFragen();
            //    dialog_rb_auswahl = my_dialog.getRadioButtonAuswahl();

            //    writeToIni( "von_bis_dialog", "dialog_nummer_ab", dialog_nummer_ab );
            //    writeToIni( "von_bis_dialog", "dialog_anzahl_fragen", dialog_anzahl_fragen );
            //    writeToIni( "von_bis_dialog", "dialog_rb_auswahl", dialog_rb_auswahl );

            //    if ( dialog_rb_auswahl == "-1" ) {
            //        anwStartAbfrageModus( false, CLng( dialog_anzahl_fragen ), CLng( dialog_nummer_ab ), CLng( dialog_nummer_ab ) + CLng( dialog_anzahl_fragen ) );
            //    } else {
            //        anwStartAbfrageModus( false, CLng( dialog_rb_auswahl ), CLng( dialog_nummer_ab ), CLng( dialog_nummer_ab ) + CLng( dialog_rb_auswahl ) );
            //    }

            //}

            //EndFunktion:

            //    //On Error R esume } //

            //    Unload my_dialog

            my_dialog.Dispose();

            my_dialog = null;

            //    return;

            //errAnwStartLernsitzungDialogAuswahl:

            //    MsgBox Error + " ( Nr. " + CStr( Err ) + " )" + Chr( 13 ) + "Fehler in errAnwStartLernsitzungDialogAuswahl"

            //    //Resume EndFunktion

        }

        private void m_mnuOptionenKnz50JokerImmer_Click( object sender, EventArgs e )
        {
            m_ui_knz_50_50_joker_immer_moeglich = !m_ui_knz_50_50_joker_immer_moeglich;

            m_mnuOptionenKnz50JokerImmer.Checked = m_ui_knz_50_50_joker_immer_moeglich;

            /*
             * Wurde der 50%-Joker durchgefuehrt und die Einstellung ist, 
             * dass der 50%-Joker nur einmal gemacht werden darf, muss der 
             * Button fuer den Joker ausgeblendet werden.
             */
            if ( ( m_ui_knz_50_50_joker_durchgefuehrt ) && ( m_ui_knz_50_50_joker_immer_moeglich == false ) )
            {
                m_btn5050Joker.Enabled = false;
            }
            else
            {
                m_btn5050Joker.Enabled = m_ui_knz_50_50_joker_moeglich;
            }

            fkIniDatei.writeIniBoolean( "OPTIONEN", "KNZ_50_PROZENT_JOKER_IMMER_MOEGLICH", m_ui_knz_50_50_joker_immer_moeglich );
        }

        private void anwSetAntwortReduzierungFKatalog( int  pAnzahlFalscheAntwortenJeKorrekterAntwort )
        {
            if ( m_anw_fragen_frager != null )
            {
                m_anw_fragen_frager.startAntwortReduktion( pAnzahlFalscheAntwortenJeKorrekterAntwort );
      
                if ( m_aktuelle_frage != null ) 
                {
                    anwNeueFrageAnzeigen( ANZEIGE_AKTUELLE_FRAGE );

                   // anwUiSetStatusText( m_aktuelle_frage.getUiAktivString() );
                }
            }
        }


        private void anwStartFragenUmstellung()
        {
            if ( m_anw_fragen_frager != null )
            {
                m_anw_fragen_frager.startFragenUmstellung();
      
                if ( m_aktuelle_frage != null ) 
                {
                    anwNeueFrageAnzeigen( ANZEIGE_ERSTE_FRAGE );

                   // anwUiSetStatusText( m_aktuelle_frage.getUiAktivString() );
                }
            }
        }

        private void m_mnuReduzierung1_Click(object sender, EventArgs e)
        {
            anwSetAntwortReduzierungFKatalog( 1 );
        }

        private void m_mnuReduzierung2_Click(object sender, EventArgs e)
        {
            anwSetAntwortReduzierungFKatalog( 2 );
        }

        private void m_mnuReduzierung3_Click(object sender, EventArgs e)
        {
            anwSetAntwortReduzierungFKatalog( 3 );
        }

        private void m_mnuReduzierungAus_Click(object sender, EventArgs e)
        {
            anwSetAntwortReduzierungFKatalog( 8 );
        }

        private void m_mnuDateiFragenUmstellen_Click(object sender, EventArgs e)
        {
            anwStartFragenUmstellung();
        }

        private void m_mnuDateiXmlHinzufuegen_Click(object sender, EventArgs e)
        {
            m_lblGesamteFragen.Text = " --- ";

            /* 
             * Letzter Dateiname
             * Aus der INI-Datei wird der Name der zuletzt geoefnneten XML-Datei geladen.
             * Dieser Name erscheint dann als Vorauswahl in der Dialog-Box.
             */
            String datei_name = fkIniDatei.readIniDateiName( "DATEI_NAME_XML_FRAGENKATALOG" );

            /* 
             * Dateifilter
             * Die zur Auswahl stehenden Datei-Erweiterungen werden als String initialisiert.
             */
            String datei_filter = "TXT-Datei ( *.txt )\0*.txt\0XML-Datei ( *.xml )\0*.xml\0alle Dateien ( *.* )\0*.*\0";

            /* 
             * Aufruf der Dateiauswahl-Dialog-Box
             */
            datei_name = fkCommonDialog.getOpenName( datei_filter, "txt", "c:\\", datei_name, "Fragenkatalog laden" );

            /* 
             * Der Status-Text wird auf "Laden" gesetzt
             */
            m_lblStatusAnzeige1.Text = "Lade: " + datei_name;

            Application.DoEvents();

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                /* 
                 * Pruefung: Wurde eine Datei ausgewaehlt?
                 * Wurde keine Datei ausgewaehlt, ist der Dateiname ein Leerstring.
                 * Wurde eine Datei ausgewaehlt, ist der Dateiname gesetzt.
                 */
                if ( datei_name != null )
                {
                    if ( getAnwFragenFrager().ladeXmlFragenKatalog( datei_name, false ) )
                    {
                        /* 
                         * Konnte die Datei eingelesen werden, wird der Dateiname in der INI-Datei gespeichert
                         */
                        fkIniDatei.writeIniDateiName( "DATEI_NAME_XML_FRAGENKATALOG", datei_name );

                        m_lblGesamteFragen.Text = getAnwFragenFrager().getAnzahlLabel();

                        anwNeueFrageAnzeigen( ANZEIGE_ERSTE_FRAGE );
                    }
                    else
                    {
                        MessageBox.Show( "Die XML-Datei \"" + datei_name + "\" konnte nicht geladen werden!", "Fehler Laden" );
                    }
                }
            }
            catch ( Exception err_inst )
            {
                Console.WriteLine("Fehler: errXmlDateiLaden\n" + err_inst.Message);// + "" + err_inst.StackTrace );
            }

            Cursor.Current = Cursors.Default;

            m_lblStatusAnzeige1.Text = "";
        }
    }
}
