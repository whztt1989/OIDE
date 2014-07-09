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

namespace OIDE.Scene.Model
{
    public class StaticObjectModel : ISceneItem, ISceneNode, IGameEntity
    {
        private ProtoType.StaticEntity mData;

        [XmlIgnore]
        [Browsable(false)]
        public IItem Parent { get; private set; }

        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }
        public String ContentID { get; set; }

        [XmlIgnore]
        public ProtoType.Node Node { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public OIDE.DAL.MDB.SceneNodes SceneNode { get; private set; }

        [XmlIgnore]
        [Browsable(false)]
        public object DBData { get; private set; }

   
        [XmlIgnore]
        [Category("Conections")]
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public object ProtoData { get { return mData; } }

        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }

        public Int32 ID { get; set; }
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
                MenuItem miSave = new MenuItem() { Header = "Save" };
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

        public Boolean Open() { return true; }
        public Boolean Save()
        {
            try
            {
                (DBData as OIDE.DAL.MDB.GameEntity).Data = ProtoSerialize.Serialize(ProtoData);

                if ((DBData as OIDE.DAL.MDB.GameEntity).EntID > 0)
                    m_dbI.updateGameEntity(DBData as OIDE.DAL.MDB.GameEntity);
                else
                {
                    (DBData as OIDE.DAL.MDB.GameEntity).EntType = (decimal)ProtoType.EntityTypes.NT_Static;
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

        private ICommand CmdSave;

        public Boolean Create() { return true; }
        public Boolean Delete() { return true; }

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

            Parent = parent;
            SceneItems = new ObservableCollection<ISceneItem>();
            CmdSave = new CmdSaveStaticObject(this);
            //  mtest = new Byte[10];
            Items = new CollectionOfIItem();

            if (dbI != null)
                m_dbI = dbI;
            else
                m_dbI = new IDAL();

            DBData = m_dbI.selectGameEntity(id);
            // Console.WriteLine(BitConverter.ToString(res));
            try
            {
                mData = ProtoSerialize.Deserialize<ProtoType.StaticEntity>((DBData as OIDE.DAL.MDB.GameEntity).Data);
            }
            catch
            {
                //  mData = new ProtoType.PhysicsObject();
            }

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
