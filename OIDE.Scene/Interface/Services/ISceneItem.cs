using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Properties.Interface;

namespace OIDE.Scene.Interface.Services
{
    public interface ISceneItem : IItem
    {
        /// <summary>
        /// subitems of this sceneitem
        /// </summary>
        ObservableCollection<ISceneItem> SceneItems { get; }

        TreeNode TreeNode { get; }
        //Boolean Visible { get; set; }
        //Boolean Enabled { get; set; }
    }
}
