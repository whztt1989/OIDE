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
using OIDE.Animation.Interface.Services;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace OIDE.Animation.View
{
    /// <summary>
    /// Interaktionslogik für AnimationToolView.xaml
    /// </summary>
    public partial class AnimationToolView : UserControl, IContentView//, INotifyPropertyChanged
    {
        IPropertiesService mPropertiesService;
        IAnimationService m_AnimationService;
        IUnityContainer mcontainer;
        public AnimationToolView(IUnityContainer container)
        {
            InitializeComponent();


            mcontainer = container;

            mPropertiesService = container.Resolve<IPropertiesService>();

            m_AnimationService = container.Resolve<IAnimationService>();
            //mSceneService.TreeList = _treeList;

        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tv = sender as TreeView;
            var gi = tv.SelectedItem as IItem;
            m_AnimationService.SelectedAnimation.SelectedItem = gi as IAnimationItem;

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
