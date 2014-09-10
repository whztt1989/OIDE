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

namespace OIDE.Scene.Model
{

    public class CreatureModel : ISceneItem, ISceneNode
    {
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }

        private ProtoType.CharEntity mData;

        public void Drop(IItem item)
        {
            if (item is FileItem)
            {
                if (mData.gameEntity == null)
                    mData.gameEntity = new ProtoType.GameEntity();

                ProtoType.Mesh mesh = new ProtoType.Mesh();
                mesh.Name = (item as FileItem).ContentID;
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
                }
            }
        }

        //  private List<String> mMeshes;
        private List<Mesh> mMeshes;
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        [NewItemTypes(new Type[] { typeof(Mesh), typeof(Plane), typeof(Cube) })]
        public List<Mesh> Meshes { get { return mMeshes; } set { mMeshes = value; } }
        //public List<ProtoType.Mesh> Meshes { get { return mData.gameEntity.meshes; } }

        private List<PhysicObject> m_Physics;
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        public List<PhysicObject> Physics { get { return m_Physics; } set { m_Physics = value; } }


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
                MenuItem miSave = new MenuItem() { Command = CmdSaveCreatureObj, Header = "Save" };
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
                try
                {
                    mData = ProtoSerialize.Deserialize<ProtoType.CharEntity>((DBData as OIDE.DAL.MDB.GameEntity).Data);
                }
                catch
                {
                    mData = new ProtoType.CharEntity();
                }
            }
            else
            {
                mData = new ProtoType.CharEntity();

                DBData = new GameEntity() { EntType = (decimal)ProtoType.EntityTypes.NT_Character };
            }
            return true;
        }

        private ICommand CmdSaveCreatureObj;

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
                    DLL_Singleton.Instance.command("cmd physic " + gameEntity.EntID, gameEntity.Data, gameEntity.Data.Length); //.updateObject(0, (int)ObjType.Physic);

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

        public CreatureModel()
        {

        }

        public CreatureModel(IItem parent, IUnityContainer unityContainer, IDAL dbI = null, Int32 id = 0)
        {
            UnityContainer = unityContainer;

            mMeshes = new List<Mesh>();
            //mMeshes = new List<string>();
            Parent = parent;
            SceneItems = new ObservableCollection<ISceneItem>();
            CmdSaveCreatureObj = new CmdSaveCreatureObject(this);
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


    public class CmdSaveCreatureObject : ICommand
    {
        private CreatureModel m_CreatureModel;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_CreatureModel.Save();
        }

        public CmdSaveCreatureObject(CreatureModel som)
        {
            m_CreatureModel = som;
        }
    }
}