using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ADock.ViewModel.ProjVM
{
    public class CVMSpriteAnimation : FileViewModel
    {
        private String mFilePath;

        public String FilePath { get { return mFilePath; } set { mFilePath = value; OnPropertyChanged("FilePath"); } }

        public CVMSpriteAnimation()
        {
          
        }
    }
}
