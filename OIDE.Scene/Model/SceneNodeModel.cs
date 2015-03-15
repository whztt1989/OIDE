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
using DAL;
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

namespace OIDE.Scene.Model
{
    public class SceneNodeModel : ViewModelBase, ISceneItem
    {
        FB_SceneNode m_FB_SceneNode; 

        #region UI Properties

        public Boolean IsVisible { get { return m_FB_SceneNode.IsVisible; } set { m_FB_SceneNode.IsVisible = value; RaisePropertyChanged("IsVisible"); } }
        public Boolean IsEnabled { get { return m_FB_SceneNode.IsEnabled; } set { m_FB_SceneNode.IsEnabled = value; RaisePropertyChanged("IsEnabled"); } }
        [ExpandableObject]
        public Quaternion Rotation { get { return m_FB_SceneNode.Rotation; } set { m_FB_SceneNode.Rotation = value; RaisePropertyChanged("Rotation"); } }
        [ExpandableObject]
        public Vector3 Location { get { return m_FB_SceneNode.Location; } set { m_FB_SceneNode.Location = value; RaisePropertyChanged("Location"); } }
        [ExpandableObject]
        public Vector3 Scale { get { return m_FB_SceneNode.Scale; } set { m_FB_SceneNode.Scale = value; RaisePropertyChanged("Scale"); } }
        
        #endregion

        #region DBProperties

        private DAL.MDB.SceneNode m_SceneNodeDB;

        public long EntityID { get { return (long)m_SceneNodeDB.EntID; } set { m_SceneNodeDB.EntID = value; RaisePropertyChanged("EntityID"); } }

        public DAL.MDB.SceneNode SceneNodeDB
        {
            get {    return m_SceneNodeDB; }
            set 
            {
                m_SceneNodeDB = value;
                ContentID = "NodeID:##:" + value.NodeID;
                //  m_FB_SceneNodeModel.Read(value.Data); //date is only loaded from serialized objects not flatbuffer data! 
            }
        }

        #endregion


        public String ContentID { get; set; }

        public void Drop(IItem item)
        {

        }

        private string m_Name;
        public String Name { get { return m_Name; } set { m_Name = value; RaisePropertyChanged("Name"); } }

        [Browsable(false)]
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }


        [Browsable(false)]
        public CollectionOfIItem Items { get; set; }

        [Browsable(false)]
        public List<MenuItem> MenuOptions
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

        [Browsable(false)]
        public IItem Parent { get; private set; }

        [Browsable(false)]
        public Boolean IsExpanded { get; set; }

        [Browsable(false)]
        public Boolean IsSelected { get; set; }

        [Browsable(false)]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        public Boolean Create() { return true; }
        public Boolean Closing() { return true; }

        public Boolean Open(object id)
        {
            uint sceneID = 0;
            if (uint.TryParse(id.ToString(), out sceneID) && sceneID > 0) //scenedata already loaded with scene
            {
                NodeID = (int)SceneNodeDB.NodeID;
                var loaded  = Helper.Utilities.USystem.XMLSerializer.Deserialize<FB_SceneNode>("Scene/" + sceneID + "/Nodes/" + SceneNodeDB.NodeID + ".xml");
                if (loaded != null) // catch null reference if file not found
                    m_FB_SceneNode = loaded;
            }
            else
            {
                //todo load node directly from xml file - what is nodeid and scenenode here? .. not a use case        
            }
       
            return true;
        }

        public Boolean Save(object param) 
        {
            m_SceneNodeDB.Data = m_FB_SceneNode.CreateByteBuffer();

            long sceneID = 0;
            if(!long.TryParse(param.ToString(), out sceneID))
            {
                MessageBox.Show("error in ScenNodeModel.Save -> sceneID invalid");
                return false;
            }
            if (NodeID < 1)
            {
                MessageBox.Show("error in ScenNodeModel.Save -> NodeID '" + NodeID + "' invalid");
                return false;
            }

            m_SceneNodeDB.Name = Name;
            m_SceneNodeDB.SceneID = sceneID;
            m_SceneNodeDB.NodeID = NodeID;

            //save sceneNode to db
            if (NodeID > 0)
                m_DBI.updateSceneNode(m_SceneNodeDB);
            //else
            //    m_DBI.insertSceneNode(m_SceneNodeDB);

            Helper.Utilities.USystem.XMLSerializer.Serialize<FB_SceneNode>(m_FB_SceneNode,"Scene/" + sceneID + "/Nodes/" + NodeID + ".xml"); //ProtoSerialize.Deserialize<ProtoType.Node>(node.Data);

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
            (Parent as IScene).SceneItems.Remove(this);

            m_DBI.DeleteSceneNode(NodeID);

            return true; 
        }

        private IDAL m_DBI;

        [Browsable(false)]
        public IUnityContainer UnityContainer { get; private set; }

        private CmdDeleteNode mCmdDeleteNode;

        public SceneNodeModel(IScene parent, IUnityContainer container, IDAL  dbi)
        {
            this.Parent = parent;

            if(dbi == null)
                m_DBI = new IDAL();
            else
                m_DBI = dbi;

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
