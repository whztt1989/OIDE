using System;
using System.Linq;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace MogreInWpf
{
    public static class MogreRootFactory
    {
        #region (Public) Parameters

        public static string ResourcesFileName = "resources.cfg";

        #endregion

        //public static void CreateRoot()
        //{           
        //    #region Get Render Window
            
        //    IntPtr hWnd = IntPtr.Zero;

        //    foreach (PresentationSource source in PresentationSource.CurrentSources)
        //    {
        //        var hwndSource = (source as HwndSource);
        //        if (hwndSource != null)
        //        {
        //            hWnd = hwndSource.Handle;
        //            break;
        //        }
        //    }

        //    if (hWnd == IntPtr.Zero) throw new Exception("Could not get hWnd");

        //    var misc = new NameValuePairList();
        //    misc["externalWindowHandle"] = hWnd.ToString();
        //    RenderWindow _renderWindow = _root.CreateRenderWindow("OgreImageSource Windows", 0, 0, false, misc);
        //    _renderWindow.IsAutoUpdated = false;

        //    #endregion
        //}

        #region Resource Loading

        private static double _currentProcess;
        private static double _resourceItemScalar;

        #endregion
    }
    public class ResourceLoadEventArgs : EventArgs
    {
        public ResourceLoadEventArgs(string name, double progress)
        {
            this.Name = name;
            this.Progress = progress;
        }

        public string Name { get; private set; }
        public double Progress { get; private set; }
    }
}
