using Module.PFExplorer.Service;
using OIDE.Scene.Interface;
using OIDE.Scene.Interface.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OIDE.Scene.Service
{
    public class SceneItem : ProjectItemModel, ISceneItem
    {

        [XmlIgnore]
        [Browsable(false)]
        public CollectionOfISceneItem SceneItems { get; private set; }

        public SceneItem()
        {
            SceneItems = new CollectionOfISceneItem();
         
        }

    }
}
