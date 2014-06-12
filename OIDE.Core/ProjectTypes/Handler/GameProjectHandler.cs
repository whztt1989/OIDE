using Microsoft.Practices.Unity;
using OIDE.Core.ProjectTypes.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TModul.PFExplorer.Interface.Services;
using Wide.Core.Attributes;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace OIDE.Core.ProjectTypes.Handler
{
  // [FileContent("EC Leser", "*.md", 1)]
    [NewContent("GameProject", 1, "GameProject")]
    internal class GameProjectHandler : IContentHandler
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
        public GameProjectHandler(IUnityContainer container, ILoggerService loggerService)
        {
            _container = container;
            _loggerService = loggerService;
       //     _dialog = new SaveFileDialog();
        }

        #region IContentHandler Members

        public ContentViewModel NewContent(object parameter)
        {
            var vm = _container.Resolve<GameProjectViewModel>();
            var model = _container.Resolve<GameProjectModel>();
            var view = _container.Resolve<GameProjectView>();

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

            var mProjectTreeService = _container.Resolve<IProjectTreeService>();
            var commandManager = _container.Resolve<ICommandManager>();
            var menuService = _container.Resolve<IMenuService>();

            //---------------------------------------------
            //Projekt Tree
            //---------------------------------------------
            CategoryModel root = new CategoryModel(commandManager, menuService) { Name = "RootNode" };
            GameProjectModel order = new GameProjectModel(commandManager, menuService) { Name = "Game", IsExpanded = true };
            root.Items.Add(order);
            mProjectTreeService.Items.Add(root);
            mProjectTreeService.RootItem = root;

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
            GameProjectViewModel vm = _container.Resolve<GameProjectViewModel>();
            var model = _container.Resolve<GameProjectModel>();
            var view = _container.Resolve<GameProjectView>();

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
