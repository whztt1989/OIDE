using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Properties.Interface;
using System.Windows.Shapes;
using Wide.Interfaces.Services;
using Wide.Core.Services;

namespace OIDE.Gorilla.Interface.Services
{
    public enum GorillaType
    {
        Font,
        Sprite,
        SpriteAnim
    }

    public interface IGorillaItem : IItem
    {
        /// <summary>
        /// subitems of this sceneitem
        /// </summary>
      //  ObservableCollection<IGorillaItem> GorillaItems { get; }
        GorillaType GorillaType { get; }
        Rectangle Rectangle { get; set; }
       // Int32 NodeID { get; set; }

        Double X { get; }
        Double Y { get; }
        Double Width { get; }
        Double Height { get; }


       // TreeNode TreeNode { get; }
        //Boolean Visible { get; set; }
        //Boolean Enabled { get; set; }
    }
}
