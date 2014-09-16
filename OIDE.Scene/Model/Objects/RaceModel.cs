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
using OIDE.VFS.View;

namespace OIDE.Scene.Model
{

    public enum RaceGender
    {
        HumanNEU,
        HumaWEU
    }

    public class RaceModel : IItem
    {
        private ProtoType.RaceGender m_ProtoData;

        private ProtoType.RaceGender ProtoData
        {
            get
            {
                if (m_ProtoData == null)
                {
                    m_ProtoData = new ProtoType.RaceGender();
                    m_ProtoData.race = new ProtoType.Race();
                    //mRaceGenderVM = new RaceGenderViewModel(mData);

                    if (m_ProtoData.bodyPhys == null) m_ProtoData.bodyPhys = new ProtoType.PhysicsObject();
                    if (m_ProtoData.footLPhys == null) m_ProtoData.footLPhys = new ProtoType.PhysicsObject();
                    if (m_ProtoData.footRPhys == null) m_ProtoData.footRPhys = new ProtoType.PhysicsObject();
                    if (m_ProtoData.HandLPhys == null) m_ProtoData.HandLPhys = new ProtoType.PhysicsObject();
                    if (m_ProtoData.HandRPhys == null) m_ProtoData.HandRPhys = new ProtoType.PhysicsObject();

                }

                return m_ProtoData;
            }

            set
            {
                m_ProtoData = value;
            }
        }

        public void Drop(IItem item)
        {
            if (item is FileItem)
            {
                //if (mData.gameEntity == null)
                //    mData.gameEntity = new ProtoType.GameEntity();

                //ProtoType.Mesh mesh = new ProtoType.Mesh();
                //mesh.Name = (item as FileItem).Path;
                //mData.gameEntity.meshes.Add(mesh);
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

        private Race mDBData;

        [XmlIgnore]
        [Browsable(false)]
        public object DBData
        {
            get { return mDBData; }
            set
            {
                mDBData = value as Race;

                Race dbData = value as Race;
                ProtoType.RaceGender data = new ProtoType.RaceGender();

                if (dbData.Data != null)
                {
                    ProtoType.RaceGender protoData = ProtoSerialize.Deserialize<ProtoType.RaceGender>(dbData.Data);

                    if (protoData != null)
                    {
                        ProtoData = protoData;
                    }

                    //foreach (var item in mData.gameEntity.physics)
                    //    m_Physics.Add(new PhysicObject() { ProtoData = item });
                }
            }
        }

        //  private List<String> mMeshes;
        //private List<Mesh> mMeshes;
        //[Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        //[NewItemTypes(new Type[] { typeof(Mesh), typeof(Plane), typeof(Cube) })]
        //public List<Mesh> Meshes { get { return mMeshes; } set { mMeshes = value; } }
        ////public List<ProtoType.Mesh> Meshes { get { return mData.gameEntity.meshes; } }

        //private List<PhysicObject> m_Physics;
        //[Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        //public List<PhysicObject> Physics { get { return m_Physics; } set { m_Physics = value; } }


        // [XmlIgnore]
        // public ProtoType.OgreSysType OgreSystemType { get { return mData.gameEntity.ogreSysType; } set { mData.gameEntity.ogreSysType = value; } }

      //  [XmlIgnore]
        //[Category("Conections")]
        //[Description("This property is a complex property and has no default editor.")]
        //  [ExpandableObject]
       // [Browsable(false)]
        //public ProtoType.RaceGender ProtoData { get { return mData; } }

        // private RaceGenderViewModel mRaceGenderVM;
        #region RaceGender

        [Category("Base")]
        public String Name { get { return ProtoData.race.name; } set { ProtoData.race.name = value; } }
        [Category("Base")]
        public String Description { get { return ProtoData.race.description; } set { ProtoData.race.description = value; } }
        // public ProtoType.Race Race { get { return ProtoData.race; } set { ProtoData.race = value; } }
        [Category("Physic")]
        [ExpandableObject]
        public ProtoType.PhysicsObject BodyPhys { get { return ProtoData.bodyPhys; } set { ProtoData.bodyPhys = value; } }
        [Category("Physic")]
        [ExpandableObject]
        public ProtoType.PhysicsObject FootLPhys { get { return ProtoData.footLPhys; } set { ProtoData.footLPhys = value; } }
        [Category("Physic")]
        [ExpandableObject]
        public ProtoType.PhysicsObject FootRPhys { get { return ProtoData.footRPhys; } set { ProtoData.footRPhys = value; } }
        [Category("Physic")]
        [ExpandableObject]
        public ProtoType.PhysicsObject HandLPhys { get { return ProtoData.HandLPhys; } set { ProtoData.HandLPhys = value; } }
        [Category("Physic")]
        [ExpandableObject]
        public ProtoType.PhysicsObject HanfRPhys { get { return ProtoData.HandRPhys; } set { ProtoData.HandRPhys = value; } }

        public float TurnSpeed { get { return ProtoData.turnSpeed; } set { ProtoData.turnSpeed = value; } }

        [Category("Data")]   
        public ProtoType.Gender Gender { get { return ProtoData.gender; } set { ProtoData.gender = value; } }
        [Category("Data")]
        public List<ProtoType.Sound> Sound { get { return ProtoData.sounds; } set { } }
        [Category("Data")]
        public List<ProtoType.Face> Face { get { return ProtoData.faces; } set { } }
        [Category("Data")]
        public List<ProtoType.Skin> Skin { get { return ProtoData.skins; } set { } }
        [Category("Data")]
        public List<ProtoType.HairColor> HairColor { get { return ProtoData.hairColors; } set { } }
        [Category("Data")]
        public List<ProtoType.HairStyle> HairStyle { get { return ProtoData.hairStyles; } set { } }
        [Category("Data")]
        public List<ProtoType.FacialFeature> FacialFeature { get { return ProtoData.facials; } set { } }
        [Category("Data")]
        public List<ProtoType.FacialColor> FacialColor { get { return ProtoData.facialcolors; } set { } }
        [Category("Data")]
        public List<ProtoType.Shoulder> Shoulder { get { return ProtoData.shoulder; } set { ; } }
        [Category("Data")]
        public List<ProtoType.Boots> Boots { get { return ProtoData.boots; } set { } }
        [Category("Data")]
        public List<ProtoType.SpellEntity> SpellEntity { get { return ProtoData.spells; } set { } }

        #endregion
        // [XmlIgnore]
       // [ExpandableObject]
       // public RaceGenderViewModel RaceGender { get { return mRaceGenderVM; } }

        private IDAL m_dbI;

        [Category("Identification")]
        public Int64 ID { get { return (DBData as Race).RaceID; } set { (DBData as Race).RaceID = value; } }

        private String mSkeleton;

        [Editor(typeof(VFPathEditor), typeof(VFPathEditor))]
        [Category("GameEntity")]
        public String Skeleton { get { return mSkeleton; } set { mSkeleton = value; } }

        [Editor(typeof(VFPathEditor), typeof(VFPathEditor))]
        [Category("GameEntity")]
        public string AnimationTree { get { return ProtoData.animationTree; } set { ProtoData.animationTree = value; } }
        [Editor(typeof(VFPathEditor), typeof(VFPathEditor))]
        [Category("GameEntity")]
        public string AnimationInfo { get { return ProtoData.animationInfo; } set { ProtoData.animationInfo = value; } }
      

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
                MenuItem miSave = new MenuItem() { Command = CmdSaveRaceObj, Header = "Save" };
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
            //mProtoData = new ProtoType.RaceGender();
           // mData.gameEntity = new ProtoType.GameEntity();

            DBData = new Race() {  };
            return true;
        }

        public Boolean Open(object id)
        {
            int contentID = Helper.StringToContentIDData(ContentID).IntValue;
            if (contentID > 0)
            {
                DBData = m_dbI.selectRace(contentID);
                // Console.WriteLine(BitConverter.ToString(res));
                try
                {
                   ProtoType.RaceGender protoData = ProtoSerialize.Deserialize<ProtoType.RaceGender>((DBData as OIDE.DAL.MDB.Race).Data);
                   if (protoData != null)
                       ProtoData = protoData;
                }
                catch
                {
                }
            }
            else
            {

                DBData = new Race() {  };
            }
            return true;
        }

        private ICommand CmdSaveRaceObj;

        public Boolean Save()
        {
            try
            {
                OIDE.DAL.MDB.Race race = DBData as OIDE.DAL.MDB.Race;

                //Update Phyiscs Data
                //ProtoData.gameEntity.physics.Clear();
                //foreach (var item in m_Physics)
                //    ProtoData.gameEntity.physics.Add(item.ProtoData);

                race.Data = ProtoSerialize.Serialize(ProtoData);
                //race.Data..Name = this.Name;

                if (race.RaceID > 0)
                    m_dbI.updateRace(race);
                else
                {
                    //      gameEntity.EntType = (decimal)ProtoType.EntityTypes.NT_Race;
                    m_dbI.insertRace(race);
                    (DBData as OIDE.DAL.MDB.Race).RaceID = race.RaceID;
                }

           //     if (DLL_Singleton.Instance.EditorInitialized)
           //         DLL_Singleton.Instance.consoleCmd("cmd physic " + gameEntity.EntID); //.updateObject(0, (int)ObjType.Physic);

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

        public RaceModel()
        {
            SceneItems = new ObservableCollection<ISceneItem>();
            CmdSaveRaceObj = new CmdSaveRaceObject(this);
            Items = new CollectionOfIItem();
            m_dbI = new IDAL();
            SceneNode = new DAL.MDB.SceneNodes();
        }

        public RaceModel(IItem parent, IUnityContainer unityContainer, IDAL dbI = null, Int64 id = 0)
        {
            UnityContainer = unityContainer;
            ID = id;
           // mMeshes = new List<Mesh>();
            //mMeshes = new List<string>();
            Parent = parent;
            SceneItems = new ObservableCollection<ISceneItem>();
            CmdSaveRaceObj = new CmdSaveRaceObject(this);
            //  mtest = new Byte[10];
            Items = new CollectionOfIItem();

            if (dbI != null)
                m_dbI = dbI;
            else
                m_dbI = new IDAL();

          //  m_Physics = new List<PhysicObject>();
          

           // mData.gameEntity = new ProtoType.GameEntity();
            /// ???????????????????????????
            SceneNode = new DAL.MDB.SceneNodes();
        }
    }


    public class CmdSaveRaceObject : ICommand
    {
        private RaceModel m_RaceModel;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_RaceModel.Save();
        }

        public CmdSaveRaceObject(RaceModel som)
        {
            m_RaceModel = som;
        }
    }
}