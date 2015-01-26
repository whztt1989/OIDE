
using System;
using System.Windows.Media.Media3D;
using Wide.Interfaces;

namespace ADock.ViewModel.ProjVM
{
    public class CVMUITemplate : ViewModelBase
    {
        private UInt16 mOutlineWidth;
        private UInt16 mIntensityModifier;
        private Point3D mGlyphColor;
        private Point3D mOutlineColor;
        private UInt16 mFontSize;
        private String mFontFile;


        public UInt16 OutlineWidth { get { return  mOutlineWidth;} set { mOutlineWidth = value; RaisePropertyChanged("OutlineWidth");}}
        public UInt16 IntensityModifier { get { return mIntensityModifier; } set { mIntensityModifier = value; RaisePropertyChanged("IntensityModifier"); } }
        public Point3D GlyphColor { get { return mGlyphColor; } set { mGlyphColor = value; RaisePropertyChanged("GlyphColor"); } }
        public Point3D OutlineColor { get { return mOutlineColor; } set { mOutlineColor = value; RaisePropertyChanged("OutlineColor"); } }
        public UInt16 FontSize { get { return mFontSize; } set { mFontSize = value; RaisePropertyChanged("FontSize"); } }
        public String FontFile { get { return mFontFile; } set { mFontFile = value; RaisePropertyChanged("FontFile"); } }

        public CVMUITemplate()
        {
          
        }
    }
}
