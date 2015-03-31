#region License

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
using OIDE.Scene.Model;
using OIDE.Scene.Interface;
using Wide.Core.Services;
using Module.PFExplorer.Service;

namespace OIDE.Scene
{

    public class SpawnPointCategoryModel : ProjectItemModel, ISceneItem
    {
        /// <summary>
        /// override for serializable
        /// </summary>
        [Browsable(false)]
        public override CollectionOfIItem Items { get { return base.Items; } set { base.Items = value; } }



        public void Drop(IItem item) { }

        [Browsable(false)]
        [XmlIgnore]
        public CollectionOfISceneItem SceneItems { get; private set; }


        [Browsable(false)]
        [XmlIgnore]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> menuOptions = new List<MenuItem>();
                MenuItem miAdd = new MenuItem() { Command = new CmdCreateSpawnPoint(UnityContainer), CommandParameter = this, Header = "Create SpawnPoint" };
                menuOptions.Add(miAdd);

                return menuOptions;
            }
        }

        public Boolean Create(IUnityContainer unityContainer) { return true; }
        public Boolean Open(IUnityContainer unityContainer, object id) { return true; }
        public Boolean Save(object param) { return true; }
        public void Refresh() { }
        public void Finish() { }
        public Boolean Delete() { return true; }
        public Boolean Closing() { return true; }

        public SpawnPointCategoryModel()
        {
             SceneItems = new CollectionOfISceneItem();
        }
    }


    public class CmdCreateSpawnPoint : ICommand
    {
        private IUnityContainer mContainer;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SpawnPointCategoryModel parent = parameter as SpawnPointCategoryModel;

            SpawnPointModel pom = new SpawnPointModel(parent, parent.UnityContainer) { Name = "SpawnPoint NEW", ContentID = "StaticEntID:##" };

            pom.Save(parameter);

            parent.Items.Add(pom);

            ISceneService sceneService = parent.UnityContainer.Resolve<ISceneService>();
        }

        public CmdCreateSpawnPoint(IUnityContainer container)
        {
            mContainer = container;
        }
    }
}