#region License

//The MIT License (MIT)

//Copyright (c) 2014 Konrad Huber

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

#endregion

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