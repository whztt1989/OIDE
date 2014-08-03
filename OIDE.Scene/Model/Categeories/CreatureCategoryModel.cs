﻿#region License

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

    public class CreatureCategoryModel : ViewModelBase, ISceneItem
    {
        private Boolean m_IsExpanded;

        public String Name { get; set; }

        public void Drop(IItem item) { }

        [Browsable(false)]
        public CollectionOfIItem Items { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }

        public String ContentID { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public List<MenuItem> MenuOptions 
        {
              get
            {
                List<MenuItem>  menuOptions = new List<MenuItem>();
                MenuItem miAdd = new MenuItem() { Command = new CmdAddCharacterObj(UnityContainer), CommandParameter = this, Header = "Create character object" };
                menuOptions.Add(miAdd);

                return menuOptions;
            }
        }

      
        public Boolean IsExpanded { get { return m_IsExpanded; } set { m_IsExpanded = value; RaisePropertyChanged("IsExpanded"); } }
        public Boolean IsSelected { get; set; }
        public Boolean Enabled { get; set; }
        public Boolean Visible { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        [Browsable(false)]
        [XmlIgnore]
        public IItem Parent { get; private set; }

        public Boolean Create() { return true; }
        public Boolean Open(object id) { return true; }
        public Boolean Save() { return true; }
        public Boolean Delete() { return true; }
        public Boolean Closing() { return true; }

        [Browsable(false)]
        [XmlIgnore]
        public IUnityContainer UnityContainer { get; private set; }

        public CreatureCategoryModel()
        {

        }

        public CreatureCategoryModel(IItem parent, IUnityContainer container)
        {
            UnityContainer = container;
            Parent = parent;
            Items = new CollectionOfIItem();
            SceneItems = new ObservableCollection<ISceneItem>();
        }
    }

    public class CmdAddCreatureObj : ICommand
    {
        private IUnityContainer mContainer;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            CreatureCategoryModel parent = parameter as CreatureCategoryModel;

            CharacterObjModel pom = new CharacterObjModel(parent, parent.UnityContainer) { Name = "Character Obj NEW", ContentID = "CharacterEntID:##" };

            pom.Create();

            parent.Items.Add(pom);

            ISceneService sceneService = parent.UnityContainer.Resolve<ISceneService>();
        }

        public CmdAddCreatureObj(IUnityContainer container)
        {
            mContainer = container;
        }
    }
}