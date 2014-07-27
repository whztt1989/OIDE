using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using OIDE.VFS;
using System.IO;
using System.Windows.Input;
using OIDE.VFS.VFS_Types.RootFileSystem;

namespace OIDE.Core.Model
{
    public class OIDE_RFS : RootVFS
    {
        /// <summary>
        /// Default constructor for serialization
        /// </summary>
        public OIDE_RFS()
        {

        }

        //todo OnExpand Event .. to load zip file not in constructor?
        public OIDE_RFS(IItem parent, IUnityContainer container)
            : base(parent , container)
        {

            IsDirty = true;
        }
    }
}
