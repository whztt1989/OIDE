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
using System.Collections.ObjectModel;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace OIDE.Gorilla
{
    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    public class GorillaModel : TextModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MDModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public GorillaModel(ICommandManager commandManager, IMenuService menuService)
            : base(commandManager, menuService)
        {
            mRectangles = new ObservableCollection<System.Windows.UIElement>();
            mImages = new ObservableCollection<ViewModelBase>();

            ImageFolder = @"D:\Projekte\coop\XEngine\data\Test\XETUI\art";
            ImageExtensions = "*.png";
        }

        private String mFilePath;
        private ObservableCollection<System.Windows.UIElement> mRectangles;
        ObservableCollection<ViewModelBase> mImages;


        public String FilePath { get { return mFilePath; } set { mFilePath = value; RaisePropertyChanged("FilePath"); } }
        public ObservableCollection<ViewModelBase> Items { get { return mImages; } }
        public ObservableCollection<System.Windows.UIElement> Rectangles { get { return mRectangles; } }

        private String m_ImageFolder;
        public String ImageFolder { get { return m_ImageFolder; } set { m_ImageFolder = value; RaisePropertyChanged("ImageFolder"); } }
        private String m_ImageExtensions;
        public String ImageExtensions { get { return m_ImageExtensions; } set { m_ImageExtensions = value; RaisePropertyChanged("ImageExtensions"); } }

        public void Gen()
        {
            OIDE.Gorilla.Atlas.COAtlas.GenAtlas(mRectangles, mImages, m_ImageFolder, m_ImageExtensions);
        }


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
    }
}