#region License

// Copyright (c) 2014 Huber Konrad
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
using Module.PFExplorer;
using Module.PFExplorer.Utilities;
using System.Xml.Serialization;
using Module.PFExplorer.Interface;
using System.Xml;
using System.Xml.Schema;
using OIDE.DAL;
using Microsoft.Practices.Unity;
using OIDE.VFS.Interface;
using OIDE.VFS.VFS_Types.zip;
using System.IO.Compression;
using System.IO;
using WpfTreeViewBinding.Model;
using Microsoft.Win32;

namespace OIDE.VFS
{
    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    //  [XmlInclude(typeof(ScenesListModel))]
    // [XmlInclude(typeof(FileCategoryModel))]
    //[XmlInclude(typeof(SceneDataModel))]
    //[XmlInclude(typeof(PhysicsObjectModel))]
    //[Serializable]
    public class VFS_ZipModel : ContentModel, IItem, IVFSArchive
    {
        private string result;
        private CollectionOfIItem m_Items;

        [XmlAttribute]
        public Int32 ID { get; set; }

        public void Drop(IItem item) { }

        [XmlAttribute]
        public String Name { get; set; }

        public Boolean Closing() { return true; }
        /// <summary>
        /// ContentID for WIDE
        /// </summary>
        public String ContentID { get; set; }
       
        /// <summary>
        /// Collection of subitems of this object
        /// </summary>
        [Browsable(false)]
        //[XmlArray("Items")]
        // [XmlArrayItem(typeof(PhysicsObjectModel)), XmlArrayItem(typeof(SceneDataModel)), XmlArrayItem(typeof(CategoryModel)), XmlArrayItem(typeof(ScenesModel))]
        //[XmlElement(typeof(ScenesModel))]
        //[XmlElement(typeof(CategoryModel))]
        //[XmlElement(typeof(SceneDataModel))]
        //[XmlElement(typeof(PhysicsObjectModel))]
        public CollectionOfIItem Items { get { return m_Items; } set { m_Items = value; } }


        private CmdSaveVFSZip CmdSave;

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
                list.Add(new MenuItem() { Command = CmdSave, Header = "Save" });
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
        public Boolean Open(object id)
        {

            if (!File.Exists(FilePath))
            {
               Name += " _not found";

                return false;
            }
          

           using (ZipArchive archive = ZipFile.OpenRead(FilePath))
           {
               //Loops through each file in the zip file
               foreach (ZipArchiveEntry file in archive.Entries)
               {
                   Boolean isFile = false;
                    Boolean isDirectory = false;

                   //Outputs relevant file information to the console
                   Console.WriteLine("File Name: {0}", file.Name);
                   Console.WriteLine("File Size: {0} bytes", file.Length);
                   Console.WriteLine("Compression Ratio: {0}", ((double)file.CompressedLength / file.Length).ToString("0.0%"));

                   //Identifies the destination file name and path
                 //  fileUnzipFullName = Path.Combine(dirToUnzipTo, file.FullName);

                   //Extracts the files to the output folder in a safer manner
                 //  if (!System.IO.File.Exists(fileUnzipFullName))
                 //  {
                       //Calculates what the new full path for the unzipped file should be
                //   var itemProvider = new ItemProvider();

                   //if (Directory.Exists(@"D:\Projekte\Src Game\Data\Data_Release\Assets"))
                   //    mItems = itemProvider.GetItems(@"D:\Projekte\Src Game\Data\Data_Release\Assets");

                   //####    fileUnzipFullPath = Path.GetDirectoryName(fileUnzipFullName);

                       //Creates the directory (if it doesn't exist) for the new path
                   //####     Directory.CreateDirectory(fileUnzipFullPath);

                       //Extracts the file to (potentially new) path
                   //    file.ExtractToFile(fileUnzipFullName);

                       
                   //var items = new List<Item>();

                       //####       var dirInfo = new DirectoryInfo(path);

                   //create directory node
                    //   foreach (var directory in dirInfo.GetDirectories())
                     //  {

                       isFile = true;

                       if (isDirectory)
                       {
                           var item = new DirectoryItem
                           {
                               //####          Name = directory.Name,
                               //####          Path = directory.FullName,
                               //####          Items = GetItems(directory.FullName)
                           };

                           m_Items.Add(item);
                       }
                      // }

                   //create file node
                     //  foreach (var file in dirInfo.GetFiles())
                    //   {
                       if (isFile)
                       {
                           ICommandManager commandMgr = UnityContainer.Resolve<ICommandManager>();
                           IMenuService menuService = UnityContainer.Resolve<IMenuService>();
                           
                           var item = new FileItem(commandMgr, menuService)
                           {
                               ContentID  = "FILE:##:",
                               Name = file.Name,
                               Path = file.FullName
                           };

                           m_Items.Add(item);
                       }
                   //    }

               //    }
               }
           }

       //     VFS_Zip.SimpleUnzip();

            return true; }

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

        public VFS_ZipModel()
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
        public VFS_ZipModel(IItem parent, IUnityContainer container)
        {
            //ZIP support in .net 4.5
            //http://www.codeproject.com/Articles/381661/Creating-Zip-Files-Easily-in-NET

            //c++ zip zugriff?=
            //https://devel.nuclex.org/framework/browser/storage/Nuclex.Storage.Native/trunk/Include/Nuclex/Storage/FileSystem/ContainerFileCodec.h

            UnityContainer = container;
            m_Items = new CollectionOfIItem();
            this.RaiseConfirmation = new DelegateCommand(this.OnRaiseConfirmation);
            this.ConfirmationRequest = new InteractionRequest<Confirmation>();
            this.CmdSave = new CmdSaveVFSZip(this);
            //  this.SelectAEFRequest = new InteractionRequest<PSelectAEFViewModel>();
            //  this.RaiseSelectAEF = new DelegateCommand(this.OnRaiseSelectAEF);
            //ScenesModel scenes = new ScenesModel(this, commandManager, menuService) { Name = "Scenes" };
            //SceneDataModel scene = new SceneDataModel(scenes, commandManager, menuService) { Name = "Scene 1.xml" };
            //SceneDataModel sceneLogin = new SceneDataModel(scenes, commandManager, menuService) { Name = "Scene_Login.xml" };
            //SceneDataModel sceneCSelect = new SceneDataModel(scenes, commandManager, menuService) { Name = "Scene_CSelect.xml" };
            //scenes.Items.Add(sceneLogin);
            //scenes.Items.Add(sceneCSelect);

            //FileCategoryModel gameData = new FileCategoryModel(this, commandManager, menuService) { Name = "Asset Browser" };
            //FileCategoryModel objectsAB = new FileCategoryModel(gameData, commandManager, menuService) { Name = "Objects" };
            //FileCategoryModel objectAB1 = new FileCategoryModel(objectsAB, commandManager, menuService) { Name = "Floor" };
            //objectsAB.Items.Add(objectAB1);
            //gameData.Items.Add(objectsAB);
            //gameData.Items.Add(new FileCategoryModel(scene, commandManager, menuService) { Name = "Meshes" });
            //gameData.Items.Add(new FileCategoryModel(scene, commandManager, menuService) { Name = "Materials" });
            //gameData.Items.Add(new FileCategoryModel(scene, commandManager, menuService) { Name = "Sounds" });
            //gameData.Items.Add(new PhysicsObjectModel(scene, commandManager, menuService, 0) { Name = "PhysicObjects" });
            //m_Items.Add(gameData);


            Items.Add(new FileCategoryModel(parent, container) { Name = "Meshes" });
            Items.Add(new FileCategoryModel(parent, container) { Name = "Textures" });
            Items.Add(new FileCategoryModel(parent, container) { Name = "Sounds" });

            //-----------------------------------------
            //Customize Database category structure
            //-----------------------------------------
            //  GameDBFileModel dbData = new GameDBFileModel(this, container);
            //  dbData.IsExpanded = true;



            // m_Items.Add(dbData);



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
            //        this.Location = location;
            RaisePropertyChanged("Location");
        }

        internal void SetDirty(bool value)
        {
            // this.IsDirty = value;
        }

        [XmlIgnore]
        public string HTMLResult { get; set; }

        public void SetHtml(string transform)
        {
            this.HTMLResult = transform;
            RaisePropertyChanged("HTMLResult");
        }
    }


    public class CmdSaveVFSZip : ICommand
    {
        private VFS_ZipModel mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mpm.Save();
        }

        public CmdSaveVFSZip(VFS_ZipModel pm)
        {
            mpm = pm;
        }
    }
}