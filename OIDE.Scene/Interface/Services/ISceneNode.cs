using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Properties.Interface;

namespace OIDE.Scene.Interface.Services
{
    public interface ISceneNode
    {
        /// <summary>
        /// scenenode from database
        /// </summary>
        OIDE.DAL.MDB.SceneNodes SceneNode { get; }
                    
        /// <summary>
        /// deserialized node data
        /// </summary>
        ProtoType.Node Node { get; set; }
    }
}
