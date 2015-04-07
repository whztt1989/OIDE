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
    
    
using OIDE.Scene.Interface.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Module.Properties.Interface;
using Wide.Interfaces.Services;
using Microsoft.Practices.Unity;
using Module.Protob.Utilities;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Windows.Input;
using OIDE.InteropEditor.DLL;
using System.Xml.Serialization;
using Module.Properties.Helpers;
using OIDE.Scene.Model.Objects;
using Module.Properties.Types;
using DAL;
using DAL.MDB;
using WIDE_Helpers;
using OIDE.Scene.Model.Objects.ObjectData;
using System.Windows;
using System.IO;
using OIDE.Scene.Interface;
using Wide.Core.Services;
using Module.PFExplorer.Interface;
using Module.PFExplorer.Utilities;
using Module.PFExplorer.Service;
using OIDE.Scene.Service;
using Module.DB.Interface.Services;

namespace OIDE.Scene.Model
{
    public class StaticObjectModel : SceneItem, IDBFileItem
    {
        #region serializable data

        private FB_StaticObjectModel m_FBData;
      
        [XmlIgnore]
        [ExpandableObject]
        public FB_StaticObjectModel FB_StaticObject { get { return m_FBData; } }

        #endregion


        public void Drop(IItem item) 
        { 
             //if(item is FileItem)
             //{
             //    if (m_FBData == null )
             //        this.Open(item.UnityContainer, this.ContentID);

             //    //todo
             //    //ProtoType.Mesh mesh = new ProtoType.Mesh();
             //    //mesh.Name = (item as FileItem).ContentID;
             //    //mData.gameEntity.meshes.Add(mesh);
             //}
        }

        [XmlIgnore]
        [Browsable(false)]
        public DAL.IDAL.EntityContainer DB_Entity { get; set; }
     
        //[Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        //[NewItemTypes(new Type[] { typeof(Mesh), typeof(Plane), typeof(Cube) })]
        //public List<Mesh> Meshes { get { return mMeshes; } set { mMeshes = value; } }

        [XmlIgnore]
        [Browsable(false)]
        public override List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem miSave = new MenuItem() { Command = CmdSaveStaticObj, Header = "Save" };
                list.Add(miSave);
                MenuItem miDelete = new MenuItem() { Command = CmdDeleteStaticObj, Header = "Delete" };
                list.Add(miDelete);
             
                list.Add(miSave);

                return list;
            }
        }

      
        private Boolean m_opened;

        public override Boolean Open(IUnityContainer unityContainer, object id)
        {
            if (m_opened)
                return true;

//            m_FBData = new FB_StaticObjectModel() { UnityContainer = unityContainer , Parent = this};

            UnityContainer = unityContainer;
            base.m_DBService = unityContainer.Resolve<IDatabaseService>();
            DataContext.Context = ((IDAL)m_DBService.CurrentDB).GetDataContextOpt(false);
       
        
            //   DB_Entity = m_dbI.selectEntityData(WIDE_Helper.StringToContentIDData(ContentID).IntValue); // database data

            //read data from lokal json file
                m_FBData = Helper.Utilities.USystem.XMLSerializer.Deserialize<FB_StaticObjectModel>(ItemFolder + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml"); //ProtoSerialize.Deserialize<ProtoType.Node>(node.Data);
            if (m_FBData == null)
                Create(unityContainer);
            else
                RaisePropertyChanged("FB_StaticObject");

           // m_FBData.SetFBData(m_FBData.EntityBaseModel); //set base entity data

            //test
         //   m_FBData.Read(DB_Entity.Entity.Data);


            return m_opened = true;
        }

        public void Refresh() { }
        public void Finish() { }

        public Boolean SaveToDB()
        {
            String DBPath = DBFileUtil.GetDBFilePath(this.Parent);
            if (!String.IsNullOrEmpty(DBPath))
            {
                DB_Entity.Entity.Data = m_FBData.CreateByteBuffer();
                DB_Entity.Entity.Name = Name;

               // if (WIDE_Helper.StringToContentIDData(ContentID).IntValue > 0)
                DB_Entity.Entity.EntID = WIDE_Helper.StringToContentIDData(ContentID).IntValue;

                //test
                m_FBData.Read(DB_Entity.Entity.Data);

                //if (DB_Entity.Entity.EntID > 0)
                //    m_dbI.updateEntity(DB_Entity.Entity);
                //else
                //{
                    DB_Entity.Entity.EntType = (decimal)EntityTypes.NT_Static;
                    IDAL.insertEntity(DataContext, DB_Entity.Entity);
             //       ContentID = ContentID + ":" + DB_Entity.Entity.EntID;
             //   }
            }
            return true;
        }

        public override Boolean Save(object param)
        {
            try
            {
                SaveToDB();
               // m_FBData.EntityBaseModel = m_FBData.EntityBaseModel;
                Helper.Utilities.USystem.XMLSerializer.Serialize<FB_StaticObjectModel>(m_FBData, ItemFolder + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml");  // XML Serialize

            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
            return true;
        }

        private ICommand CmdSaveStaticObj;
        private ICommand CmdDeleteStaticObj;

        public override Boolean Create(IUnityContainer unityContainer)
        {
            m_FBData = new FB_StaticObjectModel() { UnityContainer = unityContainer, Parent = this };

            RaisePropertyChanged("FB_StaticObject");

            UnityContainer = unityContainer;
            base.m_DBService = unityContainer.Resolve<IDatabaseService>();
            DataContext.Context = ((IDAL)m_DBService.CurrentDB).GetDataContextOpt(false);
       
            return true; 
        }
        public override Boolean Delete()
        {

            try
            {
                IDAL.deleteEntity(DataContext, DB_Entity.Entity);
                Parent.Items.Remove(this);

                if (File.Exists(ItemFolder + "\\" + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml"))
                    File.Delete(ItemFolder + "\\" + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml");

                MessageBox.Show("Static entity deleted");
            }catch(Exception ex)
            {
                 MessageBox.Show("Error: static entity not deleted: " + ex.Message);
            }
            return true;
        }
        public Boolean Closing() { return true; }

        /// <summary>
        /// Default contructor for serialization
        /// </summary>
        public StaticObjectModel()
       //     : base(null)
        {
          
            //mMeshes = new List<string>();
          //  Parent = parent;
            CmdSaveStaticObj = new CmdSaveStaticObject(this);
            CmdDeleteStaticObj = new CmdDeleteStaticObject(this);
            //  mtest = new Byte[10];


            Name = "New Static Obj";
            

          //  m_FBData.m_BaseObj_FBData = new FB_EntityBaseModel();

            DB_Entity = new DAL.IDAL.EntityContainer();
            DB_Entity.Entity = new Entity();
            //mData = new ProtoType.StaticEntity();
            //mData.gameEntity = new ProtoType.GameEntity();
            /// ???????????????????????????
        }

        //public StaticObjectModel(IItem parent, IUnityContainer unityContainer, IDAL dbI = null, Int32 id = 0)
        //    : base(unityContainer)
        //{     
        //    UnityContainer = unityContainer;

        //    //mMeshes = new List<string>();
        //    Parent = parent;
        //    SceneItems = new CollectionOfISceneItem();
        //    CmdSaveStaticObj = new CmdSaveStaticObject(this);
        //    CmdDeleteStaticObj = new CmdDeleteStaticObject(this);
        //  //  mtest = new Byte[10];
        //    Items = new CollectionOfIItem();

        //    if (dbI != null)
        //        m_dbI = dbI;
        //    else
        //        m_dbI = new IDAL(unityContainer);


            
        //    m_FBData = new FB_StaticObjectModel();
        //    base.m_BaseObj_FBData = new FB_EntityBaseModel();

        //    DB_Entity = new DAL.IDAL.EntityContainer();
        //    DB_Entity.Entity = new Entity();
        //    //mData = new ProtoType.StaticEntity();
        //    //mData.gameEntity = new ProtoType.GameEntity();
        //    /// ???????????????????????????
        //    SceneNode = new DAL.MDB.SceneNode();

        //}
    }

  public class CmdDeleteStaticObject : ICommand
    {
        private StaticObjectModel m_StaticObjectModel;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_StaticObjectModel.Delete();
        }

        public CmdDeleteStaticObject(StaticObjectModel som)
        {
            m_StaticObjectModel = som;
        }
    }
    public class CmdSaveStaticObject : ICommand
    {
        private StaticObjectModel m_StaticObjectModel;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_StaticObjectModel.Save(parameter);
        }

        public CmdSaveStaticObject(StaticObjectModel som)
        {
            m_StaticObjectModel = som;
        }
    }
}
