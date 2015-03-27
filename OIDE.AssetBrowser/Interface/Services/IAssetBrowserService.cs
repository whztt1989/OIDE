using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Module.Properties.Interface;
using Wide.Interfaces;
using Wide.Interfaces.Controls;
using Wide.Interfaces.Services;

namespace OIDE.AssetBrowser.Interface.Services
{
    /// <summary>
    /// Interface IToolbarService - the application's toolbar tray is returned by this service
    /// </summary>
    public interface IAssetBrowserTreeService
    {
        /// <summary>
        /// contextmenu of the project explorer
        /// </summary>
        System.Windows.Controls.ContextMenu ContextMenu { get; }

        /// <summary>
        /// The list of themes registered with the theme manager
        /// </summary>
        /// <value>The themes.</value>
        CollectionOfIItem Items { get; set; }

        /// <summary>
        /// AssetBrowser View TreeList Component
        /// </summary>
        TreeList TreeList { get; set; }

        /// <summary>
        /// Adds a theme to the theme manager
        /// </summary>
        /// <param name="theme">The theme to add</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise</returns>
        bool AddItem(IItem item);

        /// <summary>
        /// Called to set the current theme from the list of themes
        /// </summary>
        /// <param name="name">The name of the theme</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise</returns>
       // bool SetCurrent(Guid guid);

        /// <summary>
        /// selected item
        /// </summary>
        IItem SelectedItem { get; set; }

        /// <summary>
        /// Returns the current item set in the project/file manager
        /// </summary>
        /// <value>The current item.</value>
     //   IItem RootItem { get; }

        /// <summary>
        /// Gets the right click menu.
        /// </summary>
        /// <value>The right click menu.</value>
        AbstractMenuItem RightClickMenu { get; }

        /// <summary>
        /// Set item as root item
        /// </summary>
        /// <param name="item">item</param>
        void SetAsRoot(IItem item);
    }

}
