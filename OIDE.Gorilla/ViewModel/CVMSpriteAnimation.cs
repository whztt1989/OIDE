using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wide.Interfaces;

namespace ADock.ViewModel.ProjVM
{
    public class CVMSpriteAnimation : ViewModelBase
    {
        private String mFilePath;

        public String FilePath { get { return mFilePath; } set { mFilePath = value; RaisePropertyChanged("FilePath"); } }

        public CVMSpriteAnimation()
        {
          
        }
    }
}
