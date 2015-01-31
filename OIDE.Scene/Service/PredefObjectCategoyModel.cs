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

namespace OIDE.Service
{

    public class PredefObjectCategoyModel : ViewModelBase, ISceneItem
    {
        public String Name { get; set; }

        [Browsable(false)]
        public CollectionOfIItem Items { get;  set; }

        [Browsable(false)]
        [XmlIgnore]
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }

        public void Drop(IItem item) { }

         public Int32 NodeID { get; set; }
        public String ContentID { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public List<MenuItem> MenuOptions { get; protected set; }

        public Boolean IsExpanded { get; set; }
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
        public Boolean Save(object param) { return true; }
        public void Refresh() { }
        public void Finish() { }
        public Boolean Delete() { return true; }
        public Boolean Closing() { return true; }

        [Browsable(false)]
        [XmlIgnore]
        public IUnityContainer UnityContainer { get; private set; }

        public PredefObjectCategoyModel()
        {

        }

        public PredefObjectCategoyModel(IItem parent, IUnityContainer container)
        {
            UnityContainer = container;
            Parent = parent;
            Items = new CollectionOfIItem();
            SceneItems = new ObservableCollection<ISceneItem>();
            MenuOptions = new List<MenuItem>();

            MenuItem miAdd = new MenuItem() { Header = "Create predefined object" };

            MenuItem miPredefObj1 = new MenuItem() { Command = new CmdCreatePredefObj(container), CommandParameter = this, Header = "Create Cube" };
            MenuItem miPredefObj2 = new MenuItem() { Command = new CmdCreatePredefObj(container), CommandParameter = this, Header = "Create Cone" };


            miAdd.Items.Add(miPredefObj2);
            miAdd.Items.Add(miPredefObj1);
            MenuOptions.Add(miAdd);
        }
    }

    public class CmdCreatePredefObj : ICommand
    {
        private IUnityContainer mContainer;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            PredefObjectCategoyModel parent = parameter as PredefObjectCategoyModel;

            //PredefObjectModel pom = new PredefObjectModel(parent, parent.UnityContainer) { Name = "Static Obj NEW", ContentID = "StaticEntID:##" };

            //pom.Save(parameter);

            //parent.Items.Add(pom);

            ISceneService sceneService = parent.UnityContainer.Resolve<ISceneService>();
        }

        public CmdCreatePredefObj(IUnityContainer container)
        {
            mContainer = container;
        }
    }
}