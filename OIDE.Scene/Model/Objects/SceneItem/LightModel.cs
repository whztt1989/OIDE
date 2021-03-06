﻿#region License

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
    
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using OIDE.Scene.Interface.Services;
using System.Xml.Serialization;
using OIDE.Scene.Interface;
using Wide.Interfaces.Services;
using Wide.Core.Services;
using OIDE.Scene.Service;

namespace OIDE.Scene.Model
{
    public class LightModel : SceneItem
    {
        public void Drop(IItem item) { }


        [XmlIgnore]
        [Browsable(false)]
        public override List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem miSave = new MenuItem() {  Header = "Save" };
                list.Add(miSave);
                return list;
            }
        }

      
        public Boolean Create(IUnityContainer unityContainer) { return true; }
        public Boolean Open(IUnityContainer unityContainer, object id) { return true; }
        public Boolean Save(object param) { return true; }
        public void Refresh() { }
        public Boolean Delete() { return true; }
 
        /// <summary>
        /// default constructor for serlialization
        /// </summary>
        public LightModel()
        {
                SceneItems = new CollectionOfISceneItem();
        }
    }
}
