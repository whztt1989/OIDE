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

using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Module.Properties.Interface;
using Module.Properties.Helpers;
using System.Windows;
using System.Windows.Controls;
using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using DragDrop = GongSolutions.Wpf.DragDrop.DragDrop;
using WIDE_Helpers;
using OIDE.Gorilla.Interface.Services;
using OIDE.Gorilla.Service;

namespace OIDE.Gorilla
{

    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    public class GorillaToolModel : ToolModel, IDropTarget
    {

        //ObservableCollection<ContentModel> mItems;

        //public ObservableCollection<ContentModel> Items { get { return mItems; } }


        //  ObservableCollection<ContentModel> mItems;

     //   public CollectionOfIItem Items { get { return mProjectTreeService.Items; } }
        ObservableCollection<IItem> schjrott;
        public ObservableCollection<IItem> Items
        { 
            get { return schjrott; }
            set { schjrott = value; RaisePropertyChanged("Items"); }
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            IItem sourceItem = dropInfo.Data as IItem;
            IItem targetItem = dropInfo.TargetItem as IItem;

            if (sourceItem != null && targetItem != null)
            {
                //over graph Gorilla item
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = System.Windows.DragDropEffects.Copy;
            }else
            {
                var item = dropInfo.VisualTargetItem as TreeViewItem;
                var view = dropInfo.VisualTarget as TreeView;
                bool directlyOverItem = false;
                var dc = view.DataContext as GorillaToolModel;

                if (dc != null)
                {
                    dropInfo.Effects = System.Windows.DragDropEffects.Copy;

                }
                if (item != null && view != null)
                {
                    var result = view.InputHitTest(dropInfo.DropPosition) as System.Windows.UIElement;
                    if (result != null)
                    {
                        var ancestor = result.GetVisualAncestor<TreeViewItem>();
                        directlyOverItem = (ancestor != null) && (ancestor == item);
                    }
                    //var ftc = item.DataContext as FolderTestCase;
                    //if (ftc != null && directlyOverItem)
                    //{
                    //    int insertIndex = dropInfo.InsertIndex;
                    //    IList destinationList = GetList(dropInfo.TargetCollection);
                    //    IEnumerable data = ExtractData(dropInfo.Data);

                    //    if (dropInfo.DragInfo.VisualSource == dropInfo.VisualTarget)
                    //    {
                    //        IList sourceList = GetList(dropInfo.DragInfo.SourceCollection);

                    //        foreach (object o in data)
                    //        {
                    //            int index = sourceList.IndexOf(o);

                    //            if (index != -1)
                    //            {
                    //                sourceList.RemoveAt(index);

                    //                if (sourceList == destinationList && index < insertIndex)
                    //                {
                    //                    --insertIndex;
                    //                }
                    //            }
                    //        }
                    //    }

                    //    foreach (object o in data)
                    //    {
                    //        ftc.Children.Add(o as TestCase);
                    //    }
                    //}
                }
                if (item == null && view != null)
                {
                    //Case when you drop anywhere on the tree and not on an item
                    //TestScenario scenaio = view.DataContext as TestScenario;
                    //TestCase tc = dropInfo.Data as TestCase;
                    //if (scenaio != null && tc != null)
                    //{
                    //    if (tc.Scenario == scenaio)
                    //    {
                    //        scenaio.Children.Remove(tc);
                    //    }
                    //    //Clone the dragged object - you never know if the object is dragged from one window to another
                    //    tc = tc.Clone() as TestCase;
                    //    tc.IsSelected = true;
                    //    scenaio.Children.Add(tc);
                    //}
                }
                else if (!directlyOverItem)
                {
                //    base.Drop(dropInfo);
                }

            }
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            IItem sourceItem = dropInfo.Data as IItem;
            IItem targetItem = dropInfo.TargetItem as IItem;

            if (targetItem != null)
                targetItem.Drop(sourceItem);
            //targetItem.Children.Add(sourceItem);
       

                var GorillaItem = sourceItem as IGorillaItem;
                if (GorillaItem != null)
                {
                    //DAL.MDB.GorillaNode node = new GorillaNode()
                    //{
                    //    Name = "NEWNode_" + GorillaItem.Name,
                    //    EntID = WIDE_Helper.StringToContentIDData(GorillaItem.ContentID).IntValue,

                    //};
                 //###   var gItem = new GorillaItem(m_GorillaService.SelectedGorilla, m_GorillaService.SelectedGorilla.UnityContainer);
                    //###    m_GorillaService.SelectedGorilla.GorillaItems.Add(gItem);

                    //IGorillaNode tmp = item as IGorillaNode;

                    //if (tmp.Node == null)
                    //    tmp.Node = new ProtoType.Node();

                    //m_GorillaItems.Add(tmp as IGorillaItem);
                }
        }

        //public ObservableCollection<IGorillaItem> Items { 
        //    get { 
        //        return m_GorillaService.GorillaItems;
        //    }
        //}
        IGorillaService m_GorillaService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MDModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public GorillaToolModel(IUnityContainer container) 
        {
      //      container.Resolve<ICommandManager>(), container.Resolve<IMenuService>()
     //       IGorillaService GorillaService, ICommandManager commandManager, IMenuService menuService
          //  mProjectTreeService = container.Resolve<IProjectTreeService>();

     //       mProjectTreeService.Items = new CollectionOfIItem();

            m_GorillaService = container.Resolve<IGorillaService>();
            Items = new ObservableCollection<IItem>();
         //   m_GorillaService.SelectedGorilla = new GorillaDataModel();
            m_GorillaService.GTM = this;
            //Service für project contextmenu buttons .....
            //tray.ContextMenu = new ContextMenu();
            //tray.ContextMenu.ItemsSource = _children;

         //   mItems = new ObservableCollection<ContentModel>();


            //CategoryModel cdata = new CategoryModel(commandManager, menuService) { Name = "Data" };
            //mItems.Add(cdata);
            //------------- Gorillas ----------------------
            //VMCategory cGorillas = new VMCategory(,commandManager, menuService) { Name = "Gorillas" };

            //p1.Items.Add(cGorillas);

            //CVMGorilla sv = new CVMGorilla() { Name = "Gorilla 1" };
            //sv.Items.Add(new CVMCategory() { Name = "Cameras" });
            //sv.Items.Add(new CVMCategory() { Name = "Models" });
            //sv.Items.Add(new CVMCategory() { Name = "Sound" });
            //cGorillas.Items.Add(sv);

        }

        //internal void SetLocation(object location)
        //{
        //    this.Location = location;
        //    RaisePropertyChanged("Location");
        //}

        //internal void SetDirty(bool value)
        //{
        //    this.IsDirty = value;
        //}
    } 
    
}
