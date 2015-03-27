using Module.PFExplorer;
using Module.Properties.Interface;
using OIDE.AssetBrowser;
using OIDE.Core.Model;
using OIDE.Scene.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Wide.Interfaces.Services;

namespace OIDE.Core.ProjectTypes.Model
{
    [XmlInclude(typeof(ScenesListModel))]
    [XmlInclude(typeof(FileCategoryModel))]
    [XmlInclude(typeof(FolderCategoryModel))]
    [XmlInclude(typeof(SceneDataModel))]
    [XmlInclude(typeof(OIDE_RFS))]
    [Serializable]
    public class OIDEProjectData
    {
        public String Name { get; set; }
        public CollectionOfIItem Items { get; set; }
        public Boolean IsExpanded { get; set; }
        public ObservableCollection<OIDEStateModel> GameStates { get; set; }

        public OIDEProjectData()
        {
            Items = new CollectionOfIItem();
            GameStates = new ObservableCollection<OIDEStateModel>();
        }
    }
}
