using System.Windows;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using System;
using Microsoft.Practices.Prism.Events;
using Module.Tools.Logger;
using Module.Tools.Output;
using Module.Core;

namespace OIDE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MDBootstrapper b;

        Wide.Core.CoreModule WideCoreModule;
        Module.Tools.Logger.LoggerModule LoggerModule;
        Module.Tools.Output.OutputModule OutputModule;
        Module.Core.CoreModule CoreModule;
        Module.Properties.CoreModule PropertiesModule;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            b = new MDBootstrapper();
            b.Run();
            var shell = b.Container.Resolve<IShell>();
            (shell as Window).Loaded += App_Loaded;
            (shell as Window).Unloaded += App_Unloaded;

            //----------   normaly done in WIDE
            var eventaggregator = b.Container.Resolve<IEventAggregator>();

            WideCoreModule = new Wide.Core.CoreModule(b.Container, eventaggregator);
            WideCoreModule.Initialize();

            Application.Current.MainWindow.DataContext = b.Container.Resolve<AbstractWorkspace>();


            LoggerModule = new Module.Tools.Logger.LoggerModule(b.Container);
            LoggerModule.Initialize();

            OutputModule = new Module.Tools.Output.OutputModule(b.Container);
            OutputModule.Initialize();

            PropertiesModule = new Module.Properties.CoreModule(b.Container, eventaggregator);
            PropertiesModule.Initialize();

            //needed for styles
            CoreModule = new Module.Core.CoreModule(b.Container, eventaggregator);
            CoreModule.Initialize();


            //need to load core module first -> Show needs the style resources
            (shell as Window).Show();

            //----------  

        }

        void App_Unloaded(object sender, System.EventArgs e)
        {
            var shell = b.Container.Resolve<IShell>();

            var workspace = b.Container.Resolve<AbstractWorkspace>();
            workspace.Documents.Clear(); // Clear Documents before app close

            shell.SaveLayout();
        }

        void App_Loaded(object sender, RoutedEventArgs e)
        {
            var shell = b.Container.Resolve<IShell>();
            shell.LoadLayout();


            //var OutputService = b.Container.Resolve<IOutputService>("MOS");
            //Console.SetOut(OutputService.TBStreamWriter);

            //var eventaggregator = b.Container.Resolve<IEventAggregator>();


         //   var OutputService = b.Container.Resolve<IOutputService>("MOS");
         //   Console.SetOut(OutputService.TBStreamWriter);
        }
    }
}