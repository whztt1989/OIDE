using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Properties.Interface;

namespace OIDE.Scene.Interface.Services
{
    public interface ISceneItem : IItem
    {
        Boolean Visible { get; set; }
        Boolean Enabled { get; set; }
    }
}
