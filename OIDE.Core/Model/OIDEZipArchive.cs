using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using OIDE.VFS;

namespace OIDE.Core.Model
{
    class OIDEZipArchive : VFS_ZipModel
    {

        //todo OnExpand Event .. to load zip file not in constructor?
        public OIDEZipArchive(IItem parent, IUnityContainer container , String filepath)
        {
            FilePath = filepath;



        }
    }
}
