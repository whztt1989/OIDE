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
using XIDE.Scene.View;

namespace XIDE.Scene
{
    internal class SceneGraphToolViewModel : ToolViewModel
    {
        private readonly IEventAggregator _aggregator;
        private readonly ILoggerService _logger;
        private IWorkspace _workspace;
        private readonly IUnityContainer _container;
        private readonly SceneGraphToolModel _model;
        private readonly SceneGraphToolView _view;

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Left; }
        }

        public SceneGraphToolViewModel(IUnityContainer container, AbstractWorkspace workspace)
        {
            _workspace = workspace;
            _container = container;
            Name = "Scene";
            Title = "Scene";
            ContentId = "Scene";
            _model = new SceneGraphToolModel(container.Resolve<ICommandManager>(), container.Resolve<IMenuService>());
            Model = _model;
            IsVisible = true;

            _view = new SceneGraphToolView(_container);
            _view.DataContext = _model;
            View = _view;

            _aggregator = _container.Resolve<IEventAggregator>();
            _aggregator.GetEvent<LogEvent>().Subscribe(AddLog);

            _logger = _container.Resolve<ILoggerService>();


        }

        private void AddLog(ILoggerService logger)
        {
          //  _model.AddLog(logger);
        }


    }
}