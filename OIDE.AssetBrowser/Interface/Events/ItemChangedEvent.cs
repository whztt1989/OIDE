using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces.Services;

namespace OIDE.AssetBrowser.Interface.Events
{
    /// <summary>
    /// Class ThemeChangeEvent - This event happens when a theme is changed.
    /// </summary>
    public class ItemChangeEvent : CompositePresentationEvent<IItem>
    {

    }
}
