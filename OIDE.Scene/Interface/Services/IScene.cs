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

    }
}
