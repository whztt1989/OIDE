﻿#region License

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
using OIDE.Scene.Model;
using OIDE.Scene.Interface.Services;
using Module.PFExplorer.Interface.Services;
using Module.Properties.Interface;

namespace OIDE.Scene
{
    // [FileContent("EC Leser", "*.md", 1)]
    [NewContent("Scene", 1, "Scene")]
    internal class SceneViewerHandler : IContentHandler
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
        private SaveFileDialog _dialog;

        /// <summary>
        /// Constructor of ECHandler - all parameters are injected
        /// </summary>
        /// <param name="container">The injected container of the application</param>
        /// <param name="loggerService">The injected logger service of the application</param>
        public SceneViewerHandler(IUnityContainer container, ILoggerService loggerService)
        {
            _container = container;
            _loggerService = loggerService;
            _dialog = new SaveFileDialog();
        }

        #region IContentHandler Members

        public ContentViewModel NewContent(object parameter)
        {
            var vm = _container.Resolve<SceneViewerViewModel>();
            var model = _container.Resolve<SceneViewerModel>();
            var view = _container.Resolve<SceneViewerView>();

            //Model details
            _loggerService.Log("Creating a new simple file using SceneHandler", LogCategory.Info, LogPriority.Low);

            //Clear the undo stack
            //   model.Document.UndoStack.ClearAll();

            //Set the model and view
            vm.SetModel(model);
            vm.SetView(view);
            vm.Title = "Scene";
            vm.View.DataContext = model;
            vm.SetHandler(this);
            model.SetDirty(true);

            model.SetLocation("SceneID:##:");

            IProjectTreeService pfExplorerService = _container.Resolve<IProjectTreeService>();
            ISceneService sceneService = _container.Resolve<ISceneService>();

            IItem parent = null;

            if(pfExplorerService.SelectedItem != null)
              parent = pfExplorerService.SelectedItem;

            SceneDataModel newScene = new SceneDataModel(parent, _container) { Name = "Scene NEW", ContentID = "SceneID:##:" };
            
            if(pfExplorerService.SelectedItem != null)
                pfExplorerService.SelectedItem.Items.Add(newScene);
         
            sceneService.Scenes.Add(newScene);
          //  sceneService.SelectedScene = newScene;
            newScene.Open(-1);


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
            var location = info as string;
            if (location != null)
            {
                SceneViewerViewModel vm = _container.Resolve<SceneViewerViewModel>();
                var model = _container.Resolve<SceneViewerModel>();
                var view = _container.Resolve<SceneViewerView>();

                //Model details
                model.SetLocation(info);
                try
                {
                    ISceneService sceneService = _container.Resolve<ISceneService>();

                    //          string[] split = Regex.Split(info.ToString(), ":##:");
                    //if (split.Count() == 2)
                    //{
                    //    string identifier = split[0];
                    //    string ID = split[1];
                    //    if (identifier == "SceneID")
                    //    {
                    var scene = sceneService.Scenes.Where(x => x.ContentID == info.ToString());
                    if (scene.Any())
                    {
                        sceneService.SelectedScene = scene.First();
                        sceneService.SelectedScene.Open(info);
                    }
                    //  model.SetLocation("AuftragID:##:" + info + "");

                    //      model.Document.Text = File.ReadAllText(location);
                    model.SetDirty(true);
                    //   }
                }



                catch (Exception exception)
                {
                    _loggerService.Log(exception.Message, LogCategory.Exception, LogPriority.High);
                    _loggerService.Log(exception.StackTrace, LogCategory.Exception, LogPriority.High);
                    return null;
                }

                //Clear the undo stack
                // model.Document.UndoStack.ClearAll();

                //Set the model and view
                vm.SetModel(model);
                vm.SetView(view);
                vm.Title = "SceneViewer";//model.nae  // Path.GetFileName("Scene gefunden");
                vm.View.DataContext = model;

                return vm;
            }
            return null;
        }

        public ContentViewModel OpenContentFromId(string contentId)
        {
            //string[] split = Regex.Split(contentId, ":##:");
            //if (split.Count() == 2)
            //{
            //    string identifier = split[0];
            //    string path = split[1];
            //    if (identifier == "FILE" && File.Exists(path))
            //    {
            return OpenContent(contentId);
            //    }
            //}
            //return null;
        }

        /// <summary>
        /// Saves the content of the TextViewModel
        /// </summary>
        /// <param name="contentViewModel">This needs to be a TextViewModel that needs to be saved</param>
        /// <param name="saveAs">Pass in true if you need to Save As?</param>
        /// <returns>true, if successful - false, otherwise</returns>
        public virtual bool SaveContent(ContentViewModel contentViewModel, bool saveAs = false)
        {
            var gameProjectViewModel = contentViewModel as SceneViewerViewModel;

            if (gameProjectViewModel == null)
            {
                _loggerService.Log("ContentViewModel needs to be a SceneViewertViewModel to save details", LogCategory.Exception,
                                   LogPriority.High);
                throw new ArgumentException("ContentViewModel needs to be a SceneViewertViewModel to save details");
            }

            var gameProjectModel = gameProjectViewModel.Model as SceneViewerModel;

            if (gameProjectModel == null)
            {
                _loggerService.Log("SceneViewertViewModel does not have a SceneViewertModel which should have the text",
                                   LogCategory.Exception, LogPriority.High);
                throw new ArgumentException("SceneViewertViewModel does not have a SceneViewertModel which should have the text");
            }

            var location = gameProjectModel.Location as string;

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
                _dialog.DefaultExt = "scene";
                _dialog.Filter = "Scene files (*.scene)|*.scene";

                if (_dialog.ShowDialog() == true)
                {
                    location = _dialog.FileName;
                    gameProjectModel.SetLocation(location);
                    gameProjectViewModel.Title = Path.GetFileName(location);
                    try
                    {
                        //-----------------------------------
                        // Serialize Object
                        //-----------------------------------
                        //using (FileStream Str = new FileStream(location, FileMode.Create))
                        //{
                        //    XmlSerializer Ser = new XmlSerializer(typeof(CollectionOfIItem));
                        //    Ser.Serialize(Str, gameProjectModel.Items);
                        //    Str.Close();
                        //}

                        //####        gameProjectModel.SerializeObjectToXML();
                        //using (FileStream fs = new FileStream(location, FileMode.Open))
                        //{
                        //    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(gameProjectModel.GetType());
                        //    x.Serialize(fs, gameProjectModel);
                        //    fs.Close();
                        //}

                        gameProjectModel.SetDirty(false);
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
                    //-----------------------------------
                    // Serialize Object
                    //-----------------------------------
                    //####     gameProjectModel.SerializeObjectToXML();
                    //using (FileStream fs = new FileStream(location, FileMode.Open))
                    //{
                    //    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(gameProjectModel.GetType());
                    //    x.Serialize(fs, gameProjectModel);
                    //    fs.Close();
                    //}
                   ISceneService sceneService = _container.Resolve<ISceneService>();
                   sceneService.SelectedScene.Save();

                       //  File.WriteAllText(location, gameProjectModel.Ser);
                    gameProjectModel.SetDirty(false);
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
                if (identifier == "SceneID" && ValidateContentType(path))
                {
                    return true;
                }
            }
            return false;
            //   return "SceneViewer" == contentId ? true : false;
        }

        #endregion
    }
}