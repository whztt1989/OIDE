#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

// modified by Konrad Huber

#endregion

using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Controls;
using OIDE.SocketServer.Settings;
using System.Windows;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;
using Wide.Interfaces.Settings;
using Wide.Interfaces.Themes;

namespace OIDE.SocketServer
{
    [Module(ModuleName = "OIDE.SocketServer")]
    [ModuleDependency("Module.Tools.Logger")]
   // [ModuleDependency("Module.Search")]
    [ModuleDependency("Module.Properties")]
    [ModuleDependency("Module.Tools.Output")]
    public class CoreModule : IModule
    {
        private IUnityContainer _container;
        private IEventAggregator _eventAggregator;

        public CoreModule(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            _eventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent
                                                                              {Message = "Loading Core Module"});
            
            LoadCommands();
            LoadMenus();
            LoadToolbar();
            RegisterParts();
            LoadSettings();
        }

        private void LoadToolbar()
        {
            _eventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent
                                                                              {Message = "Toolbar.."});
            var toolbarService = _container.Resolve<IToolbarService>();
            var menuService = _container.Resolve<IMenuService>();

            //toolbarService.Add(new ToolbarViewModel("Standard", 1) {Band = 0, BandIndex = 0});
            //toolbarService.Get("Standard").Add(menuService.Get(Resources._File).Get(Resources._New));
            //toolbarService.Get("Standard").Add(menuService.Get(Resources._File).Get(Resources._Open));
            //toolbarService.Get("Standard").Add(menuService.Get(Resources._File).Get(Resources._Save));

            //toolbarService.Add(new ToolbarViewModel(Resources.Edit, 1) {Band = 0, BandIndex = 1});
            //toolbarService.Get(Resources.Edit).Add(menuService.Get(Resources._Edit).Get(Resources._Undo));
            //toolbarService.Get(Resources.Edit).Add(menuService.Get(Resources._Edit).Get(Resources._Redo));
            //toolbarService.Get(Resources.Edit).Add(MenuItemViewModel.Separator(3));
            //toolbarService.Get(Resources.Edit).Add(menuService.Get(Resources._Edit).Get(Resources.Cut));
            //toolbarService.Get(Resources.Edit).Add(menuService.Get(Resources._Edit).Get(Resources.Copy));
            //toolbarService.Get(Resources.Edit).Add(menuService.Get(Resources._Edit).Get(Resources._Paste));

            //menuService.Get(Resources._Tools).Add(toolbarService.RightClickMenu);

            //toolbarService.ToolBarTrayVisible = false;

            //Initiate the position settings changes for toolbar
            _container.Resolve<IToolbarPositionSettings>();
        }

        private void LoadSettings()
        {
            ISettingsManager manager = _container.Resolve<ISettingsManager>();
            //manager.Add(new SocketServerSettingsItem("Text Editor", 1, null));
            //manager.Get("Text Editor").Add(new SocketServerSettingsItem("General", 1, EditorOptions.Default));
        }

        private void RegisterParts()
        {
            _container.RegisterType<SocketServerHandler>();
            _container.RegisterType<SocketServerViewModel>();
            _container.RegisterType<SocketServerView>();

            IContentHandler handler = _container.Resolve<SocketServerHandler>();
            _container.Resolve<IContentHandlerRegistry>().Register(handler);
        }



        private void LoadCommands()
        {
            _eventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent
                                                                              {Message = "Commands.."});
            var manager = _container.Resolve<ICommandManager>();

            //var openCommand = new DelegateCommand(OpenModule);
            //var exitCommand = new DelegateCommand(CloseCommandExecute);
            //var saveCommand = new DelegateCommand(SaveDocument, CanExecuteSaveDocument);
            //var saveAsCommand = new DelegateCommand(SaveAsDocument, CanExecuteSaveAsDocument);
            //var themeCommand = new DelegateCommand<string>(ThemeChangeCommand);
            //var loggerCommand = new DelegateCommand(ToggleLogger);
            //var OutputCommand = new DelegateCommand(ToggleOutput);


            //manager.RegisterCommand("OPEN", openCommand);
            //manager.RegisterCommand("SAVE", saveCommand);
            //manager.RegisterCommand("SAVEAS", saveAsCommand);
            //manager.RegisterCommand("EXIT", exitCommand);
            //manager.RegisterCommand("LOGSHOW", loggerCommand);
            //manager.RegisterCommand("OUTPUTSHOW", OutputCommand);
            // manager.RegisterCommand("THEMECHANGE", themeCommand);
        }

        private void CloseCommandExecute()
        {
            IShell shell = _container.Resolve<IShell>();
            shell.Close();
        }

        private void LoadMenus()
        {
            _eventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent
                                                                              {Message = "Menus.."});
            var manager = _container.Resolve<ICommandManager>();
            var menuService = _container.Resolve<IMenuService>();
            var settingsManager = _container.Resolve<ISettingsManager>();
            var themeSettings = _container.Resolve<IThemeSettings>();
            var recentFiles = _container.Resolve<IRecentViewSettings>();
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            ToolViewModel logger = workspace.Tools.First(f => f.ContentId == "Logger");
            ToolViewModel Output = workspace.Tools.First(f => f.ContentId == "Output");
       //   ToolViewModel searchView = workspace.Tools.First(f => f.ContentId == "Search");
            ToolViewModel propertiesView = workspace.Tools.First(f => f.ContentId == "Properties");

           //in xaml {x:Static avalonDockProperties:Resources.Anchorable_AutoHide}
 //           menuService.Add(new MenuItemViewModel(Resources._File, 1));

 //           menuService.Get(Resources._File).Add(
 //               (new MenuItemViewModel(Resources._New, 3,
 //                                      new BitmapImage(
 //                                          new Uri(
 //                                              @"pack://application:,,,/OIDE.SocketServer;component/Icons/NewRequest_8796.png")),
 //                                      manager.GetCommand("NEW"),
 //                                      new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N"))));

 //           menuService.Get(Resources._File).Add(
 //               (new MenuItemViewModel(Resources._Open, 4,
 //                                      new BitmapImage(
 //                                          new Uri(
 //                                              @"pack://application:,,,/OIDE.SocketServer;component/Icons/OpenFileDialog_692.png")),
 //                                      manager.GetCommand("OPEN"),
 //                                      new KeyGesture(Key.O, ModifierKeys.Control, "Ctrl + O"))));
 //           menuService.Get(Resources._File).Add(new MenuItemViewModel(Resources._Save, 5,
 //                                                              new BitmapImage(
 //                                                                  new Uri(
 //                                                                      @"pack://application:,,,/OIDE.SocketServer;component/Icons/Save_6530.png")),
 //                                                              manager.GetCommand("SAVE"),
 //                                                              new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl + S")));
 //           menuService.Get(Resources._File).Add(new SaveAsMenuItemViewModel(Resources.SAVEAS, 6,
 //                                                  new BitmapImage(
 //                                                      new Uri(
 //                                                          @"pack://application:,,,/OIDE.SocketServer;component/Icons/Save_6530.png")),
 //                                                  manager.GetCommand("SAVEAS"),null,false,false,_container));

 //           menuService.Get(Resources._File).Add(new MenuItemViewModel(Resources.Close, 8, null, manager.GetCommand("CLOSE"),
 //                                                              new KeyGesture(Key.F4, ModifierKeys.Control, "Ctrl + F4")));

 ////##          menuService.Get(Resources._File).Add(recentFiles.RecentMenu);

 //           menuService.Get(Resources._File).Add(new MenuItemViewModel(Resources.E_xit, 101, null, manager.GetCommand("EXIT"),
 //                                                              new KeyGesture(Key.F4, ModifierKeys.Alt, "Alt + F4")));


 //           menuService.Add(new MenuItemViewModel(Resources._Edit, 2));
 //           menuService.Get(Resources._Edit).Add(new MenuItemViewModel(Resources._Undo, 1,
 //                                                              new BitmapImage(
 //                                                                  new Uri(
 //                                                                      @"pack://application:,,,/OIDE.SocketServer;component/Icons/Undo_16x.png")),
 //                                                              ApplicationCommands.Undo));
 //           menuService.Get(Resources._Edit).Add(new MenuItemViewModel(Resources._Redo, 2,
 //                                                              new BitmapImage(
 //                                                                  new Uri(
 //                                                                      @"pack://application:,,,/OIDE.SocketServer;component/Icons/Redo_16x.png")),
 //                                                              ApplicationCommands.Redo));
 //           menuService.Get(Resources._Edit).Add(MenuItemViewModel.Separator(15));
 //           menuService.Get(Resources._Edit).Add(new MenuItemViewModel(Resources.Cut, 20,
 //                                                              new BitmapImage(
 //                                                                  new Uri(
 //                                                                      @"pack://application:,,,/OIDE.SocketServer;component/Icons/Cut_6523.png")),
 //                                                              ApplicationCommands.Cut));
 //           menuService.Get(Resources._Edit).Add(new MenuItemViewModel(Resources.Copy, 21,
 //                                                              new BitmapImage(
 //                                                                  new Uri(
 //                                                                      @"pack://application:,,,/OIDE.SocketServer;component/Icons/Copy_6524.png")),
 //                                                              ApplicationCommands.Copy));
 //           menuService.Get(Resources._Edit).Add(new MenuItemViewModel(Resources._Paste, 22,
 //                                                              new BitmapImage(
 //                                                                  new Uri(
 //                                                                      @"pack://application:,,,/OIDE.SocketServer;component/Icons/Paste_6520.png")),
 //                                                              ApplicationCommands.Paste));

 //           menuService.Add(new MenuItemViewModel(Resources._View, 3));
       
 //           if (logger != null)
 //               menuService.Get(Resources._View).Add(new MenuItemViewModel(Resources._Logger, 2,
 //                   //new BitmapImage(
 //                   //    new Uri(
 //                   //        @"pack://application:,,,/ComID.Core;component/Icons/Undo_16x.png"))
 //                                                                        null,
 //                                                                  manager.GetCommand("LOGSHOW")) { IsCheckable = true, IsChecked = logger.IsVisible });
 //           if (Output != null)
 //               menuService.Get(Resources._View).Add(new MenuItemViewModel(Resources._Output, 2,
 //                   //new BitmapImage(
 //                   //    new Uri(
 //                   //        @"pack://application:,,,/ComID.Core;component/Icons/Undo_16x.png"))
 //                                                                        null,
 //                                                                  manager.GetCommand("OUTPUTSHOW")) { IsCheckable = true, IsChecked = Output.IsVisible });


 //           //if (searchView != null)
 //           //    menuService.Get("_View").Add(new MenuItemViewModel("_Search", 2,
 //           //        //new BitmapImage(
 //           //        //    new Uri(
 //           //        //        @"pack://application:,,,/ComID.Core;component/Icons/Undo_16x.png"))
 //           //                                                             null,
 //           //                                                       manager.GetCommand("SEARCHSHOW")) { IsCheckable = true, IsChecked = searchView.IsVisible });
 //           if (propertiesView != null)
 //               menuService.Get(Resources._View).Add(new MenuItemViewModel(Resources._Properties, 3,
 //                   //new BitmapImage(
 //                   //    new Uri(
 //                   //        @"pack://application:,,,/ComID.Core;component/Icons/Undo_16x.png"))
 //                                                                        null,
 //                                                                  manager.GetCommand("PROPERTIESSHOW")) { IsCheckable = true, IsChecked = propertiesView.IsVisible });

 //           menuService.Get(Resources._View).Add(new MenuItemViewModel(Resources.Themes, 1));

 //           //Set the checkmark of the theme menu's based on which is currently selected
 //           //menuService.Get("_View").Get("Themes").Add(new MenuItemViewModel("Dark", 1, null,
 //           //                                                                 manager.GetCommand("THEMECHANGE"))
 //           //                                               {
 //           //                                                   IsCheckable = true,
 //           //                                                   IsChecked = (themeSettings.SelectedTheme == "Dark"),
 //           //                                                   CommandParameter = "Dark"
 //           //                                               });
 //           menuService.Get(Resources._View).Get(Resources.Themes).Add(new MenuItemViewModel("Light", 2, null,
 //                                                                            manager.GetCommand("THEMECHANGE"))
 //                                                          {
 //                                                              IsCheckable = true,
 //                                                              IsChecked = (themeSettings.SelectedTheme == "Light"),
 //                                                              CommandParameter = "Light"
 //                                                          });

 //           menuService.Add(new MenuItemViewModel(Resources._Tools, 4));
 //           menuService.Get(Resources._Tools).Add(new MenuItemViewModel(Resources.Settings, 1, null, settingsManager.SettingsCommand));

 //           menuService.Add(new MenuItemViewModel(Resources._Help, 4));
        }

        #region Commands

        #region Open

        private void OpenModule()
        {
            var service = _container.Resolve<IOpenDocumentService>();
            service.Open();
        }

        #endregion

        #region Save

        private bool CanExecuteSaveDocument()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            if (workspace.ActiveDocument != null
                && workspace.ActiveDocument.Model != null)
            {
                return workspace.ActiveDocument.Model.IsDirty;
            }
            return false;
        }

        private bool CanExecuteSaveAsDocument()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            return (workspace.ActiveDocument != null) ;
        }

        private void SaveDocument()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            ICommandManager manager = _container.Resolve<ICommandManager>();
            workspace.ActiveDocument.Handler.SaveContent(workspace.ActiveDocument);
            manager.Refresh();
        }

        private void SaveAsDocument()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            ICommandManager manager = _container.Resolve<ICommandManager>();
            if (workspace.ActiveDocument != null)
            {
                workspace.ActiveDocument.Handler.SaveContent(workspace.ActiveDocument, true);
                manager.Refresh();
            }
        }
        #endregion

        #endregion
    }
}