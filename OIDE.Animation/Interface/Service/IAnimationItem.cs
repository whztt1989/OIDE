using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Properties.Interface;
using System.Windows.Shapes;
using Wide.Interfaces.Services;

namespace OIDE.Animation.Interface.Services
{
    public enum AnimationType
    {
        Font,
        Sprite,
        SpriteAnim
    }

    public interface IAnimationItem : IItem
    {
        /// <summary>
        /// subitems of this sceneitem
        /// </summary>
      //  ObservableCollection<IAnimationItem> AnimationItems { get; }
        AnimationType AnimationType { get; }
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
