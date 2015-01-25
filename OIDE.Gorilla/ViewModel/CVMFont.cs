
using System;
using Interface;
namespace ADock.ViewModel.ProjVM
{
    public class CVMFont : ViewModelBase
    {
        private UInt16 mOutlineWidth;
        private UInt16 mIntensityModifier;
        private Point3D mGlyphColor;
        private Point3D mOutlineColor;
        private UInt16 mFontSize;
        private String mFontFile;


        public UInt16 OutlineWidth { get { return  mOutlineWidth;} set { mOutlineWidth = value; OnPropertyChanged("OutlineWidth");}}
        public UInt16 IntensityModifier { get { return mIntensityModifier; } set { mIntensityModifier = value; OnPropertyChanged("IntensityModifier"); } }
        public Point3D GlyphColor { get { return mGlyphColor; } set { mGlyphColor = value; OnPropertyChanged("GlyphColor"); } }
        public Point3D OutlineColor { get { return mOutlineColor; } set { mOutlineColor = value; OnPropertyChanged("OutlineColor"); } }
        public UInt16 FontSize { get { return mFontSize; } set { mFontSize = value; OnPropertyChanged("FontSize"); } }
        public String FontFile { get { return mFontFile; } set { mFontFile = value; OnPropertyChanged("FontFile"); } }

        public CVMFont()
        {
          
        }
    }
}
