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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OIDE.InteropEditor.DLL;

namespace OIDE.InteropEditor
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
            , new OIDE.InteropEditor.DLL.DLL_Singleton.MessageReceivedDelegate(OIDE.InteropEditor.DLL.DLL_Singleton.OnMessageReceived)
            , new OIDE.InteropEditor.DLL.DLL_Singleton.ExceptionParsedDelegate(OIDE.InteropEditor.DLL.DLL_Singleton.OnExceptionReceived));


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
