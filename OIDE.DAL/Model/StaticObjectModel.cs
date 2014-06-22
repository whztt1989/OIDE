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

namespace OIDE.DAL.Model
{
    public class StaticObjectModel : ISceneItem
    {
        public IItem Parent { get; private set; }
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }

        public Int32 ID { get; protected set; }
        public String Name { get; set; }
        [Browsable(false)]
        public CollectionOfIItem Items { get; private set; }
        public Guid Guid { get; private set; }
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
        public Boolean HasChildren { get { return Items != null && Items.Count > 0 ? true : false; } }


        public StaticObjectModel(IItem parent)
        {
            Parent = parent;
        }


    }
}
