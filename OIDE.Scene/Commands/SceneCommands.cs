using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Module.History.Service;
using DAL;
using OIDE.Scene.Model;

namespace OIDE.Scene.Commands
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

            m_model.Delete();
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

    public class CmdSaveScene : ICommand
    {
        private SceneDataModel mpm;
        public event EventHandler CanExecuteChanged;

        //public bool CanRedo() { return true; }
        //public bool CanUndo() { return true; }
        //public void Redo() { }
        //public string ShortMessage() { return "Save Scene"; }
        //public void Undo() { }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mpm.Save(parameter);

        }

        public CmdSaveScene(SceneDataModel pm)
        {
            mpm = pm;
        }
    }
}
