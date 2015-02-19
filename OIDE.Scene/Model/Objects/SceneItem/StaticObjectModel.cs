﻿#region License

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
using OIDE.VFS.VFS_Types.RootFileSystem;
using OIDE.Scene.Model.Objects;
using Module.Properties.Types;
using DAL;
using DAL.MDB;
using WIDE_Helpers;
using OIDE.Scene.Model.Objects.ObjectData;
using System.Windows;
using System.IO;

namespace OIDE.Scene.Model
{
    public class StaticObjectModel : EntityBaseModel, ISceneItem
    {
        private FB_StaticObjectModel m_FBData;

        public void Drop(IItem item) 
        { 
             if(item is FileItem)
             {
                 if (m_FBData == null )
                     this.Open(this.ContentID);

                 //todo
                 //ProtoType.Mesh mesh = new ProtoType.Mesh();
                 //mesh.Name = (item as FileItem).ContentID;
                 //mData.gameEntity.meshes.Add(mesh);
             }
        }

        public Int32 NodeID { get; set; }
        
        [XmlIgnore]
        [Browsable(false)]
        public IItem Parent { get; private set; }

        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }
        public String ContentID { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public DAL.MDB.SceneNode SceneNode { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public DAL.IDAL.EntityContainer DB_Entity { get; set; }
     
        //[Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        //[NewItemTypes(new Type[] { typeof(Mesh), typeof(Plane), typeof(Cube) })]
        //public List<Mesh> Meshes { get { return mMeshes; } set { mMeshes = value; } }


        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }

        private string m_Name;
        public String Name { get { return m_Name; } set { m_Name = value; RaisePropertyChanged("Name"); } }

        [Browsable(false)]
        public CollectionOfIItem Items { get; set; }


        [XmlIgnore]
        [Browsable(false)]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem miSave = new MenuItem() { Command = CmdSaveStaticObj, Header = "Save" };
                list.Add(miSave);
                MenuItem miDelete = new MenuItem() { Command = CmdDeleteStaticObj, Header = "Delete" };
                list.Add(miDelete);
                MenuItem miObjects = new MenuItem() { Header = "Add new object" };

                MenuItem miObj1 = new MenuItem() {  Header = "Add Plane" };
                MenuItem miObj2 = new MenuItem() {  Header = "Add Cube" };
                miObjects.Items.Add(miObj1);
                miObjects.Items.Add(miObj2);

                list.Add(miObjects);
                list.Add(miSave);

                return list;
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public IDAL IDAL { get { return m_dbI; } }

        private IDAL m_dbI;

        [Browsable(false)]
        public Boolean IsExpanded { get; set; }

        private Boolean m_opened;

        private Boolean m_IsSelected;
        [Browsable(false)]
        public Boolean IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                m_IsSelected = value;
                    
                Open(WIDE_Helper.StringToContentIDData(ContentID).IntValue);
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        public Boolean Open(object id)
        {
            if (m_opened)
                return true;

            //   DB_Entity = m_dbI.selectEntityData(WIDE_Helper.StringToContentIDData(ContentID).IntValue); // database data

            //read data from lokal json file
            m_FBData = Helper.Utilities.USystem.XMLSerializer.Deserialize<FB_StaticObjectModel>("Scene/Entities/" + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml"); //ProtoSerialize.Deserialize<ProtoType.Node>(node.Data);
            if (m_FBData == null)
                Create();
           

            base.SetFBData(m_FBData.EntityBaseModel); //set base entity data

            //test
         //   m_FBData.Read(DB_Entity.Entity.Data);


            return m_opened = true;
        }

        public void Refresh() { }
        public void Finish() { }

        public Boolean Save(object param)
        {
            try
            {
                DB_Entity.Entity.Data = m_FBData.CreateByteBuffer(base.m_BaseObj_FBData);
                DB_Entity.Entity.Name = Name;

                if (WIDE_Helper.StringToContentIDData(ContentID).IntValue > 0)
                    DB_Entity.Entity.EntID = WIDE_Helper.StringToContentIDData(ContentID).IntValue;

                //test
                m_FBData.Read(DB_Entity.Entity.Data);

                if (DB_Entity.Entity.EntID > 0)
                    m_dbI.updateEntity(DB_Entity.Entity);
                else
                {
                    DB_Entity.Entity.EntType = (decimal)EntityTypes.NT_Static;
                    m_dbI.insertEntity(DB_Entity.Entity);
                    ContentID = ContentID + ":"+ DB_Entity.Entity.EntID;
                }

                m_FBData.EntityBaseModel = base.m_BaseObj_FBData;
                Helper.Utilities.USystem.XMLSerializer.Serialize<FB_StaticObjectModel>(m_FBData, "Scene/Entities/" + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml");  // XML Serialize

            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
            return true;
        }

        private ICommand CmdSaveStaticObj;
        private ICommand CmdDeleteStaticObj;

        public Boolean Create()
        {
            m_FBData = new FB_StaticObjectModel();
            
            return true; 
        }
        public Boolean Delete() {

            try
            {
                m_dbI.deleteEntity(DB_Entity.Entity);
                Parent.Items.Remove(this);

                if (File.Exists("Scene/Entities/" + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml"))
                    File.Delete("Scene/Entities/" + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml");

                MessageBox.Show("Static entity deleted");
            }catch(Exception ex)
            {
                 MessageBox.Show("Error: static entity not deleted: " + ex.Message);
            }
            return true;
        }
        public Boolean Closing() { return true; }

        [XmlIgnore]
        [Browsable(false)]
        public IUnityContainer UnityContainer { get; private set; }

        /// <summary>
        /// Default contructor for serialization
        /// </summary>
        public StaticObjectModel()
            : base(null)
        {

        }

        public StaticObjectModel(IItem parent, IUnityContainer unityContainer, IDAL dbI = null, Int32 id = 0)
            : base(unityContainer)
        {     
            UnityContainer = unityContainer;

            //mMeshes = new List<string>();
            Parent = parent;
            SceneItems = new ObservableCollection<ISceneItem>();
            CmdSaveStaticObj = new CmdSaveStaticObject(this);
            CmdDeleteStaticObj = new CmdDeleteStaticObject(this);
          //  mtest = new Byte[10];
            Items = new CollectionOfIItem();

            if (dbI != null)
                m_dbI = dbI;
            else
                m_dbI = new IDAL();


            
            m_FBData = new FB_StaticObjectModel();
            base.m_BaseObj_FBData = new FB_EntityBaseModel();

            DB_Entity = new DAL.IDAL.EntityContainer();
            DB_Entity.Entity = new Entity();
            //mData = new ProtoType.StaticEntity();
            //mData.gameEntity = new ProtoType.GameEntity();
            /// ???????????????????????????
            SceneNode = new DAL.MDB.SceneNode();

        }
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