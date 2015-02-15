using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;

namespace OIDE.Scene.Interface.Services
{
    public struct Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
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
