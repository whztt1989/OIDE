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
using System.Windows.Media.Media3D;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using OIDE.Gorilla.Interface.Services;
using OIDE.Gorilla.Service;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace OIDE.Gorilla
{
    public enum SquareSize
    {
        SS_256 = 256,
        SS_512 = 512,
        SS_1024 = 1024,
        SS_2048 = 2048,
        SS_4096 = 4096,
    };

    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    public class GorillaModel : TextModel , IGorilla
    {

        #region private members

        private UInt16 mOutlineWidth;
        private UInt16 mIntensityModifier;
        private Point3D mGlyphColor;
        private Point3D mOutlineColor;
        private UInt16 mFontSize;
        private String mFontFile;
        private String mAlphabetFile;
        private SquareSize mSquareTextureSize;
      //  private String mGeneratedFontImage;
        private String mGorillaCode;
        private ObservableCollection<ViewModelBase> mFonts;
        private String m_PathToGorillaFile;
        private String m_ImageExtensions;
        private String m_FontImagePath;
        private String m_ImageFolder;

        private String mFilePath;
        private ObservableCollection<System.Windows.UIElement> mRectangles;
        private ObservableCollection<IGorillaItem> mImages;

        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="MDModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public GorillaModel(IUnityContainer container, ICommandManager commandManager, IMenuService menuService)
            : base(commandManager, menuService)
        {
            UnityContainer = container;
            mFonts = new ObservableCollection<ViewModelBase>();
            mRectangles = new ObservableCollection<System.Windows.UIElement>();
            mImages = new ObservableCollection<IGorillaItem>();

            ImageFolder = @"D:\Projekte\coop\XEngine\data\Test\XETUI\art";
            ImageExtensions = "*.png";
            FontImagePath = @"D:\Projekte\coop\Build\arial.png";
            PathToGorillaFile = @"D:\Projekte\coop\XEngine\data\Test\XETUI\arial.gorilla";

            StreamReader streamReader = new StreamReader(m_PathToGorillaFile);
            GorillaCode = streamReader.ReadToEnd();
            streamReader.Close();

            width = SquareSize.SS_512;
            height = SquareSize.SS_512;
        }

        #region properties

        public SquareSize width { get; set; }
        public SquareSize height { get; set; }

        [Browsable(false)]
        public IUnityContainer UnityContainer { get; set; }

        public String PathToGorillaFile { get { return m_PathToGorillaFile; } set { m_PathToGorillaFile = value; RaisePropertyChanged("PathToGorillaFile"); } }
      //  public String FilePath { get { return mFilePath; } set { mFilePath = value; RaisePropertyChanged("FilePath"); } }

        public ObservableCollection<IGorillaItem> GorillaItems { get { return mImages; } set { mImages = value; } }
        public ObservableCollection<System.Windows.UIElement> Rectangles { get { return mRectangles; } }

        public String ImageFolder { get { return m_ImageFolder; } set { m_ImageFolder = value; RaisePropertyChanged("ImageFolder"); } }
        public String ImageExtensions { get { return m_ImageExtensions; } set { m_ImageExtensions = value; RaisePropertyChanged("ImageExtensions"); } }
        public String FontImagePath { get { return m_FontImagePath; } set { m_FontImagePath = value; RaisePropertyChanged("FontImagePath"); } }

        //   public ObservableCollection<ViewModelBase> Items { get { return mFonts; } }
        public String AlphabetFile { get { return mAlphabetFile; } set { mAlphabetFile = value; RaisePropertyChanged("AlphabetFile"); } }
        public SquareSize SquareTextureSize { get { return mSquareTextureSize; } set { mSquareTextureSize = value; RaisePropertyChanged("SquareTextureSize"); } }
      //  public String GeneratedFontImage { get { return mGeneratedFontImage; } set { mGeneratedFontImage = value; RaisePropertyChanged("GeneratedFontImage"); } }
        public UInt16 OutlineWidth { get { return mOutlineWidth; } set { mOutlineWidth = value; RaisePropertyChanged("OutlineWidth"); } }
        public UInt16 IntensityModifier { get { return mIntensityModifier; } set { mIntensityModifier = value; RaisePropertyChanged("IntensityModifier"); } }
        public Point3D GlyphColor { get { return mGlyphColor; } set { mGlyphColor = value; RaisePropertyChanged("GlyphColor"); } }
        public Point3D OutlineColor { get { return mOutlineColor; } set { mOutlineColor = value; RaisePropertyChanged("OutlineColor"); } }
        public UInt16 FontSize { get { return mFontSize; } set { mFontSize = value; RaisePropertyChanged("FontSize"); } }
        public String FontFile { get { return mFontFile; } set { mFontFile = value; RaisePropertyChanged("FontFile"); } }

        [Browsable(false)]
        public String GorillaCode
        {
            get { return mGorillaCode; } 
            set {
                mGorillaCode = value;
                Document.Text = mGorillaCode;
                RaisePropertyChanged("GorillaCode"); } }

        #endregion

        #region methods

        public void GenFont()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                String basePath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

                psi.FileName = @".\fontgen.exe";
                FontFile = psi.FileName;
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardInput = true;
                psi.Arguments = "-f gorilla -t 512";
                System.Diagnostics.Process proz = System.Diagnostics.Process.Start(psi);

                proz.StandardInput.WriteLine("-o 2 -i 2 -s 18 -c \"1 1 0\" -C \"1 0 0\" arial.ttf");
                proz.StandardInput.WriteLine("-o 1 -i 2 -s 24 -c \"0 0 1\" -C \"1 0 1\" arial.ttf");
                proz.StandardInput.WriteLine("-o 2 -i 3 -s 36 -c \"1 1 1\" -C \"0 0 0\" arial.ttf");

                proz.StandardInput.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public void Gen()
        {
            OIDE.Gorilla.Atlas.COAtlas.GenAtlas(mRectangles, mImages, m_ImageFolder, m_ImageExtensions, width, height, this, UnityContainer);
        }

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

        public bool AddItem(IGorillaItem item) { return true; }

        [Browsable(false)]
        public IGorillaItem SelectedItem { get; set; }
         [Browsable(false)]
         public CollectionOfIItem Items { get; set; }
        public string ContentID { get; set; }
        [Browsable(false)]
        public bool HasChildren { get; set; }
        [Browsable(false)]
        public bool IsExpanded { get; set; }

        private Boolean m_IsSelected;
        [Browsable(false)]
        public bool IsSelected {
            get { return m_IsSelected; }
            set
            {
                m_IsSelected = value;
                var gorillaService = UnityContainer.Resolve<IGorillaService>();
                gorillaService.SelectedGorilla = this;
            }
        }
        [Browsable(false)]
        public List<System.Windows.Controls.MenuItem> MenuOptions { get; set; }
        public string Name { get; set; }
        [Browsable(false)]
        public IItem Parent { get; set; }

        public bool Create() { return true; }
        public bool Delete() { return true; }
        public void Drop(IItem item) {  }
        public void Finish() { }
         public bool Open(object paramID) { return true; }
         public void Refresh() { }
         public bool Save(object param = null) { return true; }

        #endregion
    }
}