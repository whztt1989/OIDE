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
using OIDE.DAL;
using System.Windows.Input;
using OIDE.InteropEditor.DLL;
using System.Xml.Serialization;
using Module.Properties.Helpers;
using OIDE.DAL.MDB;
using OIDE.VFS.VFS_Types.RootFileSystem;
using OIDE.Scene.Model.Objects;

namespace OIDE.Scene.Model
{
    public class Plane : Mesh
    {
        public ProtoType.Vec3f normal { get { return ProtoData.plane.normal; } set { ProtoData.plane.normal = value; } }
        public float constant { get { return ProtoData.plane.constant; } set { ProtoData.plane.constant = value; } }
        public float width { get { return ProtoData.plane.width; } set { ProtoData.plane.width = value; } }
        public float height { get { return ProtoData.plane.height; } set { ProtoData.plane.height = value; } }
        public Int32 xsegments { get { return ProtoData.plane.xsegments; } set { ProtoData.plane.xsegments = value; } }
        public Int32 ysegments { get { return ProtoData.plane.ysegments; } set { ProtoData.plane.ysegments = value; } }
        public bool normals { get { return ProtoData.plane.normals; } set { ProtoData.plane.normals = value; } }
        public Int32 numTexCoordSets { get { return ProtoData.plane.numTexCoordSets; } set { ProtoData.plane.numTexCoordSets = value; } }
        public float xTile { get { return ProtoData.plane.xTile; } set { ProtoData.plane.xTile = value; } }
        public float yTile { get { return ProtoData.plane.yTile; } set { ProtoData.plane.yTile = value; } }
      
        public ProtoType.Vec3f upVector { get { return ProtoData.plane.normal; } set { ProtoData.plane.normal = value; } }

        public Plane()
        {
            ProtoData.plane = new ProtoType.OgrePlane();
            ProtoData.plane.upVector = new ProtoType.Vec3f();
            ProtoData.plane.normal = new ProtoType.Vec3f();
        }
    }

    public class Cube : Mesh
    {
        public float width { get { return ProtoData.cube.width; } set { ProtoData.cube.width = value; } }

        public Cube()
        {
            ProtoData.cube = new ProtoType.OgreCube();
        }
    }

    public class Mesh
    {
        public String RessGrp { get { return ProtoData.RessGrp; } set { ProtoData.RessGrp = value; } }
        public String Name { get { return ProtoData.Name; } set { ProtoData.Name = value; } }
        
        [XmlIgnore]
        [Browsable(false)]
        public ProtoType.Mesh ProtoData { get; set; }

        public Mesh()
        {
            ProtoData = new ProtoType.Mesh();
        }
    }


    public class StaticObjectModel : ISceneItem, IGameEntity
    {
        private ProtoType.StaticEntity mData;

        public void Drop(IItem item) 
        { 
             if(item is FileItem)
             {
                 if (mData.gameEntity == null)
                     mData.gameEntity = new ProtoType.GameEntity();

                 ProtoType.Mesh mesh = new ProtoType.Mesh();
                 mesh.Name = (item as FileItem).Path;
                 mData.gameEntity.meshes.Add(mesh);
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
        public ProtoType.Node Node { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public OIDE.DAL.MDB.SceneNodes SceneNode { get; private set; }

        private GameEntity mDBData;

        [XmlIgnore]
        [Browsable(false)]
        public object DBData
        {
            get   {  return mDBData;  }
            set
            {
                mDBData = value as GameEntity;
             
               
                GameEntity dbData = value as GameEntity;
                ProtoType.StaticEntity dataStaticObj = new ProtoType.StaticEntity();

                if (dbData.Data != null)
                {
                    mData = ProtoSerialize.Deserialize<ProtoType.StaticEntity>(dbData.Data);
 
                    if (mData.gameEntity == null)
                       mData.gameEntity = new ProtoType.GameEntity();

                    foreach (var item in mData.gameEntity.physics)
                        m_Physics.Add(new PhysicObject() { ProtoData = item });

                    foreach (var item in mData.gameEntity.meshes)
                    {
                        if (item.cube != null)
                            mMeshes.Add(new Cube() { ProtoData = item });
                        else if (item.plane != null)
                            mMeshes.Add(new Plane() { ProtoData = item });
                        else
                            mMeshes.Add(new Mesh() { ProtoData = item });
                    }
                }

            }
        }

      //  private List<String> mMeshes;

        private List<Mesh> mMeshes;
     
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        [NewItemTypes(new Type[] { typeof(Mesh), typeof(Plane), typeof(Cube) })]
        public List<Mesh> Meshes { get { return mMeshes; } set { mMeshes = value; } }

     //   public List<ProtoType.Mesh> Meshes { get { return mData.gameEntity.meshes; } }



        private List<PhysicObject> m_Physics;
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        public List<PhysicObject> Physics { get { return m_Physics; } set { m_Physics = value; } }


    //    [XmlIgnore]
   //     public ProtoType.OgreSysType OgreSystemType { get { return mData.gameEntity.ogreSysType; } set { mData.gameEntity.ogreSysType = value; } }

        [XmlIgnore]
        //[Category("Conections")]
        //[Description("This property is a complex property and has no default editor.")]
      //  [ExpandableObject]
        [Browsable(false)]
        public ProtoType.StaticEntity ProtoData { get { return mData; } }


        [XmlIgnore]
        public Int32 StaticGroup { get { return mData.group; } set { mData.group = value; } }

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
            try
            {
                mData = ProtoSerialize.Deserialize<ProtoType.StaticEntity>((DBData as OIDE.DAL.MDB.GameEntity).Data);
            }
            catch
            {
                mData = new ProtoType.StaticEntity();
            }
            return true; 
        }

        public Boolean Save()
        {
            try
            {
                OIDE.DAL.MDB.GameEntity gameEntity = DBData as OIDE.DAL.MDB.GameEntity;

                //Update Phyiscs Data
                ProtoData.gameEntity.physics.Clear();
                foreach(var item in m_Physics)
                    ProtoData.gameEntity.physics.Add(item.ProtoData);

                //Update mesh Data
                ProtoData.gameEntity.meshes.Clear();
                foreach (var item in mMeshes)
                    ProtoData.gameEntity.meshes.Add(item.ProtoData);


                gameEntity.Data = ProtoSerialize.Serialize(ProtoData);
                gameEntity.Name = this.Name;

                if (gameEntity.EntID > 0)
                    m_dbI.updateGameEntity(gameEntity);
                else
                {
                    gameEntity.EntType = (decimal)ProtoType.EntityTypes.NT_Static;
                    m_dbI.insertGameEntity(gameEntity);
                }

                if (DLL_Singleton.Instance.EditorInitialized)
                    DLL_Singleton.Instance.consoleCmd("cmd physic " + gameEntity.EntID); //.updateObject(0, (int)ObjType.Physic);

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

            mMeshes = new List<Mesh>();
            m_Physics = new List<PhysicObject>();
            mData = new ProtoType.StaticEntity();
            mData.gameEntity = new ProtoType.GameEntity();
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
            m_StaticObjectModel.Save();
        }

        public CmdSaveStaticObject(StaticObjectModel som)
        {
            m_StaticObjectModel = som;
        }
    }
}
