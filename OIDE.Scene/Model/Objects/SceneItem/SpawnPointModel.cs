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
using DAL;
using System.Windows.Input;
using OIDE.InteropEditor.DLL;
using System.Xml.Serialization;
using Module.Properties.Helpers;
using OIDE.Scene.Model.Objects;
using Module.Properties.Types;
using WIDE_Helpers;
using OIDE.Scene.Interface;
using Wide.Core.Services;

namespace OIDE.Scene.Model
{
    public class SpawnPointModel : PItem, ISceneItem
    {
        //private ProtoType.SpawnPoint mData;

        public void Drop(IItem item) 
        { 
             //if(item is FileItem)
             //{
             //  //  if (mData.gameEntity == null)
             //  //      mData.gameEntity = new ProtoType.GameEntity();

             //  //  ProtoType.Mesh mesh = new ProtoType.Mesh();
             //  //  mesh.Name = (item as FileItem).ContentID;
             //  //  mData.gameEntity.meshes.Add(mesh);
             //}
        }


        [XmlIgnore]
        [Browsable(false)]
        public IItem Parent { get; set; }

        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }
        public String ContentID { get; set; }

        public Int32 NodeID { get; set; }
        
        [XmlIgnore]
        [Browsable(false)]
        public DAL.MDB.SceneNode SceneNode { get; private set; }

        private FB_SpawnPointModel mDBData;

        [XmlIgnore]
        [Browsable(false)]
        public object DBData
        {
            get   {  return mDBData;  }
            set
            {
                mDBData = value as FB_SpawnPointModel;
             
               
                //GameEntity dbData = value as GameEntity;
                //ProtoType.SpawnPoint dataSpawnPoint = new ProtoType.SpawnPoint();

                //if (dbData.Data != null)
                //{
                //    mData = ProtoSerialize.Deserialize<ProtoType.SpawnPoint>(dbData.Data);
 
                // //   if (mData.gameEntity == null)
                // //      mData.gameEntity = new ProtoType.GameEntity();

                //    //foreach (var item in mData.gameEntity.physics)
                //    //    m_Physics.Add(new PhysicObject() { ProtoData = item });

                //    //foreach (var item in mData.gameEntity.materials)
                //    //    m_Materials.Add(new Material() { ProtoData = item });


                //    //foreach (var item in mData.gameEntity.meshes)
                //    //{
                //    //    if (item.cube != null)
                //    //        mMeshes.Add(new Cube() { ProtoData = item });
                //    //    else if (item.plane != null)
                //    //        mMeshes.Add(new Plane() { ProtoData = item });
                //    //    else
                //    //        mMeshes.Add(new Mesh() { ProtoData = item });
                //    //}
                //}

            }
        }

      //  private List<String> mMeshes;

        //private List<Mesh> mMeshes;
     
        //[Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        //[NewItemTypes(new Type[] { typeof(Mesh), typeof(Plane), typeof(Cube) })]
        //public List<Mesh> Meshes { get { return mMeshes; } set { mMeshes = value; } }

     //   public List<ProtoType.Mesh> Meshes { get { return mData.gameEntity.meshes; } }


     //   private List<Material> m_Materials;

     //   [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
     ////   [NewItemTypes(new Type[] { typeof(Mesh), typeof(Plane), typeof(Cube) })]
     //   public List<Material> Materials { get { return m_Materials; } set { m_Materials = value; } }



        //private List<PhysicObject> m_Physics;
        //[Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        //public List<PhysicObject> Physics { get { return m_Physics; } set { m_Physics = value; } }


    //    [XmlIgnore]
   //     public ProtoType.OgreSysType OgreSystemType { get { return mData.gameEntity.ogreSysType; } set { mData.gameEntity.ogreSysType = value; } }

        //[XmlIgnore]
        //[Category("Conections")]
        //[Description("This property is a complex property and has no default editor.")]
      //  [ExpandableObject]
        //[Browsable(false)]
        //public ProtoType.SpawnPoint ProtoData { get { return mData; } }


        //[XmlIgnore]
        //public Int32 SpawnPointGroup { get { return mData.SPGroup; } set { mData.SPGroup = value; } }

        [XmlIgnore]
        [Browsable(false)]
        public CollectionOfISceneItem SceneItems { get; private set; }

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
                MenuItem miSave = new MenuItem() { Command = CmdSaveSpawnPointObj, Header = "Save" };
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

        public Boolean Open(IUnityContainer unityContainer, object id)
        {

            DBData = m_dbI.selectEntity(WIDE_Helper.StringToContentIDData(ContentID).IntValue);
            // Console.WriteLine(BitConverter.ToString(res));
            //try
            //{
            //    mData = ProtoSerialize.Deserialize<ProtoType.SpawnPoint>((DBData as DAL.MDB.GameEntity).Data);
            //}
            //catch
            //{
            //    mData = new ProtoType.SpawnPoint();
            //}
            return true; 
        }

        public void Refresh() { }
        public void Finish() { }

        public Boolean Save(object param)
        {
            try
            {
                DAL.MDB.Entity gameEntity = DBData as DAL.MDB.Entity;
                //todo   if (gameEntity == null)
                //todo    gameEntity = new FB_SpawnPointModel();
         
                
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
                //    gameEntity.EntType = (decimal)ProtoType.EntityTypes.NT_SpawnPoint;
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

        private ICommand CmdSaveSpawnPointObj;

        public Boolean Create(IUnityContainer unityContainer) { return true; }
        public Boolean Delete() { return true; }
        public Boolean Closing() { return true; }

        [XmlIgnore]
        [Browsable(false)]
        public IUnityContainer UnityContainer { get; set; }

        /// <summary>
        /// Default contructor for serialization
        /// </summary>
        public SpawnPointModel()
        {

        }

        public SpawnPointModel(IItem parent, IUnityContainer unityContainer, IDAL dbI = null, Int32 id = 0)
        {     
            UnityContainer = unityContainer;

            //mMeshes = new List<string>();
            Parent = parent;
            SceneItems = new CollectionOfISceneItem();
            CmdSaveSpawnPointObj = new CmdSaveSpawnPoint(this);
            //  mtest = new Byte[10];
            Items = new CollectionOfIItem();

            if (dbI != null)
                m_dbI = dbI;
            else
                m_dbI = new IDAL(unityContainer);

          
            //mData = new ProtoType.SpawnPoint();
          //  mData.gameEntity = new ProtoType.GameEntity();

        }


    }


    public class CmdSaveSpawnPoint : ICommand
    {
        private SpawnPointModel m_SpawnPointObjectModel;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_SpawnPointObjectModel.Save(parameter);
        }

        public CmdSaveSpawnPoint(SpawnPointModel som)
        {
            m_SpawnPointObjectModel = som;
        }
    }
}
