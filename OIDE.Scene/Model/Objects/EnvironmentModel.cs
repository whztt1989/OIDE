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

namespace OIDE.Scene.Model
{
    public class EnvironmentModel : ISceneItem
    {
        public IItem Parent { get; private set; }
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }

        public String ContentID { get; set; }

        public ObservableCollection<ISceneItem> SceneItems { get; private set; }

        public Int32 ID { get; protected set; }
        public String Name { get; set; }
        [Browsable(false)]
        public CollectionOfIItem Items { get; private set; }

        public TreeNode TreeNode { get; set; }
    

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

        #region Environment Data

        
        #endregion 

        public Boolean Open() { return true; }
        public Boolean Save() { return true; }
        public Boolean Delete() { return true; }

        public IUnityContainer UnityContainer { get; private set; }


        public EnvironmentModel (IItem parent,IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
            Parent = parent;
            SceneItems = new ObservableCollection<ISceneItem>();
        }


    }
}
