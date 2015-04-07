using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Properties.Interface;
using Wide.Interfaces.Services;
using Wide.Core.Services;
using OIDE.Scene.Model;

namespace OIDE.Scene.Interface.Services
{
    public interface ISceneItem : IItem
    {
        /// <summary>
        /// subitems of this scenenode
        /// </summary>
        CollectionOfISceneItem SceneItems { get; }

        ISceneItem SelectedItem { get; set; }


     //   Int32 NodeID { get; set; }

       // TreeNode TreeNode { get; }
        //Boolean Visible { get; set; }
        //Boolean Enabled { get; set; }
    }
}
