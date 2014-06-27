using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PInvokeWrapper.DLL;

namespace PInvokeWrapper
{
    public class Interface
    {
     //   CDLL_XE mDllXE;

        public Interface()
        {
            //TEST----

            //-----------------------------------------------------------------------
            // perform P/invoke
            //-----------------------------------------------------------------------
            DLL_Singleton.an_unmanaged_function(
            ""
            , 2
            , "2"
            , "2"
            , 2
            , 2
            , new PInvokeWrapper.DLL.DLL_Singleton.MessageReceivedDelegate(PInvokeWrapper.DLL.DLL_Singleton.OnMessageReceived)
            , new PInvokeWrapper.DLL.DLL_Singleton.ExceptionParsedDelegate(PInvokeWrapper.DLL.DLL_Singleton.OnExceptionReceived));


            //-----------------------------------------------------------------------

// inside unmanaged c++ code ("my.dll")

//-----------------------------------------------------------------------

 

//define c++ delegate signatures

 

//typedef void (__stdcall *callbackDelegatePointer)(

//       int param1,

//       char message[2100],

//       int param3,

//       int param4,

//       int param5);

//typedef void (__stdcall *exceptionDelegatePointer)(

//       char* exceptionMessage);

 

////allow p/invoke by delcaring method “extern "C"”

//extern "C" __declspec(dllexport)

//void __stdcall an_unmanaged_function(

//       char *aa,

//       int   bb,

//       char *cc,

//       char *dd,

//       int   ee,

//       int   ff,

//       callbackDelegatePointer onMessageReceived,

//       exceptionDelegatePointer onException)

 

////call delegates

//onMessageReceived(param1, message, param3, param4, param5);

             

//onException("Hello Exception");

        }
        //public Boolean StartState(IntPtr hwnd)
        //{
        //    CDLL_Loader dllLoader = new CDLL_Loader();

        //  //  mDllXE = new CDLL_XE(@"D:\Projekte\Src Game\_Engine\XEngine\build\VS2010\XEngine\XEALL\Debug\EditorI.dll");


        //    return false;// mDllXE.StartState(hwnd);
        //}
    }
}
