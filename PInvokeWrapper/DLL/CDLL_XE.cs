using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace PInvokeWrapper.DLL
{
    public enum Status
    {
        Unknown,
        OK,
        Error,
    }

    public enum ObjType
    {
        Physic = 0,
	    GameEntity = 1,
    }

    public class DLL_Singleton : CDLL_Loader
    {
         private static DLL_Singleton instance;

         public static DLL_Singleton Instance
           {
              get 
              {
                 if (instance == null)
                 {
                     instance = new DLL_Singleton(@"D:\Projekte\Src Game\_Engine\XEngine\build\VS2010\XEngine\XEALL\Debug\EditorI.dll");
                  }
                 return instance;
              }
           }

        private IntPtr DLLPtr;

      //  public delegate bool StartStateDelegate(IntPtr hwnd);

        public delegate bool stateInitDelegate(IntPtr hwnd);
        public delegate bool stateUpdateDelegate();
        public delegate void quitDelegate();
        public delegate int updateObjectDelegate(uint id, int type);
        public delegate void consoleCmdDelegate(String command);

      //  public delegate void ChipAuthent1Delegate(byte bReaderID, byte[] ucEncData);
      //  public delegate byte ChipAuthent2Delegate(byte bReaderID, byte[] ucEncData);
      //  public delegate void ChipSetLogDelegate(byte bReaderID, byte bLog, StringBuilder pLgDir);
      //  public delegate void ChipSetParaDelegate(byte bReaderID, byte[] ucPara, int iParaLen);
      //  public delegate int ChipKarteDaDelegate(byte bReaderID);
     
      //  public delegate int ChipLoadPecuKeysDelegate(byte bReaderID,StringBuilder ucPara, int iParaLen);
      ////  public delegate int ChipIsPecuDelegate(byte bReaderID,byte bIsPecu, CHIP_INIT ciData,  int iStatus, int iFremdKartennr);
      //  public delegate int ChipIsPecuDelegate(byte bReaderID, ref byte bIsPecu, CHIP_INIT ciData, ref CHIP_DATA cdRead);
      //  public delegate int ChipReadSnrDelegate(byte bReaderID, byte[] ucSnr, ref int iSerNrLen);
      //  public delegate int ChipReadDataDelegate(byte bReaderID,ref CHIP_DATA cdRead);
      ////  public delegate int ChipReadTransDelegate(byte bReaderID,int iTransnr,ref TRANSAKTION trTrans);
      //  public delegate int ChipInitPecuDelegate(byte bReaderID, CHIP_INIT ciData, ref CHIP_DATA cdWrite);
      //  public delegate int ChipWriteDataDelegate(byte bReaderID,ref CHIP_DATA cdWrite);

      //  public delegate int ChipWriteBlockDelegate(byte bReaderID,char ucBlocknr, char ucKey, StringBuilder ucData);

      //  public delegate void ChipDestroyDelegate(byte bReaderID);

      //  public StartStateDelegate StartState { get; set; }
        public stateInitDelegate stateInit { get; set; }
        public stateUpdateDelegate stateUpdate { get; set; }
        public quitDelegate quit { get; set; }
        public updateObjectDelegate updateObject { get; set; }
        public consoleCmdDelegate consoleCmd { get; set; }
       //public ChipAuthent1Delegate ChipAuthent1 { get; set; }
        //public ChipAuthent2Delegate ChipAuthent2 { get; set; }
        //public ChipSetLogDelegate ChipSetLog { get; set; }
        //public ChipSetParaDelegate ChipSetPara { get; set; }
        //public ChipKarteDaDelegate ChipKarteDa { get; set; }


     //   public ChipDestroyDelegate ChipDestroy { get; set; }

      //  [DllImport(@"D:\Projekte\Src Game\_Engine\XEngine\build\VS2010\XEngine\XEALL\Debug\EditorI.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
      //  public static extern bool startState(IntPtr hwnd);

        //[DllImport(@"D:\Projekte\Src Game\_Engine\XEngine\build\VS2010\XEngine\XEALL\Debug\EditorI.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        //public static extern bool stateInit(IntPtr hwnd);
        //[DllImport(@"D:\Projekte\Src Game\_Engine\XEngine\build\VS2010\XEngine\XEALL\Debug\EditorI.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        //public static extern bool stateUpdate();
        //[DllImport(@"D:\Projekte\Src Game\_Engine\XEngine\build\VS2010\XEngine\XEALL\Debug\EditorI.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        //public static extern bool quit();

        public DLL_Singleton(String dllPath)
        {
            //     CPManager.Instance.VMLog.fAddLog(Log.DLL_RFID, "Initialisiere RFID DLL ...");

            fLoadDLL(dllPath, ref DLLPtr);

            try
            {

                //   startState();
                //      CPManager.Instance.VMLog.fAddLog(Log.DLL_RFID, "Erstelle RFID DLL Methoden...");

             //   StartState = (StartStateDelegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("startState", DLLPtr), typeof(StartStateDelegate));
                stateInit = (stateInitDelegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("stateInit", DLLPtr), typeof(stateInitDelegate));
                stateUpdate = (stateUpdateDelegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("stateUpdate", DLLPtr), typeof(stateUpdateDelegate));
                quit = (quitDelegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("quit", DLLPtr), typeof(quitDelegate));
                updateObject = (updateObjectDelegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("updateObject", DLLPtr), typeof(updateObjectDelegate));
                consoleCmd = (consoleCmdDelegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("consoleCmd", DLLPtr), typeof(consoleCmdDelegate));
                
                //   ChipAuthent1 = (ChipAuthent1Delegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("ChipAuthent1", DLLPtr), typeof(ChipAuthent1Delegate));
                //ChipAuthent2 = (ChipAuthent2Delegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("ChipAuthent2", DLLPtr), typeof(ChipAuthent2Delegate));
                //ChipSetLog = (ChipSetLogDelegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("ChipSetLog", DLLPtr), typeof(ChipSetLogDelegate));
                //ChipSetPara = (ChipSetParaDelegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("ChipSetPara", DLLPtr), typeof(ChipSetParaDelegate));


                //       CPManager.Instance.VMLog.fAddLog(Log.DLL_RFID, "RFID DLL initialisiert ...");
            }
            catch (Exception ex)
            {
                //         Utility.CErrLog.fAddError("Fehler beim auslesen der RFID DLL",ex,true);
            }
        }

      /*  public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Free other state (managed objects).
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.
            try
            {
            //    if (fFreeDLL(DLLPtr) == 0) CPManager.Instance.VMLog.fAddLog(Log.DLL_RFID, "RFIDDll freigegeben.");
          //      else CPManager.Instance.VMLog.fAddFehler(Log.DLL_RFID, "Fehler bei RFIDDll freigeben.");
            }
            catch { }
        }*/
    }
}