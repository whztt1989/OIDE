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
    
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OIDE.Scene.Interface.Services;
using Module.Properties.Interface;
using Microsoft.Practices.Unity;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Xml.Serialization;

namespace OIDE.Scene.Model
{
    public class CameraModel : ISceneItem, ISceneNode
    {
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }
        public String ContentID { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }

        public void Drop(IItem item) { }


        public Byte[] ByteBuffer
        {
            get
            {
                //todo return m_FB_SceneNode.CreateByteBuffer();
                return new Byte[0];
            }
        }

        [XmlIgnore]
        public DAL.MDB.SceneNodes SceneNode { get; private set; }

        public Int32 ID { get; set; }
        public String Name { get; set; }
      
        [Browsable(false)]
        public CollectionOfIItem Items { get; private set; }

        //[XmlIgnore]
        //private ProtoType.Camera mData;

        //[Category("Conections")]
        //[Description("This property is a complex property and has no default editor.")]
        //[ExpandableObject]
        //public ProtoType.Camera Data
        //{
        //    get
        //    {
        //        return mData;
        //    }
        //    set { mData = value; }
        //}

        [XmlIgnore]
        [Browsable(false)]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem miSave = new MenuItem() { Header = "Save" };
                list.Add(miSave);
                return list;
            }
        }
   
        [Browsable(false)]
        public Boolean IsExpanded { get; set; }

        [Browsable(false)]
        public Boolean IsSelected { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        [Browsable(false)]
        [XmlIgnore]
        public IItem Parent { get; private set; }

        public Boolean Open(object id)
        {

            //todo ! from db
           // SceneNodes = new SceneNodes() { NodeID = sNode.NodeID, EntID = sNode.Node.EntityID, SceneID = ID, Data = ProtoSerialize.Serialize(sNode.Node) };
                        
            return true; }
        public Boolean Create() { return true; }
        public Boolean Save(object param) { return true; }
        public void Refresh() { }
        public void Finish() { }
        public Boolean Delete() { return true; }
        public Boolean Closing() { return true; }

        [Browsable(false)]
        [XmlIgnore]
        public IUnityContainer UnityContainer { get; private set; }

        public CameraModel (IItem parent,IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
            Parent = parent;
        }
    }
}
