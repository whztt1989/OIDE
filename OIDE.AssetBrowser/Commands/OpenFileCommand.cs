using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wide.Interfaces;

namespace OIDE.AssetBrowser.Commands
{
    class OpenCOMCommand 
    {
        IUnityContainer mContainer;

        public OpenCOMCommand(IUnityContainer container)
        {
            mContainer = container;
        }

        public void OnSubmit(object arg)
        {
            IWorkspace workspace = mContainer.Resolve<AbstractWorkspace>();
            if (workspace.ActiveDocument != null)
            {
              //  AssetBrowserModel tmp = workspace.ActiveDocument.Model as AssetBrowserModel;
                //tmp.Status = tmp.ECI.OpenEC(tmp.COM, tmp.Baud);
            }

        //    Console.WriteLine("Executed!!");
        }

        public bool CanSubmit(object arg) 
        {
            
            return true;
        
        }


    }
}
