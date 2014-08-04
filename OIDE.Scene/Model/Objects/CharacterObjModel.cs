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
using OIDE.DAL.MDB;
using Module.Protob.Utilities;
using OIDE.Scene.Model.Objects;
using System.Windows.Input;
using OIDE.InteropEditor.DLL;
using OIDE.DAL;
using Module.Properties.Helpers;
using Module.Properties.Types;

namespace OIDE.Scene.Model
{

    public class RaceGenderViewModel
    {
        private ProtoType.RaceGender mProtoData;

        public RaceGenderViewModel(Int32 idx)
        {
            mProtoData = new ProtoType.RaceGender();
            mProtoData.race = new ProtoType.Race();
            if (idx == 0)
                mProtoData.race.name = "Human NEU";
            else if (idx == 1)
                mProtoData.race.name = "Human WEU";
        }

        public String Name { get { return mProtoData.race.name; } set { mProtoData.race.name = value; } }
        public String Description { get { return mProtoData.race.description; } set { mProtoData.race.description = value; } }
        public ProtoType.Race Race { get { return mProtoData.race; } }
        public ProtoType.PhysicsObject BodyPhys { get { return mProtoData.bodyPhys; } }
        public ProtoType.PhysicsObject FootLPhys { get { return mProtoData.footLPhys; } }
        public ProtoType.PhysicsObject FootRPhys { get { return mProtoData.footRPhys; } }
        public ProtoType.PhysicsObject HandLPhys { get { return mProtoData.HandLPhys; } }
        public ProtoType.PhysicsObject HanfRPhys { get { return mProtoData.HandRPhys; } }

        public float TurnSpeed { get { return mProtoData.turnSpeed; } }
        public ProtoType.Gender Gender { get { return mProtoData.gender; } }
        public List<ProtoType.Sound> Sound { get { return mProtoData.sounds; } }
        public List<ProtoType.Face> Face { get { return mProtoData.faces; } }
        public List<ProtoType.Skin> Skin { get { return mProtoData.skins; } }
        public List<ProtoType.HairColor> HairColor { get { return mProtoData.hairColors; } }
        public List<ProtoType.HairStyle> HairStyle { get { return mProtoData.hairStyles; } }
        public List<ProtoType.FacialFeature> FacialFeature { get { return mProtoData.facials; } }
        public List<ProtoType.FacialColor> FacialColor { get { return mProtoData.facialcolors; } }
        public List<ProtoType.Shoulder> Shoulder { get { return mProtoData.shoulder; } }
        public List<ProtoType.Boots> Boots { get { return mProtoData.boots; } }
        public List<ProtoType.SpellEntity> SpellEntity { get { return mProtoData.spells; } }
    }

    public enum RaceGender 
    {
        HumanNEU,
        HumaWEU
    }

    //itamsource combobox
    //http://wpftoolkit.codeplex.com/discussions/351513

    public class RaceGenderItemsSource : IItemsSource
    {
        public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
        {
            Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection raceGenders = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
          
            raceGenders.Add(0, "Human NEU");
            raceGenders.Add(1, "Human WEU");
            return raceGenders;
        }
    }

    public class CharacterObjModel : ISceneItem, ISceneNode
    {
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }


        #region protodata

        private Int32 mRaceGenderVM;
        private List<RaceGenderViewModel> mRaceGenderVMList;

     //   private RaceGender mSelRGenderVM;

       //    [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.ComboBoxEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.ComboBoxEditor))]
        //    [NewItemTypes(new Type[] { typeof(RaceGenderViewModel) })]

        [Category("Customization")]
        [ItemsSource(typeof(RaceGenderItemsSource))]
        [Editor(typeof(ObjectComboBoxEditor), typeof(ObjectComboBoxEditor))]  
        public Int32 RaceGender { get { return mRaceGenderVM; } set { mRaceGenderVM = value; } }

        [Category("Customization")]
        public ProtoType.Face Face { get { return mData.face; } }
        [Category("Customization")]
        public ProtoType.Skin Skin { get { return mData.skin; } }
        [Category("Customization")]
        public ProtoType.HairColor HairColor { get { return mData.hairColor; } }
        [Category("Customization")]
        public ProtoType.HairStyle HairStyle { get { return mData.hairStyle; } }
        [Category("Customization")]
        public ProtoType.FacialFeature FacialFeature { get { return mData.facialfeature; } }
        [Category("Customization")]
        public ProtoType.FacialColor FacialColor { get { return mData.facialcolor; } }

        public ProtoType.Boots Boots { get { return mData.boots; } }
        public ProtoType.Shoulder Shoulder { get { return mData.shoulderMesh; } }

        public ProtoType.OneHandWeapon OneHandLWeapon { get { return mData.oneHLWeapon; } }
        public ProtoType.OneHandWeapon OneHandRWeapon { get { return mData.oneHRWeapon; } }
        public ProtoType.TwoHandWeapon TwoHandWeapon { get { return mData.twoHWeapon; } }

        public ProtoType.AI AI { get { return mData.ai; } }



        #endregion

        private ProtoType.CharEntity mData;

        public void Drop(IItem item)
        {
            if (item is FileItem)
            {
                if (mData.gameEntity == null)
                    mData.gameEntity = new ProtoType.GameEntity();

                ProtoType.Mesh mesh = new ProtoType.Mesh();
                mesh.Name = (item as FileItem).Path;
                mData.gameEntity.meshes.Add(mesh);
            }
        }

        public String ContentID { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }

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
            get { return mDBData; }
            set
            {
                mDBData = value as GameEntity;

                GameEntity dbData = value as GameEntity;
                ProtoType.CharEntity dataStaticObj = new ProtoType.CharEntity();

                if (dbData.Data != null)
                {
                    mData = ProtoSerialize.Deserialize<ProtoType.CharEntity>(dbData.Data);

                    if (mData.gameEntity == null)
                        mData.gameEntity = new ProtoType.GameEntity();

                    foreach (var item in mData.gameEntity.physics)
                        m_Physics.Add(new PhysicObject() { ProtoData = item });


                    mRaceGenderVMList = new List<RaceGenderViewModel>();

                    mRaceGenderVMList.Add(new RaceGenderViewModel(0));
                    mRaceGenderVMList.Add(new RaceGenderViewModel(1));
                }
            }
        }

        #region GameEntityData

        //  private List<String> mMeshes;
        private List<Mesh> mMeshes;
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        [NewItemTypes(new Type[] { typeof(Mesh), typeof(Plane), typeof(Cube) })]
        public List<Mesh> Meshes { get { return mMeshes; } set { mMeshes = value; } }
        //public List<ProtoType.Mesh> Meshes { get { return mData.gameEntity.meshes; } }

        private List<PhysicObject> m_Physics;
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        public List<PhysicObject> Physics { get { return m_Physics; } set { m_Physics = value; } }

        private List<ProtoType.Sound> mSounds;
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        [NewItemTypes(new Type[] { typeof(ProtoType.Sound) })]
        public List<ProtoType.Sound> Sounds { get { return mSounds; } set { mSounds = value; } }

        public Int32 Mode { get { return mData.gameEntity.mode; } set { mData.gameEntity.mode = value; } }
        public Boolean CastShadows { get { return mData.gameEntity.castShadows; } set { mData.gameEntity.castShadows = value; } }
        public ProtoType.Debug Debug { get { return mData.gameEntity.debug; } set { mData.gameEntity.debug = value; } }
        public string AnimationTree { get { return mData.gameEntity.animationTree; } set { mData.gameEntity.animationTree = value; } }
        public string AnimationInfo { get { return mData.gameEntity.animationInfo; } set { mData.gameEntity.animationInfo = value; } }


        #endregion

        // [XmlIgnore]
        // public ProtoType.OgreSysType OgreSystemType { get { return mData.gameEntity.ogreSysType; } set { mData.gameEntity.ogreSysType = value; } }

        [XmlIgnore]
        //[Category("Conections")]
        //[Description("This property is a complex property and has no default editor.")]
        //  [ExpandableObject]
        [Browsable(false)]
        public ProtoType.CharEntity ProtoData { get { return mData; } }

        private IDAL m_dbI;

        public Int32 ID { get; set; }
        public String Name { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public CollectionOfIItem Items { get; private set; }

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
        public IItem Parent { get; private set; }

        public Boolean Closing() { return true; }

        public Boolean Create()
        {
            mData = new ProtoType.CharEntity();
            mData.gameEntity = new ProtoType.GameEntity();

            DBData = new GameEntity() { EntType = (decimal)ProtoType.EntityTypes.NT_Character };
            return true;
        }

        public Boolean Open(object id)
        {
            int contentID = Helper.StringToContentIDData(ContentID).IntValue;
            if (contentID > 0)
            {
                DBData = m_dbI.selectGameEntity(contentID);
                // Console.WriteLine(BitConverter.ToString(res));
                //try
                //{
                //    mData = ProtoSerialize.Deserialize<ProtoType.CharEntity>((DBData as OIDE.DAL.MDB.GameEntity).Data);
                //}
                //catch
                //{
                //    mData = new ProtoType.CharEntity();
                //}
            }
            else
            {
                mData = new ProtoType.CharEntity();

                DBData = new GameEntity() { EntType = (decimal)ProtoType.EntityTypes.NT_Character };
            }
            return true;
        }

        private ICommand CmdSaveCharacterObj;

        public Boolean Save()
        {
            try
            {
                OIDE.DAL.MDB.GameEntity gameEntity = DBData as OIDE.DAL.MDB.GameEntity;

                //Update Phyiscs Data
                ProtoData.gameEntity.physics.Clear();
                foreach (var item in m_Physics)
                    ProtoData.gameEntity.physics.Add(item.ProtoData);

                gameEntity.Data = ProtoSerialize.Serialize(ProtoData);
                gameEntity.Name = this.Name;

                if (gameEntity.EntID > 0)
                    m_dbI.updateGameEntity(gameEntity);
                else
                {
                    gameEntity.EntType = (decimal)ProtoType.EntityTypes.NT_Character;
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

        public Boolean Delete() { return true; }

        [XmlIgnore]
        [Browsable(false)]
        public IUnityContainer UnityContainer { get; private set; }

        public CharacterObjModel()
        {

        }

        public CharacterObjModel(IItem parent, IUnityContainer unityContainer, IDAL dbI = null, Int32 id = 0)
        {
            UnityContainer = unityContainer;

            mMeshes = new List<Mesh>();
            mSounds = new List<ProtoType.Sound>();
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
            mData = new ProtoType.CharEntity();
            mData.gameEntity = new ProtoType.GameEntity();
            /// ???????????????????????????
            SceneNode = new DAL.MDB.SceneNodes();
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
            m_CharacterObjModel.Save();
        }

        public CmdSaveCharacterObject(CharacterObjModel som)
        {
            m_CharacterObjModel = som;
        }
    }
}