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
using System.Collections.ObjectModel;
using Wide.Interfaces;
using OIDE.Gorilla.Service;
using OIDE.Gorilla.Interface.Services;
using Microsoft.Practices.Unity;
using OIDE.Gorilla.Model;
using Module.Properties.Interface;
using Wide.Interfaces.Services;

namespace OIDE.Gorilla.Atlas
{
    class COAtlas
    {
        public static void GenAtlas(ObservableCollection<System.Windows.UIElement> list,
            ObservableCollection<IItem> oc, 
            String pathToImageFolder, 
            String searchFilext,
            SquareSize width,
            SquareSize height,
            GorillaModel gorilla,
            IUnityContainer container)
        {
            MaxRectsBinPack test = new MaxRectsBinPack((int)width, (int)height, false);

            foreach (String imageFile in Directory.GetFiles(pathToImageFolder, searchFilext))
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(imageFile);
                Rect newPos = test.Insert(image.Width, image.Height, MaxRectsBinPack.FreeRectChoiceHeuristic.RectBestLongSideFit);

                Rectangle testRect =  new Rectangle
                {
                    Stroke = Brushes.LightBlue,
                    StrokeThickness = 1
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

                SpriteModel newSprite = new SpriteModel(gorilla, container);
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
