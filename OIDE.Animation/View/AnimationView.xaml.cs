using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Windows.Shapes;
using System.Windows.Media;
using OIDE.Animation.Service;
using OIDE.Animation.Model;
using System.Windows.Media.Imaging;
using System.Net.Cache;
using OIDE.Animation.Nodes;

namespace OIDE.Animation
{
    /// <summary>
    /// Interaktionslogik für UCT_Animation.xaml
    /// </summary>
    public partial class AnimationView : UserControl
    {
        public AnimationView()
        {
            InitializeComponent();    
        }


      //  private void btnGenFont_Click(object sender, System.Windows.RoutedEventArgs e)
      //  {
      //    //  String path = (this.DataContext as AnimationModel).FontImagePath;
      ////      imgFont.Source = new BitmapImage(); 
      //  //    (this.DataContext as AnimationModel).GenFont();
           

      //     //  SetImage((this.DataContext as AnimationModel).FontImagePath);

      //       //imgFont.Source = new BitmapImage();
      //       //SetImage((this.DataContext as AnimationModel).FontImagePath);
      //  }

        //private void SetImage(String filename)
        //{
        //    BitmapImage bi = new BitmapImage();
        //    bi.BeginInit();
        //    bi.CacheOption = BitmapCacheOption.None;
        //    bi.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
        //    bi.CacheOption = BitmapCacheOption.OnLoad;
        //    bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
        //    bi.UriSource = new Uri(filename);
        //    bi.EndInit();
        //    (this.DataContext as AnimationModel).FontImage = bi;
        //}

        private Point startPoint;
        private Rectangle rect;

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
           // if (!Keyboard.IsKeyDown(Key.N))
           //     return;

           // startPoint = e.GetPosition(canvas);

           // rect = new Rectangle
           // {
           //     Stroke = Brushes.LightBlue,
           //     StrokeThickness = 1
           // };
           // Canvas.SetLeft(rect, startPoint.X);
           // Canvas.SetTop(rect, startPoint.X);
           //// canvas.Children.Add(rect);

           // var dc = (this.DataContext as AnimationModel);

           // mNewDropElement = new SpriteModel(dc,dc.UnityContainer);
           // mNewDropElement.Name = "NewDroppedEle";
           // mNewDropElement.Rectangle = rect;

           // ((AnimationModel)this.DataContext).Rectangles.Add(rect);
           // ((AnimationModel)this.DataContext).Items.Add(mNewDropElement);
        }


        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.LeftButton == MouseButtonState.Released || rect == null)
            //    return;

            //var pos = e.GetPosition(canvas);

            //var x = Math.Min(pos.X, startPoint.X);
            //var y = Math.Min(pos.Y, startPoint.Y);

            //var w = Math.Max(pos.X, startPoint.X) - x;
            //var h = Math.Max(pos.Y, startPoint.Y) - y;

            //rect.Width = w;
            //rect.Height = h;

            //Canvas.SetLeft(rect, x);
            //Canvas.SetTop(rect, y);

            ////    canvas.Children.Add(rect);
            //mNewDropElement.X = (int)x;
            //mNewDropElement.Y = (int)y;
            //mNewDropElement.Width = (int)w;
            //mNewDropElement.Height = (int)h;

        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rect = null;
        }

        private void canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void textEditor_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGenAnimationFile_Click(object sender, RoutedEventArgs e)
        {
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           animationTree.ElementList =  (this.DataContext as AnimationModel).Elements;
           animationTree.ConnectionList = (this.DataContext as AnimationModel).Connections;
            //   SetImage((this.DataContext as AnimationModel).FontImagePath);

        }

    }
}
