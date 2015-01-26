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

        public String Name { get { return mName; } set { mName = value; RaisePropertyChanged("Name"); } }
        public String Path { get { return mPath; } set { mPath = value; RaisePropertyChanged("Path"); } }
        public Rectangle Rectangle { get { return mRectangle; } set { mRectangle = value; RaisePropertyChanged("Rectangle"); } }

    }
}
