﻿using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using Module.Properties.Types;
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
    public  class RootVFS : ContentModel, IVFSArchive
    {
        private string result;

        public void Drop(IItem item) { }

        private CmdSaveRFS CmdSaveRFS;

        /// <summary>
        /// List of menu options
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public override List<MenuItem> MenuOptions
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

        public Boolean Create(IUnityContainer unityContainer) { return true; }
        public Boolean Open(IUnityContainer unityContainer, object id)
        {
            FilePath = id.ToString();
            Name = Path.GetFileName(FilePath);

            if (!Directory.Exists(FilePath))
            {
               Name += " _not found";

                return false;
            }

            var itemProvider = new ItemProvider();

            if (Directory.Exists(FilePath))
                Items = itemProvider.GetItems(FilePath);
          

            return true; 
        }

        public Boolean Save(object param = null)
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


        public void Refresh() { }
        public void Finish() { }
    

       
        public Boolean Delete() { return true; }


        #region Archive Data

        /// <summary>
        /// in this case (root virtual filesystem) the filepath is the folderpath(root path)
        /// </summary>
        [Editor(typeof(FilePathEditor), typeof(FilePathEditor))]
        public String FilePath { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MDModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public RootVFS()
        {
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