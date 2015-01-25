using System.Windows;
using System.Windows.Controls;
using CLGorilla.Common;
using System.Windows.Input;
using System;
using System.Windows.Shapes;
using CLGorilla.ViewModel;
using System.Windows.Media;

namespace CLGorilla.Documents.Gorilla
{
    /// <summary>
    /// Interaktionslogik für UCT_Gorilla.xaml
    /// </summary>
    public partial class UCT_Gorilla : UserControl
    {
        public UCT_Gorilla()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            COGorilla tmp = new COGorilla();
            tmp.GenerateGorillaFile();
            // 
        }

        private Point startPoint;
        private Rectangle rect;
        private CVMElement mNewDropElement;

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(canvas);

            rect = new Rectangle
            {
                Stroke = Brushes.LightBlue,
                StrokeThickness = 2
            };
            Canvas.SetLeft(rect, startPoint.X);
            Canvas.SetTop(rect, startPoint.X);
           // canvas.Children.Add(rect);

            mNewDropElement = new CVMElement();
            mNewDropElement.Name = "NewDroppedEle";

            ((ADock.ViewModel.ProjVM.CVMGorilla)this.DataContext).Rectangles.Add(rect);
            ((ADock.ViewModel.ProjVM.CVMGorilla)this.DataContext).Items.Add(mNewDropElement);
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
            ((ADock.ViewModel.ProjVM.CVMGorilla)this.DataContext).Gen();

            Console.WriteLine("Atlas generated");
        }

        private void canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
