using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADock;
using System.IO;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using CLGorilla.ViewModel;
using System.Collections.ObjectModel;

namespace CLGorilla.Documents.Atlas
{
    class COAtlas
    {
        public static void GenAtlas(ObservableCollection<System.Windows.UIElement> list, ObservableCollection<ViewModelBase> oc)
        {
            MaxRectsBinPack test = new MaxRectsBinPack(512, 1024, false);

            foreach (String imageFile in Directory.GetFiles(@"D:\Projekte\Src Game\Tools\imageTools\images", "*.png"))
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(imageFile);
                Rect newPos = test.Insert(image.Width, image.Height, MaxRectsBinPack.FreeRectChoiceHeuristic.RectBestLongSideFit);

                Rectangle testRect =  new Rectangle
                {
                    Stroke = Brushes.LightBlue,
                    StrokeThickness = 2
                };

                ImageBrush ib = new ImageBrush();
                BitmapImage bmi = new BitmapImage(new Uri(imageFile, UriKind.Absolute));
                ib.ImageSource = bmi;
                testRect.Fill = ib;

                testRect.Width = newPos.Width;
                testRect.Height = newPos.Height;

                Canvas.SetLeft(testRect, newPos.Left);
                Canvas.SetTop(testRect, newPos.Top);

                list.Add(testRect);
                
                // canvas.Children.Add(testRect);

                CVMSprite newSprite = new CVMSprite();
                newSprite.Name = System.IO.Path.GetFileName(imageFile); 
                newSprite.Path = imageFile;
                newSprite.Rectangle = testRect;

                oc.Add(newSprite);

                //pictureBox.Width = image.Width;
                //pictureBox.Height = image.Height;
                //pictureBox.Image = image;
            }
        }
    }
}
