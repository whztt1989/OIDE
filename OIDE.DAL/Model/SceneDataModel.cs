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
using Module.Properties.Interface;
using Wide.Interfaces.Services;

namespace OIDE.DAL.Model
{
    public class CmdCreateFile : ICommand
    {
        private SceneDataModel m_model;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //  m_model.Items.Add(new CameraModel(m_model) { Name = "Camera 1" });
            //IDAL dbI = new IDAL();

            //// To serialize the hashtable and its key/value pairs,  
            //// you must first open a stream for writing. 
            //// In this case, use a file stream.
            //using (MemoryStream inputStream = new MemoryStream())
            //{
            //    // write to a file
            //    ProtoBuf.Serializer.Serialize(inputStream, mpm.Data);

            //    if (mpm.ID > -1)
            //        dbI.updatePhysics(mpm.ID, inputStream.ToArray());
            //    else
            //        dbI.insertPhysics(mpm.ID, inputStream.ToArray());
            //}

            //DLL_Singleton.Instance.updateObject(0, (int)ObjType.Physic);
        }

        public CmdCreateFile(SceneDataModel model)
        {
            m_model = model;
        }
    }

    public class CmdDeleteScene : ICommand
    {
        private SceneDataModel m_model;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_model.Items.Clear();
            m_model.Parent.Items.Remove(m_model);
            //IDAL dbI = new IDAL();

            //// To serialize the hashtable and its key/value pairs,  
            //// you must first open a stream for writing. 
            //// In this case, use a file stream.
            //using (MemoryStream inputStream = new MemoryStream())
            //{
            //    // write to a file
            //    ProtoBuf.Serializer.Serialize(inputStream, mpm.Data);

            //    if (mpm.ID > -1)
            //        dbI.updatePhysics(mpm.ID, inputStream.ToArray());
            //    else
            //        dbI.insertPhysics(mpm.ID, inputStream.ToArray());
            //}

            //DLL_Singleton.Instance.updateObject(0, (int)ObjType.Physic);
        }

        public CmdDeleteScene(SceneDataModel model)
        {
            m_model = model;
        }
    }

    /// <summary>
    /// Complete Scene description
    /// </summary>
    public class SceneDataModel : IItem
    {
       
        private CollectionOfIItem m_Items;
        ICommand m_cmdCreateFile;
        ICommand m_cmdDelete;

        public Int32 ID { get; set; }
        [XmlAttribute]
        public String Name { get; set; }
        [Browsable(false)]
        public CollectionOfIItem Items { get { return m_Items; }  set { m_Items = value; } }

        [XmlIgnore]
        public Guid Guid { get; private set; }
       
        [Browsable(false)]
        [XmlIgnore]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem miSave = new MenuItem() { Command = m_cmdCreateFile, Header = "Add File" };
                list.Add(miSave);
                MenuItem miDelete = new MenuItem() { Command = m_cmdDelete, Header = "Delete" };
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
        public Boolean HasChildren { get { return Items != null && Items.Count > 0 ? true : false; } }

        [XmlIgnore]
        public IItem Parent { get; private set; }
       

        #region Scene Data


        #endregion

        public SceneDataModel()
        {

        }

        public SceneDataModel(IItem parent, ICommandManager commandManager, IMenuService menuService,Int32 id = -1)
        {
            Parent = parent;
            m_Items = new CollectionOfIItem();
        }

    }
}
