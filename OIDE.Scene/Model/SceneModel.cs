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

        private string result;
    

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

        [Browsable(false)]
        public Boolean IsExpanded { get; set; }
        [Browsable(false)]
        public Boolean IsSelected { get; set; }
        public Boolean HasChildren { get { return Items != null && Items.Count > 0 ? true : false; } }


        public SceneModel(ICommandManager commandManager, IMenuService menuService, IUnityContainer container)
            : base(commandManager, menuService)
        {

            this.ConfirmationRequest = new InteractionRequest<Confirmation>();
            this.RaiseConfirmation = new DelegateCommand(this.OnRaiseConfirmation);
        
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
