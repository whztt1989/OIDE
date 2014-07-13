using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using OIDE.Scene.Interface.Services;
using Module.Properties.Interface;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Xml.Serialization;
using OIDE.DAL;
using Microsoft.Practices.Unity;
using Module.Protob.Utilities;
using OIDE.InteropEditor.DLL;
using Module.Properties.Helpers;

namespace OIDE.Scene.Model
{
    public class PhysicsObjectModel : ISceneItem, IGameEntity
    {
        private ProtoType.PhysicsObject mData;
        private ICommand CmdSave;

        public String ContentID { get; set; }

        public void Drop(IItem item) { }

        [XmlIgnore]
        public IItem Parent { get; private set; }
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }

        [XmlIgnore]
        public ProtoType.Node Node { get; set; }

        [XmlIgnore]
        public OIDE.DAL.MDB.SceneNodes SceneNode { get; private set; }

        [XmlIgnore]
        public object DBData { get; private set; }

        [XmlIgnore]
        [Category("Conections")]
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public object ProtoData { get { return mData; } }

        [XmlAttribute]
        public String Name { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }

        [Browsable(false)]
        public CollectionOfIItem Items { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem miSave = new MenuItem() { Command = CmdSave, Header = "Save" };
                list.Add(miSave);
                return list;
            }
        }

        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsExpanded { get; set; }

        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsSelected { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }


        //private object mtest;

        //[Editor(typeof(ByteArrayUserControlEditor), typeof(ByteArrayUserControlEditor))]
        //public object test
        //{
        //    get { return mtest; }
        //    set
        //    {

        //            //Console.WriteLine("setted:" + BitConverter.ToString(value));
        //            mtest = value;

        //    }
        //}
        public PhysicsObjectModel() { }


        public Boolean Create() { return true; }
        public Boolean Open() {

            DBData = m_dbI.selectGameEntity(Helper.StringToContentIDData(ContentID).IntValue);
            // Console.WriteLine(BitConverter.ToString(res));
            try
            {
                mData = ProtoSerialize.Deserialize<ProtoType.PhysicsObject>((DBData as OIDE.DAL.MDB.GameEntity).Data);
            }
            catch
            {
                mData = new ProtoType.PhysicsObject();
            }
            return true; }
        public Boolean Save()
        {

            try
            {
                OIDE.DAL.MDB.GameEntity gameEntity = DBData as OIDE.DAL.MDB.GameEntity;
                gameEntity.Data = ProtoSerialize.Serialize(ProtoData);
                gameEntity.Name = this.Name;

                if (gameEntity.EntID > 0)
                    m_dbI.updateGameEntity(gameEntity);
                else
                {
                    gameEntity.EntType = (decimal)ProtoType.EntityTypes.NT_Physic;
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

        [XmlIgnore]
        [Browsable(false)]
        public IDAL IDAL { get { return m_dbI; } }

        private IDAL m_dbI;

        public PhysicsObjectModel(IItem parent, IUnityContainer container, ProtoType.PhysicsObject data, IDAL dbI = null, Int32 id = 0)
        //     : base(commandManager, menuService)
        {
            mData = data;
            UnityContainer = container;
            Parent = parent;
            Node = new ProtoType.Node();

            //treeview!!!
            //https://97382ac7-a-62cb3a1a-s-sites.googlegroups.com/site/mynetsamples/Home/HeterogeneousHierarchicalGrid_dotnetlearning.zip?attachauth=ANoY7cpVpx5NrxBapNDAY1J9TVZWnC3BbjAV_9eW3oEODR3KipOEtme6DajN31YDXndxPDnBb0IthlB2b3v72ODqSuwSkGoncu4flFwGAN7W1-sFmoOazjUzNwNyEiIiLtaW-iq05MJ8UCZicgNm4AEGonLl-JzzQkkuqP6dugIIxUioXowS9buLI8FDuTvj167BxnXqs6a7tbROPI9d5v_7_Y2soGpuAlP9P64EiaXqdDPD3pZbBEHQkTeOovCu2naswMlbMxCVMpYxXOr1irMwZWHWJcCf1A%3D%3D&attredirects=0

            if (dbI != null)
                m_dbI = dbI;
            else
                m_dbI = new IDAL();

          

            /// ???????????????????????????
            SceneNode = new DAL.MDB.SceneNodes();


            CmdSave = new CmdSavePhysicObject(this);
            //  mtest = new Byte[10];
            Items = new CollectionOfIItem();
            SceneItems = new ObservableCollection<ISceneItem>();
        }
    }


    public class CmdSavePhysicObject : ICommand
    {
        private PhysicsObjectModel mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mpm.Save();
        }

        public CmdSavePhysicObject(PhysicsObjectModel pm)
        {
            mpm = pm;
        }
    }
}