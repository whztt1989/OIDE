using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using OIDE.Gorilla.Interface.Services;
using Wide.Interfaces;

namespace OIDE.Gorilla.Service
{
    public class GorillaItem : ViewModelBase, IGorillaItem
    {
        /// <summary>
        /// subitems of this sceneitem
        /// </summary>
        //ObservableCollection<GorillaItem> GorillaItems { get; }


        public GorillaItem(IGorilla gorilla, IUnityContainer container)
        {

        }

    }
}
