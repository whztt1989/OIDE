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
using OIDE.Scene.Commands;
using OIDE.Scene.Interface.Services;
using Wide.Interfaces.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Module.Protob.Utilities;
using Module.History.Service;
using OIDE.Scene.Service;
using Wide.Interfaces;
using System.Windows.Media;
using FlatBuffers;
using OIDE.Scene.Model.Objects;
using OIDE.Scene.Model.Objects.FBufferObject;
using OIDE.Scene.Interface;
using Module.PFExplorer.Interface;
using Module.PFExplorer.Utilities;
using WIDE_Helpers;
using Module.DB.Interface.Services;
using OIDE.IDAL;

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
    public class SceneDataModel : SceneItem, IDBFileItem
    {
        private ICommand CmdDeleteScene;
        private ICommand CmdSaveScene;

        private ISceneService m_SceneService;
        private ISceneItem mSelectedItem;
        //ICommand m_cmdCreateFile;
        //ICommand m_cmdDelete;

        public Int32 NextNodeCount()
        {
            return SceneItems.Count(x => x is SceneNodeModel) + 1;
        }

        public override void Drop(IItem item)
        {
            try
            {
                if (item is ISceneItem)
                {
                    if(FB_SceneData == null)
                    {
                      //  MessageBox.Show
                    }

                    var sceneItem = item as ISceneItem;
                    createNewNode(sceneItem);
                   
                }
            }
            catch(Exception ex)
            {

            }
        }

        public SceneNodeModel createNewNode(ISceneItem sceneItem)
        {
            var newNode = new SceneNodeModel()
                       {
                           Parent = this,
                           UnityContainer = UnityContainer,
                           DataContext = DataContext,
                           NodeID = NextNodeCount(),
                           EntityID = Module.Properties.Helpers.Helper.StringToContentIDData(sceneItem.ContentID).IntValue,
                           Name = "NEWNode_" + sceneItem.Name ?? "Noname"
                       };

            FB_SceneData.SceneItems.Add(newNode);

            return newNode;
        }

        #region serializable data

        private FB_Scene m_FBData;// = new FB_Scene();

        [XmlIgnore]
        [ExpandableObject]
        public FB_Scene FB_SceneData
        {
            get { return m_FBData; }
        }

        #endregion

        [XmlIgnore]
        [ExpandableObject]
        public override CollectionOfISceneItem SceneItems { get { return m_FBData.SceneItems; } }

        public bool AddItem(ISceneItem item)
        {
            if (!SceneItems.Contains(item))
            {
                SceneItems.Add(item);
                return true;
            }
            return false;
        }

        [XmlIgnore]
        [Browsable(false)]
        public override ISceneItem SelectedItem
        {
            get
            {
                return mSelectedItem;
            }
            set
            {
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
    
        public long SceneID { get; set; }

                #region public command methods

        public override Boolean Create(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
            m_SceneService = unityContainer.Resolve<ISceneService>();
       
            base.m_DBService = unityContainer.Resolve<IDatabaseService>();
            DataContext.Context = ((IDAL.IDAL)m_DBService.CurrentDB).GetDataContextOpt(false);
       
            m_FBData = new FB_Scene() { UnityContainer = unityContainer, Parent = this };

            RaisePropertyChanged("FB_SceneData");

            return true;
        }

        public override Boolean Closing()
        {
       //     m_Opened = false;
            return true;
        }

      //  private Boolean m_Opened;

        private IProjectFile m_ParentProject;

        public override Boolean Open(IUnityContainer unityContainer, object id)
        {
            //if (m_Opened)
            //    return false;
            //else
            //    m_Opened = true;

            //get parent project
            m_ParentProject = PFUtilities.GetRekursivParentPF(this.Parent) as IProjectFile;

            if (m_ParentProject == null)
                return false;

            UnityContainer = unityContainer;
            m_SceneService = unityContainer.Resolve<ISceneService>();

            base.m_DBService = unityContainer.Resolve<IDatabaseService>();
            DataContext.Context = ((IDAL.IDAL)m_DBService.CurrentDB).GetDataContextOpt(false);
       
            int sceneID = Module.Properties.Helpers.Helper.StringToContentIDData(ContentID).IntValue;

            this.Location = ItemFolder + "\\" + sceneID + ".xml";
          
            //read sceneData from DAL -- Read data from XML not from database -> database data not human readable
            m_FBData = Helper.Utilities.USystem.XMLSerializer.Deserialize<FB_Scene>(this.Location.ToString()); // XML Serialize
            if (m_FBData == null)
                Create(unityContainer);
            else
                RaisePropertyChanged("FB_SceneData");

            RaisePropertyChanged("SceneItems");

            m_SceneService.SelectedScene = this;

       //only able to save data into database not load
            return true ;


       //     DB_SceneData = IDAL.selectSceneDataOnly(DataContext,sceneID); // database data

       ////     m_FB_SceneData.Read(DB_SceneData.Data); //just for testing if data correctly saved!


       //     IEnumerable<DAL.IDAL.SceneNodeContainer> result = IDAL.selectSceneNodes(DataContext, sceneID); //scenenodes from database

       //     try
       //     {
       //         //select all Nodes
       //         foreach (var nodeContainer in result)
       //         {
       //             //if(!SceneItems.Where(x => x.NodeID == nodeContainer.Node.NodeID).Any()) // add node to scene if not exists
       //             //{
       //             //    //var sceneNode = new SceneNodeModel(this, UnityContainer, m_DBI) { SceneNodeDB = nodeContainer.Node, Name = nodeContainer.Node.Name ?? "NodeNoname" };
       //             //    //sceneNode.Open(UnityContainer, sceneID);
       //             //    //SceneItems.Add(sceneNode);
       //             //}

       //             switch ((EntityTypes)nodeContainer.Entity.EntType)
       //             {
       //             //    case NodeTypes.Static:

       //             //        m_SceneItems.Add(new StaticObjectModel(this, UnityContainer, m_DBI)
       //             //        {
       //             //            ContentID = "StaticID:##:" + node.GameEntity.EntID,
       //             //            DBData =  node.GameEntity,
       //             //            Name = node.GameEntity.Name ?? ("StatNoname" + (int)node.GameEntity.EntID),
       //             //            Node = nodeDeserialized

       //             //        });// Data = gameEntityDataDeserialized });

       //             //        break;
       //             //    case NodeTypes.Physic:

       //             //        ProtoType.PhysicsObject dataPhysObj = new ProtoType.PhysicsObject();
       //             //        if (node.GameEntity.Data != null)
       //             //            dataPhysObj = ProtoSerialize.Deserialize<ProtoType.PhysicsObject>(node.GameEntity.Data);

       //             //        m_SceneItems.Add(new PhysicsObjectModel(this, UnityContainer, dataPhysObj, m_DBI)
       //             //        {
       //             //            ContentID = "PhysicID:##:" + node.GameEntity.EntID,
       //             //            Name = node.GameEntity.Name ?? ("PhysNoname" + (int)node.GameEntity.EntID),
       //             //            Node = nodeDeserialized
       //             //        });// Data = gameEntityDataDeserialized });

       //             //        break;

       //             //    //case NodeTypes.Physic:
       //             //    //    //  var itemCam = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == ""); // Search for Camera category
       //             //    //   // if (itemCam.Any())
       //             //    //        m_SceneItems.Add(new PhysicsObjectModel(this, UnityContainer , ) { Node = nodeDeserialized });

       //             //    //    break;
       //             //    //case NodeTypes.Static:

       //             //    //    break;
       //             //    case NodeTypes.Camera:
       //             //        //todo contentid for camera

       //             //        //   SceneNodes = new SceneNodes() { NodeID = sNode.NodeID, EntID = sNode.Node.EntityID, SceneID = ID, Data = ProtoSerialize.Serialize(sNode.Node) };


       //             //        var itemCam = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == ""); // Search for Camera category
       //             //        if (itemCam.Any())
       //             //            itemCam.First().SceneItems.Add(new CameraModel(itemCam.First(), UnityContainer) { Node = nodeDeserialized });

       //             //        break;
       //             //    case NodeTypes.Light:

       //             //        var itemLight = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == "");
       //             //        if (itemLight.Any())
       //             //            itemLight.First().SceneItems.Add(new LightModel(itemLight.First(), UnityContainer) { Node = nodeDeserialized });
       //             //        break;
       //                }

       //         }
       //     }
       //     catch (Exception ex)
       //     {
       //         //   m_SceneService.SelectedScene.SceneItems.Clear();
       //     }
       //     RaisePropertyChanged(null);
       //     return true;
        }

        public void Refresh() { }
        public void Finish() { }

        public Boolean SaveToDB()
        {
            String DBPath = DBFileUtil.GetDBFilePath(this.Parent);
            if (!String.IsNullOrEmpty(DBPath))
            {
                IDAL.MDB.Scene dbSceneData = new IDAL.MDB.Scene();
                dbSceneData .Data = m_FBData.CreateByteBuffer();
                dbSceneData.SceneID = SceneID;
                dbSceneData.Name = this.Name;

                IDAL.IDAL.insertScene(DataContext, dbSceneData);

             }
            return true;

        }

        public override Boolean Save(object param)
        {
            SaveToDB();
       
            this.Location = ItemFolder + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml";

            Helper.Utilities.USystem.XMLSerializer.Serialize<FB_Scene>(m_FBData, this.Location.ToString()); // XML Serialize
            
  
            //##   DLL_Singleton.Instance.consoleCmd("cmd sceneUpdate 0"); //.updateObject(0, (int)ObjType.Physic);

            //---------------------------
            //Nodes
            //------------------------------
            try
            {
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
                        sNode.DataContext = DataContext;
                        sNode.Parent = this;
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

        public override Boolean Delete()
        {
            IDAL.IDAL.DeleteScene(DataContext, Module.Properties.Helpers.Helper.StringToContentIDData(ContentID).IntValue);

            this.Location = ItemFolder + WIDE_Helper.StringToContentIDData(ContentID).IntValue + ".xml";
      
            File.Delete(this.Location.ToString());

            this.Parent.Items.Remove(this);

            return true;
        }
        
        #endregion
        


        public SceneDataModel()
        {
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
