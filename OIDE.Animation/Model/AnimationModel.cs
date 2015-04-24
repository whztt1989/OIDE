#region License

//The MIT License (MIT)

//Copyright (c) 2014 Konrad Huber

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Media3D;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using OIDE.Animation.Interface.Services;
using OIDE.Animation.Service;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using System.Windows.Media;
using Module.Properties.Interface.Services;
using System.Windows.Shapes;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Windows.Media.Imaging;
using DLL.NodeEditor.ViewModel;
using OIDE.Animation.Nodes;
using DLL.NodeEditor.Nodes;
using Wide.Core.Services;

namespace OIDE.Animation.Model
{

    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    public class AnimationModel : PItem
    {
        #region private members

        #endregion

        private ObservableCollection<ElementViewModel> m_Elements;
        public ObservableCollection<ElementViewModel> Elements { get { return m_Elements; } set { m_Elements = value; RaisePropertyChanged("Elements"); } }

        private ObservableCollection<ConnectionViewModel> m_Connections;
        public ObservableCollection<ConnectionViewModel> Connections { get { return m_Connections; } set { m_Connections = value; RaisePropertyChanged("Connections"); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="MDModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public AnimationModel()
         //   : base(commandManager, menuService)
        {
           
            Name = "Animation New";
           
            //StreamReader streamReader = new StreamReader(m_PathToAnimationFile);
            //AnimationCode = streamReader.ReadToEnd();
            //streamReader.Close();

            m_Elements = new ObservableCollection<ElementViewModel>();
            m_Connections = new ObservableCollection<ConnectionViewModel>();
       //     var animSErvice = UnityContainer.Resolve<IAnimationService>();
         //   animSErvice.ATM.
        }

        #region properties

     
        #endregion

        #region methods

        // Handle Exited event and display process information.
        //private void myProcess_Exited(object sender, System.EventArgs e)
        //{

        ////    eventHandled = true;
        //    Console.WriteLine("Exit time:    {0}\r\n" +
        //        "Exit code:    {1}\r", proz.ExitTime, proz.ExitCode);

        //    if (AutoGenFont)
        //    {
        //     //   GenFont();
        //        OIDE.Animation.Helper.FileLoader.LoadAnimationFont(PathToFontAnimationFile, this);
        //    }

        //    OIDE.Animation.Atlas.COAtlas.GenAtlas(mRectangles, m_items, m_ImageFolder, m_ImageExtensions, Width, Height, this, UnityContainer);


        //    AnimationCode = OIDE.Animation.Helper.FileLoader.GenerateAnimationCode(this);


        //    //BitmapImage bi = new BitmapImage();
        //    //bi.BeginInit();
        //    //bi.CacheOption = BitmapCacheOption.None;
        //    ////    bi.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
        //    //bi.CacheOption = BitmapCacheOption.OnLoad;
        //    //bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
        //    //bi.UriSource = new Uri(FontImagePath);
        //    //bi.EndInit();
        //    //FontImage = bi;
        //}


        /// <summary>
        /// Location is the *.gorilla file
        /// </summary>
        /// <param name="location"></param>
        internal void SetLocation(object location)
        {
            this.Location = location;
            RaisePropertyChanged("Location");
        }

        internal void SetDirty(bool value)
        {
            this.IsDirty = value;
        }

        public string HTMLResult { get; set; }

        public void SetHtml(string transform)
        {
            this.HTMLResult = transform;
            RaisePropertyChanged("HTMLResult");
        }

        public bool AddItem(IAnimationItem item) { return true; }

        private IAnimationItem m_SelectedItem;
        [Browsable(false)]
        public IAnimationItem SelectedItem
        {
            get { return m_SelectedItem; }
            set
            {
                //if (m_SelectedItem != null)
                //{
                //    m_SelectedItem.Rectangle.Stroke = Brushes.LightBlue;
                //    m_SelectedItem.Rectangle.StrokeThickness = 1;
                //}
                if (value != null)
                {
                  
                }
                m_SelectedItem = value;
                //if (m_SelectedItem != null)
                //{
                //    m_SelectedItem.Rectangle.Stroke = Brushes.Red;
                //    m_SelectedItem.Rectangle.StrokeThickness = 1;
                //}

            }
        }

        private Boolean m_IsSelected;
        [Browsable(false)]
        public override bool IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                m_IsSelected = value;
                var gorillaService = UnityContainer.Resolve<IAnimationService>();
                gorillaService.SelectedAnimation = this;
            }
        }
        [Browsable(false)]
        public override List<System.Windows.Controls.MenuItem> MenuOptions { get; set; }

        public TElement AddElement<TElement>(double x, double y)
          where TElement : ElementViewModel, new()
        {
            var element = new TElement { X = x, Y = y };
            Elements.Add(element);
            return element;
        }

        public override bool Create(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;

            var element1 = AddElement<ColorInput>(100, 50);
            //  element1.Bitmap = BitmapUtility.CreateFromBytes(DesignTimeImages.Desert);

            var element2 = AddElement<ColorInput>(100, 300);
            element2.Color = Colors.Green;

            var element3 = AddElement<Multiply>(400, 250);

            Connections.Add(new ConnectionViewModel(
                element1.OutputConnector,
                element3.InputConnectors[0]));

            Connections.Add(new ConnectionViewModel(
                element2.OutputConnector,
                element3.InputConnectors[1]));

            var tEle = new SequenceNode();
            Elements.Add(tEle);

            return true;
        }
        public override bool Delete() { return true; }
        public override void Drop(IItem item) { }
        public override void Finish() { }


        public override bool Open(IUnityContainer unityContainer, object paramID)
        {
            UnityContainer = unityContainer;
            //DAL.Utility.JSONSerializer.Deserialize<AnimationData>(this.Location.ToString());



            var tEle = new SequenceNode();
            Elements.Add(tEle);

            return true;
        }

        public override void Refresh() { }
        public override bool Save(object param = null)
        {
            //DAL.Utility.JSONSerializer.Serialize<AnimationData>(m_AnimationData, param.ToString());
            
            return true; }

        #endregion
    }
}