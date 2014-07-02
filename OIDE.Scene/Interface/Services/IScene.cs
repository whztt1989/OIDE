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
    public interface IScene : IItem
    {
        ObservableCollection<ISceneItem> SceneItems { get; }
   
        bool AddItem(ISceneItem item);

        ISceneItem SelectedItem { get; set; }
    }
}
