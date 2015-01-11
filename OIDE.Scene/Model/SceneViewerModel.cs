#region License

//The MIT License (MIT)

//Copyright (c) 2014 Konrad Huber

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

#endregion

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
using DAL;
using System.IO;
using System.Text.RegularExpressions;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using Wide.Interfaces;

namespace OIDE.Scene.Model
{



    /// <summary>
    /// Complete Scene description
    /// </summary>
    public class SceneViewerModel : ContentModel, IDropTarget
    {

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            IItem sourceItem = dropInfo.Data as IItem;
            IItem targetItem = dropInfo.TargetItem as IItem;

            if (sourceItem != null && targetItem != null)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Copy;
            }


            var item = dropInfo.VisualTargetItem as TreeViewItem;
            if (item != null)
            {
                var tc = item.DataContext as SceneViewerModel;
                if (tc != null)
                {
                    if (tc == dropInfo.Data)
                    {
                        dropInfo.Effects = DragDropEffects.None;
                        return;
                    }
                }

                //var folderTest = item.DataContext as FolderTestCase;
                //if (folderTest != null)
                //{
                //    if (folderTest.AreNewItemsAllowed())
                //    {
                //        folderTest.IsExpanded = true;
                //        base.DragOver(dropInfo);
                //    }
                //    return;
                //}

                //base.DragOver(dropInfo);
            }
            else
            {
                var view = dropInfo.VisualTarget as OIDE.Scene.View.SceneViewerView;
                if (view != null)
                {
                    IDropTarget dropHandler = GongSolutions.Wpf.DragDrop.DragDrop.GetDropHandler(view);
                    if (dropHandler == this)
                    {
                        dropInfo.Effects = DragDropEffects.Move;
                    }
                }
            }
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            IItem sourceItem = dropInfo.Data as IItem;
            IItem targetItem = dropInfo.TargetItem as IItem;

            var view = dropInfo.VisualTarget as OIDE.Scene.View.SceneViewerView;
            if (view != null)
            {
                IDropTarget dropHandler = GongSolutions.Wpf.DragDrop.DragDrop.GetDropHandler(view);
                if (dropHandler == this)
                {
                    //dropInfo.Effects = DragDropEffects.Move;
                    SceneViewerModel tmp  = dropHandler as SceneViewerModel;

                    if (sourceItem is ISceneItem)
                    {
                        ISceneNode tmpSceneNode = sourceItem as ISceneNode;

                        //todo
                        //if (tmpSceneNode.Node == null)
                        //{
                        //    tmpSceneNode.Node = new ProtoType.Node();
                        //    Point dropPos = dropInfo.DropPosition;
                        //    tmpSceneNode.Node.transform = new ProtoType.TransformStateData();
                        //    tmpSceneNode.Node.transform.loc = new ProtoType.Vec3f();
                        //    tmpSceneNode.Node.transform.loc.x = (float)dropPos.X;
                        //    tmpSceneNode.Node.transform.loc.y = (float)dropPos.Y;
                        //}
                    }

                    tmp.SceneService.SelectedScene.Drop(sourceItem);
                }
            }

         //   targetItem.Drop(sourceItem);
            //targetItem.Children.Add(sourceItem);
        }

     //    private SceneViewerModel m_model;

        public event EventHandler CanExecuteChanged;

        public Guid Guid { get; private set; }
        [Browsable(false)]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                //MenuItem miSave = new MenuItem() { Command = m_cmdCreateFile, Header = "Add File" };
                //list.Add(miSave);
                //MenuItem miDelete = new MenuItem() { Command = m_cmdDelete, Header = "Delete" };
                //list.Add(miDelete);
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

        //public void SetCurrentScene(IItem root)
        //{

        //}

        ISceneService m_SceneService;

        public ISceneService SceneService { get { return m_SceneService; } }

        public SceneViewerModel(ICommandManager commandManager, IMenuService menuService, ISceneService sceneService)
        {
            this.ConfirmationRequest = new InteractionRequest<Confirmation>();
            this.RaiseConfirmation = new DelegateCommand(this.OnRaiseConfirmation);

            m_SceneService = sceneService;

          

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
