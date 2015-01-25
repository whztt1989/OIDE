
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace ADock.ViewModel.ProjVM
{


    public class CVMFontSettings : FileViewModel
    {
        private String mAlphabetFile;
        private SquareSize mSquareTextureSize;
        private String mGeneratedFontImage;
        private ObservableCollection<ViewModelBase> mFonts;

        public CVMFontSettings()
        {
            mFonts = new ObservableCollection<ViewModelBase>();
        }
       
        public ObservableCollection<ViewModelBase> Items { get { return mFonts; } }
        public String AlphabetFile { get { return mAlphabetFile; } set { mAlphabetFile = value; OnPropertyChanged("AlphabetFile"); } }
        public SquareSize SquareTextureSize { get { return mSquareTextureSize; } set { mSquareTextureSize = value; OnPropertyChanged("SquareTextureSize"); } }
        public String GeneratedFontImage { get { return mGeneratedFontImage; } set { mGeneratedFontImage = value; OnPropertyChanged("GeneratedFontImage"); } }
  
    }

    public enum SquareSize
    {
        SS_256 = 0,
        SS_512 = 1,
        SS_1024 = 2,
    };
}
