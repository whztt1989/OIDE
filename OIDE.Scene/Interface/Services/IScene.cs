using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Properties.Interface;

namespace OIDE.Scene.Interface.Services
{
    public interface IScene : IItem
    {
        ObservableCollection<ISceneItem> SceneItems { get; }

        TreeList TreeList { get; set; }

        bool AddItem(ISceneItem item);

        ISceneItem SelectedItem { get; set; }

        /// <summary>
        /// Returns the current item set in the project/file manager
        /// </summary>
        /// <value>The current item.</value>
        ISceneItem RootItem { get; set; }


    }
}
