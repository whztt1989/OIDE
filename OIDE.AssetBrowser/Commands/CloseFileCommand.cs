using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces;

namespace OIDE.AssetBrowser.Commands
{
    class CloseFileCommand
    {
        IUnityContainer mContainer;

        public CloseFileCommand(IUnityContainer container)
        {
            mContainer = container;
        }

        public void OnSubmit(object arg)
        {
            IWorkspace workspace = mContainer.Resolve<AbstractWorkspace>();
            if (workspace.ActiveDocument != null)
            {
                //AssetBrowserModel tmp = workspace.ActiveDocument.Model as AssetBrowserModel;
               // tmp.Status = tmp.ECI.CloseEC();
            }
        }

        public bool CanSubmit(object arg)
        {

            return true;

        }


    }
}
