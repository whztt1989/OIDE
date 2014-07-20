using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using OIDE.DAL;
using OIDE.Scene.Commands;
using OIDE.Scene.Interface.Services;
using Wide.Interfaces.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using ProtoBuf;
using Module.Protob.Utilities;
using OIDE.DAL.MDB;
using Module.History.Service;
using Module.Properties.Helpers;
using OIDE.Scene.Service;
using Wide.Interfaces;

namespace OIDE.Scene.Model
{
    public enum NodeTypes
    {
        Unkown = 0,
        Physic = 1,
        Character = 2,
        Static = 3,
        Terrain = 4,
        Light = 5,
        Camera = 6,

    }


    /// <summary>
    /// Complete Scene description
    /// </summary>
    public class SceneDataModel : ViewModelBase, IScene
    {

        private ICommand CmdDeleteScene;
        private ICommand CmdSaveScene;
        private ProtoType.Scene mProtoData;
        private CollectionOfIItem m_Items;
        private ObservableCollection<ISceneItem> m_SceneItems;
        private ISceneItem mSelectedItem;
        //ICommand m_cmdCreateFile;
        //ICommand m_cmdDelete;

        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }
        public String ContentID { get; set; }

        public void Drop(IItem item)
        {
            if (item is ISceneItem)
            {
                ISceneNode tmp = item as ISceneNode;

                if (tmp.Node == null)
                    tmp.Node = new ProtoType.Node();

                m_SceneItems.Add(tmp as ISceneItem);
            }
        }

        private DAL.MDB.Scene mDBData;
        [Browsable(false)]
        [XmlIgnore]
        public DAL.MDB.Scene SceneData 
        {
            get   {  return mDBData; }
            set
            {
                mDBData = value;

                try
                {
                    mProtoData = ProtoSerialize.Deserialize<ProtoType.Scene>(mDBData.Data);
                    if (ProtoData.colourAmbient == null)
                        ProtoData.colourAmbient = new ProtoType.Colour();
                }
                catch
                {
                    mProtoData = new ProtoType.Scene();
                }
            }
        }

        //public struct Ambient
        //{
        //    public float r { get; set; }
        //    public float g { get; set; }
        //    public float b { get; set; }
        //    public float a { get; set; }
        //}

        //private Ambient m_Ambient;
        [XmlIgnore]
        [Category("Conections")]
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public ProtoType.Colour ColourAmbient { get { return ProtoData.colourAmbient; } set { ProtoData.colourAmbient = value; RaisePropertyChanged("ColourAmbient"); } }

        [XmlIgnore]
        //[Category("Conections")]
        //[Description("This property is a complex property and has no default editor.")]
        //[ExpandableObject]
        [Browsable(false)]
        public ProtoType.Scene ProtoData { get { return mProtoData; } set { mProtoData = value; } }

        public bool AddItem(ISceneItem item)
        {
            if (!m_SceneItems.Contains(item))
            {
                m_SceneItems.Add(item);
                return true;
            }
            return false;
        }

        [Browsable(false)]
        [XmlIgnore]
        public ObservableCollection<ISceneItem> SceneItems { get { return m_SceneItems; } set { m_SceneItems = value; } }

        [XmlIgnore]
        [Browsable(false)]
        public ISceneItem SelectedItem
        {
            get
            {
                return mSelectedItem;
            }
            set { mSelectedItem = value; }
        }


        [XmlAttribute]
        public String Name { get; set; }

        [Browsable(false)]
        public CollectionOfIItem Items { get { return m_Items; } set { m_Items = value; } }

        [Browsable(false)]
        [XmlIgnore]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem miSave = new MenuItem() { Command = CmdSaveScene, Header = "Save scene" };
                list.Add(miSave);

                MenuItem miDelete = new MenuItem() { Command = CmdDeleteScene, Header = "Delete scene" };
                list.Add(miDelete);
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
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        [XmlIgnore]
        public IItem Parent { get; private set; }

        #region Scene Data


        #endregion


        private IDAL m_DBI;

        public Boolean Create() { return true; }

        public Boolean Closing() { return true; }
        public Boolean Open()
        {
            m_SceneService.SelectedScene = this;
            int sceneID = 0;

            sceneID = Helper.StringToContentIDData(m_SceneService.SelectedScene.ContentID).IntValue;
          
            IEnumerable<OIDE.DAL.MDB.SceneNodes> result = m_DBI.selectSceneNodes(sceneID);

           try
            {
                //select all Nodes
                foreach (var node in result)
                {
                    //ProtoType.Node nodeDeserialized;
                    //if (node.Data == null)
                    //    nodeDeserialized = new ProtoType.Node();
                    //else
                    //    nodeDeserialized = ProtoSerialize.Deserialize<ProtoType.Node>(node.Data);

                    m_SceneItems.Add(new SceneNodeEntity(this, UnityContainer,m_DBI) { SceneNode = node, Name = node.Name ?? "NodeNoname" });
                    //switch ((NodeTypes)node.GameEntity.EntType)
                    //{
                    //    case NodeTypes.Static:

                    //        m_SceneItems.Add(new StaticObjectModel(this, UnityContainer, m_DBI)
                    //        {
                    //            ContentID = "StaticID:##:" + node.GameEntity.EntID,
                    //            DBData =  node.GameEntity,
                    //            Name = node.GameEntity.Name ?? ("StatNoname" + (int)node.GameEntity.EntID),
                    //            Node = nodeDeserialized

                    //        });// Data = gameEntityDataDeserialized });

                    //        break;
                    //    case NodeTypes.Physic:

                    //        ProtoType.PhysicsObject dataPhysObj = new ProtoType.PhysicsObject();
                    //        if (node.GameEntity.Data != null)
                    //            dataPhysObj = ProtoSerialize.Deserialize<ProtoType.PhysicsObject>(node.GameEntity.Data);

                    //        m_SceneItems.Add(new PhysicsObjectModel(this, UnityContainer, dataPhysObj, m_DBI)
                    //        {
                    //            ContentID = "PhysicID:##:" + node.GameEntity.EntID,
                    //            Name = node.GameEntity.Name ?? ("PhysNoname" + (int)node.GameEntity.EntID),
                    //            Node = nodeDeserialized
                    //        });// Data = gameEntityDataDeserialized });

                    //        break;

                    //    //case NodeTypes.Physic:
                    //    //    //  var itemCam = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == ""); // Search for Camera category
                    //    //   // if (itemCam.Any())
                    //    //        m_SceneItems.Add(new PhysicsObjectModel(this, UnityContainer , ) { Node = nodeDeserialized });

                    //    //    break;
                    //    //case NodeTypes.Static:

                    //    //    break;
                    //    case NodeTypes.Camera:
                    //        //todo contentid for camera

                    //        //   SceneNodes = new SceneNodes() { NodeID = sNode.NodeID, EntID = sNode.Node.EntityID, SceneID = ID, Data = ProtoSerialize.Serialize(sNode.Node) };


                    //        var itemCam = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == ""); // Search for Camera category
                    //        if (itemCam.Any())
                    //            itemCam.First().SceneItems.Add(new CameraModel(itemCam.First(), UnityContainer) { Node = nodeDeserialized });

                    //        break;
                    //    case NodeTypes.Light:

                    //        var itemLight = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == "");
                    //        if (itemLight.Any())
                    //            itemLight.First().SceneItems.Add(new LightModel(itemLight.First(), UnityContainer) { Node = nodeDeserialized });
                    //        break;
                    //}

                }
            }
            catch (Exception ex)
            {
                //   m_SceneService.SelectedScene.SceneItems.Clear();
            }

            return true;
        }

        public long? FogID { get; set; }
        public long SceneID { get; set; }
        public long? SkyID { get; set; }
        public long? TerrID { get; set; }

        public Boolean Save()
        {
            mDBData.Data = ProtoSerialize.Serialize(mProtoData);


            //  mData = ProtoSerialize.Deserialize<ProtoType.Scene>(result);

      
            // ProtoType.Scene protoData = new ProtoType.Scene();
            // protoData.colourAmbient = new ProtoType.Colour() { r = 5 , b =  6 , g = 7 };
    
            //save scene to db
            if (SceneData.SceneID > 0)
                m_DBI.updateScene(SceneData);
            else
                m_DBI.insertScene(SceneData);

           

            //##   DLL_Singleton.Instance.consoleCmd("cmd sceneUpdate 0"); //.updateObject(0, (int)ObjType.Physic);

            //---------------------------
            //Nodes
            //------------------------------
            try
            {
                OIDE.DAL.MDB.SceneNodes nodes = new DAL.MDB.SceneNodes();

                //select all Nodes
                foreach (var sceneItem in m_SceneService.SelectedScene.SceneItems)
                {
                    //foreach (var sceneItem in sceneCategoryItem.SceneItems)
                    //{
                    ISceneNode sNode = sceneItem as ISceneNode;
                    ISceneItem sItem = sceneItem as ISceneItem;

                    //-------------  Camera ----------------------------
                    //if (sceneItem is CameraModel)
                    //{
                    //    CameraModel obj = sceneItem as CameraModel;  
                    //    sNode
                    //    ProtoSerialize.Serialize(obj.Node);//insert into Scene Nodes
                    //    result = ProtoSerialize.Serialize(obj.Data);  //insert Object Data
                    //}

                    ////-------------  Camera ----------------------------
                    //else if (sceneItem is PhysicsObjectModel)
                    //{
                    //    PhysicsObjectModel obj = sceneItem as PhysicsObjectModel;
                    //    ProtoSerialize.Serialize(obj.Node);//insert into Scene Nodes
                    //    result = ProtoSerialize.Serialize(obj.Data);  //insert Object Data
                    //}

                    //Create scenenode for database
                    sNode.SceneNode.EntID = Helper.StringToContentIDData(sItem.ContentID).IntValue;
                 
                    sNode.SceneNode.SceneID = Helper.StringToContentIDData(ContentID).IntValue;
                    sNode.SceneNode.Name = sItem.Name;
                    sNode.SceneNode.Data = ProtoSerialize.Serialize(sNode.Node);//Node data


                    //save sceneNode to db
                    if (sNode.SceneNode.NodeID > 0)
                        m_DBI.updateSceneNode(sNode.SceneNode);
                    else
                        m_DBI.insertSceneNode(sNode.SceneNode);

                    //add items to scene categories not root !!
                    //using (MemoryStream stream = new MemoryStream(sceneItem.SceneItems..Node.Data))
                    //{
                    //    scenenode.Node nodeDeserialized = ProtoBuf.Serializer.Deserialize<scenenode.Node>(stream);

                    //    switch ((NodeTypes)nodeDeserialized.type)
                    //    {
                    //        case NodeTypes.Camera:
                    //            var itemCam = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == "");
                    //            if (itemCam.Any())
                    //                itemCam.First().SceneItems.Add(new CameraModel(itemCam.First(), UnityContainer));

                    //            break;
                    //        case NodeTypes.Light:

                    //            var itemLight = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == "");
                    //            if (itemLight.Any())
                    //                itemLight.First().SceneItems.Add(new LightModel(itemLight.First(), UnityContainer));
                    //            break;
                    //    }
                    //}
                    //  }
                }

                //                IEnumerable<OIDE.DAL.IDAL.SceneNodeContainer> result = dbI.SaveSceneNodes(sceneID);

            }
            catch
            {
            }

            return true;
        }

        public Boolean Delete()
        {
            m_DBI.DeleteScene(Helper.StringToContentIDData(ContentID).IntValue);

            return true;
        }

        public SceneDataModel()
        {
            m_DBI = new IDAL();
            m_SceneItems = new ObservableCollection<ISceneItem>();
            mProtoData = new ProtoType.Scene();
        }

        public IUnityContainer UnityContainer { get { return m_Container; } }

        private IUnityContainer m_Container;
        private ISceneService m_SceneService;

        public SceneDataModel(IItem parent, IUnityContainer container)
        {
            Parent = parent;
            m_Container = container;
            m_SceneService = container.Resolve<ISceneService>();
            m_DBI = new IDAL();
            mProtoData = new ProtoType.Scene();

            ////if (dbData == null)
            ////    SceneData = m_DBI.selectSceneDataOnly(id);
            ////else
            //    SceneData = dbData;

            ////  Console.WriteLine(BitConverter.ToString(res));
            //try
            //{
            //    mData = ProtoSerialize.Deserialize<ProtoType.Scene>(SceneData.Data);
            //}
            //catch
            //{
            //    mData = new ProtoType.Scene();
            //}


            m_SceneItems = new ObservableCollection<ISceneItem>();
            m_Items = new CollectionOfIItem();
            CmdSaveScene = new CmdSaveScene(this);
            CmdDeleteScene = new CmdDeleteScene(this);
        }
    }


}
