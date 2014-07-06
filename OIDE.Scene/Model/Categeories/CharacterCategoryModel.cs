#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using Module.Properties.Interface;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using OIDE.Scene.Interface.Services;
using System.Xml.Serialization;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using OIDE.DAL;
using System.IO;
using OIDE.Scene.Model;

namespace OIDE.Scene
{

    public class CharacterCategoryModel : ViewModelBase, ISceneItem
    {
        public String Name { get;set; }
        public CollectionOfIItem Items { get; private set; }

        public ObservableCollection<ISceneItem> SceneItems { get; private set; }

        public String ContentID { get; set; }
      

        [XmlIgnore]
        public List<MenuItem> MenuOptions { get; protected set; }

        private Boolean m_IsExpanded;

        public Boolean IsExpanded { get { return m_IsExpanded; } set { m_IsExpanded = value; RaisePropertyChanged("IsExpanded"); } }
        public Boolean IsSelected { get; set; }
        public Boolean Enabled { get; set; }
        public Boolean Visible { get; set; }

        [XmlIgnore]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

         [XmlIgnore]
        public IItem Parent { get; private set; }

         public Boolean Open() { return true; }
         public Boolean Save() { return true; }
         public Boolean Delete() { return true; }

         public IUnityContainer UnityContainer { get; private set; }

         public TreeNode TreeNode { get; set; }

         public CharacterCategoryModel()
        {

        }

         public CharacterCategoryModel(IItem parent, IUnityContainer container)
        {
            UnityContainer = container;
            Parent = parent;
            Items = new CollectionOfIItem();
            SceneItems = new ObservableCollection<ISceneItem>();
            MenuOptions = new List<MenuItem>();

            MenuItem miAdd = new MenuItem() { Command = new CmdAddCharacterObj(container), CommandParameter = this, Header = "Add physic object" };
            MenuOptions.Add(miAdd);

        }
    }

    public class CmdAddCharacterObj : ICommand
    {
        private IUnityContainer mContainer;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            CharacterCategoryModel parent = parameter as CharacterCategoryModel;

        //    PhysicsObjectModel pom = new PhysicsObjectModel(parent, parent.UnityContainer) { Name = "Phys 1" , ContentID = "PhysicID:##" };


      //     parent.SceneItems.Add(pom);
  //         parent.Parent.Items.IsExpanded = true;
        //  parent.Items.Add(pom);

            ISceneService sceneService = parent.UnityContainer.Resolve<ISceneService>();
           // sceneService.TreeList.
            //IDAL dbI = new IDAL();

            //// To serialize the hashtable and its key/value pairs,  
            //// you must first open a stream for writing. 
            //// In this case, use a file stream.
            //using (MemoryStream inputStream = new MemoryStream())
            //{
            //    // write to a file
            //    ProtoBuf.Serializer.Serialize(inputStream, mpcm.Data);

            //    if (mpcm.ID > -1)
            //        dbI.updatePhysics(mpcm.ID, inputStream.ToArray());
            //    else
            //        dbI.insertPhysics(mpcm.ID, inputStream.ToArray());
            //}

          //todo !!  DLL_Singleton.Instance.consoleCmd("cmd physic 0"); //.updateObject(0, (int)ObjType.Physic);
        }

        public CmdAddCharacterObj(IUnityContainer container)
        {
            mContainer = container;
        }
    }
}