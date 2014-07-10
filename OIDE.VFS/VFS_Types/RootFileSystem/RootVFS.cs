using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using OIDE.VFS.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace OIDE.VFS.VFS_Types.RootFileSystem
{
    public  class RootVFS : ContentModel, IItem, IVFSArchive
    {

        private string result;
        private CollectionOfIItem m_Items;

        [XmlAttribute]
        public Int32 ID { get; set; }
     
        [XmlAttribute]
        public String Name { get; set; }

        /// <summary>
        /// ContentID for WIDE
        /// </summary>
        public String ContentID { get; set; }
       
        /// <summary>
        /// Collection of subitems of this object
        /// </summary>
        [Browsable(false)]
        public CollectionOfIItem Items { get { return m_Items; } set { m_Items = value; } }

        private CmdSaveRFS CmdSaveRFS;

        /// <summary>
        /// List of menu options
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                list.Add(new MenuItem() { Header = "Import File(s)" });
                list.Add(new MenuItem() { Header = "Create Folder" });
                list.Add(new MenuItem() { Header = "Delete" });
                list.Add(new MenuItem() { Header = "Extract File/Folder To" });
                list.Add(new MenuItem() { Header = "Extract All To" });

                list.Add(new MenuItem() { Header = "Remove" });
                list.Add(new MenuItem() { Command = CmdSaveRFS, Header = "Save" });
                return list;
            }
        }

        #region Item

        /// <summary>
        /// Item ist Expanded
        /// </summary>
        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsExpanded { get; set; }

        /// <summary>
        /// Item is selected
        /// </summary>
        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsSelected { get; set; }

        /// <summary>
        /// Item has children
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public Boolean HasChildren { get { return Items != null && Items.Count > 0 ? true : false; } }

        /// <summary>
        /// parent of this item
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public IItem Parent { get; private set; }

        #endregion 

        [Browsable(false)]
        [XmlIgnore]
        public ICommand RaiseConfirmation { get; private set; }
        //  public ICommand RaiseSelectAEF { get; private set; }

        //   public InteractionRequest<PSelectAEFViewModel> SelectAEFRequest { get; private set; }
        [Browsable(false)]
        [XmlIgnore]
        public InteractionRequest<Confirmation> ConfirmationRequest { get; private set; }

        private void OnRaiseConfirmation()
        {
            this.ConfirmationRequest.Raise(
                new Confirmation { Content = "Confirmation Message", Title = "WPF Confirmation" },
                (cb) => { Result = cb.Confirmed ? "The user confirmed" : "The user cancelled"; });
        }

        [Browsable(false)]
        [XmlIgnore]
        public string Result
        {
            get { return this.result; }
            set { this.result = value; RaisePropertyChanged("Result"); }
        }

        public Boolean Create() { return true; }
        public Boolean Open()
        {

            if (!File.Exists(FilePath))
            {
               Name += " _not found";

                return false;
            }

            var itemProvider = new ItemProvider();

            if (Directory.Exists(FilePath))
                m_Items = itemProvider.GetItems(FilePath);
          

            return true; 
        }

        public Boolean Save()
        {
            if (!File.Exists(FilePath))
            {
                FilePath = FilePath + "/" + Name;
                File.Create(FilePath);
                this.IsDirty = false;

                var logger = UnityContainer.Resolve<ILoggerService>();
                logger.Log("Zip file '" + FilePath + "' created", LogCategory.Info, LogPriority.Medium);

                return true;
            }

            return false; 
        
        }
       
        public Boolean Delete() { return true; }


        #region Archive Data

        public String FilePath { get; set; }

        #endregion

        public RootVFS()
        {
            m_Items = new CollectionOfIItem();
        }

        [Browsable(false)]
        [XmlIgnore]
        public IUnityContainer UnityContainer { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="MDModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public RootVFS(IItem parent, IUnityContainer container)
        {
            UnityContainer = container;
            m_Items = new CollectionOfIItem();
            this.RaiseConfirmation = new DelegateCommand(this.OnRaiseConfirmation);
            this.ConfirmationRequest = new InteractionRequest<Confirmation>();
            this.CmdSaveRFS = new CmdSaveRFS(this);
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
 
        public bool CreateFolder()
        {
            return true;
        }
    }


    public class CmdSaveRFS : ICommand
    {
        private RootVFS mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mpm.Save();
        }

        public CmdSaveRFS(RootVFS pm)
        {
            mpm = pm;
        }
    }


    public class CmdCreateRFSFolder : ICommand
    {
        private RootVFS mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mpm.CreateFolder();
        }

        public CmdCreateRFSFolder(RootVFS pm)
        {
            mpm = pm;
        }
    }
}