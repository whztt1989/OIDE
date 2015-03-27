#region License

// Copyright (c) 2014 Huber Konrad
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System;
using System.Linq;
using Module.Properties.Interface;
using Module.PFExplorer;
using Module.PFExplorer.Utilities;
using System.Xml.Serialization;
using Module.PFExplorer.Interface;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Practices.Unity;
using OIDE.Scene.Model;
using OIDE.Scene.Interface.Services;
using OIDE.Scene;
using OIDE.VFS;
using OIDE.Core.Model;
using Module.History.Service;
using OIDE.AssetBrowser.Interface.Services;
using Helper.Utilities.Event;
using OIDE.Core.ProjectTypes.Model;
using DAL;
using Wide.Core.Services;

namespace OIDE.Core
{
    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    public class OIDEProjectModel : ContentModel, ICategoryItem
    {
        private OIDEProjectData m_OIDEProjectData;
        private string result;

        public void Drop(IItem item) { }

        public override String Name
        {
            get { return m_OIDEProjectData.Name; }
            set
            {
                if (m_OIDEProjectData.Name != value)
                    IsDirty = true;

                m_OIDEProjectData.Name = value;

                RaisePropertyChanged("Name");
            }
        }


        [Browsable(false)]
        //[XmlArray("Items")]
        // [XmlArrayItem(typeof(PhysicsObjectModel)), XmlArrayItem(typeof(SceneDataModel)), XmlArrayItem(typeof(CategoryModel)), XmlArrayItem(typeof(ScenesModel))]
        //[XmlElement(typeof(ScenesModel))]
        //[XmlElement(typeof(CategoryModel))]
        //[XmlElement(typeof(SceneDataModel))]
        //[XmlElement(typeof(PhysicsObjectModel))]
    //    [XmlElement(typeof(PhysicsObjectModel))]
        public override CollectionOfIItem Items { get { return m_OIDEProjectData.Items; } set { m_OIDEProjectData.Items = value; } }
        public ObservableCollection<OIDEStateModel> GameStates { get { return m_OIDEProjectData.GameStates; } set { m_OIDEProjectData.GameStates = value; } }

        //todo list selection
        public OIDEStateModel GameState { get; set; }

        [Browsable(false)]
        public override List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();

                MenuItem miAddItem = new MenuItem() { Header = "Add Item" };

                foreach (var type in CanAddThisItems)
                {
                    miAddItem.Items.Add(new MenuItem() { Header = type.Name, Command = new CmdAddExistingItemToOIDEProject(this), CommandParameter = type });
                }

                list.Add(miAddItem);
                list.Add(new MenuItem() { Header = "Save" });
                list.Add(new MenuItem() { Header = "Rename" });
                return list;
            }
        }

        [Browsable(false)]
        public override Boolean IsExpanded
        { 
            get { return m_OIDEProjectData.IsExpanded; } 
            set {
                if (m_OIDEProjectData.IsExpanded != value)
                    IsDirty = true;

                m_OIDEProjectData.IsExpanded = value;
            } }

       
        public ICommand RaiseConfirmation { get; private set; }
        //  public ICommand RaiseSelectAEF { get; private set; }

        //   public InteractionRequest<PSelectAEFViewModel> SelectAEFRequest { get; private set; }
        public InteractionRequest<Confirmation> ConfirmationRequest { get; private set; }

        private void OnRaiseConfirmation()
        {
            this.ConfirmationRequest.Raise( new Confirmation { Content = "Confirmation Message", Title = "WPF Confirmation" },
                (cb) => { Result = cb.Confirmed ? "The user confirmed" : "The user cancelled"; });
        }

        public List<System.Type> CanAddThisItems { get; private set; }

        //private void OnRaiseSelectAEF()
        //{
        //    this.SelectAEFRequest.Raise(
        //        new PSelectAEFViewModel { Title = "Items" },
        //        (vm) =>
        //        {
        //            if (vm.SelectedItem != null)
        //            {
        //                Result = "The user selected: " + vm.SelectedItem;
        //            }
        //            else
        //            {
        //                Result = "The user didn't select an item.";
        //            }
        //        });
        //}

        public string Result
        {
            get { return this.result; }
            set { this.result = value; RaisePropertyChanged("Result"); }
        }

        public override Boolean Create(IUnityContainer unityContainer) { return true; }

        private void SetRekursivParent(object child, object parent)
        {
            var childItem = child as IItem;
            if (childItem != null)
            {
                childItem.Parent = parent as IItem;

                if (childItem.Items != null)
                {
                    foreach (var cItem in childItem.Items)
                    {
                        SetRekursivParent(cItem, childItem);
                    }
                }
            }
        }

        public override Boolean Open(IUnityContainer unityContainer, object id) 
        {
            //-----------------------------------
            // Deserialize Object
            //-----------------------------------
            m_OIDEProjectData = ObjectSerialize.DeSerializeObjectFromXML<OIDEProjectData>(m_OIDEProjectData, id.ToString());

            try
            {
                //set Parents
                foreach (var item in m_OIDEProjectData.Items)
                {
                    SetRekursivParent(item, this);
                }
            }catch(Exception ex)
            {

            }

            return true; 
        }

        private void RekursiveSaveItem(CollectionOfIItem items)
        {
            foreach (var item in Items.Where(x => x.IsDirty))
                item.Save();
        }

        public override Boolean Save(object param = null)
        {

            //-----------------------------------
            // Serialize Object
            //-----------------------------------
            ObjectSerialize.SerializeObjectToXML<OIDEProjectData>(m_OIDEProjectData, param.ToString());
                 
            //save all dirty objects
            RekursiveSaveItem(Items);

            return true; 
        }
        public override Boolean Delete() { return true; }
        public override Boolean Closing() { return true; }
        public override void Refresh() { }
        public override void Finish() { }
    
        #region Settings

        public String AssetFolder { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MDModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public OIDEProjectModel(IUnityContainer container, ISceneService sceneService, ICommandManager commandManager, IMenuService menuService)
         //   : base(commandManager, menuService)
        {
            UnityContainer = container;
         //   m_Items = new CollectionOfIItem();
            m_OIDEProjectData = new OIDEProjectData();
            
            this.RaiseConfirmation = new DelegateCommand(this.OnRaiseConfirmation);
            this.ConfirmationRequest = new InteractionRequest<Confirmation>();
            this.CanAddThisItems = new List<Type>();
        
            CanAddThisItems.Add(typeof(OIDE_RFS));
            CanAddThisItems.Add(typeof(OIDEZipArchive));
            CanAddThisItems.Add(typeof(OIDEDBFileModel));
       //   CanAddThisItems.Add(typeof(OIDEDBFileModel));
            //  this.SelectAEFRequest = new InteractionRequest<PSelectAEFViewModel>();
            //  this.RaiseSelectAEF = new DelegateCommand(this.OnRaiseSelectAEF);

            //###    var assetBrowser = container.Resolve<IAssetBrowserTreeService>();
            //###     OIDE_RFS fileAssets = new OIDE_RFS(this, container) { Name = "Assets VFS", ContentID = "RootVFSID:##:" };
            //###      fileAssets.Open(AssetFolder);
            //###      m_Items.Add(fileAssets);
            //###       assetBrowser.SetAsRoot(fileAssets);
            //foreach (var item in fileAssets.Items)
            //    assetBrowser.AddItem(item);

            //-----------------------------------------
            //Customize Database category structure
            //-----------------------------------------
       //###     OIDEDBFileModel dbData = new OIDEDBFileModel(this, container);
       //###    dbData.IsExpanded = true;

            //            PredefObjectCategoyModel predefCategory = new PredefObjectCategoyModel(this, container) { Name = "Ogre System Objects" };

            //            predefCategory.Items.Add(new PredefObjectModel() { Name = "Cube" });
            ////sceneService.PredefObjects  ??????
            //            m_Items.Add(predefCategory);

            //###    m_Items.Add(dbData);
        }

        internal void SetLocation(object location)
        {
            this.Location = location;
            RaisePropertyChanged("Location");
        }

        internal void SetDirty(bool value)
        {
            this.IsDirty = value;
        }
    }

    public class CmdRenameOIDEProject : ICommand
    {
        private OIDEProjectModel mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mpm.Save();
        }

        public CmdRenameOIDEProject(OIDEProjectModel pm)
        {
            mpm = pm;
        }
    }

    public class CmdAddNewItemToOIDEProject : ICommand
    {
        private OIDEProjectModel mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mpm.Save();
        }

        public CmdAddNewItemToOIDEProject(OIDEProjectModel pm)
        {
            mpm = pm;
        }
    }

    public class CmdAddExistingItemToOIDEProject : IHistoryCommand
    {
        private OIDEProjectModel mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Type t = parameter as Type;
      
            //to create the objects i need the parameter data!!!!
   //         mpm.Save();
            if (t.Name == "OIDEZipArchive")
            {
                mpm.Items.Add(new OIDEZipArchive() { Parent = mpm, UnityContainer = mpm.UnityContainer, Name = "Unknown.zip" });
               // Type instance = (Type)Activator.CreateInstance(t);
               // object obj = t.GetConstructor(new Type[] { }).Invoke(new object[] { });
             //   mpm.Items.Add(obj as IItem);
            }
            else if (t.Name == "OIDEDBFileModel")
            {
                OIDEDBFileModel dbData = new OIDEDBFileModel() {Name = "DBFile"};
                mpm.Items.Add(dbData);
                dbData.IsExpanded = true;

                DBCategoryModel objects = new DBCategoryModel() { Parent = dbData, UnityContainer = mpm.UnityContainer, Name = "Entities" };
                objects.IsExpanded = true;

                StaticObjectCategoyModel staticObjects = new StaticObjectCategoyModel() { Parent = dbData, UnityContainer = mpm.UnityContainer, Name = "Statics" };
                objects.Items.Add(staticObjects);
                CharacterCategoryModel characterObjects = new CharacterCategoryModel() { Parent = dbData, UnityContainer = mpm.UnityContainer, Name = "Characters" };
                objects.Items.Add(characterObjects);

                SpawnPointCategoryModel allSpawns = new SpawnPointCategoryModel() { Parent = dbData, UnityContainer = mpm.UnityContainer, Name = "SpawnPoints" };
                SpawnPointCategoryModel allTrigger = new SpawnPointCategoryModel() { Parent = dbData, UnityContainer = mpm.UnityContainer, Name = "Triggers" };
                SpawnPointCategoryModel allLights = new SpawnPointCategoryModel() { Parent = dbData, UnityContainer = mpm.UnityContainer, Name = "Lights" };
                SpawnPointCategoryModel allSkies = new SpawnPointCategoryModel() { Parent = dbData, UnityContainer = mpm.UnityContainer, Name = "Skies" };
                SpawnPointCategoryModel allTerrains = new SpawnPointCategoryModel() { Parent = dbData, UnityContainer = mpm.UnityContainer, Name = "Terrains" };
                SpawnPointCategoryModel allSounds = new SpawnPointCategoryModel() { Parent = dbData, UnityContainer = mpm.UnityContainer, Name = "Sounds" };
                StaticObjectCategoyModel DynamicObjects = new StaticObjectCategoyModel() { Parent = dbData, UnityContainer = mpm.UnityContainer, Name = "Dynamics" };

                objects.Items.Add(allTrigger);
                objects.Items.Add(allSpawns);
                objects.Items.Add(allLights);
                objects.Items.Add(allSkies);
                objects.Items.Add(allTerrains);
                objects.Items.Add(allSounds);
                objects.Items.Add(DynamicObjects);
                dbData.Items.Add(objects);

            }
            else if (t.Name == "OIDE_RFS")
            {
                mpm.AssetFolder = @"E:\Projekte\coop\OIDE\data"; //todo set per propertygrid
                var assetBrowser = mpm.UnityContainer.Resolve<IAssetBrowserTreeService>();
                OIDE_RFS fileAssets = new OIDE_RFS() { Parent = mpm, UnityContainer = mpm.UnityContainer, Name = "Assets VFS", ContentID = "RootVFSID:##:" };
                fileAssets.Open(mpm.UnityContainer, mpm.AssetFolder);
                //        m_Items.Add(fileAssets);
                assetBrowser.SetAsRoot(fileAssets);

                mpm.Items.Add(new OIDE_RFS() { Parent = mpm, UnityContainer = mpm.UnityContainer, Name = "RFS" });
            }
        }

        public CmdAddExistingItemToOIDEProject(OIDEProjectModel pm)
        {
            mpm = pm;
        }

        public  bool CanRedo() { return true; }
        public bool CanUndo() { return true; }
        public void Redo() { }
        public string ShortMessage() { return "add item"; }
        public void Undo() { }

    }

    public class CmdSaveOIDEProject : ICommand
    {
        private OIDEProjectModel mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mpm.Save();
        }

        public CmdSaveOIDEProject(OIDEProjectModel pm)
        {
            mpm = pm;
        }
    }
}