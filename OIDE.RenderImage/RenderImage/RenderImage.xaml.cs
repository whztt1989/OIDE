using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using OIDE.InteropEditor.DLL;

namespace OIDE.RenderImage.RenderImage
{
    //Alternative ?: http://wpfwin32renderer.codeplex.com/

    /// <summary>
    /// Interaktionslogik für RenderImage.xaml
    /// </summary>
    public partial class RenderImage : UserControl
    {
        //uint TIME_INTERVAL_IN_MILLISECONDS = 32;
        //private System.Threading.Timer _timer;


        /// <summary>
		/// The Mogre render panel that will be hosted in WPF
		/// </summary>
		private RenderPanel mWinFormsRenderPanel;

        public RenderImage()
        {
            InitializeComponent();
        }

        public void init()
        {
            mWinFormsRenderPanel = new RenderPanel();
            mWinFormsRenderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mWinFormsRenderPanel.Name = "Viewport";

            //   mWinFormsRenderPanel.InitializeRendering(EditorRoot.GetInstance().DefaultWorkspace);

            winFormsHost.Child = mWinFormsRenderPanel;
       //     DLL_Singleton.stateInit(mWinFormsRenderPanel.Handle);

            bool test = DLL_Singleton.Instance.stateInit(mWinFormsRenderPanel.Handle);

         //   _timer = new Timer(Callback, null, TIME_INTERVAL_IN_MILLISECONDS, Timeout.Infinite);


            mWinFormsRenderPanel.Start();

            //set Editor as initialized
            DLL_Singleton.Instance.EditorInitialized = true;
                
            //int i = 0;
            //while (i == 0)
            //{
            //    PInvokeWrapper.DLL.CDLL_XE.stateUpdate();
            //}
            //ThreadStart start = delegate()
            //{
            //    // ...

            //    // Sets the Text on a TextBlock Control.
            //    // This will work as its using the dispatcher
            //    Dispatcher.Invoke(DispatcherPriority.Normal,
            //                      new Action(update),
            //                      "From Other Thread");
            //};

            //  Thread threadTest = new Thread(update);
            //  threadTest.Start();
        }

        //private void Callback(Object state)
        //{
        //    // Long running operation
        //    _timer.Change(TIME_INTERVAL_IN_MILLISECONDS, Timeout.Infinite);

        //   // if (DLL != null)
        //    Dispatcher.Invoke(DispatcherPriority.Normal,
        //                          new Action(update),
        //                          "From Other Thread");
            

           
        //}
        //void update()
        //{
        //PInvokeWrapper.DLL.CDLL_XE.stateUpdate();
        //    //    int i = 0;
        ////    while (i == 0)
        ////    {
        ////        PInvokeWrapper.DLL.CDLL_XE.stateUpdate();
        ////    }
        //}

    }
}
