﻿using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using SFML.Window;

namespace OIDE.InteropEditor.DLL
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

        /// <summary>
        /// true = Native Editor is initialized, false = not initialized
        /// </summary>
        public Boolean EditorInitialized { get; set; }

        public static DLL_Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DLL_Singleton(@"D:\Projekte\Src Game\_Engine\XEngine\build\VS2010\XEngine\XEALL\Debug\XE_IEditor.dll");
                }
                return instance;
            }
        }

        private IntPtr DLLPtr;

        //  public delegate bool StartStateDelegate(IntPtr hwnd);

    //        SFML_EDITOR_API unsigned char* consoleCmd(const char* command, unsigned char* data);
	
    //SFML_EDITOR_API bool stateInit(sf::WindowHandle hwnd, const char* stateName);
    //SFML_EDITOR_API bool moveToState(const char* stateName);
    //SFML_EDITOR_API bool stateUpdate();
    //SFML_EDITOR_API void quit();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr stateInitDelegate(String stateName, int width, int height);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool stateUpdateDelegate();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void quitDelegate();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int updateObjectDelegate(uint id, int type);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void commandDelegate(String command, byte[] data, int len);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PushEventDelegate(Event pEvent);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RenderTargetSizeDelegate(String rtName, double x, double y);

        //http://geekswithblogs.net/VROD/archive/2009/04/07/130805.aspx
        //-----------------------------------------------------------------------

        //delegates - define a signatures for unmanaged c++ code to call

        //-----------------------------------------------------------------------

        public delegate void MessageReceivedDelegate(

           int param1,

           string messageString,

           int param3,

           int param4,

           int param5);





        public delegate void ExceptionParsedDelegate(

          string exceptionMessage);



        //-----------------------------------------------------------------------

        // P/invoke mapping with pointers to delegated methods

        //-----------------------------------------------------------------------

        [DllImport(@"D:\Projekte\Src Game\_Engine\XEngine\build\VS2010\XEngine\XEALL\Debug\XE_IEditor.dll"
           , CallingConvention = CallingConvention.StdCall)]
        public static extern void an_unmanaged_function(
           string aa,
            int bb,
            string cc,
            string dd,
            int ee,
            int ff,
            MessageReceivedDelegate call,
            ExceptionParsedDelegate exception);









        //-----------------------------------------------------------------------

        // delegated methods (called inside unmanaged c++ code)

        //-----------------------------------------------------------------------

        public static void OnExceptionReceived(

           string exceptionMessage)
        {



            //handle exceptionMessage

        }
        public static void OnMessageReceived(

            int param1,

           string messageString,

           int param3,

           int param4,

           int param5)
        {



            //handle exceptionMessage

        }
        //protected void OnMessageParsed(myMessage theMesssage)

        //{

        //    //handle theMesssage

        //}


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
        public commandDelegate command { get; set; }
        public PushEventDelegate PushEvent { get; set; }
        public RenderTargetSizeDelegate RenderTargetSize { get; set; }
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
   
        //[DllImport(@"D:\Projekte\Src Game\_Engine\XEngine\build\VS2010\XEngine\XEALL\Debug\XE_IEditor.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        //public static extern int pushEvent(Event pushEvent);

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
                //    updateObject = (updateObjectDelegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("updateObject", DLLPtr), typeof(updateObjectDelegate));
                command = (commandDelegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("command", DLLPtr), typeof(commandDelegate));
                PushEvent = (PushEventDelegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("pushEvent", DLLPtr), typeof(PushEventDelegate));
                RenderTargetSize = (RenderTargetSizeDelegate)Marshal.GetDelegateForFunctionPointer(fLoadDLL_Fkt("renderTargetSize", DLLPtr), typeof(RenderTargetSizeDelegate));

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