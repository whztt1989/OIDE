#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;

namespace OIDE.AssetBrowser
{
    internal class AssetBrowserToolViewModel : ToolViewModel
    {
        private readonly IEventAggregator _aggregator;
        private readonly ILoggerService _logger;
        private IWorkspace _workspace;
        private readonly IUnityContainer _container;
        private readonly AssetBrowserToolModel _model;
        private readonly AssetBrowserToolView _view;

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Right; }
        }

        public AssetBrowserToolViewModel(IUnityContainer container, AbstractWorkspace workspace)
        {
            _workspace = workspace;
            _container = container;
            Name = "AssetBrowser";
            Title = "AssetBrowser";
            ContentId = "AssetBrowser";
            _model = new AssetBrowserToolModel(container);//.Resolve<ICommandManager>(), container.Resolve<IMenuService>());
            Model = _model;
            IsVisible = true;

            _view = new AssetBrowserToolView(_container);
            _view.DataContext = _model;
            View = _view;

            _aggregator = _container.Resolve<IEventAggregator>();
            _aggregator.GetEvent<LogEvent>().Subscribe(AddLog);

            _logger = _container.Resolve<ILoggerService>();



            //CVMAssetBrowser p1 = ((CVMAssetBrowser)Workspace.This.VMTV.List[0]);
            //// CVMCategory cdata = new CVMCategory() { Name = "Data" };
            //// p1.Items.Add(cdata);

            ////------------- Scenes ----------------------
            //CVMCategory cScenes = new CVMCategory() { Name = "Scenes" };

            //p1.Items.Add(cScenes);

            //CVMScene sv = new CVMScene() { Name = "Scene 1" };
            //sv.Items.Add(new CVMCategory() { Name = "Cameras" });
            //sv.Items.Add(new CVMCategory() { Name = "Models" });
            //sv.Items.Add(new CVMCategory() { Name = "Sound" });
            //cScenes.Items.Add(sv);
            //// Workspace.This.VMTV.List.Add(new CVMAssetBrowser() { Name = "Proj2" });

            ////------------- Models ----------------------
            //CVMCategory cModels = new CVMCategory() { Name = "Models" };
            //CVMCategory cChars = new CVMCategory() { Name = "Characters" };
            //CVMModel cModel = new CVMModel() { Name = "OgreHead", File = "ogrehead.mesh" };
            //CVMModel cModel2 = new CVMModel() { Name = "Human", File = "human.mesh" };

            //cChars.Items.Add(cModel);
            //cChars.Items.Add(cModel2);
            //cModels.Items.Add(cChars);
            //CVMCategory cCreatures = new CVMCategory() { Name = "Creatures" };
            //cModels.Items.Add(cCreatures);
            //p1.Items.Add(cModels);

            ////------------------ Sounds -------------------------
            //CVMCategory cSounds = new CVMCategory() { Name = "Sounds" };
            //p1.Items.Add(cSounds);

            ////------------------ Particles -------------------------
            //CVMCategory cParticles = new CVMCategory() { Name = "Particles" };
            //p1.Items.Add(cParticles);

        }

        private void AddLog(ILoggerService logger)
        {
          //  _model.AddLog(logger);
        }


    }
}