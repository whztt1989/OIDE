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

namespace OIDE.Scene.Model
{
    /// <summary>
    /// Complete Scene description
    /// </summary>
    public class SceneDataModel : IScene
    {

        private IHistoryCommand CmdSave;
        private ProtoType.Scene mData;
        private CollectionOfIItem m_Items;
        private ObservableCollection<ISceneItem> m_SceneItems;
        private ISceneItem mSelectedItem;
        //ICommand m_cmdCreateFile;
        //ICommand m_cmdDelete;

        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }
        public String ContentID { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public DAL.MDB.Scene SceneData { get; private set; }

        [XmlIgnore]
        [Category("Conections")]
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public ProtoType.Colour ColourAmbient { get { return mData.colourAmbient; } set { mData.colourAmbient = value; } }
      
        [XmlIgnore]
        [Category("Conections")]
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public ProtoType.Scene Data  {  get { return mData; } set { mData = value; }  }

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
        public ObservableCollection<ISceneItem> SceneItems { get { return m_SceneItems; }  set { m_SceneItems = value; } }

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
                MenuItem miSave = new MenuItem() { Command = CmdSave, Header = "Save" };
                list.Add(miSave);
                //MenuItem miDelete = new MenuItem() { Command = m_cmdDelete, Header = "Delete" };
                //list.Add(miDelete);
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

        private IDAL m_DBI;

        public Boolean Create() { return true; }
        
        public Boolean Open()
        {
            m_SceneService.SelectedScene = this;
            int sceneID = 0;
            string[] split = Regex.Split(m_SceneService.SelectedScene.ContentID, ":##:");
            if (split.Count() == 2)
            {
                string identifier = split[0];
                string path = split[1];
                if (identifier == "SceneID")
                {
                    int.TryParse(path, out sceneID);
                }
            }

            //includes Node and GameEntity
            IEnumerable<OIDE.DAL.IDAL.SceneNodeContainer> result = m_DBI.selectSceneNodes(sceneID);


            //  Console.WriteLine(BitConverter.ToString(res));
            try
            {
                //select all Nodes
                foreach (var node in result)
                {
                    ProtoType.Node nodeDeserialized = ProtoSerialize.Deserialize<ProtoType.Node>(node.Node.Data);
                    ProtoType.GameEntity gameEntityDeserialized = ProtoSerialize.Deserialize<ProtoType.GameEntity>(node.GameEntity.Data);


                    //add items to scene categories not root !!
                    switch ((NodeTypes)node.GameEntity.EntType)
                    {
                        case NodeTypes.Camera:
                            //todo contentid for camera

                         //   SceneNodes = new SceneNodes() { NodeID = sNode.NodeID, EntID = sNode.Node.EntityID, SceneID = ID, Data = ProtoSerialize.Serialize(sNode.Node) };
             

                            var itemCam = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == ""); // Search for Camera category
                            if (itemCam.Any())
                                itemCam.First().SceneItems.Add(new CameraModel(itemCam.First(), UnityContainer) {  Node = nodeDeserialized });

                            break;
                        case NodeTypes.Light:

                            var itemLight = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == "");
                            if (itemLight.Any())
                                itemLight.First().SceneItems.Add(new LightModel(itemLight.First(), UnityContainer) { Node = nodeDeserialized });
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                m_SceneService.SelectedScene.SceneItems.Clear();
            }

            //---------------------------------------------
            //Scene Graph Tree
            //---------------------------------------------
            //SceneCategoryModel root = new SceneCategoryModel(null, commandManager, menuService) { Name = "RootNode" };

            //   Scene.SceneCategoryModel rootScene = new Scene.SceneCategoryModel(null, commandManager, menuService) { Name = "RootNode" };
            //SceneDataModel scene = new SceneDataModel(null, m_Container) { Name = "Scene 1" };

            SpawnPointCategoryModel spawns = new SpawnPointCategoryModel(this, m_Container) { Name = "SpawnPoints" };
            //SceneCategoryModel controller1 = new SceneCategoryModel(scene, m_Container) { Name = "SpawnPoint 1" };
            //controllers.Items.Add(controller1);
            spawns.IsExpanded = true;
            this.SceneItems.Add(spawns);



            PhysicCategoryModel dynamics = new PhysicCategoryModel(this, m_Container) { Name = "Physics" };

            dynamics.IsExpanded = true;

            this.SceneItems.Add(dynamics);


            //PhysicsObjectModel pom = new PhysicsObjectModel(dynamics, dynamics.UnityContainer) { Name = "Phys 1", ContentID = "PhysicID:##" };

            //dynamics.SceneItems.Add(pom);
            ////SceneCategoryModel triggers = new SceneCategoryModel(scene, m_Container) { Name = "Triggers" };
            //SceneCategoryModel trigger1 = new SceneCategoryModel(scene, m_Container) { Name = "Trigger 1" };
            //triggers.Items.Add(trigger1);
            //scene.SceneItems.Add(triggers);


            StaticObjectCategoyModel statics = new StaticObjectCategoyModel(this, m_Container) { Name = "Statics" };
            statics.IsExpanded = true;
            this.SceneItems.Add(statics);

          //  m_SceneService.SetAsRoot(this);

            //PhysicsObjectModel pom = new PhysicsObjectModel(dynamics, dynamics.UnityContainer) { Name = "Phys 1", ContentID = "PhysicID:##" };
            //dynamics.SceneItems.Add(pom);
            //SceneCategoryModel obj1 = new SceneCategoryModel(scene, m_Container) { Name = "Object1" };
            //SceneCategoryModel physics = new SceneCategoryModel(statics, m_Container) { Name = "Physics" };
            //PhysicsObjectModel po1 = new PhysicsObjectModel(physics, m_Container, 0) { Name = "pomChar1" };
            //physics.Items.Add(po1);
            //obj1.Items.Add(physics);
            //SceneCategoryModel obj2 = new SceneCategoryModel(scene, m_Container) { Name = "Floor (Obj)" };
            //statics.Items.Add(obj2);
            //statics.Items.Add(obj1);
            //scene.SceneItems.Add(statics);

            //SceneCategoryModel terrain = new SceneCategoryModel(scene, m_Container) { Name = "Terrain" };
            //scene.SceneItems.Add(terrain);


            //    scene.Items.Add(scene);
            //    m_SceneService.Scenes.Add(scene);

            return true;
        }

        public long? FogID { get; set; }
        public long SceneID { get; set; }
        public long? SkyID { get; set; }
        public long? TerrID { get; set; }

        public Boolean Save()
        {

            // ProtoType.Scene protoData = new ProtoType.Scene();
            // protoData.colourAmbient = new ProtoType.Colour() { r = 5 , b =  6 , g = 7 };

            //save scene to db
            if (SceneData.SceneID > 0)
               m_DBI.updateScene(SceneData);
            else
                m_DBI.insertScene(SceneData);

            //  mData = ProtoSerialize.Deserialize<ProtoType.Scene>(result);


            //##   DLL_Singleton.Instance.consoleCmd("cmd sceneUpdate 0"); //.updateObject(0, (int)ObjType.Physic);

            //---------------------------
            //Nodes
            //------------------------------
            try
            {
                OIDE.DAL.MDB.SceneNodes nodes = new DAL.MDB.SceneNodes();

                //select all Nodes
                foreach (var sceneCategoryItem in m_SceneService.SelectedScene.SceneItems)
                {
                    foreach (var sceneItem in sceneCategoryItem.SceneItems)
                    {
                        ISceneNode sNode = sceneItem as ISceneNode;

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
                    }
                }

                //                IEnumerable<OIDE.DAL.IDAL.SceneNodeContainer> result = dbI.SaveSceneNodes(sceneID);

            }
            catch
            {
            }

            return true;
        }

        public Boolean Delete() { return true; }

        public SceneDataModel()
        {
            m_DBI = new IDAL();
            m_SceneItems = new ObservableCollection<ISceneItem>();
        }

        public IUnityContainer UnityContainer { get { return m_Container; } }

        private IUnityContainer m_Container;
        private ISceneService m_SceneService;

        public SceneDataModel(IItem parent, IUnityContainer container, Int32 id = 0)
        {
            Parent = parent;
            m_Container = container;
            m_SceneService = container.Resolve<ISceneService>();
            m_DBI = new IDAL();
           
            SceneData = m_DBI.selectSceneDataOnly(id);
           
            //  Console.WriteLine(BitConverter.ToString(res));
            try
            {
                mData = ProtoSerialize.Deserialize<ProtoType.Scene>(SceneData.Data);
            }
            catch
            {
                mData = new ProtoType.Scene();
            }

            m_SceneItems = new ObservableCollection<ISceneItem>();
            m_Items = new CollectionOfIItem();
            CmdSave = new CmdSaveScene(this);
        }
    }


}
