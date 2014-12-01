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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CLXEditor.Render;
using OIDE.InteropEditor.DLL;
using SFML.Window;
using OIDE.Scene.ViewModel.Objects;

namespace OIDE.Scene.View
{
    /// <summary>
    /// Interaktionslogik für SceneView.xaml
    /// </summary>
    public partial class SceneViewerView : UserControl
    {
        //RenderMode mRenderMode;

        public SceneViewerView()
        {
            InitializeComponent();

            //mRenderMode = new CLXEditor.Render.RenderMode(ImageGrid);

            //if (mRenderMode.AutoUpdateViewportSize)
            //{
            //    MogreImage.SizeChanged += new SizeChangedEventHandler(mRenderMode.MogreImage_SizeChanged);
            //}
        }

        private void MogreImage_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MogreImage_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void TestCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            //if (!mRenderMode.Initialized)
            //{
            //    MogreInWpf.MogreImage tmp = mRenderMode.init();
            //    if (tmp != null)
            //        MogreImage.Source = tmp;
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        ///    Window test = tw;
           renderImage.init();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Event test = new Event();
            test.Type = EventType.GainedFocus;
           
          //  test.Key.Code = SFML.Window.Keyboard.Key.W;
            int test2;

//            test2 = DLL_Singleton.pushEvent(test);

            test2 = DLL_Singleton.Instance.PushEvent(test);


           


            byte[] tmparr = new byte[0];

       //     DLL_Singleton.Instance.command("cmd", tmparr, 0);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Event testReleased = new Event();
            testReleased.Type = EventType.LostFocus;
            //testReleased.Key.Code = SFML.Window.Keyboard.Key.W;
            DLL_Singleton.Instance.PushEvent(testReleased);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
         //   OIDE.InteropEditor.DLL.DLL_Singleton.StatesData test = new OIDE.InteropEditor.DLL.DLL_Singleton.StatesData();

            String test = "sdfs";
      //      test[0] = "y";
        //    test.buffer[0] = "xx";
          //  test.buffer = { "one", "two", "three", "four", "five" };
          //  test.size = 5;
            try
            {
                DLL_Singleton.Instance.getStatesData(ref test);
            }catch(Exception ex)
            {

            }
        }

        private void btnTextFB_Click(object sender, RoutedEventArgs e)
        {
            SceneDataViewModel tmp = new SceneDataViewModel();
            tmp.Create();
            tmp.Read();
        }


    }
}
