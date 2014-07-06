#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using Microsoft.Practices.Unity;
using OIDE.Scene.Interface.Services;
using System.Collections.ObjectModel;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Module.PFExplorer.Interface.Services;
using Module.Properties.Interface;
using OIDE.Scene.Model;

namespace OIDE.Scene
{

    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    public class SceneGraphToolModel : ToolModel
    {
        private ISceneService m_SceneService;

        //ObservableCollection<ContentModel> mItems;

        //public ObservableCollection<ContentModel> Items { get { return mItems; } }

        IProjectTreeService mProjectTreeService;

        //  ObservableCollection<ContentModel> mItems;

     //   public CollectionOfIItem Items { get { return mProjectTreeService.Items; } }
        ObservableCollection<ISceneItem> schjrott;
        public ObservableCollection<ISceneItem> Items
        { 
            get { return schjrott; }
            set { schjrott = value; RaisePropertyChanged("Items"); }
        }



        //public ObservableCollection<ISceneItem> Items { 
        //    get { 
        //        return m_SceneService.SceneItems;
        //    }
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="MDModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public SceneGraphToolModel(IUnityContainer container) 
        {
      //      container.Resolve<ICommandManager>(), container.Resolve<IMenuService>()
     //       ISceneService sceneService, ICommandManager commandManager, IMenuService menuService
            mProjectTreeService = container.Resolve<IProjectTreeService>();

            mProjectTreeService.Items = new CollectionOfIItem();

            m_SceneService = container.Resolve<ISceneService>();
            Items = new ObservableCollection<ISceneItem>();
         //   m_SceneService.SelectedScene = new SceneDataModel();
            m_SceneService.SGTM = this;
            //Service für project contextmenu buttons .....
            //tray.ContextMenu = new ContextMenu();
            //tray.ContextMenu.ItemsSource = _children;

         //   mItems = new ObservableCollection<ContentModel>();


            //CategoryModel cdata = new CategoryModel(commandManager, menuService) { Name = "Data" };
            //mItems.Add(cdata);
            //------------- Scenes ----------------------
            //VMCategory cScenes = new VMCategory(,commandManager, menuService) { Name = "Scenes" };

            //p1.Items.Add(cScenes);

            //CVMScene sv = new CVMScene() { Name = "Scene 1" };
            //sv.Items.Add(new CVMCategory() { Name = "Cameras" });
            //sv.Items.Add(new CVMCategory() { Name = "Models" });
            //sv.Items.Add(new CVMCategory() { Name = "Sound" });
            //cScenes.Items.Add(sv);

        }

        //internal void SetLocation(object location)
        //{
        //    this.Location = location;
        //    RaisePropertyChanged("Location");
        //}

        //internal void SetDirty(bool value)
        //{
        //    this.IsDirty = value;
        //}

        public string HTMLResult { get; set; }

        public void SetHtml(string transform)
        {
            this.HTMLResult = transform;
            RaisePropertyChanged("HTMLResult");
        }
    } 
    
}
