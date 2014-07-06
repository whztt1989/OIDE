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

namespace OIDE.Scene.Model
{
    public class CameraModel : ISceneItem, ISceneNode
    {
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }

        public String ContentID { get; set; }
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }

        public ProtoType.Node Node { get; set; }
        public OIDE.DAL.MDB.SceneNodes SceneNode { get; private set; }


        public Int32 ID { get; set; }
        public String Name { get; set; }
        [Browsable(false)]
        public CollectionOfIItem Items { get; private set; }

        public TreeNode TreeNode { get; set; }

        private ProtoType.Camera mData;

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
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }
        public IItem Parent { get; private set; }

        public Boolean Open() {

            //todo ! from db
           // SceneNodes = new SceneNodes() { NodeID = sNode.NodeID, EntID = sNode.Node.EntityID, SceneID = ID, Data = ProtoSerialize.Serialize(sNode.Node) };
                        
            return true; }
        public Boolean Save() { return true; }
        public Boolean Delete() { return true; }

        public IUnityContainer UnityContainer { get; private set; }


        public CameraModel (IItem parent,IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
            Parent = parent;
        }

    }
}
