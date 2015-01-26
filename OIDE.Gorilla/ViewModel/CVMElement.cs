using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADock;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using Wide.Interfaces;

namespace CLGorilla.ViewModel
{
    public class CVMElement : ViewModelBase
    {
        Rectangle rectselected = new Rectangle
        {
            Stroke = Brushes.Red,
            StrokeThickness = 2
        };

        private Int32 mX;
        private Int32 mY;
        private Int32 mWidth;
        private Int32 mHeight;
        private String mName;

        public Rectangle RectSelected { get { return rectselected; } set { rectselected = value; } }
        public Int32 X { get { return mX; } set { mX = value; Canvas.SetLeft(rectselected, value); RaisePropertyChanged("X"); } }
        public Int32 Y { get { return mY; } set { mY = value; Canvas.SetTop(rectselected, value); RaisePropertyChanged("Y"); } }
        public Int32 Width { get { return mWidth; } set { mWidth = value; rectselected.Width = value; RaisePropertyChanged("Width"); } }
        public Int32 Height { get { return mHeight; } set { mHeight = value; rectselected.Height = value; RaisePropertyChanged("Height"); } }
        public String Name { get { return mName; } set { mName = value; RaisePropertyChanged("Name"); } }


    }
}
