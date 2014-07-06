using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace OIDE.InteropEditor.DLL
{
    public class OFS_Interop
    {
        [DllImport(@"D:\Projekte\Src Game\_Engine\XEngine\build\VS2010\XEngine\XEALL\Debug\EditorI.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern bool stateInit(IntPtr hwnd);
       
    }
}
