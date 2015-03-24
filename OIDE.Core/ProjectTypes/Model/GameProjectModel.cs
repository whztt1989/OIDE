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

namespace OIDE.Core
{
    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    public class GameProjectModel : ContentModel, IItem, ICategoryItem
    {
        private GameProjectData m_GameProjectData;
        private string result;
        private String m_Name;

        public void Drop(IItem item) { }
        public Int32 ID { get; set; }
        public String Name { get { return m_GameProjectData.Name; }
            set {
                if (m_GameProjectData.Name != value)
                    IsDirty = true;

                  m_GameProjectData.Name = value; 

                RaisePropertyChanged("Name");
            } }
        public String ContentID { get; set; }

        [Browsable(false)]
        //[XmlArray("Items")]
        // [XmlArrayItem(typeof(PhysicsObjectModel)), XmlArrayItem(typeof(SceneDataModel)), XmlArrayItem(typeof(CategoryModel)), XmlArrayItem(typeof(ScenesModel))]
        //[XmlElement(typeof(ScenesModel))]
        //[XmlElement(typeof(CategoryModel))]
        //[XmlElement(typeof(SceneDataModel))]
        //[XmlElement(typeof(PhysicsObjectModel))]
    //    [XmlElement(typeof(PhysicsObjectModel))]
        public CollectionOfIItem Items { get { return m_GameProjectData.Items; } set { m_GameProjectData.Items = value; } }

        [Browsable(false)]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();

                MenuItem miAddItem = new MenuItem() { Header = "Add Item" };

                foreach (var type in CanAddThisItems)
                {
                    miAddItem.Items.Add(new MenuItem() { Header = type.Name, Command = new CmdAddExistingItemToGameProject(this), CommandParameter = type });
                }

                list.Add(miAddItem);
                list.Add(new MenuItem() { Header = "Save" });
                list.Add(new MenuItem() { Header = "Rename" });
                return list;
            }
        }

        [Browsable(false)]
        public Boolean IsExpanded { 
            get { return m_GameProjectData.IsExpanded; } 
            set {
                if (m_GameProjectData.IsExpanded != value)
                    IsDirty = true;

                m_GameProjectData.IsExpanded = value;
            } }

        [Browsable(false)]
        public Boolean IsSelected { get; set; }

        public Boolean HasChildren { get { return Items != null && Items.Count > 0 ? true : false; } }

        public IItem Parent { get; private set; }

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

        public IUnityContainer UnityContainer { get; private set; }

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

        public Boolean Create(IUnityContainer unityContainer) { return true; }
        public Boolean Open(IUnityContainer unityContainer, object id) 
        {
            //-----------------------------------
            // Deserialize Object
            //-----------------------------------
            m_GameProjectData = ObjectSerialize.DeSerializeObjectFromXML<GameProjectData>(m_GameProjectData, id.ToString());

            return true; 
        }
        public Boolean Save(object param = null)
        {

            //-----------------------------------
            // Serialize Object
            //-----------------------------------
            ObjectSerialize.SerializeObjectToXML<GameProjectData>(m_GameProjectData, param.ToString());
                 

            return true; 
        }
        public Boolean Delete() { return true; }
        public Boolean Closing() { return true; }
        public void Refresh() { }
        public void Finish() { }
    
        #region Settings

        public String AssetFolder { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MDModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public GameProjectModel(IUnityContainer container, ISceneService sceneService, ICommandManager commandManager, IMenuService menuService)
         //   : base(commandManager, menuService)
        {
            UnityContainer = container;
         //   m_Items = new CollectionOfIItem();
            m_GameProjectData = new GameProjectData();
            
            this.RaiseConfirmation = new DelegateCommand(this.OnRaiseConfirmation);
            this.ConfirmationRequest = new InteractionRequest<Confirmation>();
            this.CanAddThisItems = new List<Type>();
        
            CanAddThisItems.Add(typeof(OIDE_RFS));
            CanAddThisItems.Add(typeof(OIDEZipArchive));
            CanAddThisItems.Add(typeof(GameDBFileModel));
       //   CanAddThisItems.Add(typeof(GameDBFileModel));
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
       //###     GameDBFileModel dbData = new GameDBFileModel(this, container);
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

    public class CmdRenameGameProject : ICommand
    {
        private GameProjectModel mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mpm.Save();
        }

        public CmdRenameGameProject(GameProjectModel pm)
        {
            mpm = pm;
        }
    }

    public class CmdAddNewItemToGameProject : ICommand
    {
        private GameProjectModel mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mpm.Save();
        }

        public CmdAddNewItemToGameProject(GameProjectModel pm)
        {
            mpm = pm;
        }
    }

    public class CmdAddExistingItemToGameProject : IHistoryCommand
    {
        private GameProjectModel mpm;
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
                mpm.Items.Add(new OIDEZipArchive(mpm, mpm.UnityContainer , "") { Name = "Unknown.zip" });
               // Type instance = (Type)Activator.CreateInstance(t);
               // object obj = t.GetConstructor(new Type[] { }).Invoke(new object[] { });
             //   mpm.Items.Add(obj as IItem);
            }
            else if (t.Name == "GameDBFileModel")
            {
                GameDBFileModel dbData = new GameDBFileModel(mpm, mpm.UnityContainer) {Name = "DBFile"};
                mpm.Items.Add(dbData);
                dbData.IsExpanded = true;
            }
            else if (t.Name == "OIDE_RFS")
            {
                mpm.AssetFolder = @"E:\Projekte\coop\OIDE\data"; //todo set per propertygrid
                var assetBrowser = mpm.UnityContainer.Resolve<IAssetBrowserTreeService>();
                OIDE_RFS fileAssets = new OIDE_RFS(mpm, mpm.UnityContainer) { Name = "Assets VFS", ContentID = "RootVFSID:##:" };
                fileAssets.Open(mpm.UnityContainer, mpm.AssetFolder);
                //        m_Items.Add(fileAssets);
                assetBrowser.SetAsRoot(fileAssets);

                mpm.Items.Add(new OIDE_RFS(mpm, mpm.UnityContainer) { Name = "RFS" });
            }
        }

        public CmdAddExistingItemToGameProject(GameProjectModel pm)
        {
            mpm = pm;
        }

        public  bool CanRedo() { return true; }
        public bool CanUndo() { return true; }
        public void Redo() { }
        public string ShortMessage() { return "add item"; }
        public void Undo() { }

    }

    public class CmdSaveGameProject : ICommand
    {
        private GameProjectModel mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mpm.Save();
        }

        public CmdSaveGameProject(GameProjectModel pm)
        {
            mpm = pm;
        }
    }
}