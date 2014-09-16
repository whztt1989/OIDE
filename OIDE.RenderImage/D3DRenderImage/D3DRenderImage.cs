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
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using OIDE.InteropEditor.DLL;

using TimerClass = System.Windows.Threading.DispatcherTimer;

namespace OIDE.RenderImage.D3DRenderImage
{
    public class D3DRenderImage : D3DImage
    {
        private TimerClass _timer;

        //[StructLayout(LayoutKind.Sequential)]
        public class SIZE
        {
            public int Width = 0;
            public int Height = 0;
        }

        private IntPtr _scene;

        public D3DRenderImage(IntPtr hwnd)
        {
            _scene = hwnd;

            _timer = new TimerClass(); //test


            // create a D3DImage to host the scene and
            // monitor it for changes in front buffer availability
            // _di = new D3DImage();
            base.IsFrontBufferAvailableChanged
                += new DependencyPropertyChangedEventHandler(OnIsFrontBufferAvailableChanged);


            // make a brush of the scene available as a resource on the window
           // Resources["RotatingTriangleScene"] = new ImageBrush(this);

            // begin rendering the custom D3D scene into the D3DImage
            BeginRenderingScene();


            _timer.Interval = TimeSpan.FromMilliseconds(1000 / 60);

            _timer.Start();
        }

        

        private void OnIsFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // if the front buffer is available, then WPF has just created a new
            // D3D device, so we need to start rendering our custom scene
            if (base.IsFrontBufferAvailable)
            {
                BeginRenderingScene();
            }
            else
            {
                // If the front buffer is no longer available, then WPF has lost its
                // D3D device so there is no reason to waste cycles rendering our
                // custom scene until a new device is created.
                StopRenderingScene();
            }
        }

        private void BeginRenderingScene()
        {
            if (base.IsFrontBufferAvailable)
            {
                // create a custom D3D scene and get a pointer to its surface
               // _scene = InitializeScene();

                // set the back buffer using the new scene pointer
                base.Lock();
                base.SetBackBuffer(D3DResourceType.IDirect3DSurface9, _scene);
                base.Unlock();

                if (_timer != null)
                {
                    _timer.Tick += OnRendering;
                }
                else
                {
                    // leverage the Rendering event of WPF's composition target to
                    // update the custom D3D scene
                    CompositionTarget.Rendering += OnRendering;
                }
            }
        }

        private void StopRenderingScene()
        {
            if (_timer != null)
            {

                _timer.Tick -= OnRendering;
            }
            else
            {
                // This method is called when WPF loses its D3D device.
                // In such a circumstance, it is very likely that we have lost 
                // our custom D3D device also, so we should just release the scene.
                // We will create a new scene when a D3D device becomes 
                // available again.
                CompositionTarget.Rendering -= OnRendering;
            }
         //   ReleaseScene();
            _scene = IntPtr.Zero;
        }

        private void OnRendering(object sender, EventArgs e)
        {
            // when WPF's composition target is about to render, we update our 
            // custom render target so that it can be blended with the WPF target
           
            UpdateScene();
        }

        private void UpdateScene()
        {
            if (base.IsFrontBufferAvailable && _scene != IntPtr.Zero)
            {
                // lock the D3DImage
                base.Lock();

                // update the scene (via a call into our custom library)
              //  SIZE size = new SIZE();
             //   size.Height = 100;
            //    size.Width = 100;
             //   RenderScene(size);
                bool res = DLL_Singleton.Instance.stateUpdate();

                // invalidate the updated region of the D3DImage (in this case, the whole image)
               // base.AddDirtyRect(new Int32Rect(0, 0, size.Width, size.Height));
                base.AddDirtyRect(new Int32Rect(0, 0, PixelWidth, PixelHeight));

                // unlock the D3DImage
                base.Unlock();
            }
        }
    }
}
