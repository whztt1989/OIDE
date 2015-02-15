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
using System.Windows.Controls;
using OIDE.Scene.Interface;
using Module.Properties.Interface;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using OIDE.Scene.Interface.Services;
using Microsoft.Practices.Unity;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Xml.Serialization;
using OIDE.VFS.VFS_Types.RootFileSystem;
using Module.Protob.Utilities;
using OIDE.Scene.Model.Objects;
using System.Windows.Input;
using OIDE.InteropEditor.DLL;
using DAL;
using Module.Properties.Helpers;
using Module.Properties.Types;
using OIDE.VFS.View;
using DAL.MDB;
using WIDE_Helpers;
using OIDE.Scene.Model.Objects.ObjectData;

namespace OIDE.Scene.Model
{
    //public class RaceGenderItemsSource : IItemsSource
    //{
    //    public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
    //    {
    //        Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection raceGenders = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
          
    //        raceGenders.Add(0, "Human NEU");
    //        raceGenders.Add(1, "Human WEU");
    //        return raceGenders;
    //    }
    //}

    public class CharacterObjModel : EntityBaseModel, ISceneItem
    {
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }
        public Int32 NodeID { get; set; }
      
        #region protodata
    
        //    [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.ComboBoxEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.ComboBoxEditor))]
        //    [NewItemTypes(new Type[] { typeof(RaceGenderViewModel) })]
      //   [ItemsSource(typeof(RaceGenderItemsSource))]
        [Editor(typeof(ObjectComboBoxEditor), typeof(ObjectComboBoxEditor))]  
      
      //  public ProtoType.AI AI { get { return mData.ai; } }

        #endregion

      //  protected ProtoType.CharEntity mData;

        public void Drop(IItem item)
        {
            //if (item is CharacterCustomizeModel)
            //{

            //}

            if (item is FileItem)
            {
             //   if (mData.gameEntity == null)
             //       mData.gameEntity = new ProtoType.GameEntity();

          //      ProtoType.Mesh mesh = new ProtoType.Mesh();
             //   mesh.Name = (item as FileItem).ContentID;
              //  mData.gameEntity.meshes.Add(mesh);
            }
        }

        public String ContentID { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<ISceneItem> SceneItems { get; protected set; }

        public Byte[] ByteBuffer
        {
            get
            {
                //todo return m_FB_SceneNode.CreateByteBuffer();
                return new Byte[0];
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public DAL.MDB.SceneNode SceneNode { get; protected set; }

        protected Entity mDBData;

        [XmlIgnore]
        [Browsable(false)]
        public object DBData
        {
            get { return mDBData; }
            set
            {
                mDBData = value as Entity;

                Entity dbData = value as Entity;
                //ProtoType.CharEntity dataStaticObj = new ProtoType.CharEntity();

                //if (dbData.Data != null)
                //{
                //    mData = ProtoSerialize.Deserialize<ProtoType.CharEntity>(dbData.Data);

                //    if (mData.gameEntity == null)
                //        mData.gameEntity = new ProtoType.GameEntity();

                //    foreach (var item in mData.gameEntity.physics)
                //        m_Physics.Add(new PhysicObject() { ProtoData = item });

                //    m_Race = mDBData.RaceID ?? 0;
                //}
            }
        }

        #region GameEntityData

        //todo prototype!!!! or? skeleton path is located in mesh  file ...
        private String mSkeleton;

        [Editor(typeof(VFPathEditor), typeof(VFPathEditor))]
        [Category("GameEntity")]
        public String Skeleton { get { return mSkeleton; } set { mSkeleton = value; } }
      
        //  private List<String> mMeshes;
        //private List<Mesh> mMeshes;
        //[Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        //[NewItemTypes(new Type[] { typeof(Mesh), typeof(Plane), typeof(Cube) })]
        //[Category("GameEntity")]
        //public List<Mesh> Meshes { get { return mMeshes; } set { mMeshes = value; } }
        //public List<ProtoType.Mesh> Meshes { get { return mData.gameEntity.meshes; } }

        private List<PhysicObject> m_Physics;
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        [Category("GameEntity")]
        public List<PhysicObject> Physics { get { return m_Physics; } set { m_Physics = value; } }

        //private List<ProtoType.Sound> mSounds;
        //[Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        //[NewItemTypes(new Type[] { typeof(ProtoType.Sound) })]
        //[Category("GameEntity")]
        //public List<ProtoType.Sound> Sounds { get { return mSounds; } set { mSounds = value; } }

        //[Category("GameEntity")]
        //public Int32 RenderQueue { get { return mData.gameEntity.mode; } set { mData.gameEntity.mode = value; } }

        //[Category("GameEntity")]
        //public Int32 Mode { get { return mData.gameEntity.mode; } set { mData.gameEntity.mode = value; } }
        //[Category("GameEntity")]
        //public Boolean CastShadows { get { return mData.gameEntity.castShadows; } set { mData.gameEntity.castShadows = value; } }
        //[Category("GameEntity")]
        //public string AnimationTree { get { return mData.gameEntity.animationTree; } set { mData.gameEntity.animationTree = value; } }
        //[Category("GameEntity")]
        //public string AnimationInfo { get { return mData.gameEntity.animationInfo; } set { mData.gameEntity.animationInfo = value; } }
      
        //[Category("Debug")]
        //public ProtoType.Debug Debug { get { return mData.gameEntity.debug; } set { mData.gameEntity.debug = value; } }

        private long m_Race;
        [Category("Character")]
        public long RaceID { get { return m_Race; } set { m_Race = value; } }
      

        #endregion

        // [XmlIgnore]
        // public ProtoType.OgreSysType OgreSystemType { get { return mData.gameEntity.ogreSysType; } set { mData.gameEntity.ogreSysType = value; } }

        [XmlIgnore]
        //[Category("Conections")]
        //[Description("This property is a complex property and has no default editor.")]
        //  [ExpandableObject]
        [Browsable(false)]
        //public ProtoType.CharEntity ProtoData { get { return mData; } }

        private IDAL m_dbI;

        public Int32 ID { get; set; }
        public String Name { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public CollectionOfIItem Items { get; protected set; }

        [XmlIgnore]
        [Browsable(false)]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem miSave = new MenuItem() { Command = CmdSaveCharacterObj, Header = "Save" };
                list.Add(miSave);
                return list;
            }
        }


        [Browsable(false)]
        public Boolean IsExpanded { get; set; }

        [Browsable(false)]
        public Boolean IsSelected { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        [XmlIgnore]
        [Browsable(false)]
        public IItem Parent { get; protected set; }

        public Boolean Closing() { return true; }

        public Boolean Create()
        {
            //mData = new ProtoType.CharEntity();
            //mData.gameEntity = new ProtoType.GameEntity();

            //DBData = new GameEntity() { EntType = (decimal)ProtoType.EntityTypes.NT_Character };
            return true;
        }

        public Boolean Open(object id)
        {
            int contentID = WIDE_Helper.StringToContentIDData(ContentID).IntValue;
            if (contentID > 0)
            {
                DBData = m_dbI.selectEntity(contentID);

                // Console.WriteLine(BitConverter.ToString(res));
                //try
                //{
                //    mData = ProtoSerialize.Deserialize<ProtoType.CharEntity>((DBData as DAL.MDB.GameEntity).Data);
                //}
                //catch
                //{
                //    mData = new ProtoType.CharEntity();
                //}
            }
            else
            {
                //mData = new ProtoType.CharEntity();

                //DBData = new GameEntity() { EntType = (decimal)ProtoType.EntityTypes.NT_Character };
            }
            return true;
        }

        private ICommand CmdSaveCharacterObj;

        public void Refresh() { }
        public void Finish() { }

        public Boolean Save(object param)
        {
            try
            {
                DAL.MDB.Entity gameEntity = DBData as DAL.MDB.Entity;

                //Update Phyiscs Data
                //ProtoData.gameEntity.physics.Clear();
                //foreach (var item in m_Physics)
                //    ProtoData.gameEntity.physics.Add(item.ProtoData);

                //gameEntity.Data = ProtoSerialize.Serialize(ProtoData);
                //gameEntity.Name = this.Name;
                //gameEntity.RaceID = m_Race;

                //if (gameEntity.EntID > 0)
                //    m_dbI.updateGameEntity(gameEntity);
                //else
                //{
                //    gameEntity.EntType = (decimal)ProtoType.EntityTypes.NT_Character;
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

        public Boolean Delete() { return true; }

        [XmlIgnore]
        [Browsable(false)]
        public IUnityContainer UnityContainer { get; protected set; }

        public CharacterObjModel()
            : base(null)
        {

        }

        public CharacterObjModel(IItem parent, IUnityContainer unityContainer, IDAL dbI = null, Int32 id = 0)
            : base(unityContainer)
        {
            UnityContainer = unityContainer;

           // mMeshes = new List<Mesh>();
        //    mSounds = new List<ProtoType.Sound>();
         //   mRaceGenderVM = new RaceGenderViewModel();

            //mMeshes = new List<string>();
            Parent = parent;
            SceneItems = new ObservableCollection<ISceneItem>();
            CmdSaveCharacterObj = new CmdSaveCharacterObject(this);
            //  mtest = new Byte[10];
            Items = new CollectionOfIItem();

            if (dbI != null)
                m_dbI = dbI;
            else
                m_dbI = new IDAL();

            m_Physics = new List<PhysicObject>();
        //    mData = new ProtoType.CharEntity();
         //   mData.gameEntity = new ProtoType.GameEntity();
            /// ???????????????????????????
            SceneNode = new DAL.MDB.SceneNode();
        }
    }


    public class CmdSaveCharacterObject : ICommand
    {
        private CharacterObjModel m_CharacterObjModel;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_CharacterObjModel.Save(parameter);
        }

        public CmdSaveCharacterObject(CharacterObjModel som)
        {
            m_CharacterObjModel = som;
        }
    }
}