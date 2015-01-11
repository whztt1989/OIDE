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

namespace OIDE.Scene.Model
{
    public class Plane : Mesh
    {
        //[Editor(typeof(Vector3Editor), typeof(Vector3Editor))]
        //public ProtoType.Vec3f normal { get { return ProtoData.plane.normal; } set { ProtoData.plane.normal = value; } }
       
        //public float constant { get { return ProtoData.plane.constant; } set { ProtoData.plane.constant = value; } }
        //public float width { get { return ProtoData.plane.width; } set { ProtoData.plane.width = value; } }
        //public float height { get { return ProtoData.plane.height; } set { ProtoData.plane.height = value; } }
        //public Int32 xsegments { get { return ProtoData.plane.xsegments; } set { ProtoData.plane.xsegments = value; } }
        //public Int32 ysegments { get { return ProtoData.plane.ysegments; } set { ProtoData.plane.ysegments = value; } }
        //public bool normals { get { return ProtoData.plane.normals; } set { ProtoData.plane.normals = value; } }
        //public Int32 numTexCoordSets { get { return ProtoData.plane.numTexCoordSets; } set { ProtoData.plane.numTexCoordSets = value; } }
        //public float xTile { get { return ProtoData.plane.xTile; } set { ProtoData.plane.xTile = value; } }
        //public float yTile { get { return ProtoData.plane.yTile; } set { ProtoData.plane.yTile = value; } }

        //[Editor(typeof(Vector3Editor), typeof(Vector3Editor))]
        //public ProtoType.Vec3f upVector { get { return ProtoData.plane.upVector; } set { ProtoData.plane.upVector = value; } }



//new Vector3(ProtoData.plane.normal.x, ProtoData.plane.normal.y, ProtoData.plane.normal.z)
        public Plane()
        {
            //ProtoData.plane = new ProtoType.OgrePlane();
            //ProtoData.plane.upVector = new ProtoType.Vec3f();
            //ProtoData.plane.normal = new ProtoType.Vec3f();
        }
    }

    public class Cube : Mesh
    {
        //public float width { get { return ProtoData.cube.width; } set { ProtoData.cube.width = value; } }

        public Cube()
        {
            //ProtoData.cube = new ProtoType.OgreCube();
        }
    }

    public class Mesh
    {
        //public String RessGrp { get { return ProtoData.RessGrp; } set { ProtoData.RessGrp = value; } }
        //public String Name { get { return ProtoData.Name; } set { ProtoData.Name = value; } }
        
        //[XmlIgnore]
        //[Browsable(false)]
        //public ProtoType.Mesh ProtoData { get; set; }

        public Mesh()
        {
            //ProtoData = new ProtoType.Mesh();
        }
    }

    public class Material
    {
        //public String RessGrp { get { return ProtoData.RessGrp; } set { ProtoData.RessGrp = value; } }
        //public String Name { get { return ProtoData.Name; } set { ProtoData.Name = value; } }

        //[XmlIgnore]
        //[Browsable(false)]
        //public ProtoType.Material ProtoData { get; set; }

        public Material()
        {
            //ProtoData = new ProtoType.Material();
        }
    }

    public class StaticObjectModel : ISceneItem, IGameEntity
    {
        //private ProtoType.StaticEntity mData;

        public void Drop(IItem item) 
        { 
             if(item is FileItem)
             {
                 //if (mData.gameEntity == null)
                 //    mData.gameEntity = new ProtoType.GameEntity();

                 //ProtoType.Mesh mesh = new ProtoType.Mesh();
                 //mesh.Name = (item as FileItem).ContentID;
                 //mData.gameEntity.meshes.Add(mesh);
             }
        }

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
        public DAL.MDB.SceneNodes SceneNode { get; private set; }

        private GameEntity mDBData;

        [XmlIgnore]
        [Browsable(false)]
        public object DBData
        {
            get   {  return mDBData;  }
            set
            {
                mDBData = value as GameEntity;
             
               
                //GameEntity dbData = value as GameEntity;
                //ProtoType.StaticEntity dataStaticObj = new ProtoType.StaticEntity();

                //if (dbData.Data != null)
                //{
                //    mData = ProtoSerialize.Deserialize<ProtoType.StaticEntity>(dbData.Data);
 
                //    if (mData.gameEntity == null)
                //       mData.gameEntity = new ProtoType.GameEntity();

                //    foreach (var item in mData.gameEntity.physics)
                //        m_Physics.Add(new PhysicObject() { ProtoData = item });

                //    foreach (var item in mData.gameEntity.materials)
                //        m_Materials.Add(new Material() { ProtoData = item });


                //    foreach (var item in mData.gameEntity.meshes)
                //    {
                //        if (item.cube != null)
                //            mMeshes.Add(new Cube() { ProtoData = item });
                //        else if (item.plane != null)
                //            mMeshes.Add(new Plane() { ProtoData = item });
                //        else
                //            mMeshes.Add(new Mesh() { ProtoData = item });
                //    }
                //}

            }
        }

      //  private List<String> mMeshes;

        private List<Mesh> mMeshes;
     
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        [NewItemTypes(new Type[] { typeof(Mesh), typeof(Plane), typeof(Cube) })]
        public List<Mesh> Meshes { get { return mMeshes; } set { mMeshes = value; } }

     //   public List<ProtoType.Mesh> Meshes { get { return mData.gameEntity.meshes; } }


        private List<Material> m_Materials;

        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
     //   [NewItemTypes(new Type[] { typeof(Mesh), typeof(Plane), typeof(Cube) })]
        public List<Material> Materials { get { return m_Materials; } set { m_Materials = value; } }



        private List<PhysicObject> m_Physics;
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        public List<PhysicObject> Physics { get { return m_Physics; } set { m_Physics = value; } }


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

        [Browsable(false)]
        public Boolean IsSelected { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        public Boolean Open(object id)
        {

            DBData = m_dbI.selectGameEntity(Helper.StringToContentIDData(ContentID).IntValue);
            // Console.WriteLine(BitConverter.ToString(res));
            //try
            //{
            //    mData = ProtoSerialize.Deserialize<ProtoType.StaticEntity>((DBData as DAL.MDB.GameEntity).Data);
            //}
            //catch
            //{
            //    mData = new ProtoType.StaticEntity();
            //}
            return true; 
        }

        public void Refresh() { }
        public void Finish() { }

        public Boolean Save(object param)
        {
            try
            {
                DAL.MDB.GameEntity gameEntity = DBData as DAL.MDB.GameEntity;

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

                //gameEntity.Data = ProtoSerialize.Serialize(ProtoData);
                //gameEntity.Name = this.Name;

                //if (gameEntity.EntID > 0)
                //    m_dbI.updateGameEntity(gameEntity);
                //else
                //{
                //    gameEntity.EntType = (decimal)ProtoType.EntityTypes.NT_Static;
                //    m_dbI.insertGameEntity(gameEntity);
                //}

                //if (DLL_Singleton.Instance.EditorInitialized)
                //    DLL_Singleton.Instance.command("cmd physic " + gameEntity.EntID, gameEntity.Data, gameEntity.Data.Length); //.updateObject(0, (int)ObjType.Physic);

            }
            catch (Exception ex)
            {
                //     MessageBox.Show("dreck_" + id + "_!!!!");
            }
            return true;
        }

        private ICommand CmdSaveStaticObj;

        public Boolean Create() { return true; }
        public Boolean Delete() { return true; }
        public Boolean Closing() { return true; }

        [XmlIgnore]
        [Browsable(false)]
        public IUnityContainer UnityContainer { get; private set; }

        /// <summary>
        /// Default contructor for serialization
        /// </summary>
        public StaticObjectModel()
        {

        }

        public StaticObjectModel(IItem parent, IUnityContainer unityContainer, IDAL dbI = null, Int32 id = 0)
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

            m_Materials = new List<Material>();
            mMeshes = new List<Mesh>();
            m_Physics = new List<PhysicObject>();
            //mData = new ProtoType.StaticEntity();
            //mData.gameEntity = new ProtoType.GameEntity();
            /// ???????????????????????????
            SceneNode = new DAL.MDB.SceneNodes();

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
