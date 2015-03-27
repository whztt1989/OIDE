#region License

//The MIT License (MIT)

//Copyright (c) 2014 Konrad Huber

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

#endregion

using Microsoft.Practices.Unity;
using OIDE.Core.ProjectTypes.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.PFExplorer.Interface.Services;
using Wide.Core.Attributes;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Module.PFExplorer;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Xml.Serialization;
using Module.Properties.Interface;
using OIDE.Scene.Interface.Services;
using Module.PFExplorer.Utilities;

namespace OIDE.Core.ProjectTypes.Handler
{
  // [FileContent("EC Leser", "*.oideProject", 1)]
    [NewContent("OIDEProject", 1, "OIDEProject")]
    internal class OIDEProjectHandler : IContentHandler
    {
        /// <summary>
        /// The save file dialog
        /// </summary>
        private SaveFileDialog _dialog;


        /// <summary>
        /// The injected container
        /// </summary>
        private readonly IUnityContainer _container;

        /// <summary>
        /// The injected logger service
        /// </summary>
        private readonly ILoggerService _loggerService;
        
        /// <summary>
        /// The save file dialog
        /// </summary>
   //     private SaveFileDialog _dialog;

        /// <summary>
        /// Constructor of ECHandler - all parameters are injected
        /// </summary>
        /// <param name="container">The injected container of the application</param>
        /// <param name="loggerService">The injected logger service of the application</param>
        public OIDEProjectHandler(IUnityContainer container, ILoggerService loggerService)
        {
            _container = container;
            _loggerService = loggerService;
            _dialog = new SaveFileDialog();
        }

        #region IContentHandler Members

        public ContentViewModel NewContent(object parameter)
        {
            var vm = _container.Resolve<OIDEProjectViewModel>();
            var model = _container.Resolve<OIDEProjectModel>();
            var view = _container.Resolve<OIDEProjectView>();

            //Model details
            _loggerService.Log("Creating a new simple file using SceneHandler", LogCategory.Info, LogPriority.Low);

            //Clear the undo stack
           // model.Document.UndoStack.ClearAll();

            //Set the model and view
            vm.SetModel(model);
            vm.SetView(view);
            vm.Title = "New OIDE Project";
            vm.View.DataContext = model;
            vm.SetHandler(this);
            model.SetDirty(true);

            var mSceneService = _container.Resolve<ISceneService>();
            var mProjectTreeService = _container.Resolve<IProjectTreeService>();
            var commandManager = _container.Resolve<ICommandManager>();
            var menuService = _container.Resolve<IMenuService>();

            //---------------------------------------------
            //Projekt Tree
            //---------------------------------------------
        //    FileCategoryModel root = new FileCategoryModel(null, _container) { Name = "RootNode" };
          
            //OIDEProjectModel test = new OIDEProjectModel(commandManager, menuService) { Name = "Test", IsExpanded = true };
            //root.Items.Add(test);

          //  OIDEProjectModel oideProject = _container.Resolve<OIDEProjectModel>(); //new OIDEProjectModel(_container) { Name = "Game", IsExpanded = true };
            model.Name = "New OIDE Project";
            model.IsExpanded = true;
         //   root.Items.Add(oideProject);
           
          //  mProjectTreeService.Items.Add(root);
            mProjectTreeService.SetAsRoot(model);

          

            return vm;
        }

        /// <summary>
        /// Validates the content by checking if a file exists for the specified location
        /// </summary>
        /// <param name="info">The string containing the file location</param>
        /// <returns>True, if the file exists and has a .oideProject extension - false otherwise</returns>
        public bool ValidateContentType(object info)
        {
            String fileeExt = Path.GetExtension(info.ToString());

            if (fileeExt.EndsWith(".oideProj"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Opens a file and returns the corresponding OIDEProjectViewModel
        /// </summary>
        /// <param name="info">The string location of the file</param>
        /// <returns>The <see cref="OIDEProjectViewModel"/> for the file.</returns>
        public ContentViewModel OpenContent(object info, object param)
        {
            var location = info as string;
            if (location != null)
            {

                try
                {
                    OIDEProjectViewModel vm = _container.Resolve<OIDEProjectViewModel>();
                    var model = _container.Resolve<OIDEProjectModel>();
                    var view = _container.Resolve<OIDEProjectView>();
                    var mProjectTreeService = _container.Resolve<IProjectTreeService>();
        
                  
                    //Model details
                    model.SetLocation(info); 

                    //Set the model and view
                    vm.SetModel(model);
                    vm.SetView(view);
                    vm.Title = Path.GetFileName(location);
                    vm.View.DataContext = model;

                    model.Open(_container, location);

              //      model.Document.Text = File.ReadAllText(location);
                    model.SetDirty(false);

                    mProjectTreeService.SetAsRoot(model);


                    return vm;
                }
                catch (Exception exception)
                {
                    _loggerService.Log(exception.Message, LogCategory.Exception, LogPriority.High);
                    _loggerService.Log(exception.StackTrace, LogCategory.Exception, LogPriority.High);
                    return null;
                }

                //Clear the undo stack
             //   model.Document.UndoStack.ClearAll();
            }
            return null;
        }

        public ContentViewModel OpenContentFromId(string contentId, object param)
        {
            string[] split = Regex.Split(contentId, ":##:");
            if (split.Count() == 2)
            {
                string identifier = split[0];
                string path = split[1];
                if (identifier == "FILE" && File.Exists(path))
                {
                    return OpenContent(path, param);
                }
            }
            return null;
        }

        /// <summary>
        /// Saves the content of the TextViewModel
        /// </summary>
        /// <param name="contentViewModel">This needs to be a TextViewModel that needs to be saved</param>
        /// <param name="saveAs">Pass in true if you need to Save As?</param>
        /// <returns>true, if successful - false, otherwise</returns>
        public virtual bool SaveContent(ContentViewModel contentViewModel, bool saveAs = false)
        {
            var oideProjectViewModel = contentViewModel as OIDEProjectViewModel;

            if (oideProjectViewModel == null)
            {
                _loggerService.Log("ContentViewModel needs to be a OIDEProjectViewModel to save details", LogCategory.Exception,
                                   LogPriority.High);
                throw new ArgumentException("ContentViewModel needs to be a OIDEProjectViewModel to save details");
            }

            var oideProjectModel = oideProjectViewModel.Model as OIDEProjectModel;

            if (oideProjectModel == null)
            {
                _loggerService.Log("OIDEProjectViewModel does not have a OIDEProjectModel which should have the text",
                                   LogCategory.Exception, LogPriority.High);
                throw new ArgumentException("OIDEProjectViewModel does not have a OIDEProjectModel which should have the text");
            }

            var location = oideProjectModel.Location as string;

            if (location == null)
            {
                //If there is no location, just prompt for Save As..
                saveAs = true;
            }

            if (saveAs)
            {
                if (location != null)
                    _dialog.InitialDirectory = Path.GetDirectoryName(location);

                _dialog.CheckPathExists = true;
                _dialog.DefaultExt = "oideProj";
                _dialog.Filter = "OIDEProject files (*.oideProj)|*.oideProj";

                if (_dialog.ShowDialog() == true)
                {
                    location = _dialog.FileName;
                    oideProjectModel.SetLocation(location);
                    oideProjectViewModel.Title = Path.GetFileName(location);
                    try
                    {
                        if (oideProjectModel.Save(location))
                            oideProjectModel.SetDirty(false);
                        return true;
                    }
                    catch (Exception exception)
                    {
                        _loggerService.Log(exception.Message, LogCategory.Exception, LogPriority.High);
                        if (exception.InnerException != null) _loggerService.Log(exception.InnerException.Message, LogCategory.Exception, LogPriority.High);
                        _loggerService.Log(exception.StackTrace, LogCategory.Exception, LogPriority.High);
                        return false;
                    }
                }
            }
            else
            {
                try
                {
                    if (oideProjectModel.Save(location))
                        oideProjectModel.SetDirty(false);

                    return true;
                }
                catch (Exception exception)
                {
                    _loggerService.Log(exception.Message, LogCategory.Exception, LogPriority.High);
                    _loggerService.Log(exception.StackTrace, LogCategory.Exception, LogPriority.High);
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Validates the content from an ID - the ContentID from the ContentViewModel
        /// </summary>
        /// <param name="contentId">The content ID which needs to be validated</param>
        /// <returns>True, if valid from content ID - false, otherwise</returns>
        public bool ValidateContentFromId(string contentId)
        {
            if (contentId == null)
                return false;

            string[] split = Regex.Split(contentId, ":##:");
            if (split.Count() == 2)
            {
                string identifier = split[0];
                string path = split[1];
                if (identifier == "FILE" && ValidateContentType(path))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
