﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Windows.Shapes;
using System.Windows.Media;
using OIDE.Gorilla.Service;
using OIDE.Gorilla.Model;
using System.Windows.Media.Imaging;
using System.Net.Cache;

namespace OIDE.Gorilla
{
    /// <summary>
    /// Interaktionslogik für UCT_Gorilla.xaml
    /// </summary>
    public partial class GorillaView : UserControl
    {
        public GorillaView()
        {
            InitializeComponent();
        }


        private void btnGenFont_Click(object sender, System.Windows.RoutedEventArgs e)
        {
          //  String path = (this.DataContext as GorillaModel).FontImagePath;
      //      imgFont.Source = new BitmapImage(); 
            (this.DataContext as GorillaModel).GenFont();


           //  SetImage((this.DataContext as GorillaModel).FontImagePath);

             //imgFont.Source = new BitmapImage();
             //SetImage((this.DataContext as GorillaModel).FontImagePath);
        }

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
        //    (this.DataContext as GorillaModel).FontImage = bi;
        //}

        private Point startPoint;
        private Rectangle rect;
        private SpriteModel mNewDropElement;

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.N))
                return;

            startPoint = e.GetPosition(canvas);

            rect = new Rectangle
            {
                Stroke = Brushes.LightBlue,
                StrokeThickness = 1
            };
            Canvas.SetLeft(rect, startPoint.X);
            Canvas.SetTop(rect, startPoint.X);
           // canvas.Children.Add(rect);

            var dc = (this.DataContext as GorillaModel);

            mNewDropElement = new SpriteModel(dc,dc.UnityContainer);
            mNewDropElement.Name = "NewDroppedEle";
            mNewDropElement.Rectangle = rect;

            ((GorillaModel)this.DataContext).Rectangles.Add(rect);
            ((GorillaModel)this.DataContext).Items.Add(mNewDropElement);
        }


        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released || rect == null)
                return;

            var pos = e.GetPosition(canvas);

            var x = Math.Min(pos.X, startPoint.X);
            var y = Math.Min(pos.Y, startPoint.Y);

            var w = Math.Max(pos.X, startPoint.X) - x;
            var h = Math.Max(pos.Y, startPoint.Y) - y;

            rect.Width = w;
            rect.Height = h;

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);

            //    canvas.Children.Add(rect);
            mNewDropElement.X = (int)x;
            mNewDropElement.Y = (int)y;
            mNewDropElement.Width = (int)w;
            mNewDropElement.Height = (int)h;

        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rect = null;
        }
        private void btnGenAtlas_Click(object sender, RoutedEventArgs e)
        {
            ((GorillaModel)this.DataContext).Gen();

            Console.WriteLine("Atlas generated");
        }

        private void canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void textEditor_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGenGorillaFile_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnLoadFontCodeFile_Click(object sender, RoutedEventArgs e)
        {
            OIDE.Gorilla.Helper.FileLoader.LoadGorillaFont(((GorillaModel)this.DataContext).PathToFontGorillaFile, ((GorillaModel)this.DataContext));
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
         //   SetImage((this.DataContext as GorillaModel).FontImagePath);
        }

    }
}
