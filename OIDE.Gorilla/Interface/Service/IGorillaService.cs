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

namespace OIDE.Gorilla.Interface.Services
{
    /// <summary>
    /// Interface IToolbarService - the application's toolbar tray is returned by this service
    /// </summary>
    public interface IGorillaService
    {
        System.Windows.Controls.ContextMenu ContextMenu { get; }

        ObservableCollection<IItem> PredefObjects { get; }

        /// <summary>
        /// The list of themes registered with the theme manager
        /// </summary>
        /// <value>The themes.</value>
        ObservableCollection<IGorilla> Gorillas { get; }

     //   TreeList TreeList { get; set; }

      //  IScene RootItem { get; }

      //  void SetAsRoot(IScene scene);

        bool AddGorilla(IGorilla gorilla);

        /// <summary>
        /// Called to set the current theme from the list of themes
        /// </summary>
        /// <param name="name">The name of the theme</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise</returns>
       // bool SetCurrent(Guid guid);

        IGorilla SelectedGorilla { get; set; }

        GorillaToolModel GTM { get; set; }
        /// <summary>
        /// Gets the right click menu.
        /// </summary>
        /// <value>The right click menu.</value>
        AbstractMenuItem RightClickMenu { get; }
    }
}
