#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Controls;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;
using Wide.Interfaces.Settings;
using System.Windows;
using OIDE.AssetBrowser.Interface.Services;
using OIDE.AssetBrowser.View;
using OIDE.AssetBrowser.Service;

namespace OIDE.AssetBrowser
{
    [Module(ModuleName = "OIDE.AssetBrowser")]
    [ModuleDependency("Module.Properties")]
    [ModuleDependency("Module.Tools.Logger")]
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
                                                                              {Message = "Loading AssetBrowser Module"});
            LoadTheme();
            LoadCommands();
            LoadMenus();
            LoadToolbar();
            RegisterParts();
            LoadSettings();
        }

        private void LoadInterface()
        {
            var projTreeService = _container.Resolve<IAssetBrowserTreeService>();

            var menuService = _container.Resolve<IMenuService>();

            //projTreeService.Add(new ToolbarViewModel("Edit", 1) { Band = 1, BandIndex = 2 });
            //projTreeService.Get("Edit").Add(menuService.Get("_Edit").Get("_Undo"));
            //projTreeService.Get("Edit").Add(menuService.Get("_Edit").Get("_Redo"));
            //projTreeService.Get("Edit").Add(menuService.Get("_Edit").Get("Cut"));
            //projTreeService.Get("Edit").Add(menuService.Get("_Edit").Get("Copy"));
            //projTreeService.Get("Edit").Add(menuService.Get("_Edit").Get("_Paste"));

            
            menuService.Get("_ProjFile").Add(projTreeService.RightClickMenu);
        }

        private void LoadToolbar()
        {
            _eventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent {Message = "Toolbar.."});
           var manager = _container.Resolve<ICommandManager>();
           
            var toolbarService = _container.Resolve<IToolbarService>();
            var menuService = _container.Resolve<IMenuService>();


            //toolbarService.Add(new ToolbarViewModel("Standard", 1) {Band = 1, BandIndex = 1});
            //toolbarService.Get("Standard").Add(menuService.Get("_File").Get("_New"));
            //toolbarService.Get("Standard").Add(menuService.Get("_File").Get("_Open"));

            //toolbarService.Add(new ToolbarViewModel("Edit", 1) {Band = 1, BandIndex = 2});
            //toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("_Undo"));
            //toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("_Redo"));
            //toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("Cut"));
            //toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("Copy"));
            //toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("_Paste"));

            ////  <Button Content="Open" Name="btnOpen" Click="btnOpen_Click" />
            ////<Button Content="Status" Name="btnStatus" Click="btnStatus_Click" />
            ////<Button Content="Eject" Name="btnEject" Click="btnEject_Click" />
            ////<Button Content="Close" Name="btnClose" Click="btnClose_Click" />

            ////<Button Content="IC Reset" Name="btnICReset" Click="btnICReset_Click" />
            ////<Button Content="IC Card Select" Name="btnICCardSelect" Click="btnICCardSelect_Click" />

            //toolbarService.Add(new ToolbarViewModel("EC", 1) { Band = 1, BandIndex = 3 });

            //MenuItemViewModel test = new MenuItemViewModel("OpenCOM", 0,
            //                           new BitmapImage(new Uri(@"pack://application:,,,/OIDE.AssetBrowser;component/Icons/NewRequest_8796.png")),
            //                           manager.GetCommand("OPENCOM"),
            //                           new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N"));
          

            //toolbarService.Get("EC").Add(test);

            //toolbarService.Get("EC").Add(new MenuItemViewModel("Status", 1,
            //                           new BitmapImage(new Uri(@"pack://application:,,,/OIDE.AssetBrowser;component/Icons/NewRequest_8796.png")),
            //                           manager.GetCommand("ECSTATUS"),
            //                           new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N")));
            //toolbarService.Get("EC").Add(new MenuItemViewModel("Auswurf", 2,
            //                           new BitmapImage(
            //                               new Uri(@"pack://application:,,,/OIDE.AssetBrowser;component/Icons/NewRequest_8796.png")),
            //                           manager.GetCommand("ECEJECT"),
            //                           new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N")));
            //toolbarService.Get("EC").Add(new MenuItemViewModel("Close", 3,
            //                           new BitmapImage(
            //                               new Uri(@"pack://application:,,,/OIDE.AssetBrowser;component/Icons/NewRequest_8796.png")),
            //                           manager.GetCommand("ECCLOSE"),
            //                           new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N")));
            //toolbarService.Get("EC").Add(new MenuItemViewModel("ICDirectControl", 4,
            //                           new BitmapImage(
            //                               new Uri(@"pack://application:,,,/OIDE.AssetBrowser;component/Icons/NewRequest_8796.png")),
            //                           manager.GetCommand("ECECDIRECTCTRL"),
            //                           new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N")));
            //toolbarService.Get("EC").Add(new MenuItemViewModel("ICCSel", 5,
            //                           new BitmapImage(
            //                               new Uri(@"pack://application:,,,/OIDE.AssetBrowser;component/Icons/NewRequest_8796.png")),
            //                           manager.GetCommand("ICCSel"),
            //                           new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N")));


            //menuService.Get("_Tools").Add(toolbarService.RightClickMenu);

            //Initiate the position settings changes for toolbar
            _container.Resolve<IToolbarPositionSettings>();
        }

        private void LoadSettings()
        {
            //ISettingsManager manager = _container.Resolve<ISettingsManager>();
            //manager.Add(new MDSettingsItem("Text Editor", 1, null));
            //manager.Get("Text Editor").Add(new MDSettingsItem("General", 1, EditorOptions.Default));
        }

        private void RegisterParts()
        {
            //------------------------------------------------------
            // REgister AssetBrowser-FileExplorer
            //------------------------------------------------------
            _container.RegisterType<IAssetBrowserTreeService, AssetBrowserTreeManager>(new ContainerControlledLifetimeManager());

            //------------------------------------------------------
            //Register File Type
            //------------------------------------------------------
            //_container.RegisterType<AssetBrowserHandler>();
            //_container.RegisterType<AssetBrowserViewModel>();
            //_container.RegisterType<AssetBrowserView>();

            //IContentHandler handler = _container.Resolve<AssetBrowserHandler>();
            //_container.Resolve<IContentHandlerRegistry>().Register(handler);

            //_container.RegisterType<ECHandler>();
            //_container.RegisterType<ECViewModel>();
            //_container.RegisterType<OIDE.AssetBrowser.View.EC.ECView>();

            //IContentHandler handler2 = _container.Resolve<ECHandler>();
            //_container.Resolve<IContentHandlerRegistry>().Register(handler2);
            
            _container.RegisterType<AssetBrowserToolViewModel>();
           // _container.RegisterType<AssetBrowserToolView>();

            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            workspace.Tools.Add(_container.Resolve<AssetBrowserToolViewModel>()); 
        }

        private void LoadTheme()
        {
            _eventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent
                                                                              {Message = "Themes.."});
            //var manager = _container.Resolve<IThemeManager>();
            //var themeSettings = _container.Resolve<IThemeSettings>();
            //var win = _container.Resolve<IShell>() as Window;
            //manager.AddTheme(new LightTheme());
            //manager.AddTheme(new DarkTheme());
            //win.Dispatcher.InvokeAsync(() => manager.SetCurrent(themeSettings.SelectedTheme));
        }

        private void LoadCommands()
        {
            _eventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent
                                                                              {Message = "Commands.."});
            var manager = _container.Resolve<ICommandManager>();

         //  var openCommand = new DelegateCommand(OpenModule);
            //var exitCommand = new DelegateCommand(CloseCommandExecute);
            //var saveCommand = new DelegateCommand(SaveDocument, CanExecuteSaveDocument);
            //var saveAsCommand = new DelegateCommand(SaveAsDocument, CanExecuteSaveAsDocument);
            //var themeCommand = new DelegateCommand<string>(ThemeChangeCommand);
            //var loggerCommand = new DelegateCommand(ToggleLogger);

            //OIDE.AssetBrowser.Commands.OpenCOMCommand mOpenComCommand = new Commands.OpenCOMCommand(_container);
            //var openCOMCommand = new DelegateCommand<OIDE.AssetBrowser.Commands.OpenCOMCommand>(mOpenComCommand.OnSubmit, mOpenComCommand.CanSubmit);
            //manager.RegisterCommand("OPENCOM", openCOMCommand);

            //OIDE.AssetBrowser.Commands.ECStatusCommand mECStatusCommand = new Commands.ECStatusCommand(_container);
            //var ECStatusCommand = new DelegateCommand<OIDE.AssetBrowser.Commands.ECStatusCommand>(mECStatusCommand.OnSubmit, mECStatusCommand.CanSubmit);
            //manager.RegisterCommand("ECSTATUS", ECStatusCommand);

            //OIDE.AssetBrowser.Commands.ECEjectCommand mECEjectCommand = new Commands.ECEjectCommand(_container);
            //var ecEjectCommand = new DelegateCommand<OIDE.AssetBrowser.Commands.ECEjectCommand>(mECEjectCommand.OnSubmit, mECEjectCommand.CanSubmit);
            //manager.RegisterCommand("ECEJECT", ecEjectCommand);

            //OIDE.AssetBrowser.Commands.CloseCommand mECCloseCommand = new Commands.CloseCommand(_container);
            //var ecCloseCommand = new DelegateCommand<OIDE.AssetBrowser.Commands.CloseCommand>(mECCloseCommand.OnSubmit, mECCloseCommand.CanSubmit);
            //manager.RegisterCommand("ECCLOSE", ecCloseCommand);

            //OIDE.AssetBrowser.Commands.ICCSelectCommand mECICSelCommand = new Commands.ICCSelectCommand(_container);
            //var ecICCSelCommand = new DelegateCommand<OIDE.AssetBrowser.Commands.ICCSelectCommand>(mECICSelCommand.OnSubmit, mECICSelCommand.CanSubmit);
            //manager.RegisterCommand("ICCSel", ecICCSelCommand);

            //OIDE.AssetBrowser.Commands.ICCDirectControlCommand mECICDirectControlCommand = new Commands.ICCDirectControlCommand(_container);
            //var ecICDirectControlCommand = new DelegateCommand<OIDE.AssetBrowser.Commands.ICCDirectControlCommand>(mECICDirectControlCommand.OnSubmit, mECICDirectControlCommand.CanSubmit);
            //manager.RegisterCommand("ECECDIRECTCTRL", ecICDirectControlCommand);
           

            //manager.RegisterCommand("OPEN", openCommand);
            //manager.RegisterCommand("SAVE", saveCommand);
            //manager.RegisterCommand("SAVEAS", saveAsCommand);
            //manager.RegisterCommand("EXIT", exitCommand);
            //manager.RegisterCommand("LOGSHOW", loggerCommand);
            //manager.RegisterCommand("THEMECHANGE", themeCommand);
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

            //menuService.Add(new MenuItemViewModel("_File", 1));

            //menuService.Get("_File").Add(
            //    (new MenuItemViewModel("_New", 3,
            //                           new BitmapImage(
            //                               new Uri(
            //                                   @"pack://application:,,,/OIDE.AssetBrowser;component/Icons/NewRequest_8796.png")),
            //                           manager.GetCommand("NEW"),
            //                           new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N"))));

            //menuService.Get("_File").Add(
            //    (new MenuItemViewModel("_Open", 4,
            //                           new BitmapImage(
            //                               new Uri(
            //                                   @"pack://application:,,,/OIDE.AssetBrowser;component/Icons/OpenFileDialog_692.png")),
            //                           manager.GetCommand("OPEN"),
            //                           new KeyGesture(Key.O, ModifierKeys.Control, "Ctrl + O"))));
            //menuService.Get("_File").Add(new MenuItemViewModel("_Save", 5,
            //                                                   new BitmapImage(
            //                                                       new Uri(
            //                                                           @"pack://application:,,,/OIDE.AssetBrowser;component/Icons/Save_6530.png")),
            //                                                   manager.GetCommand("SAVE"),
            //                                                   new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl + S")));
            //menuService.Get("_File").Add(new SaveAsMenuItemViewModel("Save As..", 6,
            //                                       new BitmapImage(
            //                                           new Uri(
            //                                               @"pack://application:,,,/OIDE.AssetBrowser;component/Icons/Save_6530.png")),
            //                                       manager.GetCommand("SAVEAS"),null,false,false,_container));

            //menuService.Get("_File").Add(new MenuItemViewModel("Close", 8, null, manager.GetCommand("CLOSE"),
            //                                                   new KeyGesture(Key.F4, ModifierKeys.Control, "Ctrl + F4")));

            //menuService.Get("_File").Add(recentFiles.RecentMenu);

            //menuService.Get("_File").Add(new MenuItemViewModel("E_xit", 101, null, manager.GetCommand("EXIT"),
            //                                                   new KeyGesture(Key.F4, ModifierKeys.Alt, "Alt + F4")));


            //menuService.Add(new MenuItemViewModel("_Edit", 2));
            //menuService.Get("_Edit").Add(new MenuItemViewModel("_Undo", 1,
            //                                                   new BitmapImage(
            //                                                       new Uri(
            //                                                           @"pack://application:,,,/OIDE.AssetBrowser;component/Icons/Undo_16x.png")),
            //                                                   ApplicationCommands.Undo));
            //menuService.Get("_Edit").Add(new MenuItemViewModel("_Redo", 2,
            //                                                   new BitmapImage(
            //                                                       new Uri(
            //                                                           @"pack://application:,,,/OIDE.AssetBrowser;component/Icons/Redo_16x.png")),
            //                                                   ApplicationCommands.Redo));
            //menuService.Get("_Edit").Add(MenuItemViewModel.Separator(15));
            //menuService.Get("_Edit").Add(new MenuItemViewModel("Cut", 20,
            //                                                   new BitmapImage(
            //                                                       new Uri(
            //                                                           @"pack://application:,,,/OIDE.AssetBrowser;component/Icons/Cut_6523.png")),
            //                                                   ApplicationCommands.Cut));
            //menuService.Get("_Edit").Add(new MenuItemViewModel("Copy", 21,
            //                                                   new BitmapImage(
            //                                                       new Uri(
            //                                                           @"pack://application:,,,/OIDE.AssetBrowser;component/Icons/Copy_6524.png")),
            //                                                   ApplicationCommands.Copy));
            //menuService.Get("_Edit").Add(new MenuItemViewModel("_Paste", 22,
            //                                                   new BitmapImage(
            //                                                       new Uri(
            //                                                           @"pack://application:,,,/OIDE.AssetBrowser;component/Icons/Paste_6520.png")),
            //                                                   ApplicationCommands.Paste));

            //menuService.Add(new MenuItemViewModel("_View", 3));

            //if (logger != null)
            //    menuService.Get("_View").Add(new MenuItemViewModel("_Logger", 1,
            //                                                       new BitmapImage(
            //                                                           new Uri(
            //                                                               @"pack://application:,,,/OIDE.AssetBrowser;component/Icons/Undo_16x.png")),
            //                                                       manager.GetCommand("LOGSHOW"))
            //                                     {IsCheckable = true, IsChecked = logger.IsVisible});

            //menuService.Get("_View").Add(new MenuItemViewModel("Themes", 1));

            ////Set the checkmark of the theme menu's based on which is currently selected
            //menuService.Get("_View").Get("Themes").Add(new MenuItemViewModel("Dark", 1, null,
            //                                                                 manager.GetCommand("THEMECHANGE"))
            //                                               {
            //                                                   IsCheckable = true,
            //                                                   IsChecked = (themeSettings.SelectedTheme == "Dark"),
            //                                                   CommandParameter = "Dark"
            //                                               });
            //menuService.Get("_View").Get("Themes").Add(new MenuItemViewModel("Light", 2, null,
            //                                                                 manager.GetCommand("THEMECHANGE"))
            //                                               {
            //                                                   IsCheckable = true,
            //                                                   IsChecked = (themeSettings.SelectedTheme == "Light"),
            //                                                   CommandParameter = "Light"
            //                                               });

            //menuService.Add(new MenuItemViewModel("_Tools", 4));
            //menuService.Get("_Tools").Add(new MenuItemViewModel("Settings", 1, null, settingsManager.SettingsCommand));

            //menuService.Add(new MenuItemViewModel("_Help", 4));
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
            if (workspace.ActiveDocument != null)
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

        #region Theme

        private void ThemeChangeCommand(string s)
        {
            var manager = _container.Resolve<IThemeManager>();
            var menuService = _container.Resolve<IMenuService>();
            var win = _container.Resolve<IShell>() as Window;
            MenuItemViewModel mvm =
                menuService.Get("_View").Get("Themes").Get(manager.CurrentTheme.Name) as MenuItemViewModel;

            if (manager.CurrentTheme.Name != s)
            {
                if (mvm != null)
                    mvm.IsChecked = false;
                win.Dispatcher.InvokeAsync(() => manager.SetCurrent(s));
            }
            else
            {
                if (mvm != null)
                    mvm.IsChecked = true;
            }
        }

        #endregion

        #region Logger click

        private void ToggleLogger()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            var menuService = _container.Resolve<IMenuService>();
            ToolViewModel logger = workspace.Tools.First(f => f.ContentId == "Logger");
            if (logger != null)
            {
                logger.IsVisible = !logger.IsVisible;
                var mi = menuService.Get("_View").Get("_Logger") as MenuItemViewModel;
                mi.IsChecked = logger.IsVisible;
            }
        }

        #endregion

        #endregion
    }
}