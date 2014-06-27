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
using PInvokeWrapper.DLL;

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
            renderImage.init();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            DLL_Singleton.Instance.consoleCmd("cmd");
        }

    }
}
