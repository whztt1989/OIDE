using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADock;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using Wide.Interfaces;
using OIDE.Gorilla.Service;
using OIDE.Gorilla.Interface.Services;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;

namespace CLGorilla.ViewModel
{
    public class SpriteModel : ViewModelBase, IGorillaItem
    {
        Rectangle rectselected = new Rectangle
        {
            Stroke = Brushes.Red,
            StrokeThickness = 2
        };

        private String mName;

        private String mPath;
        private Rectangle mRectangle;

        public GorillaType GorillaType { get { return GorillaType.Sprite; } }

        public String Path { get { return mPath; } set { mPath = value; RaisePropertyChanged("Path"); } }
        public Rectangle Rectangle { get { return mRectangle; } set { mRectangle = value; RaisePropertyChanged("Rectangle"); } }

        //public Rectangle RectSelected { get { return rectselected; } set { rectselected = value; } }
        public Double X { get { return Canvas.GetLeft(mRectangle); } set { Canvas.SetLeft(mRectangle, value); RaisePropertyChanged("X"); } }
        public Double Y { get { return Canvas.GetTop(mRectangle); } set { Canvas.SetTop(mRectangle, value); RaisePropertyChanged("Y"); } }
        public Double Width { get { return mRectangle.Width; } set { mRectangle.Width = value; RaisePropertyChanged("Width"); } }
        public Double Height { get { return mRectangle.Height; } set { mRectangle.Height = value; RaisePropertyChanged("Height"); } }
        public String Name { get { return mName; } set { mName = value; RaisePropertyChanged("Name"); } }

        public SpriteModel(IGorilla gorilla, IUnityContainer container)
       //     : base(gorilla, container)
        {

        }

        #region IItem 

        public string ContentID { get; set; }
        public bool HasChildren { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsSelected { get; set; }
        public CollectionOfIItem Items { get; set; }
        public List<System.Windows.Controls.MenuItem> MenuOptions { get; set; }
        public IItem Parent { get; set; }
        public IUnityContainer UnityContainer { get; set; }

        public bool Create() { return true; }
        public bool Delete() { return true; }
        public void Drop(IItem item) {  }
        public void Finish() {  }
        public bool Open(object paramID) { return true; }
        public void Refresh() {  }
        public bool Save(object param = null) { return true; }

        #endregion

    }
}
