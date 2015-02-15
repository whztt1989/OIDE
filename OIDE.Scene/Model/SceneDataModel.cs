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
using DAL;
using OIDE.Scene.Commands;
using OIDE.Scene.Interface.Services;
using Wide.Interfaces.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using ProtoBuf;
using Module.Protob.Utilities;
using Module.History.Service;
using OIDE.Scene.Service;
using Wide.Interfaces;
using System.Windows.Media;
using FlatBuffers;
using OIDE.Scene.Model.Objects;
using DAL.MDB;
using OIDE.Scene.Model.Objects.FBufferObject;

namespace OIDE.Scene.Model
{

    public enum EntityTypes : ushort
    {
        NT_Unkown,
        NT_Physic,
        NT_Character,
        NT_Static,
        NT_Terrain,
        NT_Light,
        NT_Camera,
        NT_SpawnPoint,
    }

    /// <summary>
    /// Complete Scene description
    /// </summary>
    public class SceneDataModel : ViewModelBase, IScene
    {

        private ICommand CmdDeleteScene;
        private ICommand CmdSaveScene;
        
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
            try
            {
                if (item is ISceneItem)
                {
                    var sceneItem = item as ISceneItem;

                    DAL.MDB.SceneNode node = new SceneNode()
                    {
                        Name = "NEWNode_" + sceneItem.Name,
                        EntID = Module.Properties.Helpers.Helper.StringToContentIDData(sceneItem.ContentID).IntValue,
                    };

                    int highestNodeID = 1;
                    if(m_SceneItems.Any())
                        highestNodeID = m_SceneItems.Max(x => x.NodeID) + 1;

                    m_SceneItems.Add(new SceneNodeModel(this, UnityContainer, m_DBI) { NodeID = highestNodeID, SceneNodeDB = node, Name = node.Name ?? "NodeNoname" });
                }
            }
            catch(Exception ex)
            {

            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public DAL.MDB.Scene DB_SceneData  { get;  set; }

        private FB_Scene m_FB_SceneData = new FB_Scene();
        
        [XmlIgnore]
        [Category("Scene Data")]
        [Description("scene ambient color")]
        public System.Windows.Media.Color ColourAmbient 
        {
            get { return m_FB_SceneData.ColourAmbient; }
            set {
                //var m_oldColourAmbient = value;
          
                int res = m_FB_SceneData.SetColourAmbient(value);
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer SceneDataModel.ColourAmbient.SetColourAmbient ungültig (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                 //   ColourAmbient = m_oldColourAmbient;
                }

                RaisePropertyChanged("ColourAmbient"); 
            } 
        }

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

        private Boolean m_IsSelected;
        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsSelected {
            get { return m_IsSelected; } 
            set
            {
                m_IsSelected = value;
                if (value) Open(null);
            }
        }

        [XmlIgnore]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        [XmlIgnore]
        public IItem Parent { get; private set; }

        #region Scene Data

        public long? FogID { get; set; }
        public long SceneID { get; set; }
        public long? SkyID { get; set; }
        public long? TerrID { get; set; }

        #endregion


        private IDAL m_DBI;


        #region public command methods

        public Boolean Create() { return true; }

        public Boolean Closing() {
            m_Opened = false;
            return true; }

        private Boolean m_Opened;

        public Boolean Open(object id)
        {
            if (m_Opened)
                return false;
            else
                m_Opened = true;

            m_SceneService.SelectedScene = this;

            int sceneID = Module.Properties.Helpers.Helper.StringToContentIDData(ContentID).IntValue;
          
            String path = AppDomain.CurrentDomain.BaseDirectory + "Scene\\" + sceneID + ".xml";
            //read sceneData from DAL -- Read data from XML not from database -> database data not human readable
            m_FB_SceneData = Helper.Utilities.USystem.XMLSerializer.Deserialize<FB_Scene>(path); // XML Serialize
            if (m_FB_SceneData == null)
                m_FB_SceneData = new FB_Scene();
            
       
            m_FB_SceneData.RelPathToXML = "Scene\\" + sceneID + ".xml";
            m_FB_SceneData.AbsPathToXML = path;


            DB_SceneData = m_DBI.selectSceneDataOnly(sceneID); // database data

       //     m_FB_SceneData.Read(DB_SceneData.Data); //just for testing if data correctly saved!


            IEnumerable<DAL.IDAL.SceneNodeContainer> result = m_DBI.selectSceneNodes(sceneID); //scenenodes from database

            try
            {
                //select all Nodes
                foreach (var nodeContainer in result)
                {
                    if(!m_SceneItems.Where(x => x.NodeID == nodeContainer.Node.NodeID).Any()) // add node to scene if not exists
                    {
                        var sceneNode = new SceneNodeModel(this, UnityContainer, m_DBI) { SceneNodeDB = nodeContainer.Node, Name = nodeContainer.Node.Name ?? "NodeNoname" };
                        sceneNode.Open(sceneID);
                        m_SceneItems.Add(sceneNode);
                    }

                    switch ((EntityTypes)nodeContainer.Entity.EntType)
                    {
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
                       }

                }
            }
            catch (Exception ex)
            {
                //   m_SceneService.SelectedScene.SceneItems.Clear();
            }
            RaisePropertyChanged(null);
            return true;
        }


        public void Refresh() { }
        public void Finish() { }

        public Boolean Save(object param)
        {
          //  mDBData.Data = m_FBByteBuffer.Data; //ProtoSerialize.Serialize(mProtoData);


            //  mData = ProtoSerialize.Deserialize<ProtoType.Scene>(result);

            DB_SceneData.Data = m_FB_SceneData.CreateByteBuffer();
            DB_SceneData.SceneID = SceneID;


          //  m_FB_SceneData.Read(DB_SceneData.Data);

            // ProtoType.Scene protoData = new ProtoType.Scene();
            // protoData.colourAmbient = new ProtoType.Colour() { r = 5 , b =  6 , g = 7 };
       
            //save scene to db
            if (DB_SceneData.SceneID > 0)
                m_DBI.updateScene(DB_SceneData);
            else
                m_DBI.insertScene(DB_SceneData);

            Helper.Utilities.USystem.XMLSerializer.Serialize<FB_Scene>(m_FB_SceneData, m_FB_SceneData.AbsPathToXML); // XML Serialize

            //##   DLL_Singleton.Instance.consoleCmd("cmd sceneUpdate 0"); //.updateObject(0, (int)ObjType.Physic);

            //---------------------------
            //Nodes
            //------------------------------
            try
            {
                DAL.MDB.SceneNode nodes = new DAL.MDB.SceneNode();

                //select all Nodes
                foreach (var sceneItem in m_SceneService.SelectedScene.SceneItems)
                {
                    //foreach (var sceneItem in sceneCategoryItem.SceneItems)
                    //{
                    //ISceneNode sNode = sceneItem as ISceneNode;
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
                    var sNode = sceneItem as SceneNodeModel;
                    if (sNode != null)
                        sNode.Save(SceneID);
                    
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

                //                IEnumerable<DAL.IDAL.SceneNodeContainer> result = dbI.SaveSceneNodes(sceneID);

            }
            catch
            {
            }

            return true;
        }

        public Boolean Delete()
        {
            m_DBI.DeleteScene(Module.Properties.Helpers.Helper.StringToContentIDData(ContentID).IntValue);

            return true;
        }
        
        #endregion

        public SceneDataModel()
        {
            m_DBI = new IDAL();
            DB_SceneData = new DAL.MDB.Scene();
            m_SceneItems = new ObservableCollection<ISceneItem>();
          //  mProtoData = new ProtoType.Scene();

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
            DB_SceneData = new DAL.MDB.Scene();
            //     mProtoData = new ProtoType.Scene();

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
