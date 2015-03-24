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

namespace OIDE.Animation.Model
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

    public class Texture
    {
        public String file { get; set; }
        public Position whitepixel { get; set; }
    }

    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    public class AnimationModel : TextModel , IItem
    {
        #region private members

        private Rectangle m_SelectedRectangle;
        private UInt16 mOutlineWidth;
        private UInt16 mIntensityModifier;
        private Point3D mGlyphColor;
        private Point3D mOutlineColor;
        private UInt16 mFontSize;
        private String mFontFile;
        //  private String mGeneratedFontImage;
        private String mAnimationCode;
 
        //settings
        private String mAlphabetFile;
        private SquareSize mSquareTextureSize;
        private String m_PathToAnimationFile;
        private String m_PathToFontAnimationFile;
        private String m_ImageExtensions;
        private String m_FontImagePath;
        private String m_ImageFolder;
        private Texture m_Texture;
        private BitmapImage m_fontImage;
      //  private String mFilePath;
        private ObservableCollection<System.Windows.UIElement> mRectangles;
        private CollectionOfIItem m_items;
        private Boolean m_AutoGenFont;

        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="MDModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public AnimationModel(IUnityContainer container, ICommandManager commandManager, IMenuService menuService)
            : base(commandManager, menuService)
        {
            UnityContainer = container;
            mRectangles = new ObservableCollection<System.Windows.UIElement>();
            m_items = new CollectionOfIItem();

            SelectedRectangle = new Rectangle();
            mRectangles.Add(m_SelectedRectangle);
            ImageFolder = @"D:\Projekte\coop\XEngine\data\Test\XETUI\art";
            ImageExtensions = "*.png";
            FontImagePath = @"D:\Projekte\coop\Build\arial.png";
            TextureName = "arial.png";
            Name = "TestAnimationAtlas";
            PathToAnimationFile = @"D:\Projekte\coop\Build\" + Name + ".gorilla";

            PathToFontAnimationFile = @"D:\Projekte\coop\Build\arial.gorilla";

            //StreamReader streamReader = new StreamReader(m_PathToAnimationFile);
            //AnimationCode = streamReader.ReadToEnd();
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
                m_Width = value; TexWidth = (int)value;// clearAnimationitems();
                RaisePropertyChanged("Width");
            }
        }
        [Category("Texture")]
        public SquareSize Height
        {
            get { return m_Height; }
            set
            {
                m_Height = value; TexHeight = (int)value; //clearAnimationitems();
                RaisePropertyChanged("Height");
            }
        }

        [Category("Texture")]
        public Texture AnimationTexture
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
        public String PathToFontAnimationFile { get { return m_PathToFontAnimationFile; } set { m_PathToFontAnimationFile = value; RaisePropertyChanged("PathToFontAnimationFile"); } }
        [Category("Animation")]
        public String PathToAnimationFile { get { return m_PathToAnimationFile; } set { m_PathToAnimationFile = value; RaisePropertyChanged("PathToAnimationFile"); } }
        //  public String FilePath { get { return mFilePath; } set { mFilePath = value; RaisePropertyChanged("FilePath"); } }

        //[Category("Animation")]
        //public ObservableCollection<IAnimationItem> AnimationItems { get { return m_GItems; } set { m_GItems = value; } }
        [Category("Animation")]
        public ObservableCollection<System.Windows.UIElement> Rectangles { get { return mRectangles; } }

        [Category("Animation")]
        [Description("Automatically generate fontdata.")]
        public Boolean AutoGenFont { get { return m_AutoGenFont; } set { m_AutoGenFont = value; RaisePropertyChanged("AutoGenFont"); } }

        [Category("Animation")]
        public String ImageFolder { get { return m_ImageFolder; } set { m_ImageFolder = value; RaisePropertyChanged("ImageFolder"); } }
        [Category("Animation")]
        public String ImageExtensions { get { return m_ImageExtensions; } set { m_ImageExtensions = value; RaisePropertyChanged("ImageExtensions"); } }
        [Category("Font")]
        [Description("FontFile")]
        public String FontImagePath { get { return m_FontImagePath; } set { m_FontImagePath = value; RaisePropertyChanged("FontImagePath"); } }
        public BitmapImage FontImage { get { return m_fontImage; } set { m_fontImage = value; RaisePropertyChanged("FontImage"); } }

        [Category("FontGenerator")]
        [Description("specify an alphabet file. alphabet needs to be ASCII code ascending order.")]
      public String AlphabetFile { get { return mAlphabetFile; } set { mAlphabetFile = value; RaisePropertyChanged("AlphabetFile"); } }
        [Category("FontGenerator")]
     public SquareSize SquareTextureSize { get { return mSquareTextureSize; } set { mSquareTextureSize = value; RaisePropertyChanged("SquareTextureSize"); } }
        //  public String GeneratedFontImage { get { return mGeneratedFontImage; } set { mGeneratedFontImage = value; RaisePropertyChanged("GeneratedFontImage"); } }
        [Category("FontGenerator")]
        [Description("Outline width. Leave to 0 for no outline")]
        public UInt16 OutlineWidth { get { return mOutlineWidth; } set { mOutlineWidth = value; RaisePropertyChanged("OutlineWidth"); } }
        [Category("FontGenerator")]
        [Description("Intensity modifier")]
        public UInt16 IntensityModifier { get { return mIntensityModifier; } set { mIntensityModifier = value; RaisePropertyChanged("IntensityModifier"); } }
        [Category("FontGenerator")]
        [Description("Main glyph color (ex: 0 1 0)")]
      public Point3D GlyphColor { get { return mGlyphColor; } set { mGlyphColor = value; RaisePropertyChanged("GlyphColor"); } }
        [Category("FontGenerator")]
        [Description("Outline color (ex: 0 1 1)")]
      public Point3D OutlineColor { get { return mOutlineColor; } set { mOutlineColor = value; RaisePropertyChanged("OutlineColor"); } }
        [Category("FontGenerator")]
        [Description("Font point size")]
      public UInt16 FontSize { get { return mFontSize; } set { mFontSize = value; RaisePropertyChanged("FontSize"); } }
        [Category("FontGenerator")]
        [Description("Specify input font file (optional)")]
     public String FontFile { get { return mFontFile; } set { mFontFile = value; RaisePropertyChanged("FontFile"); } }

        [Browsable(false)]
        public String AnimationCode
        {
            get { return mAnimationCode; }
            set
            {
                mAnimationCode = value;
                Document.Text = mAnimationCode;
                RaisePropertyChanged("AnimationCode");
            }
        }

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
                psi.Arguments = "-f gorilla -t " + ((int)SquareTextureSize).ToString();
                psi.CreateNoWindow = true;

             //   proz.StartInfo.FileName = @".\fontgen.exe";
                //proz.StartInfo.CreateNoWindow = true;
                //proz.EnableRaisingEvents = true;
                //proz.Exited += new EventHandler(myProcess_Exited);
                System.Diagnostics.Process proccess = System.Diagnostics.Process.Start(psi);
            
                //https://msdn.microsoft.com/de-de/library/h6ak8zt5%28v=vs.110%29.aspx

                proccess.StandardInput.WriteLine("-o 2 -i 2 -s 18 -c \"1 1 0\" -C \"1 0 0\" arial.ttf");
                proccess.StandardInput.WriteLine("-o 1 -i 2 -s 24 -c \"0 0 1\" -C \"1 0 1\" arial.ttf");
                proccess.StandardInput.WriteLine("-o 2 -i 3 -s 36 -c \"1 1 1\" -C \"0 0 0\" arial.ttf");

               
            //    proz.WaitForExit();
                proccess.StandardInput.Close();
             //   proz.Close();

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.None;
                //    bi.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bi.UriSource = new Uri(FontImagePath);
                bi.EndInit();
                FontImage = bi;
         
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void clearAnimationitems()
        {
            var copy = new ObservableCollection<System.Windows.UIElement>(mRectangles);
            foreach (var item in copy)
            {
                mRectangles.Remove(item);
            }

         //   m_items.Clear();
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
                var gorillaService = UnityContainer.Resolve<IAnimationService>();
                gorillaService.SelectedAnimation = this;
            }
        }
        [Browsable(false)]
        public List<System.Windows.Controls.MenuItem> MenuOptions { get; set; }
        public string Name { get; set; }
        [Browsable(false)]
        public IItem Parent { get; set; }

        public bool Create(IUnityContainer unityContainer) { return true; }
        public bool Delete() { return true; }
        public void Drop(IItem item) { }
        public void Finish() { }


        public bool Open(IUnityContainer unityContainer, object paramID) 
        {
            //DAL.Utility.JSONSerializer.Deserialize<AnimationData>(this.Location.ToString());
           
            return true; 
        }

        public void Refresh() { }
        public bool Save(object param = null)
        {
            //DAL.Utility.JSONSerializer.Serialize<AnimationData>(m_AnimationData, param.ToString());
            
            return true; }

        #endregion
    }
}