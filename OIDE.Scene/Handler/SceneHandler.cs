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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Practices.Unity;
using Wide.Core.Attributes;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Microsoft.Win32;
using OIDE.Scene;
using OIDE.Scene.View;

namespace OIDE.Scene
{
   // [FileContent("EC Leser", "*.md", 1)]
    [NewContent("Scene", 1, "Scene")]
    internal class SceneHandler : IContentHandler
    {
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
        public SceneHandler(IUnityContainer container, ILoggerService loggerService)
        {
            _container = container;
            _loggerService = loggerService;
       //     _dialog = new SaveFileDialog();
        }

        #region IContentHandler Members

        public ContentViewModel NewContent(object parameter)
        {
            var vm = _container.Resolve<SceneViewModel>();
            var model = _container.Resolve<SceneModel>();
            var view = _container.Resolve<SceneView>();

            //Model details
            _loggerService.Log("Creating a new simple file using SceneHandler", LogCategory.Info, LogPriority.Low);

            //Clear the undo stack
           // model.Document.UndoStack.ClearAll();

            //Set the model and view
            vm.SetModel(model);
            vm.SetView(view);
            vm.Title = "EC Leser";
            vm.View.DataContext = model;
            vm.SetHandler(this);
            model.SetDirty(true);

            return vm;
        }

        /// <summary>
        /// Validates the content by checking if a file exists for the specified location
        /// </summary>
        /// <param name="info">The string containing the file location</param>
        /// <returns>True, if the file exists and has a .md extension - false otherwise</returns>
        public bool ValidateContentType(object info)
        {
            return true;
        }

        /// <summary>
        /// Opens a file and returns the corresponding MDViewModel
        /// </summary>
        /// <param name="info">The string location of the file</param>
        /// <returns>The <see cref="MDViewModel"/> for the file.</returns>
        public ContentViewModel OpenContent(object info)
        {
            //var location = info as string;
            //if (location != null)
            //{
            SceneViewModel vm = _container.Resolve<SceneViewModel>();
            var model = _container.Resolve<SceneModel>();
            var view = _container.Resolve<SceneView>();

                //Model details
                model.SetLocation(info);
                try
                {
              //      model.Document.Text = File.ReadAllText(location);
                    model.SetDirty(false);
                }
                catch (Exception exception)
                {
                    _loggerService.Log(exception.Message, LogCategory.Exception, LogPriority.High);
                    _loggerService.Log(exception.StackTrace, LogCategory.Exception, LogPriority.High);
                    return null;
                }

                //Clear the undo stack
             //   model.Document.UndoStack.ClearAll();

                //Set the model and view
                vm.SetModel(model);
                vm.SetView(view);
                vm.Title = Path.GetFileName("Leser gefunden");
                vm.View.DataContext = model;

                return vm;
         //   }
         //   return null;
        }

        public ContentViewModel OpenContentFromId(string contentId)
        {
        //    string[] split = Regex.Split(contentId, ":##:");
        //    if (split.Count() == 2)
        //    {
        //        string identifier = split[0];
        //        string path = split[1];
        //        if (identifier == "FILE" && File.Exists(path))
        //        {
        //            return OpenContent(path);
        //        }
        //    }
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
            return true;
        }

        /// <summary>
        /// Validates the content from an ID - the ContentID from the ContentViewModel
        /// </summary>
        /// <param name="contentId">The content ID which needs to be validated</param>
        /// <returns>True, if valid from content ID - false, otherwise</returns>
        public bool ValidateContentFromId(string contentId)
        {

                    return true;
        }

        #endregion
    }
}