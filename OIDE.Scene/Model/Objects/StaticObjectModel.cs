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
using OIDE.VFS.VFS_Types.RootFileSystem;
using OIDE.Scene.Model.Objects;
using Module.Properties.Types;
using DAL;
using DAL.MDB;
using WIDE_Helpers;
using OIDE.Scene.Model.Objects.ObjectData;

namespace OIDE.Scene.Model
{
    public class StaticObjectModel : EntityBaseModel, ISceneItem, IGameEntity
    {
        private FB_StaticObjectModel m_FBData;

        public void Drop(IItem item) 
        { 
             if(item is FileItem)
             {
                 if (m_FBData == null || m_FBData.EntityBaseModel == null)
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
        public ISceneNode Node { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public DAL.MDB.SceneNode SceneNode { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public DAL.IDAL.EntityContainer DB_Entity { get; set; }

     //   private Entity mDBData;

        //[XmlIgnore]
        //[Browsable(false)]
        //public object DB_S_ObjectData
        //{
        //    get   {  return mDBData;  }
        //    set
        //    {
        //        mDBData = value as Entity;

        //        //GameEntity dbData = value as GameEntity;
        //        //ProtoType.StaticEntity dataStaticObj = new ProtoType.StaticEntity();

        //        //if (dbData.Data != null)
        //        //{
        //        //    mData = ProtoSerialize.Deserialize<ProtoType.StaticEntity>(dbData.Data);
 
        //        //    if (mData.gameEntity == null)
        //        //       mData.gameEntity = new ProtoType.GameEntity();

        //        //    foreach (var item in mData.gameEntity.physics)
        //        //        m_Physics.Add(new PhysicObject() { ProtoData = item });

        //        //    foreach (var item in mData.gameEntity.materials)
        //        //        m_Materials.Add(new Material() { ProtoData = item });


        //        //    foreach (var item in mData.gameEntity.meshes)
        //        //    {
        //        //        if (item.cube != null)
        //        //            mMeshes.Add(new Cube() { ProtoData = item });
        //        //        else if (item.plane != null)
        //        //            mMeshes.Add(new Plane() { ProtoData = item });
        //        //        else
        //        //            mMeshes.Add(new Mesh() { ProtoData = item });
        //        //    }
        //        //}

        //    }
        //}

      //  private List<String> mMeshes;

        //private List<Mesh> mMeshes;
     
        //[Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        //[NewItemTypes(new Type[] { typeof(Mesh), typeof(Plane), typeof(Cube) })]
        //public List<Mesh> Meshes { get { return mMeshes; } set { mMeshes = value; } }

     //   public List<ProtoType.Mesh> Meshes { get { return mData.gameEntity.meshes; } }








    //    [XmlIgnore]
   //     public ProtoType.OgreSysType OgreSystemType { get { return mData.gameEntity.ogreSysType; } set { mData.gameEntity.ogreSysType = value; } }

        //[XmlIgnore]
        //[Category("Conections")]
        //[Description("This property is a complex property and has no default editor.")]
      //  [ExpandableObject]
        //[Browsable(false)]
        //public ProtoType.StaticEntity ProtoData { get { return mData; } }


        //[XmlIgnore]
        //public Int32 StaticGroup { get { return mData.group; } set { mData.group = value; } }

        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }

        public String Name { get; set; }

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
            m_FBData = DAL.Utility.JSONSerializer.Deserialize<FB_StaticObjectModel>("Scene/Entities/" + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".json"); //ProtoSerialize.Deserialize<ProtoType.Node>(node.Data);
            if (m_FBData == null)
                Create();

            return m_opened = true;
        }

        public void Refresh() { }
        public void Finish() { }

        public Boolean Save(object param)
        {
            try
            {
              //  DAL.MDB.Entity gameEntity = DB_S_ObjectData as DAL.MDB.Entity;
                
                //Update Phyiscs Data
                //ProtoData.gameEntity.physics.Clear();
                //foreach(var item in m_Physics)
                //    ProtoData.gameEntity.physics.Add(item.ProtoData);

                ////Update mesh Data
                //ProtoData.gameEntity.meshes.Clear();
                //foreach (var item in mMeshes)
                //    ProtoData.gameEntity.meshes.Add(item.ProtoData);


                //ProtoData.gameEntity.materials.Clear();
                //foreach (var item in m_Materials)
                //    ProtoData.gameEntity.materials.Add(item.ProtoData);


                DB_Entity.Entity.Data = m_FBData.CreateByteBuffer();
                DB_Entity.Entity.EntID = WIDE_Helper.StringToContentIDData(ContentID).IntValue;


                if (DB_Entity.Entity.EntID > 0)
                    m_dbI.updateEntity(DB_Entity.Entity);
                else
                {
                    DB_Entity.Entity.EntType = (decimal)EntityTypes.NT_Static;
                    m_dbI.insertEntity(DB_Entity.Entity);
                }

                //if (DLL_Singleton.Instance.EditorInitialized)
                //    DLL_Singleton.Instance.command("cmd physic " + gameEntity.EntID, gameEntity.Data, gameEntity.Data.Length); //.updateObject(0, (int)ObjType.Physic);

                DAL.Utility.JSONSerializer.Serialize<FB_StaticObjectModel>(m_FBData, "Scene/Entities/" + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".json");  // XML Serialize

            }
            catch (Exception ex)
            {
                //     MessageBox.Show("dreck_" + id + "_!!!!");
            }
            return true;
        }

        private ICommand CmdSaveStaticObj;

        public Boolean Create()
        {
            m_FBData = new FB_StaticObjectModel();
            
            return true; 
        }
        public Boolean Delete() { return true; }
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
            //  mtest = new Byte[10];
            Items = new CollectionOfIItem();

            if (dbI != null)
                m_dbI = dbI;
            else
                m_dbI = new IDAL();


            
            m_FBData = new FB_StaticObjectModel();
            base.m_BaseObj_FBData = m_FBData;

            DB_Entity = new DAL.IDAL.EntityContainer();
            DB_Entity.Entity = new Entity();
            //mData = new ProtoType.StaticEntity();
            //mData.gameEntity = new ProtoType.GameEntity();
            /// ???????????????????????????
            SceneNode = new DAL.MDB.SceneNode();

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
