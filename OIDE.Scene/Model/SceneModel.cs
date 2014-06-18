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
using System.Windows.Input;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Commands;

namespace OIDE.Scene.Model
{
    public class CmdCreateFile : ICommand
    {
        private SceneModel m_model;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
          //  m_model.Items.Add(new CameraModel(m_model) { Name = "Camera 1" });
            //IDAL dbI = new IDAL();

            //// To serialize the hashtable and its key/value pairs,  
            //// you must first open a stream for writing. 
            //// In this case, use a file stream.
            //using (MemoryStream inputStream = new MemoryStream())
            //{
            //    // write to a file
            //    ProtoBuf.Serializer.Serialize(inputStream, mpm.Data);

            //    if (mpm.ID > -1)
            //        dbI.updatePhysics(mpm.ID, inputStream.ToArray());
            //    else
            //        dbI.insertPhysics(mpm.ID, inputStream.ToArray());
            //}

            //DLL_Singleton.Instance.updateObject(0, (int)ObjType.Physic);
        }

        public CmdCreateFile(SceneModel model)
        {
            m_model = model;
        }
    }

    public class CmdDeleteScene : ICommand
    {
        private SceneModel m_model;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_model.Items.Clear();
            m_model.Parent.Items.Remove(m_model);
            //IDAL dbI = new IDAL();

            //// To serialize the hashtable and its key/value pairs,  
            //// you must first open a stream for writing. 
            //// In this case, use a file stream.
            //using (MemoryStream inputStream = new MemoryStream())
            //{
            //    // write to a file
            //    ProtoBuf.Serializer.Serialize(inputStream, mpm.Data);

            //    if (mpm.ID > -1)
            //        dbI.updatePhysics(mpm.ID, inputStream.ToArray());
            //    else
            //        dbI.insertPhysics(mpm.ID, inputStream.ToArray());
            //}

            //DLL_Singleton.Instance.updateObject(0, (int)ObjType.Physic);
        }

        public CmdDeleteScene(SceneModel model)
        {
            m_model = model;
        }
    }


    /// <summary>
    /// Complete Scene description
    /// </summary>
    public class SceneModel : TextModel, IScene
    {
         private SceneModel m_model;

        public event EventHandler CanExecuteChanged;
      
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

        private string result;

        public Boolean IsExpanded { get; set; }
        [Browsable(false)]
        public Boolean IsSelected { get; set; }
        public Boolean HasChildren { get { return Items != null && Items.Count > 0 ? true : false; } }

        public ICommand RaiseConfirmation { get; private set; }
        public ICommand RaiseSelectAEF { get; private set; }

        //public InteractionRequest<PSelectAEFViewModel> SelectAEFRequest { get; private set; }
        public InteractionRequest<Confirmation> ConfirmationRequest { get; private set; }

        private void OnRaiseConfirmation()
        {
            this.ConfirmationRequest.Raise( new Confirmation { Content = "Confirmation Message", Title = "WPF Confirmation" },
                (cb) => { Result = cb.Confirmed ? "The user confirmed" : "The user cancelled"; });
        }


        //private void RaiseConfirmation()
        //{
        //    this.RaiseConfirmation.Raise(
        //        new PSelectAEFViewModel { Title = "Items" },
        //        (vm) =>
        //        {
        //            if (vm.SelectedItem != null)
        //            {
        //                Result = "The user selected: " + vm.SelectedItem;
        //            }
        //            else
        //            {
        //                Result = "The user didn't select an item.";
        //            }
        //        });
        //}

        public string Result
        {
            get
            {
                return this.result;
            }

            set
            {
                this.result = value;
                RaisePropertyChanged("Result");
            }
        }


           public IItem Parent { get; private set; }
       


        public SceneModel(IItem parent , ICommandManager commandManager, IMenuService menuService)
            : base(commandManager, menuService)
        {
            Parent = parent;
            this.ConfirmationRequest = new InteractionRequest<Confirmation>();
            this.RaiseConfirmation = new DelegateCommand(this.OnRaiseConfirmation);

            m_Items = new ObservableCollection<IItem>();
            m_cmdCreateFile = new CmdCreateFile(this);
            m_cmdDelete = new CmdDeleteScene(this);

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
