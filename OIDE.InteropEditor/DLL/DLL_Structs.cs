using System;
using System.Runtime.InteropServices;

namespace OIDE.InteropEditor.DLL
{
   
    /// <summary>
    /// Struktur für Daten die zur Medieninitialisierung benötigt werden
    /// </summary>
    public struct CHIP_INIT
    {
      //  public CHIP_INIT(Int32 ID = 0)
       // {
         //   ucNoKnr1 = new byte[(int)MAXSize.MAX_CARDTYPE];       

      //  }

      //  public byte ucInitTyp;                  // Initialisierungsvariante:
                                                //  0 = kein Init, 1 = Init mit Auslesen Kartennr,
                                                //  2 = Init mit fortlaufender Kartennummer, 3 = Init mit Abfrage Unikatsnummer
    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)MAXSize.MAX_CARDTYPE)]
     //   public byte[] ucNoKnr1;                 // Blocknr/Segmentnr/Filenr Kartennummer Teil 1 (0...

    }

    
    /// <summary>
    /// Struktur für Chip Daten
    /// </summary>
    public struct CHIP_DATA
    {
        /// <summary>
        /// Initialisierungs Konstruktor
        /// </summary>
        /// <param name="ID">Chipdata ID</param>
        public CHIP_DATA(Int32 ID)
        {
    //         ui64Kartennummer = 0;  //(nur 9 Stellen verwenden, um Zahl als long zu speichern     
        }

     //   [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)MAXSize.MAX_char_SNR)]
    //    public byte[] ucSeriennummer; //MAXSize.MAX_char_SNR];
    };
}