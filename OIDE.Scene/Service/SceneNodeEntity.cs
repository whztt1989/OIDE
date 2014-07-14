﻿using System;
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
using OIDE.DAL;
using OIDE.Scene.Interface.Services;
using ProtoType;
using Wide.Interfaces;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace OIDE.Scene.Service
{
    public class SceneNodeEntity : ViewModelBase, ISceneNode , ISceneItem
    {
        public String ContentID { get; set; }

        public void Drop(IItem item) { }

        [XmlIgnore]
        [Browsable(false)]
        public IItem Parent { get; private set; }
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public ProtoType.Node Node { get; set; }

        private OIDE.DAL.MDB.SceneNodes mSceneNode;

        [XmlIgnore]
        public long EntityID { get { return (long)mSceneNode.EntID; } set { mSceneNode.EntID = value;  RaisePropertyChanged("EntityID"); } }

        [XmlIgnore]
        [ExpandableObject]
        public Quat4f Rotation { get { return Node.transform.rot; } set { Node.transform.rot = value; RaisePropertyChanged("Rotation"); } }
        [XmlIgnore]
        [ExpandableObject]
        public Vec3f Location { get { return Node.transform.loc; } set { Node.transform.loc = value; RaisePropertyChanged("Location"); } }
        [XmlIgnore]
        [ExpandableObject]
        public Vec3f Scale { get { return Node.transform.scl; } set { Node.transform.scl = value; RaisePropertyChanged("Scale"); } }

        [XmlIgnore]
        public OIDE.DAL.MDB.SceneNodes SceneNode { get { return mSceneNode; }
            set 
            { 
                mSceneNode = value;
                ContentID = "NodeID:##:" + value.NodeID;

                if (value.Data == null)
                {
                    Node = new ProtoType.Node();
                    Node.transform = new TransformStateData();
                    Node.transform.scl = new Vec3f();
                    Node.transform.loc = new Vec3f();
                    Node.transform.rot = new Quat4f();
                }
                else
                    Node = ProtoSerialize.Deserialize<ProtoType.Node>(value.Data);
            }
        }



        [XmlAttribute]
        public String Name { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<ISceneItem> SceneItems { get; private set; }


        [Browsable(false)]
        public CollectionOfIItem Items { get; set; }

        [Browsable(false)]
        [XmlIgnore]
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
        [XmlAttribute]
        public Boolean IsExpanded { get; set; }

        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsSelected { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        public Boolean Create() { return true; }
        public Boolean Open()
        {

           
            return true;
        }
        public Boolean Save()
        {
            return true;
        }

        public Boolean Delete()
        {
          //  m_model.Items.Clear();
            (Parent as IScene).SceneItems.Remove(this);

            m_DBI.DeleteSceneNode(Helper.StringToContentIDData(ContentID).IntValue);

            return true; 
        }

        private IDAL m_DBI;


        [XmlIgnore]
        [Browsable(false)]
        public IUnityContainer UnityContainer { get; private set; }

        private CmdDeleteNode mCmdDeleteNode;

        public SceneNodeEntity(IScene parent, IUnityContainer container, IDAL  dbi)
        {
            this.Parent = parent;

            if(dbi == null)
                m_DBI = new IDAL();
            else
                m_DBI = dbi;

            mCmdDeleteNode = new CmdDeleteNode(this);
        }

    }

    public class CmdDeleteNode : ICommand
    {
        private SceneNodeEntity m_SceneNodeEntity;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_SceneNodeEntity.Delete();
        }

        public CmdDeleteNode(SceneNodeEntity som)
        {
            m_SceneNodeEntity = som;
        }
    }
}