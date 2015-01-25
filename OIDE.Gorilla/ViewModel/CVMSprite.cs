using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace CLGorilla.ViewModel
{
    class CVMSprite : CVMElement
    {
        private String mName;
        private String mPath;
        private Rectangle mRectangle;

        public String Name { get { return mName; } set { mName = value; OnPropertyChanged("Name"); } }
        public String Path { get { return mPath; } set { mPath = value; OnPropertyChanged("Path"); } }
        public Rectangle Rectangle { get { return mRectangle; } set { mRectangle = value; OnPropertyChanged("Rectangle"); } }

    }
}
