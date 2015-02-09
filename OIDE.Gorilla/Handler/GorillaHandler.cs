﻿#region License

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

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Practices.Unity;
using Wide.Core.Attributes;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Microsoft.Win32;
using OIDE.Gorilla.Model;
using OIDE.Gorilla.Model.Objects;

namespace OIDE.Gorilla
{
    [FileContent("Gorilla files", "*.gorilladat", 1)]
    [NewContent("Gorilla files", 1, "Creates a new Gorilla file", "pack://application:,,,/OIDE.Gorilla;component/Icons/GorillaType.png")]
    internal class GorillaHandler : IContentHandler
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
        /// Constructor of GorillaHandler - all parameters are injected
        /// </summary>
        /// <param name="container">The injected container of the application</param>
        /// <param name="loggerService">The injected logger service of the application</param>
        public GorillaHandler(IUnityContainer container, ILoggerService loggerService)
        {
            _container = container;
            _loggerService = loggerService;
            _dialog = new SaveFileDialog();
        }

        #region IContentHandler Members

        public ContentViewModel NewContent(object parameter)
        {
            var vm = _container.Resolve<GorillaViewModel>();
            var model = _container.Resolve<GorillaModel>();
            var view = _container.Resolve<GorillaView>();

            //Model details
            _loggerService.Log("Creating a new simple file using GorillaHandler", LogCategory.Info, LogPriority.Low);

            //Clear the undo stack
            model.Document.UndoStack.ClearAll();

            //Set the model and view
            vm.SetModel(model);
            vm.SetView(view);
            vm.Title = "untitled-Gorilla";
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
            string location = info as string;
            string extension = "";

            if (location == null)
            {
                return false;
            }

            extension = Path.GetExtension(location);
            return File.Exists(location) && extension == ".gorilladat";
        }

        /// <summary>
        /// Opens a file and returns the corresponding GorillaViewModel
        /// </summary>
        /// <param name="info">The string location of the file</param>
        /// <returns>The <see cref="GorillaViewModel"/> for the file.</returns>
        public ContentViewModel OpenContent(object info, object param)
        {
            var location = info as string;
            if (location != null)
            {
                GorillaViewModel vm = _container.Resolve<GorillaViewModel>();
                var model = _container.Resolve<GorillaModel>();
                var view = _container.Resolve<GorillaView>();

                //Model details
                model.SetLocation(info);
                try
                {
                 //   model.Document.Text = File.ReadAllText(location);
                    model.Open(location);
                    model.SetDirty(false);
                }
                catch (Exception exception)
                {
                    _loggerService.Log(exception.Message, LogCategory.Exception, LogPriority.High);
                    _loggerService.Log(exception.StackTrace, LogCategory.Exception, LogPriority.High);
                    return null;
                }

                //Clear the undo stack
                model.Document.UndoStack.ClearAll();

                //Set the model and view
                vm.SetModel(model);
                vm.SetView(view);
                vm.Title = Path.GetFileName(location);
                vm.View.DataContext = model;

                return vm;
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
            var mdViewModel = contentViewModel as GorillaViewModel;

            if (mdViewModel == null)
            {
                _loggerService.Log("ContentViewModel needs to be a GorillaViewModel to save details", LogCategory.Exception,
                                   LogPriority.High);
                throw new ArgumentException("ContentViewModel needs to be a GorillaViewModel to save details");
            }

            var mdModel = mdViewModel.Model as GorillaModel;

            if (mdModel == null)
            {
                _loggerService.Log("GorillaViewModel does not have a GorillaModel which should have the text",
                                   LogCategory.Exception, LogPriority.High);
                throw new ArgumentException("GorillaViewModel does not have a GorillaModel which should have the text");
            }

            var location = mdModel.Location as string;

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
                _dialog.DefaultExt = "gorilladat";
                _dialog.Filter = "Gorilla File (*.gorilladat)|*.gorilladat";
                
                if (_dialog.ShowDialog() == true)
                {
                    location = _dialog.FileName;
                    mdModel.SetLocation(location);
                    mdViewModel.Title = Path.GetFileName(location);
                    try
                    {
                        //GorillaData test = new GorillaData() { GorillaFile = "ddfd", Texture = "sdfsdfsd" };

                        //DAL.Utility.JSONSerializer.Serialize<GorillaData>(test, location);
                        //   File.WriteAllText(location, mdModel.Document.Text);
                        mdModel.SetDirty(false);
                        return true;
                    }
                    catch (Exception exception)
                    {
                        _loggerService.Log(exception.Message, LogCategory.Exception, LogPriority.High);
                        _loggerService.Log(exception.StackTrace, LogCategory.Exception, LogPriority.High);
                        return false;
                    }
                }
            }
            else
            {
                try
                {
                    DAL.Utility.JSONSerializer.Serialize<GorillaModel>(mdModel, location);
                  //  File.WriteAllText(location, mdModel.Document.Text);
                    mdModel.SetDirty(false);
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