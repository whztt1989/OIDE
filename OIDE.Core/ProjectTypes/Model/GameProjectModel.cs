#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System;
using Module.Properties.Interface;
using OIDE.DAL.Model;
using Module.PFExplorer;

namespace OIDE.Core
{
    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    public class GameProjectModel : TextModel , IItem
    {
        public Int32 ID { get; protected set; }
        public String Name { get; set; }
        [Browsable(false)]
        public ObservableCollection<IItem> Items { get { return m_Items; } }
        private ObservableCollection<IItem> m_Items;


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


        public IItem Parent { get; private set; }


        private string result;
     
        public ICommand RaiseConfirmation { get; private set; }
      //  public ICommand RaiseSelectAEF { get; private set; }

     //   public InteractionRequest<PSelectAEFViewModel> SelectAEFRequest { get; private set; }
        public InteractionRequest<Confirmation> ConfirmationRequest { get; private set; }

        private void OnRaiseConfirmation()
        {
            this.ConfirmationRequest.Raise(
                new Confirmation { Content = "Confirmation Message", Title = "WPF Confirmation" },
                (cb) => { Result = cb.Confirmed ? "The user confirmed" : "The user cancelled"; });
        }


        //private void OnRaiseSelectAEF()
        //{
        //    this.SelectAEFRequest.Raise(
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

        public GameProjectModel()
            : base(null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MDModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public GameProjectModel(ICommandManager commandManager, IMenuService menuService)
            : base(commandManager, menuService)
        {
            m_Items = new ObservableCollection<IItem>();
            this.RaiseConfirmation = new DelegateCommand(this.OnRaiseConfirmation);
            this.ConfirmationRequest = new InteractionRequest<Confirmation>();
          //  this.SelectAEFRequest = new InteractionRequest<PSelectAEFViewModel>();
          //  this.RaiseSelectAEF = new DelegateCommand(this.OnRaiseSelectAEF);
            ScenesModel scenes = new ScenesModel(this, commandManager, menuService) { Name = "Scenes" };
            SceneModel scene = new SceneModel(scenes) { Name = "Scene 1" };
            scene.Items.Add(new CategoryModel(scene,commandManager, menuService) { Name = "CharacterObjects" });
            scene.Items.Add(new CategoryModel(scene, commandManager, menuService) { Name = "AICharacterObjects" });
            scene.Items.Add(new CategoryModel(scene, commandManager, menuService) { Name = "StaticObjects" });
            scene.Items.Add(new PhysicsObjectModel(scene, commandManager, menuService) { Name = "PhysicObjects" });
          
            scenes.Items.Add(scene);
            m_Items.Add(scenes);
         
          

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

        public string HTMLResult { get; set; }

        public void SetHtml(string transform)
        {
            this.HTMLResult = transform;
            RaisePropertyChanged("HTMLResult");
        }
    }
}