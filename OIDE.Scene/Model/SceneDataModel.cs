﻿#region License

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
using Module.Properties.Helpers;
using OIDE.Scene.Service;
using Wide.Interfaces;
using System.Windows.Media;
using FlatBuffers;
using OIDE.Scene.Model.Objects;
using DAL.MDB;

namespace OIDE.Scene.Model
{
    /// <summary>
    /// Complete Scene description
    /// </summary>
    public class SceneDataModel : ViewModelBase, IScene
    {

        private ICommand CmdDeleteScene;
        private ICommand CmdSaveScene;
        
        //private FlatBuffers.ByteBuffer m_FBByteBuffer;       
        //private XFBType.Scene m_FBData;

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
                var sceneItem  = item as ISceneItem;

                DAL.MDB.SceneNodes node = new SceneNodes() 
                {
                    Name = "NEWNode_" + sceneItem.Name,
                    EntID = Helper.StringToContentIDData(sceneItem.ContentID).IntValue,
                     
                };
                m_SceneItems.Add(new SceneNodeEntity(this, UnityContainer, m_DBI)
                { SceneNode = node, Name = node.Name ?? "NodeNoname" });
                
                //ISceneNode tmp = item as ISceneNode;
            
                //if (tmp.Node == null)
                //    tmp.Node = new ProtoType.Node();

                //m_SceneItems.Add(tmp as ISceneItem);
            }
        }


        [Browsable(false)]
        [XmlIgnore]
        public DAL.MDB.Scene DB_SceneData  { get;  set; }

        private FB_SceneModel m_FB_SceneData = new FB_SceneModel();
        
      
        [XmlIgnore]
        [Category("Scene Data")]
        [Description("scene ambient color")]
        public System.Windows.Media.Color ColourAmbient 
        {
            get { return m_FB_SceneData.ColourAmbient; }
            set {
                var m_oldColourAmbient = value;
          
                int res = m_FB_SceneData.SetColourAmbient(value);
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer SceneDataModel.ColourAmbient.SetColourAmbient ungültig (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                    ColourAmbient = m_oldColourAmbient;
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
        public Boolean Open(object id)
        {
            m_SceneService.SelectedScene = this;
            int sceneID = 0;

            sceneID = Helper.StringToContentIDData(m_SceneService.SelectedScene.ContentID).IntValue;

            IEnumerable<DAL.MDB.SceneNodes> result = m_DBI.selectSceneNodes(sceneID);

            try
            {
                m_FB_SceneData = FB_SceneModel.XMLDeSerialize("Scene/" + sceneID + ".xml"); // XML Serialize

                //read sceneData from DAL -- Read data from XML not from database -> database data not human readable
                //just for testing if data correctly saved!
                        DAL.MDB.Scene scene = m_DBI.selectSceneDataOnly(sceneID);
               m_FB_SceneData.Read(scene.Data);

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
            RaisePropertyChanged(null);
            return true;
        }

        public long? FogID { get; set; }
        public long SceneID { get; set; }
        public long? SkyID { get; set; }
        public long? TerrID { get; set; }

        public void Refresh() { }
        public void Finish() { }

        public Boolean Save(object param)
        {
          //  mDBData.Data = m_FBByteBuffer.Data; //ProtoSerialize.Serialize(mProtoData);


            //  mData = ProtoSerialize.Deserialize<ProtoType.Scene>(result);

            DB_SceneData.Data = m_FB_SceneData.CreateByteBuffer();
            DB_SceneData.SceneID = SceneID;
     
            // ProtoType.Scene protoData = new ProtoType.Scene();
            // protoData.colourAmbient = new ProtoType.Colour() { r = 5 , b =  6 , g = 7 };
       
            //save scene to db
            if (DB_SceneData.SceneID > 0)
                m_DBI.updateScene(DB_SceneData);
            else
                m_DBI.insertScene(DB_SceneData);

            m_FB_SceneData.XMLSerialize("Scene/" + 1 + ".xml");//DB_SceneData.SceneID); // XML Serialize

            ByteBuffer bbReadoutTest = new ByteBuffer(DB_SceneData.Data);
            var m_FBDataNOT = XFBType.Scene.GetRootAsScene(bbReadoutTest); // read      
            XFBType.Colour colourNOT = m_FBDataNOT.ColourAmbient();

            //##   DLL_Singleton.Instance.consoleCmd("cmd sceneUpdate 0"); //.updateObject(0, (int)ObjType.Physic);

            //---------------------------
            //Nodes
            //------------------------------
            try
            {
                DAL.MDB.SceneNodes nodes = new DAL.MDB.SceneNodes();

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
                    sNode.SceneNode.EntID = sNode.SceneNode.EntID;
                    
                    sNode.SceneNode.SceneID = Helper.StringToContentIDData(ContentID).IntValue;
                    sNode.SceneNode.Name = sItem.Name;
                    sNode.SceneNode.Data = ProtoSerialize.Serialize(sNode.ByteBuffer);//Node data


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

                //                IEnumerable<DAL.IDAL.SceneNodeContainer> result = dbI.SaveSceneNodes(sceneID);

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
