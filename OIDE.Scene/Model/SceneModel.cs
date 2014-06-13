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
using Wide.Core.TextDocument;
using Wide.Interfaces.Services;
using Microsoft.Practices.Unity;

namespace OIDE.Scene.Model
{
    /// <summary>
    /// Complete Scene description
    /// </summary>
    internal class SceneModel : TextModel
    {
        public Int32 ID { get; protected set; }
        public String Name { get; set; }
        [Browsable(false)]
        public ObservableCollection<IItem> Items { get; private set; }
        public Guid Guid { get; private set; }
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
        public Boolean HasChildren { get { return Items != null && Items.Count > 0 ? true : false; } }


        public SceneModel(ICommandManager commandManager, IMenuService menuService, IUnityContainer container)
            : base(commandManager, menuService)
        {


         //   service.SetStartPage();

            //------------- Scenes ----------------------
            //VMCategory cScenes = new VMCategory(,commandManager, menuService) { Name = "Scenes" };

            //p1.Items.Add(cScenes);

            //CVMScene sv = new CVMScene() { Name = "Scene 1" };
            //sv.Items.Add(new CVMCategory() { Name = "Cameras" });
            //sv.Items.Add(new CVMCategory() { Name = "Models" });
            //sv.Items.Add(new CVMCategory() { Name = "Sound" });
            //cScenes.Items.Add(sv);

        }


        internal void SetLocation(object location)
        {
            this.Location = location;
            RaisePropertyChanged("Location");
        }

        internal void SetDirty(bool value)
        {
            this.IsDirty = value;
        }
    }
}
