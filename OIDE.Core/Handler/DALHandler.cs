﻿using Microsoft.Practices.Unity;
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

namespace OIDE.Core.ProjectTypes.Handler
{
  // [FileContent("EC Leser", "*.gameProject", 1)]
    [NewContent("DAL", 1, "DAL")]
    internal class DALHandler : IContentHandler
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
        public DALHandler(IUnityContainer container, ILoggerService loggerService)
        {
            _container = container;
            _loggerService = loggerService;
            _dialog = new SaveFileDialog();
        }

        #region IContentHandler Members

        public ContentViewModel NewContent(object parameter)
        {
            var vm = _container.Resolve<DALViewModel>();
            var model = _container.Resolve<DALModel>();
            var view = _container.Resolve<DALView>();

            //Model details
            _loggerService.Log("Creating a new simple file using SceneHandler", LogCategory.Info, LogPriority.Low);

            //Clear the undo stack
           // model.Document.UndoStack.ClearAll();

            //Set the model and view
            vm.SetModel(model);
            vm.SetView(view);
            vm.Title = "Game";
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
            FileCategoryModel root = new FileCategoryModel(null, _container) { Name = "RootNode" };
          
            //DALModel test = new DALModel(commandManager, menuService) { Name = "Test", IsExpanded = true };
            //root.Items.Add(test);

            DALModel gameProject = _container.Resolve<DALModel>(); //new DALModel(_container) { Name = "Game", IsExpanded = true };
            gameProject.Name = "Game";
            gameProject.IsExpanded = true;
            root.Items.Add(gameProject);
           
          //  mProjectTreeService.Items.Add(root);
            mProjectTreeService.SetAsRoot(root);

          

            return vm;
        }

        /// <summary>
        /// Validates the content by checking if a file exists for the specified location
        /// </summary>
        /// <param name="info">The string containing the file location</param>
        /// <returns>True, if the file exists and has a .gameProject extension - false otherwise</returns>
        public bool ValidateContentType(object info)
        {
            return true;
        }

        /// <summary>
        /// Opens a file and returns the corresponding DALViewModel
        /// </summary>
        /// <param name="info">The string location of the file</param>
        /// <returns>The <see cref="DALViewModel"/> for the file.</returns>
        public ContentViewModel OpenContent(object info)
        {
            //var location = info as string;
            //if (location != null)
            //{
            DALViewModel vm = _container.Resolve<DALViewModel>();
            var model = _container.Resolve<DALModel>();
            var view = _container.Resolve<DALView>();

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
            string[] split = Regex.Split(contentId, ":##:");
            if (split.Count() == 2)
            {
                string identifier = split[0];
                string path = split[1];
                if (identifier == "FILE" && File.Exists(path))
                {
                    return OpenContent(path);
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
            var gameProjectViewModel = contentViewModel as DALViewModel;

            if (gameProjectViewModel == null)
            {
                _loggerService.Log("ContentViewModel needs to be a DALViewModel to save details", LogCategory.Exception,
                                   LogPriority.High);
                throw new ArgumentException("ContentViewModel needs to be a DALViewModel to save details");
            }

            var gameProjectModel = gameProjectViewModel.Model as DALModel;

            if (gameProjectModel == null)
            {
                _loggerService.Log("DALViewModel does not have a DALModel which should have the text",
                                   LogCategory.Exception, LogPriority.High);
                throw new ArgumentException("DALViewModel does not have a DALModel which should have the text");
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
                _dialog.DefaultExt = "gameProj";
                _dialog.Filter = "DAL files (*.gameProj)|*.gameProj";

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

                        gameProjectModel.SerializeObjectToXML();
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
                    gameProjectModel.SerializeObjectToXML();
                    //using (FileStream fs = new FileStream(location, FileMode.Open))
                    //{
                    //    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(gameProjectModel.GetType());
                    //    x.Serialize(fs, gameProjectModel);
                    //    fs.Close();
                    //}

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
