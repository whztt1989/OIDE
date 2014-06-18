using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using OIDE.Scene.Interface.Services;
using Module.Properties.Interface;

namespace OIDE.DAL.Model
{


    /// <summary>
    /// Complete Scene description
    /// </summary>
    public class SceneData : IItem
    {
        private ObservableCollection<IItem> m_Items;
        ICommand m_cmdCreateFile;
        ICommand m_cmdDelete;

        public Int32 ID { get; protected set; }
        public String Name { get; set; }
        [Browsable(false)]
        public ObservableCollection<IItem> Items { get { return m_Items; } }
        public Guid Guid { get; private set; }
        [Browsable(false)]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem miSave = new MenuItem() { Command = m_cmdCreateFile, Header = "Add File" };
                list.Add(miSave);
                MenuItem miDelete = new MenuItem() { Command = m_cmdDelete, Header = "Delete" };
                list.Add(miDelete);
                return list;
            }
        }

        [Browsable(false)]
        public Boolean IsExpanded { get; set; }
        [Browsable(false)]
        public Boolean IsSelected { get; set; }
        public Boolean HasChildren { get { return Items != null && Items.Count > 0 ? true : false; } }

        public IItem Parent { get; private set; }
       

        #region Scene Data


        #endregion


        public SceneData(IItem parent)
        {
            Parent = parent;
            m_Items = new ObservableCollection<IItem>();
        }

    }
}
