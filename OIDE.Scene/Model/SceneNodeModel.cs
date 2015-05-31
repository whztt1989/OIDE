using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using Microsoft.Practices.Unity;
using Module.Properties.Helpers;
using Module.Properties.Interface;
using Module.Protob.Utilities;
using OIDE.Scene.Interface.Services;
using Wide.Interfaces;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using OIDE.Scene.Model.Objects;
using WIDE_Helpers;
using System.Windows;
using Module.Protob.Interface;
using OIDE.InteropEditor.DLL;
using FlatBuffers;
using OIDE.Scene.Model.Objects.FBufferObject;
using OIDE.Scene.Interface;
using Wide.Interfaces.Services;
using Wide.Core.Services;
using OIDE.Scene.Service;
using Module.DB.Interface.Services;
using OIDE.IDAL;

namespace OIDE.Scene.Model
{
    [Serializable]
    public class SceneNodeModel : SceneItem
    {
        FB_SceneNode m_FB_SceneNode; 

        #region UI Properties

        public Boolean IsVisible { get { return m_FB_SceneNode.IsVisible; } set { m_FB_SceneNode.IsVisible = value; RaisePropertyChanged("IsVisible"); } }
    
        [ExpandableObject]
        public Quaternion Rotation { get { return m_FB_SceneNode.Rotation; }
            set {
                m_FB_SceneNode.Rotation = value;
                RaisePropertyChanged("Rotation"); } }
        [ExpandableObject]
        public Vector3 Location { get { return m_FB_SceneNode.Location; } set { m_FB_SceneNode.Location = value; RaisePropertyChanged("Location"); } }
        [ExpandableObject]
        public Vector3 Scale { get { return m_FB_SceneNode.Scale; } set { m_FB_SceneNode.Scale = value; RaisePropertyChanged("Scale"); } }
        
        #endregion

        #region DBProperties

        private long m_EntityID;

        public long EntityID { get { return m_EntityID; } set { m_EntityID = value; RaisePropertyChanged("EntityID"); } }

        private IDAL.MDB.SceneNode m_SceneNodeDB;


        [XmlIgnore]
        [Browsable(false)]
        public IDAL.MDB.SceneNode SceneNodeDB
        {
            get { if (m_SceneNodeDB == null) m_SceneNodeDB = new IDAL.MDB.SceneNode(); return m_SceneNodeDB; }
            set 
            {
                m_SceneNodeDB = value;
                ContentID = "NodeID:##:" + value.NodeID;
                //  m_FB_SceneNodeModel.Read(value.Data); //date is only loaded from serialized objects not flatbuffer data! 
            }
        }

        #endregion

        public void Drop(IItem item)
        {

        }

        [Browsable(false)]
        public CollectionOfISceneItem SceneItems { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public override List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem miSave = new MenuItem() {  Header = "IsVisible" };
                list.Add(miSave);

                MenuItem miDelete = new MenuItem() { Command = this.mCmdDeleteNode, Header = "Delete Node" };
                list.Add(miDelete);

                return list;
            }
        }


        public Boolean Create(IUnityContainer unityContainer) {

            base.m_DBService = unityContainer.Resolve<IDatabaseService>();
        
            return true; }
        public Boolean Closing() { return true; }

        public Boolean Open(IUnityContainer unityContainer, object id)
        {
            uint sceneID = 0;

            base.m_DBService = unityContainer.Resolve<IDatabaseService>();
        
            //done in scene open!


            //if (uint.TryParse(id.ToString(), out sceneID) && sceneID > 0) //scenedata already loaded with scene
            //{
            //    NodeID = (int)SceneNodeDB.NodeID;
            //    var loaded  = Helper.Utilities.USystem.XMLSerializer.Deserialize<FB_SceneNode>("Scene/" + sceneID + "/Nodes/" + SceneNodeDB.NodeID + ".xml");
            //    if (loaded != null) // catch null reference if file not found
            //        m_FB_SceneNode = loaded;
            //}
            //else
            //{
            //    //todo load node directly from xml file - what is nodeid and scenenode here? .. not a use case        
            //}
       
            return true;
        }



        public Boolean SaveToDB()
        {
            String DBPath = DBFileUtil.GetDBFilePath(this.Parent);
            if (!String.IsNullOrEmpty(DBPath))
            {
                m_SceneNodeDB = new IDAL.MDB.SceneNode();

                m_SceneNodeDB.Data = m_FB_SceneNode.CreateByteBuffer();

          

                m_SceneNodeDB.Name = Name;
                m_SceneNodeDB.SceneID = m_SceneID;
                m_SceneNodeDB.NodeID = NodeID;
                m_SceneNodeDB.EntID = EntityID;

                //save sceneNode to db
                if (NodeID > 0)
                    IDAL.IDAL.updateSceneNode(DataContext, m_SceneNodeDB);
            }

            return true;
        }

        private long m_SceneID;

        public Boolean Save(object param) 
        {
            m_SceneID = 0;
            if (!long.TryParse(param.ToString(), out m_SceneID))
            {
                MessageBox.Show("error in ScenNodeModel.Save -> sceneID invalid");
                return false;
            }
            if (NodeID < 1)
            {
                MessageBox.Show("error in ScenNodeModel.Save -> NodeID '" + NodeID + "' invalid");
                return false;
            }

            SaveToDB();
      

          
            //else
            //    m_DBI.insertSceneNode(m_SceneNodeDB);

              //done in scene save!
  
            //Helper.Utilities.USystem.XMLSerializer.Serialize<FB_SceneNode>(m_FB_SceneNode,"Scene/" + sceneID + "/Nodes/" + NodeID + ".xml"); //ProtoSerialize.Deserialize<ProtoType.Node>(node.Data);

            return true; 
        }

        public void Refresh() { }
        public void Finish() { }

        private int m_NodeID;

        public int NodeID
        {
            get { return m_NodeID; }
            set {
                m_NodeID = value; RaisePropertyChanged("NodeID"); 
            } }

        public Boolean Delete()
        {
          //  m_model.Items.Clear();
            (Parent as ISceneItem).SceneItems.Remove(this);

            IDAL.IDAL.DeleteSceneNode(DataContext, NodeID);

            return true; 
        }

 
        private CmdDeleteNode mCmdDeleteNode;

        public SceneNodeModel()//IScene parent, IUnityContainer container, IDAL  dbi)
        {
            mCmdDeleteNode = new CmdDeleteNode(this);
         
            m_FB_SceneNode = new FB_SceneNode();
            m_FB_SceneNode.Rotation = new Quaternion();
            m_FB_SceneNode.Location = new Vector3();
            m_FB_SceneNode.Scale = new Vector3();
        }

    }

    public class CmdDeleteNode : ICommand
    {
        private SceneNodeModel m_SceneNodeEntity;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_SceneNodeEntity.Delete();
        }

        public CmdDeleteNode(SceneNodeModel som)
        {
            m_SceneNodeEntity = som;
        }
    }
}
