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
using System.Xml.Serialization;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace OIDE.Scene.Model
{
    public class EnvironmentModel : ISceneItem
    {
      public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }

        public String ContentID { get; set; }

        public void Drop(IItem item) { }

        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }

        [XmlIgnore]
        [ExpandableObject]
        public ProtoType.Node Node { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public OIDE.DAL.MDB.SceneNodes SceneNode { get; private set; }


        public String Name { get; set; }


        [Browsable(false)]
        public CollectionOfIItem Items { get; set; }

        [XmlIgnore]
        private ProtoType.Camera mData;

        [XmlIgnore]
        [Category("Conections")]
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public ProtoType.Camera Data
        {
            get
            {
                return mData;
            }
            set { mData = value; }
        }

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

        [XmlIgnore]
        [Browsable(false)]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        [XmlIgnore]
        [Browsable(false)]
        public IItem Parent { get; private set; }
        public Boolean Closing() { return true; }

        public Boolean Open(object id)
        {

            //todo ! from db
            // SceneNodes = new SceneNodes() { NodeID = sNode.NodeID, EntID = sNode.Node.EntityID, SceneID = ID, Data = ProtoSerialize.Serialize(sNode.Node) };

            return true;
        }

        public Boolean Create() { return true; }
        public Boolean Save() { return true; }
        public Boolean Delete() { return true; }

        [XmlIgnore]
        [Browsable(false)]
        public IUnityContainer UnityContainer { get; private set; }

        /// <summary>
        /// default constructor for serialization
        /// </summary>
        public EnvironmentModel()
        {

        }

        public EnvironmentModel(IItem parent, IUnityContainer unityContainer)
        {
            Items = new CollectionOfIItem();
        }
    }
}
