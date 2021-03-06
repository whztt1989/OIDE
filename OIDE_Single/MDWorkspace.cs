﻿using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using System.ComponentModel;
using Wide.Interfaces.Services;
using Module.Properties.Interface.Services;

namespace OIDE
{
    internal class MDWorkspace : AbstractWorkspace
    {
        private string _document;
        private ILoggerService _logger;
        private const string _title = "OIDE";
        private IUnityContainer m_container;

        public MDWorkspace(IUnityContainer container, IEventAggregator eventAggregator)
            : base(container, eventAggregator)
        {
            m_container = container;
            IEventAggregator aggregator = container.Resolve<IEventAggregator>();
            aggregator.GetEvent<ActiveContentChangedEvent>().Subscribe(ContentChanged);
            _document = "";
        }

        public override ImageSource Icon
        {
            get
            {
                ImageSource imageSource = new BitmapImage(new Uri("pack://application:,,,/OIDE;component/Icon.png"));
                return imageSource;
            }
        }

        public override string Title
        {
            get
            {
                string newTitle = _title;
                if (_document != "")
                {
                    newTitle += " - " + _document;
                }
                return newTitle;
            }
        }

        private ILoggerService Logger
        {
            get
            {
                if (_logger == null)
                    _logger = _container.Resolve<ILoggerService>();
                return _logger;
            }
        }

        private void ContentChanged(ContentViewModel model)
        {
            _document = model == null ? "" : model.Title;
            RaisePropertyChanged("Title");
            if(model != null)
            {
                //--------------------------------------------------------------------
                //set current model for propertygrid if active document has changed
                var propService = m_container.Resolve<IPropertiesService>();
                var selectedModel = model.Model as Module.Properties.Interface.IItem;
                if (selectedModel != null)
                {
                    propService.CurrentItem = selectedModel;
                    selectedModel.IsSelected = true;
                }
                //--------------------------------------------------------------------
               
                Logger.Log("Active document changed to " + model.Title, LogCategory.Info, LogPriority.None);
            }
        }

        protected override void ModelChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            string newValue = ActiveDocument == null ? "" : ActiveDocument.Title;
            if (_document != newValue)
            {
                _document = newValue;
                RaisePropertyChanged("Title");
                base.ModelChangedEventHandler(sender, e);
            }
        }
    }
}