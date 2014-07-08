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

namespace OIDE.Scene.Model
{
    public class PhysicsObjectModel : ISceneItem , ISceneNode, IGameEntity
    {
        ICommand CmdSave;
        public String ContentID { get; set; }

        [XmlIgnore]
        public IItem Parent { get; private set; }
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }
      
        public ProtoType.Node Node { get; set; }
        public OIDE.DAL.MDB.SceneNodes SceneNode { get; private set; }

        public object DBData { get; private set; }
        private ProtoType.PhysicsObject mData;
        [Category("Conections")]
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public object ProtoData  {  get { return mData; } }

        [XmlAttribute]
        public String Name { get; set; }

        public ObservableCollection<ISceneItem> SceneItems { get; private set; }
        [Browsable(false)]
        public CollectionOfIItem Items { get; set; }

        public TreeNode TreeNode { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public List<MenuItem> MenuOptions { get {
            List<MenuItem> list = new List<MenuItem>();
            MenuItem miSave = new MenuItem() { Command = CmdSave, Header = "Save"};
            list.Add(miSave);
            return list;
        }}

        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsExpanded { get; set; }
        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsSelected { get; set; }

        [XmlIgnore]
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
        public Boolean Open() { return true; }
        public Boolean Save() {

            try
            {
                (DBData as OIDE.DAL.MDB.GameEntity).Data = ProtoSerialize.Serialize(ProtoData);

                if ((DBData as OIDE.DAL.MDB.GameEntity).EntID > 0)
                    m_dbI.updateGameEntity(DBData as OIDE.DAL.MDB.GameEntity);
                else
                {
                    (DBData as OIDE.DAL.MDB.GameEntity).EntType = (decimal)ProtoType.EntityTypes.NT_Physic;
                    m_dbI.insertGameEntity(DBData as OIDE.DAL.MDB.GameEntity);
                }

                if (DLL_Singleton.Instance.EditorInitialized)
                    DLL_Singleton.Instance.consoleCmd("cmd physic " + (DBData as OIDE.DAL.MDB.GameEntity).EntID); //.updateObject(0, (int)ObjType.Physic);

            }
            catch (Exception ex)
            {
                //     MessageBox.Show("dreck_" + id + "_!!!!");
            }
            return true;
        }
        public Boolean Delete() { return true; }

        public IUnityContainer UnityContainer { get; private set; }

        public IDAL IDAL { get { return m_dbI; } }

        private IDAL m_dbI;

        public PhysicsObjectModel(IItem parent,IUnityContainer container, IDAL dbI = null,Int32 id = 0)
       //     : base(commandManager, menuService)
        {
            UnityContainer = container;
            Parent = parent;
            Node = new ProtoType.Node();

            //treeview!!!
            //https://97382ac7-a-62cb3a1a-s-sites.googlegroups.com/site/mynetsamples/Home/HeterogeneousHierarchicalGrid_dotnetlearning.zip?attachauth=ANoY7cpVpx5NrxBapNDAY1J9TVZWnC3BbjAV_9eW3oEODR3KipOEtme6DajN31YDXndxPDnBb0IthlB2b3v72ODqSuwSkGoncu4flFwGAN7W1-sFmoOazjUzNwNyEiIiLtaW-iq05MJ8UCZicgNm4AEGonLl-JzzQkkuqP6dugIIxUioXowS9buLI8FDuTvj167BxnXqs6a7tbROPI9d5v_7_Y2soGpuAlP9P64EiaXqdDPD3pZbBEHQkTeOovCu2naswMlbMxCVMpYxXOr1irMwZWHWJcCf1A%3D%3D&attredirects=0

            if (dbI != null)
                m_dbI = dbI;
            else
                m_dbI = new IDAL();

            DBData = m_dbI.selectGameEntity(id);
           // Console.WriteLine(BitConverter.ToString(res));
            try
            {
                mData = ProtoSerialize.Deserialize<ProtoType.PhysicsObject>((DBData as OIDE.DAL.MDB.GameEntity).Data);       
            }catch
            {
              //  mData = new ProtoType.PhysicsObject();
            }

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