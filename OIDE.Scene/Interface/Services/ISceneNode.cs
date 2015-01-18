using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


    public interface ISceneNode
    {
        /// <summary>
        /// scenenode from database
        /// </summary>
        DAL.MDB.SceneNode SceneNode { get; }
                    
        /// <summary>
        /// deserialized node data
        /// </summary>
        Byte[] ByteBuffer { get; }
       // ProtoType.Node Node { get; set; }
    }
}
