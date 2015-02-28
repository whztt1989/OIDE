using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using Wide.Interfaces;

namespace OIDE.Scene.Interface.Services
{
    public class Vector3 : ViewModelBase
    {
        private float m_X;
        private float m_Y;
        private float m_Z;


        public float X { get { return m_X; } set { m_X = value; RaisePropertyChanged("X"); } }
        public float Y { get { return m_Y; } set { m_Y = value; RaisePropertyChanged("Y"); } }
        public float Z { get { return m_Z; } set { m_Z = value; RaisePropertyChanged("Z"); } }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", X, Y, Z);
        }
    }

    public struct Quaternion
    {
        public float W { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }

    public interface IScene : IItem
    {
        ObservableCollection<ISceneItem> SceneItems { get; set; }

        DAL.MDB.Scene DB_SceneData { get; }

        bool AddItem(ISceneItem item);

        ISceneItem SelectedItem { get; set; }
    }
}
