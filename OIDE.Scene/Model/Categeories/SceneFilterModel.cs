﻿#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using Module.Properties.Interface;
using OIDE.Scene.Interface.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using System.Xml.Serialization;
using Microsoft.Practices.Unity;

namespace OIDE.Scene
{
    public class SceneFilterModel : ViewModelBase, ISceneItem
    {
        public String Name { get;set; }
        public CollectionOfIItem Items { get; private set; }
        public String ContentID { get; set; }

        public Boolean Enabled { get; set; }
        public Boolean Visible { get; set; }


        [XmlIgnore]
        public List<MenuItem> MenuOptions { get; protected set; }
        public Boolean IsExpanded { get; set; }
        public Boolean IsSelected { get; set; }
     
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }
       
         [XmlIgnore]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

         [XmlIgnore]
        public IItem Parent { get; private set; }

         public SceneFilterModel()
        {

        }

         public Boolean Open() { return true; }
         public Boolean Save() { return true; }
         public Boolean Delete() { return true; }

         public IUnityContainer UnityContainer { get; private set; }
         public TreeNode TreeNode { get; set; }

        public SceneFilterModel(IItem parent, IUnityContainer unityContainer )
        {
            UnityContainer = unityContainer;
            Parent = parent;
            Items = new CollectionOfIItem();
            MenuOptions = new List<MenuItem>();

            MenuItem mib1a = new MenuItem();
            mib1a.Header = "Text.xaml";
            MenuOptions.Add(mib1a);
        }
    }
}