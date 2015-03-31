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
using OIDE.Scene.Interface;
using Module.PFExplorer.Interface;
using Module.PFExplorer.Utilities;

namespace OIDE.Scene.Model
{
    public interface IDBData
    {
        object DBData { get; }
    }

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
    [Serializable]
    public class SceneDataModel : ContentModel, IScene, IDBData
    {

        private ICommand CmdDeleteScene;
        private ICommand CmdSaveScene;


     //   private CollectionOfISceneItem m_SceneItems;
        private ISceneItem mSelectedItem;
        //ICommand m_cmdCreateFile;
        //ICommand m_cmdDelete;

        public Int32 NextNodeCount()
        {
            return SceneItems.Count(x => x is SceneNodeModel);
        }

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

                    //int highestNodeID = 1;
                    //if(SceneItems.Any())
                    //    highestNodeID = SceneItems.Max(x => x.NodeID) + 1;

                    SceneItems.Add(
                        new SceneNodeModel()
                        {
                            Parent = this,
                            UnityContainer = UnityContainer,
                            IDAL = m_DBI,
                            NodeID = NextNodeCount(),
                            SceneNodeDB = node,
                            Name = node.Name ?? "NodeNoname"
                        });
                }
            }
            catch(Exception ex)
            {

            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public DAL.MDB.Scene DB_SceneData  { get;  set; }

        #region serializable data

        private FB_Scene m_FB_SceneData = new FB_Scene();

   //     [XmlIgnore]
        [ExpandableObject]
  //      public FB_Scene FB_SceneData { get { return m_FB_SceneData; } }
        public object DBData {

            get { return m_FB_SceneData;   }
        }

        #endregion

        //#region SceneData

        ////only changeable in Scenetree! not in propertygrid
        //[XmlIgnore]
        //[Browsable(false)]
        //public System.Windows.Media.Color ColourAmbient 
        //{
        //    get { return m_FB_SceneData.ColourAmbient; }
        //    set {
        //        m_FB_SceneData.ColourAmbient = value;  
        //        RaisePropertyChanged("ColourAmbient"); 
        //    } 
        //}

        //#endregion

        public bool AddItem(ISceneItem item)
        {
            if (!SceneItems.Contains(item))
            {
                SceneItems.Add(item);
                return true;
            }
            return false;
        }

        [Browsable(false)]
        [XmlIgnore]
        public CollectionOfISceneItem SceneItems { get { return m_FB_SceneData.SceneItems; } set { m_FB_SceneData.SceneItems = value; } }
 //public CollectionOfISceneItem SceneItems { get { return m_SceneItems; } set { m_SceneItems = value; } }

        [XmlIgnore]
        [Browsable(false)]
        public ISceneItem SelectedItem
        {
            get
            {
                return mSelectedItem;
            }
            set {
                mSelectedItem = value;

                m_SceneService.SGTM.SelectedObject = value;
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public override List<MenuItem> MenuOptions
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
    
        //#region Scene Data

        //public long? FogID { get; set; }
        public long SceneID { get; set; }
        //public long? SkyID { get; set; }
        //public long? TerrID { get; set; }

        //#endregion


        private IDAL m_DBI;


        #region public command methods

        public Boolean Create(IUnityContainer unityContainer)
        {

            UnityContainer = unityContainer;
            m_SceneService = unityContainer.Resolve<ISceneService>();
            m_DBI = new IDAL(unityContainer);

            return true;
        }

        public Boolean Closing()
        {
            m_Opened = false;
            return true;
        }

        private Boolean m_Opened;

        private IProjectFile m_ParentProject;

        public Boolean Open(IUnityContainer unityContainer, object id)
        {
            if (m_Opened)
                return false;
            else
                m_Opened = true;

            //get parent project
            m_ParentProject = PFUtilities.GetRekursivParentPF(this.Parent) as IProjectFile;

            if (m_ParentProject == null)
                return false;


            UnityContainer = unityContainer;
            m_SceneService = unityContainer.Resolve<ISceneService>();

            m_DBI = new IDAL(unityContainer);

            m_SceneService.SelectedScene = this;

            int sceneID = Module.Properties.Helpers.Helper.StringToContentIDData(ContentID).IntValue;
          
            this.Location = m_ParentProject.Folder + "\\Scene\\" + sceneID + ".xml";
          
            //read sceneData from DAL -- Read data from XML not from database -> database data not human readable
            m_FB_SceneData = Helper.Utilities.USystem.XMLSerializer.Deserialize<FB_Scene>(this.Location.ToString()); // XML Serialize
            if (m_FB_SceneData == null)
                m_FB_SceneData = new FB_Scene();
            
       
            //m_FB_SceneData.RelPathToXML = "Scene\\" + sceneID + ".xml";
            //m_FB_SceneData.AbsPathToXML = path;


       //only able to save data into database not load
            return true ;


            DB_SceneData = m_DBI.selectSceneDataOnly(sceneID); // database data

       //     m_FB_SceneData.Read(DB_SceneData.Data); //just for testing if data correctly saved!


            IEnumerable<DAL.IDAL.SceneNodeContainer> result = m_DBI.selectSceneNodes(sceneID); //scenenodes from database

            try
            {
                //select all Nodes
                foreach (var nodeContainer in result)
                {
                    //if(!SceneItems.Where(x => x.NodeID == nodeContainer.Node.NodeID).Any()) // add node to scene if not exists
                    //{
                    //    //var sceneNode = new SceneNodeModel(this, UnityContainer, m_DBI) { SceneNodeDB = nodeContainer.Node, Name = nodeContainer.Node.Name ?? "NodeNoname" };
                    //    //sceneNode.Open(UnityContainer, sceneID);
                    //    //SceneItems.Add(sceneNode);
                    //}

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
            if (m_DBI == null)
                m_DBI = new IDAL(UnityContainer);

            Helper.Utilities.USystem.XMLSerializer.Serialize<FB_Scene>(m_FB_SceneData, this.Location.ToString()); // XML Serialize
            
            //  mData = ProtoSerialize.Deserialize<ProtoType.Scene>(result);
            if (DB_SceneData == null)
                DB_SceneData = new DAL.MDB.Scene();

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
                    {
                        sNode.IDAL = m_DBI;
                        sNode.Save(SceneID);
                    }
                    
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
            if (m_DBI == null)
                m_DBI = new IDAL(UnityContainer);

            m_DBI.DeleteScene(Module.Properties.Helpers.Helper.StringToContentIDData(ContentID).IntValue);

            return true;
        }
        
        #endregion

        //public SceneDataModel()
        //{
        //    m_DBI = new IDAL();
        //    DB_SceneData = new DAL.MDB.Scene();
        //    m_SceneItems = new CollectionOfISceneItem();
        //  //  mProtoData = new ProtoType.Scene();

        //}

        private ISceneService m_SceneService;

        public SceneDataModel()
        {
          //  Parent = parent;
            //m_Container = container;
            //m_SceneService = container.Resolve<ISceneService>();
            
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


      //      m_SceneItems = new CollectionOfISceneItem();
            CmdSaveScene = new CmdSaveScene(this);
            CmdDeleteScene = new CmdDeleteScene(this);
        }

        #region contentmodel

          internal void SetLocation(object location)
        {
            this.Location = location;
            RaisePropertyChanged("Location");
        }

          internal void SetDirty(bool value)
          {
              this.IsDirty = value;
          }

        #endregion
    }


}
