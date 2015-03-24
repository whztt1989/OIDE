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
using System.Windows;
using System.IO;

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

    public class CharacterEntity : EntityBaseModel, ISceneItem
    {
        private FB_CharacterObject m_FBData;


        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }
        public Int32 NodeID { get; set; }
      
        #region protodata
    
        //    [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.ComboBoxEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.ComboBoxEditor))]
        //    [NewItemTypes(new Type[] { typeof(RaceGenderViewModel) })]
      //   [ItemsSource(typeof(RaceGenderItemsSource))]
   //     [Editor(typeof(ObjectComboBoxEditor), typeof(ObjectComboBoxEditor))]  
      
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

        [XmlIgnore]
        [Browsable(false)]
        public DAL.IDAL.EntityContainer DB_Entity { get; set; }

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

        //private List<PhysicObject> m_Physics;
        //[Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        //[Category("GameEntity")]
        //public List<PhysicObject> Physics { get { return m_Physics; } set { m_Physics = value; } }

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

        //private long m_Race;
        //[Category("Character")]
        //public long RaceID { get { return m_Race; } set { m_Race = value; } }
      

        #endregion
        
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

        private Boolean m_opened;
        
        private Boolean m_IsSelected;
        [Browsable(false)]
        public Boolean IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                m_IsSelected = value;

           //     Open(WIDE_Helper.StringToContentIDData(ContentID).IntValue);
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        [XmlIgnore]
        [Browsable(false)]
        public IItem Parent { get; protected set; }

        public Boolean Closing() { return true; }

        public Boolean Open(IUnityContainer unityContainer, object id)
        {
            if (m_opened)
                return true;

            //   DB_Entity = m_dbI.selectEntityData(WIDE_Helper.StringToContentIDData(ContentID).IntValue); // database data

            //read data from lokal json file
            m_FBData = Helper.Utilities.USystem.XMLSerializer.Deserialize<FB_CharacterObject>("Scene/Entities/" + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml"); //ProtoSerialize.Deserialize<ProtoType.Node>(node.Data);
            if (m_FBData == null)
                Create(unityContainer);


            base.SetFBData(m_FBData.EntityBaseModel); //set base entity data

            //test
            //   m_FBData.Read(DB_Entity.Entity.Data);


            return m_opened = true;
        }

        private ICommand CmdSaveCharacterObj;

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
                    DB_Entity.Entity.EntType = (decimal)EntityTypes.NT_Character;
                    m_dbI.insertEntity(DB_Entity.Entity);
                    ContentID = ContentID + ":" + DB_Entity.Entity.EntID;
                }

                m_FBData.EntityBaseModel = base.m_BaseObj_FBData;
                Helper.Utilities.USystem.XMLSerializer.Serialize<FB_CharacterObject>(m_FBData, "Scene/Entities/" + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml");  // XML Serialize

            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
            return true;
        }

        public Boolean Create(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
            m_FBData = new FB_CharacterObject();

            return true;
        }

        public Boolean Delete()
        {

            try
            {
                m_dbI.deleteEntity(DB_Entity.Entity);
                Parent.Items.Remove(this);

                if (File.Exists("Scene/Entities/" + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml"))
                    File.Delete("Scene/Entities/" + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml");

                MessageBox.Show("character entity deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: character entity not deleted: " + ex.Message);
            }
            return true;
        }

        [XmlIgnore]
        [Browsable(false)]
        public IUnityContainer UnityContainer { get; protected set; }

        public CharacterEntity()
            : base(null)
        {

        }

        public CharacterEntity(IItem parent, IUnityContainer unityContainer, IDAL dbI = null, Int32 id = 0)
            : base(unityContainer)
        {
            UnityContainer = unityContainer;

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



            m_FBData = new FB_CharacterObject();
            base.m_BaseObj_FBData = new FB_EntityBaseModel();

            DB_Entity = new DAL.IDAL.EntityContainer();
            DB_Entity.Entity = new Entity();
            //mData = new ProtoType.StaticEntity();
            //mData.gameEntity = new ProtoType.GameEntity();
            /// ???????????????????????????
            SceneNode = new DAL.MDB.SceneNode();
        }
    }


    public class CmdSaveCharacterObject : ICommand
    {
        private CharacterEntity m_CharacterObjModel;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_CharacterObjModel.Save(parameter);
        }

        public CmdSaveCharacterObject(CharacterEntity som)
        {
            m_CharacterObjModel = som;
        }
    }
}