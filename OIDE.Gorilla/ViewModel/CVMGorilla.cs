using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace ADock.ViewModel.ProjVM
{
    public class CVMGorilla : FileViewModel
    {
        private String mFilePath;
        private ObservableCollection<System.Windows.UIElement> mRectangles;
        ObservableCollection<ViewModelBase> mImages;

       
        public String FilePath { get { return mFilePath; } set { mFilePath = value; OnPropertyChanged("FilePath"); } }
        public ObservableCollection<ViewModelBase> Items { get { return mImages; } }    
        public ObservableCollection<System.Windows.UIElement> Rectangles { get { return mRectangles; } }  

        public CVMGorilla()
        {
            mRectangles = new ObservableCollection<System.Windows.UIElement>();
            mImages = new ObservableCollection<ViewModelBase>();

        //    test = Workspace.This.VMTV.SelectedElement;
        }

        public void Gen()
        {
            CLGorilla.Documents.Atlas.COAtlas.GenAtlas(mRectangles, mImages);

        }
    }
}
