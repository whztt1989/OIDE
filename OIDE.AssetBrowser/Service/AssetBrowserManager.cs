﻿using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OIDE.AssetBrowser.Interface;
using OIDE.AssetBrowser.Interface.Events;
using OIDE.AssetBrowser.Interface.Services;
using Wide.Interfaces;
using Wide.Interfaces.Controls;
using Wide.Interfaces.Services;
using Module.Properties.Interface;
using System.ComponentModel;

namespace OIDE.AssetBrowser.Service
{
    /// <summary>
    /// The main project tree manager
    /// </summary>
    internal sealed class AssetBrowserTreeManager : IAssetBrowserTreeService
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

        public TreeList TreeList { get; set; }

        /// <summary>
        /// The theme manager constructor
        /// </summary>
        /// <param name="eventAggregator">The injected event aggregator</param>
        /// <param name="logger">The injected logger</param>
        public AssetBrowserTreeManager(IEventAggregator eventAggregator, ILoggerService logger)
        {
       //     Items = new CollectionOfIItem();
            _eventAggregator = eventAggregator;
            _logger = logger;
            //Service für project contextmenu buttons .....
            ContextMenu = new ContextMenu();

            MenuItem mib1a = new MenuItem();
            mib1a.Header = "Text.xaml";
            ContextMenu.Items.Add(mib1a);
        }


        public void SetAsRoot(IItem item)
        {
           // var mAssetBrowserTreeService = _container.Resolve<IAssetBrowserTreeService>();
            //var commandManager = _container.Resolve<ICommandManager>();
           // var menuService = _container.Resolve<IMenuService>();
           // var dbService = _container.Resolve<IDatabaseService>();

             

            //---------------------------------------------
            //Projekt Tree
            //---------------------------------------------
            //todo nur ein Projekt!!
            //ptS.SetAssetBrowser();
            Items.Clear();
          //  RootItem = null;

          //  FileCategoryModel root = new FileCategoryModel(null, null) { Name = "RootNode" };
            //AuftragModel order = new AuftragModel(root, dbS) { Name = "Auftrag Neu", IsExpanded = true };
          //  root.Items.Add(item);
            Items.Add(item);
      //##      RootItem = item;
        }


        /// <summary>
        /// The current item selected
        /// </summary>
        private IItem mSelectedItem;
        public IItem SelectedItem { get {
            return mSelectedItem; } set { mSelectedItem = value; } }

        //private IItem mRootItem;
        ///// <summary>
        ///// Root Item
        ///// </summary>
        //public IItem RootItem
        //{
        //    get { return mRootItem; }
        //    private set
        //    {
        //        if (RootItem != value)
        //        {
        //           TreeList.RootItem = value;
        //           TreeList.Root.Children.Clear();
        //           TreeList.Rows.Clear();
        //           TreeList.CreateChildrenNodes(TreeList.Root);
        //           mRootItem = value;
        //        }
        //    }
        //}

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


        #region IAssetBrowserTreeService Members

        public AssetBrowserToolModel ABTM { get; set; }

        /// <summary>
        /// collection of project items
        /// </summary>
        public CollectionOfIItem Items { get { return ABTM.Items; } set { ABTM.Items = value; } }


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
        public bool AddItem(IItem item)
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
