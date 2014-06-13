using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OIDE.Scene.Interface;
using OIDE.Scene.Interface.Events;
using OIDE.Scene.Interface.Services;
using Wide.Interfaces;
using Wide.Interfaces.Controls;
using Wide.Interfaces.Services;
using Module.Properties.Interface;
using System.ComponentModel;

namespace OIDE.Scene.Service
{
    /// <summary>
    /// The main project tree manager
    /// </summary>
    internal sealed class SceneManager : ISceneService
    {
        public System.Windows.Controls.ContextMenu ContextMenu { get; private set; }

        MenuItemViewModel menuItem;

        /// <summary>
        /// The injected event aggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The injected logger
        /// </summary>
        private readonly ILoggerService _logger;



        /// <summary>
        /// The theme manager constructor
        /// </summary>
        /// <param name="eventAggregator">The injected event aggregator</param>
        /// <param name="logger">The injected logger</param>
        public SceneManager(IEventAggregator eventAggregator, ILoggerService logger)
        {
            Items = new ObservableCollection<ISceneItem>();
            _eventAggregator = eventAggregator;
            _logger = logger;
            //Service für project contextmenu buttons .....
            ContextMenu = new ContextMenu();

            MenuItem mib1a = new MenuItem();
            mib1a.Header = "Text.xaml";
            ContextMenu.Items.Add(mib1a);
        }

        

        /// <summary>
        /// The current item selected
        /// </summary>
        private ISceneItem mSelectedItem;
        public ISceneItem SelectedItem
        {
            get
            {
            return mSelectedItem; } set { mSelectedItem = value; } }

        private ISceneItem mRootItem;
        /// <summary>
        /// Root Item
        /// </summary>
        public ISceneItem RootItem
        {
            get { return mRootItem; }
            set
            {
                if (RootItem != value)
                {
                   //TreeList.RootItem = value;
                   //TreeList.Root.Children.Clear();
                   //TreeList.Rows.Clear();
                   //TreeList.CreateChildrenNodes(TreeList.Root);
                   mRootItem = value;
                }
            }
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if successfully added, <c>false</c> otherwise</returns>
        //public override string Add(AbstractCommandable item)
        //{
        //    AbstractToolbar tb = item as AbstractToolbar;
        //    if (tb != null)
        //    {
        //        tb.IsCheckable = true;
        //        tb.IsChecked = true;
        //    }
        //    return base.Add(item);
        //}

        public AbstractMenuItem RightClickMenu
        {
            get
            {
                if (menuItem == null)
                {
                    menuItem = new MenuItemViewModel("_ProjFile", 100);
                    foreach (var value in ContextMenu.ItemsSource)
                    {
                        var menu = value as AbstractMenuItem;
                        menuItem.Add(menu);
                    }
                }
                return menuItem;
            }
        }


        #region IProjectTreeService Members

        /// <summary>
        /// collection of treeview items
        /// </summary>
        public ObservableCollection<ISceneItem> Items { get; internal set; }


        /// <summary>
        /// Set the current item
        /// </summary>
        /// <param name="guid">The guid of the item</param>
        /// <returns>true if the new item is set, false otherwise</returns>
        //public bool SetCurrent(Guid guid)
        //{
        //    var item = Items.Where(x => x.Guid == guid);
        //    if (item.Any())
        //    {
        //        IItem newItem = item.First();
        //        CurrentItem = newItem;

        //        //ResourceDictionary theme = Application.Current.MainWindow.Resources.MergedDictionaries[0];
        //        //ResourceDictionary appTheme = Application.Current.Resources.MergedDictionaries.Count > 0
        //        //                                  ? Application.Current.Resources.MergedDictionaries[0]
        //        //                                  : null;
        //        //theme.BeginInit();
        //        //theme.MergedDictionaries.Clear();
        //        //if (appTheme != null)
        //        //{
        //        //    appTheme.BeginInit();
        //        //    appTheme.MergedDictionaries.Clear();
        //        //}
        //        //else
        //        //{
        //        //    appTheme = new ResourceDictionary();
        //        //    appTheme.BeginInit();
        //        //    Application.Current.Resources.MergedDictionaries.Add(appTheme);
        //        //}
        //        //foreach (Uri uri in newTheme.UriList)
        //        //{
        //        //    ResourceDictionary newDict = new ResourceDictionary {Source = uri};
        //        //    /*AvalonDock and menu style needs to move to the application
        //        //     * 1. AvalonDock needs global styles as floatable windows can be created
        //        //     * 2. Menu's need global style as context menu can be created
        //        //    */
        //        //    if (uri.ToString().Contains("AvalonDock") ||
        //        //        uri.ToString().Contains("Wide;component/Interfaces/Styles/VS2012/Menu.xaml"))
        //        //    {
        //        //        appTheme.MergedDictionaries.Add(newDict);
        //        //    }
        //        //    else
        //        //    {
        //        //        theme.MergedDictionaries.Add(newDict);
        //        //    }
        //        //}
        //        //appTheme.EndInit();
        //        //theme.EndInit();
        //        _logger.Log("projectfile item set to " + newItem.Name, LogCategory.Info, LogPriority.None);
        //        _eventAggregator.GetEvent<ItemChangeEvent>().Publish(newItem);
        //    }
        //    return false;
        //}

        /// <summary>
        /// Adds a base item to the project-file manager
        /// </summary>
        /// <param name="theme">The item to add</param>
        /// <returns>true, if successful - false, otherwise</returns>
        public bool AddItem(ISceneItem item)
        {
            if (!Items.Contains(item))
            {
                Items.Add(item);
                return true;
            }
            return false;
        }

        #endregion
    }
}
