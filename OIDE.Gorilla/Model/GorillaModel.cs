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
using OIDE.Gorilla.Interface.Services;
using OIDE.Gorilla.Service;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using System.Windows.Media;
using Module.Properties.Interface.Services;
using System.Windows.Shapes;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace OIDE.Gorilla.Model
{
    public struct Position
    {
        public int x {get;set;}
        public int y {get;set;}
    }
    public enum SquareSize : int
    {
        SS_256 = 256,
        SS_512 = 512,
        SS_1024 = 1024,
        SS_2048 = 2048,
        SS_4096 = 4096,
    };
    public struct Kerning
    {
        public int RightGlyphID {get;set;}
        public int KerningValue {get;set;}
    }

    public class Texture
    {
        public String file { get; set; }
        public Position whitepixel { get; set; }
    }

    public class FontData : IItem
    {
        private GorillaModel m_Gorilla;

        public int Index { get; set; }

        [ExpandableObject]
        public Glyph Glyph { get; set; }
        public ObservableCollection<Kerning> Kerning { get; set; }
        public int VerticalOffset { get; set; }

        public FontData(GorillaModel gorilla)
        {
            m_Gorilla = gorilla;
            Kerning = new ObservableCollection<Kerning>();
            UnityContainer = gorilla.UnityContainer;
        }

        [Browsable(false)]
        public IUnityContainer UnityContainer { get; set; }
        [Browsable(false)]
        public CollectionOfIItem Items { get; set; }
        public String ContentID { get; private set; }
        [Browsable(false)]
        public bool HasChildren { get; set; }
        [Browsable(false)]
        public bool IsExpanded { get; set; }

        private Boolean m_IsSelected;
        [Browsable(false)]
        public bool IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                m_IsSelected = value;
                var propService = UnityContainer.Resolve<IPropertiesService>();
                propService.CurrentItem = this;


                m_Gorilla.SelectedRectangle.Stroke = Brushes.Red;
                m_Gorilla.SelectedRectangle.StrokeThickness = 2;
                m_Gorilla.SelectedRectangle.Width = Glyph.width;
                m_Gorilla.SelectedRectangle.Height = Glyph.height;

                Canvas.SetLeft(m_Gorilla.SelectedRectangle, Glyph.X);
                Canvas.SetTop(m_Gorilla.SelectedRectangle, Glyph.Y);
                //    var gorillaService = m_container.Resolve<IGorillaService>();
                //    gorillaService.SelectedGorilla = this;
            }
        }
        [Browsable(false)]
        public List<System.Windows.Controls.MenuItem> MenuOptions { get; set; }
        public string Name { get; set; }
        [Browsable(false)]
        public IItem Parent { get; set; }

        public bool Create() { return true; }
        public bool Delete() { return true; }
        public void Drop(IItem item) { }
        public void Finish() { }
        public bool Open(object paramID) { return true; }
        public void Refresh() { }
        public bool Save(object param = null) { return true; }
    }

    public class Glyph
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class FontModel : IItem
    {
        private GorillaModel m_Gorilla;
        private CollectionOfIItem mFonts;
        public CollectionOfIItem Items { get { return mFonts; } }

        public int size { get; set; }
        public int lineheight { get; set; }
        public int spacelength { get; set; }
        public int baseline { get; set; }
        public float kerning { get; set; }
        public int letterspacing { get; set; }
        public int monowidth { get; set; }
        public int rangeFrom { get; set; }
        public int rangeTo { get; set; }

        public FontModel(GorillaModel gorilla)
        {
            m_Gorilla = gorilla;
            UnityContainer = gorilla.UnityContainer;
            mFonts = new CollectionOfIItem();
        }

        public void SetGlyph(int index, Glyph glyph)
        {
            var font = mFonts.Where(x => (x as FontData).Index == index);
            if (font.Any())
                (font.First() as FontData).Glyph = glyph;
            else
                mFonts.Add(new FontData(m_Gorilla) { Name = index.ToString(), Index = index, Glyph = glyph });
        }

        public void SetVerticalOffset(int index, int offset)
        {
            var font = mFonts.Where(x => (x as FontData).Index == index);
            if (font.Any())
                (font.First() as FontData).VerticalOffset = offset;
            else
                mFonts.Add(new FontData(m_Gorilla) { Name = index.ToString(), Index = index, VerticalOffset = offset });
        }

        public void SetKerning(int index, Kerning kerning)
        {
            var font = mFonts.Where(x => (x as FontData).Index == index);
            if (font.Any())
                (font.First() as FontData).Kerning.Add(kerning);
            else
                mFonts.Add(new FontData(m_Gorilla) { Name = index.ToString(), Index = index, Kerning = new ObservableCollection<Kerning>() { kerning } });
        }

        [Browsable(false)]
        public IUnityContainer UnityContainer { get; set; }
        [Browsable(false)]
      //public CollectionOfIItem Items { get; set; }
        public String ContentID { get; private set; }
        [Browsable(false)]
        public bool HasChildren { get; set; }
        [Browsable(false)]
        public bool IsExpanded { get; set; }

        private Boolean m_IsSelected;
        [Browsable(false)]
        public bool IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                m_IsSelected = value;
               var propService =  UnityContainer.Resolve<IPropertiesService>();
               propService.CurrentItem = this;


            //    var gorillaService = m_container.Resolve<IGorillaService>();
            //    gorillaService.SelectedGorilla = this;
            }
        }
        [Browsable(false)]
        public List<System.Windows.Controls.MenuItem> MenuOptions { get; set; }
        public string Name { get; set; }
        [Browsable(false)]
        public IItem Parent { get; set; }

        public bool Create() { return true; }
        public bool Delete() { return true; }
        public void Drop(IItem item) { }
        public void Finish() { }
        public bool Open(object paramID) { return true; }
        public void Refresh() { }
        public bool Save(object param = null) { return true; }

    }
    
    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    public class GorillaModel : TextModel , IItem
    {

        #region private members

        private Rectangle m_SelectedRectangle;
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
        private ObservableCollection<FontModel> mFonts;
        private String m_PathToGorillaFile;
        private String m_PathToFontGorillaFile;
        private String m_ImageExtensions;
        private String m_FontImagePath;
        private String m_ImageFolder;
        private Texture m_Texture;
      //  private String mFilePath;
        private ObservableCollection<System.Windows.UIElement> mRectangles;
        private CollectionOfIItem m_items;

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
            mFonts = new ObservableCollection<FontModel>();
            mRectangles = new ObservableCollection<System.Windows.UIElement>();
            m_items = new CollectionOfIItem();

            SelectedRectangle = new Rectangle();
            mRectangles.Add(m_SelectedRectangle);
            ImageFolder = @"D:\Projekte\coop\XEngine\data\Test\XETUI\art";
            ImageExtensions = "*.png";
            FontImagePath = @"D:\Projekte\coop\Build\arial.png";
            TextureName = "arial.png";
            Name = "TestGorillaAtlas";
            PathToGorillaFile = @"D:\Projekte\coop\Build\" + Name + ".gorilla";

            PathToFontGorillaFile = @"D:\Projekte\coop\Build\arial.gorilla";

            //StreamReader streamReader = new StreamReader(m_PathToGorillaFile);
            //GorillaCode = streamReader.ReadToEnd();
            //streamReader.Close();

            Width = SquareSize.SS_2048;
            Height = SquareSize.SS_2048;
        }

        #region properties

        public Rectangle SelectedRectangle { get { return m_SelectedRectangle; } set { m_SelectedRectangle = value; } }

        public String TextureName { get; set; }

        private SquareSize m_Width;
        private SquareSize m_Height;

        [Category("Texture")]
        public SquareSize Width
        {
            get { return m_Width; }
            set
            {
                m_Width = value; TexWidth = (int)value;// clearGorillaitems();
                RaisePropertyChanged("Width");
            }
        }
        [Category("Texture")]
        public SquareSize Height
        {
            get { return m_Height; }
            set
            {
                m_Height = value; TexHeight = (int)value; //clearGorillaitems();
                RaisePropertyChanged("Height");
            }
        }

        [Category("Texture")]
        public Texture GorillaTexture
        {
            get { if (m_Texture == null) m_Texture = new Texture(); return m_Texture; }
            set { m_Texture = value; RaisePropertyChanged("Texture"); }
        }

        private int m_TexWidth;
        private int m_TexHeight;

        [Browsable(false)]
        public int TexWidth
        {
            get { return m_TexWidth; }
            set
            {
                m_TexWidth = value;
                RaisePropertyChanged("TexWidth");
            }
        }
        [Browsable(false)]
        public int TexHeight
        {
            get { return m_TexHeight; }
            set
            {
                m_TexHeight = value;
                RaisePropertyChanged("TexHeight");
            }
        }

        [Browsable(false)]
        public IUnityContainer UnityContainer { get; set; }

        /// <summary>
        /// filepath to the fontgen.exe generated file
        /// </summary>
        [Category("Font")]
        public String PathToFontGorillaFile { get { return m_PathToFontGorillaFile; } set { m_PathToFontGorillaFile = value; RaisePropertyChanged("PathToFontGorillaFile"); } }
        [Category("Gorilla")]
        public String PathToGorillaFile { get { return m_PathToGorillaFile; } set { m_PathToGorillaFile = value; RaisePropertyChanged("PathToGorillaFile"); } }
        //  public String FilePath { get { return mFilePath; } set { mFilePath = value; RaisePropertyChanged("FilePath"); } }

        //[Category("Gorilla")]
        //public ObservableCollection<IGorillaItem> GorillaItems { get { return m_GItems; } set { m_GItems = value; } }
        [Category("Gorilla")]
        public ObservableCollection<System.Windows.UIElement> Rectangles { get { return mRectangles; } }

        [Category("Gorilla")]
        public String ImageFolder { get { return m_ImageFolder; } set { m_ImageFolder = value; RaisePropertyChanged("ImageFolder"); } }
        [Category("Gorilla")]
        public String ImageExtensions { get { return m_ImageExtensions; } set { m_ImageExtensions = value; RaisePropertyChanged("ImageExtensions"); } }
        [Category("Font")]
        public String FontImagePath { get { return m_FontImagePath; } set { m_FontImagePath = value; RaisePropertyChanged("FontImagePath"); } }


        public ObservableCollection<FontModel> Fonts { get { return mFonts; } }

        [Category("Font")]
        public String AlphabetFile { get { return mAlphabetFile; } set { mAlphabetFile = value; RaisePropertyChanged("AlphabetFile"); } }
        [Category("Font")]
        public SquareSize SquareTextureSize { get { return mSquareTextureSize; } set { mSquareTextureSize = value; RaisePropertyChanged("SquareTextureSize"); } }
        //  public String GeneratedFontImage { get { return mGeneratedFontImage; } set { mGeneratedFontImage = value; RaisePropertyChanged("GeneratedFontImage"); } }
        [Category("Font")]
        public UInt16 OutlineWidth { get { return mOutlineWidth; } set { mOutlineWidth = value; RaisePropertyChanged("OutlineWidth"); } }
        [Category("Font")]
        public UInt16 IntensityModifier { get { return mIntensityModifier; } set { mIntensityModifier = value; RaisePropertyChanged("IntensityModifier"); } }
        [Category("Font")]
        public Point3D GlyphColor { get { return mGlyphColor; } set { mGlyphColor = value; RaisePropertyChanged("GlyphColor"); } }
        [Category("Font")]
        public Point3D OutlineColor { get { return mOutlineColor; } set { mOutlineColor = value; RaisePropertyChanged("OutlineColor"); } }
        [Category("Font")]
        public UInt16 FontSize { get { return mFontSize; } set { mFontSize = value; RaisePropertyChanged("FontSize"); } }
        [Category("Font")]
        public String FontFile { get { return mFontFile; } set { mFontFile = value; RaisePropertyChanged("FontFile"); } }

        [Browsable(false)]
        public String GorillaCode
        {
            get { return mGorillaCode; }
            set
            {
                mGorillaCode = value;
                Document.Text = mGorillaCode;
                RaisePropertyChanged("GorillaCode");
            }
        }

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

        //private void clearGorillaitems()
        //{
        //    var copy = new ObservableCollection<System.Windows.UIElement>(mRectangles);
        //    foreach (var item in copy)
        //    {
        //        mRectangles.Remove(item);
        //    }

        //    m_items.Clear();
        //}

        public void Gen()
        {
        //    clearGorillaitems();

            OIDE.Gorilla.Atlas.COAtlas.GenAtlas(mRectangles, m_items, m_ImageFolder, m_ImageExtensions, Width, Height, this, UnityContainer);


            GorillaCode = OIDE.Gorilla.Helper.FileLoader.GenerateGorillaCode(this);
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

        private IGorillaItem m_SelectedItem;
        [Browsable(false)]
        public IGorillaItem SelectedItem
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
                    SelectedRectangle.Stroke = Brushes.Red;
                    SelectedRectangle.StrokeThickness = 2;
                    SelectedRectangle.Width = value.Width;
                    SelectedRectangle.Height = value.Height;

                    Canvas.SetLeft(SelectedRectangle, value.X);
                    Canvas.SetTop(SelectedRectangle, value.Y);
                }
                m_SelectedItem = value;
                //if (m_SelectedItem != null)
                //{
                //    m_SelectedItem.Rectangle.Stroke = Brushes.Red;
                //    m_SelectedItem.Rectangle.StrokeThickness = 1;
                //}

            }
        }

        [Browsable(false)]
        public CollectionOfIItem Items { get { return m_items; } private set { m_items = value; } }
        public object ContentID { get; private set; }
        [Browsable(false)]
        public bool HasChildren { get; set; }
        [Browsable(false)]
        public bool IsExpanded { get; set; }

        private Boolean m_IsSelected;
        [Browsable(false)]
        public bool IsSelected
        {
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
        public void Drop(IItem item) { }
        public void Finish() { }
        public bool Open(object paramID) { return true; }
        public void Refresh() { }
        public bool Save(object param = null) { return true; }

        #endregion
    }
}