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

namespace OIDE.Core.Model
{
    public class OIDEZipArchive : VFS_ZipModel
    {
        /// <summary>
        /// Default constructor for serialization
        /// </summary>
        public OIDEZipArchive()
        {

        }

        //todo OnExpand Event .. to load zip file not in constructor?
        public OIDEZipArchive(IItem parent, IUnityContainer container, String filepath)
            : base(parent , container)
        {
            FilePath = filepath;
            Name = Path.GetFileName(filepath);
            IsDirty = true;
        }
    }
}
