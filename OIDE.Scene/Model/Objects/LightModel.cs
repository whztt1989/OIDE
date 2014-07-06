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

namespace OIDE.Scene.Model
{
    public class LightModel : ISceneItem, ISceneNode
    {
        public IItem Parent { get; private set; }
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }

        public ProtoType.Node Node { get; set; }
        public OIDE.DAL.MDB.SceneNodes SceneNode { get; private set; }
        
        
        public String ContentID { get; set; }

        public TreeNode TreeNode { get; set; }
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }
        public Int32 ID { get; protected set; }
        public String Name { get; set; }
        [Browsable(false)]
        public CollectionOfIItem Items { get; private set; }
        [Browsable(false)]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem miSave = new MenuItem() {  Header = "Save" };
                list.Add(miSave);
                return list;
            }
        }

        [Browsable(false)]
        public Boolean IsExpanded { get; set; }
        [Browsable(false)]
        public Boolean IsSelected { get; set; }
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        public Boolean Open() { return true; }
        public Boolean Save() { return true; }
        public Boolean Delete() { return true; }

        public IUnityContainer UnityContainer { get; private set; }


        public LightModel (IItem parent,IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
            Parent = parent;
            SceneItems = new ObservableCollection<ISceneItem>();
        }

    }
}
