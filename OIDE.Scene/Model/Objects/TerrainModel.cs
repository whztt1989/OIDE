using OIDE.Scene.Interface.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Module.Properties.Interface;
using Microsoft.Practices.Unity;

namespace OIDE.Scene.Model
{
    public class TerrainModel : ISceneItem
    {
        public IItem Parent { get; private set; }
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }

        public void Drop(IItem item) { }

        public String ContentID { get; set; }
      

        public Int32 ID { get; protected set; }
        public String Name { get; set; }

        public ObservableCollection<ISceneItem> SceneItems { get; private set; }
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

        public Boolean Create() { return true; }
        public Boolean Open(object id) { return true; }
        public Boolean Save() { return true; }
        public Boolean Delete() { return true; }
        public Boolean Closing() { return true; }

        [Browsable(false)]
        public Boolean IsExpanded { get; set; }
        [Browsable(false)]
        public Boolean IsSelected { get; set; }
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        public IUnityContainer UnityContainer { get; private set; }
        public TreeNode TreeNode { get; set; }

        public TerrainModel(ISceneItem parent,IUnityContainer unityContainer )
        {
            UnityContainer = unityContainer;
            Parent = parent;
            SceneItems = new ObservableCollection<ISceneItem>();
            parent.SceneItems.Add(this);
        }

    }
}
