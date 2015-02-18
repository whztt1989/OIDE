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
using OIDE.Service;
using OIDE.AssetBrowser.Interface.Services;
using OIDE.Core.ProjectTypes.Model;

namespace OIDE.Core
{
    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    [XmlInclude(typeof(ScenesListModel))]
    [XmlInclude(typeof(FileCategoryModel))]
    [XmlInclude(typeof(SceneDataModel))]
    [Serializable]
    public class GameProjectModel : ContentModel, IItem, ISerializableObj, ICategoryItem
    {
        private string result;

        public void Drop(IItem item) { }

        [XmlAttribute]
        public Int32 ID { get; set; }
        [XmlAttribute]
        public String Name { get; set; }

        public String ContentID { get; set; }

        private CollectionOfIItem m_Items;


        [Browsable(false)]
        //[XmlArray("Items")]
        // [XmlArrayItem(typeof(PhysicsObjectModel)), XmlArrayItem(typeof(SceneDataModel)), XmlArrayItem(typeof(CategoryModel)), XmlArrayItem(typeof(ScenesModel))]
        //[XmlElement(typeof(ScenesModel))]
        //[XmlElement(typeof(CategoryModel))]
        //[XmlElement(typeof(SceneDataModel))]
        //[XmlElement(typeof(PhysicsObjectModel))]
        public CollectionOfIItem Items { get { return m_Items; } set { m_Items = value; } }


        [Browsable(false)]
        [XmlIgnore]
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
        [XmlAttribute]
        public Boolean IsExpanded { get; set; }

        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsSelected { get; set; }

        [XmlIgnore]
        public Boolean HasChildren { get { return Items != null && Items.Count > 0 ? true : false; } }

        [XmlIgnore]
        public IItem Parent { get; private set; }

        [XmlIgnore]
        public ICommand RaiseConfirmation { get; private set; }
        //  public ICommand RaiseSelectAEF { get; private set; }

        //   public InteractionRequest<PSelectAEFViewModel> SelectAEFRequest { get; private set; }
        [XmlIgnore]
        public InteractionRequest<Confirmation> ConfirmationRequest { get; private set; }

        private void OnRaiseConfirmation()
        {
            this.ConfirmationRequest.Raise(
                new Confirmation { Content = "Confirmation Message", Title = "WPF Confirmation" },
                (cb) => { Result = cb.Confirmed ? "The user confirmed" : "The user cancelled"; });
        }


        [XmlIgnore]
        public List<System.Type> CanAddThisItems { get; private set; }

        [XmlIgnore]
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

        [XmlIgnore]
        public string Result
        {
            get { return this.result; }
            set { this.result = value; RaisePropertyChanged("Result"); }
        }

        public void SerializeObjectToXML()
        {
            ObjectSerialize.SerializeObjectToXML<GameProjectModel>(this, this.Location.ToString());
        }

        public Boolean Create() { return true; }
        public Boolean Open(object id) 
        { 
        
            return true; 
        }
        public Boolean Save(object param = null) { return true; }
        public Boolean Delete() { return true; }
        public Boolean Closing() { return true; }
        public void Refresh() { }
        public void Finish() { }
    
        public GameProjectModel()
        {
        }

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
          
            AssetFolder = "D:\\Projekte\\coop\\AssetData"; //todo set per propertygrid

            UnityContainer = container;
            m_Items = new CollectionOfIItem();
            this.RaiseConfirmation = new DelegateCommand(this.OnRaiseConfirmation);
            this.ConfirmationRequest = new InteractionRequest<Confirmation>();
            this.CanAddThisItems = new List<Type>();

            CanAddThisItems.Add(typeof(OIDE_RFS));
            CanAddThisItems.Add(typeof(OIDEZipArchive));
         //   CanAddThisItems.Add(typeof(GameDBFileModel));
            //  this.SelectAEFRequest = new InteractionRequest<PSelectAEFViewModel>();
            //  this.RaiseSelectAEF = new DelegateCommand(this.OnRaiseSelectAEF);

            var assetBrowser = container.Resolve<IAssetBrowserTreeService>();
            OIDE_RFS fileAssets = new OIDE_RFS(this, container) { Name = "Assets VFS", ContentID = "RootVFSID:##:" };
            fileAssets.Open(AssetFolder);
            m_Items.Add(fileAssets);
            assetBrowser.SetAsRoot(fileAssets);
            //foreach (var item in fileAssets.Items)
            //    assetBrowser.AddItem(item);


            var gsc = new OIDE.Core.ProjectTypes.Model.GameStateCategory() { Name = "GameStates" };
            gsc.Items = new CollectionOfIItem();
            var gsctrl = new OIDE.Core.ProjectTypes.Model.GameStateModel() { Name = "XTControllerState" };
            gsc.Items.Add(gsctrl);
            var gsctrl1 = new OIDE.Core.ProjectTypes.Model.GameStateModel() { Name = "XTUIState" };
            gsc.Items.Add(gsctrl1);
            var gsctrl2 = new OIDE.Core.ProjectTypes.Model.GameStateModel() { Name = "XTInputControllerState" };
            gsc.Items.Add(gsctrl2);
            m_Items.Add(gsc);


            //-----------------------------------------
            //Customize Database category structure
            //-----------------------------------------
            GameDBFileModel dbData = new GameDBFileModel(this, container);
           dbData.IsExpanded = true;

            //            PredefObjectCategoyModel predefCategory = new PredefObjectCategoyModel(this, container) { Name = "Ogre System Objects" };

            //            predefCategory.Items.Add(new PredefObjectModel() { Name = "Cube" });
            ////sceneService.PredefObjects  ??????
            //            m_Items.Add(predefCategory);

            m_Items.Add(dbData);
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