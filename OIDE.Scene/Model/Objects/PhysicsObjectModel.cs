using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using OIDE.Scene.Interface.Services;
using PInvokeWrapper.DLL;
using Module.Properties.Interface;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Xml.Serialization;
using OIDE.DAL;
using Microsoft.Practices.Unity;

namespace OIDE.Scene.Model
{
    public class PhysicsObjectModel : ISceneItem
    {
        ICommand CmdSave;
        public String ContentID { get; set; }

        [XmlIgnore]
        public IItem Parent { get; private set; }
        public Boolean Visible { get; set; }
        public Boolean Enabled { get; set; }

        public Int32 ID { get; set; }
        [XmlAttribute]
        public String Name { get; set; }

        public ObservableCollection<ISceneItem> SceneItems { get; private set; }
        [Browsable(false)]
        public CollectionOfIItem Items { get; set; }

        public TreeNode TreeNode { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public List<MenuItem> MenuOptions { get {
            List<MenuItem> list = new List<MenuItem>();
            MenuItem miSave = new MenuItem() { Command = CmdSave, Header = "Save"};
            list.Add(miSave);
            return list;
        }}

        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsExpanded { get; set; }
        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsSelected { get; set; }

        [XmlIgnore]
        public Boolean HasChildren { get { return SceneItems != null && SceneItems.Count > 0 ? true : false; } }

        private gameentity.PhysicsObject mData;

        [Category("Conections")]
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public gameentity.PhysicsObject Data
        {
            get
            {

                return mData;
            }
            set { mData = value; }
        }
        //private object mtest;

        //[Editor(typeof(ByteArrayUserControlEditor), typeof(ByteArrayUserControlEditor))]
        //public object test
        //{
        //    get { return mtest; }
        //    set
        //    {
                
        //            //Console.WriteLine("setted:" + BitConverter.ToString(value));
        //            mtest = value;
                
        //    }
        //}
        public PhysicsObjectModel() { }


        public Boolean Open() { return true; }
        public Boolean Save() { return true; }
        public Boolean Delete() { return true; }

        public IUnityContainer UnityContainer { get; private set; }

        public PhysicsObjectModel(IItem parent,IUnityContainer container,Int32 id = -1)
       //     : base(commandManager, menuService)
        {
            UnityContainer = container;
            Parent = parent;

            ID = id;
            IDAL dbI = new IDAL();
            Byte[] res = dbI.selectPhysics(id);
            Console.WriteLine(BitConverter.ToString(res));
            try
            {
                using (MemoryStream stream = new MemoryStream(res))
                {
                    mData = ProtoBuf.Serializer.Deserialize<gameentity.PhysicsObject>(stream);
                }
            }catch
            {
                mData = new gameentity.PhysicsObject();
            }
            //using (StreamReader outputStream = new StreamReader("DataFile.dat"))
            //{
            //    // read from a file
            //    mData = ProtoBuf.Serializer.Deserialize<Person>(outputStream.BaseStream);
            //}

            CmdSave = new CmdSave(this);
           //  mtest = new Byte[10];
            Items = new CollectionOfIItem();
            SceneItems = new ObservableCollection<ISceneItem>();
        }
    }


    public class CmdSave : ICommand
    {
        private PhysicsObjectModel mpm;
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
                    dbI.updatePhysics(mpm.ID, inputStream.ToArray());
                else
                    dbI.insertPhysics(mpm.ID, inputStream.ToArray());
            }

            DLL_Singleton.Instance.consoleCmd("cmd physic 0"); //.updateObject(0, (int)ObjType.Physic);
        }

        public CmdSave(PhysicsObjectModel pm)
        {
            mpm = pm;
        }
    }
}