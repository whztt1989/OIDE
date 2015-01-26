
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Wide.Interfaces;
namespace ADock.ViewModel.ProjVM
{


    public class CVMUITemplates : ViewModelBase
    {
        private String mAlphabetFile;
        private SquareSize mSquareTextureSize;
        private String mGeneratedFontImage;
        private ObservableCollection<ViewModelBase> mFonts;

        public CVMUITemplates()
        {
            mFonts = new ObservableCollection<ViewModelBase>();
        }
       
        public ObservableCollection<ViewModelBase> Items { get { return mFonts; } }
        public String AlphabetFile { get { return mAlphabetFile; } set { mAlphabetFile = value; RaisePropertyChanged("AlphabetFile"); } }
        public SquareSize SquareTextureSize { get { return mSquareTextureSize; } set { mSquareTextureSize = value; RaisePropertyChanged("SquareTextureSize"); } }
        public String GeneratedFontImage { get { return mGeneratedFontImage; } set { mGeneratedFontImage = value; RaisePropertyChanged("GeneratedFontImage"); } }
  
    }

}
