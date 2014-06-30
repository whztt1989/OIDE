using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using Module.Properties.Interface;
using OIDE.DAL;
using OIDE.Scene.Interface.Services;
using PInvokeWrapper.DLL;
using Wide.Interfaces.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace OIDE.Scene.Model
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
    public class SceneDataModel : ISceneItem, IScene
    {
       
        private CollectionOfIItem m_Items;
        ICommand m_cmdCreateFile;
        ICommand m_cmdDelete;

        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }

        public String ContentID { get { return "SceneViewer"; } }

        ICommand CmdSave;

        [Category("Conections")]
        [Description("This property is a complex property and has no default editor.")]
         [ExpandableObject]
        public ProtoType.Colour ColourAmbient { get { return mData.colourAmbient; } set { mData.colourAmbient = value; } }

        private ProtoType.Scene mData;

        [Category("Conections")]
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public ProtoType.Scene Data
        {
            get
            {
             
                return mData;
            }
            set { mData = value; }
        }

        private ObservableCollection<ISceneItem> m_SceneItems; 

       [XmlIgnore]
        public ObservableCollection<ISceneItem> SceneItems { get { return m_SceneItems; } private set { m_SceneItems = value; } }


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
                MenuItem miSave = new MenuItem() {Command = CmdSave, Header = "Save" };
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
        public Boolean HasChildren { get { return Items != null && Items.Count > 0 ? true : false; } }

        [XmlIgnore]
        public IItem Parent { get; private set; }
       

        #region Scene Data


        #endregion

        public Boolean Open() { return true; }
        public Boolean Save() { return true; }
        public Boolean Delete() { return true; }

        public SceneDataModel()
        {

        }

        public SceneDataModel(IItem parent, ICommandManager commandManager, IMenuService menuService,Int32 id = -1)
        {
            Parent = parent;

            ID = id;
            IDAL dbI = new IDAL();
            IEnumerable<OIDE.DAL.IDAL.SceneNodeData> result = dbI.selectScene(id);
          //  Console.WriteLine(BitConverter.ToString(res));
            try
            {
                Boolean SceneLoaded = false;

                foreach (OIDE.DAL.IDAL.SceneNodeData node in result)
                {
                    //achtung nur einmal!
                    if (!SceneLoaded)
                    {
                        using (MemoryStream stream = new MemoryStream(node.Scene.Data))
                        {
                            mData = ProtoBuf.Serializer.Deserialize<ProtoType.Scene>(stream);
                            SceneLoaded = true;
                        }
                    }

                    using (MemoryStream stream = new MemoryStream(node.Nodes.Data))
                    {
                        mData = ProtoBuf.Serializer.Deserialize<ProtoType.Scene>(stream);
                    }
                }
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


    public class CmdSaveScene : ICommand
    {
        private SceneDataModel mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            IDAL dbI = new IDAL();

            // To serialize the hashtable and its key/value pairs,  
            // you must first open a stream for writing. 
            // In this case, use a file stream.
            using (MemoryStream inputStream = new MemoryStream())
            {
                // write to a file
                ProtoBuf.Serializer.Serialize(inputStream, mpm.Data);

                if (mpm.ID > -1)
                    dbI.updateScene(mpm.ID, inputStream.ToArray());
                else
                    dbI.insertScene(mpm.ID, inputStream.ToArray());
            }

            DLL_Singleton.Instance.consoleCmd("cmd sceneUpdate 0"); //.updateObject(0, (int)ObjType.Physic);
        }

        public CmdSaveScene(SceneDataModel pm)
        {
            mpm = pm;
        }
    }
}
