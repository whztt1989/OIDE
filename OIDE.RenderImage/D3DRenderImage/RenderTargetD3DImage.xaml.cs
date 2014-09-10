using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OIDE.InteropEditor.DLL;

namespace OIDE.RenderImage.D3DRenderImage
{
    /// <summary>
    /// Interaktionslogik für RenderTargetD3DImage.xaml
    /// </summary>
    public partial class RenderTargetD3DImage : UserControl
    {
        public RenderTargetD3DImage()
        {
            InitializeComponent();

           // Window yourParentWindow = Window.GetWindow(this);
        //    HwndSource source = (HwndSource)HwndSource.FromWindow.GetWindow(this)Visual(Window.GetWindow(this));
         //   Window window = Window.GetWindow(this);

        

        }

        public void init()
        {
            IntPtr hWnd = IntPtr.Zero;

            foreach (PresentationSource source in PresentationSource.CurrentSources)
            {
                var hwndSource = (source as HwndSource);
                if (hwndSource != null)
                {
                    hWnd = hwndSource.Handle;
                    break;
                }
            }

            if (hWnd == IntPtr.Zero)
            {
                throw new Exception("Failed to get hWnd from PresentationSource.CurrentSources.");
            }

            // IntPtr windowHandle = new WindowInteropHelper(window).Handle;

            //  IntPtr hwnd = (PresentationSource.FromVisual(Window.GetWindow(this)) as HwndSource).Handle;
            // // IntPtr hwnd = source.Handle;
            //  bool test =
            int w = (int)this.ActualWidth;
            int h = (int)this.ActualHeight;
          
            IntPtr ddbackbuffer = DLL_Singleton.Instance.stateInit("",w, h);  // DDBACKBUFFER

            RenderImage.Source = new D3DRenderImage(ddbackbuffer);
        }

        private void RenderImage_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RenderImage_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void RenderImage_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void RenderImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void RenderImage_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void RenderImage_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void RenderImage_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void RenderImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DLL_Singleton.Instance.RenderTargetSize("", e.NewSize.Width, e.NewSize.Height);
        }


    }
}
