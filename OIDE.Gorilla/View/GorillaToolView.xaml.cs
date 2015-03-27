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
using Module.Properties.Interface;
using Module.Properties.Interface.Services;
using OIDE.Gorilla.Interface.Services;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace OIDE.Gorilla.View
{
    /// <summary>
    /// Interaktionslogik für GorillaToolView.xaml
    /// </summary>
    public partial class GorillaToolView : UserControl, IContentView//, INotifyPropertyChanged
    {
        IPropertiesService mPropertiesService;
        IGorillaService m_GorillaService;
        IUnityContainer mcontainer;
        public GorillaToolView(IUnityContainer container)
        {
            InitializeComponent();


            mcontainer = container;

            mPropertiesService = container.Resolve<IPropertiesService>();

            m_GorillaService = container.Resolve<IGorillaService>();
            //mSceneService.TreeList = _treeList;

        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tv = sender as TreeView;
            var gi = tv.SelectedItem as IItem;
            m_GorillaService.SelectedGorilla.SelectedItem = gi as IGorillaItem;

            mPropertiesService.CurrentItem = gi;
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {

        }

        private void _treeList_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }
    }
}
