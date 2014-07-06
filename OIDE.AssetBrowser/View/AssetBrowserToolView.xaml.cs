#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
//using MarkdownSharp;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using System.Threading;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using Module.Properties.Interface.Services;
using Module.Properties.Interface;
using OIDE.AssetBrowser.Interface.Services;
using System.Collections.Generic;
using System.Windows.Input;

namespace OIDE.AssetBrowser
{
    /// <summary>
    /// Interaction logic for MDView.xaml
    /// </summary>
    public partial class AssetBrowserToolView :UserControl, IContentView, INotifyPropertyChanged
    {
        IPropertiesService mPropertiesService;
        IAssetBrowserTreeService mAssetBrowserTreeService;
        IUnityContainer m_Container;

        public AssetBrowserToolView(IUnityContainer container)
        {
            InitializeComponent();

            m_Container = container;
            mPropertiesService = container.Resolve<IPropertiesService>();
            
            mAssetBrowserTreeService = container.Resolve<IAssetBrowserTreeService>();
         //   mAssetBrowserTreeService.TreeList = _treeList;
           
        }

        /// <summary>
        /// Should be called when a property value has changed
        /// </summary>
        /// <param name="propertyName">The property name</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            //standardOutput.AutoFlush = true;
            //Console.SetOut(standardOutput);
        }

        //doesnt trigger?
        //private void ContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        //{
        //  ContextMenu tmp =  (ContextMenu)sender;
        //  tmp.ItemsSource = mAssetBrowserTreeService.CurrentItem.MenuOptions;
        //}

        private void ContextMenu_Opened(object sender, System.Windows.RoutedEventArgs e)
        {
            ContextMenu tmp = (ContextMenu)sender;

            //multiple items selected
            //if (treeView.SelectedItems.Count > 1)
            //{
            //    List<MenuItem> lmi = new List<MenuItem>();
            //    lmi.Add(new MenuItem() { Header = "Multiselected!" });
            //    tmp.ItemsSource = lmi;
            //}
            //one item selected
            if (mAssetBrowserTreeService.SelectedItem != null)
            {
                tmp.ItemsSource = mAssetBrowserTreeService.SelectedItem.MenuOptions;
            }

            //nothing selected          
        }

        private void _treeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void _treeList_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (mAssetBrowserTreeService.SelectedItem == null || mAssetBrowserTreeService.SelectedItem.MenuOptions == null)
            {
                e.Handled = true;
            }
        }

        protected void ProjItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
           var item = ((TreeViewItem)sender).Header as IItem; //Casting back to the binded IITem

            if (item != null)
            {
                //TreeNode tn = tmp.GetValue();.SelectedNode;
                //if (tn != null)
                //{
                    IWorkspace workspace = m_Container.Resolve<AbstractWorkspace>();

                    IOpenDocumentService odS = m_Container.Resolve<IOpenDocumentService>();

                    var openValue = odS.OpenFromID(item.ContentID,true);
                    //creg.GetContentHandler();

                    //NewContentAttribute newContent = window.NewContent;
                    //if (newContent != null)
                    //{
                    //    IContentHandler handler = _dictionary[newContent];
                     //  var openValue = handler.NewContent(newContent);
                 //   workspace.Documents.Add(openValue);
                //    workspace.ActiveDocument = openValue;
                    //}
             //       mAssetBrowserTreeService.SelectedItem = (IItem)tn.Tag;
             //       mPropertiesService.CurrentItem = (IItem)tn.Tag;
         //       }
            }

          //  var track = ((ListViewItem)sender).Content as Track; //Casting back to the binded Track
        }

        private void TreeView_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tmp = (TreeView)sender;
            if (tmp != null)
            {
                IItem tn = tmp.SelectedItem as IItem;
                if (tn != null)
                {
                    mAssetBrowserTreeService.SelectedItem = tn;
                    mPropertiesService.CurrentItem =tn;
                }
            }
        }
    }
}