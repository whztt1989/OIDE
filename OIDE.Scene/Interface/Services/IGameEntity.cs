using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIDE.Scene.Interface.Services
{
    public interface IGameEntity
    {
        object DBData { get; }
        object ProtoData { get; }
    }
}
