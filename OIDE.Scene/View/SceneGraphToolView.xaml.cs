using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Module.Properties.Interface.Services;
using OIDE.Scene.Interface.Services;
using Module.Scene;

namespace OIDE.Scene.View
{
    /// <summary>
    /// Interaktionslogik für SceneGraphToolView.xaml
    /// </summary>
    public partial class SceneGraphToolView :UserControl, IContentView, INotifyPropertyChanged
    {
         IPropertiesService mPropertiesService;
        ISceneService mSceneService;

        public SceneGraphToolView(IUnityContainer container)
        {
            InitializeComponent();
       
            mPropertiesService = container.Resolve<IPropertiesService>();

            mSceneService = container.Resolve<ISceneService>();
            mSceneService.TreeList = _treeList;
           
        }

        /// <summary>
        /// Should be called when a property value has changed
        /// </summary>
        /// <param name="propertyName">The property name</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            //standardOutput.AutoFlush = true;
            //Console.SetOut(standardOutput);
        }

        //doesnt trigger?
        //private void ContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        //{
        //  ContextMenu tmp =  (ContextMenu)sender;
        //  tmp.ItemsSource = mSceneService.CurrentItem.MenuOptions;
        //}

        private void ContextMenu_Opened(object sender, System.Windows.RoutedEventArgs e)
        {
            ContextMenu tmp = (ContextMenu)sender;

            //multiple items selected
            if (_treeList.SelectedItems.Count > 1)
            {
                List<MenuItem> lmi = new List<MenuItem>();
                lmi.Add(new MenuItem() { Header = "Multiselected!" });
                tmp.ItemsSource = lmi;
            }
            //one item selected
            else if(_treeList.SelectedItem != null)
            {
                tmp.ItemsSource = mSceneService.SelectedItem.MenuOptions;
            }

            //nothing selected          
        }

        private void _treeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TreeList tmp = (TreeList)sender;
            if (tmp != null)
            {
                TreeNode tn = tmp.SelectedNode;
                if (tn != null)
                {
                    mSceneService.SelectedItem = (ISceneItem)tn.Tag;
                    mPropertiesService.CurrentItem = (ISceneItem)tn.Tag;
                }
            }
        }

        private void _treeList_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (mSceneService.SelectedItem == null || mSceneService.SelectedItem.MenuOptions == null)
            {
                e.Handled = true;
            }
        }

    }
}
