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
using System.ComponentModel;
using OIDE.Gorilla.Model;
using Wide.Interfaces.Services;
using Wide.Core.Services;

namespace OIDE.Gorilla.Model
{
    public class SpriteModel : PItem, IGorillaItem
    {
        Rectangle rectselected = new Rectangle
        {
            Stroke = Brushes.Red,
            StrokeThickness = 2
        };

        private String mName;

        private String mPath;
        private Rectangle mRectangle;

        [Browsable(false)]
        public GorillaType GorillaType { get { return GorillaType.Sprite; } }

        [Category("Sprite")]
        public String Path { get { return mPath; } set { mPath = value; RaisePropertyChanged("Path"); } }
       
        [Browsable(false)]
        public Rectangle Rectangle { get { return mRectangle; } set { mRectangle = value; RaisePropertyChanged("Rectangle"); } }

        //public Rectangle RectSelected { get { return rectselected; } set { rectselected = value; } }
        [Category("Sprite")]
        public Double X { get { return Canvas.GetLeft(mRectangle); } set { Canvas.SetLeft(mRectangle, value); RaisePropertyChanged("X"); } }
        [Category("Sprite")]
        public Double Y { get { return Canvas.GetTop(mRectangle); } set { Canvas.SetTop(mRectangle, value); RaisePropertyChanged("Y"); } }
        [Category("Sprite")]
        public Double Width { get { return mRectangle.Width; } set { mRectangle.Width = value; RaisePropertyChanged("Width"); } }
        [Category("Sprite")]
        public Double Height { get { return mRectangle.Height; } set { mRectangle.Height = value; RaisePropertyChanged("Height"); } }
        [Category("Sprite")]
        public String Name { get { return mName; } set { mName = value; RaisePropertyChanged("Name"); } }

        public SpriteModel(GorillaModel gorilla, IUnityContainer container)
       //     : base(gorilla, container)
        {

        }

        #region IItem 

        public string ContentID { get; private set; }
        [Browsable(false)]
        public bool HasChildren { get; set; }
        [Browsable(false)]
        public bool IsExpanded { get; set; }
        [Browsable(false)]
        public bool IsSelected { get; set; }
        [Browsable(false)]
        public CollectionOfIItem Items { get; set; }
        [Browsable(false)]
        public List<System.Windows.Controls.MenuItem> MenuOptions { get; set; }
        [Browsable(false)]
        public IItem Parent { get; set; }
        [Browsable(false)]
        public IUnityContainer UnityContainer { get; set; }

        public bool Create(IUnityContainer unityContainer) { return true; }
        public bool Delete() { return true; }
        public void Drop(IItem item) {  }
        public void Finish() {  }
        public bool Open(IUnityContainer unityContainer, object paramID) { return true; }
        public void Refresh() {  }
        public bool Save(object param = null) { return true; }

        #endregion

    }
}
